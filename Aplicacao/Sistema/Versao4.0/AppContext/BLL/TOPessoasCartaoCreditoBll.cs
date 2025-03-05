using System;

namespace BLL
{
    public class TOPessoasCartaoCreditoBll
    {
        //Dados do Comprador
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string EMail { get; set; }
        public string Passaporte { get; set; }
        public string DocumentosOutros { get; set; }
        public int? dTpEntidade { get; set; }
        public string TipoEntidade { get; set; }
        public int? dNacionalidade { get; set; }
        public string Nacionalidade { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Senha { get; set; }
        public string SenhaConfirmar { get; set; }
        public bool? Cliente { get; set; }
        public bool? Tutor { get; set; }
        public int? dTpFone { get; set; }
        public string DDDTelefone { get; set; }
        public string Telefone { get; set; }
        public string TelefoneComMascara { get; set; }

        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string NumeroLogradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

        //Dados do Cartão
        public int IdCartao { get; set; }
        public string NrCartao { get; set; }
        public string NrCartaoComposto { get; set; }
        public string CodSeg { get; set; }
        public int? dBandeira { get; set; }
        public string Bandeira { get; set; }
        public string VencimCartao { get; set; }
        public string NomeCartao { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
