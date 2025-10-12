using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ApuestasApp.Data;
using ApuestasApp.Models;

namespace ApuestasApp.Forms;

public partial class SuggestionsForm : Form
{
    private readonly SuggestionRepository _repository = new();
    private readonly BindingSource _bindingSource = new();
    private List<Suggestion> _allSuggestions = new();

    public SuggestionsForm()
    {
        InitializeComponent();
        dgvSuggestions.AutoGenerateColumns = true;
        dgvSuggestions.DataSource = _bindingSource;
    }

    private void SuggestionsForm_Load(object? sender, EventArgs e)
    {
        LoadSuggestions();
    }

    private void LoadSuggestions()
    {
        try
        {
            var suggestions = _repository.GetAll();
            _allSuggestions = new List<Suggestion>(suggestions);
            ApplyFilter();
        }
        catch (Exception ex)
        {
            ShowError("No se pudieron cargar las sugerencias.", ex);
        }
    }

    private void ApplyFilter()
    {
        int? selectedId = null;
        if (_bindingSource.Count > 0 && _bindingSource.Current is Suggestion current)
        {
            selectedId = current.Id;
        }

        var filter = txtFilter.Text?.Trim();

        IEnumerable<Suggestion> filtered = _allSuggestions;
        if (!string.IsNullOrEmpty(filter))
        {
            filtered = _allSuggestions.Where(s => !string.IsNullOrEmpty(s.Dato) &&
                                                  s.Dato!.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        _bindingSource.DataSource = new BindingList<Suggestion>(filtered.ToList());

        if (selectedId.HasValue)
        {
            SelectSuggestion(selectedId.Value);
        }
    }

    private Suggestion? GetSelectedSuggestion()
    {
        if (_bindingSource.Count == 0)
        {
            return null;
        }

        return _bindingSource.Current as Suggestion;
    }

    private void SelectSuggestion(int suggestionId)
    {
        for (var index = 0; index < _bindingSource.Count; index++)
        {
            if (_bindingSource[index] is Suggestion suggestion && suggestion.Id == suggestionId)
            {
                _bindingSource.Position = index;
                return;
            }
        }
    }

    private void btnAdd_Click(object? sender, EventArgs e)
    {
        using var editor = new SuggestionEditorForm();
        if (editor.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var suggestion = editor.Suggestion;
                suggestion.Id = _repository.Add(suggestion);
                LoadSuggestions();
                SelectSuggestion(suggestion.Id);
            }
            catch (Exception ex)
            {
                ShowError("No se pudo agregar la sugerencia.", ex);
            }
        }
    }

    private void btnEdit_Click(object? sender, EventArgs e)
    {
        var selected = GetSelectedSuggestion();
        if (selected == null)
        {
            MessageBox.Show(this, "Selecciona una sugerencia para editar.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var editor = new SuggestionEditorForm(selected);
        if (editor.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                _repository.Update(editor.Suggestion);
                LoadSuggestions();
                SelectSuggestion(editor.Suggestion.Id);
            }
            catch (Exception ex)
            {
                ShowError("No se pudo actualizar la sugerencia.", ex);
            }
        }
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        var selected = GetSelectedSuggestion();
        if (selected == null)
        {
            MessageBox.Show(this, "Selecciona una sugerencia para eliminar.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var result = MessageBox.Show(this, "¿Seguro que deseas eliminar la sugerencia seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _repository.Delete(selected.Id);
            LoadSuggestions();
        }
        catch (Exception ex)
        {
            ShowError("No se pudo eliminar la sugerencia.", ex);
        }
    }

    private void txtFilter_TextChanged(object? sender, EventArgs e)
    {
        ApplyFilter();
    }

    private void dgvSuggestions_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            btnEdit.PerformClick();
        }
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void ShowError(string message, Exception exception)
    {
        MessageBox.Show(this, $"{message}\n\n{exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
