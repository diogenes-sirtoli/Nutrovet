using System;

namespace BLL
{
    public class TOPessoasBll
    {
        public int IdPessoa { get; set; }
        public int? cIdTpPessoa { get; set; }
        public string cTipoPessoa { get; set; }
        public int? cdTpEntidade { get; set; }
        public string cTipoEntidade { get; set; }
        public string cNome { get; set; }
        public string cNomePesquisa { get; set; }
        public DateTime? cDtNascim { get; set; }
        public int? cdNacionalidade { get; set; }
        public string cNacionalidade { get; set; }
        public string cRG { get; set; }
        public string cCPF { get; set; }
        public string cCNPJ { get; set; }
        public string cPassaporte { get; set; }
        public string cDocumentosOutros { get; set; }
        public string cEMail { get; set; }
        public string cTelefone { get; set; }
        public string cCelular { get; set; }
        public string cLogradouro { get; set; }
        public string cLogr_Nr { get; set; }
        public string cLogr_Compl { get; set; }
        public string cLogr_Bairro { get; set; }
        public string cLogr_CEP { get; set; }
        public string cLogr_Cidade { get; set; }
        public string cLogr_UF { get; set; }
        public string cLogr_Pais { get; set; }
        public string cUsuario { get; set; }
        public string cSenha { get; set; }
        public bool? cBloqueado { get; set; }
        public bool? cAtivo { get; set; }
        public int? cIdOperador { get; set; }
        public string cIP { get; set; }
        public DateTime? cDataCadastro { get; set; }
        public int IdTutores { get; set; }
        public int IdTutor { get; set; }
        public int? tIdTpPessoa { get; set; }
        public string tTipoPessoa { get; set; }
        public int? tdTpEntidade { get; set; }
        public string tTipoEntidade { get; set; }
        public string tNome { get; set; }
        public string tNomePesquisa { get; set; }
        public DateTime? tDtNascim { get; set; }
        public string tRG { get; set; }
        public string tCPF { get; set; }
        public string tCNPJ { get; set; }
        public string tPassaporte { get; set; }
        public string tDocumentosOutros { get; set; }
        public string tEMail { get; set; }
        public string tTelefone { get; set; }
        public string tCelular { get; set; }
    }
}
