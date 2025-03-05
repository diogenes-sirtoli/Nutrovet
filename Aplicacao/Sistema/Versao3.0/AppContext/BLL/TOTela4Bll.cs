using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    [Serializable]
    public class TOTela4Bll
    {
        public int IdRef { get; set; }
        public string NomeRef { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public object Campo { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
