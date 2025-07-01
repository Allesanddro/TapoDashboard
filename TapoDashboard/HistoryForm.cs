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

        private void HistoryForm_Load(object sender, EventArgs e)
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
                var plugs = DatabaseHelper.GetUniquePlugs();
                cmbIpAddresses.DataSource = plugs;
                cmbIpAddresses.DisplayMember = "DisplayText";
                cmbIpAddresses.ValueMember = "IP";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load IPs from database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadSummaryData();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (cmbIpAddresses.SelectedValue is not string selectedIp || string.IsNullOrEmpty(selectedIp))
            {
                MessageBox.Show("Please select a plug.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
        public string IPAddress { get; set; } = string.Empty;
        public string PlugName { get; set; } = string.Empty;
        public decimal Wattage { get; set; }
        public decimal Kwh30Day { get; set; }
        public decimal Cost30Day { get; set; }
    }
}