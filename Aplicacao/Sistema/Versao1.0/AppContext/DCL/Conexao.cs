using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;

namespace DCL
{
    public static class Conexao
    {
        public static string StrCon()
        {
            StringBuilder StrConex = new StringBuilder();

            string _servidor = "", _databaseName = "", _serverName = "", _senha = "",
                    _usuario = "";

            if (HttpContext.Current != null)
            {
                HttpServerUtility _server = HttpContext.Current.Server;
                _servidor = _server.MachineName.ToUpper();
            }
            else
            {
                _servidor = Environment.GetEnvironmentVariable("COMPUTERNAME");
            }

            switch (_servidor)
            {
                case "SICORP-01":
                case "DESKTOP-9CLMC0P":
                    {
                        _serverName = _servidor + "\\SQLEXPRESS";
                        _databaseName = "NutrovetTest"/*"Nutrovet"*/;

                        break;
                    }
                case "S198-12-156-200":
                    {
                        _serverName = _servidor + "\\SQLEXPRESS";
                        _databaseName = "Nutrovet";

                        break;
                    }
            }

            _usuario = "sa";
            _senha = "A1x291x0";

            SqlConnectionStringBuilder sqlBuilder =
                new SqlConnectionStringBuilder()
                {
                    DataSource = _serverName,
                    InitialCatalog = _databaseName,
                    IntegratedSecurity = false,
                    PersistSecurityInfo = false,
                    MultipleActiveResultSets = true,
                    UserID = _usuario,
                    Password = _senha,
                    PacketSize = 4096
                };

            return sqlBuilder.ToString();
        }
    }
}
