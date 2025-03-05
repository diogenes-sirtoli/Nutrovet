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

namespace Nutrovet.Cardapio
{
    public partial class CardapioBalanceamento : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clNutrientesAuxGruposBll gruposNutrientesBll = new
            clNutrientesAuxGruposBll();
        protected clExigenciasNutrBll exigenciasNutrBll = new clExigenciasNutrBll();
        protected ExigenciasNutricionai exigenciasNutrDcl;
        protected clAnimaisBll animaisBll = new clAnimaisBll();
        protected Animai animaisDcl;
        protected TOAnimaisBll dadosPacienteTO;
        protected clCardapioAlimentosBll cardapioAlimentosBll = new
            clCardapioAlimentosBll();
        protected CardapiosAlimento cardapioAlimentosDcl;
        protected TOCardapioResumoBll cardapioAlimentosTO;
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;
        protected clConfigReceituarioBll configReceitBll = new clConfigReceituarioBll();
        protected ConfigReceituario configReceitDcl;
        protected clReceituarioBll receituarioBll = new clReceituarioBll();
        protected DCL.Receituario receituarioDcl;
        protected TOReceituarioBll receituarioTO;
        protected clReceituarioNutrientesBll recNutrBll = new clReceituarioNutrientesBll();
        protected ReceituarioNutriente recNutrDcl;
        protected TOReceituarioNutrientesBll recNutrTO;
        protected List<TOReceituarioNutrientesBll> listRecNutrTO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (acessosBll.Permissao(
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "6.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        ArrayList _cardapios = (ArrayList)Session["ArrayCardapios"];
                        Session.Remove("ArrayCardapios");
                        Session.Remove("BalancoDieta");
                        ViewState["ArrayCardapios"] = _cardapios;

                        PopulaTblsExigNutr();

                        Page.Form.DefaultFocus = ddlTblExigNutr.ClientID;
                    }

                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        protected void ddlTblExigNutr_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("BalancoDieta");

            PopulaTabelaNutricional();

            if (Funcoes.Funcoes.ConvertePara.Int(ddlTblExigNutr.SelectedValue) > 0)
            {
                ftbObservacao.ReadOnly = false;

                lbSalvaReceita.Enabled = true;
                lbSalvaReceita.CssClass = "btn btn-primary-nutrovet";
            }
            else
            {
                acGrupos.DataSource = null;
                acGrupos.DataBind();

                ftbObservacao.ReadOnly = true;
                ftbObservacao.Text = string.Empty;

                lbSalvaReceita.Enabled = false;
                lbSalvaReceita.CssClass = "btn btn-info";

                lbImpressaoCardapio.NavigateUrl = "";
                lbImpressaoCardapio.Enabled = false;
                lbImpressaoCardapio.CssClass = "btn btn-info";
            }

            acGrupos.SelectedIndex = 0;
        }

        protected void acGrupos_ItemDataBound(object sender, AjaxControlToolkit.AccordionItemEventArgs e)
        {
            int _tabNutr = Funcoes.Funcoes.ConvertePara.Int(
                ddlTblExigNutr.SelectedValue);

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
                        Repeater rptTabelasNutricionais = (Repeater)
                            e.AccordionItem.FindControl("rptTabelasNutricionais");

                        Repeater rptTblNutr_II = (Repeater)
                            e.AccordionItem.FindControl("rptTblNutr_II");

                        ArrayList _cardapios = (ArrayList)ViewState["ArrayCardapios"];

                        if (_cardapios.Count > 1)
                        {
                            ArrayList conjuntoExigNutr = new ArrayList();
                            List<TOExigNutrTabelasBll> retListagem = new List<TOExigNutrTabelasBll>();

                            if (ViewState["CardapioItem01"] == null)
                            {
                                int _idcardapio1 = Funcoes.Funcoes.ConvertePara.Int(_cardapios[0]);

                                cardapioTO = cardapioBll.CarregarTO(_idcardapio1);
                                ViewState["CardapioItem01"] = cardapioTO;
                            }
                            else
                            {
                                cardapioTO = (TOCardapioBll)ViewState["CardapioItem01"];
                            }

                            if (ViewState["dadosPaciente"] == null)
                            {
                                dadosPacienteTO = animaisBll.CarregarTO(Funcoes.Funcoes.ConvertePara.Int(
                                    cardapioTO.IdAnimal));
                                ViewState["dadosPaciente"] = dadosPacienteTO;
                            }
                            else
                            {
                                dadosPacienteTO = (TOAnimaisBll)ViewState["dadosPaciente"];
                            }

                            bool _lactante = Funcoes.Funcoes.ConvertePara.Bool(cardapioTO.Lactante);
                            bool _gestante = Funcoes.Funcoes.ConvertePara.Bool(cardapioTO.Gestante);

                            DominiosBll.ExigenciasNutrAuxIndicacoes situacaoDoAnimal =
                                animaisBll.CrescimentoAnimal(dadosPacienteTO, _gestante,
                                _lactante);

                            foreach (var item in _cardapios)
                            {
                                TOCardapioResumoBll resumo = cardapioBll.CardapioResumo(
                                    Funcoes.Funcoes.ConvertePara.Int(item),
                                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));

                                List<TOExigNutrTabelasBll> listagem =
                                    cardapioAlimentosBll.ListarCardapioExigNutr(
                                        Math.Round(Funcoes.Funcoes.ConvertePara.Decimal(resumo.Energia), 0),
                                        Funcoes.Funcoes.ConvertePara.Int(item),
                                        Funcoes.Funcoes.ConvertePara.Int(ddlTblExigNutr.SelectedValue),
                                        Funcoes.Funcoes.ConvertePara.Int(dadosPacienteTO.IdEspecie),
                                        (int)situacaoDoAnimal,
                                        Funcoes.Funcoes.ConvertePara.Int(ViewState["IdGrupo"]));

                                conjuntoExigNutr.Add(listagem);
                            }

                            retListagem = cardapioAlimentosBll.MediaDosCardapios(conjuntoExigNutr);

                            Session["BalancoDieta"] = cardapioAlimentosBll.GeraRelatorio(
                                (List<TOExigNutrTabelasBll>)Session["BalancoDieta"],
                                retListagem);

                            switch (_tabNutr)
                            {
                                case 1:
                                case 3:
                                    {
                                        rptTblNutr_II.DataSource = null;
                                        rptTblNutr_II.DataBind();
                                        rptTblNutr_II.Visible = false;

                                        rptTabelasNutricionais.Visible = true;
                                        rptTabelasNutricionais.DataSource = retListagem;
                                        rptTabelasNutricionais.DataBind();

                                        break;
                                    }
                                case 2:
                                    {
                                        rptTabelasNutricionais.DataSource = null;
                                        rptTabelasNutricionais.DataBind();
                                        rptTabelasNutricionais.Visible = false;

                                        rptTblNutr_II.Visible = true;
                                        rptTblNutr_II.DataSource = retListagem;
                                        rptTblNutr_II.DataBind();

                                        break;
                                    }
                            }
                        }

                        break;
                    }
            }
        }

        protected void rptTabelasNutricionais_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        TOExigNutrTabelasBll exigNutrTabelasTO =
                            (TOExigNutrTabelasBll)e.Item.DataItem;

                        Label lblValMin = (Label)e.Item.FindControl("lblValMin");
                        Label lblValMax = (Label)e.Item.FindControl("lblValMax");
                        Label lblValEmCardapio = (Label)e.Item.FindControl("lblValEmCardapio");
                        Label lblValFalta = (Label)e.Item.FindControl("lblValFalta");
                        Label lblValSobra = (Label)e.Item.FindControl("lblValSobra");

                        lblValMin.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Minimo) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Minimo) : "");
                        lblValMax.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Maximo) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Maximo) : "");
                        lblValEmCardapio.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.EmCardapio) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.EmCardapio) : "");
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

        protected void rptTblNutr_II_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        TOCardapioBll cardapioTO = (TOCardapioBll)ViewState["CardapioItem01"];
                        Label lblEnergia = (Label)e.Item.FindControl("lblEnergia");

                        lblEnergia.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            cardapioTO.FatorEnergia) > 0 ? string.Format("{0:#,##0.00}",
                            cardapioTO.FatorEnergia) : "");

                        break;
                    }
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        TOExigNutrTabelasBll exigNutrTabelasTO =
                            (TOExigNutrTabelasBll)e.Item.DataItem;

                        Label lblValMin = (Label)e.Item.FindControl("lblValMin");
                        Label lblValMax = (Label)e.Item.FindControl("lblValMax");
                        Label lblValAdeq = (Label)e.Item.FindControl("lblValAdeq");
                        Label lblValRecomend = (Label)e.Item.FindControl("lblValRecomend");
                        Label lblValEmCardapio = (Label)e.Item.FindControl("lblValEmCardapio");
                        Label lblValFalta = (Label)e.Item.FindControl("lblValFalta");
                        Label lblValSobra = (Label)e.Item.FindControl("lblValSobra");

                        lblValMin.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Minimo) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Minimo) : "");
                        lblValMax.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Maximo) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Maximo) : "");
                        lblValAdeq.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Adequado) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Adequado) : "");
                        lblValRecomend.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Recomendado) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Recomendado) : "");
                        lblValEmCardapio.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.EmCardapio) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.EmCardapio) : "");
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

        protected void lbFecharBalCard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cardapio/CardapioSelecao.aspx?perm=" +
                Funcoes.Funcoes.Seguranca.Criptografar("False"));
        }

        private void PopulaTabelaNutricional()
        {
            acGrupos.DataSource = gruposNutrientesBll.Listar(true);
            acGrupos.DataBind();
        }

        private void PopulaTblsExigNutr()
        {
            ddlTblExigNutr.Items.AddRange(exigenciasNutrBll.ListarTabNutr());
            ddlTblExigNutr.DataBind();

            ddlTblExigNutr.Items.Insert(0, new ListItem("--- Selecione ---", "0"));
        }

        protected void lbSalvaReceita_Click(object sender, EventArgs e)
        {
            if (Session["BalancoDieta"] != null)
            {
                int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
                configReceitDcl = configReceitBll.Carregar(_idPessoa);

                if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
                {
                    List<TOExigNutrTabelasBll> _listagemTbls =
                        ((List<TOExigNutrTabelasBll>)Session["BalancoDieta"]).
                            Where(w => w.EmCardapio > 0 || w.Falta > 0).ToList();

                    if (_listagemTbls.Count > 0)
                    {
                        if (ViewState["CardapioItem01"] != null)
                        {
                            Inserir(configReceitDcl, _listagemTbls);
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Warning,
                                "Pelo menos duas Dietas devem fazer parte da Composição!!!",
                                "NutroVET Informa",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Warning,
                            "Não Existem Nutrientes na Composição da Dieta!!!",
                            "NutroVET Informa",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
                    }
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
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Selecione uma Tabela de Exigência Nutricional!!!",
                    "NutroVET Informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
            }
        }

        private void Inserir(ConfigReceituario _configRec, 
            List<TOExigNutrTabelasBll> _listagem)
        {
            bllRetorno insertRet;

            //Cria Objeto da Receita
            receituarioDcl = new DCL.Receituario();
            cardapioTO = (TOCardapioBll)ViewState["CardapioItem01"];
            ArrayList _qtdCardapios = (ArrayList)ViewState["ArrayCardapios"];

            receituarioDcl.IdCardapio = cardapioTO.IdCardapio;
            receituarioDcl.dTpRec = 
                (int)DominiosBll.ReceitasAuxTipos.Balanceamento_de_Dietas;
            receituarioDcl.Titulo = "Balanceamento de Dietas - " +
                ddlTblExigNutr.SelectedItem.Text;
            receituarioDcl.Veiculo = string.Empty;
            receituarioDcl.QuantVeic = string.Empty;
            receituarioDcl.InstrucoesReceita = string.Empty;
            receituarioDcl.Posologia = string.Empty;
            receituarioDcl.Prescricao = string.Empty;
            receituarioDcl.Observacao = ftbObservacao.Text;
            receituarioDcl.DataReceita = DateTime.Today;
            receituarioDcl.dTblExigNutr = Funcoes.Funcoes.ConvertePara.Int(
                ddlTblExigNutr.SelectedItem.Value);
            receituarioDcl.QuantDietas = _qtdCardapios.Count;
            receituarioDcl.LocalReceita = (_configRec != null ? _configRec.Logr_Cidade : "");
            receituarioDcl.NrReceita = receituarioBll.GerarNumeroArquivo();
            receituarioDcl.Arquivo = "RecBalanc_" + receituarioDcl.NrReceita + "_" +
                DateTime.Today.ToString("yy") + ".pdf";

            receituarioDcl.Ativo = true;
            receituarioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            receituarioDcl.DataCadastro = DateTime.Now;
            receituarioDcl.IP = Request.UserHostAddress;

            //Cria Objetos dos Itens da Receita
            foreach (TOExigNutrTabelasBll item in _listagem)
            {
                recNutrDcl = recNutrBll.ConverterBalanceamento(item);

                recNutrDcl.Ativo = true;
                recNutrDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                recNutrDcl.DataCadastro = DateTime.Now;
                recNutrDcl.IP = Request.UserHostAddress;

                receituarioDcl.ReceituarioNutrientes.Add(recNutrDcl);
            }

            insertRet = receituarioBll.Inserir(receituarioDcl);

            if (insertRet.retorno)
            {
                string _url = string.Format(
                    "~/Cardapio/Impressao/RptBalancoCardapio.aspx?" +
                    "_tabela={0}&_idCard={1}&_idRec={2}&_quantDietas={3}",
                    ddlTblExigNutr.SelectedItem.Text, cardapioTO.IdCardapio,
                    receituarioDcl.IdReceita, _qtdCardapios.Count);

                lbImpressaoCardapio.NavigateUrl = _url;
                lbImpressaoCardapio.Enabled = true;
                lbImpressaoCardapio.CssClass = "btn btn-primary-nutrovet";

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Inserção Efetuada com Sucesso!!!",
                    "NutroVET Informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                lbImpressaoCardapio.NavigateUrl = "";
                lbImpressaoCardapio.Enabled = false;
                lbImpressaoCardapio.CssClass = "btn btn-info";

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    insertRet.mensagem,
                    "NutroVET Informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }
    }
}