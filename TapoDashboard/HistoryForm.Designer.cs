namespace TapoDashboard
{
    partial class HistoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPageIndividual1 = new TabPage();
            dgvIndividualHistory = new DataGridView();
            btnFilter = new Button();
            dtpEndDate = new DateTimePicker();
            label3 = new Label();
            dtpStartDate = new DateTimePicker();
            label2 = new Label();
            cmbIpAddresses = new ComboBox();
            label1 = new Label();
            tabPage2 = new TabPage();
            dgvSummaryHistory = new DataGridView();
            tabControl1.SuspendLayout();
            tabPageIndividual1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIndividualHistory).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSummaryHistory).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageIndividual1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1487, 723);
            tabControl1.TabIndex = 0;
            // 
            // tabPageIndividual1
            // 
            tabPageIndividual1.Controls.Add(dgvIndividualHistory);
            tabPageIndividual1.Controls.Add(btnFilter);
            tabPageIndividual1.Controls.Add(dtpEndDate);
            tabPageIndividual1.Controls.Add(label3);
            tabPageIndividual1.Controls.Add(dtpStartDate);
            tabPageIndividual1.Controls.Add(label2);
            tabPageIndividual1.Controls.Add(cmbIpAddresses);
            tabPageIndividual1.Controls.Add(label1);
            tabPageIndividual1.Location = new Point(4, 24);
            tabPageIndividual1.Name = "tabPageIndividual1";
            tabPageIndividual1.Padding = new Padding(3);
            tabPageIndividual1.Size = new Size(1479, 695);
            tabPageIndividual1.TabIndex = 0;
            tabPageIndividual1.Text = "Individual Plug History";
            tabPageIndividual1.UseVisualStyleBackColor = true;
            // 
            // dgvIndividualHistory
            // 
            dgvIndividualHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvIndividualHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIndividualHistory.Location = new Point(0, 323);
            dgvIndividualHistory.Name = "dgvIndividualHistory";
            dgvIndividualHistory.Size = new Size(1479, 369);
            dgvIndividualHistory.TabIndex = 7;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(295, 72);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(75, 23);
            btnFilter.TabIndex = 6;
            btnFilter.Text = "Apply Filter";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click_1;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(75, 72);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(200, 23);
            dtpEndDate.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 78);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 4;
            label3.Text = "End Date:";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Location = new Point(75, 38);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(200, 23);
            dtpStartDate.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 44);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 2;
            label2.Text = "Start Date:";
            // 
            // cmbIpAddresses
            // 
            cmbIpAddresses.FormattingEnabled = true;
            cmbIpAddresses.Location = new Point(61, 5);
            cmbIpAddresses.Name = "cmbIpAddresses";
            cmbIpAddresses.Size = new Size(214, 23);
            cmbIpAddresses.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 8);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 0;
            label1.Text = "Plug IP:";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dgvSummaryHistory);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1479, 695);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Total Cost History";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvSummaryHistory
            // 
            dgvSummaryHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSummaryHistory.Dock = DockStyle.Fill;
            dgvSummaryHistory.Location = new Point(3, 3);
            dgvSummaryHistory.Name = "dgvSummaryHistory";
            dgvSummaryHistory.Size = new Size(1473, 689);
            dgvSummaryHistory.TabIndex = 0;
            // 
            // HistoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1499, 735);
            Controls.Add(tabControl1);
            Name = "HistoryForm";
            Text = "Form2";
            Load += HistoryForm_Load_1;
            tabControl1.ResumeLayout(false);
            tabPageIndividual1.ResumeLayout(false);
            tabPageIndividual1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIndividualHistory).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSummaryHistory).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPageIndividual1;
        private Label label1;
        private TabPage tabPage2;
        private DateTimePicker dtpStartDate;
        private Label label2;
        private ComboBox cmbIpAddresses;
        private Button btnFilter;
        private DateTimePicker dtpEndDate;
        private Label label3;
        private DataGridView dgvIndividualHistory;
        private DataGridView dgvSummaryHistory;
    }
}