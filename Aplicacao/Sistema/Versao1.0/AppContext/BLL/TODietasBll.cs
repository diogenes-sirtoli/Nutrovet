using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TODietasBll
    {
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public int IdDieta { get; set; }
        public string Dieta { get; set; }
        public decimal? Carboidrato { get; set; }
        public decimal? Proteina { get; set; }
        public decimal? Gordura { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
