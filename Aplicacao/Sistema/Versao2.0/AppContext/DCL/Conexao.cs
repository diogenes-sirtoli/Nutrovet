using System;
using System.Text;
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
                case "SICORP-02":
                case "SICORP-SERVER":
                    {
                        _serverName = _servidor + "\\SQLEXPRESS";
                        _databaseName = /*"NutrovetTest"*/"Nutrovet";

                        break;
                    }
                case "PC-D1R3ARJU":
                    {
                        _serverName = _servidor;
                        _databaseName = /*"NutrovetTest"*/"Nutrovet";

                        break;
                    }
                case "S198-12-156-200":
                case "NUTROVET-SERVER":
                    {
                        _serverName = _servidor + "\\SQLEXPRESS";
                        _databaseName = /*"NutrovetTest"*/"Nutrovet";

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

        public static bool ServidorLocal()
        {
            bool _retServidor = false;
            string _servidorNome = "";

            if (HttpContext.Current != null)
            {
                HttpServerUtility _server = HttpContext.Current.Server;
                _servidorNome = _server.MachineName.ToUpper();
            }
            else
            {
                _servidorNome = Environment.GetEnvironmentVariable("COMPUTERNAME");
            }

            switch (_servidorNome)
            {
                case "SICORP-01":
                case "SICORP-02":
                case "SICORP-SERVER":
                case "PC-D1R3ARJU":
                    {
                        //_retServidor = true;
                        _retServidor = true;

                        break;
                    }
                case "S198-12-156-200":
                case "NUTROVET-SERVER":
                    {
                        //_retServidor = false;
                        _retServidor = false;

                        break;
                    }
            }

            return _retServidor;
        }
    }
}
