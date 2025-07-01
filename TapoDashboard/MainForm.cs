using Smdn.TPSmartHomeDevices.Tapo;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace TapoDashboard
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DatabaseHelper.InitializeDatabase();
            dgvPlugs.ColumnCount = 5;
            dgvPlugs.Columns[0].Name = "Name";
            dgvPlugs.Columns[1].Name = "IP Address";
            dgvPlugs.Columns[2].Name = "Wattage (W)";
            dgvPlugs.Columns[3].Name = "30-Day Usage (kWh)";
            dgvPlugs.Columns[4].Name = "30-Day Cost (€)";
            dgvPlugs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtPassword.UseSystemPasswordChar = true;
            LoadSettings();
        }

        private void LoadSettings()
        {
            txtEmail.Text = Properties.Settings.Default.UserEmail;
            numCost.Value = Properties.Settings.Default.CostRate > 0 ? Properties.Settings.Default.CostRate : 29.49m;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.UserPass))
            {
                try
                {
                    byte[] encryptedData = Convert.FromBase64String(Properties.Settings.Default.UserPass);
                    byte[] unprotectedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
                    txtPassword.Text = Encoding.UTF8.GetString(unprotectedData);
                }
                catch { Properties.Settings.Default.UserPass = ""; Properties.Settings.Default.Save(); }
            }
            if (!string.IsNullOrEmpty(txtEmail.Text)) { chkRememberMe.Checked = true; }
            if (Properties.Settings.Default.IpList != null)
            {
                foreach (var ip in Properties.Settings.Default.IpList)
                {
                    if (ip != null) { lstIpAddresses.Items.Add(ip); }
                }
            }
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.CostRate = numCost.Value;
            if (chkRememberMe.Checked)
            {
                Properties.Settings.Default.UserEmail = txtEmail.Text;
                byte[] dataToProtect = Encoding.UTF8.GetBytes(txtPassword.Text);
                byte[] protectedData = ProtectedData.Protect(dataToProtect, null, DataProtectionScope.CurrentUser);
                Properties.Settings.Default.UserPass = Convert.ToBase64String(protectedData);
            }
            else
            {
                Properties.Settings.Default.UserEmail = "";
                Properties.Settings.Default.UserPass = "";
            }
            if (Properties.Settings.Default.IpList == null) { Properties.Settings.Default.IpList = new System.Collections.Specialized.StringCollection(); }
            Properties.Settings.Default.IpList.Clear();
            foreach (var item in lstIpAddresses.Items) { Properties.Settings.Default.IpList.Add(item.ToString()); }
            Properties.Settings.Default.Save();
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void btnAddIp_Click(object sender, EventArgs e)
        {
            var newIp = txtNewIp.Text.Trim();
            if (string.IsNullOrEmpty(newIp)) { MessageBox.Show("Please enter an IP address.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!IPAddress.TryParse(newIp, out _)) { MessageBox.Show("The entered text is not a valid IP address.", "Invalid IP", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (lstIpAddresses.Items.Contains(newIp)) { MessageBox.Show("This IP address is already in the list.", "Duplicate IP", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            lstIpAddresses.Items.Add(newIp);
            txtNewIp.Clear();
        }

        private void btnRemoveIp_Click(object sender, EventArgs e)
        {
            if (lstIpAddresses.SelectedItem == null) { MessageBox.Show("Please select an IP address from the list to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            lstIpAddresses.Items.Remove(lstIpAddresses.SelectedItem);
        }

        private void btnShowHistory_Click(object sender, EventArgs e)
        {
            HistoryForm historyForm = new HistoryForm();
            historyForm.Show();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            SaveSettings();
            dgvPlugs.Rows.Clear();
            btnRefresh.Enabled = false;
            btnRefresh.Text = "LOADING...";
            var email = txtEmail.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your Tapo email and password.", "Credentials Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRefresh.Enabled = true;
                btnRefresh.Text = "REFRESH DATA";
                return;
            }

            var costRate = numCost.Value / 100m;
            int successfulPlugs = 0;

            foreach (var ipString in lstIpAddresses.Items)
            {
                if (!IPAddress.TryParse(ipString.ToString(), out var ipAddress))
                {
                    dgvPlugs.Rows.Add("Invalid IP", ipString.ToString(), "N/A", "N/A", "N/A");
                    continue;
                }
                try
                {
                    using var plug = new P110M(ipAddress, email, password);
                    var deviceInfo = await plug.GetDeviceInfoAsync();
                    var plugName = string.IsNullOrEmpty(deviceInfo?.NickName) ? "(No Name)" : deviceInfo.NickName;
                    var monitoringData = await plug.GetMonitoringDataAsync();
                    var currentPower = monitoringData?.CurrentPowerConsumption ?? 0m;
                    var energyUsage = await plug.GetCumulativeEnergyUsageAsync();
                    var totalEnergyKwh = (energyUsage?.Past30Days ?? 0m) / 1000.0m;
                    var cost = totalEnergyKwh * costRate;

                    dgvPlugs.Rows.Add(
                        plugName, ipAddress.ToString(), $"{currentPower:F2}", $"{totalEnergyKwh:F4}", $"{cost:C2}"
                    );

                    var entry = new HistoryEntry
                    {
                        Timestamp = DateTime.Now,
                        IPAddress = ipAddress.ToString(),
                        PlugName = plugName ?? string.Empty,
                        Wattage = currentPower,
                        Kwh30Day = totalEnergyKwh,
                        Cost30Day = cost
                    };
                    DatabaseHelper.InsertHistoryEntry(entry);

                    successfulPlugs++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {ipAddress}: {ex.Message}");
                    dgvPlugs.Rows.Add("Error", ipAddress.ToString(), "Failed", "N/A", "Error");
                }
            }

            decimal totalCost = 0;
            foreach (DataGridViewRow row in dgvPlugs.Rows)
            {
                if (!row.IsNewRow && row.Cells[4].Value != null)
                {
                    if (decimal.TryParse(row.Cells[4].Value.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal rowCost))
                    {
                        totalCost += rowCost;
                    }
                }
            }

            if (dgvPlugs.Rows.Count > 0)
            {
                dgvPlugs.Rows.Add(new DataGridViewRow());
                var totalRow = new DataGridViewRow();
                totalRow.CreateCells(dgvPlugs);
                totalRow.Cells[2].Value = "TOTAL:";
                totalRow.Cells[4].Value = $"{totalCost:C2}";
                totalRow.DefaultCellStyle.Font = new Font(dgvPlugs.Font, FontStyle.Bold);
                totalRow.DefaultCellStyle.BackColor = Color.LightGray;
                dgvPlugs.Rows.Add(totalRow);
            }

            if (successfulPlugs > 0)
            {
                DatabaseHelper.InsertRefreshSummary(DateTime.Now, totalCost, successfulPlugs);
            }

            btnRefresh.Enabled = true;
            btnRefresh.Text = "REFRESH DATA";
        }
    }
}