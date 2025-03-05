using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    [Serializable]
    public class TOLinhaTempoBll
    {
        public int Id { get; set; }
        public int? IdAnimal { get; set; }
        public string Titulo { get; set; }
        public int? IdClassif { get; set; }
        public string Classificacao { get; set; }
        public string Tipo { get; set; }
        public string Anexo { get; set; }
        public DateTime? Data { get; set; }
        public string Hora { get; set; }
    }
}
