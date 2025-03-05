using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class TOAlimentosBll
    {
        public int? IdPessoa { get; set; }
        public string Pessoa { get; set; }
        public int IdAlimento { get; set; }
        public string Alimento { get; set; }
        public int? IdNutr { get; set; }
        public string Nutriente { get; set; }
        public decimal? Valor { get; set; }
        public string Unidade { get; set; }
        public int IdFonte { get; set; }
        public string Fonte { get; set; }
        public int IdGrupo { get; set; }
        public string GrupoAlimento { get; set; }
        public int? IdCateg { get; set; }
        public string Categoria { get; set; }
        public int? NDB_No { get; set; }
        public bool? Compartilhar { get; set; }
        public bool? fHomologado { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
