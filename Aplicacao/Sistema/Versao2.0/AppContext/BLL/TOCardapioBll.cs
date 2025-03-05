using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    [Serializable]
    public class TOCardapioBll
    {
        public int? IdTutor { get; set; }
        public string Tutor { get; set; }
        public string TutorEMail { get; set; }
        public string TutorFone { get; set; }
        public int? IdAnimal { get; set; }
        public string Animal { get; set; }
        public int? IdEspecie { get; set; }
        public string Especie { get; set; }
        public int? IdRaca { get; set; }
        public string Raca { get; set; }
        public string Sexo { get; set; }
        public int? Idade { get; set; }
        public int IdCardapio { get; set; }
        public string Descricao { get; set; }
        public DateTime DtCardapio { get; set; }
        public decimal? PesoAtual { get; set; }
        public decimal? PesoIdeal { get; set; }
        public decimal? FatorEnergia { get; set; }
        public decimal? NEM { get; set; }
        public bool? Gestante { get; set; }
        public bool? Lactante { get; set; }
        public int? LactacaoSemanas { get; set; }
        public int? NrFilhotes { get; set; }
        public int? IdDieta { get; set; }
        public string Dieta { get; set; }
        public decimal? EmCardapio { get; set; }
        public string Observacao { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
