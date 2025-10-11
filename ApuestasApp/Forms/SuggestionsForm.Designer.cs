using System.Drawing;
using System.Windows.Forms;

namespace ApuestasApp.Forms;

partial class SuggestionsForm
{
    private System.ComponentModel.IContainer? components = null;
    private DataGridView dgvSuggestions = null!;
    private Button btnAdd = null!;
    private Button btnEdit = null!;
    private Button btnDelete = null!;
    private Button btnClose = null!;

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
        dgvSuggestions = new DataGridView();
        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvSuggestions).BeginInit();
        SuspendLayout();
        //
        // dgvSuggestions
        //
        dgvSuggestions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvSuggestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSuggestions.Location = new Point(12, 12);
        dgvSuggestions.MultiSelect = false;
        dgvSuggestions.Name = "dgvSuggestions";
        dgvSuggestions.ReadOnly = true;
        dgvSuggestions.RowHeadersVisible = false;
        dgvSuggestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSuggestions.Size = new Size(460, 277);
        dgvSuggestions.TabIndex = 0;
        dgvSuggestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSuggestions.CellDoubleClick += dgvSuggestions_CellDoubleClick;
        //
        // btnAdd
        //
        btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnAdd.Location = new Point(215, 295);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(75, 35);
        btnAdd.TabIndex = 1;
        btnAdd.Text = "Agregar";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += btnAdd_Click;
        //
        // btnEdit
        //
        btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnEdit.Location = new Point(296, 295);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(75, 35);
        btnEdit.TabIndex = 2;
        btnEdit.Text = "Editar";
        btnEdit.UseVisualStyleBackColor = true;
        btnEdit.Click += btnEdit_Click;
        //
        // btnDelete
        //
        btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnDelete.Location = new Point(377, 295);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(75, 35);
        btnDelete.TabIndex = 3;
        btnDelete.Text = "Eliminar";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += btnDelete_Click;
        //
        // btnClose
        //
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnClose.DialogResult = DialogResult.Cancel;
        btnClose.Location = new Point(58, 295);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(75, 35);
        btnClose.TabIndex = 4;
        btnClose.Text = "Cerrar";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // SuggestionsForm
        //
        AcceptButton = btnAdd;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(484, 342);
        Controls.Add(btnClose);
        Controls.Add(btnDelete);
        Controls.Add(btnEdit);
        Controls.Add(btnAdd);
        Controls.Add(dgvSuggestions);
        MinimumSize = new Size(500, 381);
        Name = "SuggestionsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Sugerencias";
        Load += SuggestionsForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvSuggestions).EndInit();
        ResumeLayout(false);
    }
}
