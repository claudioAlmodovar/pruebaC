using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ApuestasApp.Data;
using ApuestasApp.Models;

namespace ApuestasApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly BetRepository _repository = new BetRepository();
        private readonly BindingSource _bindingSource = new BindingSource();
        private bool _isPopulatingResults;
        private readonly Dictionary<int, Image?> _imageCache = new Dictionary<int, Image?>();

        public MainForm()
        {
            InitializeComponent();
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;
            dgvBets.AutoGenerateColumns = true;
            dgvBets.DataSource = _bindingSource;
            cmbResults.DisplayMember = nameof(ResultFilterOption.Display);
            cmbResults.ValueMember = nameof(ResultFilterOption.ResultValue);
            dgvBets.CellFormatting += dgvBets_CellFormatting;
            dgvBets.CellContentClick += dgvBets_CellContentClick;
            dgvBets.DataBindingComplete += dgvBets_DataBindingComplete;
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            LoadResults();
            LoadBets(dtpFrom.Value, dtpTo.Value);
        }

        private void btnFilter_Click(object? sender, EventArgs e)
        {
            LoadBets(dtpFrom.Value, dtpTo.Value);
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            using var editor = new BetForm();
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var bet = editor.Bet;
                    bet.Id = _repository.Add(bet);
                    LoadResults();
                    LoadBets(dtpFrom.Value, dtpTo.Value);
                    SelectBet(bet.Id);
                }
                catch (Exception ex)
                {
                    ShowError("No se pudo guardar la apuesta.", ex);
                }
            }
        }

        private void btnEdit_Click(object? sender, EventArgs e)
        {
            var selectedBet = GetSelectedBet();
            if (selectedBet == null)
            {
                MessageBox.Show(this, "Selecciona una apuesta para editar.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var editor = new BetForm(selectedBet);
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    _repository.Update(editor.Bet);
                    LoadResults();
                    LoadBets(dtpFrom.Value, dtpTo.Value);
                    SelectBet(editor.Bet.Id);
                }
                catch (Exception ex)
                {
                    ShowError("No se pudo actualizar la apuesta.", ex);
                }
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            var selectedBet = GetSelectedBet();
            if (selectedBet == null)
            {
                MessageBox.Show(this, "Selecciona una apuesta para eliminar.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(this, "¿Seguro que deseas eliminar la apuesta seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _repository.Delete(selectedBet.Id);
                LoadResults();
                LoadBets(dtpFrom.Value, dtpTo.Value);
            }
            catch (Exception ex)
            {
                ShowError("No se pudo eliminar la apuesta.", ex);
            }
        }

        private void LoadBets(DateTime from, DateTime to)
        {
            try
            {
                ClearImageCache();
                var bets = _repository.GetBetsByDateRange(from, to);
                if (cmbResults.SelectedItem is ResultFilterOption option && !option.IsAll)
                {
                    bets = option.FilterNull
                        ? bets.Where(bet => bet.Resultado == null).ToList()
                        : bets.Where(bet => string.Equals(bet.Resultado, option.ResultValue, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                _bindingSource.DataSource = new BindingList<Bet>(new List<Bet>(bets));
            }
            catch (Exception ex)
            {
                ShowError("No se pudieron cargar las apuestas.", ex);
            }
        }

        private void dgvBets_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            EnsureImageColumn();
        }

        private void EnsureImageColumn()
        {
            var columnName = nameof(Bet.Imagen);
            if (!dgvBets.Columns.Contains(columnName))
            {
                return;
            }

            if (dgvBets.Columns[columnName] is DataGridViewImageColumn imageColumn)
            {
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imageColumn.HeaderText = "Imagen";
                imageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                imageColumn.Width = 80;
                return;
            }

            var index = dgvBets.Columns[columnName].Index;
            dgvBets.Columns.RemoveAt(index);
            var newColumn = new DataGridViewImageColumn
            {
                Name = columnName,
                DataPropertyName = columnName,
                HeaderText = "Imagen",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 80
            };
            dgvBets.Columns.Insert(index, newColumn);
        }

        private void dgvBets_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dgvBets.Columns[e.ColumnIndex].DataPropertyName != nameof(Bet.Imagen))
            {
                return;
            }

            if (dgvBets.Rows[e.RowIndex].DataBoundItem is not Bet bet)
            {
                return;
            }

            e.Value = GetImageForBet(bet);
            e.FormattingApplied = true;
        }

        private void dgvBets_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dgvBets.Columns[e.ColumnIndex].DataPropertyName != nameof(Bet.Imagen))
            {
                return;
            }

            if (dgvBets.Rows[e.RowIndex].DataBoundItem is not Bet bet)
            {
                return;
            }

            var image = GetImageForBet(bet);
            if (image == null)
            {
                MessageBox.Show(this, "La apuesta seleccionada no tiene una imagen asociada.", "Imagen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var preview = new ImagePreviewForm(image);
            preview.ShowDialog(this);
        }

        private Image? GetImageForBet(Bet bet)
        {
            if (bet.Id == 0)
            {
                return ByteArrayToImage(bet.Imagen);
            }

            if (_imageCache.TryGetValue(bet.Id, out var cachedImage))
            {
                return cachedImage;
            }

            var image = ByteArrayToImage(bet.Imagen);
            _imageCache[bet.Id] = image;
            return image;
        }

        private void ClearImageCache()
        {
            foreach (var image in _imageCache.Values)
            {
                image?.Dispose();
            }

            _imageCache.Clear();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ClearImageCache();
            base.OnFormClosed(e);
        }

        private static Image? ByteArrayToImage(byte[]? data)
        {
            if (data == null || data.Length == 0)
            {
                return null;
            }

            using var memoryStream = new MemoryStream(data);
            using var image = Image.FromStream(memoryStream);
            return new Bitmap(image);
        }

        private Bet? GetSelectedBet()
        {
            return _bindingSource.Current as Bet;
        }

        private void SelectBet(int betId)
        {
            for (var index = 0; index < _bindingSource.Count; index++)
            {
                if (_bindingSource[index] is Bet bet && bet.Id == betId)
                {
                    _bindingSource.Position = index;
                    return;
                }
            }
        }

        private void ShowError(string message, Exception exception)
        {
            MessageBox.Show(this, $"{message}\n\n{exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSuggestions_Click(object? sender, EventArgs e)
        {
            using var suggestionsForm = new SuggestionsForm();
            suggestionsForm.ShowDialog(this);
        }

        private void LoadResults()
        {
            var previousOption = cmbResults.SelectedItem as ResultFilterOption;
            var options = new BindingList<ResultFilterOption>();
            options.Add(new ResultFilterOption("Todos", null, isAll: true));

            try
            {
                _isPopulatingResults = true;

                foreach (var result in _repository.GetDistinctResults())
                {
                    if (result == null)
                    {
                        options.Add(new ResultFilterOption("Sin resultado", null, filterNull: true));
                    }
                    else
                    {
                        var display = string.IsNullOrWhiteSpace(result) ? "(Vacío)" : result;
                        options.Add(new ResultFilterOption(display, result));
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("No se pudieron cargar los resultados.", ex);
            }
            finally
            {
                cmbResults.DataSource = options;
                ResultFilterOption? optionToSelect = null;

                if (previousOption != null && !previousOption.IsAll)
                {
                    optionToSelect = previousOption.FilterNull
                        ? options.FirstOrDefault(option => option.FilterNull)
                        : options.FirstOrDefault(option => !option.FilterNull && !option.IsAll &&
                            string.Equals(option.ResultValue, previousOption.ResultValue, StringComparison.OrdinalIgnoreCase));
                }

                cmbResults.SelectedItem = optionToSelect ?? options.First();
                _isPopulatingResults = false;
            }
        }

        private void cmbResults_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isPopulatingResults)
            {
                return;
            }

            LoadBets(dtpFrom.Value, dtpTo.Value);
        }

        private sealed class ResultFilterOption
        {
            public ResultFilterOption(string display, string? resultValue, bool filterNull = false, bool isAll = false)
            {
                Display = display;
                ResultValue = resultValue;
                FilterNull = filterNull;
                IsAll = isAll;
            }

            public string Display { get; }

            public string? ResultValue { get; }

            public bool FilterNull { get; }

            public bool IsAll { get; }
        }
    }
}
