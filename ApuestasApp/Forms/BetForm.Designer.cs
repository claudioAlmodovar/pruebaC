using System.Drawing;
using System.Windows.Forms;

namespace ApuestasApp.Forms;

partial class BetForm
{
    private System.ComponentModel.IContainer? components = null;
    private DateTimePicker dtpFecha = null!;
    private TextBox txtLiga = null!;
    private TextBox txtPartido = null!;
    private NumericUpDown nudImporte = null!;
    private NumericUpDown nudGanancia = null!;
    private TextBox txtTipo = null!;
    private TextBox txtResultado = null!;
    private NumericUpDown nudCuota = null!;
    private TextBox txtNota = null!;
    private TextBox txtAntesDurante = null!;
    private TextBox txtTipster = null!;
    private Button btnAceptar = null!;
    private Button btnCancelar = null!;
    private TableLayoutPanel tableLayout = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        dtpFecha = new DateTimePicker();
        txtLiga = new TextBox();
        txtPartido = new TextBox();
        nudImporte = new NumericUpDown();
        nudGanancia = new NumericUpDown();
        txtTipo = new TextBox();
        txtResultado = new TextBox();
        nudCuota = new NumericUpDown();
        txtNota = new TextBox();
        txtAntesDurante = new TextBox();
        txtTipster = new TextBox();
        btnAceptar = new Button();
        btnCancelar = new Button();
        tableLayout = new TableLayoutPanel();
        ((System.ComponentModel.ISupportInitialize)nudImporte).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudGanancia).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudCuota).BeginInit();
        SuspendLayout();
        // 
        // tableLayout
        // 
        tableLayout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tableLayout.ColumnCount = 2;
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
        tableLayout.Location = new Point(12, 12);
        tableLayout.Name = "tableLayout";
        tableLayout.RowCount = 11;
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayout.Size = new Size(460, 350);
        tableLayout.TabIndex = 0;
        // 
        // dtpFecha
        // 
        dtpFecha.Dock = DockStyle.Fill;
        dtpFecha.Format = DateTimePickerFormat.Custom;
        dtpFecha.CustomFormat = "dd/MM/yyyy HH:mm";
        tableLayout.Controls.Add(new Label { Text = "Fecha", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 0);
        tableLayout.Controls.Add(dtpFecha, 1, 0);
        // 
        // txtLiga
        // 
        txtLiga.Dock = DockStyle.Fill;
        tableLayout.Controls.Add(new Label { Text = "Liga", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 1);
        tableLayout.Controls.Add(txtLiga, 1, 1);
        // 
        // txtPartido
        // 
        txtPartido.Dock = DockStyle.Fill;
        tableLayout.Controls.Add(new Label { Text = "Partido", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 2);
        tableLayout.Controls.Add(txtPartido, 1, 2);
        // 
        // nudImporte
        // 
        nudImporte.DecimalPlaces = 2;
        nudImporte.Maximum = 1000000;
        nudImporte.ThousandsSeparator = true;
        nudImporte.Dock = DockStyle.Fill;
        tableLayout.Controls.Add(new Label { Text = "Importe", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 3);
        tableLayout.Controls.Add(nudImporte, 1, 3);
        // 
        // nudGanancia
        // 
        nudGanancia.DecimalPlaces = 2;
        nudGanancia.Maximum = 1000000;
        nudGanancia.ThousandsSeparator = true;
        nudGanancia.Dock = DockStyle.Fill;
        tableLayout.Controls.Add(new Label { Text = "Ganancia", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 4);
        tableLayout.Controls.Add(nudGanancia, 1, 4);
        // 
        // txtTipo
        // 
        txtTipo.Dock = DockStyle.Fill;
        tableLayout.Controls.Add(new Label { Text = "Tipo", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 5);
        tableLayout.Controls.Add(txtTipo, 1, 5);
        // 
        // txtResultado
        // 
        txtResultado.Dock = DockStyle.Fill;
        txtResultado.MaxLength = 2;
        tableLayout.Controls.Add(new Label { Text = "Resultado", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 6);
        tableLayout.Controls.Add(txtResultado, 1, 6);
        // 
        // nudCuota
        // 
        nudCuota.DecimalPlaces = 2;
        nudCuota.Maximum = 1000;
        nudCuota.ThousandsSeparator = true;
        nudCuota.Dock = DockStyle.Fill;
        tableLayout.Controls.Add(new Label { Text = "Cuota", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 7);
        tableLayout.Controls.Add(nudCuota, 1, 7);
        // 
        // txtNota
        // 
        txtNota.Dock = DockStyle.Fill;
        txtNota.Multiline = true;
        tableLayout.Controls.Add(new Label { Text = "Nota", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 8);
        tableLayout.Controls.Add(txtNota, 1, 8);
        // 
        // txtAntesDurante
        // 
        txtAntesDurante.Dock = DockStyle.Fill;
        txtAntesDurante.MaxLength = 1;
        tableLayout.Controls.Add(new Label { Text = "Antes/Durante", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 9);
        tableLayout.Controls.Add(txtAntesDurante, 1, 9);
        // 
        // txtTipster
        // 
        txtTipster.Dock = DockStyle.Fill;
        tableLayout.Controls.Add(new Label { Text = "Tipster", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 10);
        tableLayout.Controls.Add(txtTipster, 1, 10);
        // 
        // btnAceptar
        // 
        btnAceptar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnAceptar.Location = new Point(316, 374);
        btnAceptar.Name = "btnAceptar";
        btnAceptar.Size = new Size(75, 30);
        btnAceptar.TabIndex = 1;
        btnAceptar.Text = "Aceptar";
        btnAceptar.UseVisualStyleBackColor = true;
        btnAceptar.Click += btnAceptar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Location = new Point(397, 374);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(75, 30);
        btnCancelar.TabIndex = 2;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        // 
        // BetForm
        // 
        AcceptButton = btnAceptar;
        CancelButton = btnCancelar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(484, 416);
        Controls.Add(btnCancelar);
        Controls.Add(btnAceptar);
        Controls.Add(tableLayout);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "BetForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Apuesta";
        ((System.ComponentModel.ISupportInitialize)nudImporte).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudGanancia).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudCuota).EndInit();
        ResumeLayout(false);
    }
}
