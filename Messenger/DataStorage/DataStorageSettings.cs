using Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public sealed class CDataStorageSettings
    {
        //TODO System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		internal String DbConnectionString { get; }

		internal CDataStorageSettings(String dbConnectionString)
        {
            if (String.IsNullOrEmpty(dbConnectionString))
            {
                SLogger.GetLogger().LogError($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({dbConnectionString})", new ArgumentException(nameof(dbConnectionString)));
                throw new ArgumentException(nameof(dbConnectionString));
            }

            DbConnectionString = dbConnectionString;
        }

        public CDataStorageSettings()
        {
            DbConnectionString =
                //ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
                "Data Source=.;Initial Catalog=messenger_db;Integrated Security=True;MultipleActiveResultSets=True;App=Messenger;Connection Timeout=60"
                //"Data Source=.\\SQLEXPRESS;Initial Catalog=messenger_db;Integrated Security=True;MultipleActiveResultSets=True;App=Messenger;Connection Timeout=60"
                ;
        }
    }
}
