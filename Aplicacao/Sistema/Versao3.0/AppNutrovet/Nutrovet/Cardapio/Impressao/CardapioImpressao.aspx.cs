using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using System.Web.Security;
using MaskEdit;

namespace Nutrovet.Cardapio.Impressao
{
    public partial class CardapioImpressao : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll assinanteBll = new clPessoasBll();
        protected Pessoa assinanteDCL;
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;
        protected TOCardapioResumoBll resumoTO;
        protected clCardapioAlimentosBll cardAlimBll = new clCardapioAlimentosBll();
        protected CardapiosAlimento cardAlimDcl;
        protected TOCardapioAlimentosBll cardAlimTO;
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

                    int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                    int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                        Funcoes.Funcoes.Seguranca.Descriptografar(
                            Funcoes.Funcoes.ConvertePara.String(
                                Request.QueryString["_idCardapio"])));
                    hlCardapio.NavigateUrl =
                        "~/Cardapio/CardapioCadastro.aspx?_idCardapio=" +
                            Funcoes.Funcoes.ConvertePara.String(
                                Request.QueryString["_idCardapio"]);

                    ViewState["_idCardapio"] = _idCardapio;

                    PopulaTela(_idCardapio, _idPessoa);
                    PopulaCabecalhoReceita(_idPessoa);
                    PopularLogo(_idPessoa);
                    PopularAssinatura(_idPessoa);
                    PopulaResumo(_idCardapio);
                    PopulaAlimentos(_idCardapio);
                }
            }
        }

        private void PopulaAlimentos(int idCardapio)
        {
            rptCardapImpressao.DataSource = cardAlimBll.ListarTO(idCardapio, 
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
            rptCardapImpressao.DataBind();
        }

        private void PopulaResumo(int idCardapio)
        {
            cardapioTO = cardapioBll.CarregarTO(idCardapio);
            resumoTO = cardapioBll.CardapioResumo(idCardapio, 
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));

            lblCarbVal.Text = resumoTO.CarboG;
            lblCarbPorcent.Text = resumoTO.CarboP;
            lblProtVal.Text = resumoTO.ProtG;
            lblProtPorcent.Text = resumoTO.ProtP;
            lblGordVal.Text = resumoTO.GordG;
            lblGordPorcent.Text = resumoTO.GordP;

            lblFibraVal.Text = resumoTO.FibrasG;
            lblUmidVal.Text = resumoTO.UmidageG;
            lblEnReq.Text = (cardapioTO.NEM > 0 ? 
                string.Format("{0:#,###} Kcal", cardapioTO.NEM) : "0");
            lblEnPresente.Text = resumoTO.EnergiaKcal;
        }

        private void PopulaTela(int idCardapio, int idPessoa)
        {
            cardapioTO = cardapioBll.CarregarTO(idCardapio);
            configReceitDcl = configReceitBll.Carregar(idPessoa);

            lblPaciente.Text = cardapioTO.Animal;
            lblPeso.Text = Funcoes.Funcoes.ConvertePara.String(cardapioTO.PesoAtual) + 
                " kg";
            lblEspecie.Text = cardapioTO.Especie;
            lblSexo.Text = cardapioTO.Sexo;
            lblRaca.Text = cardapioTO.Raca;
            lblIdade.Text = Funcoes.Funcoes.ConvertePara.String(cardapioTO.Idade) + 
                " ano(s)";
            lblTutor.Text = cardapioTO.Tutor;
            lblEMailTutor.Text = cardapioTO.TutorEMail;
            lblFoneTutor.Text = cardapioTO.TutorFone;

            ftbObservacao.Text = cardapioTO.Observacao;

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                PopulaLinksImpressao(cardapioTO);
            }
            else
            {
                lblDescricao.Text = @"É necessário configurar os Dados Básicos da Receita no 
                    MENU:</br>    Minha Conta > Perfil > Aba Receituário!!!
                    </br></br>
                    Clique no link abaixo para abir a Tela de Perfil!
                    </br>
                    </br>";

                popUpModal.Show();
            }
        }

        private void PopulaLinksImpressao(TOCardapioBll _cardapTO)
        {
            bool _existeFile = cardapioBll.ExisteArquivoReceita(_cardapTO.Arquivo);
            string url = string.Format($@"~/Cardapio/Impressao/RptCardapio.aspx?_idCard={
                Funcoes.Funcoes.ConvertePara.String(_cardapTO.IdCardapio)}&_idPessoa={
                    User.Identity.Name}&_tpImpr=");
            
            hlImprCardapioPdf.NavigateUrl = url + "pdf";
            hlImprCardapioPdf.Enabled = true;
            hlImprCardapioPdf.CssClass = "btn btn-warning";

            if (_existeFile)
            {
                hlImprCardapioWord.NavigateUrl = url + "word";
                hlImprCardapioWord.Enabled = true;
                hlImprCardapioWord.Visible = true;
                hlImprCardapioWord.CssClass = "btn btn-warning";

                hlImprCardapioExcel.NavigateUrl = url + "excel";
                hlImprCardapioExcel.Enabled = true;
                hlImprCardapioExcel.Visible = true;
                hlImprCardapioExcel.CssClass = "btn btn-warning";
            }
            else
            {
                hlImprCardapioWord.NavigateUrl = "";
                hlImprCardapioWord.Enabled = false;
                hlImprCardapioWord.Visible = false;
                hlImprCardapioWord.CssClass = "btn btn-basic";

                hlImprCardapioExcel.NavigateUrl = "";
                hlImprCardapioExcel.Enabled = false;
                hlImprCardapioExcel.Visible = false;
                hlImprCardapioExcel.CssClass = "btn btn-basic";
            }
        }

        private void PopularLogo(int idPessoa)
        {
            if ((imgLogo.ImageUrl == "") ||
                (imgLogo.ImageUrl == "~/Perfil/Logotipos/logo_receita.png"))
            {
                imgLogo.ImageUrl = configReceitBll.CarregarImgLogo(idPessoa);
            }
        }

        private void PopularAssinatura(int idPessoa)
        {
            if ((imgAssinatura.ImageUrl == "") ||
                (imgAssinatura.ImageUrl == "~/Perfil/Assinaturas/assinatura_receita.png"))
            {
                imgAssinatura.ImageUrl = configReceitBll.CarregarImgAssinatura(idPessoa);
            }
        }

        private void PopulaCabecalhoReceita(int idPessoa)
        {
            configReceitDcl = configReceitBll.Carregar(idPessoa);

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                lblNomeClin.Text = (configReceitDcl.NomeClinica != "" ?
                    configReceitDcl.NomeClinica : "");
                lblSlogan.Text = (configReceitDcl.Slogan != "" ? configReceitDcl.Slogan : "");
                //hlkSite.Text = configReceitDcl.Site;
                lblEndereco.Text = configReceitBll.MontaCamposEndereco(configReceitDcl);
                lblEMail.Text = (configReceitDcl.Email != "" ? configReceitDcl.Email : "");
                lblTelefone.Text =
                    (configReceitDcl.Telefone != "" ? configReceitDcl.Telefone +
                        (configReceitDcl.Celular != "" ? " / " + configReceitDcl.Celular : "") :
                             (configReceitDcl.Celular != "" ? configReceitDcl.Celular : ""));
                lblLocalData.Text = (configReceitDcl.Logr_Cidade != "" ?
                    configReceitDcl.Logr_Cidade + ", " : "") + DateTime.Today.ToString("D");

                assinanteDCL = assinanteBll.Carregar(Funcoes.Funcoes.ConvertePara.Int(
                                            User.Identity.Name));
                lblNomeVeterinario.Text = assinanteDCL.Nome;
                lblTituloECRMV.Text = 
                    (Funcoes.Funcoes.ConvertePara.Bool(configReceitDcl.fLivreRodape) ? 
                        configReceitDcl.LivreRodape : 
                            ("Médico(a) Veterinário(a) - CRMV" + 
                                (configReceitDcl.CrmvUf != null && 
                                 configReceitDcl.CrmvUf != "" &&
                                 configReceitDcl.CrmvUf != "0" ? 
                                    "/" + configReceitDcl.CrmvUf + " " : 
                                    "") + configReceitDcl.CRMV
                             )
                     );

                if (Funcoes.Funcoes.ConvertePara.Bool(configReceitDcl.fLivreCabecalho))
                {
                    divCabecalhoGrande.Visible = false;
                    divCabecalhoSlim.Visible = true;

                    lblCabecalhoSlim.Text = configReceitDcl.LivreCabecalho;
                }
                else
                {
                    divCabecalhoGrande.Visible = true;
                    divCabecalhoSlim.Visible = false;

                    lblCabecalhoSlim.Text = "";
                }
            }
            else
            {
                divCabecalhoGrande.Visible = true;
                divCabecalhoSlim.Visible = false;

                lblSlogan.Text = "(Configure os Dados da Receita em Perfil > Aba Receituário)";
                lblEndereco.Text = "";
                lblEMail.Text = "";
                lblTelefone.Text = "";
                lblLocalData.Text = "";
            }
        }

        protected void lbSalvaReceita_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            configReceitDcl = configReceitBll.Carregar(_idPessoa);

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                Alterar(Funcoes.Funcoes.ConvertePara.Int(ViewState["_idCardapio"]));
            }
            else
            {
                lblDescricao.Text = @"É necessário configurar os Dados Básicos da Receita no 
                    MENU:</br>    Minha Conta > Perfil > Aba Receituário!!!
                    </br></br>
                    Clique no link abaixo para abir a Tela de Perfil!
                    </br>
                    </br>";

                popUpModal.Show();
            }
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cardapio/CardapioCadastro.aspx?_idCardapio=" +
                Funcoes.Funcoes.Seguranca.Criptografar(Funcoes.Funcoes.ConvertePara.String(
                    ViewState["_idCardapio"])));
        }

        protected void rptCardapImpressao_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {   
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        cardAlimTO = (TOCardapioAlimentosBll)e.Item.DataItem;
                        decimal?  _soma = Funcoes.Funcoes.ConvertePara.Decimal(
                            ViewState["_soma"]) + cardAlimTO.Quant;

                        Label lblValor = (Label)e.Item.FindControl("lblValor");

                        lblValor.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            cardAlimTO.Quant) > 0 ? string.Format("{0:#,##0.00}",
                            cardAlimTO.Quant) : "0");

                        ViewState["_soma"] = _soma;

                        break;
                    }
                case ListItemType.Footer:
                    {
                        Label lblTotalValor = (Label)e.Item.FindControl("lblTotalValor");

                        lblTotalValor.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            ViewState["_soma"]) > 0 ? string.Format("{0:#,##0.00}",
                            ViewState["_soma"]) : "0");

                        break;
                    }
            }
        }

        private void Alterar(int _idReceita)
        {
            bllRetorno updateRet;
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            configReceitDcl = configReceitBll.Carregar(_idPessoa);

            cardapioDcl = cardapioBll.Carregar(_idReceita);

            cardapioDcl.Observacao = ftbObservacao.Text;

            cardapioDcl.Ativo = true;
            cardapioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            cardapioDcl.DataCadastro = DateTime.Now;
            cardapioDcl.IP = Request.UserHostAddress;

            updateRet = cardapioBll.Alterar(cardapioDcl);

            if (updateRet.retorno)
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Alteração Efetuada com Sucesso!!!",
                    "NutroVET Informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    updateRet.mensagem,
                    "NutroVET Informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }
    }
}