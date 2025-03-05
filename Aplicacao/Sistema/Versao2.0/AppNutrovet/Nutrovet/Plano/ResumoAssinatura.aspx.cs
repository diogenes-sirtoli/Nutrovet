using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DCL;
using BLL;
using System.Text.RegularExpressions;
using MosaicoSolutions.ViaCep;
using MosaicoSolutions.ViaCep.Modelos;
using PhoneNumbers;
using MaskEdit;

namespace Nutrovet.Plano
{
    public partial class ResumoAssinatura : System.Web.UI.Page
    {
        protected clAcessosVigenciaPlanosBll planoVigenteBll = new clAcessosVigenciaPlanosBll();
        protected clAcessosVigenciaCupomBll cupomBll = new clAcessosVigenciaCupomBll();
        protected AcessosVigenciaCupomDesconto cupomDcl;
        protected clPessoasBll pessoasBll = new clPessoasBll();
        protected clTutoresBll tutoresBll = new clTutoresBll();
        protected LogrPaisBll paisesBll = new LogrPaisBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["PlanoAssinatura"] == null)
                {
                    Response.Redirect("~/Plano/EscolherAssinatura.aspx");
                }
                else
                {
                    PopulaPlano();
                    PopulaTela();
                    PopulaPais(Funcoes.Funcoes.ConvertePara.Int(
                        ddlNacionalidadeAssinante.SelectedValue));
                }
            }
        }

        private void PopulaPlano()
        {
            List<TOPlanosBll> listagem = (List<TOPlanosBll>)Session["PlanoAssinatura"];
            //int _count = Enum.GetNames(typeof(DominiosBll.PlanosAuxTipos)).Length;
            //List<decimal> 

            TOPlanosBll _plano = listagem.Where(l => l.dPlanoTp !=
                (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar).SingleOrDefault();
            decimal _somaPlanos = Funcoes.Funcoes.ConvertePara.Decimal(
                listagem.Where(l => l.dPlanoTp !=
                    (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar).Sum(s => s.ValorPlano));
            decimal _somaModulos = Funcoes.Funcoes.ConvertePara.Decimal(
                listagem.Where(l => l.dPlanoTp ==
                    (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar).Sum(s => s.ValorPlano));
            lblValorAssinatura.Text = string.Format("Total: {0:c}",
                (_somaPlanos + _somaModulos));

            ViewState["_somaPlanos"] = _somaPlanos;
            ViewState["_somaModulos"] = _somaModulos;

            switch (_plano.dPlanoTp)
            {
                case 4:
                    {
                        cardCartCred.Visible = false;

                        break;
                    }
                default:
                    {
                        cardCartCred.Visible = true;

                        break;
                    }
            }

            rptPlanos.DataSource = ColocaPercentual(listagem.Where(l => l.dPlanoTp !=
                (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar));
            rptPlanos.DataBind();
            rptModulos.DataSource = ColocaPercentual(listagem.Where(l => l.dPlanoTp ==
                (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar));
            rptModulos.DataBind();
        }

        private List<TOPlanosBll> ColocaPercentual(IEnumerable<TOPlanosBll> _planos)
        {
            List<TOPlanosBll> retListagem = new List<TOPlanosBll>();
            TOPlanosBll retItem;

            foreach (TOPlanosBll item in _planos)
            {
                retItem = item;
                retItem.Plano = item.Plano.Replace("Percent", "%");

                retListagem.Add(retItem);
            }

            return retListagem.ToList();
        }

        private void PopulaPais(int _nacional)
        {
            ddlPaisAssinante.DataTextField = "nome_pais";
            ddlPaisAssinante.DataValueField = "sigla";

            switch (_nacional)
            {

                case 0:
                    {
                        ddlPaisAssinante.DataSource = null;
                        ddlPaisAssinante.Items.Clear();

                        break;
                    }
                case 1:
                    {
                        ddlPaisAssinante.DataSource = paisesBll.ListarPaisNacional();

                        break;
                    }
                case 2:
                    {
                        ddlPaisAssinante.DataSource = paisesBll.ListarPaisesInternacionais();

                        break;
                    }
            }

            ddlPaisAssinante.DataBind();

            ddlPaisAssinante.Items.Insert(0, new ListItem("-- Selecione --", "0"));

            if (_nacional == 1)
            {
                ddlPaisAssinante.SelectedIndex = ddlPaisAssinante.Items.IndexOf(
                    ddlPaisAssinante.Items.FindByText("Brasil"));
            }
        }

        protected void lbVoltar_Click(object sender, EventArgs e)
        {
            GravaDadosNaSessao();

            Response.Redirect("~/Plano/EscolherAssinatura.aspx");
        }

        protected void lbAvancarFinalizarAssinatura_Click(object sender, EventArgs e)
        {
            bllRetorno _validaTela = ValidaCampos();

            if (_validaTela.retorno)
            {
                GravaDadosNaSessao();

                List<TOPlanosBll> listagem = (List<TOPlanosBll>)Session["PlanoAssinatura"];
                TOPlanosBll _plano = listagem.Where(l => l.dPlanoTp !=
                    (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar).SingleOrDefault();

                if (_plano.dPlanoTp == (int)DominiosBll.PlanosAuxTipos.Voucher_30_Dias_Gratuíto)
                {
                    bllRetorno retVoucher = GravaUsuarioNoBancoDados();

                    if (retVoucher.retorno)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    else
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Error,
                            "Não foi Possível Gravar suas Informações no Banco de Dados! Por Favor, Contate o Administrador!",
                            "Nutrovet Informa - Erro",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
                else
                {
                    Response.Redirect("~/Plano/FinalizarAssinatura.aspx");
                }
            }
            else
            {
                Mascaras(ddlNacionalidadeAssinante.SelectedValue, rblTpPessoa.SelectedValue);

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    _validaTela.mensagem, "Atenção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private bllRetorno ValidaCampos()
        {
            List<TOPlanosBll> listagem = (List<TOPlanosBll>)Session["PlanoAssinatura"];
            TOPlanosBll _plano = listagem.Where(l => l.dPlanoTp !=
                (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar).SingleOrDefault();

            Regex rgEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            Regex rgCartao = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");
            Regex rgCartao2 = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");
            Regex rgCpf = new Regex(@"(^\d{3}\x2E\d{3}\x2E\d{3}\x2D\d{2}$)");
            Regex rgCnpj = new Regex(@"(^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$)");
            Regex rgValidadeCartao = new Regex(@"(0[1-9]|10|11|12)[0-9]{2}$");
            Regex rgTelefone = new Regex(@"^(\([0-9]{2}\))\s([9]{1})?([0-9]{4})-([0-9]{4})$");
            Regex rgCep = new Regex(@"^\d{5}-\d{3}$");

            int _tpPessoa = Funcoes.Funcoes.ConvertePara.Int(rblTpPessoa.SelectedValue);

            if ((_tpPessoa == 1) && (meCnpjCpfAssinante.Text != "") &&
                (ddlNacionalidadeAssinante.SelectedValue == "1"))
            {
                meCnpjCpfAssinante.Text = FormataCPF(meCnpjCpfAssinante.Text);
            }

            if ((_tpPessoa == 2) && (meCnpjCpfAssinante.Text != "") &&
                (ddlNacionalidadeAssinante.SelectedValue == "2"))
            {
                meCnpjCpfAssinante.Text = FormataCNPJ(meCnpjCpfAssinante.Text);
            }

            if ((rblTpFone.SelectedValue == "1") && (tbTelefoneAssinante.Text != "") &&
                (tbTelefoneAssinante.Text.Length >= 9))
            {
                tbTelefoneAssinante.Text = FormataFixo(tbTelefoneAssinante.Text);
            }

            if ((rblTpFone.SelectedValue == "2") && (tbTelefoneAssinante.Text != "") &&
                (tbTelefoneAssinante.Text.Length >= 10))
            {
                tbTelefoneAssinante.Text = FormataCelular(tbTelefoneAssinante.Text);
            }

            if ((meCEP.Text != "") && (meCEP.Text.Length >= 8) &&
                (ddlNacionalidadeAssinante.SelectedValue == "1"))
            {
                meCEP.Text = FormataCEP(meCEP.Text);
            }

            if (_tpPessoa <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo PESSOA FÍSICA ou JURÍDICA deve ser selecionado!");
            }
            else if ((_tpPessoa == 1) && (tbNomeAssinante.Text == ""))
            {
                tbNomeAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NOME DO ASSINANTE deve ser preenchido!");
            }
            else if ((_tpPessoa == 2) && (tbNomeAssinante.Text == ""))
            {
                tbNomeAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo RAZÃO SOCIAL DO ASSIANTE deve ser preenchido!");
            }
            else if ((_tpPessoa == 1) && (meCnpjCpfAssinante.Text == "") &&
                ddlNacionalidadeAssinante.SelectedIndex == 0)
            {
                meCnpjCpfAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CPF deve ser preenchido!");
            }
            else if ((_tpPessoa == 2) && (meCnpjCpfAssinante.Text == "") &&
                ddlNacionalidadeAssinante.SelectedIndex == 0)
            {
                meCnpjCpfAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CNPJ deve ser preenchido!");
            }
            else if ((_tpPessoa == 1) && (!rgCpf.IsMatch(meCnpjCpfAssinante.Text)) &&
                ddlNacionalidadeAssinante.SelectedIndex == 0)
            {
                meCnpjCpfAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Formato de CPF Inválido!");
            }
            else if ((_tpPessoa == 2) && (!rgCnpj.IsMatch(meCnpjCpfAssinante.Text)) &&
                ddlNacionalidadeAssinante.SelectedIndex == 0)
            {
                meCnpjCpfAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Formato de CNPJ Inválido!");
            }
            else if ((_tpPessoa == 1) && (!Funcoes.Funcoes.Validacoes.Cpf(meCnpjCpfAssinante.Text)) &&
                ddlNacionalidadeAssinante.SelectedIndex == 0)
            {
                meCnpjCpfAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "CPF Inválido!");
            }
            else if ((_tpPessoa == 2) && (!Funcoes.Funcoes.Validacoes.Cnpj(meCnpjCpfAssinante.Text)) &&
                ddlNacionalidadeAssinante.SelectedIndex == 0)
            {
                meCnpjCpfAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "CNPJ Inválido!");
            }
            else if (((_tpPessoa == 1) || (_tpPessoa == 2)) && (tbPassaporteAssinante.Text == "") &&
                ddlNacionalidadeAssinante.SelectedIndex == 1)
            {
                tbPassaporteAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo DOCUMENTO deve ser preenchido!");
            }
            else if ((_tpPessoa == 1) && (tbDataNascimentoAssinante.Text == ""))
            {
                tbDataNascimentoAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo DATA DE NASCIMENTO deve ser preenchido!");
            }
            else if ((_tpPessoa == 1) && (!MaiorDe18(tbDataNascimentoAssinante.Text)))
            {
                tbDataNascimentoAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Usuário deve ser MAIOR de 18 anos!");
            }
            else if (tbEmailAssinante.Text == "")
            {
                tbEmailAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo E-MAIL deve ser preenchido!");
            }
            else if (!rgEmail.IsMatch(tbEmailAssinante.Text))
            {
                tbEmailAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Formato do E-MAIL Inválido!");
            }
            else if (tbSenhaAssinante.Text == "")
            {
                tbSenhaAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo SENHA deve ser preenchido!");
            }
            else if (tbConfSenhaAssinante.Text == "")
            {
                tbConfSenhaAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CONFIRMA SENHA deve ser preenchido!");
            }
            else if ((tbSenhaAssinante.Text != tbConfSenhaAssinante.Text) ||
                (tbConfSenhaAssinante.Text != tbSenhaAssinante.Text))
            {
                tbSenhaAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "As SENHAS devem ser idênticas!");
            }
            else if (tbTelefoneAssinante.Text == "")
            {
                tbTelefoneAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TELEFONE DO ASSINANTE deve ser preenchido!");
            }
            //else if ((ddlNacionalidadeAssinante.SelectedValue == "1") &&
            //         (!rgTelefone.IsMatch(tbTelefoneAssinante.Text)))
            else if (!ValidaNumeroFone(tbTelefoneAssinante.Text, ddlPaisAssinante.SelectedValue))
            {
                tbTelefoneAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Formato do TELEFONE Inválido!");
            }
            else if (meCEP.Text == "")
            {
                meCEP.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CEP deve ser preenchido!");
            }
            else if ((ddlNacionalidadeAssinante.SelectedValue == "1") &&
                     (!rgCep.IsMatch(meCEP.Text)))
            {
                meCEP.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Formato do CEP Inválido!");
            }
            else if (tbLogradouro.Text == "")
            {
                tbLogradouro.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo LOGRADOURO deve ser preenchido!");
            }
            else if (tbNumeroLogradouro.Text == "")
            {
                tbNumeroLogradouro.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NÚMERO DO LOGRADOURO deve ser preenchido!");
            }
            else if ((tbComplementoLogradouro.Text == "") &&
                     (ddlNacionalidadeAssinante.SelectedValue == "2"))
            {
                tbComplementoLogradouro.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo COMPLEMENTO DO LOGRADOURO deve ser preenchido!");
            }
            else if (tbBairro.Text == "")
            {
                tbBairro.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo BAIRRO deve ser preenchido!");
            }
            else if (tbCidade.Text == "")
            {
                tbCidade.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo MUNICÍPIO deve ser preenchido!");
            }
            else if (tbEstado.Text == "")
            {
                tbEstado.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ESTADO deve ser preenchido!");
            }

            if (_plano.dPlanoTp != (int)DominiosBll.PlanosAuxTipos.Voucher_30_Dias_Gratuíto)
            {
                if (meNumeroCartaoCredito.Text == "")
                {
                    meNumeroCartaoCredito.Focus();

                    return
                        bllRetorno.GeraRetorno(false,
                            "Campo NÚMERO DO CARTÃO deve ser preenchido!");
                }
                else if (!rgCartao2.IsMatch(Regex.Replace(meNumeroCartaoCredito.Text, @"\s", "")))
                {
                    meNumeroCartaoCredito.Focus();

                    return
                        bllRetorno.GeraRetorno(false,
                            "NÚMERO DO CARTÃO Inválido!");
                }
                else if (meCodigoSeguranca.Text == "")
                {
                    meCodigoSeguranca.Focus();

                    return
                        bllRetorno.GeraRetorno(false,
                            "Campo CÓDIGO DE SEGURANÇA DO CARTÃO deve ser preenchido!");
                }
                else if (tbMesAnoValidadeCartao.Text == "")
                {
                    tbMesAnoValidadeCartao.Focus();

                    return
                        bllRetorno.GeraRetorno(false,
                            "Campo VENCIMENTO DO CARTÃO deve ser preenchido!");
                }
                else if (!rgValidadeCartao.IsMatch(tbMesAnoValidadeCartao.Text))
                {
                    tbMesAnoValidadeCartao.Focus();

                    return
                        bllRetorno.GeraRetorno(false,
                            "VALIDADE DO CARTÃO Inválida!");
                }
                else if (tbNomeDoCartao.Text == "")
                {
                    tbNomeDoCartao.Focus();

                    return
                        bllRetorno.GeraRetorno(false,
                            "Campo NOME DO CARTÃO deve ser preenchido!");
                }
            }

            bllRetorno retornoBll = planoVigenteBll.EhClienteVigente(tbNomeAssinante.Text,
                tbEmailAssinante.Text);

            if (retornoBll.retorno)
            {
                lbConcluirAssinatura.Visible = true;
            }
            else
            {
                lbConcluirAssinatura.Visible = false;

                return
                    bllRetorno.GeraRetorno(false, retornoBll.mensagem);
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private string FormataCPF(string _nrCpf)
        {
            string _cpf = Funcoes.Funcoes.Mascaras.CPF(
                Funcoes.Funcoes.TiraCaracteresInvalidos(_nrCpf));

            return _cpf;
        }

        private string FormataCNPJ(string _nrCnpj)
        {
            string _cnpj = Funcoes.Funcoes.Mascaras.Cnpj(
                Funcoes.Funcoes.TiraCaracteresInvalidos(_nrCnpj));

            return _cnpj;
        }

        private string FormataCEP(string _nrCep)
        {
            string _cep = Funcoes.Funcoes.Mascaras.Cep(
                Funcoes.Funcoes.TiraCaracteresInvalidos(_nrCep));

            return _cep;
        }

        private string FormataCelular(string _nrCelular)
        {
            string _celular = Funcoes.Funcoes.Mascaras.Celular(
                Funcoes.Funcoes.TiraCaracteresInvalidos(_nrCelular));

            return _celular;
        }

        private string FormataFixo(string _nrFixo)
        {
            string _fixo = Funcoes.Funcoes.Mascaras.Telefone(
                Funcoes.Funcoes.TiraCaracteresInvalidos(_nrFixo));

            return _fixo;
        }

        private bool ValidaNumeroFone(string numeroFone, string codigoPais)
        {
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                PhoneNumber phoneNumber = phoneUtil.Parse(numeroFone, codigoPais);

                //bool isMobile = false;
                bool isValidNumber = phoneUtil.IsValidNumber(phoneNumber); // returns true for valid number    

                // returns trueor false w.r.t phone number region  
                bool isValidRegion = phoneUtil.IsValidNumberForRegion(phoneNumber, codigoPais);

                string region = phoneUtil.GetRegionCodeForNumber(phoneNumber); // GB, US , PK    

                var numberType = phoneUtil.GetNumberType(phoneNumber); // Produces Mobile , FIXED_LINE    

                string phoneNumberType = numberType.ToString();

                //if (!string.IsNullOrEmpty(phoneNumberType) && phoneNumberType == "CELULAR")
                //{
                //  isMobile = true;
                //}

                var originalNumber = phoneUtil.Format(phoneNumber, PhoneNumberFormat.E164); // Produces "+923336323997"    
                return true;


            }
            catch (NumberParseException ex)
            {

                string errorMessage = "NumberParseException was thrown: " + ex.Message.ToString();

                return false;

                //returnResult = new GenericResponse<ValidatePhoneNumberModel>()
                //{
                //    Message = errorMessage,
                //    StatusCode = HttpStatusCode.BadRequest,
                //    StatusMessage = "Failed"
                //};
            }
        }

        private void GravaDadosNaSessao()
        {
            TOPessoasCartaoCreditoBll _dadosComprador = new TOPessoasCartaoCreditoBll();

            _dadosComprador.dTpEntidade = Funcoes.Funcoes.ConvertePara.Int(
                rblTpPessoa.SelectedValue);
            _dadosComprador.TipoEntidade = rblTpPessoa.SelectedValue;
            _dadosComprador.Nome = tbNomeAssinante.Text;
            _dadosComprador.dNacionalidade = Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinante.SelectedValue);

            switch (Funcoes.Funcoes.ConvertePara.Int(rblTpPessoa.SelectedValue))
            {
                case 1:
                    {
                        switch (ddlNacionalidadeAssinante.SelectedValue)
                        {
                            case "1":
                                {
                                    _dadosComprador.CPF = meCnpjCpfAssinante.Text;
                                    _dadosComprador.Passaporte = "";

                                    break;
                                }
                            case "2":
                                {
                                    _dadosComprador.CPF = "";
                                    _dadosComprador.Passaporte = tbPassaporteAssinante.Text;

                                    break;
                                }
                        }

                        _dadosComprador.CNPJ = "";
                        _dadosComprador.DataNascimento =
                            (tbDataNascimentoAssinante.Text != "" ?
                            DateTime.Parse(tbDataNascimentoAssinante.Text) :
                            DateTime.Parse("01/01/1900"));
                        break;
                    }
                case 2:
                    {
                        _dadosComprador.CPF = "";
                        _dadosComprador.Passaporte = "";
                        _dadosComprador.CNPJ = meCnpjCpfAssinante.Text;
                        _dadosComprador.DataNascimento = DateTime.Parse("01/01/1900");

                        break;
                    }
            }

            _dadosComprador.EMail = tbEmailAssinante.Text;
            _dadosComprador.Tutor = false;
            _dadosComprador.Cliente = false;
            _dadosComprador.Senha = tbSenhaAssinante.Text;
            _dadosComprador.SenhaConfirmar = tbConfSenhaAssinante.Text;
            _dadosComprador.dTpFone = Funcoes.Funcoes.ConvertePara.Int(
                rblTpFone.SelectedValue);

            if (tbTelefoneAssinante.Text != "")
            {
                var apenasDigitos = new Regex(@"[^\d]");
                string texto = apenasDigitos.Replace(tbTelefoneAssinante.Text, "");

                string DDI = (Funcoes.Funcoes.ConvertePara.Int(
                    _dadosComprador.dNacionalidade) == 1 ? "+55" : "+1");
                string DDD = (texto).Substring(0, 2);
                string Fone = (texto).Substring(2);

                _dadosComprador.DDDTelefone = (Funcoes.Funcoes.ConvertePara.Int(
                    _dadosComprador.dNacionalidade) == 1 ? DDD : DDI + DDD);
                _dadosComprador.Telefone = Fone;
            }

            _dadosComprador.TelefoneComMascara = tbTelefoneAssinante.Text;
            _dadosComprador.CEP = meCEP.Text;
            _dadosComprador.Logradouro = tbLogradouro.Text;
            _dadosComprador.NumeroLogradouro = tbNumeroLogradouro.Text;
            _dadosComprador.Complemento = tbComplementoLogradouro.Text;
            _dadosComprador.Bairro = tbBairro.Text;
            _dadosComprador.Cidade = tbCidade.Text;
            _dadosComprador.Estado = tbEstado.Text;
            _dadosComprador.Pais = ddlPaisAssinante.SelectedValue;

            _dadosComprador.NrCartao = meNumeroCartaoCredito.Text;
            _dadosComprador.CodSeg = meCodigoSeguranca.Text;
            _dadosComprador.VencimCartao = tbMesAnoValidadeCartao.Text;
            _dadosComprador.NomeCartao = tbNomeDoCartao.Text;

            Session["DadosComprador"] = _dadosComprador;
        }

        private void PopulaTela()
        {
            if (Session["DadosComprador"] != null)
            {
                TOPessoasCartaoCreditoBll _dadosComprador =
                    (TOPessoasCartaoCreditoBll)Session["DadosComprador"];

                rblTpPessoa.SelectedValue = Funcoes.Funcoes.ConvertePara.String(
                    _dadosComprador.dTpEntidade);
                tbNomeAssinante.Text = _dadosComprador.Nome;
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlNacionalidadeAssinante,
                    _dadosComprador.dNacionalidade);

                switch (Funcoes.Funcoes.ConvertePara.Int(rblTpPessoa.SelectedValue))
                {
                    case 1:
                        {
                            meCnpjCpfAssinante.Text = _dadosComprador.CPF;
                            tbDataNascimentoAssinante.Text =
                                _dadosComprador.DataNascimento.Value.ToString("dd/MM/yyyy");
                            break;
                        }
                    case 2:
                        {
                            meCnpjCpfAssinante.Text = _dadosComprador.CNPJ;
                            tbDataNascimentoAssinante.Text = "";

                            break;
                        }
                }

                tbDataNascimentoAssinante.Text = (_dadosComprador.DataNascimento != null ?
                    _dadosComprador.DataNascimento.Value.ToString("dd/MM/yyyy") : "");
                tbEmailAssinante.Text = _dadosComprador.EMail;
                tbSenhaAssinante.Text = _dadosComprador.Senha;
                tbConfSenhaAssinante.Text = _dadosComprador.SenhaConfirmar;
                rblTpFone.SelectedValue = Funcoes.Funcoes.ConvertePara.String(
                    _dadosComprador.dTpFone);
                tbTelefoneAssinante.Text = FormataTelefone(_dadosComprador.dTpFone,
                    _dadosComprador.DDDTelefone, _dadosComprador.Telefone);

                if ((_dadosComprador.CEP != "") && (_dadosComprador.CEP != null))
                {
                    meCEP.Text = _dadosComprador.CEP;
                    tbLogradouro.Text = _dadosComprador.Logradouro;
                    tbNumeroLogradouro.Text = _dadosComprador.NumeroLogradouro;
                    tbComplementoLogradouro.Text = _dadosComprador.Complemento;
                    tbBairro.Text = _dadosComprador.Bairro;
                    tbCidade.Text = _dadosComprador.Cidade;
                    tbEstado.Text = _dadosComprador.Estado;
                    lbCEPInformado.Text = _dadosComprador.CEP;
                    ddlPaisAssinante.SelectedValue = _dadosComprador.Pais;
                    mvTabControlEndereco.ActiveViewIndex = 1;
                }

                meNumeroCartaoCredito.Text = _dadosComprador.NrCartao;
                meCodigoSeguranca.Text = _dadosComprador.CodSeg;
                tbMesAnoValidadeCartao.Text = _dadosComprador.VencimCartao;
                tbNomeDoCartao.Text = _dadosComprador.NomeCartao;

                Mascaras(ddlNacionalidadeAssinante.SelectedValue, rblTpPessoa.SelectedValue);
                MascaraTelefone(rblTpFone.SelectedValue, false);
            }
            else
            {
                LimpaCampos();
            }
        }

        private string FormataTelefone(int? dTpFone, string ddd, string nrfone)
        {
            string _retFone = "";

            switch (dTpFone)
            {
                case 1://fixo
                    {
                        _retFone = "(" + ddd + ") " + nrfone.Insert(4, "-");

                        break;
                    }
                case 2://Celular
                    {
                        _retFone = "(" + ddd + ") " + nrfone.Insert(5, "-");

                        break;
                    }
            }

            return _retFone;
        }

        private bool MaiorDe18(string _dataAniver)
        {
            bool _ret = false;
            DateTime _aniver = DateTime.Parse(_dataAniver);
            int _idade = Funcoes.Funcoes.Datas.CalculaIdade(_aniver);

            if (_idade >= 18)
            {
                _ret = true;
            }

            return _ret;
        }

        protected void rblTpPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _tpPessoa = Funcoes.Funcoes.ConvertePara.Int(rblTpPessoa.SelectedValue);
            LimpaCampos();
            SelecionaCamposTipoPessoa(_tpPessoa);
        }

        private void SelecionaCamposTipoPessoa(int _tpPessoa)
        {
            Mascaras(ddlNacionalidadeAssinante.SelectedValue, rblTpPessoa.SelectedValue);

            meCnpjCpfAssinante.Text = "";
            tbDataNascimentoAssinante.Text = "";
        }

        private void LimpaCampos()
        {
            tbNomeAssinante.Text = "";
            meCnpjCpfAssinante.Text = "";
            tbDataNascimentoAssinante.Text = "";
            tbEmailAssinante.Text = "";
            tbSenhaAssinante.Text = "";
            tbSenhaAssinante.Attributes.Add("value", "");
            tbConfSenhaAssinante.Text = "";
            tbConfSenhaAssinante.Attributes.Add("value", "");
            tbPassaporteAssinante.Attributes.Add("value", "");
            tbTelefoneAssinante.Text = "";
            meCEP.Text = "";
            tbLogradouro.Text = "";
            tbNumeroLogradouro.Text = "";
            tbComplementoLogradouro.Text = "";
            tbBairro.Text = "";
            tbCidade.Text = "";
            tbEstado.Text = "";
            lbCEPInformado.Text = "";
            mvTabControlEndereco.ActiveViewIndex = 0;

            meNumeroCartaoCredito.Text = "";
            meCodigoSeguranca.Text = "";
            tbMesAnoValidadeCartao.Text = "";
            tbNomeDoCartao.Text = "";
        }

        private void LiberaCamposLogradouro(bool _liberar)
        {
            tbLogradouro.Enabled = _liberar;
            tbBairro.Enabled = _liberar;
            tbCidade.Enabled = _liberar;
            tbEstado.Enabled = _liberar;
        }

        protected void rptPlanos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        break;
                    }
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        Label lblValorPlanoSelecionado = (Label)e.Item.FindControl(
                            "lblValorPlanoSelecionado");
                        Label lblDescrB = (Label)e.Item.FindControl("lblDescrB");
                        decimal _soma = (decimal)ViewState["_somaPlanos"];

                        lblValorPlanoSelecionado.Text = string.Format("{0:c}", _soma);

                        HtmlGenericControl _iconeDoPlano = (HtmlGenericControl)e.Item.FindControl(
                            "iconeDoPlano");

                        Label lblTextoPlanoSelecionado = (Label)e.Item.FindControl(
                            "lblTextoPlanoSelecionado");

                        string _plano = ((TOPlanosBll)e.Item.DataItem).Plano;

                        switch (_plano)
                        {

                            case "Básico":
                                {
                                    _iconeDoPlano.Attributes["class"] = "fas fa-paper-plane";
                                    break;
                                }
                            case "Intermediário":
                                {
                                    _iconeDoPlano.Attributes["class"] = "fas fa-plane fa-flip-horizontal";
                                    break;
                                }
                            case "Completo":
                                {
                                    _iconeDoPlano.Attributes["class"] = "fas fa-rocket";
                                    break;
                                }
                        }

                        lblTextoPlanoSelecionado.Text = _plano +
                            " - " + ((TOPlanosBll)e.Item.DataItem).Periodo;
                        lblDescrB.Text = ((TOPlanosBll)e.Item.DataItem).TipoPlano;

                        break;
                    }
            }
        }

        protected void rptModulos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        Label lblValorModuloAdicional = (Label)e.Item.FindControl(
                            "lblValorModuloAdicional");
                        decimal _soma = (decimal)ViewState["_somaModulos"];

                        lblValorModuloAdicional.Text = string.Format("{0:c}", _soma);

                        break;
                    }
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        Label lblTextoModuloAdicional = (Label)e.Item.FindControl(
                            "lblTextoModuloAdicional");
                        lblTextoModuloAdicional.Text = ((TOPlanosBll)e.Item.DataItem).Plano +
                            " - " + ((TOPlanosBll)e.Item.DataItem).Periodo;
                        break;
                    }
            }
        }

        protected void rptPlanos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect("~/Plano/EscolherAssinatura.aspx");
        }

        protected void rptModulos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "excluir":
                    {
                        Excluir(_id);
                        PopulaPlano();

                        break;
                    }
            }
        }

        private void Excluir(int id)
        {
            List<TOPlanosBll> listagem = (List<TOPlanosBll>)Session["PlanoAssinatura"];
            List<TOPlanosBll> retornoLista = new List<TOPlanosBll>();
            TOPlanosBll retornoItem;

            foreach (TOPlanosBll item in listagem)
            {
                if (item.IdPlano != id)
                {
                    retornoItem = item;

                    retornoLista.Add(retornoItem);
                }
            }

            Session["PlanoAssinatura"] = retornoLista;
        }

        protected void btnNão_Click(object sender, EventArgs e)
        {
            lbConcluirAssinatura.Visible = false;
        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            List<TOPlanosBll> _listagemPlanos = (List<TOPlanosBll>)Session["PlanoAssinatura"];
            TOPlanosBll dadosPlano = _listagemPlanos.FirstOrDefault();

            if (Session["DadosComprador"] != null)
            {
                TOPessoasCartaoCreditoBll _dadosComprador =
                    (TOPessoasCartaoCreditoBll)Session["DadosComprador"];

                _dadosComprador.Tutor = true;

                Session["DadosComprador"] = _dadosComprador;


                if (dadosPlano.dPlanoTp != (int)DominiosBll.PlanosAuxTipos.Voucher_30_Dias_Gratuíto)
                {
                    bllRetorno retornoDados = tutoresBll.ModificaTutorParaCliente(_dadosComprador.Nome,
                        _dadosComprador.EMail);
                }
            }

            if (dadosPlano.dPlanoTp != (int)DominiosBll.PlanosAuxTipos.Voucher_30_Dias_Gratuíto)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                Response.Redirect("~/Plano/FinalizarAssinatura.aspx");
            }
        }

        protected void tbCEP_TextChanged(object sender, EventArgs e)
        {
            if (meCEP.Text != "")
            {
                int _nacional = Funcoes.Funcoes.ConvertePara.Int(
                    ddlNacionalidadeAssinante.SelectedValue);

                switch (_nacional)
                {
                    case 1:
                        {
                            try
                            {
                                meCEP.Mascara = MaskEdit.MEdit.TpMascara.CEP;
                                meCEP.Attributes["placeholder"] = "xxxxx-xxx";

                                Cep _cep = meCEP.Text;
                                var viaCepService = ViaCepService.Default();
                                var endereco = viaCepService.ObterEndereco(_cep);

                                tbLogradouro.Text = endereco.Logradouro;
                                tbBairro.Text = endereco.Bairro;
                                tbCidade.Text = endereco.Localidade;
                                tbEstado.Text = endereco.UF;

                                lbCEPInformado.Text = "CEP Informado: " + meCEP.Text;

                                if (endereco.Logradouro != "")
                                {
                                    LiberaCamposLogradouro(false);
                                }
                                else
                                {
                                    LiberaCamposLogradouro(true);
                                }

                                mvTabControlEndereco.ActiveViewIndex = 1;

                                meCnpjCpfAssinante.Visible = true;
                                tbPassaporteAssinante.Visible = false;

                            }
                            catch (Exception err)
                            {
                                lbCEPInformado.Text = "CEP Informado: " + meCEP.Text;
                                mvTabControlEndereco.ActiveViewIndex = 1;
                                LiberaCamposLogradouro(true);

                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                                    err.Message, "Atenção",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);
                            }

                            break;
                        }
                    case 2:
                        {
                            meCEP.Mascara = MaskEdit.MEdit.TpMascara.String;
                            meCEP.Attributes["placeholder"] = "";

                            lbCEPInformado.Text = "ZIP CODE Informado: " + meCEP.Text;

                            LiberaCamposLogradouro(true);

                            mvTabControlEndereco.ActiveViewIndex = 1;

                            meCnpjCpfAssinante.Visible = false;
                            tbPassaporteAssinante.Visible = true;

                            break;
                        }
                }
            }
            else
            {
                mvTabControlEndereco.ActiveViewIndex = 0;
            }
        }

        protected void lbCorrigirCEPInformado_Click(object sender, EventArgs e)
        {
            int _nacional = Funcoes.Funcoes.ConvertePara.Int(
                    ddlNacionalidadeAssinante.SelectedValue);

            meCEP.Text = "";

            switch (_nacional)
            {
                case 1:
                    {
                        meCEP.Mascara = MaskEdit.MEdit.TpMascara.CEP;
                        meCEP.Attributes["placeholder"] = "xxxxx-xxx";
                        lbCEPInformado.Text = "";

                        break;
                    }
                case 2:
                    {
                        meCEP.Mascara = MaskEdit.MEdit.TpMascara.String;
                        meCEP.Attributes["placeholder"] = "";
                        lbCEPInformado.Text = "";

                        break;
                    }
            }

            mvTabControlEndereco.ActiveViewIndex = 0;
        }

        protected void tbSenhaAssinante_TextChanged(object sender, EventArgs e)
        {
            tbSenhaAssinante.Attributes.Add("value", tbSenhaAssinante.Text);
        }

        protected void tbConfSenhaAssinante_TextChanged(object sender, EventArgs e)
        {
            tbConfSenhaAssinante.Attributes.Add("value", tbConfSenhaAssinante.Text);
        }

        protected void rblTpFone_SelectedIndexChanged(object sender, EventArgs e)
        {
            MascaraTelefone(rblTpFone.SelectedValue, true);
        }

        private void MascaraTelefone(string _tpFone, bool _apagarTelefone)
        {
            int _selecao = (Funcoes.Funcoes.ConvertePara.Int(
                            ddlNacionalidadeAssinante.SelectedValue) == 1 ?
                            Funcoes.Funcoes.ConvertePara.Int(_tpFone) :
                            3);

            if (_apagarTelefone)
            {
                tbTelefoneAssinante.Text = "";
            }

            switch (_selecao)
            {
                case 1:
                    {
                        tbTelefoneAssinante.Mascara = MEdit.TpMascara.Telefone;
                        tbTelefoneAssinante.Attributes["placeholder"] = "(xx) xxxx-xxxx";

                        break;
                    }
                case 2:
                    {
                        tbTelefoneAssinante.Mascara = MEdit.TpMascara.Celular;
                        tbTelefoneAssinante.Attributes["placeholder"] = "(xx) xxxxx-xxxx";

                        break;
                    }
                case 3:
                    {
                        tbTelefoneAssinante.Mascara = MEdit.TpMascara.String;
                        tbTelefoneAssinante.Attributes["placeholder"] = "";

                        break;
                    }
            }
        }

        protected void ddlNacionalidadeAssinante_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaPais(Funcoes.Funcoes.ConvertePara.Int(ddlNacionalidadeAssinante.SelectedValue));

            LimpaCampos();

            Mascaras(ddlNacionalidadeAssinante.SelectedValue, rblTpPessoa.SelectedValue);
            MascaraTelefone(rblTpFone.SelectedValue, true);
        }

        private void Mascaras(string _nacional, string _tpPessoa)
        {
            int _idNacional = Funcoes.Funcoes.ConvertePara.Int(_nacional);
            int _idTpPessoa = Funcoes.Funcoes.ConvertePara.Int(_tpPessoa);

            switch (_idNacional)
            {
                case 1:
                    {
                        meCnpjCpfAssinante.Visible = true;
                        tbPassaporteAssinante.Visible = false;

                        lbCEP.Text = "CEP";
                        meCEP.Mascara = MEdit.TpMascara.CEP;
                        meCEP.Attributes["placeholder"] = "xxxxx-xxx";

                        switch (_idTpPessoa)
                        {
                            case 1:
                                {
                                    lbNomeAssinante.Text = "Nome Completo";
                                    divDataNascimento.Visible = true;

                                    lbTituloTipoPessoaAssinante.Text = "CPF";
                                    meCnpjCpfAssinante.MaxLength = 14;
                                    meCnpjCpfAssinante.Mascara = MEdit.TpMascara.CPF;
                                    meCnpjCpfAssinante.Attributes["placeholder"] = "CPF";
                                    meCnpjCpfAssinante.Attributes["title"] = "CPF do Assinante";

                                    break;
                                }
                            case 2:
                                {
                                    lbNomeAssinante.Text = "Razão Social";
                                    divDataNascimento.Visible = false;

                                    lbTituloTipoPessoaAssinante.Text = "CNPJ";
                                    meCnpjCpfAssinante.MaxLength = 19;
                                    meCnpjCpfAssinante.Mascara = MEdit.TpMascara.CNPJ;
                                    meCnpjCpfAssinante.Attributes["placeholder"] = "CNPJ";
                                    meCnpjCpfAssinante.Attributes["title"] = "CNPJ do Assinante";

                                    break;
                                }
                        }
                        lbConcluirAssinatura.Visible = true;
                        LiberaCamposLogradouro(false);

                        break;
                    }
                case 2:
                    {
                        meCnpjCpfAssinante.Visible = false;
                        tbPassaporteAssinante.Visible = true;

                        lbCEP.Text = "ZIP CODE";
                        meCEP.Mascara = MEdit.TpMascara.String;
                        meCEP.Attributes["placeholder"] = "";

                        switch (_idTpPessoa)
                        {
                            case 1:
                                {
                                    lbNomeAssinante.Text = "Nome Completo";
                                    divDataNascimento.Visible = true;
                                    meCnpjCpfAssinante.MaxLength = 14;

                                    lbTituloTipoPessoaAssinante.Text = "Documento";

                                    break;
                                }
                            case 2:
                                {
                                    lbNomeAssinante.Text = "Razão Social";
                                    divDataNascimento.Visible = false;
                                    meCnpjCpfAssinante.MaxLength = 19;

                                    lbTituloTipoPessoaAssinante.Text = "CNPJ";

                                    break;
                                }
                        }

                        bllRetorno _validaTela = ValidaCampos();

                        bllRetorno _validarPlanoEstrangeiro = ValidarPlanoEstrangeiro();

                        if (!_validarPlanoEstrangeiro.retorno)
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Info,
                                _validarPlanoEstrangeiro.mensagem,
                                "Nutrovet INFORMA",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                            lbConcluirAssinatura.Visible = false;
                        }

                        LiberaCamposLogradouro(true);

                        break;
                    }
            }
        }

        private bllRetorno ValidarPlanoEstrangeiro()
        {
            List<TOPlanosBll> listagem = (List<TOPlanosBll>)Session["PlanoAssinatura"];
            TOPlanosBll _plano = listagem.Where(l => l.dPlanoTp !=
                (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar).SingleOrDefault();
            bllRetorno _retorno = new bllRetorno();

            if (_plano.dPlanoTp == (int)DominiosBll.PlanosAuxTipos.Voucher_30_Dias_Gratuíto)
            {
                _retorno.retorno = true;
                _retorno.mensagem = "Plano selecionado válido!";
            }
            else if ((ddlNacionalidadeAssinante.SelectedValue == "2") && 
                     (_plano.Periodo == "Mensal"))
            {
                _retorno.retorno = false;
                _retorno.mensagem = "Somente os planos ANUAIS podem ser utilizados, " +
                    "para nacionalidade ESTRANGEIRA!</br></br>Por favor, escolha um " +
                    "novo Plano!!!";
            }
            else
            {
                _retorno.retorno = true;
                _retorno.mensagem = "Plano selecionado válido!";
            }

            return _retorno;
        }

        private bllRetorno GravaUsuarioNoBancoDados()
        {
            TOPessoasCartaoCreditoBll dadosComprador =
                    (TOPessoasCartaoCreditoBll)Session["DadosComprador"];
            List<TOPlanosBll> _listagemPlanos = (List<TOPlanosBll>)Session["PlanoAssinatura"];
            TOPlanosBll dadosPlano = _listagemPlanos.FirstOrDefault();

            bllRetorno retornoDados = new bllRetorno();
            clTutoresBll tutoresBll = new clTutoresBll();
            clPessoasBll pessoaBll = new clPessoasBll();
            Pessoa pessoaDcl;
            Tutore tutoresDcl;
            Acesso acessoDcl;
            AcessosVigenciaPlano vigenciaPlanoDcl;
            AcessosVigenciaSituacao vigenciaSituacaoDcl;

            if (Funcoes.Funcoes.ConvertePara.Bool(dadosComprador.Tutor))
            {
                retornoDados = tutoresBll.ModificaTutorParaCliente(dadosComprador.Nome,
                    dadosComprador.EMail);
            }
            else
            {
                #region PESSOAS
                pessoaDcl = new Pessoa();
                pessoaDcl.IdTpPessoa = (int)DominiosBll.PessoasAuxTipos.Cliente;
                pessoaDcl.Nome = dadosComprador.Nome;
                pessoaDcl.dTpEntidade = dadosComprador.dTpEntidade;
                pessoaDcl.dNacionalidade = dadosComprador.dNacionalidade;
                pessoaDcl.DataNascimento = dadosComprador.DataNascimento;
                pessoaDcl.Nome = dadosComprador.Nome;

                switch (Funcoes.Funcoes.ConvertePara.Int(dadosComprador.dTpEntidade))
                {
                    case 1:
                        {
                            switch (dadosComprador.dNacionalidade)
                            {
                                case 1:
                                    {
                                        pessoaDcl.CPF = dadosComprador.CPF;
                                        pessoaDcl.Passaporte = "";

                                        break;
                                    }
                                case 2:
                                    {
                                        pessoaDcl.CPF = "";
                                        pessoaDcl.Passaporte = dadosComprador.Passaporte;

                                        break;
                                    }
                                default:
                                    {
                                        pessoaDcl.CPF = "";
                                        pessoaDcl.Passaporte = "";

                                        break;
                                    }
                            }

                            pessoaDcl.CNPJ = "";

                            break;
                        }
                    case 2:
                        {
                            pessoaDcl.Passaporte = "";
                            pessoaDcl.CPF = "";
                            pessoaDcl.CNPJ = dadosComprador.CNPJ;

                            break;
                        }
                    default:
                        {
                            pessoaDcl.Passaporte = "";
                            pessoaDcl.CPF = "";
                            pessoaDcl.CNPJ = "";

                            break;
                        }
                }

                pessoaDcl.Email = dadosComprador.EMail;

                switch (Funcoes.Funcoes.ConvertePara.Int(dadosComprador.dTpFone))
                {
                    case 1:
                        {
                            pessoaDcl.Telefone = dadosComprador.TelefoneComMascara;
                            pessoaDcl.Celular = "";

                            break;
                        }
                    case 2:
                        {
                            pessoaDcl.Telefone = "";
                            pessoaDcl.Celular = dadosComprador.TelefoneComMascara;

                            break;
                        }
                    default:
                        {
                            pessoaDcl.Telefone = "";
                            pessoaDcl.Celular = "";

                            break;
                        }
                }

                pessoaDcl.Logradouro = dadosComprador.Logradouro;
                pessoaDcl.Logr_Nr = dadosComprador.NumeroLogradouro;
                pessoaDcl.Logr_Compl = dadosComprador.Complemento;
                pessoaDcl.Logr_Bairro = dadosComprador.Bairro;
                pessoaDcl.Logr_CEP = dadosComprador.CEP;
                pessoaDcl.Logr_Cidade = dadosComprador.Cidade;
                pessoaDcl.Logr_UF = dadosComprador.Estado;
                pessoaDcl.Logr_Pais = dadosComprador.Pais;

                pessoaDcl.Usuario = dadosComprador.EMail;
                pessoaDcl.Senha = Funcoes.Funcoes.Seguranca.Criptografar(
                    dadosComprador.Senha);
                pessoaDcl.Bloqueado = false;

                pessoaDcl.Ativo = true;
                pessoaDcl.IdOperador = 1;
                pessoaDcl.DataCadastro = DateTime.Now;
                pessoaDcl.IP = Request.UserHostAddress;
                #endregion                

                #region ACESSOS
                acessoDcl = new Acesso();
                acessoDcl.IdAcFunc = 3;
                acessoDcl.Inserir = true;
                acessoDcl.Alterar = true;
                acessoDcl.Excluir = true;
                acessoDcl.Consultar = true;
                acessoDcl.AcoesEspeciais = false;
                acessoDcl.Relatorios = true;
                acessoDcl.SuperUser = false;
                acessoDcl.TermoUso = false;

                acessoDcl.Ativo = true;
                acessoDcl.IdOperador = 1;
                acessoDcl.DataCadastro = DateTime.Now;
                acessoDcl.IP = Request.UserHostAddress;
                #endregion

                #region VIGÊNCIA PLANOS
                vigenciaPlanoDcl = new AcessosVigenciaPlano();
                vigenciaPlanoDcl.IdPlano = Funcoes.Funcoes.ConvertePara.Int(
                    dadosPlano.IdPlano);
                vigenciaPlanoDcl.DtInicial = DateTime.Today;
                vigenciaPlanoDcl.DtFinal = DateTime.Today.AddMonths(1);
                vigenciaPlanoDcl.IdCupom = dadosPlano.VoucherId;

                vigenciaPlanoDcl.Ativo = true;
                vigenciaPlanoDcl.IdOperador = 1;
                vigenciaPlanoDcl.DataCadastro = DateTime.Now;
                vigenciaPlanoDcl.IP = Request.UserHostAddress;
                #endregion

                #region VIGÊNCIA SITUAÇÃO

                for (int i = 1; i < 4; i++)
                {
                    vigenciaSituacaoDcl = new AcessosVigenciaSituacao();
                    vigenciaSituacaoDcl.IdSituacao = i;
                    vigenciaSituacaoDcl.DataSituacao = DateTime.Today;

                    vigenciaSituacaoDcl.Ativo = true;
                    vigenciaSituacaoDcl.IdOperador = 1;
                    vigenciaSituacaoDcl.DataCadastro = DateTime.Now;
                    vigenciaSituacaoDcl.IP = Request.UserHostAddress;

                    vigenciaPlanoDcl.AcessosVigenciaSituacaos.Add(vigenciaSituacaoDcl);
                }

                #endregion

                #region Coloca tudo no Banco de Dados

                pessoaDcl.AcessosVigenciaPlanos.Add(vigenciaPlanoDcl);
                pessoaDcl.Acesso.Add(acessoDcl);

                #endregion

                //Insere tudo no Banco de Dados
                retornoDados = pessoaBll.Inserir(pessoaDcl);

                #region TUTORES e InvalidaCupom
                if (retornoDados.retorno)
                {
                    if (dadosPlano.VoucherId > 0)
                    {
                        cupomDcl = cupomBll.Carregar(dadosPlano.VoucherId);

                        cupomDcl.fUsado = true;

                        cupomDcl.Ativo = true;
                        cupomDcl.IdOperador = 1;
                        cupomDcl.DataCadastro = DateTime.Now;
                        cupomDcl.IP = Request.UserHostAddress;

                        bllRetorno retAlteraCupom = cupomBll.Alterar(cupomDcl);
                    }

                    tutoresDcl = new Tutore
                    {
                        IdCliente = pessoaDcl.IdPessoa,
                        IdTutor = pessoaDcl.IdPessoa,

                        Ativo = true,
                        IdOperador = 1,
                        DataCadastro = DateTime.Now,
                        IP = Request.UserHostAddress
                    };

                    bllRetorno retornoTutores = tutoresBll.Inserir(tutoresDcl);
                }
                #endregion
            }

            return retornoDados;
        }
    }
}