using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Persistence.Configuration
{
    public static class DBConfiguration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),"../../Presentation/JobPortalAPI.API"));
                Console.WriteLine();
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("DefaultSQLConnection");
            }
        }
    }
}
