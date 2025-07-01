using System.Data;
using System.Globalization;

namespace TapoDashboard
{
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();
        }

        private void HistoryForm_Load_1(object sender, EventArgs e)
        {
            dgvIndividualHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvIndividualHistory.ReadOnly = true;
            dgvIndividualHistory.AllowUserToAddRows = false;
            dgvSummaryHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSummaryHistory.ReadOnly = true;
            dgvSummaryHistory.AllowUserToAddRows = false;

            dtpEndDate.Value = DateTime.Now;
            dtpStartDate.Value = DateTime.Now.AddDays(-30);

            try
            {
                // Use the new method to get plug identifiers
                var plugs = DatabaseHelper.GetUniquePlugs();
                cmbIpAddresses.DataSource = plugs;
                // Tell the dropdown what to SHOW the user
                cmbIpAddresses.DisplayMember = "DisplayText";
                // Tell the dropdown what the underlying VALUE is
                cmbIpAddresses.ValueMember = "IP";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load IPs from database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadSummaryData();
        }

        private void btnFilter_Click_1(object sender, EventArgs e)
        {
            if (cmbIpAddresses.SelectedValue == null)
            {
                MessageBox.Show("Please select a plug.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the IP address from the dropdown's 'SelectedValue'
            string selectedIp = cmbIpAddresses.SelectedValue.ToString();
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddTicks(-1);

            try
            {
                var entries = DatabaseHelper.GetHistoryForIP(selectedIp, startDate, endDate);
                dgvIndividualHistory.DataSource = entries;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load history for {selectedIp}: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSummaryData()
        {
            try
            {
                var summaries = DatabaseHelper.GetSummaryHistory();
                dgvSummaryHistory.DataSource = summaries;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load summary history: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class HistoryEntry
    {
        public DateTime Timestamp { get; set; }
        public string IPAddress { get; set; }
        public string PlugName { get; set; }
        public decimal Wattage { get; set; }
        public decimal Kwh30Day { get; set; }
        public decimal Cost30Day { get; set; }
    }
}