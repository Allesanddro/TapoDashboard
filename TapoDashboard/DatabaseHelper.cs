using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Windows.Forms;

namespace TapoDashboard
{
    public static class DatabaseHelper
    {
        private static readonly string dbPath = Path.Combine(AppContext.BaseDirectory, "tapo_history.sqlite");

        public static void InitializeDatabase()
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS History (
                        Timestamp TEXT NOT NULL,
                        IPAddress TEXT NOT NULL,
                        PlugName TEXT,
                        Wattage REAL NOT NULL,
                        Kwh30Day REAL NOT NULL,
                        Cost30Day REAL NOT NULL
                    );
                ";
                command.ExecuteNonQuery();

                command.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS RefreshSummary (
                        SummaryID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Timestamp TEXT NOT NULL,
                        TotalCost REAL NOT NULL,
                        PlugCount INTEGER NOT NULL
                    );
                ";
                command.ExecuteNonQuery();
            }
        }

        public static void InsertHistoryEntry(HistoryEntry entry)
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO History (Timestamp, IPAddress, PlugName, Wattage, Kwh30Day, Cost30Day) VALUES ($timestamp, $ip, $name, $wattage, $kwh, $cost)";
                command.Parameters.AddWithValue("$timestamp", entry.Timestamp.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("$ip", entry.IPAddress);
                command.Parameters.AddWithValue("$name", entry.PlugName);
                command.Parameters.AddWithValue("$wattage", entry.Wattage);
                command.Parameters.AddWithValue("$kwh", entry.Kwh30Day);
                command.Parameters.AddWithValue("$cost", entry.Cost30Day);
                command.ExecuteNonQuery();
            }
        }

        public static void InsertRefreshSummary(DateTime timestamp, decimal totalCost, int plugCount)
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO RefreshSummary (Timestamp, TotalCost, PlugCount) VALUES ($timestamp, $totalCost, $plugCount)";
                command.Parameters.AddWithValue("$timestamp", timestamp.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("$totalCost", totalCost);
                command.Parameters.AddWithValue("$plugCount", plugCount);
                command.ExecuteNonQuery();
            }
        }

        public static List<PlugIdentifier> GetUniquePlugs()
        {
            var plugs = new List<PlugIdentifier>();
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT t1.IPAddress, t1.PlugName
                    FROM History t1
                    INNER JOIN (
                        SELECT IPAddress, MAX(Timestamp) AS MaxTimestamp
                        FROM History
                        GROUP BY IPAddress
                    ) t2 ON t1.IPAddress = t2.IPAddress AND t1.Timestamp = t2.MaxTimestamp
                    ORDER BY t1.IPAddress;
                ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        plugs.Add(new PlugIdentifier
                        {
                            IP = reader.GetString(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return plugs;
        }

        public static List<HistoryEntry> GetHistoryForIP(string ipAddress, DateTime startDate, DateTime endDate)
        {
            var entries = new List<HistoryEntry>();
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM History WHERE IPAddress = $ip AND Timestamp >= $start AND Timestamp <= $end ORDER BY Timestamp DESC";
                command.Parameters.AddWithValue("$ip", ipAddress);
                command.Parameters.AddWithValue("$start", startDate.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("$end", endDate.ToString("o", CultureInfo.InvariantCulture));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entries.Add(new HistoryEntry
                        {
                            Timestamp = DateTime.Parse(reader.GetString(0), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                            IPAddress = reader.GetString(1),
                            PlugName = reader.GetString(2),
                            Wattage = (decimal)reader.GetDouble(3),
                            Kwh30Day = (decimal)reader.GetDouble(4),
                            Cost30Day = (decimal)reader.GetDouble(5)
                        });
                    }
                }
            }
            return entries;
        }

        public static List<RefreshSummary> GetSummaryHistory()
        {
            var summaries = new List<RefreshSummary>();
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Timestamp, TotalCost, PlugCount FROM RefreshSummary ORDER BY Timestamp DESC";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        summaries.Add(new RefreshSummary
                        {
                            Timestamp = DateTime.Parse(reader.GetString(0), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                            TotalCost = (decimal)reader.GetDouble(1),
                            PlugCount = reader.GetInt32(2)
                        });
                    }
                }
            }
            return summaries;
        }
    }

    public class RefreshSummary
    {
        public DateTime Timestamp { get; set; }
        public decimal TotalCost { get; set; }
        public int PlugCount { get; set; }
    }

    public class PlugIdentifier
    {
        public string IP { get; set; }
        public string Name { get; set; }
        public string DisplayText => $"{Name} ({IP})";
    }
}