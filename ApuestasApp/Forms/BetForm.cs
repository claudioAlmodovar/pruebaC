using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using ApuestasApp.Models;

namespace ApuestasApp.Forms;

public partial class BetForm : Form
{
    public Bet Bet { get; private set; }
    private Image? _currentImage;

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
            Tipster = existing.Tipster,
            Imagen = existing.Imagen == null ? null : (byte[])existing.Imagen.Clone()
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
        SetImage(ByteArrayToImage(Bet.Imagen));
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
        Bet.Imagen = ImageToByteArray(_currentImage);

        DialogResult = DialogResult.OK;
        Close();
    }

    private static string? NormalizeText(string? text)
    {
        var trimmed = text?.Trim();
        return string.IsNullOrWhiteSpace(trimmed) ? null : trimmed;
    }

    private void btnPegarImagen_Click(object? sender, EventArgs e)
    {
        if (!Clipboard.ContainsImage())
        {
            MessageBox.Show(this, "El portapapeles no contiene una imagen.", "Pegar imagen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var image = Clipboard.GetImage();
        if (image == null)
        {
            MessageBox.Show(this, "No se pudo obtener la imagen del portapapeles.", "Pegar imagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        SetImage(image);
    }

    private void btnQuitarImagen_Click(object? sender, EventArgs e)
    {
        SetImage(null);
    }

    private void SetImage(Image? image)
    {
        if (_currentImage != null)
        {
            pbImagen.Image = null;
            _currentImage.Dispose();
            _currentImage = null;
        }

        if (image != null)
        {
            _currentImage = new Bitmap(image);
            image.Dispose();
            pbImagen.Image = _currentImage;
        }
        else
        {
            pbImagen.Image = null;
        }

        btnQuitarImagen.Enabled = _currentImage != null;
    }

    private static byte[]? ImageToByteArray(Image? image)
    {
        if (image == null)
        {
            return null;
        }

        using var memoryStream = new MemoryStream();
        image.Save(memoryStream, ImageFormat.Png);
        return memoryStream.ToArray();
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
}
