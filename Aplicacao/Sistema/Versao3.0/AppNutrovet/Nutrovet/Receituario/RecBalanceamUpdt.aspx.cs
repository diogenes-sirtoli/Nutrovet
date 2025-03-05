using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using AjaxControlToolkit;
using MaskEdit;
using System.Web.Security;
using System.Collections;
using System.Data.SqlClient;

namespace Nutrovet.Receituario
{
    public partial class RecBalanceamUpdt : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll assinanteBll = new clPessoasBll();
        protected Pessoa assinanteDCL;
        protected clAnimaisBll animaisBll = new clAnimaisBll();
        protected Animai animaisDcl;
        protected TOAnimaisBll dadosPacienteTO;
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;
        protected clConfigReceituarioBll configReceitBll = new clConfigReceituarioBll();
        protected ConfigReceituario configReceitDcl;
        protected clExigenciasNutrBll exigenciasNutrBll = new clExigenciasNutrBll();
        protected ExigenciasNutricionai exigenciasNutrDcl;
        protected clReceituarioBll receituarioBll = new clReceituarioBll();
        protected DCL.Receituario receituarioDcl;
        protected TOReceituarioBll receituarioTO;
        protected clReceituarioNutrientesBll recNutrBll = new clReceituarioNutrientesBll();
        protected ReceituarioNutriente recNutrDcl;
        protected List<TOReceituarioNutrientesBll> listRecNutrTO;
        protected clNutrientesAuxGruposBll gruposNutrientesBll = new
            clNutrientesAuxGruposBll();

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
                    int _idReceita = Funcoes.Funcoes.ConvertePara.Int(
                        Funcoes.Funcoes.Seguranca.Descriptografar(
                            Funcoes.Funcoes.ConvertePara.String(
                                Request.QueryString["_idReceita"])));
                    Session.Remove("BalancoDieta");

                    if (_idReceita > 0)
                    {
                        ViewState["_idReceita"] = _idReceita;

                        PopulaCabecalhoReceita(_idPessoa);
                        PopularLogo(_idPessoa);
                        PopularAssinatura(_idPessoa);
                        PopulaTela(_idReceita, _idPessoa);

                        string x = Funcoes.Funcoes.ManterPosicaoSelecionadaGridView(
                                gridRepeater.ClientID);
                        ClientScript.RegisterStartupScript(this.GetType(), "tt", x);
                    }
                    else
                    {
                        Response.Redirect("~/MenuGeral.aspx?perm=" +
                            Funcoes.Funcoes.Seguranca.Criptografar("False"));
                    } 
                }
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

        private void PopulaTela(int _idReceita, int _idPessoa)
        {
            receituarioDcl = receituarioBll.Carregar(_idReceita);
            cardapioTO = cardapioBll.CarregarTO(receituarioDcl.IdCardapio);
            configReceitDcl = configReceitBll.Carregar(_idPessoa);

            ViewState["_cardapio"] = cardapioTO;

            lblPaciente.Text = cardapioTO.Animal;
            lblPeso.Text = Funcoes.Funcoes.ConvertePara.String(cardapioTO.PesoAtual) +
                " Kg(s)";
            lblEspecie.Text = cardapioTO.Especie;
            lblSexo.Text = cardapioTO.Sexo;
            lblRaca.Text = cardapioTO.Raca;
            lblIdade.Text = Funcoes.Funcoes.ConvertePara.String(cardapioTO.Idade) +
                " ano(s)";
            lblTutor.Text = cardapioTO.Tutor;
            lblEMailTutor.Text = cardapioTO.TutorEMail;
            lblFoneTutor.Text = cardapioTO.TutorFone;

            ftbObservacao.Text = receituarioDcl.Observacao;
            lblTblExigNutr.Text = (
                Funcoes.Funcoes.ConvertePara.Int(receituarioDcl.dTblExigNutr) > 0 ? 
                Funcoes.Funcoes.CarregarEnumNome<DominiosBll.ExigenciasNutrAuxTabelas>(
                    receituarioDcl.dTblExigNutr) : 
                "");

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                PopulaLinksImpressao(receituarioDcl);
            }

            PopulaTabelaNutricional(_idReceita);
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
                lblSlogan.Text = "(Configure os Dados da Receita em Perfil > Aba Receituário)";
                lblEndereco.Text = "";
                lblEMail.Text = "";
                lblTelefone.Text = "";
                lblLocalData.Text = "";
            }
        }

        private void PopulaTabelaNutricional(int _idRec)
        {
            acGrupos.DataSource = recNutrBll.ListarGruposNutr(_idRec);
            acGrupos.DataBind();

            acGrupos.SelectedIndex = 0;
        }

        protected void acGrupos_ItemDataBound(object sender, AjaxControlToolkit.AccordionItemEventArgs e)
        {
            switch (e.ItemType)
            {
                case AccordionItemType.Header:
                    {
                        HiddenField hfExigNutr =
                            (HiddenField)e.AccordionItem.FindControl("hfExigNutr");

                        ViewState["IdGrupo"] = Funcoes.Funcoes.ConvertePara.Int(
                            hfExigNutr.Value);

                        break;
                    }
                case AccordionItemType.Content:
                    {
                        Repeater rptTblNutr_II = (Repeater)
                            e.AccordionItem.FindControl("rptTblNutr_II");

                        int _idGrupo = Funcoes.Funcoes.ConvertePara.Int(
                            ViewState["IdGrupo"]);
                        int _idReceita = Funcoes.Funcoes.ConvertePara.Int(
                            ViewState["_idReceita"]);

                        List<TOReceituarioNutrientesBll> retListagem =
                                recNutrBll.ListarImpressao(_idReceita).Where(
                                    w => w.IdGrupo == _idGrupo).ToList();

                        Session["BalancoDieta"] = recNutrBll.GeraRelatorio(
                                (List<TOExigNutrTabelasBll>)Session["BalancoDieta"],
                                retListagem);

                        rptTblNutr_II.Visible = true;
                        rptTblNutr_II.DataSource = retListagem;
                        rptTblNutr_II.DataBind();

                        break;
                    }
            }
        }
        
        protected void rptTblNutr_II_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        cardapioTO = (TOCardapioBll)ViewState["_cardapio"];
                        Label lblEnergia = (Label)e.Item.FindControl("lblEnergia");

                        lblEnergia.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            cardapioTO.FatorEnergia) > 0 ? string.Format("{0:#,##0.00}",
                            cardapioTO.FatorEnergia) : "");

                        break;
                    }
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        TOReceituarioNutrientesBll exigNutrTabelasTO =
                            (TOReceituarioNutrientesBll)e.Item.DataItem;

                        Label lblValMin = (Label)e.Item.FindControl("lblValMin");
                        Label lblValMax = (Label)e.Item.FindControl("lblValMax");
                        Label lblValAdeq = (Label)e.Item.FindControl("lblValAdeq");
                        Label lblValRecomend = (Label)e.Item.FindControl("lblValRecomend");
                        Label lblValEmCardapio = (Label)e.Item.FindControl("lblValEmCardapio");
                        Label lblValFalta = (Label)e.Item.FindControl("lblValFalta");
                        Label lblValSobra = (Label)e.Item.FindControl("lblValSobra");

                        lblValMin.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.DoseMin) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.DoseMin) : "");
                        lblValMax.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.DoseMax) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.DoseMax) : "");
                        lblValAdeq.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Adequado) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Adequado) : "");
                        lblValRecomend.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Recomendado) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Recomendado) : "");
                        lblValEmCardapio.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Consta) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Consta) : "");
                        lblValFalta.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Falta) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Falta) : "");

                        if (Funcoes.Funcoes.ConvertePara.Decimal(exigNutrTabelasTO.Falta) > 0)
                        {
                            lblValFalta.CssClass = "TableNutrFalta";
                        }
                        else
                        {
                            lblValFalta.CssClass = "";
                        }

                        if (Funcoes.Funcoes.ConvertePara.Decimal(exigNutrTabelasTO.Sobra) > 0)
                        {
                            lblValSobra.CssClass = "TableNutrSobra";
                        }
                        else
                        {
                            lblValSobra.CssClass = "";
                        }

                        lblValSobra.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Sobra) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Sobra) : "");

                        break;
                    }
            }
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Receituario/ReceituarioSelecao.aspx");
        }

        protected void lbSalvaReceita_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            int _idReceita = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idReceita"]);
            configReceitDcl = configReceitBll.Carregar(_idPessoa);

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                Alterar(_idReceita);
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

        private void Alterar(int _idReceita)
        {
            bllRetorno updateRet;

            receituarioDcl = receituarioBll.Carregar(_idReceita);

            receituarioDcl.Observacao = ftbObservacao.Text;

            receituarioDcl.Ativo = true;
            receituarioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            receituarioDcl.DataCadastro = DateTime.Now;
            receituarioDcl.IP = Request.UserHostAddress;

            updateRet = receituarioBll.Alterar(receituarioDcl);

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

        private void PopulaLinksImpressao(DCL.Receituario _receita)
        {
            string _tblExigNutr = Funcoes.Funcoes.CarregarEnumNome<
                DominiosBll.ExigenciasNutrAuxTabelas>(receituarioDcl.dTblExigNutr);
            bool _existeFile = receituarioBll.ExisteArquivoReceita(_receita.Arquivo);
            string url = string.Format("~/Cardapio/Impressao/RptBalancoCardapio.aspx?" +
                "_tabela={0}&_idCard={1}&_idRec={2}&_quantDietas={3}&_tpImpr=",
                _tblExigNutr, receituarioDcl.IdCardapio, receituarioDcl.IdReceita, 
                receituarioDcl.QuantDietas);

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
    }
}