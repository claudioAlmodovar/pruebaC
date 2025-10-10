using System;
using System.Windows.Forms;
using ApuestasApp.Models;

namespace ApuestasApp.Forms;

public partial class BetForm : Form
{
    public Bet Bet { get; private set; }

    public BetForm()
    {
        InitializeComponent();
        Bet = new Bet
        {
            Fecha = DateTime.Now
        };
        LoadBetToControls();
    }

    public BetForm(Bet existing) : this()
    {
        Bet = new Bet
        {
            Id = existing.Id,
            Fecha = existing.Fecha,
            Liga = existing.Liga,
            Partido = existing.Partido,
            Importe = existing.Importe,
            Ganancia = existing.Ganancia,
            Tipo = existing.Tipo,
            Resultado = existing.Resultado,
            Cuota = existing.Cuota,
            Nota = existing.Nota,
            AntesDurante = existing.AntesDurante,
            Tipster = existing.Tipster
        };
        LoadBetToControls();
    }

    private void LoadBetToControls()
    {
        dtpFecha.Value = Bet.Fecha == default ? DateTime.Now : Bet.Fecha;
        txtLiga.Text = Bet.Liga ?? string.Empty;
        txtPartido.Text = Bet.Partido ?? string.Empty;
        nudImporte.Value = ClampToRange(Bet.Importe ?? 0, nudImporte);
        nudGanancia.Value = ClampToRange(Bet.Ganancia ?? 0, nudGanancia);
        txtTipo.Text = Bet.Tipo ?? string.Empty;
        txtResultado.Text = Bet.Resultado ?? string.Empty;
        nudCuota.Value = ClampToRange(Bet.Cuota ?? 0, nudCuota);
        txtNota.Text = Bet.Nota ?? string.Empty;
        txtAntesDurante.Text = Bet.AntesDurante ?? string.Empty;
        txtTipster.Text = Bet.Tipster ?? string.Empty;
    }

    private static decimal ClampToRange(decimal value, NumericUpDown control)
    {
        if (value < control.Minimum)
        {
            return control.Minimum;
        }

        if (value > control.Maximum)
        {
            return control.Maximum;
        }

        return value;
    }

    private void btnAceptar_Click(object? sender, EventArgs e)
    {
        Bet.Fecha = dtpFecha.Value;
        Bet.Liga = NormalizeText(txtLiga.Text);
        Bet.Partido = NormalizeText(txtPartido.Text);
        Bet.Importe = nudImporte.Value;
        Bet.Ganancia = nudGanancia.Value;
        Bet.Tipo = NormalizeText(txtTipo.Text);
        Bet.Resultado = NormalizeText(txtResultado.Text);
        Bet.Cuota = nudCuota.Value;
        Bet.Nota = NormalizeText(txtNota.Text);
        Bet.AntesDurante = NormalizeText(txtAntesDurante.Text);
        Bet.Tipster = NormalizeText(txtTipster.Text);

        DialogResult = DialogResult.OK;
        Close();
    }

    private static string? NormalizeText(string? text)
    {
        var trimmed = text?.Trim();
        return string.IsNullOrWhiteSpace(trimmed) ? null : trimmed;
    }
}
