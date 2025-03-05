using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    [Serializable]
    public class TOAnimaisBll
    {
        public int IdPessoa { get; set; }
        public string cNome { get; set; }
        public int IdTutores { get; set; }
        public int IdTutor { get; set; }
        public string tNome { get; set; }
        public int IdAnimal { get; set; }
        public string Animal { get; set; }
        public string AnimalPesquisa { get; set; }
        public int? IdEspecie { get; set; }
        public string Especie { get; set; }
        public int? IdRaca { get; set; }
        public string Raca { get; set; }
        public int? IdSexo { get; set; }
        public string Sexo { get; set; }
        public DateTime? DtNascim { get; set; }
        public int? Idade { get; set; }
        public int? IdadeDia { get; set; }
        public int? IdadeMes { get; set; }
        public int? IdadeAno { get; set; }
        public decimal? PesoAtual { get; set; }
        public decimal? PesoIdeal { get; set; }
        public int? CrescInicial { get; set; }
        public int? CrescFinal { get; set; }
        public int? IdadeAdulta { get; set; }
        public string Observacoes { get; set; }
        public bool? RecalcularNEM { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
