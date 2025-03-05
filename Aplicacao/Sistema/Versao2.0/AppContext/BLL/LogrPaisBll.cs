using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.IO;
using DCL;
using DAL;
using System.Web;

namespace BLL
{
    public class LogrPaisBll
    {
        public List<LogrPais> ListarPaisesInternacionais()
        {
            HttpServerUtility _server;
            List<LogrPais> _retPais;

            if (HttpContext.Current != null)
            {
                _server = HttpContext.Current.Server;

                string path = _server.MapPath("~/JSons/paisesInternacional.json");
                string text = File.ReadAllText(path);

                _retPais = JsonConvert.DeserializeObject<List<LogrPais>>(text);
            }
            else
            {
                _retPais = null;
            }


            return _retPais;
        }

        public List<LogrPais> ListarPaisNacional()
        {
            HttpServerUtility _server;
            List<LogrPais> _retPais;

            if (HttpContext.Current != null)
            {
                _server = HttpContext.Current.Server;

                string path = _server.MapPath("~/JSons/paisesNacional.json");
                string text = File.ReadAllText(path);

                _retPais = JsonConvert.DeserializeObject<List<LogrPais>>(text);
            }
            else
            {
                _retPais = null;
            }


            return _retPais;
        }

        public List<LogrUF> ListarUFNacional()
        {
            HttpServerUtility _server;
            List<LogrUF> _retUF;

            if (HttpContext.Current != null)
            {
                _server = HttpContext.Current.Server;

                string path = _server.MapPath("~/JSons/ufBrasil.json");
                string text = File.ReadAllText(path);

                _retUF = JsonConvert.DeserializeObject<List<LogrUF>>(text);
            }
            else
            {
                _retUF = null;
            }


            return _retUF;
        }
    }
}
