namespace FinTrack
{
    public static class AppSettings
    {
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, "fintrack.db");
    }
}