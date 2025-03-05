using System;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace DCL
{
    public static class Conexao
    {
        public static string StrCon()
        {
            string servidor;
            string databaseName = "", serverName = "";

            if (HttpContext.Current != null)
            {
                HttpServerUtility _server = HttpContext.Current.Server;
                servidor = _server.MachineName.ToUpper();
            }
            else
            {
                servidor = Environment.GetEnvironmentVariable("COMPUTERNAME");
            }

            string providerName = "System.Data.SqlClient";

            switch (servidor)
            {
                case "SICORP-01":
                case "SICORP-02":
                case "SICORP-SERVER":
                    {
                        serverName = servidor + "\\SQLEXPRESS";
                        databaseName = /*"NutrovetTest"*/"Nutrovet";

                        break;
                    }
                case "PC-D1R3ARJU":
                    {
                        serverName = servidor;
                        databaseName = /*"NutrovetTest"*/"Nutrovet";

                        break;
                    }
                case "S198-12-156-200":
                case "NUTROVET-SERVER":
                    {
                        serverName = servidor + "\\SQLEXPRESS";
                        databaseName = /*"NutrovetTest"*/"Nutrovet";

                        break;
                    }
            }

            SqlConnectionStringBuilder sqlBuilder =
                new SqlConnectionStringBuilder
                {
                    DataSource = serverName,
                    InitialCatalog = databaseName,
                    IntegratedSecurity = false,
                    PersistSecurityInfo = false,
                    MultipleActiveResultSets = true,
                    UserID = "sa",
                    Password = "A1x291x0",
                    PacketSize = 4096
                };

            string providerString = sqlBuilder.ToString();

            EntityConnectionStringBuilder entityBuilder =
                new EntityConnectionStringBuilder();

            entityBuilder.Provider = providerName;
            entityBuilder.ProviderConnectionString = providerString;
            entityBuilder.Metadata = @"res://*/NutrovetDataBase.csdl|
                                        res://*/NutrovetDataBase.ssdl|
                                        res://*/NutrovetDataBase.msl";

            return entityBuilder.ToString();
        }
    }
}
