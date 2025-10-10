using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ApuestasApp.Data;
using ApuestasApp.Models;

namespace ApuestasApp.Forms;

public partial class MainForm : Form
{
    private readonly BetRepository _repository = new();
    private readonly BindingSource _bindingSource = new();

    public MainForm()
    {
        InitializeComponent();
        dtpFrom.Value = DateTime.Today;
        dtpTo.Value = DateTime.Today;
        dgvBets.AutoGenerateColumns = true;
        dgvBets.DataSource = _bindingSource;
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
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
            var bets = _repository.GetBetsByDateRange(from, to);
            _bindingSource.DataSource = new BindingList<Bet>(new List<Bet>(bets));
        }
        catch (Exception ex)
        {
            ShowError("No se pudieron cargar las apuestas.", ex);
        }
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
}
