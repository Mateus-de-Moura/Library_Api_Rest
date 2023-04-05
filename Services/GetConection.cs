using System.Security.Cryptography.X509Certificates;

namespace Biblioteca_Api.Services
{
    public  class GetConection
    {
        public  string conn;
        public  string conection()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory()).
              AddJsonFile("appsettings.json").Build();
            conn = configuration.GetConnectionString("Conexao");

            return conn;    
        }
    }
}
