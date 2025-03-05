using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;
using System.Threading;
using DCL;
using BLL;
using System.Text.RegularExpressions;
using MaskEdit;
using System.IO;
using MosaicoSolutions.ViaCep;
using MosaicoSolutions.ViaCep.Modelos;
using PagarMe;
using PhoneNumbers;

namespace Nutrovet.Perfil
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected clAcessosBll logonBll = new clAcessosBll();
        protected Acesso logonDcl = new Acesso();
        protected clPessoasBll pessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected LogrPaisBll paisesBll = new LogrPaisBll();
        protected clPessoasCartaoCreditoBll pessoaCartaoCreditoBll = new clPessoasCartaoCreditoBll();
        protected PessoasCartaoCredito pessoaCartaoCreditoDcl;
        protected TOPessoasCartaoCreditoBll pessoaCartaoCreditoTO;
        protected clPortalContatosBll contatosBll = new clPortalContatosBll();
        protected PortalContato contatosDcl;
        protected clAcessosVigenciaPlanosBll acessosVigenciaPlanosBLL = new clAcessosVigenciaPlanosBll();
        protected AcessosVigenciaPlano acessosVigenciaPlanoDCL;
        protected TOAcessosVigenciaPlanosBll acessosVigenciaPlanoTO;
        protected clAcessosVigenciaSituacaoBll situacaoBll = new clAcessosVigenciaSituacaoBll();
        protected AcessosVigenciaSituacao situacaoDcl;
        protected AcessosVigenciaSituacao vigenciaSituacaoTp1Dcl, vigenciaSituacaoTp2Dcl,
            vigenciaSituacaoTp3Dcl;
        protected clPlanosAssinaturasBll planosAssinaturasBll = new clPlanosAssinaturasBll();
        protected PlanosAssinatura planosAssinaturaDCL;
        protected clAssinaturaPMOBll _assinaturaPmoBll = new clAssinaturaPMOBll();
        protected TOAssinaturaPMOBll _assinaturaPmoTO;
        protected TOPessoasBll _pessoasTO;
        protected clPessoasBll _pessoasBll = new clPessoasBll();
        protected Pessoa _pessoasDcl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    lblAno.Text = DateTime.Today.ToString("yyyy");

                    ViewState["DadosSalvos"] = false;
                    int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
                    populaTelaPerfil(_idPessoa);
                    PopulaPais();
                    PopulaPlanoBasico();
                    PopulaPlanoIntermediario();
                    PopulaPlanoCompleto();

                    imgFoto.ImageUrl = CarregarImagem(_idPessoa);
                    lblFileUpload.Text = imgFoto.ImageUrl;
                }
            }
        }

        private void PopulaPais()
        {
            ddlPais.DataTextField = "nome_pais";
            ddlPais.DataValueField = "sigla";
            ddlPais.DataSource = paisesBll.ListarPaises();
            ddlPais.DataBind();

            ddlPais.Items.Insert(0, new ListItem("-- Selecione --", "0"));

            //if ((pessoaDcl.Logr_Pais != null) && (pessoaDcl.Logr_Pais != ""))
            //{
            //    ddlPaisAssinantePerfil1.SelectedIndex = ddlPaisAssinantePerfil1.Items.IndexOf(
            //        ddlPaisAssinantePerfil1.Items.FindByValue(pessoaDcl.Logr_Pais));
            //}
            //else
            //{
            //    ddlPaisAssinantePerfil1.SelectedIndex = ddlPaisAssinantePerfil1.Items.IndexOf(
            //        ddlPaisAssinantePerfil1.Items.FindByText("Brasil"));
            //}
        }

        private void populaTelaPerfil(int _idPessoa)
        {
            pessoaDcl = pessoaBll.Carregar(_idPessoa);

            //Tab Meus Dados
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlNacionalidadeAssinantePerfil,
                pessoaDcl.dNacionalidade);
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlTipoPessoaAssinantePerfil,
                pessoaDcl.dTpEntidade);
            SelecionaCamposTipoPessoa(ddlTipoPessoaAssinantePerfil.SelectedIndex);
            meCnpjCpfAssinantePerfil.Text = pessoaDcl.CPF ?? pessoaDcl.CNPJ;
            tbxRGAssinantePerfil.Text = pessoaDcl.RG != null ? pessoaDcl.RG : "";
            txbDataNascimentoAssinantePerfil.Text = (pessoaDcl.DataNascimento != null ?
                pessoaDcl.DataNascimento.Value.ToString("dd/MM/yyyy") : "");
            lbCodAssiante.Text = Funcoes.Funcoes.ConvertePara.String(pessoaDcl.IdPessoa);
            tbxNomeAssinantePerfil.Text = pessoaDcl.Nome;
            tbxEmailAssinantePerfil.Text = pessoaDcl.Email;
            tbxTelefoneAssinantePerfil.Text = pessoaDcl.Telefone;
            tbxCelularAssinantePerfil.Text = pessoaDcl.Celular;

            //Tab Endereço Assinante
            ddlPais.SelectedValue = pessoaDcl.Logr_Pais;

            if ((pessoaDcl.Logr_CEP != null) && (pessoaDcl.Logr_CEP != ""))
            {
                meCEPAssinantePerfil.Text = pessoaDcl.Logr_CEP;
                tbxLogradouroAssinantePerfil.Text = pessoaDcl.Logradouro;
                tbxNumeroLogradouroAssinantePerfil.Text = pessoaDcl.Logr_Nr;
                tbxComplementoLogradouroPerfil.Text = pessoaDcl.Logr_Compl;
                tbxCidadeAssinantePerfil.Text = pessoaDcl.Logr_Cidade;
                tbxBairroAssinantePerfil.Text = pessoaDcl.Logr_Bairro;
                tbxEstadoAssinantePerfil.Text = pessoaDcl.Logr_UF;
                lbCEPInformado.Text = "CEP Informado: " + pessoaDcl.Logr_CEP;
                mvTabControlEndereco.ActiveViewIndex = 1;
                BloqueiaCamposLogradouro(false);
            }

            //Tab Meu Plano
            if (!Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"]))
            {
                PopulaAbaPlanos(_idPessoa);
            }
            else
            {
                lbAlterarPlanoAssinantePerfil.Visible = false;
                lbInserirCartao.Visible = false ;
            }

            //Tab Trocar Senha

            tbxUsuarioAtualAssinantePerfil.Text = pessoaDcl.Usuario;

            tbxSenhaAtualAssinantePerfil.Text = Funcoes.Funcoes.Seguranca.Descriptografar(
                pessoaDcl.Senha);
            tbxNovaSenhaAssinantePerfil.Text = "";
            tbxConfirmacaoSenhaAssinantePerfil.Text = "";

            //Tab Mensagem
            tbxNome.Text = pessoaDcl.Nome;
            tbxEmail.Text = pessoaDcl.Email;
        }

        private void PopulaAbaPlanos(int idPessoa)
        {
            acessosVigenciaPlanoTO = acessosVigenciaPlanosBLL.CarregarPlano(idPessoa);

            if (acessosVigenciaPlanoTO != null)
            {
                ViewState["IdVigencia"] = acessosVigenciaPlanoTO.IdVigencia;

                //Labels

                int _valorStatusPagarMe = Funcoes.Funcoes.CarregarEnumValor<
                    DominiosBll.AcessosPlanosAuxSituacaoIngles>(acessosVigenciaPlanoTO.StatusPagarMe);

                lbValorCodigoAssinaturaAssinantePerfil.Text = 
                    acessosVigenciaPlanoTO.IdSubscriptionPagarMe;
                lblPlanoAssinantePerfil.Text = acessosVigenciaPlanoTO.Plano + " - " +
                    acessosVigenciaPlanoTO.Periodo;
                lbDataCadastroAssinaturaAssinantePerfil.Text =
                    (acessosVigenciaPlanoTO.DataCadastro != null ?
                     acessosVigenciaPlanoTO.DataCadastro.Value.ToString("dd/MM/yyyy") : "");
                lbDataInicialAssinaturaAssinantePerfil.Text = (acessosVigenciaPlanoTO.DtInicial != null ?
                    acessosVigenciaPlanoTO.DtInicial.ToString("dd/MM/yyyy") : "");
                lbDataFinalAssinaturaAssinantePerfil.Text = (acessosVigenciaPlanoTO.DtFinal != null ?
                    acessosVigenciaPlanoTO.DtFinal.ToString("dd/MM/yyyy") : "");
                lbStatusAssinaturaAssinantePerfil.Text =
                    (acessosVigenciaPlanoTO.IdSubscriptionPagarMe != null && _valorStatusPagarMe > 0 ?
                        Funcoes.Funcoes.CarregarEnumNome<DominiosBll.AcessosPlanosAuxSituacao>(
                            _valorStatusPagarMe) : "Outros");
                lblValorPlanoAssinantePerfil.Text = string.Format("{0:c}",
                    acessosVigenciaPlanoTO.ValorPlano);

                //botões
                lbAlterarPlanoAssinantePerfil.Visible = 
                    acessosVigenciaPlanosBLL.MostraBotaoRenovarAssinatura(
                        Funcoes.Funcoes.CarregarEnumItem<DominiosBll.AcessosPlanosAuxSituacaoIngles>(
                            acessosVigenciaPlanoTO.StatusPagarMe), acessosVigenciaPlanoTO.DtFinal);

                if ((acessosVigenciaPlanoTO.IdSubscriptionPagarMe != null) &&
                    (Funcoes.Funcoes.ConvertePara.Int(acessosVigenciaPlanoTO.IdSubscriptionPagarMe) > 0))
                {
                    lbNumeroCartaoCreditoAssinantePerfil.Text = acessosVigenciaPlanoTO.NrCartao;
                    lbVencimentoCartaoCreditoAssinantePerfil.Text = (
                        acessosVigenciaPlanoTO.VencimCartao != null &&
                            acessosVigenciaPlanoTO.VencimCartao != "" ?
                                acessosVigenciaPlanoTO.VencimCartao.Insert(2, "/") : "");
                    lbNomeCartaoCreditoAssinantePerfil.Text = acessosVigenciaPlanoTO.NomeCartao;
                }

                PopulaAssinaturas(acessosVigenciaPlanoTO.IdPessoa);
                PopulaGridCartaoes(acessosVigenciaPlanoTO.IdPessoa);
            }
        }

        private void PopulaAssinaturas(int idPessoa)
        {
            rptAssinaturas.DataSource = null;
            rptAssinaturas.DataBind();

            List<TOAssinaturaPMOBll> assinaturasTO = new List<TOAssinaturaPMOBll>();

            assinaturasTO = _assinaturaPmoBll.ListarAssinaturasClientesRealTime(idPessoa);

            rptAssinaturas.DataSource = assinaturasTO;
            rptAssinaturas.DataBind();
        }

        private void PopulaGridCartaoes(int idPessoa)
        {
            rptListagemCartoesCredito.DataSource = pessoaCartaoCreditoBll.Listar(idPessoa, true);
            rptListagemCartoesCredito.DataBind();
        }

        private void PopulaCartaoesAssinatura(int idPessoa)
        {
            ddlCCRenovAssin.DataTextField = "NrCartaoComposto";
            ddlCCRenovAssin.DataValueField = "IdCartao";
            ddlCCRenovAssin.DataSource = pessoaCartaoCreditoBll.Listar(idPessoa, true);
            ddlCCRenovAssin.DataBind();

            ddlCCRenovAssin.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        protected void BloqueiaCamposLogradouro(bool _ativa)
        {
            tbxLogradouroAssinantePerfil.Enabled = _ativa;
            tbxBairroAssinantePerfil.Enabled = _ativa;
            tbxCidadeAssinantePerfil.Enabled = _ativa;
            tbxEstadoAssinantePerfil.Enabled = _ativa;
        }

        protected void aMeusDadosPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 0;
            liMeusDadosPerfil.Attributes["class"] = "active";
            liMeuPlanoPerfil.Attributes["class"] = "";
            liTrocaSenhaPerfil.Attributes["class"] = "";
            liImagemPerfil.Attributes["class"] = "";
            liMensagemPerfil.Attributes["class"] = "";
        }

        protected void aMeuPlanoPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 1;
            liMeusDadosPerfil.Attributes["class"] = "";
            liMeuPlanoPerfil.Attributes["class"] = "active";
            liTrocaSenhaPerfil.Attributes["class"] = "";
            liImagemPerfil.Attributes["class"] = "";
            liMensagemPerfil.Attributes["class"] = "";
        }

        protected void aTrocarSenhaPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 2;
            liMeusDadosPerfil.Attributes["class"] = "";
            liMeuPlanoPerfil.Attributes["class"] = "";
            liTrocaSenhaPerfil.Attributes["class"] = "active";
            liImagemPerfil.Attributes["class"] = "";
            liMensagemPerfil.Attributes["class"] = "";
        }

        protected void aImagemPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 3;
            liMeusDadosPerfil.Attributes["class"] = "";
            liMeuPlanoPerfil.Attributes["class"] = "";
            liTrocaSenhaPerfil.Attributes["class"] = "";
            liImagemPerfil.Attributes["class"] = "active";
            liMensagemPerfil.Attributes["class"] = "";
        }

        protected void aMensagemPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 4;
            liMeusDadosPerfil.Attributes["class"] = "";
            liMeuPlanoPerfil.Attributes["class"] = "";
            liTrocaSenhaPerfil.Attributes["class"] = "";
            liImagemPerfil.Attributes["class"] = "";
            liMensagemPerfil.Attributes["class"] = "active";
        }

        protected void lbAlterarPlano_Click(object sender, EventArgs e)
        {
            bllRetorno _validaTela = ValidaCampos(0);
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);

            if (_validaTela.retorno)
            {
                LimpaCamposCartaoCredito();
                PopulaCartaoesAssinatura(_idPessoa);

                lblTituloModalRenovarAssinatura.Text = "Selecionando Plano e Cartão de Crédito";
                popUpModalEscolhaPlanoRenovacao.Show();
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    _validaTela.mensagem, "Atenção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            AlterarPessoas(_idPessoa, true);
        }

        private void AlterarPessoas(int _id, bool _mostrarToastr)
        {
            int _tpPessoa = Funcoes.Funcoes.ConvertePara.Int(ddlTipoPessoaAssinantePerfil.SelectedValue);

            if ((_tpPessoa == 1) && (meCnpjCpfAssinantePerfil.Text != "") &&
                ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
            {
                meCnpjCpfAssinantePerfil.Text = FormataCPF(meCnpjCpfAssinantePerfil.Text);
            }

            if ((_tpPessoa == 2) && (meCnpjCpfAssinantePerfil.Text != "") &&
                ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
            {
                meCnpjCpfAssinantePerfil.Text = FormataCNPJ(meCnpjCpfAssinantePerfil.Text);
            }

            if ((tbxCelularAssinantePerfil.Text != "") && (tbxCelularAssinantePerfil.Text.Length >= 10))
            {
                tbxCelularAssinantePerfil.Text = FormataCelular(tbxCelularAssinantePerfil.Text);
            }

            if ((meCEPAssinantePerfil.Text != "") && (meCEPAssinantePerfil.Text.Length >= 8) &&
                (ddlNacionalidadeAssinantePerfil.SelectedValue == "1"))
            {
                meCEPAssinantePerfil.Text = FormataCEP(meCEPAssinantePerfil.Text);
            }

            pessoaDcl = pessoaBll.Carregar(_id);

            pessoaDcl.Nome = tbxNomeAssinantePerfil.Text;
            pessoaDcl.Email = tbxEmailAssinantePerfil.Text;
            pessoaDcl.Telefone = tbxTelefoneAssinantePerfil.Text;
            pessoaDcl.Celular = tbxCelularAssinantePerfil.Text;
            pessoaDcl.DataNascimento = (txbDataNascimentoAssinantePerfil.Text != "" ?
                DateTime.Parse(txbDataNascimentoAssinantePerfil.Text) :
                DateTime.Parse("01/01/1910"));

            if (ddlTipoPessoaAssinantePerfil.SelectedIndex == 0)
            {
                pessoaDcl.CPF = meCnpjCpfAssinantePerfil.Text;
                pessoaDcl.RG = tbxRGAssinantePerfil.Text;
                pessoaDcl.dTpEntidade = 1;
            }
            else if (ddlTipoPessoaAssinantePerfil.SelectedIndex == 1)
            {
                pessoaDcl.CNPJ = meCnpjCpfAssinantePerfil.Text;
                pessoaDcl.dTpEntidade = 2;
            }
            else
            {
                pessoaDcl.CPF = "";
                pessoaDcl.RG = "";
                pessoaDcl.CNPJ = "";
                pessoaDcl.dTpEntidade = 1;
            }

            pessoaDcl.dNacionalidade = Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinantePerfil.SelectedValue);
            pessoaDcl.Logr_Pais = ddlPais.Visible == true ?
                ddlPais.SelectedItem.Value : "";
            pessoaDcl.Logr_CEP = meCEPAssinantePerfil.Text;
            pessoaDcl.Logradouro = tbxLogradouroAssinantePerfil.Text;
            pessoaDcl.Logr_Nr = tbxNumeroLogradouroAssinantePerfil.Text;
            pessoaDcl.Logr_Compl = tbxComplementoLogradouroPerfil.Text;
            pessoaDcl.Logr_Bairro = tbxBairroAssinantePerfil.Text;
            pessoaDcl.Logr_Cidade = tbxCidadeAssinantePerfil.Text;
            pessoaDcl.Logr_UF = tbxEstadoAssinantePerfil.Text;

            pessoaDcl.Ativo = true;
            pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            pessoaDcl.DataCadastro = DateTime.Now;
            pessoaDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = pessoaBll.Alterar(pessoaDcl);

            if (alterarRet.retorno)
            {
                Pessoa usuarioDcl = (Pessoa)Session["_dadosBasicos"];
                usuarioDcl.Nome = pessoaDcl.Nome;
                Session["_dadosBasicos"] = usuarioDcl;
                ViewState["DadosSalvos"] = true;

                if (_mostrarToastr)
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Success, alterarRet.mensagem,
                        "Informe NutroVET - Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true); 
                }

            }
            else
            {
                if (_mostrarToastr)
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning, alterarRet.mensagem,
                        "Informe NutroVET - Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true); 
                }
            }
        }

        protected void lbSalvarAlteracaoUsuarioAcesso_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            AlterarUsuarioAcesso(_idPessoa);
        }

        protected void lbSalvarAlteracaoSenhaAcesso_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            AlterarSenhaAcesso(_idPessoa);
        }

        private void AlterarUsuarioAcesso(int _id)
        {
            pessoaDcl = pessoaBll.Carregar(_id);

            pessoaDcl.Usuario = tbxUsuarioNovoAssinantePerfil.Text;

            pessoaDcl.Ativo = true;
            pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            pessoaDcl.DataCadastro = DateTime.Now;
            pessoaDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = pessoaBll.AlterarLogon(pessoaDcl, false);

            if (alterarRet.retorno)
            {
                tbxUsuarioNovoAssinantePerfil.Text = "";
                tbxUsuarioAtualAssinantePerfil.Text = pessoaDcl.Usuario;

                Funcoes.Funcoes.Toastr.ShowToast(this,
                  Funcoes.Funcoes.Toastr.ToastType.Success,
                  alterarRet.mensagem,
                  "Informe NutroVET - Alteração de Usuário",
                  Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                  true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    alterarRet.mensagem,
                    "Informe NutroVET - Alteração de Usuário",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void AlterarSenhaAcesso(int _id)
        {
            if (tbxNovaSenhaAssinantePerfil.Text == 
                tbxConfirmacaoSenhaAssinantePerfil.Text)
            {
                pessoaDcl = pessoaBll.Carregar(_id);

                pessoaDcl.Senha = Funcoes.Funcoes.Seguranca.Criptografar(
                    tbxNovaSenhaAssinantePerfil.Text);

                pessoaDcl.Ativo = true;
                pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                pessoaDcl.DataCadastro = DateTime.Now;
                pessoaDcl.IP = Request.UserHostAddress;

                bllRetorno alterarRet = pessoaBll.AlterarLogon(pessoaDcl, true);

                if (alterarRet.retorno)
                {
                    tbxSenhaAtualAssinantePerfil.Text = tbxNovaSenhaAssinantePerfil.Text;
                    tbxNovaSenhaAssinantePerfil.Text = "";
                    tbxConfirmacaoSenhaAssinantePerfil.Text = "";

                    Funcoes.Funcoes.Toastr.ShowToast(this,
                      Funcoes.Funcoes.Toastr.ToastType.Success,
                      alterarRet.mensagem,
                      "Informe NutroVET - Alteração de Senha",
                      Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                      true);
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        alterarRet.mensagem,
                        "Informe NutroVET - Alteração de Senha",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Senhas não Conferem!",
                    "Informe NutroVET - Alteração de Senha",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }



        protected void lbEmail_Click(object sender, EventArgs e)
        {
            contatosDcl = new PortalContato
            {
                Assunto = tbxAssunto.Text,
                NomeContato = tbxNome.Text,
                EmailContato = tbxEmail.Text,
                Mensagem = tbxMsg.Text,
                DataEnvio = DateTime.Today,
                DataResposta = DateTime.Parse("01/01/1910"),
                MsgSituacao = (int)DominiosBll.PortalContatoAuxSituacao.Enviada
            };

            bllRetorno _ret = contatosBll.Inserir(contatosDcl);

            if (_ret.retorno)
            {
                LimpaTela();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                  Funcoes.Funcoes.Toastr.ToastType.Success, _ret.mensagem,
                  "Mensagem", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                  true);

            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning, _ret.mensagem,
                    "Mensagem", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void LimpaTela()
        {
            tbxAssunto.Text = "";
            tbxNome.Text = "";
            tbxEmail.Text = "";
            tbxMsg.Text = "";
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Session.Remove("ToastrPessoa");
            Response.Redirect("~/MenuGeral.aspx");
        }

        protected void lbEnviaImagem_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.PostedFile.ContentLength < 512000)
                {
                    string _extensao = Path.GetExtension(FileUpload1.FileName);
                    string _cliente = "Cliente_" + User.Identity.Name + _extensao;

                    try
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/Perfil/Fotos/") +
                             _cliente);
                        lblFileUpload.Text = "Arquivo Gravado como: " + _cliente;

                        imgFoto.ImageUrl = CarregarImagem(Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name));
                    }
                    catch (Exception ex)
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Error,
                            "ERRO: " + ex.Message.ToString(),
                            "Informe NutroVET - Upload",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Tamanho do Arquivo Inválido!!!<br />Máx: 500 Kb",
                        "Informe NutroVET - Upload",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Arquivo não Selecionado!!!",
                    "Informe NutroVET - Upload",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected string CarregarImagem(int idPessoa)
        {
            string path = Server.MapPath("~/Perfil/Fotos/");
            DirectoryInfo _directorio = new DirectoryInfo(path);

            string search = (from d in _directorio.GetFiles("Cliente_" + idPessoa + ".*")
                             orderby d.LastAccessTime descending
                             select d.Name).FirstOrDefault();

            return "~/Perfil/Fotos/" + search + "?time=" + DateTime.Now.ToString();
        }

        protected void ddlTpPessoaAssinante_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _tpPessoa = Funcoes.Funcoes.ConvertePara.Int(ddlTipoPessoaAssinantePerfil.SelectedIndex);
            SelecionaCamposTipoPessoa(_tpPessoa);
        }

        protected void ddlNacionalidadeAssinante_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpaCampos();

            CamposInternacionais(Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinantePerfil.SelectedValue));
        }

        private void CamposInternacionais(int _nacionalidadeAssinante)
        {
            if (_nacionalidadeAssinante == 1)
            {
                lbCEPAssinantePerfil.Text = "CEP";
                meCnpjCpfAssinantePerfil.Visible = true;
                tbxPassaporteAssinante.Visible = false;
                lbTituloRGAssinantePerfil.Visible = true;
                divRGAssinantePerfil.Visible = true;

                Mascaras(ddlNacionalidadeAssinantePerfil.SelectedValue, false);

                switch (Funcoes.Funcoes.ConvertePara.Int(
                    ddlTipoPessoaAssinantePerfil.SelectedValue))
                {
                    case 1:
                        {
                            lbTituloTipoPessoaAssinantePerfil.Text = "CPF";
                            meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CPF;
                            meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CPF";
                            meCnpjCpfAssinantePerfil.Attributes["title"] = "CPF do Assinante";
                            break;
                        }
                    case 2:
                        {
                            lbTituloTipoPessoaAssinantePerfil.Text = "CNPJ";
                            meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CNPJ;
                            meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CNPJ";
                            meCnpjCpfAssinantePerfil.Attributes["title"] = "CNPJ do Assinante";

                            break;
                        }
                }
            }
            else
            {
                lbTituloTipoPessoaAssinantePerfil.Text = "Passaporte";
                meCnpjCpfAssinantePerfil.Visible = false;
                tbxPassaporteAssinante.Visible = true;

                lbTituloRGAssinantePerfil.Visible = false;
                divRGAssinantePerfil.Visible = false;

                lbCEPAssinantePerfil.Text = "ZIP CODE";
                Mascaras(ddlNacionalidadeAssinantePerfil.SelectedValue, false);
            }
        }

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpaCampos();

            CamposInternacionais(Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinantePerfil.SelectedValue));
        }

        private void Mascaras(string _nacional, bool _apagaInformacoes)
        {
            int _selecao = Funcoes.Funcoes.ConvertePara.Int(_nacional);

            if (_apagaInformacoes)
            {
                meCEPAssinantePerfil.Text = "";
            }

            switch (_selecao)
            {
                case 1:
                    {
                        tbxTelefoneAssinantePerfil.Mascara = MEdit.TpMascara.Telefone;
                        tbxCelularAssinantePerfil.Mascara = MEdit.TpMascara.Telefone;
                        tbxTelefoneAssinantePerfil.Attributes["placeholder"] = "(xx) xxxx-xxxx";
                        tbxCelularAssinantePerfil.Attributes["placeholder"] = "(xx) xxxx-xxxx";
                        meCEPAssinantePerfil.Mascara = MEdit.TpMascara.CEP;
                        meCEPAssinantePerfil.Attributes["placeholder"] = "xxxxx-xxx";
                        LiberaCamposLogradouro(false);

                        break;
                    }
                case 2:
                    {
                        tbxTelefoneAssinantePerfil.Mascara = MEdit.TpMascara.String;
                        tbxTelefoneAssinantePerfil.Attributes["placeholder"] = "";
                        tbxCelularAssinantePerfil.Mascara = MEdit.TpMascara.String;
                        tbxCelularAssinantePerfil.Attributes["placeholder"] = "";
                        meCEPAssinantePerfil.Mascara = MEdit.TpMascara.String;
                        meCEPAssinantePerfil.Attributes["placeholder"] = "";
                        LiberaCamposLogradouro(true);

                        break;
                    }
            }
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

        private void SelecionaCamposTipoPessoa(int _tpPessoa)
        {
            switch (_tpPessoa)
            {
                case 0:
                    {
                        lbNomeAssinantePerfil.Text = "Nome";
                        lbTituloTipoPessoaAssinantePerfil.Text = "CPF";
                        meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CPF;
                        meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CPF";
                        meCnpjCpfAssinantePerfil.Attributes["title"] = "CPF do Assinante";
                        lbTituloRGAssinantePerfil.Visible = true;
                        divRGAssinantePerfil.Visible = true;
                        tbxNomeAssinantePerfil.Attributes["placeholder"] = "Nome do Assinante";
                        tbxNomeAssinantePerfil.Attributes["title"] = "Nome do Assinante";
                        divDataNascimentoAssinantePerfil.Visible = true;
                        meCnpjCpfAssinantePerfil.Text = "";
                        tbxRGAssinantePerfil.Text = "";
                        txbDataNascimentoAssinantePerfil.Text = "";

                        if (ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
                        {
                            lbTituloTipoPessoaAssinantePerfil.Text = "CPF";
                            meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CPF;
                            meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CPF";
                            meCnpjCpfAssinantePerfil.Attributes["title"] = "CPF do Assinante";
                        }
                        else
                        {
                            lbTituloTipoPessoaAssinantePerfil.Text = "Passaporte";
                            meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.String;
                            meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "Passaporte";
                            meCnpjCpfAssinantePerfil.Attributes["title"] = "Passaporte do Assinante";
                        }
                        break;
                    }
                case 1:
                    {
                        lbNomeAssinantePerfil.Text = "Razão Social";
                        lbTituloTipoPessoaAssinantePerfil.Text = "CNPJ";
                        meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CNPJ;
                        meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CNPJ";
                        meCnpjCpfAssinantePerfil.Attributes["title"] = "CNPJ do Assinante";
                        lbTituloRGAssinantePerfil.Visible = false;
                        divRGAssinantePerfil.Visible = false;
                        tbxNomeAssinantePerfil.Attributes["placeholder"] = "Razão Social do Assinante";
                        tbxNomeAssinantePerfil.Attributes["title"] = "Razão Social do Assinante";
                        divDataNascimentoAssinantePerfil.Visible = false;
                        meCnpjCpfAssinantePerfil.Text = "";
                        tbxRGAssinantePerfil.Text = "";
                        txbDataNascimentoAssinantePerfil.Text = "";

                        if (ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
                        {
                            lbTituloTipoPessoaAssinantePerfil.Text = "CNPJ";
                            meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CNPJ;
                            meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CNPJ";
                            meCnpjCpfAssinantePerfil.Attributes["title"] = "CNPJ do Assinante";
                        }
                        else
                        {
                            lbTituloTipoPessoaAssinantePerfil.Text = "Passaporte";
                            meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.String;
                            meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "Passaporte";
                            meCnpjCpfAssinantePerfil.Attributes["title"] = "Passaporte do Assinante";
                        }
                        break;
                    }
            }
        }

        private void LiberaCamposLogradouro(bool _liberar)
        {
            tbxLogradouroAssinantePerfil.Enabled = _liberar;
            tbxBairroAssinantePerfil.Enabled = _liberar;
            tbxCidadeAssinantePerfil.Enabled = _liberar;
            tbxEstadoAssinantePerfil.Enabled = _liberar;
        }

        protected void tbCEP_TextChanged(object sender, EventArgs e)
        {
            if (meCEPAssinantePerfil.Text != "")
            {
                int _nacional = Funcoes.Funcoes.ConvertePara.Int(
                    ddlNacionalidadeAssinantePerfil.SelectedValue);

                switch (_nacional)
                {
                    case 1:
                        {
                            try
                            {
                                meCEPAssinantePerfil.Mascara = MEdit.TpMascara.CEP;
                                meCEPAssinantePerfil.Attributes["placeholder"] = "xxxxx-xxx";

                                Cep _cep = meCEPAssinantePerfil.Text;
                                var viaCepService = ViaCepService.Default();
                                var endereco = viaCepService.ObterEndereco(_cep);

                                tbxLogradouroAssinantePerfil.Text = endereco.Logradouro;
                                tbxBairroAssinantePerfil.Text = endereco.Bairro;
                                tbxCidadeAssinantePerfil.Text = endereco.Localidade;
                                tbxEstadoAssinantePerfil.Text = endereco.UF;

                                lbCEPInformado.Text = "CEP Informado: " + meCEPAssinantePerfil.Text;

                                if (endereco.Logradouro != "")
                                {
                                    LiberaCamposLogradouro(false);
                                }
                                else
                                {
                                    LiberaCamposLogradouro(true);
                                }

                                ddlPais.SelectedIndex = ddlPais.Items.IndexOf(
                                    ddlPais.Items.FindByText("Brasil"));

                                mvTabControlEndereco.ActiveViewIndex = 1;

                                meCnpjCpfAssinantePerfil.Visible = true;
                                tbxPassaporteAssinante.Visible = false;

                            }
                            catch (Exception err)
                            {
                                lbCEPInformado.Text = "CEP Informado: " + meCEPAssinantePerfil.Text;
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
                            meCEPAssinantePerfil.Mascara = MEdit.TpMascara.String;
                            meCEPAssinantePerfil.Attributes["placeholder"] = "";
                            lbCEPInformado.Text = "ZIP CODE Informado: " + meCEPAssinantePerfil.Text;
                            LiberaCamposLogradouro(true);
                            mvTabControlEndereco.ActiveViewIndex = 1;
                            meCnpjCpfAssinantePerfil.Visible = false;
                            tbxPassaporteAssinante.Visible = true;
                            break;
                        }
                }
            }
        }

        protected void lbCorrigirCEPInformado_Click(object sender, EventArgs e)
        {
            int _nacional = Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinantePerfil.SelectedValue);

            meCEPAssinantePerfil.Text = "";
            tbxNumeroLogradouroAssinantePerfil.Text = "";
            tbxComplementoLogradouroPerfil.Text = "";

            switch (_nacional)
            {
                case 1:
                    {
                        meCEPAssinantePerfil.Mascara = MEdit.TpMascara.CEP;
                        meCEPAssinantePerfil.Attributes["placeholder"] = "xxxxx-xxx";
                        lbCEPInformado.Text = "";
                        ddlPais.SelectedIndex = ddlPais.Items.IndexOf(
                            ddlPais.Items.FindByValue("Brasil"));

                        break;
                    }
                case 2:
                    {
                        meCEPAssinantePerfil.Mascara = MEdit.TpMascara.String;
                        meCEPAssinantePerfil.Attributes["placeholder"] = "";
                        lbCEPInformado.Text = "";
                        ddlPais.SelectedIndex = ddlPais.Items.IndexOf(
                            ddlPais.Items.FindByValue("Estados Unidos"));
                        break;
                    }
            }

            mvTabControlEndereco.ActiveViewIndex = 0;
        }

        protected void ddlNacionalidadeAssinantePerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _nacionalidadeAssinante = Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinantePerfil.SelectedValue);

            if (_nacionalidadeAssinante == 1)
            {
                lbCEPAssinantePerfil.Text = "CEP";
                meCEPAssinantePerfil.ToolTip = "CEP";
                lbCEPInformado.Text = "CEP Informado";
                meCnpjCpfAssinantePerfil.Visible = true;
                tbxPassaporteAssinante.Visible = false;
                SelecionaCamposTipoDocumentoEstrangeiro(2);

                switch (Funcoes.Funcoes.ConvertePara.Int(ddlTipoPessoaAssinantePerfil.SelectedValue))
                {
                    case 1:
                        {
                            lbTituloTipoPessoaAssinantePerfil.Text = "CPF";
                            meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CPF;
                            meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CPF";
                            meCnpjCpfAssinantePerfil.Attributes["title"] = "CPF do Assinante";
                            break;
                        }
                    case 2:
                        {
                            lbTituloTipoPessoaAssinantePerfil.Text = "CNPJ";
                            meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CNPJ;
                            meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CNPJ";
                            meCnpjCpfAssinantePerfil.Attributes["title"] = "CNPJ do Assinante";

                            break;
                        }
                }
            }
            else
            {
                lbCEPAssinantePerfil.Text = "ZIP CODE";
                meCEPAssinantePerfil.ToolTip = "ZIP CODE";
                lbCEPInformado.Text = "ZIP CODE";
                meCnpjCpfAssinantePerfil.Visible = false;
                tbxPassaporteAssinante.Visible = true;
                SelecionaCamposTipoDocumentoEstrangeiro(1);

            }
        }

        private void SelecionaCamposTipoDocumentoEstrangeiro(int _tpDocumentoEstrengeiro)
        {

            switch (_tpDocumentoEstrengeiro)
            {
                case 1:
                    {
                        lbTituloTipoPessoaAssinantePerfil.Text = "Passaporte";
                        meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.String;

                        meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "Passaporte";
                        meCnpjCpfAssinantePerfil.Attributes["title"] = "Passaporte do Assinante";
                        meCnpjCpfAssinantePerfil.Visible = false;
                        tbxPassaporteAssinante.Visible = true;
                        break;
                    }
                case 2:
                    {
                        lbTituloTipoPessoaAssinantePerfil.Text = "CPF";
                        meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CPF;
                        meCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CPF";
                        meCnpjCpfAssinantePerfil.Attributes["title"] = "CPF";
                        meCnpjCpfAssinantePerfil.Visible = true;
                        tbxPassaporteAssinante.Visible = false;
                        break;
                    }
            }
        }

        private void LimpaCampos()
        {
            tbxPassaporteAssinante.Visible = false;
            tbxPassaporteAssinante.Text = "";

            if (ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
            {
                ddlPais.SelectedIndex = ddlPais.Items.IndexOf(
                    ddlPais.Items.FindByValue("Brasil"));
            }
            else
            {
                ddlPais.SelectedIndex = ddlPais.Items.IndexOf(
                    ddlPais.Items.FindByValue("Estados Unidos"));
            }

            meCEPAssinantePerfil.Text = "";
            tbxLogradouroAssinantePerfil.Text = "";
            tbxNumeroLogradouroAssinantePerfil.Text = "";
            tbxComplementoLogradouroPerfil.Text = "";
            tbxBairroAssinantePerfil.Text = "";
            tbxCidadeAssinantePerfil.Text = "";
            tbxEstadoAssinantePerfil.Text = "";
            lbCEPInformado.Text = "";
            mvTabControlEndereco.ActiveViewIndex = 0;
        }

        protected void rptListagemCartoesCredito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _idCartao = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "inserir":
                    {
                        lblTituloModal.Text = "Inserindo novo Cartão de Crédito";

                        popUpModal.Show();

                        break;
                    }
                case "editar":
                    {
                        lblTituloModal.Text = "Alterando Cartão de Crédito";
                        pessoaCartaoCreditoDcl = pessoaCartaoCreditoBll.Carregar(_idCartao);

                        meNumeroCartaoCredito.Text = pessoaCartaoCreditoDcl.NrCartao;
                        meCodigoSegurancaCartaoCredito.Text = pessoaCartaoCreditoDcl.CodSeg;
                        tbValidadeCartaoCreditoModal.Text = pessoaCartaoCreditoDcl.VencimCartao;
                        tbNomeCartaoCredito.Text = pessoaCartaoCreditoDcl.NomeCartao;

                        ViewState["IdCartao"] = _idCartao;

                        popUpModal.Show();

                        break;
                    }
                case "excluir":
                    {
                        ExcluirCartaoCredito(_idCartao);

                        break;
                    }
                case "vincular":
                    {
                        VincularCartaoCredito(_idCartao);

                        break;
                    }
            }
        }

        private void InserirCartaoCredito()
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            bllRetorno retornoDados = new bllRetorno();

            #region CARTÃO DE CRÉDITO
            pessoaCartaoCreditoDcl = new PessoasCartaoCredito();
            pessoaCartaoCreditoDcl.IdPessoa = _idPessoa;
            pessoaCartaoCreditoDcl.NrCartao = meNumeroCartaoCredito.Text.Replace(" ", "");
            pessoaCartaoCreditoDcl.dBandeira = pessoaCartaoCreditoBll.BandeiraCreditCard(
                pessoaCartaoCreditoDcl.NrCartao);
            pessoaCartaoCreditoDcl.CodSeg = meCodigoSegurancaCartaoCredito.Text;
            pessoaCartaoCreditoDcl.VencimCartao = tbValidadeCartaoCreditoModal.Text;
            pessoaCartaoCreditoDcl.NomeCartao = tbNomeCartaoCredito.Text;

            pessoaCartaoCreditoDcl.Ativo = true;
            pessoaCartaoCreditoDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            pessoaCartaoCreditoDcl.DataCadastro = DateTime.Now;
            pessoaCartaoCreditoDcl.IP = Request.UserHostAddress;
            #endregion

            //Insere tudo no Banco de Dados
            retornoDados = pessoaCartaoCreditoBll.Inserir(pessoaCartaoCreditoDcl);

            if (retornoDados.retorno)
            {
                LimpaCamposCartaoCredito();
                PopulaGridCartaoes(_idPessoa);

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, retornoDados.mensagem,
                    "NutroVET informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, retornoDados.mensagem,
                    "NutroVET informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);

                popUpModal.Show();
            }
        }

        private void AlterarCartaoCredito(int idCartao)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            bllRetorno retornoDados = new bllRetorno();

            #region CARTÃO DE CRÉDITO
            pessoaCartaoCreditoDcl = pessoaCartaoCreditoBll.Carregar(idCartao);

            pessoaCartaoCreditoDcl.NrCartao = meNumeroCartaoCredito.Text.Replace(" ", "");
            //pessoaCartaoCreditoDcl.dBandeira = pessoaCartaoCreditoBll.BandeiraCreditCard(
            //    pessoaCartaoCreditoDcl.NrCartao);
            pessoaCartaoCreditoDcl.CodSeg = meCodigoSegurancaCartaoCredito.Text;
            pessoaCartaoCreditoDcl.VencimCartao = tbValidadeCartaoCreditoModal.Text;
            pessoaCartaoCreditoDcl.NomeCartao = tbNomeCartaoCredito.Text;

            pessoaCartaoCreditoDcl.Ativo = true;
            pessoaCartaoCreditoDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            pessoaCartaoCreditoDcl.DataCadastro = DateTime.Now;
            pessoaCartaoCreditoDcl.IP = Request.UserHostAddress;
            #endregion

            //Altera tudo no Banco de Dados
            retornoDados = pessoaCartaoCreditoBll.Alterar(pessoaCartaoCreditoDcl);

            if (retornoDados.retorno)
            {
                LimpaCamposCartaoCredito();
                PopulaGridCartaoes(_idPessoa);

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, retornoDados.mensagem,
                    "NutroVET informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, retornoDados.mensagem,
                    "NutroVET informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);

                popUpModal.Show();
            }
        }

        protected void rptListagemCartoesCredito_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) ||
                (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton lbVincularCCRegistro = (LinkButton)e.Item.FindControl("lbVincularCCRegistro");

                pessoaCartaoCreditoTO = (TOPessoasCartaoCreditoBll)e.Item.DataItem;
                acessosVigenciaPlanoTO = acessosVigenciaPlanosBLL.CarregarTO(
                    Funcoes.Funcoes.ConvertePara.Int(ViewState["IdVigencia"]));

                if (pessoaCartaoCreditoTO.IdCartao == acessosVigenciaPlanoTO.IdCartao)
                {
                    lbNumeroCartaoCreditoAssinantePerfil.Text = acessosVigenciaPlanoTO.NrCartao;
                    lbVencimentoCartaoCreditoAssinantePerfil.Text = (
                        acessosVigenciaPlanoTO.VencimCartao != null &&
                            acessosVigenciaPlanoTO.VencimCartao != "" ?
                                acessosVigenciaPlanoTO.VencimCartao.Insert(2, "/") : "");
                    lbNomeCartaoCreditoAssinantePerfil.Text = acessosVigenciaPlanoTO.NomeCartao;

                    lbVincularCCRegistro.Text = "<i class='far fa-dot-circle'></i>";
                    ViewState["IdCartao"] = pessoaCartaoCreditoTO.IdCartao;
                }
                else
                {
                    lbVincularCCRegistro.Text = "<i class='far fa-circle'></i>";
                }
            }
        }

        private void LimpaCamposCartaoCredito()
        {
            meNumeroCartaoCredito.Text = "";
            meCodigoSegurancaCartaoCredito.Text = "";
            tbValidadeCartaoCreditoModal.Text = "";
            tbNomeCartaoCredito.Text = "";
            ViewState.Remove("IdCartao");
        }

        private void ExcluirCartaoCredito(int _id)
        {
            acessosVigenciaPlanoTO = acessosVigenciaPlanosBLL.CarregarTO(
                    Funcoes.Funcoes.ConvertePara.Int(ViewState["IdVigencia"]));

            if ((acessosVigenciaPlanoTO != null) && (acessosVigenciaPlanoTO.IdVigencia > 0))
            {
                pessoaCartaoCreditoDcl = pessoaCartaoCreditoBll.Carregar(_id);

                if (pessoaCartaoCreditoDcl.IdCartao == acessosVigenciaPlanoTO.IdCartao)
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning, 
                        "Você NÃO Pode Excluir um Cartão Associado a um Assinatura!!! Substitua o Cartão da Assinatura",
                        "NutroVET informa - Exclusão", 
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
                else
                {
                    pessoaCartaoCreditoDcl.Ativo = false;
                    pessoaCartaoCreditoDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
                    pessoaCartaoCreditoDcl.DataCadastro = DateTime.Now;
                    pessoaCartaoCreditoDcl.IP = Request.UserHostAddress;

                    bllRetorno ret = pessoaCartaoCreditoBll.Alterar(pessoaCartaoCreditoDcl);

                    if (ret.retorno)
                    {
                        LimpaCamposCartaoCredito();
                        PopulaGridCartaoes(pessoaCartaoCreditoDcl.IdPessoa);

                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Success, ret.mensagem,
                            "NutroVET informa - Exclusão",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                    else
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Error, ret.mensagem,
                            "NutroVET informa - Exclusão",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
            }
        }

        protected void rptAssinaturas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) ||
                (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Label lblStatusReg = (Label)e.Item.FindControl("lblStatusReg");
                Label lblValorReg = (Label)e.Item.FindControl("lblValorReg");
                Label lblInicioReg = (Label)e.Item.FindControl("lblInicioReg");
                Label lblFimReg = (Label)e.Item.FindControl("lblFimReg");
                LinkButton lbCancelarAssinatura = (LinkButton)e.Item.FindControl("lbCancelarAssinatura");
                LinkButton lbUpgradeAssinatura = (LinkButton)e.Item.FindControl("lbUpgradeAssinatura");

                _assinaturaPmoTO = (TOAssinaturaPMOBll)e.Item.DataItem;

                string _valor = (_assinaturaPmoTO.Amount != null ? 
                    Funcoes.Funcoes.ConvertePara.String(_assinaturaPmoTO.Amount).Insert(
                        Funcoes.Funcoes.ConvertePara.String(_assinaturaPmoTO.Amount).Length - 2, ",") : "");

                int _idStatus = Funcoes.Funcoes.CarregarEnumValor<
                    DominiosBll.AcessosPlanosAuxSituacaoIngles>(lblStatusReg.Text);
                lblStatusReg.Text = Funcoes.Funcoes.CarregarEnumNome<DominiosBll.AcessosPlanosAuxSituacao>(
                    _idStatus);

                lblValorReg.Text = string.Format("{0:c}", _valor);
                lblInicioReg.Text = string.Format("{0:d}", _assinaturaPmoTO.CurrentPeriodStart);
                lblFimReg.Text = string.Format("{0:d}", _assinaturaPmoTO.CurrentPeriodEnd);

                bool _status = acessosVigenciaPlanosBLL.MostraBotaoUpgradeAssinatura(
                        Funcoes.Funcoes.CarregarEnumItem<DominiosBll.AcessosPlanosAuxSituacaoIngles>(
                            _assinaturaPmoTO.Status));

                lbCancelarAssinatura.Visible = _status;
                lbUpgradeAssinatura.Visible = _status;
            }
        }

        protected void rptAssinaturas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);

            switch (e.CommandName)
            {
                case "cancelar":
                    {
                        lblTituloModal.Text = "Cancelando Assinatura Atual!";

                        CancelarAssinatura(_id);

                        break;
                    }
                case "upgrade":
                    {
                        lblTituloModal.Text = "Atualizando Assinatura Atual!";
                        PopulaCartaoesAssinatura(_idPessoa);

                        popUpModalEscolhaPlanoRenovacao.Show();

                        break;
                    }
            }
        }

        private void CancelarAssinatura(int id)
        {
            acessosVigenciaPlanoDCL = acessosVigenciaPlanosBLL.Carregar(id);

            bllRetorno _retCancelar = _assinaturaPmoBll.Cancelar(
                acessosVigenciaPlanoDCL.IdSubscriptionPagarMe);
            string _status = (_retCancelar.objeto != null && _retCancelar.objeto.Count > 0 ?
                _retCancelar.objeto[0].ToString() : "");

            if (_retCancelar.retorno)
            {
                acessosVigenciaPlanoDCL.StatusPagarMe = _status;
                acessosVigenciaPlanoDCL.DtFinal = DateTime.Today;

                acessosVigenciaPlanoDCL.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                acessosVigenciaPlanoDCL.Ativo = true;
                acessosVigenciaPlanoDCL.DataCadastro = DateTime.Now;
                acessosVigenciaPlanoDCL.IP = Request.UserHostAddress;

                bllRetorno retVigencia = acessosVigenciaPlanosBLL.Alterar(acessosVigenciaPlanoDCL);

                if (retVigencia.retorno)
                {
                    situacaoDcl = new AcessosVigenciaSituacao();
                    situacaoDcl.IdVigencia = acessosVigenciaPlanoDCL.IdVigencia;
                    situacaoDcl.IdSituacao = (int)DominiosBll.AcessosPlanosAuxSituacaoIngles.Canceled;
                    situacaoDcl.DataSituacao = DateTime.Today;

                    situacaoDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                    situacaoDcl.Ativo = true;
                    situacaoDcl.DataCadastro = DateTime.Now;
                    situacaoDcl.IP = Request.UserHostAddress;

                    bllRetorno retSituacao = situacaoBll.Inserir(situacaoDcl);

                    if (retSituacao.retorno)
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Success,
                            "O Cancelamento de sua Assinatura foi efetuado com Sucesso!!!",
                            "NutroVET informa - Cancelamento",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);

                        Thread.Sleep(6000);
                        Response.Redirect("~/MenuGeral.aspx");
                    }
                    else
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Error,
                            retSituacao.mensagem, "NutroVET informa - Situação",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Error,
                        retVigencia.mensagem, "NutroVET informa - Vigência",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    _retCancelar.mensagem, "NutroVET informa - Cancelamento",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private bllRetorno ValidaCampos(int _op)
        {
            Regex rgEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            Regex rgCartao = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");
            Regex rgCartao2 = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");
            Regex rgCpf = new Regex(@"(^\d{3}\x2E\d{3}\x2E\d{3}\x2D\d{2}$)");
            Regex rgCnpj = new Regex(@"(^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$)");
            Regex rgValidadeCartao = new Regex(@"(0[1-9]|10|11|12)[0-9]{2}$");
            Regex rgTelefone = new Regex(@"^(\([0-9]{2}\))\s([9]{1})?([0-9]{4})-([0-9]{4})$");
            Regex rgCep = new Regex(@"^\d{5}-\d{3}$");

            int _tpPessoa = Funcoes.Funcoes.ConvertePara.Int(ddlTipoPessoaAssinantePerfil.SelectedValue);

            if ((_tpPessoa == 1) && (meCnpjCpfAssinantePerfil.Text != "") &&
                (ddlNacionalidadeAssinantePerfil.SelectedValue == "1"))
            {
                meCnpjCpfAssinantePerfil.Text = FormataCPF(meCnpjCpfAssinantePerfil.Text);
            }

            if ((_tpPessoa == 2) && (meCnpjCpfAssinantePerfil.Text != "") &&
                (ddlNacionalidadeAssinantePerfil.SelectedValue == "2"))
            {
                meCnpjCpfAssinantePerfil.Text = FormataCNPJ(meCnpjCpfAssinantePerfil.Text);
            }

            if ((tbxCelularAssinantePerfil.Text != "") && (tbxCelularAssinantePerfil.Text.Length >= 10))
            {
                tbxCelularAssinantePerfil.Text = FormataCelular(tbxCelularAssinantePerfil.Text);
            }

            if ((meCEPAssinantePerfil.Text != "") && (meCEPAssinantePerfil.Text.Length >= 8) &&
                (ddlNacionalidadeAssinantePerfil.SelectedValue == "1"))
            {
                meCEPAssinantePerfil.Text = FormataCEP(meCEPAssinantePerfil.Text);
            }

            if (_tpPessoa <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TIPO DE PESSOA, na aba Dados, deve ser selecionado!");
            }
            else if ((_tpPessoa == 1) && (tbxNomeAssinantePerfil.Text == ""))
            {
                tbxNomeAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NOME DO ASSINANTE, na aba Dados, deve ser preenchido!");
            }
            else if ((_tpPessoa == 2) && (tbxNomeAssinantePerfil.Text == ""))
            {
                tbxNomeAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo RAZÃO SOCIAL DO ASSIANTE, na aba Dados, deve ser preenchido!");
            }
            else if ((_tpPessoa == 1) && (meCnpjCpfAssinantePerfil.Text == "") &&
                ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
            {
                meCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CPF, na aba Dados, deve ser preenchido!");
            }
            else if ((_tpPessoa == 2) && (meCnpjCpfAssinantePerfil.Text == "") &&
                ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
            {
                meCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CNPJ, na aba Dados, deve ser preenchido!");
            }
            else if ((_tpPessoa == 1) && (!rgCpf.IsMatch(meCnpjCpfAssinantePerfil.Text)) &&
                ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
            {
                meCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "Formato de CPF Inválido!");
            }
            else if ((_tpPessoa == 2) && (!rgCnpj.IsMatch(meCnpjCpfAssinantePerfil.Text)) &&
                ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
            {
                meCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "Formato de CNPJ Inválido!");
            }
            else if ((_tpPessoa == 1) && (!Funcoes.Funcoes.Validacoes.Cpf(meCnpjCpfAssinantePerfil.Text)) &&
                ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
            {
                meCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "CPF Inválido!");
            }
            else if ((_tpPessoa == 2) && (!Funcoes.Funcoes.Validacoes.Cnpj(meCnpjCpfAssinantePerfil.Text)) &&
                ddlNacionalidadeAssinantePerfil.SelectedIndex == 0)
            {
                meCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "CNPJ Inválido!");
            }
            else if (((_tpPessoa == 1) || (_tpPessoa == 2)) && (tbxPassaporteAssinante.Text == "") &&
                ddlNacionalidadeAssinantePerfil.SelectedIndex == 1)
            {
                tbxPassaporteAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo PASSAPORTE, na aba Dados, deve ser preenchido!");
            }
            else if ((_tpPessoa == 1) && (txbDataNascimentoAssinantePerfil.Text == ""))
            {
                txbDataNascimentoAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo DATA DE NASCIMENTO, na aba Dados, deve ser preenchido!");
            }
            else if ((_tpPessoa == 1) && (!MaiorDe18(txbDataNascimentoAssinantePerfil.Text)))
            {
                txbDataNascimentoAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "Usuário deve ser MAIOR de 18 anos!");
            }
            else if (tbxEmailAssinantePerfil.Text == "")
            {
                tbxEmailAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo E-MAIL, na aba Dados, deve ser preenchido!");
            }
            else if (!rgEmail.IsMatch(tbxEmailAssinantePerfil.Text))
            {
                tbxEmailAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "Formato do E-MAIL Inválido!");
            }
            else if (tbxCelularAssinantePerfil.Text == "")
            {
                tbxCelularAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TELEFONE CELULAR DO ASSINANTE, na aba Dados, deve ser preenchido!");
            }
            else if (meCEPAssinantePerfil.Text == "")
            {
                meCEPAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CEP, na aba Dados, deve ser preenchido!");
            }
            else if ((ddlNacionalidadeAssinantePerfil.SelectedValue == "1") &&
                     (!rgCep.IsMatch(meCEPAssinantePerfil.Text)))
            {
                meCEPAssinantePerfil.Focus();

                return 
                    bllRetorno.GeraRetorno(false, "Formato do CEP Inválido!");
            }
            else if (tbxLogradouroAssinantePerfil.Text == "")
            {
                tbxLogradouroAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "Campo LOGRADOURO, na aba Dados, deve ser preenchido!");
            }
            else if (tbxNumeroLogradouroAssinantePerfil.Text == "")
            {
                tbxNumeroLogradouroAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NÚMERO DO LOGRADOURO, na aba Dados, deve ser preenchido!");
            }
            else if (tbxBairroAssinantePerfil.Text == "")
            {
                tbxBairroAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo BAIRRO, na aba Dados, deve ser preenchido!");
            }
            else if (tbxCidadeAssinantePerfil.Text == "")
            {
                tbxCidadeAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo MUNICÍPIO, na aba Dados, deve ser preenchido!");
            }
            else if (tbxEstadoAssinantePerfil.Text == "")
            {
                tbxEstadoAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ESTADO, na aba Dados, deve ser preenchido!");
            }
            else if (rptListagemCartoesCredito.Items.Count <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Pelo menos UM Cartão de Crédito deve ser Cadastrado para Associar à sua Assinatura!!!");
            }
            else if (lbValorCodigoAssinaturaAssinantePerfil.Text == "" && _op == 1)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Você não pode vincular o cartão de crédito, pois não tem uma assinatura vigente. Utilize a opção Renovar Assinatura!");
            }
            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private string FormataCPF(string _nrCpf)
        {
            string _cpf = Funcoes.Funcoes.Mascaras.CPF(Funcoes.Funcoes.TiraCaracteresInvalidos(_nrCpf));

            return _cpf;
        }

        private string FormataCNPJ(string _nrCnpj)
        {
            string _cnpj = Funcoes.Funcoes.Mascaras.Cnpj(Funcoes.Funcoes.TiraCaracteresInvalidos(_nrCnpj));

            return _cnpj;
        }

        private string FormataCEP(string _nrCep)
        {
            string _cep = Funcoes.Funcoes.Mascaras.Cep(Funcoes.Funcoes.TiraCaracteresInvalidos(_nrCep));

            return _cep;
        }

        private string FormataCelular(string _nrCelular)
        {
            string _celular = Funcoes.Funcoes.Mascaras.Celular(Funcoes.Funcoes.TiraCaracteresInvalidos(_nrCelular));

            return _celular;
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

                var originalNumber = phoneUtil.Format(phoneNumber, PhoneNumberFormat.E164); // Produces "+923336323997"    
                return true;
            }
            catch (NumberParseException ex)
            {

                string errorMessage = "NumberParseException was thrown: " + ex.Message.ToString();

                return false;
            }
        }

        protected void lbSalvarCartaoCredito_Click(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ConvertePara.Int(ViewState["IdCartao"]) > 0)
            {
                AlterarCartaoCredito(Funcoes.Funcoes.ConvertePara.Int(ViewState["IdCartao"]));
            }
            else
            {
                InserirCartaoCredito();
            }
        }

        protected void lbInserirCartao_Click(object sender, EventArgs e)
        {
            LimpaCamposCartaoCredito();
            lblTituloModal.Text = "Inserindo novo Cartão de Crédito";
            popUpModal.Show();
        }

        protected void rblBasico_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblIntermediario.SelectedIndex = -1;
            rblCompleto.SelectedIndex = -1;

            popUpModalEscolhaPlanoRenovacao.Show();
        }

        protected void rblIntermediario_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblBasico.SelectedIndex = -1;
            rblCompleto.SelectedIndex = -1;

            popUpModalEscolhaPlanoRenovacao.Show();
        }

        protected void rblCompleto_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblIntermediario.SelectedIndex = -1;
            rblBasico.SelectedIndex = -1;

            popUpModalEscolhaPlanoRenovacao.Show();
        }

        private void PopulaPlanoBasico()
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Básico;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Plano_Principal;

            rblBasico.SelectedIndex = -1;
            rblBasico.Items.Clear();
            rblBasico.DataValueField = "IdPlano";
            rblBasico.DataTextField = "ValorDescricao";
            rblBasico.DataSource = planosAssinaturasBll.ListarPlanos(_codPlano, _tpPlano);
            rblBasico.DataBind();
        }

        private void PopulaPlanoIntermediario()
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Intermediário;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Plano_Principal;

            rblIntermediario.SelectedIndex = -1;
            rblIntermediario.Items.Clear();
            rblIntermediario.DataValueField = "IdPlano";
            rblIntermediario.DataTextField = "ValorDescricao";
            rblIntermediario.DataSource = planosAssinaturasBll.ListarPlanos(_codPlano, _tpPlano);
            rblIntermediario.DataBind();
        }

        private void PopulaPlanoCompleto()
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Completo;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Plano_Principal;

            rblCompleto.SelectedIndex = -1;
            rblCompleto.Items.Clear();
            rblCompleto.DataValueField = "IdPlano";
            rblCompleto.DataTextField = "ValorDescricao";
            rblCompleto.DataSource = planosAssinaturasBll.ListarPlanos(_codPlano, _tpPlano);
            rblCompleto.DataBind();
        }

        protected void lbSalvarRenovarAssinatura_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);

            acessosVigenciaPlanoTO = acessosVigenciaPlanosBLL.CarregarPlano(_idPessoa);

            int _idAssinaturaPagarme = Funcoes.Funcoes.ConvertePara.Int(
                acessosVigenciaPlanoTO.IdSubscriptionPagarMe);

            bool _status = acessosVigenciaPlanosBLL.MostraBotaoUpgradeAssinatura(
                    Funcoes.Funcoes.CarregarEnumItem<DominiosBll.AcessosPlanosAuxSituacaoIngles>(
                        acessosVigenciaPlanoTO.StatusPagarMe));

            if ((_idAssinaturaPagarme > 0) && (_status))
            {
                AlterarPlano(_idPessoa, _idAssinaturaPagarme);
            }
            else
            {
                Inserir();
            }
        }

        protected void Inserir()
        {
            Regex apenasDigitos = new Regex(@"[^\d]");
            int _idPlano = 0;
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);

            if (Funcoes.Funcoes.ConvertePara.Int(rblBasico.SelectedValue) > 0)
            {
                _idPlano = Funcoes.Funcoes.ConvertePara.Int(rblBasico.SelectedValue);
            }
            else if (Funcoes.Funcoes.ConvertePara.Int(rblIntermediario.SelectedValue) > 0)
            {
                _idPlano = Funcoes.Funcoes.ConvertePara.Int(rblIntermediario.SelectedValue);
            }
            else if (Funcoes.Funcoes.ConvertePara.Int(rblCompleto.SelectedValue) > 0)
            {
                _idPlano = Funcoes.Funcoes.ConvertePara.Int(rblCompleto.SelectedValue);
            }

            if (_idPlano > 0)
            {
                if (Funcoes.Funcoes.ConvertePara.Int(ddlCCRenovAssin.SelectedValue) > 0)
                {
                    if (!Funcoes.Funcoes.ConvertePara.Bool(ViewState["DadosSalvos"]))
                    {
                        AlterarPessoas(_idPessoa, false);
                    }

                    pessoaCartaoCreditoDcl = pessoaCartaoCreditoBll.Carregar(
                        Funcoes.Funcoes.ConvertePara.Int(ddlCCRenovAssin.SelectedValue));
                    planosAssinaturaDCL = planosAssinaturasBll.Carregar(_idPlano);
                    _pessoasTO = _pessoasBll.CarregarTO(_idPessoa,
                        (int)DominiosBll.PessoasAuxTipos.Cliente);

                    _assinaturaPmoTO = new TOAssinaturaPMOBll();
                    Regex apenasDigitosAmount = new Regex(@"[^\d]");

                    _assinaturaPmoTO.IdPlan = (Conexao.ServidorLocal() ?
                        Funcoes.Funcoes.ConvertePara.String(planosAssinaturaDCL.IdPlanoPagarMeTestes) :
                            Funcoes.Funcoes.ConvertePara.String(planosAssinaturaDCL.IdPlanoPagarMe));

                    string _amount = apenasDigitosAmount.Replace(
                        Funcoes.Funcoes.ConvertePara.String(planosAssinaturaDCL.ValorPlano), "");
                    _amount = _amount.Substring(0, _amount.Length - 2);

                    _assinaturaPmoTO.Amount = Funcoes.Funcoes.ConvertePara.Int(_amount);

                    _assinaturaPmoTO.CardNumber = pessoaCartaoCreditoDcl.NrCartao;
                    _assinaturaPmoTO.CardHolderName = pessoaCartaoCreditoDcl.NomeCartao;
                    _assinaturaPmoTO.CardExpirationDate = pessoaCartaoCreditoDcl.VencimCartao;
                    _assinaturaPmoTO.CardCvv = pessoaCartaoCreditoDcl.CodSeg;

                    _assinaturaPmoTO.NameCustomer = _pessoasTO.cNome;
                    _assinaturaPmoTO.IdCustomer = Funcoes.Funcoes.ConvertePara.String(_idPessoa);
                    _assinaturaPmoTO.TypeCustomer = _pessoasTO.cdTpEntidade == 1 ?
                        (int)CustomerType.Individual : (int)CustomerType.Corporation;
                    _assinaturaPmoTO.DocumentNumberCustomer = _pessoasTO.cdTpEntidade == 1 ?
                        _pessoasTO.cCPF : _pessoasTO.cCNPJ;
                    _assinaturaPmoTO.EmailCustomer = _pessoasTO.cEMail;

                    _assinaturaPmoTO.ZipCodeCustomer = _pessoasTO.cLogr_CEP;
                    _assinaturaPmoTO.StreetCustomer = _pessoasTO.cLogradouro;
                    _assinaturaPmoTO.StreetNumberCustomer = _pessoasTO.cLogr_Nr;
                    _assinaturaPmoTO.ComplementaryCustomer = _pessoasTO.cLogr_Compl;
                    _assinaturaPmoTO.NeighborhoodCustomer = _pessoasTO.cLogr_Bairro;
                    _assinaturaPmoTO.CityCustomer = _pessoasTO.cLogr_Cidade;
                    _assinaturaPmoTO.StateCustomer = _pessoasTO.cLogr_UF;
                    _assinaturaPmoTO.ContryCustomer = _pessoasTO.cLogr_Pais;

                    _assinaturaPmoTO.NationalityCustomer = _pessoasTO.cdNacionalidade == 2 ?
                        (int)DocumentType.Passport : _pessoasTO.cdTpEntidade == 1 ?
                            (int)DocumentType.Cpf : (int)DocumentType.Cnpj;
                    _assinaturaPmoTO.DocumentNumberCustomer = (_pessoasTO.cdNacionalidade == 2 ?
                        _pessoasTO.cDocumentosOutros : (_pessoasTO.cdTpEntidade == 1 ? _pessoasTO.cCPF :
                            _pessoasTO.cCNPJ));

                    _assinaturaPmoTO.DddCustomer = _pessoasTO.cCelular.Substring(1, 2);
                    _assinaturaPmoTO.PhoneCustomer = apenasDigitos.Replace(_pessoasTO.cCelular.Substring(5,
                        (_pessoasTO.cCelular.Length - 5)), "");

                    bllRetorno _retRenovarAssinatura = _assinaturaPmoBll.Inserir(_assinaturaPmoTO);

                    if (_retRenovarAssinatura.retorno)
                    {
                        TOAssinaturaPMOBll _subscriptionPagarMe = null;
                        bllRetorno _retInsereNoBanco;

                        if (_retRenovarAssinatura.objeto.Count > 0)
                        {
                            foreach (TOAssinaturaPMOBll item in _retRenovarAssinatura.objeto)
                            {
                                _subscriptionPagarMe = item;
                            }
                        }

                        if (_subscriptionPagarMe != null)
                        {
                            #region VIGÊNCIA PLANOS
                            acessosVigenciaPlanoDCL = new AcessosVigenciaPlano();
                            acessosVigenciaPlanoDCL.IdPessoa = _idPessoa;
                            acessosVigenciaPlanoDCL.IdPlano = _idPlano;
                            acessosVigenciaPlanoDCL.IdSubscriptionPagarMe = _subscriptionPagarMe.Id;
                            acessosVigenciaPlanoDCL.IdCartao = pessoaCartaoCreditoDcl.IdCartao;
                            acessosVigenciaPlanoDCL.StatusPagarMe = _subscriptionPagarMe.Status;
                            acessosVigenciaPlanoDCL.DtInicial = DateTime.Today;
                            acessosVigenciaPlanoDCL.DtFinal = (_subscriptionPagarMe.CurrentPeriodEnd != null ?
                                _subscriptionPagarMe.CurrentPeriodEnd.Value : DateTime.Today.AddYears(1));

                            acessosVigenciaPlanoDCL.Ativo = true;
                            acessosVigenciaPlanoDCL.IdOperador = 1;
                            acessosVigenciaPlanoDCL.DataCadastro = DateTime.Now;
                            acessosVigenciaPlanoDCL.IP = Request.UserHostAddress;
                            #endregion

                            #region VIGÊNCIA SITUAÇÃO

                            //Tipo 1
                            vigenciaSituacaoTp1Dcl = new AcessosVigenciaSituacao();
                            vigenciaSituacaoTp1Dcl.IdSituacao =
                                (int)DominiosBll.AcessosPlanosAuxSituacao.Julgamento;
                            vigenciaSituacaoTp1Dcl.DataSituacao = DateTime.Today;

                            vigenciaSituacaoTp1Dcl.Ativo = true;
                            vigenciaSituacaoTp1Dcl.IdOperador = 1;
                            vigenciaSituacaoTp1Dcl.DataCadastro = DateTime.Now;
                            vigenciaSituacaoTp1Dcl.IP = Request.UserHostAddress;

                            //Tipo 2
                            vigenciaSituacaoTp2Dcl = new AcessosVigenciaSituacao();
                            vigenciaSituacaoTp2Dcl.IdSituacao =
                                (int)DominiosBll.AcessosPlanosAuxSituacao.Pago;
                            vigenciaSituacaoTp2Dcl.DataSituacao = DateTime.Today;

                            vigenciaSituacaoTp2Dcl.Ativo = true;
                            vigenciaSituacaoTp2Dcl.IdOperador = 1;
                            vigenciaSituacaoTp2Dcl.DataCadastro = DateTime.Now;
                            vigenciaSituacaoTp2Dcl.IP = Request.UserHostAddress;

                            //Tipo 3
                            vigenciaSituacaoTp3Dcl = new AcessosVigenciaSituacao();
                            vigenciaSituacaoTp3Dcl.IdSituacao =
                                (int)DominiosBll.AcessosPlanosAuxSituacao.Permitido;
                            vigenciaSituacaoTp3Dcl.DataSituacao = DateTime.Today;

                            vigenciaSituacaoTp3Dcl.Ativo = true;
                            vigenciaSituacaoTp3Dcl.IdOperador = 1;
                            vigenciaSituacaoTp3Dcl.DataCadastro = DateTime.Now;
                            vigenciaSituacaoTp3Dcl.IP = Request.UserHostAddress;
                            #endregion

                            #region Coloca tudo no Banco de Dados
                            acessosVigenciaPlanoDCL.AcessosVigenciaSituacaos.Add(vigenciaSituacaoTp1Dcl);
                            acessosVigenciaPlanoDCL.AcessosVigenciaSituacaos.Add(vigenciaSituacaoTp2Dcl);
                            acessosVigenciaPlanoDCL.AcessosVigenciaSituacaos.Add(vigenciaSituacaoTp3Dcl);

                            //Insere tudo no Banco de Dados
                            _retInsereNoBanco = acessosVigenciaPlanosBLL.Inserir(acessosVigenciaPlanoDCL);
                            #endregion

                            if (_retInsereNoBanco.retorno)
                            {
                                _assinaturaPmoBll.CacheExcluir();

                                Thread.Sleep(6000);
                                Response.Redirect("~/MenuGeral.aspx");
                            }
                            else
                            {
                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Error,
                                    "Erro ao Gravar a Assinatura no Banco de Dados!!! Contate o Admonistrador!!!",
                                    "NutroVET informa - Renovação da Assinatura",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);
                            }

                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                "Erro ao Gravar a Assinatura no Banco de Dados!!! Contate o Admonistrador!!!",
                                "NutroVET informa - Renovação da Assinatura",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }
                    }
                    else
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Error, _retRenovarAssinatura.mensagem,
                            "NutroVET informa - Renovação da Assinatura",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);

                        popUpModal.Show();
                    }
                }
                else
                {
                    popUpModalEscolhaPlanoRenovacao.Show();

                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Um Cartão de Crédito deve ser Selecionado!!!",
                        "NutroVET informa - Renovação da Assinatura",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                popUpModalEscolhaPlanoRenovacao.Show();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Um Plano deve ser Selecionado!!!",
                    "NutroVET informa - Renovação da Assinatura",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void AlterarPlano(int _idPessoa, int _idAssinatura)
        {
            Regex apenasDigitos = new Regex(@"[^\d]");
            int _idPlano = 0;

            if (Funcoes.Funcoes.ConvertePara.Int(rblBasico.SelectedValue) > 0)
            {
                _idPlano = Funcoes.Funcoes.ConvertePara.Int(rblBasico.SelectedValue);
            }
            else if (Funcoes.Funcoes.ConvertePara.Int(rblIntermediario.SelectedValue) > 0)
            {
                _idPlano = Funcoes.Funcoes.ConvertePara.Int(rblIntermediario.SelectedValue);
            }
            else if (Funcoes.Funcoes.ConvertePara.Int(rblCompleto.SelectedValue) > 0)
            {
                _idPlano = Funcoes.Funcoes.ConvertePara.Int(rblCompleto.SelectedValue);
            }

            if (_idPlano > 0)
            {
                if (Funcoes.Funcoes.ConvertePara.Int(ddlCCRenovAssin.SelectedValue) > 0)
                {
                    if (!Funcoes.Funcoes.ConvertePara.Bool(ViewState["DadosSalvos"]))
                    {
                        AlterarPessoas(_idPessoa, false);
                    }

                    pessoaCartaoCreditoDcl = pessoaCartaoCreditoBll.Carregar(
                        Funcoes.Funcoes.ConvertePara.Int(ddlCCRenovAssin.SelectedValue));
                    planosAssinaturaDCL = planosAssinaturasBll.Carregar(_idPlano);
                    _pessoasTO = _pessoasBll.CarregarTO(_idPessoa,
                        (int)DominiosBll.PessoasAuxTipos.Cliente);

                    _assinaturaPmoTO = new TOAssinaturaPMOBll();
                    Regex apenasDigitosAmount = new Regex(@"[^\d]");

                    _assinaturaPmoTO.IdPlan = (Conexao.ServidorLocal() ?
                        Funcoes.Funcoes.ConvertePara.String(planosAssinaturaDCL.IdPlanoPagarMeTestes) :
                            Funcoes.Funcoes.ConvertePara.String(planosAssinaturaDCL.IdPlanoPagarMe));

                    string _amount = apenasDigitosAmount.Replace(
                        Funcoes.Funcoes.ConvertePara.String(planosAssinaturaDCL.ValorPlano), "");
                    _amount = _amount.Substring(0, _amount.Length - 2);

                    _assinaturaPmoTO.Amount = Funcoes.Funcoes.ConvertePara.Int(_amount);

                    _assinaturaPmoTO.CardNumber = pessoaCartaoCreditoDcl.NrCartao;
                    _assinaturaPmoTO.CardHolderName = pessoaCartaoCreditoDcl.NomeCartao;
                    _assinaturaPmoTO.CardExpirationDate = pessoaCartaoCreditoDcl.VencimCartao;
                    _assinaturaPmoTO.CardCvv = pessoaCartaoCreditoDcl.CodSeg;

                    _assinaturaPmoTO.NameCustomer = _pessoasTO.cNome;
                    _assinaturaPmoTO.IdCustomer = Funcoes.Funcoes.ConvertePara.String(_idPessoa);
                    _assinaturaPmoTO.TypeCustomer = _pessoasTO.cdTpEntidade == 1 ?
                        (int)CustomerType.Individual : (int)CustomerType.Corporation;
                    _assinaturaPmoTO.DocumentNumberCustomer = _pessoasTO.cdTpEntidade == 1 ?
                        _pessoasTO.cCPF : _pessoasTO.cCNPJ;
                    _assinaturaPmoTO.EmailCustomer = _pessoasTO.cEMail;

                    _assinaturaPmoTO.ZipCodeCustomer = _pessoasTO.cLogr_CEP;
                    _assinaturaPmoTO.StreetCustomer = _pessoasTO.cLogradouro;
                    _assinaturaPmoTO.StreetNumberCustomer = _pessoasTO.cLogr_Nr;
                    _assinaturaPmoTO.ComplementaryCustomer = _pessoasTO.cLogr_Compl;
                    _assinaturaPmoTO.NeighborhoodCustomer = _pessoasTO.cLogr_Bairro;
                    _assinaturaPmoTO.CityCustomer = _pessoasTO.cLogr_Cidade;
                    _assinaturaPmoTO.StateCustomer = _pessoasTO.cLogr_UF;
                    _assinaturaPmoTO.ContryCustomer = _pessoasTO.cLogr_Pais;

                    _assinaturaPmoTO.NationalityCustomer = _pessoasTO.cdNacionalidade == 2 ?
                        (int)DocumentType.Passport : _pessoasTO.cdTpEntidade == 1 ?
                            (int)DocumentType.Cpf : (int)DocumentType.Cnpj;
                    _assinaturaPmoTO.DocumentNumberCustomer = (_pessoasTO.cdNacionalidade == 2 ?
                        _pessoasTO.cDocumentosOutros : (_pessoasTO.cdTpEntidade == 1 ? _pessoasTO.cCPF :
                            _pessoasTO.cCNPJ));

                    _assinaturaPmoTO.DddCustomer = _pessoasTO.cCelular.Substring(1, 2);
                    _assinaturaPmoTO.PhoneCustomer = apenasDigitos.Replace(_pessoasTO.cCelular.Substring(5,
                        (_pessoasTO.cCelular.Length - 5)), "");

                    
                    bllRetorno _retRenovarAssinatura = _assinaturaPmoBll.Alterar(
                           Funcoes.Funcoes.ConvertePara.String(_idAssinatura), _assinaturaPmoTO);

                    if (_retRenovarAssinatura.retorno)
                    {
                        TOAssinaturaPMOBll _subscriptionPagarMe = null;
                        bllRetorno _retAlteraNoBanco;

                        if (_retRenovarAssinatura.objeto.Count > 0)
                        {
                            foreach (TOAssinaturaPMOBll item in _retRenovarAssinatura.objeto)
                            {
                                _subscriptionPagarMe = item;
                            }
                        }

                        if (_subscriptionPagarMe != null)
                        {
                            #region VIGÊNCIA PLANOS

  
                            acessosVigenciaPlanoDCL = acessosVigenciaPlanosBLL.Carregar(
                                Funcoes.Funcoes.ConvertePara.Int(ViewState["IdVigencia"]));

                            acessosVigenciaPlanoDCL.IdPlano =  _idPlano;
                            acessosVigenciaPlanoDCL.IdCartao = pessoaCartaoCreditoDcl.IdCartao;
                            acessosVigenciaPlanoDCL.StatusPagarMe = _subscriptionPagarMe.Status;
                            acessosVigenciaPlanoDCL.DtInicial = (
                                _subscriptionPagarMe.CurrentPeriodStart != null ?
                                    _subscriptionPagarMe.CurrentPeriodStart.Value : DateTime.Today);
                            acessosVigenciaPlanoDCL.DtFinal = (_subscriptionPagarMe.CurrentPeriodEnd != null ?
                                _subscriptionPagarMe.CurrentPeriodEnd.Value : DateTime.Today.AddYears(1));

                            acessosVigenciaPlanoDCL.Ativo = true;
                            acessosVigenciaPlanoDCL.IdOperador = 1;
                            acessosVigenciaPlanoDCL.DataCadastro = DateTime.Now;
                            acessosVigenciaPlanoDCL.IP = Request.UserHostAddress;
                            #endregion

                            _retAlteraNoBanco = acessosVigenciaPlanosBLL.Alterar(acessosVigenciaPlanoDCL);

                            if (_retAlteraNoBanco.retorno)
                            {
                                _assinaturaPmoBll.CacheExcluir();
                                //PopulaAbaPlanos(_idPessoa);
                                Thread.Sleep(6000);
                                Response.Redirect("~/MenuGeral.aspx");

                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Success,
                                    "Plano Renovado com Sucesso! Parabéns!",
                                    "NutroVET informa - Renovação da Assinatura",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);
                            }
                            else
                            {
                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Error,
                                    "Erro ao Alterar a Assinatura no Banco de Dados!!! Contate o Admnistrador!!!",
                                    "NutroVET informa - Renovação da Assinatura",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);
                            }

                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                "Erro ao Alterar a Assinatura no Banco de Dados!!! Contate o Admonistrador!!!",
                                "NutroVET informa - Renovação da Assinatura",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }
                    }
                    else
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Error, _retRenovarAssinatura.mensagem,
                            "NutroVET informa - Renovação da Assinatura",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
                else
                {
                    popUpModalEscolhaPlanoRenovacao.Show();

                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Um Cartão de Crédito deve ser Selecionado!!!",
                        "NutroVET informa - Renovação da Assinatura",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                popUpModalEscolhaPlanoRenovacao.Show();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Um Plano deve ser Selecionado!!!",
                    "NutroVET informa - Renovação da Assinatura",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }
        
        protected void VincularCartaoCredito(int _idCartao)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            bllRetorno _validaTela = ValidaCampos(1);
            if (_validaTela.retorno)
            {
                Regex apenasDigitos = new Regex(@"[^\d]");
                bllRetorno _retAlterarCC = new bllRetorno();
                acessosVigenciaPlanoTO = acessosVigenciaPlanosBLL.CarregarTO(
                    Funcoes.Funcoes.ConvertePara.Int(ViewState["IdVigencia"]));

                acessosVigenciaPlanoTO = acessosVigenciaPlanosBLL.CarregarPlano(_idPessoa);

                int _idAssinatura = Funcoes.Funcoes.ConvertePara.Int(acessosVigenciaPlanoTO.IdSubscriptionPagarMe);

                if ((acessosVigenciaPlanoTO != null) && (acessosVigenciaPlanoTO.IdVigencia > 0))
                {
                    if (_idCartao > 0)
                    {
                        if (!Funcoes.Funcoes.ConvertePara.Bool(ViewState["DadosSalvos"]))
                        {
                            AlterarPessoas(_idPessoa, false);
                        }

                        pessoaCartaoCreditoDcl = pessoaCartaoCreditoBll.Carregar(
                            Funcoes.Funcoes.ConvertePara.Int(_idCartao));

                        if (pessoaCartaoCreditoDcl.IdCartao != acessosVigenciaPlanoTO.IdCartao)
                        {
                            planosAssinaturaDCL = planosAssinaturasBll.Carregar(acessosVigenciaPlanoTO.IdPlano);
                            _pessoasTO = _pessoasBll.CarregarTO(_idPessoa,
                                (int)DominiosBll.PessoasAuxTipos.Cliente);

                            _assinaturaPmoTO = new TOAssinaturaPMOBll();
                            Regex apenasDigitosAmount = new Regex(@"[^\d]");

                            _assinaturaPmoTO.IdPlan = (Conexao.ServidorLocal() ?
                                Funcoes.Funcoes.ConvertePara.String(planosAssinaturaDCL.IdPlanoPagarMeTestes) :
                                    Funcoes.Funcoes.ConvertePara.String(planosAssinaturaDCL.IdPlanoPagarMe));

                            string _amount = apenasDigitosAmount.Replace(
                                Funcoes.Funcoes.ConvertePara.String(planosAssinaturaDCL.ValorPlano), "");
                            _amount = _amount.Substring(0, _amount.Length - 2);

                            _assinaturaPmoTO.Amount = Funcoes.Funcoes.ConvertePara.Int(_amount);

                            _assinaturaPmoTO.CardNumber = pessoaCartaoCreditoDcl.NrCartao;
                            _assinaturaPmoTO.CardHolderName = pessoaCartaoCreditoDcl.NomeCartao;
                            _assinaturaPmoTO.CardExpirationDate = pessoaCartaoCreditoDcl.VencimCartao;
                            _assinaturaPmoTO.CardCvv = pessoaCartaoCreditoDcl.CodSeg;

                            _assinaturaPmoTO.NameCustomer = _pessoasTO.cNome;
                            _assinaturaPmoTO.IdCustomer = Funcoes.Funcoes.ConvertePara.String(_idPessoa);
                            _assinaturaPmoTO.TypeCustomer = _pessoasTO.cdTpEntidade == 1 ?
                                (int)CustomerType.Individual : (int)CustomerType.Corporation;
                            _assinaturaPmoTO.DocumentNumberCustomer = _pessoasTO.cdTpEntidade == 1 ?
                                _pessoasTO.cCPF : _pessoasTO.cCNPJ;
                            _assinaturaPmoTO.EmailCustomer = _pessoasTO.cEMail;

                            _assinaturaPmoTO.ZipCodeCustomer = _pessoasTO.cLogr_CEP;
                            _assinaturaPmoTO.StreetCustomer = _pessoasTO.cLogradouro;
                            _assinaturaPmoTO.StreetNumberCustomer = _pessoasTO.cLogr_Nr;
                            _assinaturaPmoTO.ComplementaryCustomer = _pessoasTO.cLogr_Compl;
                            _assinaturaPmoTO.NeighborhoodCustomer = _pessoasTO.cLogr_Bairro;
                            _assinaturaPmoTO.CityCustomer = _pessoasTO.cLogr_Cidade;
                            _assinaturaPmoTO.StateCustomer = _pessoasTO.cLogr_UF;
                            _assinaturaPmoTO.ContryCustomer = _pessoasTO.cLogr_Pais;

                            _assinaturaPmoTO.NationalityCustomer = _pessoasTO.cdNacionalidade == 2 ?
                                (int)DocumentType.Passport : _pessoasTO.cdTpEntidade == 1 ?
                                    (int)DocumentType.Cpf : (int)DocumentType.Cnpj;
                            _assinaturaPmoTO.DocumentNumberCustomer = (_pessoasTO.cdNacionalidade == 2 ?
                                _pessoasTO.cDocumentosOutros : (_pessoasTO.cdTpEntidade == 1 ? _pessoasTO.cCPF :
                                    _pessoasTO.cCNPJ));

                            _assinaturaPmoTO.PhoneCustomer = apenasDigitos.Replace(_pessoasTO.cCelular.Substring(5,
                                (_pessoasTO.cCelular.Length - 5)), "");
                            _assinaturaPmoTO.DddCustomer = _pessoasTO.cCelular != "" ? _pessoasTO.cCelular.Substring(1, 2) : "";

                            bllRetorno _retRenovarAssinatura = _assinaturaPmoBll.Alterar(
                                   Funcoes.Funcoes.ConvertePara.String(_idAssinatura), _assinaturaPmoTO);

                            if (_retRenovarAssinatura.retorno)
                            {
                                TOAssinaturaPMOBll _subscriptionPagarMe = null;
                                bllRetorno _retAlteraNoBanco;

                                if (_retRenovarAssinatura.objeto.Count > 0)
                                {
                                    foreach (TOAssinaturaPMOBll item in _retRenovarAssinatura.objeto)
                                    {
                                        _subscriptionPagarMe = item;
                                    }
                                }

                                if (_subscriptionPagarMe != null)
                                {
                                    #region VIGÊNCIA PLANOS


                                    acessosVigenciaPlanoDCL = acessosVigenciaPlanosBLL.Carregar(
                                        Funcoes.Funcoes.ConvertePara.Int(ViewState["IdVigencia"]));

                                    //acessosVigenciaPlanoDCL.IdPlano = _idPlano;
                                    acessosVigenciaPlanoDCL.IdCartao = pessoaCartaoCreditoDcl.IdCartao;
                                    acessosVigenciaPlanoDCL.StatusPagarMe = _subscriptionPagarMe.Status;
                                    acessosVigenciaPlanoDCL.DtInicial = (
                                        _subscriptionPagarMe.CurrentPeriodStart != null ?
                                            _subscriptionPagarMe.CurrentPeriodStart.Value : DateTime.Today);
                                    acessosVigenciaPlanoDCL.DtFinal = (_subscriptionPagarMe.CurrentPeriodEnd != null ?
                                        _subscriptionPagarMe.CurrentPeriodEnd.Value : DateTime.Today.AddYears(1));

                                    acessosVigenciaPlanoDCL.Ativo = true;
                                    acessosVigenciaPlanoDCL.IdOperador = 1;
                                    acessosVigenciaPlanoDCL.DataCadastro = DateTime.Now;
                                    acessosVigenciaPlanoDCL.IP = Request.UserHostAddress;
                                    #endregion

                                    _retAlteraNoBanco = acessosVigenciaPlanosBLL.Alterar(acessosVigenciaPlanoDCL);

                                    if (_retAlteraNoBanco.retorno)
                                    {
                                        _assinaturaPmoBll.CacheExcluir();

                                        Thread.Sleep(6000);
                                        Response.Redirect("~/MenuGeral.aspx");
                                        //populaTelaPerfil(_idPessoa);
                                        Funcoes.Funcoes.Toastr.ShowToast(this,
                                            Funcoes.Funcoes.Toastr.ToastType.Success,
                                            "Cartão de Crédito Associado à Assinatura",
                                            "NutroVET informa - Alteração de Cartão de Crédito",
                                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                            true);
                                    }
                                    else
                                    {
                                        Funcoes.Funcoes.Toastr.ShowToast(this,
                                            Funcoes.Funcoes.Toastr.ToastType.Error,
                                            "Erro ao Alterar o Cartão de Crédito no Banco de Dados!!! Contate o Admnistrador!!!",
                                            "NutroVET informa - Renovação da Assinatura",
                                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                            true);
                                    }

                                }
                                else
                                {
                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Error,
                                        "Erro ao Alterar o Cartão de Crédito no Banco de Dados!!! Contate o Admonistrador!!!",
                                        "NutroVET informa - Renovação da Assinatura",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                            }
                            else
                            {
                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Error, _retRenovarAssinatura.mensagem,
                                    "NutroVET informa - Alteração do Cartão de Crédito",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);
                            }
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Warning,
                                "Você NÃO Pode Vincular um Cartão já Associado a um Assinatura!!!",
                                "NutroVET informa - Vínculo",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }
                    }
                    else
                    {
                        popUpModalEscolhaPlanoRenovacao.Show();

                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Warning,
                            "Um Cartão de Crédito deve ser Selecionado!!!",
                            "NutroVET informa - Renovação da Assinatura",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    _validaTela.mensagem, "Atenção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }


            
        }
    }
}