using System;

namespace BLL
{
    public class TOBibliotecaBll
    {
        public int IdBiblio { get; set; }
        public int? IdSecao { get; set; }
        public string Secao { get; set; }
        public string NomeArq { get; set; }
        public string Descricao { get; set; }
        public string Autor { get; set; }
        public int? Ano { get; set; }
        public string Caminho { get; set; }
        public int? Ordenador { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
