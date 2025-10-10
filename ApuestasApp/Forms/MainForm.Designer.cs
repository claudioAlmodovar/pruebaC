using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ApuestasApp.Forms;

partial class MainForm
{
    private IContainer? components = null;
    private DataGridView dgvBets = null!;
    private DateTimePicker dtpFrom = null!;
    private DateTimePicker dtpTo = null!;
    private Button btnFilter = null!;
    private Button btnAdd = null!;
    private Button btnEdit = null!;
    private Button btnDelete = null!;
    private Label lblFrom = null!;
    private Label lblTo = null!;

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
        components = new Container();
        dgvBets = new DataGridView();
        dtpFrom = new DateTimePicker();
        dtpTo = new DateTimePicker();
        btnFilter = new Button();
        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        lblFrom = new Label();
        lblTo = new Label();
        SuspendLayout();
        // 
        // dgvBets
        // 
        dgvBets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvBets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvBets.Location = new Point(12, 72);
        dgvBets.MultiSelect = false;
        dgvBets.Name = "dgvBets";
        dgvBets.ReadOnly = true;
        dgvBets.RowHeadersVisible = false;
        dgvBets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvBets.Size = new Size(860, 377);
        dgvBets.TabIndex = 5;
        dgvBets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        // 
        // dtpFrom
        // 
        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Location = new Point(70, 12);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.Size = new Size(120, 23);
        dtpFrom.TabIndex = 0;
        dtpFrom.Value = DateTime.Today;
        // 
        // dtpTo
        // 
        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Location = new Point(70, 41);
        dtpTo.Name = "dtpTo";
        dtpTo.Size = new Size(120, 23);
        dtpTo.TabIndex = 1;
        dtpTo.Value = DateTime.Today;
        // 
        // btnFilter
        // 
        btnFilter.Location = new Point(210, 12);
        btnFilter.Name = "btnFilter";
        btnFilter.Size = new Size(100, 52);
        btnFilter.TabIndex = 2;
        btnFilter.Text = "Filtrar";
        btnFilter.UseVisualStyleBackColor = true;
        btnFilter.Click += btnFilter_Click;
        // 
        // btnAdd
        // 
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.Location = new Point(638, 12);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(75, 52);
        btnAdd.TabIndex = 3;
        btnAdd.Text = "Agregar";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += btnAdd_Click;
        // 
        // btnEdit
        // 
        btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnEdit.Location = new Point(719, 12);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(75, 52);
        btnEdit.TabIndex = 4;
        btnEdit.Text = "Editar";
        btnEdit.UseVisualStyleBackColor = true;
        btnEdit.Click += btnEdit_Click;
        // 
        // btnDelete
        // 
        btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDelete.Location = new Point(800, 12);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(75, 52);
        btnDelete.TabIndex = 6;
        btnDelete.Text = "Eliminar";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += btnDelete_Click;
        // 
        // lblFrom
        // 
        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(12, 16);
        lblFrom.Name = "lblFrom";
        lblFrom.Size = new Size(36, 15);
        lblFrom.TabIndex = 7;
        lblFrom.Text = "Desde";
        // 
        // lblTo
        // 
        lblTo.AutoSize = true;
        lblTo.Location = new Point(12, 45);
        lblTo.Name = "lblTo";
        lblTo.Size = new Size(33, 15);
        lblTo.TabIndex = 8;
        lblTo.Text = "Hasta";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(884, 461);
        Controls.Add(lblTo);
        Controls.Add(lblFrom);
        Controls.Add(btnDelete);
        Controls.Add(btnEdit);
        Controls.Add(btnAdd);
        Controls.Add(btnFilter);
        Controls.Add(dtpTo);
        Controls.Add(dtpFrom);
        Controls.Add(dgvBets);
        MinimumSize = new Size(900, 500);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Control de Apuestas";
        Load += MainForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }
}
