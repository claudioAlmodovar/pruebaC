using System;
using System.Windows.Forms;
using ApuestasApp.Models;

namespace ApuestasApp.Forms;

public partial class SuggestionEditorForm : Form
{
    public Suggestion Suggestion { get; private set; }

    public SuggestionEditorForm()
    {
        InitializeComponent();
        Suggestion = new Suggestion();
        LoadSuggestion();
    }

    public SuggestionEditorForm(Suggestion existing) : this()
    {
        Suggestion = new Suggestion
        {
            Id = existing.Id,
            Dato = existing.Dato
        };
        LoadSuggestion();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        txtDato.Focus();
        txtDato.SelectAll();
    }

    private void LoadSuggestion()
    {
        txtDato.Text = Suggestion.Dato ?? string.Empty;
    }

    private void btnAceptar_Click(object? sender, EventArgs e)
    {
        var dato = NormalizeText(txtDato.Text);
        if (string.IsNullOrEmpty(dato))
        {
            MessageBox.Show(this, "Ingresa un dato para la sugerencia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtDato.Focus();
            return;
        }

        Suggestion.Dato = dato;
        DialogResult = DialogResult.OK;
        Close();
    }

    private static string? NormalizeText(string? text)
    {
        var trimmed = text?.Trim();
        return string.IsNullOrWhiteSpace(trimmed) ? null : trimmed;
    }
}
