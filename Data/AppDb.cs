using System;
using MySqlConnector;

namespace BlazorMenup.Data
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection { get; }

        public AppDb()
        {
            Connection = new MySqlConnection("Server=localhost;User ID=root;Password=rootroot;Database=menu");            
        }

        public void Dispose() => Connection.Dispose();
    }
}