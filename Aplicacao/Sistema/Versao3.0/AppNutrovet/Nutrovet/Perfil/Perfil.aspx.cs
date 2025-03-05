using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;
using DCL;
using BLL;
using System.Text.RegularExpressions;
using MaskEdit;
using MosaicoSolutions.ViaCep;
using MosaicoSolutions.ViaCep.Modelos;
using PagarMe;
using PhoneNumbers;
using System.Drawing;

//using Newtonsoft.Json;
//using System.Net;
//using System.Text;

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
        protected AcessosVigenciaSituacao vigenciaSituacaoDcl;
        protected clPlanosAssinaturasBll planosAssinaturasBll = new clPlanosAssinaturasBll();
        protected PlanosAssinatura planosAssinaturaDCL;
        protected TOPlanosBll planosAssinaturaTO;
        protected clAssinaturaPMOBll _assinaturaPmoBll = new clAssinaturaPMOBll();
        protected TOAssinaturaPMOBll _assinaturaPmoTO;
        protected TOPessoasBll _pessoasTO;
        protected clPessoasBll _pessoasBll = new clPessoasBll();
        protected Pessoa _pessoasDcl;
        protected clAcessosVigenciaCupomBll cupomBll = new clAcessosVigenciaCupomBll();
        protected AcessosVigenciaCupomDesconto cupomDcl = null;
        protected clConfigReceituarioBll configReceitBll = new clConfigReceituarioBll();
        protected ConfigReceituario configReceitDcl;

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
                    
                    PopulaUF();
                    populaTelaPerfil(_idPessoa);
                    PopulaPlanoBasico("");
                    PopulaPlanoIntermediario("");
                    PopulaPlanoCompleto("");
                    PopUpCamposObrigatorios(_idPessoa);
                    //TesteJSon();

                    imgFoto.ImageUrl = pessoaBll.CarregarImagem(_idPessoa);
                    lblFileUpload.Text = imgFoto.ImageUrl;
                }
            }
        }

        private void PopUpCamposObrigatorios(int idPessoa)
        {
            bool _camposCadastrados = pessoaBll.CamposPrincipaisCadastrados(idPessoa);
            bool _planoVigente = acessosVigenciaPlanosBLL.PlanoEstaNaVigencia(idPessoa);

            if ((_camposCadastrados == false) && (_planoVigente) &&
                (!Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
            {
                lblTxtCamposObrig.Text = 
                    "Olá, Tudo bem?</br>" +
                    "Não se preocupe, não houveram alterações na sua assinatura. " +
                    "Ela continua ativa!</br>" +
                    "Direcionamos você para a Tela de Perfil para complementar seus " +
                    "dados pessoais, permitindo que o Sistema mantenha o correto " +
                    "funcionamento e para que novas funcionalidades fiquem disponíveis.</br>" +
                    "Preencha os campos necessários e após, efetue o logon novamente.</br>" +
                    "Pedimos desculpas pelo transtorno, mas estamos trabalhando para melhorar " +
                    "cada vez mais sua experiência.</br></br>" +
                    "Nossos, agradecimentos.</br>Equipe de desenvolvimento do Nutrovet.";

                mdlCamposObrigatorios.Show();
            }
        }

        //private void TesteJSon()
        //{
        //    WebClient client = new WebClient();
        //    client.Encoding = Encoding.UTF8;
        //    /*string url = client.DownloadString("http://localhost/ProntuarioWS10/" +
        //        "SvAcessoLogon.svc/CarregarChave?_CpfCnpj=633.728.000-87");*/
        //    string url = client.DownloadString("http://localhost/ProntuarioWS10/" +
        //        "SvAcessoLogon.svc/ValidarLogon?_user=2280400&_senha=Br1tt0l0b");

        //    bllRetorno texto = JsonConvert.DeserializeObject<bllRetorno>(url);

        //    Funcoes.Funcoes.Toastr.ShowToast(this,
        //            Funcoes.Funcoes.Toastr.ToastType.Warning,
        //            texto.mensagem, "Atenção",
        //            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
        //            true);
        //}

        private void PopulaPais(object sender, int _nacional)
        {
            DropDownList ddlPaises = (DropDownList)sender;

            ddlPaises.DataTextField = "nome_pais";
            ddlPaises.DataValueField = "sigla";

            switch (_nacional)
            {

                case 0:
                    {
                        ddlPaises.DataSource = null;
                        ddlPaises.Items.Clear();

                        break;
                    }
                case 1:
                    {
                        ddlPaises.DataSource = paisesBll.ListarPaisNacional();

                        break;
                    }
                case 2:
                    {
                        ddlPaises.DataSource = paisesBll.ListarPaisesInternacionais();

                        break;
                    }
            }

            ddlPaises.DataBind();

            ddlPaises.Items.Insert(0, new ListItem("-- Selecione --", "0"));

            if (_nacional == 1)
            {
                ddlPaises.SelectedIndex = ddlPaises.Items.IndexOf(
                    ddlPaises.Items.FindByText("Brasil"));
            }
        }

        private void PopulaUF()
        {
            ddlUfCrmv.DataTextField = "Nome";
            ddlUfCrmv.DataValueField = "Sigla";
            ddlUfCrmv.DataSource = paisesBll.ListarUFNacional();
            ddlUfCrmv.DataBind();

            ddlUfCrmv.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void populaTelaPerfil(int _idPessoa)
        {
            pessoaDcl = pessoaBll.Carregar(_idPessoa);
            configReceitDcl = configReceitBll.Carregar(_idPessoa);

            //Tab Meus Dados
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlNacionalidadeAssinante,
                pessoaDcl.dNacionalidade);
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlTipoPessoaAssinante,
                pessoaDcl.dTpEntidade);

            Mascaras(ddlNacionalidadeAssinante.SelectedValue, 
                ddlTipoPessoaAssinante.SelectedValue);

            switch (Funcoes.Funcoes.ConvertePara.Int(pessoaDcl.dTpEntidade))
            {
                case 1:
                    {
                        tbxCnpjCpfAssinantePerfil.Text = pessoaDcl.CPF;

                        break;
                    }
                case 2:
                    {
                        tbxCnpjCpfAssinantePerfil.Text = pessoaDcl.CNPJ;

                        break;
                    }
                default:
                    {
                        tbxCnpjCpfAssinantePerfil.Text = "";
                        break;
                    }
            }

            tbxDocumentoPerfilAssinante.Text = pessoaDcl.Passaporte;
            tbxRGAssinantePerfil.Text = pessoaDcl.RG != null ? pessoaDcl.RG : "";
            txbDataNascimentoAssinantePerfil.Text = (pessoaDcl.DataNascimento != null ?
                pessoaDcl.DataNascimento.Value.ToString("dd/MM/yyyy") : "");
            lbCodAssiante.Text = Funcoes.Funcoes.ConvertePara.String(pessoaDcl.IdPessoa);
            tbxNomeAssinantePerfil.Text = pessoaDcl.Nome;
            tbxEmailAssinantePerfil.Text = pessoaDcl.Email;
            tbxTelefoneAssinantePerfil.Text = pessoaDcl.Telefone;
            tbxCelularAssinantePerfil.Text = pessoaDcl.Celular;

            //Tab Endereço Assinante
            PopulaPais(ddlPais, Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinante.SelectedValue));

            if ((pessoaDcl.dNacionalidade != null) &&
                Funcoes.Funcoes.ConvertePara.Int(pessoaDcl.dNacionalidade) == 2)
            {
                ddlPais.SelectedValue = pessoaDcl.Logr_Pais;
            }


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
                LiberaCamposLogradouro(false);
            }

            //Tab Meu Plano
            try
            {

                if (!Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"]))
                {
                    PopulaAbaPlanos(_idPessoa);
                }
                else
                {
                    lbAlterarPlanoAssinantePerfil.Visible = false;
                    lbInserirCartao.Visible = false ;
                }
            }
            catch (Exception e)
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    e.Message, "Atenção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
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

            //Tab Receituário
            PopulaPais(ddlPaisClinica, Funcoes.Funcoes.ConvertePara.Int(
                    ddlNacionalidadeAssinante.SelectedValue));

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                divImgReceit.Visible = true;
                tbNomeclinica.Text = configReceitDcl.NomeClinica;
                tbSlogan.Text = configReceitDcl.Slogan;
                tbSiteClinica.Text = configReceitDcl.Site;
                tbCrmv.Text = configReceitDcl.CRMV;
                ddlUfCrmv.SelectedIndex = ddlUfCrmv.Items.IndexOf(
                    ddlUfCrmv.Items.FindByValue(configReceitDcl.CrmvUf));
                tbEMailClinica.Text = configReceitDcl.Email;
                meFoneClinica.Text = configReceitDcl.Telefone;
                meCelularClinica.Text = configReceitDcl.Celular;

                cbxCabecalho.Checked = Funcoes.Funcoes.ConvertePara.Bool(
                    configReceitDcl.fLivreCabecalho);
                ftbCabecalho.ReadOnly = (Funcoes.Funcoes.ConvertePara.Bool(
                    configReceitDcl.fLivreCabecalho) ? false : true);
                ftbCabecalho.Text = (Funcoes.Funcoes.ConvertePara.Bool(
                    configReceitDcl.fLivreCabecalho) ? configReceitDcl.LivreCabecalho : "");
                cbxRodape.Checked = Funcoes.Funcoes.ConvertePara.Bool(
                    configReceitDcl.fLivreRodape);
                tbxRodape.Enabled = Funcoes.Funcoes.ConvertePara.Bool(
                    configReceitDcl.fLivreRodape);
                tbxRodape.Text = (Funcoes.Funcoes.ConvertePara.Bool(
                    configReceitDcl.fLivreRodape) ? configReceitDcl.LivreRodape : "");

                //Tab Endereço Clínica Receituário
                ddlPaisClinica.SelectedValue = configReceitDcl.Logr_Pais;

                if ((configReceitDcl.Logr_CEP != null) && (configReceitDcl.Logr_CEP != ""))
                {
                    meCEPClinica.Text = configReceitDcl.Logr_CEP;
                    tbLogrClinica.Text = configReceitDcl.Logradouro;
                    tbNrLogrClinica.Text = configReceitDcl.Logr_Nr;
                    tbComplClinica.Text = configReceitDcl.Logr_Compl;
                    tbBairroClinica.Text = configReceitDcl.Logr_Bairro;
                    tbMuniClinica.Text = configReceitDcl.Logr_Cidade;
                    tbUFClinica.Text = configReceitDcl.Logr_UF;
                    lblCepClinInfo.Text = "CEP Informado: " + configReceitDcl.Logr_CEP;
                    mvEndClinica.ActiveViewIndex = 1;
                    LiberaCamposLogrClin(false);
                }

                //Imagens
                imgLogo.ImageUrl = configReceitBll.CarregarImgLogo(_idPessoa);
                lblFileUploadLogo.Text = imgLogo.ImageUrl;
                imgAssinatura.ImageUrl = configReceitBll.CarregarImgAssinatura(_idPessoa);
                lblFileUploadAssinatura.Text = imgAssinatura.ImageUrl;
            }
            else
            {
                divImgReceit.Visible = false;
            }
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
                lbDataFinalAssinaturaAssinantePerfil.Text = "Renovação em: " + (acessosVigenciaPlanoTO.DtFinal != null ?
                    acessosVigenciaPlanoTO.DtFinal.ToString("dd/MM/yyyy") : "") + "&nbsp;&nbsp;";
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

        protected void aMeusDadosPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 0;
            liMeusDadosPerfil.Attributes["class"] = "active";
            liMeuPlanoPerfil.Attributes["class"] = "";
            liTrocaSenhaPerfil.Attributes["class"] = "";
            liImagemPerfil.Attributes["class"] = "";
            liMensagemPerfil.Attributes["class"] = "";
            liReceituarioPerfil.Attributes["class"] = "";
        }

        protected void aMeuPlanoPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 1;
            liMeusDadosPerfil.Attributes["class"] = "";
            liMeuPlanoPerfil.Attributes["class"] = "active";
            liTrocaSenhaPerfil.Attributes["class"] = "";
            liImagemPerfil.Attributes["class"] = "";
            liMensagemPerfil.Attributes["class"] = "";
            liReceituarioPerfil.Attributes["class"] = "";
        }

        protected void aTrocarSenhaPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 2;
            liMeusDadosPerfil.Attributes["class"] = "";
            liMeuPlanoPerfil.Attributes["class"] = "";
            liTrocaSenhaPerfil.Attributes["class"] = "active";
            liImagemPerfil.Attributes["class"] = "";
            liMensagemPerfil.Attributes["class"] = "";
            liReceituarioPerfil.Attributes["class"] = "";
        }

        protected void aImagemPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 3;
            liMeusDadosPerfil.Attributes["class"] = "";
            liMeuPlanoPerfil.Attributes["class"] = "";
            liTrocaSenhaPerfil.Attributes["class"] = "";
            liImagemPerfil.Attributes["class"] = "active";
            liMensagemPerfil.Attributes["class"] = "";
            liReceituarioPerfil.Attributes["class"] = "";
        }

        protected void aMensagemPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 4;
            liMeusDadosPerfil.Attributes["class"] = "";
            liMeuPlanoPerfil.Attributes["class"] = "";
            liTrocaSenhaPerfil.Attributes["class"] = "";
            liImagemPerfil.Attributes["class"] = "";
            liMensagemPerfil.Attributes["class"] = "active";
            liReceituarioPerfil.Attributes["class"] = "";
        }

        protected void aReceituarioPerfil_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 5;
            liMeusDadosPerfil.Attributes["class"] = "";
            liMeuPlanoPerfil.Attributes["class"] = "";
            liTrocaSenhaPerfil.Attributes["class"] = "";
            liImagemPerfil.Attributes["class"] = "";
            liMensagemPerfil.Attributes["class"] = "";
            liReceituarioPerfil.Attributes["class"] = "active";
        }

        protected void lbAlterarPlano_Click(object sender, EventArgs e)
        {
            bllRetorno _validaTela = ValidaCampos(0);
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);

            if (_validaTela.retorno)
            {
                LimpaCamposVoucher();

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

        private void LimpaCamposVoucher()
        {
            meVoucher.Text = "";
            ddlCCRenovAssin.Visible = true;
            lblSelecionaCC.Visible = true;
            ViewState.Remove("VoucherId");

            PopulaPlanoBasico("");
            PopulaPlanoIntermediario("");
            PopulaPlanoCompleto("");
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            bllRetorno _valida = ValidaCampos(0);

            if (_valida.retorno)
            {
                AlterarPessoas(_idPessoa, true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    _valida.mensagem,
                    "Informe NutroVET - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void AlterarPessoas(int id, bool _mostrarToastr)
        {
            int nacionalidade = ddlNacionalidadeAssinante.SelectedIndex;
            int tipoPessoa = Funcoes.Funcoes.ConvertePara.Int(
                ddlTipoPessoaAssinante.SelectedValue);
            pessoaDcl = pessoaBll.Carregar(id);

            pessoaDcl.Nome = tbxNomeAssinantePerfil.Text;
            pessoaDcl.Email = tbxEmailAssinantePerfil.Text;
            pessoaDcl.Telefone = tbxTelefoneAssinantePerfil.Text;
            pessoaDcl.dNacionalidade = nacionalidade;

            if ((tbxCelularAssinantePerfil.Text.Length >= 10) && (nacionalidade == 1))
            {
                pessoaDcl.Celular = FormataCelular(tbxCelularAssinantePerfil.Text);
            }
            else
            {
                pessoaDcl.Celular = tbxCelularAssinantePerfil.Text;
            }

            pessoaDcl.DataNascimento = string.IsNullOrEmpty(txbDataNascimentoAssinantePerfil.Text)
                ? DateTime.Parse("01/01/1910")
                : DateTime.Parse(txbDataNascimentoAssinantePerfil.Text);

            if (nacionalidade == 1) // Nacional
            {
                if (tipoPessoa == 1 && !string.IsNullOrEmpty(tbxCnpjCpfAssinantePerfil.Text))
                {
                    pessoaDcl.CPF = FormataCPF(tbxCnpjCpfAssinantePerfil.Text);
                    pessoaDcl.RG = tbxRGAssinantePerfil.Text;
                    pessoaDcl.dTpEntidade = 1;
                }
                else if (tipoPessoa == 2 && !string.IsNullOrEmpty(tbxCnpjCpfAssinantePerfil.Text))
                {
                    pessoaDcl.CNPJ = FormataCNPJ(tbxCnpjCpfAssinantePerfil.Text);
                    pessoaDcl.dTpEntidade = 2;
                }
                else
                {
                    pessoaDcl.CPF = string.Empty;
                    pessoaDcl.RG = string.Empty;
                    pessoaDcl.CNPJ = string.Empty;
                    pessoaDcl.dTpEntidade = 1;
                }
            }
            else // Internacional
            {
                if (tipoPessoa == 1 && !string.IsNullOrEmpty(tbxDocumentoPerfilAssinante.Text))
                {
                    //pessoa.CPF = meCnpjCpfAssinantePerfil.Text;
                    pessoaDcl.Passaporte = tbxDocumentoPerfilAssinante.Text;
                    pessoaDcl.RG = string.Empty;
                    pessoaDcl.dTpEntidade = 1;
                }
                else if (tipoPessoa == 2 && !string.IsNullOrEmpty(tbxCnpjCpfAssinantePerfil.Text))
                {
                    pessoaDcl.CNPJ = tbxCnpjCpfAssinantePerfil.Text;
                    pessoaDcl.dTpEntidade = 2;
                }
                else
                {
                    pessoaDcl.CPF = string.Empty;
                    pessoaDcl.RG = string.Empty;
                    pessoaDcl.CNPJ = string.Empty;
                    pessoaDcl.dTpEntidade = 1;
                }
            }

            if ((!string.IsNullOrEmpty(ddlPais.SelectedValue)) && (ddlPais.SelectedValue != "0"))
            {
                pessoaDcl.Logr_Pais = ddlPais.SelectedValue;
            }

            if (meCEPAssinantePerfil.Text != "")
            {
                if (nacionalidade == 1)
                {
                    pessoaDcl.Logr_CEP = FormataCEP(meCEPAssinantePerfil.Text);
                }
                else
                {
                    pessoaDcl.Logr_CEP = meCEPAssinantePerfil.Text;
                }
            }
            else
            {
                pessoaDcl.Logr_CEP = "";
            }

            pessoaDcl.Logradouro = tbxLogradouroAssinantePerfil.Text;
            pessoaDcl.Logr_Nr = tbxNumeroLogradouroAssinantePerfil.Text;
            pessoaDcl.Logr_Compl = tbxComplementoLogradouroPerfil.Text;
            pessoaDcl.Logr_Bairro = tbxBairroAssinantePerfil.Text;
            pessoaDcl.Logr_Cidade = tbxCidadeAssinantePerfil.Text;

            if (!string.IsNullOrEmpty(tbxEstadoAssinantePerfil.Text))
            {
                pessoaDcl.Logr_UF = tbxEstadoAssinantePerfil.Text;
            }
            else
            {
                pessoaDcl.Logr_UF = "";
            }

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
                        Funcoes.Funcoes.Toastr.ToastType.Success,
                        alterarRet.mensagem,
                        "Informe NutroVET - Alteração",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                if (_mostrarToastr)
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        alterarRet.mensagem,
                        "Informe NutroVET - Alteração",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
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
            if (tbxEmail.Text != "")
            {
                string _corpoEMail = "Mensagem Enviado pelo Perfil</br>"+
                    "E-mail do Usuário: " + tbxEmail.Text + "</br></br></br>" +
                    tbxMsg.Text;
                Funcoes.Funcoes.fncRetorno fncEnviaMailBll = contatosBll.EnviarEmail(
                    "contato@nutrovet.com.br", "contato@nutrovet.com.br", "brittolobo@hotmail.com",
                    tbxAssunto.Text, _corpoEMail);

                if (fncEnviaMailBll.retorno)
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
                            Funcoes.Funcoes.Toastr.ToastType.Error, _ret.mensagem,
                            "Mensagem", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Error,
                        fncEnviaMailBll.mensagem, "NutroVET Informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning, 
                    "Digite um E-Mail Válido!!!", "Mensagem", 
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void LimpaTela()
        {
            tbxAssunto.Text = "";
            //tbxNome.Text = "";
            //tbxEmail.Text = "";
            tbxMsg.Text = "";
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
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

                        imgFoto.ImageUrl = pessoaBll.CarregarImagem(
                            Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
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

        protected void ddlTpPessoaAssinante_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mascaras(ddlNacionalidadeAssinante.SelectedValue, 
                ddlTipoPessoaAssinante.SelectedValue);

            tbxCnpjCpfAssinantePerfil.Text = "";
            txbDataNascimentoAssinantePerfil.Text = "";
        }

        protected void ddlNacionalidadeAssinante_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaPais(ddlPais, Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinante.SelectedValue));
            PopulaPais(ddlPaisClinica, Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinante.SelectedValue));

            LimpaCampos();

            Mascaras(ddlNacionalidadeAssinante.SelectedValue, ddlTipoPessoaAssinante.SelectedValue);
        }

        private void Mascaras(string _nacional, string _tpPessoa)
        {
            int _idNacional = Funcoes.Funcoes.ConvertePara.Int(_nacional);
            int _idTpPessoa = Funcoes.Funcoes.ConvertePara.Int(_tpPessoa);

            switch (_idNacional)
            {
                case 1:
                    {
                        tbxCnpjCpfAssinantePerfil.Visible = true;
                        tbxDocumentoPerfilAssinante.Visible = false;                        

                        tbxTelefoneAssinantePerfil.Mascara = MEdit.TpMascara.Telefone;
                        tbxTelefoneAssinantePerfil.Attributes["placeholder"] = "(xx) xxxx-xxxx";

                        tbxCelularAssinantePerfil.Mascara = MEdit.TpMascara.Celular;
                        tbxCelularAssinantePerfil.Attributes["placeholder"] = "(xx) xxxxx-xxxx";

                        lbCEPAssinantePerfil.Text = "CEP";
                        meCEPAssinantePerfil.Mascara = MEdit.TpMascara.CEP;
                        meCEPAssinantePerfil.Attributes["placeholder"] = "xxxxx-xxx";

                        switch (_idTpPessoa)
                        {
                            case 1:
                                {
                                    lbNomeAssinantePerfil.Text = "Nome";
                                    divDataNascimentoAssinantePerfil.Visible = true;

                                    lbTituloRGAssinantePerfil.Visible = true;
                                    divRGAssinantePerfil.Visible = true;
                                    lbTituloTipoPessoaAssinantePerfil.Text = "CPF";
                                    //meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CPF;
                                    tbxCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CPF";
                                    tbxCnpjCpfAssinantePerfil.Attributes["title"] = "CPF do Assinante";

                                    break;
                                }
                            case 2:
                                {
                                    lbNomeAssinantePerfil.Text = "Razão Social";
                                    divDataNascimentoAssinantePerfil.Visible = false;

                                    lbTituloRGAssinantePerfil.Visible = false;
                                    divRGAssinantePerfil.Visible = false;
                                    lbTituloTipoPessoaAssinantePerfil.Text = "CNPJ";
                                    //meCnpjCpfAssinantePerfil.Mascara = MEdit.TpMascara.CNPJ;
                                    tbxCnpjCpfAssinantePerfil.Attributes["placeholder"] = "CNPJ";
                                    tbxCnpjCpfAssinantePerfil.Attributes["title"] = "CNPJ do Assinante";

                                    break;
                                }
                        }

                        LiberaCamposLogradouro(false);

                        break;
                    }
                case 2:
                    {
                        tbxCnpjCpfAssinantePerfil.Visible = false;
                        tbxDocumentoPerfilAssinante.Visible = true;
                        lbTituloRGAssinantePerfil.Visible = false;
                        divRGAssinantePerfil.Visible = false;

                        tbxTelefoneAssinantePerfil.Mascara = MEdit.TpMascara.String;
                        tbxTelefoneAssinantePerfil.Attributes["placeholder"] = "";

                        tbxCelularAssinantePerfil.Mascara = MEdit.TpMascara.String;
                        tbxCelularAssinantePerfil.Attributes["placeholder"] = "";

                        lbCEPAssinantePerfil.Text = "ZIP CODE";
                        meCEPAssinantePerfil.Mascara = MEdit.TpMascara.String;
                        meCEPAssinantePerfil.Attributes["placeholder"] = "";

                        switch (_idTpPessoa)
                        {
                            case 1:
                                {
                                    lbNomeAssinantePerfil.Text = "Nome";
                                    divDataNascimentoAssinantePerfil.Visible = true;

                                    lbTituloTipoPessoaAssinantePerfil.Text = "Documento";

                                    break;
                                }
                            case 2:
                                {
                                    lbNomeAssinantePerfil.Text = "Razão Social";
                                    divDataNascimentoAssinantePerfil.Visible = false;

                                    lbTituloTipoPessoaAssinantePerfil.Text = "CNPJ";

                                    break;
                                }
                        }

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
                    ddlNacionalidadeAssinante.SelectedValue);

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

                                tbxCnpjCpfAssinantePerfil.Visible = true;
                                tbxDocumentoPerfilAssinante.Visible = false;

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
                            tbxCnpjCpfAssinantePerfil.Visible = false;
                            tbxDocumentoPerfilAssinante.Visible = true;
                            break;
                        }
                }
            }
        }

        protected void lbCorrigirCEPInformado_Click(object sender, EventArgs e)
        {
            int _nacional = Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinante.SelectedValue);

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

        private void LimpaCampos()
        {
            tbxDocumentoPerfilAssinante.Text = "";
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
                LinkButton lbVincularCCRegistro = (LinkButton)e.Item.FindControl(
                    "lbVincularCCRegistro");
                Label lblCartaoVinculadoRegistro = (Label)e.Item.FindControl(
                    "lblCartaoVinculadoRegistro");

                pessoaCartaoCreditoTO = (TOPessoasCartaoCreditoBll)e.Item.DataItem;
                acessosVigenciaPlanoTO = acessosVigenciaPlanosBLL.CarregarTO(
                    Funcoes.Funcoes.ConvertePara.Int(ViewState["IdVigencia"]));
                _assinaturaPmoTO = _assinaturaPmoBll.Carregar(
                    acessosVigenciaPlanoTO.IdSubscriptionPagarMe);

                if (pessoaCartaoCreditoTO.IdCartao == acessosVigenciaPlanoTO.IdCartao)
                {
                    lbNumeroCartaoCreditoAssinantePerfil.Text = acessosVigenciaPlanoTO.NrCartao;
                    lbVencimentoCartaoCreditoAssinantePerfil.Text = (
                        acessosVigenciaPlanoTO.VencimCartao != null &&
                            acessosVigenciaPlanoTO.VencimCartao != "" ?
                                acessosVigenciaPlanoTO.VencimCartao.Insert(2, "/") : "");
                    lbNomeCartaoCreditoAssinantePerfil.Text = acessosVigenciaPlanoTO.NomeCartao;
                    lblCartaoVinculadoRegistro.Text = "Sim";
                    lbVincularCCRegistro.Visible = false;

                    ViewState["IdCartao"] = pessoaCartaoCreditoTO.IdCartao;
                }
                else
                {
                    lblCartaoVinculadoRegistro.Text = "Não";
                    lbVincularCCRegistro.Text = "<i class='far fa-circle'></i>";
                    lbVincularCCRegistro.Visible = (_assinaturaPmoTO != null &&
                        _assinaturaPmoTO.SubscriptionType == "Transaction" ? false : true);
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
                Label lblNomePlanoReg = (Label)e.Item.FindControl("lblNomePlanoReg");
                
                LinkButton lbCancelarAssinatura = (LinkButton)e.Item.FindControl(
                    "lbCancelarAssinatura");
                LinkButton lbUpgradeAssinatura = (LinkButton)e.Item.FindControl(
                    "lbUpgradeAssinatura");

                _assinaturaPmoTO = (TOAssinaturaPMOBll)e.Item.DataItem;

                string _valor = (_assinaturaPmoTO.Amount != null ? 
                    Funcoes.Funcoes.ConvertePara.String(_assinaturaPmoTO.Amount).Insert(
                        Funcoes.Funcoes.ConvertePara.String(
                            _assinaturaPmoTO.Amount).Length - 2, ",") : "");

                lblNomePlanoReg.Text = (_assinaturaPmoTO.NamePlan != null && 
                    _assinaturaPmoTO.NamePlan != "" ? _assinaturaPmoTO.NamePlan :
                        lblPlanoAssinantePerfil.Text);
                int _idStatus = Funcoes.Funcoes.CarregarEnumValor<
                    DominiosBll.AcessosPlanosAuxSituacaoIngles>(lblStatusReg.Text);
                lblStatusReg.Text = Funcoes.Funcoes.CarregarEnumNome<DominiosBll.AcessosPlanosAuxSituacao>(
                    _idStatus);

                lblValorReg.Text = string.Format("{0:c}", _valor);
                lblInicioReg.Text = string.Format("{0:d}", (
                    _assinaturaPmoTO.CurrentPeriodStart != null ? 
                        _assinaturaPmoTO.CurrentPeriodStart :
                            _assinaturaPmoTO.DtInicial));
                lblFimReg.Text = string.Format("{0:d}", (
                    _assinaturaPmoTO.CurrentPeriodEnd != null ?
                        _assinaturaPmoTO.CurrentPeriodEnd :
                            _assinaturaPmoTO.DtFinal));

                bool _status = acessosVigenciaPlanosBLL.MostraBotaoUpgradeAssinatura(
                        Funcoes.Funcoes.CarregarEnumItem<DominiosBll.AcessosPlanosAuxSituacaoIngles>(
                            _assinaturaPmoTO.Status));

                lbCancelarAssinatura.Visible = 
                    (_assinaturaPmoTO.SubscriptionType == "Transaction" ? false : _status);
                lbUpgradeAssinatura.Visible = 
                    (_assinaturaPmoTO.SubscriptionType == "Transaction" ? false : _status);
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
                        LimpaCamposVoucher();
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

            int _tpPessoa = Funcoes.Funcoes.ConvertePara.Int(ddlTipoPessoaAssinante.SelectedValue);

            if ((_tpPessoa == 1) && (tbxCnpjCpfAssinantePerfil.Text != "") &&
                (ddlNacionalidadeAssinante.SelectedValue == "1"))
            {
                tbxCnpjCpfAssinantePerfil.Text = FormataCPF(tbxCnpjCpfAssinantePerfil.Text);
            }

            if ((_tpPessoa == 2) && (tbxCnpjCpfAssinantePerfil.Text != "") &&
                (ddlNacionalidadeAssinante.SelectedValue == "1"))
            {
                tbxCnpjCpfAssinantePerfil.Text = FormataCNPJ(tbxCnpjCpfAssinantePerfil.Text);
            }

            if ((tbxCelularAssinantePerfil.Text != "") && (tbxCelularAssinantePerfil.Text.Length >= 10))
            {
                tbxCelularAssinantePerfil.Text = FormataCelular(tbxCelularAssinantePerfil.Text);
            }

            if ((meCEPAssinantePerfil.Text != "") && (meCEPAssinantePerfil.Text.Length >= 8) &&
                (ddlNacionalidadeAssinante.SelectedValue == "1"))
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
            else if ((_tpPessoa == 1) && (tbxCnpjCpfAssinantePerfil.Text == "") &&
                    (ddlNacionalidadeAssinante.SelectedValue == "1"))
            {
                tbxCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CPF, na aba Dados, deve ser preenchido!");
            }
            else if ((_tpPessoa == 2) && (tbxCnpjCpfAssinantePerfil.Text == "") &&
                    (ddlNacionalidadeAssinante.SelectedValue == "1"))
            {
                tbxCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CNPJ, na aba Dados, deve ser preenchido!");
            }
            else if ((_tpPessoa == 1) && (!rgCpf.IsMatch(tbxCnpjCpfAssinantePerfil.Text)) &&
                    (ddlNacionalidadeAssinante.SelectedValue == "1"))
            {
                tbxCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "Formato de CPF Inválido!");
            }
            else if ((_tpPessoa == 2) && (!rgCnpj.IsMatch(tbxCnpjCpfAssinantePerfil.Text)) &&
                    (ddlNacionalidadeAssinante.SelectedValue == "1"))
            {
                tbxCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "Formato de CNPJ Inválido!");
            }
            else if ((_tpPessoa == 1) && (!Funcoes.Funcoes.Validacoes.Cpf(tbxCnpjCpfAssinantePerfil.Text)) &&
                    (ddlNacionalidadeAssinante.SelectedValue == "1"))
            {
                tbxCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "CPF Inválido!");
            }
            else if ((_tpPessoa == 2) && (!Funcoes.Funcoes.Validacoes.Cnpj(tbxCnpjCpfAssinantePerfil.Text)) &&
                    (ddlNacionalidadeAssinante.SelectedValue == "1"))
            {
                tbxCnpjCpfAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false, "CNPJ Inválido!");
            }
            else if (((_tpPessoa == 1) || (_tpPessoa == 2)) && (tbxDocumentoPerfilAssinante.Text == "") &&
                    (ddlNacionalidadeAssinante.SelectedValue == "2"))
            {
                tbxDocumentoPerfilAssinante.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo Documento, na aba Dados, deve ser preenchido!");
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
            else if ((ddlNacionalidadeAssinante.SelectedValue == "1") &&
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
            else if ((ddlPais.SelectedValue == "0") || (ddlPais.SelectedValue == ""))
            {
                ddlPais.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo PAÍS, na aba Dados, deve ser selecionado!");
            }
            else if (tbxEstadoAssinantePerfil.Text == "")
            {
                tbxEstadoAssinantePerfil.Focus();

                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ESTADO, na aba Dados, deve ser preenchido!");
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

        private void PopulaPlanoBasico(string _voucher)
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Básico;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Plano_Principal;

            List<TOPlanosBll> _listagem = planosAssinaturasBll.ListarPlanos(
                _codPlano, _tpPlano, _voucher);

            rblBasico.SelectedIndex = -1;
            rblBasico.Items.Clear();
            rblBasico.DataValueField = "IdPlano";
            rblBasico.DataTextField = "ValorDescricao";
            rblBasico.DataSource = _listagem;
            rblBasico.DataBind();

            lblDescrB.Text = (_listagem.FirstOrDefault().TipoPlano != null ?
                _listagem.FirstOrDefault().TipoPlano : "");
        }

        private void PopulaPlanoIntermediario(string _voucher)
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Intermediário;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Plano_Principal;

            List<TOPlanosBll> _listagem = planosAssinaturasBll.ListarPlanos(
                _codPlano, _tpPlano, _voucher);

            rblIntermediario.SelectedIndex = -1;
            rblIntermediario.Items.Clear();
            rblIntermediario.DataValueField = "IdPlano";
            rblIntermediario.DataTextField = "ValorDescricao";
            rblIntermediario.DataSource = _listagem;
            rblIntermediario.DataBind();

            lblDescrI.Text = (_listagem.FirstOrDefault().TipoPlano != null ?
                _listagem.FirstOrDefault().TipoPlano : "");
        }

        private void PopulaPlanoCompleto(string _voucher)
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Completo;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Plano_Principal;

            List<TOPlanosBll> _listagem = planosAssinaturasBll.ListarPlanos(
                _codPlano, _tpPlano, _voucher);

            rblCompleto.SelectedIndex = -1;
            rblCompleto.Items.Clear();
            rblCompleto.DataValueField = "IdPlano";
            rblCompleto.DataTextField = "ValorDescricao";
            rblCompleto.DataSource = _listagem;
            rblCompleto.DataBind();

            lblDescrC.Text = (_listagem.FirstOrDefault().TipoPlano != null ?
                _listagem.FirstOrDefault().TipoPlano : "");
        }

        protected void lbSalvarRenovarAssinatura_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            int _idPlano = PlanoEscolhido();

            acessosVigenciaPlanoTO = acessosVigenciaPlanosBLL.CarregarPlano(_idPessoa);
            planosAssinaturaTO = planosAssinaturasBll.CarregarTO(_idPlano);

            int _idAssinaturaPagarme = Funcoes.Funcoes.ConvertePara.Int(
                acessosVigenciaPlanoTO.IdSubscriptionPagarMe);

            bool _status = acessosVigenciaPlanosBLL.MostraBotaoUpgradeAssinatura(
                    Funcoes.Funcoes.CarregarEnumItem<DominiosBll.AcessosPlanosAuxSituacaoIngles>(
                        acessosVigenciaPlanoTO.StatusPagarMe));

            if ((ddlNacionalidadeAssinante.SelectedValue == "2") &&
                (planosAssinaturaTO.Periodo == "Mensal"))
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Info,
                    "Somente Planos ANUAIS podem ser utilizados para nacionalidade ESTRANGEIRA!</br></br>Por favor, escolha um novo Plano!!!",
                    "Nutrovet INFORMA",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else if (rptListagemCartoesCredito.Items.Count <= 0)
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Info,
                    "Pelo menos UM Cartão de Crédito deve ser Cadastrado/Selecionado para Associar à sua Assinatura!!!</br></br>Por favor, escolha um Cartão de Crédito!!!",
                    "Nutrovet INFORMA",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                if ((_idAssinaturaPagarme > 0) && (_status) &&
                    (ddlNacionalidadeAssinante.SelectedValue == "1"))
                {
                    AlterarPlano(_idPessoa, _idAssinaturaPagarme);
                }
                else
                {
                    Inserir();
                }
            }
        }

        private int PlanoEscolhido()
        {
            int idPlano = 0;

            if (Funcoes.Funcoes.ConvertePara.Int(rblBasico.SelectedValue) > 0)
            {
                idPlano = Funcoes.Funcoes.ConvertePara.Int(rblBasico.SelectedValue);
            }
            else if (Funcoes.Funcoes.ConvertePara.Int(rblIntermediario.SelectedValue) > 0)
            {
                idPlano = Funcoes.Funcoes.ConvertePara.Int(rblIntermediario.SelectedValue);
            }
            else if (Funcoes.Funcoes.ConvertePara.Int(rblCompleto.SelectedValue) > 0)
            {
                idPlano = Funcoes.Funcoes.ConvertePara.Int(rblCompleto.SelectedValue);
            }

            return idPlano;
        }

        protected void Inserir()
        {
            Regex apenasDigitos = new Regex(@"[^\d]");
            int _idPlano = PlanoEscolhido(), _idCupom = 0;
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);

            if (Funcoes.Funcoes.ConvertePara.Int(ViewState["VoucherId"]) > 0)
            {
                _idCupom = Funcoes.Funcoes.ConvertePara.Int(ViewState["VoucherId"]);
                cupomDcl = cupomBll.Carregar(_idCupom);
            }

            if (_idPlano > 0)
            {
                if ((cupomDcl != null) && (cupomDcl.dPlanoTp ==
                    (int)DominiosBll.PlanosAuxTipos.Voucher_30_Dias_Gratuito))
                {
                    bllRetorno _retInsere;

                    #region VIGÊNCIA PLANOS

                    acessosVigenciaPlanoDCL = new AcessosVigenciaPlano();
                    acessosVigenciaPlanoDCL.IdPessoa = _idPessoa;
                    acessosVigenciaPlanoDCL.IdPlano = _idPlano;
                    acessosVigenciaPlanoDCL.DtInicial = DateTime.Today;
                    acessosVigenciaPlanoDCL.DtFinal = DateTime.Today.AddMonths(1);

                    if (_idCupom > 0)
                    {
                        acessosVigenciaPlanoDCL.IdCupom = _idCupom;
                    }

                    acessosVigenciaPlanoDCL.Ativo = true;
                    acessosVigenciaPlanoDCL.IdOperador = 1;
                    acessosVigenciaPlanoDCL.DataCadastro = DateTime.Now;
                    acessosVigenciaPlanoDCL.IP = Request.UserHostAddress;

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

                        acessosVigenciaPlanoDCL.AcessosVigenciaSituacaos.Add(vigenciaSituacaoDcl);
                    }

                    #endregion

                    #region Coloca tudo no Banco de Dados

                    _retInsere = acessosVigenciaPlanosBLL.Inserir(acessosVigenciaPlanoDCL);

                    #endregion

                    if (_retInsere.retorno)
                    {
                        #region Invalida Voucher

                        if (_idCupom > 0)
                        {
                            cupomDcl = cupomBll.Carregar(_idCupom);

                            cupomDcl.fUsado = true;

                            cupomDcl.Ativo = true;
                            cupomDcl.IdOperador = 1;
                            cupomDcl.DataCadastro = DateTime.Now;
                            cupomDcl.IP = Request.UserHostAddress;

                            bllRetorno retCupom = cupomBll.Alterar(cupomDcl);

                            ViewState.Remove("VoucherId");
                        }

                        #endregion

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

                        switch (_pessoasTO.cdNacionalidade)
                        {
                            case 1:
                                {
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
                                        _pessoasTO.cPassaporte : (_pessoasTO.cdTpEntidade == 1 ? _pessoasTO.cCPF :
                                            _pessoasTO.cCNPJ));

                                    _assinaturaPmoTO.DddCustomer = _pessoasTO.cCelular.Substring(1, 2);
                                    _assinaturaPmoTO.PhoneCustomer = apenasDigitos.Replace(_pessoasTO.cCelular.Substring(5,
                                        (_pessoasTO.cCelular.Length - 5)), "");

                                    bllRetorno _retRenovarAssinatura = _assinaturaPmoBll.Inserir(
                                        _assinaturaPmoTO);

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

                                            if (_idCupom > 0)
                                            {
                                                acessosVigenciaPlanoDCL.IdCupom = _idCupom;
                                            }

                                            acessosVigenciaPlanoDCL.Ativo = true;
                                            acessosVigenciaPlanoDCL.IdOperador = 1;
                                            acessosVigenciaPlanoDCL.DataCadastro = DateTime.Now;
                                            acessosVigenciaPlanoDCL.IP = Request.UserHostAddress;

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

                                                acessosVigenciaPlanoDCL.AcessosVigenciaSituacaos.Add(vigenciaSituacaoDcl);
                                            }

                                            #endregion

                                            #region Coloca tudo no Banco de Dados
                                            
                                            _retInsereNoBanco = acessosVigenciaPlanosBLL.Inserir(acessosVigenciaPlanoDCL);

                                            #endregion

                                            if (_retInsereNoBanco.retorno)
                                            {
                                                #region Invalida Voucher

                                                if (_idCupom > 0)
                                                {
                                                    cupomDcl = cupomBll.Carregar(_idCupom);

                                                    cupomDcl.fUsado = true;

                                                    cupomDcl.Ativo = true;
                                                    cupomDcl.IdOperador = 1;
                                                    cupomDcl.DataCadastro = DateTime.Now;
                                                    cupomDcl.IP = Request.UserHostAddress;

                                                    bllRetorno retAlteraCupom = cupomBll.Alterar(cupomDcl);

                                                    ViewState.Remove("VoucherId");
                                                }

                                                #endregion

                                                Thread.Sleep(6000);
                                                Response.Redirect("~/MenuGeral.aspx");
                                            }
                                            else
                                            {
                                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                                    Funcoes.Funcoes.Toastr.ToastType.Error,
                                                    "Erro ao Gravar a Assinatura no Banco de Dados!!! Contate o Administrador!!!",
                                                    "NutroVET informa - Renovação da Assinatura",
                                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                                    true);
                                            }

                                        }
                                        else
                                        {
                                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                                "Erro ao Gravar a Assinatura no Banco de Dados!!! Contate o Administrador!!!",
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

                                    break;
                                }
                            case 2:
                                {
                                    Transaction transacao = InserirTransacao(_pessoasTO,
                                        planosAssinaturaDCL, pessoaCartaoCreditoDcl);
                                    string statusPagamento = "";

                                    if (transacao != null)
                                    {
                                        string idTransaction = transacao.Id;
                                        statusPagamento = Funcoes.Funcoes.GetEnumItem<TransactionStatus>(
                                            (int)transacao.Status).Text;

                                        if ((statusPagamento == "Paid") || (statusPagamento == "Trialing"))
                                        {
                                            #region VIGÊNCIA PLANOS

                                            acessosVigenciaPlanoDCL = new AcessosVigenciaPlano();
                                            acessosVigenciaPlanoDCL.IdPessoa = _idPessoa;
                                            acessosVigenciaPlanoDCL.IdPlano = _idPlano;
                                            acessosVigenciaPlanoDCL.IdSubscriptionPagarMe = transacao.Id;
                                            acessosVigenciaPlanoDCL.IdCartao = pessoaCartaoCreditoDcl.IdCartao;
                                            acessosVigenciaPlanoDCL.StatusPagarMe = statusPagamento;
                                            acessosVigenciaPlanoDCL.DtInicial = DateTime.Today;
                                            acessosVigenciaPlanoDCL.DtFinal = DateTime.Today.AddYears(1);

                                            if (_idCupom > 0)
                                            {
                                                acessosVigenciaPlanoDCL.IdCupom = _idCupom;
                                            }

                                            acessosVigenciaPlanoDCL.Ativo = true;
                                            acessosVigenciaPlanoDCL.IdOperador = 1;
                                            acessosVigenciaPlanoDCL.DataCadastro = DateTime.Now;
                                            acessosVigenciaPlanoDCL.IP = Request.UserHostAddress;

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

                                                acessosVigenciaPlanoDCL.AcessosVigenciaSituacaos.Add(vigenciaSituacaoDcl);
                                            }

                                            #endregion

                                            #region Coloca tudo no Banco de Dados

                                            bllRetorno _retInsereNoBanco = acessosVigenciaPlanosBLL.Inserir(
                                                acessosVigenciaPlanoDCL);


                                            #endregion

                                            if (_retInsereNoBanco.retorno)
                                            {
                                                #region Invalida Voucher

                                                if (_idCupom > 0)
                                                {
                                                    cupomDcl = cupomBll.Carregar(_idCupom);

                                                    cupomDcl.fUsado = true;

                                                    cupomDcl.Ativo = true;
                                                    cupomDcl.IdOperador = 1;
                                                    cupomDcl.DataCadastro = DateTime.Now;
                                                    cupomDcl.IP = Request.UserHostAddress;

                                                    bllRetorno retAlteraCupom = cupomBll.Alterar(cupomDcl);

                                                    ViewState.Remove("VoucherId");
                                                }

                                                #endregion

                                                Thread.Sleep(6000);
                                                Response.Redirect("~/MenuGeral.aspx");
                                            }
                                            else
                                            {
                                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                                    Funcoes.Funcoes.Toastr.ToastType.Error,
                                                    "Erro ao Gravar a Assinatura no Banco de Dados!!! Contate o Administrador!!!",
                                                    "NutroVET informa - Renovação da Assinatura",
                                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                                    true);
                                            }
                                        }
                                        else
                                        {
                                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                                "Erro ao Processar o Pagamento!!! Contate o Administrador!!!",
                                                "NutroVET informa - Renovação da Assinatura",
                                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                                true);
                                        }
                                    }

                                    break;
                                }
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
                popUpModalEscolhaPlanoRenovacao.Show();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Um Plano deve ser Selecionado!!!",
                    "NutroVET informa - Renovação da Assinatura",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected Transaction InserirTransacao(TOPessoasBll _dadosAssinante,
                   PlanosAssinatura _dadosPlano, PessoasCartaoCredito _dadosCartao)
        {
            string DDI = "";
            string DDD = "";
            string Fone = "";

            if (_dadosAssinante.cTelefone != "")
            {
                var apenasDigitos = new Regex(@"[^\d]");
                string texto = apenasDigitos.Replace(tbxTelefoneAssinantePerfil.Text, "");

                DDI = "+1";
                DDD = DDI + (texto).Substring(0, 2);
                Fone = (texto).Substring(2);
            }

            try
            {
                CardHash card = new CardHash
                {
                    CardNumber = _dadosCartao.NrCartao,
                    CardHolderName = _dadosCartao.NomeCartao,
                    CardExpirationDate = _dadosCartao.VencimCartao,
                    CardCvv = _dadosCartao.CodSeg
                };

                string cardhash = card.Generate();

                Transaction transacao = new Transaction();

                decimal valorPlanoDecimal = Funcoes.Funcoes.ConvertePara.Decimal(
                    _dadosPlano.ValorPlano);
                int valorPlanoInt = (Funcoes.Funcoes.ConvertePara.Int(valorPlanoDecimal) * 100);

                transacao.Amount = valorPlanoInt;

                transacao.PaymentMethod = PaymentMethod.CreditCard;

                transacao.CardNumber = _dadosCartao.NrCartao;
                transacao.CardHolderName = _dadosCartao.NomeCartao;
                transacao.CardExpirationDate = _dadosCartao.VencimCartao;
                transacao.CardCvv = _dadosCartao.CodSeg;
                transacao.CardHash = cardhash;

                transacao.Customer = new Customer
                {
                    ExternalId = Funcoes.Funcoes.ConvertePara.String(_dadosAssinante.IdPessoa),
                    Name = _dadosAssinante.cNome,
                    Type = _dadosAssinante.cdTpEntidade == 1 ? CustomerType.Individual :
                        CustomerType.Corporation,
                    Email = _dadosAssinante.cEMail,
                    Country = _dadosAssinante.cLogr_Pais.ToLower(),
                    Documents = new[]
                    {
                        new Document
                        {
                            Type = DocumentType.Passport,
                            Number =  _dadosAssinante.cPassaporte
                        }
                    },
                    PhoneNumbers = new string[]
                    {
                            DDD + Fone
                    },
                    Birthday = (_dadosAssinante.cDtNascim != null ?
                        _dadosAssinante.cDtNascim.Value.ToString("yyyy-MM-dd") :
                            new DateTime(1991, 12, 12).ToString("yyyy-MM-dd"))
                };

                transacao.Billing = new Billing
                {
                    Name = _dadosAssinante.cNome,
                    Address = new Address
                    {
                        Zipcode = _dadosAssinante.cLogr_CEP,
                        Street = _dadosAssinante.cLogradouro,
                        StreetNumber = _dadosAssinante.cLogr_Nr,
                        Complementary = _dadosAssinante.cLogr_Compl,
                        Neighborhood = _dadosAssinante.cLogr_Bairro,
                        City = _dadosAssinante.cLogr_Cidade,
                        State = _dadosAssinante.cLogr_UF,
                        Country = _dadosAssinante.cLogr_Pais.ToLower()
                    }
                };

                transacao.Item = new[]
                {
                    new Item()
                    {
                        Id = Funcoes.Funcoes.ConvertePara.String(_dadosPlano.IdPlanoPagarMe),
                        Title = Funcoes.Funcoes.GetEnumItem<DominiosBll.PlanosAuxNomes>
                                    (_dadosPlano.dNomePlano).Text + " - " +
                                Funcoes.Funcoes.GetEnumItem<DominiosBll.Periodo>
                                    (_dadosPlano.dPeriodo).Text,
                        Quantity = 1,
                        Tangible = true,
                        UnitPrice = Funcoes.Funcoes.ConvertePara.Int(_dadosPlano.ValorPlano)
                    }
                };

                transacao.Save();

                return transacao;
            }
            catch (PagarMeException errPagarMe)
            {
                string errosPagarme = "";

                foreach (var item in errPagarMe.Error.Errors)
                {
                    errosPagarme += Funcoes.Funcoes.TiraCaracteresInvalidos(item.Message) + " ";
                }

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    errosPagarme,
                    "NutroVET informa - Renovação da Assinatura",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);


                return null;
            }
            catch (Exception err)
            {
                var erro = err;
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Transação não efetivada!!!",
                    "NutroVET informa - Renovação da Assinatura",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);

                return null;
            }
        }

        protected void lbVerificarVoucher_Click(object sender, EventArgs e)
        {
            if (meVoucher.Text != "")
            {
                bllRetorno _situacaoVoucher = cupomBll.VoucherSituacao(meVoucher.Text);

                if (_situacaoVoucher.retorno)
                {
                    TOAcessosVigenciaCupomBll cupomTO =
                        (TOAcessosVigenciaCupomBll)_situacaoVoucher.objeto.FirstOrDefault();

                    if ((cupomTO != null) && (cupomTO.IdCupom > 0))
                    {
                        PopulaPlanoBasico(cupomTO.NrCupom);
                        PopulaPlanoIntermediario(cupomTO.NrCupom);
                        PopulaPlanoCompleto(cupomTO.NrCupom);

                        ViewState["VoucherId"] = cupomTO.IdCupom;

                        if (cupomTO.dPlanoTp == 
                            (int) DominiosBll.PlanosAuxTipos.Voucher_30_Dias_Gratuito)
                        {
                            ddlCCRenovAssin.Visible = false;
                            lblSelecionaCC.Visible = false; 
                        }
                        else
                        {
                            ddlCCRenovAssin.Visible = true;
                            lblSelecionaCC.Visible = true;
                        }
                    }
                    else
                    {
                        ddlCCRenovAssin.Visible = true;
                        lblSelecionaCC.Visible = true;

                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Warning,
                            "Plano Inexistente! Contate o Administrador", "Atenção",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
                else
                {
                    ddlCCRenovAssin.Visible = true;
                    lblSelecionaCC.Visible = true;
                    ViewState.Remove("VoucherId");

                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        _situacaoVoucher.mensagem, "Atenção",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                ddlCCRenovAssin.Visible = true;
                lblSelecionaCC.Visible = true;
                ViewState.Remove("VoucherId");

                PopulaPlanoBasico("");
                PopulaPlanoIntermediario("");
                PopulaPlanoCompleto("");

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Digite um Voucher Válido!!!", "Atenção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }

            popUpModalEscolhaPlanoRenovacao.Show();
        }

        protected void AlterarPlano(int _idPessoa, int _idAssinatura)
        {
            Regex apenasDigitos = new Regex(@"[^\d]");
            int _idPlano = PlanoEscolhido(), _idCupom = 0;

            if (Funcoes.Funcoes.ConvertePara.Int(ViewState["VoucherId"]) > 0)
            {
                _idCupom = Funcoes.Funcoes.ConvertePara.Int(ViewState["VoucherId"]);
                cupomDcl = cupomBll.Carregar(_idCupom);
            }

            if (_idPlano > 0)
            {
                if ((cupomDcl != null) && (cupomDcl.dPlanoTp == 
                    (int)DominiosBll.PlanosAuxTipos.Voucher_30_Dias_Gratuito))
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Não é Permitido Aplicar Voucher de Gratuidade para Planos Vigentes!!!",
                        "NutroVET informa - Upgrade de Assinatura",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
                else
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
                            _pessoasTO.cPassaporte : (_pessoasTO.cdTpEntidade == 1 ? _pessoasTO.cCPF :
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

                                if (_idCupom > 0)
                                {
                                    acessosVigenciaPlanoDCL.IdCupom = _idCupom;
                                }

                                acessosVigenciaPlanoDCL.Ativo = true;
                                acessosVigenciaPlanoDCL.IdOperador = 1;
                                acessosVigenciaPlanoDCL.DataCadastro = DateTime.Now;
                                acessosVigenciaPlanoDCL.IP = Request.UserHostAddress;
                                #endregion

                                _retAlteraNoBanco = acessosVigenciaPlanosBLL.Alterar(acessosVigenciaPlanoDCL);

                                if (_retAlteraNoBanco.retorno)
                                {
                                    #region Invalida Voucher
                                    if (_idCupom > 0)
                                    {
                                        cupomDcl = cupomBll.Carregar(_idCupom);

                                        cupomDcl.fUsado = true;

                                        cupomDcl.Ativo = true;
                                        cupomDcl.IdOperador = 1;
                                        cupomDcl.DataCadastro = DateTime.Now;
                                        cupomDcl.IP = Request.UserHostAddress;

                                        bllRetorno retAlteraCupom = cupomBll.Alterar(cupomDcl);

                                        ViewState.Remove("VoucherId");
                                    }
                                    #endregion

                                    Thread.Sleep(6000);
                                    Response.Redirect("~/MenuGeral.aspx");

                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Success,
                                        "Upgrade da Assinatura Efetuada com Sucesso! Parabéns!",
                                        "NutroVET informa - Upgrade de Assinatura",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                                else
                                {
                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Error,
                                        "Erro ao Alterar a Assinatura no Banco de Dados!!! Contate o Admnistrador!!!",
                                        "NutroVET informa - Upgrade de Assinatura",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }

                            }
                            else
                            {
                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Error,
                                    "Erro ao Alterar a Assinatura no Banco de Dados!!! Contate o Admonistrador!!!",
                                    "NutroVET informa - Upgrade de Assinatura",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);
                            }
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                _retRenovarAssinatura.mensagem,
                                "NutroVET informa - Upgrade de Assinatura",
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

                int _idAssinatura = Funcoes.Funcoes.ConvertePara.Int
                    (acessosVigenciaPlanoTO.IdSubscriptionPagarMe);

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
                                _pessoasTO.cPassaporte : (_pessoasTO.cdTpEntidade == 1 ? _pessoasTO.cCPF :
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

        protected void meCEPClinica_TextChanged(object sender, EventArgs e)
        {
            if (meCEPClinica.Text != "")
            {
                int _nacional = Funcoes.Funcoes.ConvertePara.Int(
                    ddlNacionalidadeAssinante.SelectedValue);

                if (_nacional > 0)
                {
                    switch (_nacional)
                    {
                        case 1:
                            {
                                try
                                {
                                    meCEPClinica.Mascara = MEdit.TpMascara.CEP;
                                    meCEPClinica.Attributes["placeholder"] = "xxxxx-xxx";

                                    Cep _cepClin = meCEPClinica.Text;
                                    var viaCepService = ViaCepService.Default();
                                    var enderClin = viaCepService.ObterEndereco(_cepClin);

                                    tbLogrClinica.Text = enderClin.Logradouro;
                                    tbBairroClinica.Text = enderClin.Bairro;
                                    tbMuniClinica.Text = enderClin.Localidade;
                                    tbUFClinica.Text = enderClin.UF;

                                    lblCepClinInfo.Text = "CEP Informado: " + meCEPClinica.Text;

                                    if (enderClin.Logradouro != "")
                                    {
                                        LiberaCamposLogrClin(false);
                                    }
                                    else
                                    {
                                        LiberaCamposLogrClin(true);
                                    }

                                    mvEndClinica.ActiveViewIndex = 1;

                                }
                                catch (Exception err)
                                {
                                    lblCepClinInfo.Text = "CEP Informado: " + meCEPClinica.Text;
                                    mvEndClinica.ActiveViewIndex = 1;
                                    LiberaCamposLogrClin(true);

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
                                meCEPClinica.Mascara = MEdit.TpMascara.String;
                                meCEPClinica.Attributes["placeholder"] = "";

                                lblCepClinInfo.Text = "ZIP CODE Informado: " + meCEPClinica.Text;

                                LiberaCamposLogrClin(true);

                                mvEndClinica.ActiveViewIndex = 1;

                                break;
                            }
                    } 
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Selecione o Campo NACIONALIDADE  na Aba DADOS", "Atenção",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                mvEndClinica.ActiveViewIndex = 0;
            }
        }

        private void LiberaCamposLogrClin(bool _liberar)
        {
            tbLogrClinica.Enabled = _liberar;
            tbBairroClinica.Enabled = _liberar;
            tbMuniClinica.Enabled = _liberar;
            tbUFClinica.Enabled = _liberar;
        }

        protected void lbCorrigirCEPClinicaInformado_Click(object sender, EventArgs e)
        {
            int _nacional = Funcoes.Funcoes.ConvertePara.Int(
                ddlNacionalidadeAssinante.SelectedValue);

            meCEPClinica.Text = "";

            switch (_nacional)
            {
                case 1:
                    {
                        meCEPClinica.Mascara = MEdit.TpMascara.CEP;
                        meCEPClinica.Attributes["placeholder"] = "xxxxx-xxx";
                        lblCepClinInfo.Text = "";

                        break;
                    }
                case 2:
                    {
                        meCEPClinica.Mascara = MaskEdit.MEdit.TpMascara.String;
                        meCEPClinica.Attributes["placeholder"] = "";
                        lblCepClinInfo.Text = "";

                        break;
                    }
            }

            mvEndClinica.ActiveViewIndex = 0;
        }

        protected void lbEnviaLogo_Click(object sender, EventArgs e)
        {
            if (fupldLogo.HasFile)
            {
                if (fupldLogo.PostedFile.ContentLength < 512000)
                {
                    string _extensao = Path.GetExtension(fupldLogo.FileName);
                    string _logotipo = "Logotipo_" + User.Identity.Name + _extensao;

                    try
                    {
                        fupldLogo.SaveAs(Server.MapPath("~/Perfil/Logotipos/") +
                             _logotipo);
                        lblFileUploadLogo.Text = "Arquivo Gravado como: " + _logotipo;

                        imgLogo.ImageUrl = configReceitBll.CarregarImgLogo(
                            Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
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

        protected void lbEnviaAssinatura_Click(object sender, EventArgs e)
        {
            if (fupldAssinatura.HasFile)
            {
                if (fupldAssinatura.PostedFile.ContentLength < 512000)
                {
                    string _extensao = Path.GetExtension(fupldAssinatura.FileName);
                    string _assinat = "Assinatura_" + User.Identity.Name + _extensao;

                    try
                    {
                        fupldAssinatura.SaveAs(Server.MapPath("~/Perfil/Assinaturas/") +
                             _assinat);
                        lblFileUploadAssinatura.Text = "Arquivo Gravado como: " + _assinat;

                        imgAssinatura.ImageUrl = configReceitBll.CarregarImgAssinatura(
                            Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
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

        protected void lbSalvarReceituario_Click(object sender, EventArgs e)
        {
            configReceitDcl = configReceitBll.Carregar(Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name));

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                AlterarDadosReceituario(configReceitDcl);
            }
            else
            {
                InserirDadosReceituario();
            }
        }

        private void InserirDadosReceituario()
        {
            configReceitDcl = new ConfigReceituario();

            configReceitDcl.IdPessoa = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            configReceitDcl.DtIniUso = DateTime.Today;
            configReceitDcl.DtIniUsoLT = DateTime.Today;
            configReceitDcl.NomeClinica = tbNomeclinica.Text;
            configReceitDcl.Slogan = tbSlogan.Text;
            configReceitDcl.fLivreCabecalho = cbxCabecalho.Checked;
            configReceitDcl.LivreCabecalho = ftbCabecalho.Text;
            configReceitDcl.fLivreRodape = cbxRodape.Checked;
            configReceitDcl.LivreRodape = tbxRodape.Text;
            configReceitDcl.Site = tbSiteClinica.Text;
            configReceitDcl.CRMV = tbCrmv.Text;
            configReceitDcl.CrmvUf = ddlUfCrmv.SelectedValue;
            configReceitDcl.Email = tbEMailClinica.Text;
            configReceitDcl.Telefone = meFoneClinica.Text;
            configReceitDcl.Celular = meCelularClinica.Text;
            configReceitDcl.Logr_CEP = meCEPClinica.Text;
            configReceitDcl.Logradouro = tbLogrClinica.Text;
            configReceitDcl.Logr_Nr = tbNrLogrClinica.Text;
            configReceitDcl.Logr_Compl = tbComplClinica.Text;
            configReceitDcl.Logr_Bairro = tbBairroClinica.Text;
            configReceitDcl.Logr_Cidade = tbMuniClinica.Text;
            configReceitDcl.Logr_UF = tbUFClinica.Text;
            configReceitDcl.Logr_Pais = ddlPaisClinica.SelectedItem.Text;

            configReceitDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            configReceitDcl.Ativo = true;
            configReceitDcl.DataCadastro = DateTime.Now;
            configReceitDcl.IP = Request.UserHostAddress;

            bllRetorno inserirReceita = configReceitBll.Inserir(configReceitDcl);

            if (inserirReceita.retorno)
            {
                divImgReceit.Visible = true;

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    inserirReceita.mensagem,
                    "NutroVET Informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
            }
            else
            {
                divImgReceit.Visible = false;

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    inserirReceita.mensagem,
                    "NutroVET Informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
            }
        }

        private void AlterarDadosReceituario(ConfigReceituario _configReceitDcl)
        {
            if (_configReceitDcl.DtIniUsoLT == null)
            {
                _configReceitDcl.DtIniUsoLT = DateTime.Today;
            }

            _configReceitDcl.NomeClinica = tbNomeclinica.Text;
            _configReceitDcl.Slogan = tbSlogan.Text;
            _configReceitDcl.fLivreCabecalho = cbxCabecalho.Checked;
            _configReceitDcl.LivreCabecalho = ftbCabecalho.Text;
            _configReceitDcl.fLivreRodape = cbxRodape.Checked;
            _configReceitDcl.LivreRodape = tbxRodape.Text;
            _configReceitDcl.Site = tbSiteClinica.Text;
            _configReceitDcl.CRMV = tbCrmv.Text;
            _configReceitDcl.CrmvUf = ddlUfCrmv.SelectedValue;
            _configReceitDcl.Email = tbEMailClinica.Text;
            _configReceitDcl.Telefone = meFoneClinica.Text;
            _configReceitDcl.Celular = meCelularClinica.Text;
            _configReceitDcl.Logr_CEP = meCEPClinica.Text;
            _configReceitDcl.Logradouro = tbLogrClinica.Text;
            _configReceitDcl.Logr_Nr = tbNrLogrClinica.Text;
            _configReceitDcl.Logr_Compl = tbComplClinica.Text;
            _configReceitDcl.Logr_Bairro = tbBairroClinica.Text;
            _configReceitDcl.Logr_Cidade = tbMuniClinica.Text;
            _configReceitDcl.Logr_UF = tbUFClinica.Text;
            _configReceitDcl.Logr_Pais = ddlPaisClinica.SelectedItem.Text;

            _configReceitDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            _configReceitDcl.Ativo = true;
            _configReceitDcl.DataCadastro = DateTime.Now;
            _configReceitDcl.IP = Request.UserHostAddress;

            bllRetorno alterarReceita = configReceitBll.Alterar(_configReceitDcl);

            if (alterarReceita.retorno)
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    alterarReceita.mensagem,
                    "NutroVET Informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    alterarReceita.mensagem,
                    "NutroVET Informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
            }
        }

        protected void cbxCabecalho_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxCabecalho.Checked)
            {
                ftbCabecalho.ReadOnly = false;
            }
            else
            {
                ftbCabecalho.ReadOnly = true;
                ftbCabecalho.Text = string.Empty;
            }
        }

        protected void cbxRodape_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxRodape.Checked)
            {
                tbxRodape.Enabled = true;
            }
            else
            {
                tbxRodape.Enabled = false;
                tbxRodape.Text = string.Empty;
            }
        }
    }
}