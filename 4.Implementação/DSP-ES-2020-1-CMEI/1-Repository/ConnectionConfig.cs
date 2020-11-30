namespace _1_Repository
{
    public class ConnectionConfig
    {
        public string host { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string databaseName { get; set; }

        public ConnectionConfig(string host, string username, string password, string databaseName)
        {
            this.host = host;
            this.username = username;
            this.password = password;
            this.databaseName = databaseName;
        }
    }
}