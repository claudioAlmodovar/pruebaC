using System.Drawing;
using System.Windows.Forms;

namespace ApuestasApp.Forms;

partial class SuggestionEditorForm
{
    private System.ComponentModel.IContainer? components = null;
    private TextBox txtDato = null!;
    private Button btnAceptar = null!;
    private Button btnCancelar = null!;
    private Label lblDato = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        txtDato = new TextBox();
        btnAceptar = new Button();
        btnCancelar = new Button();
        lblDato = new Label();
        SuspendLayout();
        //
        // lblDato
        //
        lblDato.AutoSize = true;
        lblDato.Location = new Point(12, 15);
        lblDato.Name = "lblDato";
        lblDato.Size = new Size(33, 15);
        lblDato.TabIndex = 0;
        lblDato.Text = "Dato";
        //
        // txtDato
        //
        txtDato.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtDato.Location = new Point(12, 33);
        txtDato.MaxLength = 200;
        txtDato.Multiline = true;
        txtDato.Name = "txtDato";
        txtDato.ScrollBars = ScrollBars.Vertical;
        txtDato.Size = new Size(360, 120);
        txtDato.TabIndex = 1;
        //
        // btnAceptar
        //
        btnAceptar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnAceptar.Location = new Point(216, 167);
        btnAceptar.Name = "btnAceptar";
        btnAceptar.Size = new Size(75, 30);
        btnAceptar.TabIndex = 2;
        btnAceptar.Text = "Aceptar";
        btnAceptar.UseVisualStyleBackColor = true;
        btnAceptar.Click += btnAceptar_Click;
        //
        // btnCancelar
        //
        btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Location = new Point(297, 167);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(75, 30);
        btnCancelar.TabIndex = 3;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        //
        // SuggestionEditorForm
        //
        AcceptButton = btnAceptar;
        CancelButton = btnCancelar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(384, 209);
        Controls.Add(btnCancelar);
        Controls.Add(btnAceptar);
        Controls.Add(txtDato);
        Controls.Add(lblDato);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SuggestionEditorForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Sugerencia";
        ResumeLayout(false);
        PerformLayout();
    }
}
