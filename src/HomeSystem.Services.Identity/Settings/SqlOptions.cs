namespace HomeSystem.Services.Identity.Settings
{
    public class SqlOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool InMemory { get; set; }
    }
}