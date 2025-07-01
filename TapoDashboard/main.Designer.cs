namespace TapoDashboard
{
    partial class main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            label2 = new Label();
            chkRememberMe = new CheckBox();
            label3 = new Label();
            txtNewIp = new TextBox();
            btnAddIp = new Button();
            btnRemoveIp = new Button();
            lstIpAddresses = new ListBox();
            dgvPlugs = new DataGridView();
            btnRefresh = new Button();
            label4 = new Label();
            numCost = new NumericUpDown();
            btnShowHistory = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPlugs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCost).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 11);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(71, 8);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(100, 23);
            txtEmail.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(71, 34);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(100, 23);
            txtPassword.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 37);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 3;
            label2.Text = "Password:";
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.Location = new Point(71, 63);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(104, 19);
            chkRememberMe.TabIndex = 4;
            chkRememberMe.Text = "Remember Me";
            chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(271, 16);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 5;
            label3.Text = "New Plug IP:";
            // 
            // txtNewIp
            // 
            txtNewIp.Location = new Point(351, 13);
            txtNewIp.Name = "txtNewIp";
            txtNewIp.Size = new Size(100, 23);
            txtNewIp.TabIndex = 6;
            // 
            // btnAddIp
            // 
            btnAddIp.Location = new Point(457, 13);
            btnAddIp.Name = "btnAddIp";
            btnAddIp.Size = new Size(75, 23);
            btnAddIp.TabIndex = 7;
            btnAddIp.Text = "Add";
            btnAddIp.UseVisualStyleBackColor = true;
            btnAddIp.Click += btnAddIp_Click_1;
            // 
            // btnRemoveIp
            // 
            btnRemoveIp.Location = new Point(538, 13);
            btnRemoveIp.Name = "btnRemoveIp";
            btnRemoveIp.Size = new Size(112, 23);
            btnRemoveIp.TabIndex = 8;
            btnRemoveIp.Text = "Remove Selected";
            btnRemoveIp.UseVisualStyleBackColor = true;
            btnRemoveIp.Click += btnRemoveIp_Click_1;
            // 
            // lstIpAddresses
            // 
            lstIpAddresses.FormattingEnabled = true;
            lstIpAddresses.ItemHeight = 15;
            lstIpAddresses.Location = new Point(351, 42);
            lstIpAddresses.Name = "lstIpAddresses";
            lstIpAddresses.Size = new Size(120, 94);
            lstIpAddresses.TabIndex = 9;
            // 
            // dgvPlugs
            // 
            dgvPlugs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlugs.Dock = DockStyle.Bottom;
            dgvPlugs.Location = new Point(0, 371);
            dgvPlugs.Name = "dgvPlugs";
            dgvPlugs.Size = new Size(1034, 238);
            dgvPlugs.TabIndex = 10;
            // 
            // btnRefresh
            // 
            btnRefresh.Font = new Font("Segoe UI", 15F);
            btnRefresh.Location = new Point(351, 328);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(173, 37);
            btnRefresh.TabIndex = 11;
            btnRefresh.Text = "REFRESH DATA";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click_1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(477, 42);
            label4.Name = "label4";
            label4.Size = new Size(102, 15);
            label4.TabIndex = 12;
            label4.Text = "Cost per kWh (ct):";
            // 
            // numCost
            // 
            numCost.DecimalPlaces = 2;
            numCost.Location = new Point(585, 40);
            numCost.Name = "numCost";
            numCost.Size = new Size(120, 23);
            numCost.TabIndex = 13;
            numCost.Value = new decimal(new int[] { 2949, 0, 0, 131072 });
            // 
            // btnShowHistory
            // 
            btnShowHistory.Location = new Point(545, 342);
            btnShowHistory.Name = "btnShowHistory";
            btnShowHistory.Size = new Size(97, 23);
            btnShowHistory.TabIndex = 14;
            btnShowHistory.Text = "Show History";
            btnShowHistory.UseVisualStyleBackColor = true;
            btnShowHistory.Click += btnShowHistory_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 609);
            Controls.Add(btnShowHistory);
            Controls.Add(numCost);
            Controls.Add(label4);
            Controls.Add(btnRefresh);
            Controls.Add(dgvPlugs);
            Controls.Add(lstIpAddresses);
            Controls.Add(btnRemoveIp);
            Controls.Add(btnAddIp);
            Controls.Add(txtNewIp);
            Controls.Add(label3);
            Controls.Add(chkRememberMe);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvPlugs).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCost).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Label label2;
        private CheckBox chkRememberMe;
        private Label label3;
        private TextBox txtNewIp;
        private Button btnAddIp;
        private Button btnRemoveIp;
        private ListBox lstIpAddresses;
        private DataGridView dgvPlugs;
        private Button btnRefresh;
        private Label label4;
        private NumericUpDown numCost;
        private Button btnShowHistory;
    }
}
