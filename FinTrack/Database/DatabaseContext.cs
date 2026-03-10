using Dapper;
using Microsoft.Data.Sqlite;

namespace FinTrack.Database
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext()
        {
            _connectionString = $"Data Source={AppSettings.DatabasePath};";
            InitializeSchema();
        }

        public SqliteConnection CreateConnection() => new SqliteConnection(_connectionString);

        private void InitializeSchema()
        {
            using var conn = CreateConnection();
            conn.Open();

            conn.Execute("""
                CREATE TABLE IF NOT EXISTS __schema_version (
                    version    INTEGER PRIMARY KEY,
                    applied_at TEXT NOT NULL
                );
            """);

            int version = conn.QueryFirstOrDefault<int>("SELECT COALESCE(MAX(version), 0) FROM __schema_version");

            if (version < 1) Migration_V1(conn);
        }

        private static void Migration_V1(SqliteConnection conn)
        {
            conn.Execute("""
                CREATE TABLE IF NOT EXISTS transactions (
                    id       INTEGER PRIMARY KEY AUTOINCREMENT,
                    name     TEXT NOT NULL,
                    value    REAL NOT NULL,
                    date     TEXT NOT NULL,
                    type     TEXT NOT NULL,
                    category TEXT NOT NULL DEFAULT 'Outros'
                );
            """);

            conn.Execute("""
                INSERT INTO __schema_version (version, applied_at)
                VALUES (1, datetime('now'));
            """);
        }
    }
}