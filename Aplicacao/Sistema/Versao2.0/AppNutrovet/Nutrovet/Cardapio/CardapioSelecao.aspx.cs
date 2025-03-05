using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;

namespace Nutrovet.Cardapio
{
    public partial class CardapioSelecao : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;

        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;
        protected clTutoresBll tutoresBll = new clTutoresBll();
        protected clAnimaisBll animaisBll = new clAnimaisBll();
        protected Animai animaisDcl;
        protected clCardapioAlimentosBll cardapioAlimentosBll = new
            clCardapioAlimentosBll();
        protected CardapiosAlimento cardapioAlimentosDcl;
        protected TOCardapioResumoBll cardapioAlimentosTO;


        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/AppMenuGeral.Master";
        }

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
                        TOToastr _toastr = (TOToastr)Session["ToastrCardapios"];
                        Session.Remove("BalancoDieta");

                        if (_toastr != null)
                        {
                            MostraToastr(_toastr);
                        }

                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        ViewState["pagAtual"] = 1;
                        ViewState["pagTamanho"] = 10;

                        PopularTutor(Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name));

                        Paginar(1);
                        Page.Form.DefaultFocus = ddlTutor.ClientID;
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        //Verificar
        private void PopularTutor(int _idCliente)
        {
            List<TOTutoresBll> listagem = tutoresBll.Listar(true, _idCliente);

            ddlTutor.DataTextField = "Tutor";
            ddlTutor.DataValueField = "IdTutor";
            ddlTutor.DataSource = listagem;
            ddlTutor.DataBind();

            ddlTutor.Items.Insert(0, new ListItem("- Selecione -", "0"));
            ddlAnimais.Items.Insert(0, new ListItem("- Selecione -", "0"));
        }

        private void PopularAnimais(int _idTutor, int _idCliente)
        {
            ddlAnimais.DataTextField = "Animal";
            ddlAnimais.DataValueField = "IdAnimal";
            ddlAnimais.DataSource = animaisBll.Listar(_idTutor, _idCliente,
                DominiosBll.ListarAnimaisPor.Tutor);
            ddlAnimais.DataBind();

            ddlAnimais.Items.Insert(0, new ListItem("- Selecione -", "0"));
        }

        private void PopulaGrid(int _usuario, int _idTutor, int _idAnimal, decimal _fator,
            int _pagAtual, int _pagTamanho)
        {
            List<TOCardapioBll> cardapioListagem = cardapioBll.Listar(_usuario,
                _idTutor, _idAnimal, _fator, _pagTamanho, _pagAtual);

            ViewState["TotalItens"] = cardapioListagem.Count;

            rptCardapios.DataSource = cardapioListagem;
            rptCardapios.DataBind();
        }

        protected void Paginar(int _nrPag)
        {
            int _fator = Funcoes.Funcoes.ConvertePara.Int(ViewState["fator"]);

            ViewState["pagTotal"] = cardapioBll.TotalPaginas(
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
                Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue),
                Funcoes.Funcoes.ConvertePara.Int(ddlAnimais.SelectedValue), _fator,
                Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
            ViewState["pagAtual"] = _nrPag;

            int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]);
            int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTamanho"]);

            ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

            PopulaGrid(Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
                Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue),
                Funcoes.Funcoes.ConvertePara.Int(ddlAnimais.SelectedValue), _fator,
                _pagAtual, _pagTamanho);
        }

        private void MostraToastr(TOToastr toastr)
        {
            switch (toastr.Tipo)
            {
                case 'E':
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Error, toastr.Mensagem,
                            toastr.Titulo, Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);

                        break;
                    }
                case 'I':
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Info, toastr.Mensagem,
                            toastr.Titulo, Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);

                        break;
                    }
                case 'S':
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Success, toastr.Mensagem,
                            toastr.Titulo, Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);

                        break;
                    }
                case 'W':
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Warning, toastr.Mensagem,
                            toastr.Titulo, Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);

                        break;
                    }
            }

            Session.Remove("ToastrCardapios");
        }

        protected void rptCardapios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "inserir":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(0);

                        Response.Redirect("~/Cardapio/CardapioCadastro.aspx");

                        break;
                    }
                case "editar":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(_id);

                        Response.Redirect("~/Cardapio/CardapioCadastro.aspx?_idCardapio=" +
                            Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));


                        break;
                    }
                case "excluir":
                    {
                        hfID.Value = "";

                        Excluir(_id);

                        break;
                    }
                case "copiar":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(_id);

                        Copiar(_id);

                        break;
                    }
            }
        }

        private void Copiar(int _id)
        {
            DCL.Cardapio cardapioCopia = new DCL.Cardapio();
            List<CardapiosAlimento> listagemCopia = new List<CardapiosAlimento>();
            CardapiosAlimento itemCopia;
            LimpaFiltro();

            cardapioTO = cardapioBll.CarregarTO(_id);
            List<TOCardapioAlimentosBll> listaAlimentos =
                cardapioAlimentosBll.ListarTO(_id);

            cardapioCopia.IdPessoa = Funcoes.Funcoes.ConvertePara.Int(
                cardapioTO.IdTutor);
            cardapioCopia.Descricao = cardapioTO.Descricao;
            cardapioCopia.DtCardapio = DateTime.Today.Date;
            cardapioCopia.FatorEnergia = cardapioTO.FatorEnergia;
            cardapioCopia.Descricao = "Cópia - " +
                (Funcoes.Funcoes.ConvertePara.String(cardapioTO.Descricao) != "" ?
                    cardapioTO.Descricao : cardapioTO.Animal);
            //cardapioCopia.Gestante = cardapioDcl.Gestante;
            //cardapioCopia.Lactante = cardapioDcl.Lactante;
            //cardapioCopia.LactacaoSemanas = cardapioDcl.LactacaoSemanas;
            //cardapioCopia.NrFilhotes = cardapioDcl.NrFilhotes;
            //cardapioCopia.IdDieta = cardapioTO.IdDieta;
            cardapioCopia.EmCardapio = cardapioTO.EmCardapio;
            cardapioCopia.Observacao = cardapioTO.Observacao;

            cardapioCopia.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            cardapioCopia.Ativo = cardapioTO.Ativo;
            cardapioCopia.DataCadastro = DateTime.Now;
            cardapioCopia.IP = Request.UserHostAddress;

            foreach (var item in listaAlimentos)
            {
                itemCopia = new CardapiosAlimento();

                itemCopia.IdAlimento = item.IdAlimento;
                itemCopia.Quant = item.Quant;

                itemCopia.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
                itemCopia.Ativo = cardapioTO.Ativo;
                itemCopia.DataCadastro = DateTime.Now;
                itemCopia.IP = Request.UserHostAddress;

                listagemCopia.Add(itemCopia);
            }

            cardapioCopia.CardapiosAlimentos.AddRange(listagemCopia);

            bllRetorno inserirRet = cardapioBll.Inserir(cardapioCopia, true);

            if (inserirRet.retorno)
            {
                ViewState["_idCardapio"] = cardapioTO.IdCardapio;
                ViewState.Remove("fator");

                Paginar(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Inserção Efetuada com Sucesso !",
                    "NutroVET informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    inserirRet.mensagem,
                    "NutroVET informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Excluir(int _id)
        {
            cardapioDcl = cardapioBll.Carregar(_id);
            cardapioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            cardapioDcl.Ativo = false;
            cardapioDcl.DataCadastro = DateTime.Now;
            cardapioDcl.IP = Request.UserHostAddress;

            bllRetorno ret = cardapioBll.Excluir(cardapioDcl);

            if (ret.retorno)
            {
                ViewState.Remove("fator");
                Paginar(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    ret.mensagem, "Exclusão",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    ret.mensagem, "Exclusão",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void lbInserir_Click(object sender, EventArgs e)
        {
            hfID.Value = Funcoes.Funcoes.ConvertePara.String(0);

            Response.Redirect("~/Cardapio/CardapioCadastro.aspx");
        }

        private void LimpaFiltro()
        {
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlTutor, 0);
            ddlAnimais.Items.Clear();
            ddlAnimais.Items.Insert(0, new ListItem("- Selecione -", "0"));
        }

        protected void ddlTutores_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState.Remove("fator");

            PopularAnimais(Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue),
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));

            Paginar(1);
        }

        private void ExibeBotoes(int _totalPag)
        {
            if (_totalPag <= 1)
            {
                lbPagPrimeira.Visible = false;
                lbAnt.Visible = false;
                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lbPost.Visible = false;
                lbPagUltima.Visible = false;
            }
            else if ((_totalPag >= 2) && (_totalPag < 6))
            {
                lbPagPrimeira.Visible = true;
                lbAnt.Visible = true;
                lb1.Visible = true;
                lb2.Visible = (_totalPag < 2 ? false : true);
                lb3.Visible = (_totalPag < 3 ? false : true);
                lb4.Visible = (_totalPag < 4 ? false : true);
                lb5.Visible = (_totalPag < 5 ? false : true);
                lbPost.Visible = false;
                lbPagUltima.Visible = false;
            }
            else if (_totalPag >= 6)
            {
                lbPagPrimeira.Visible = true;
                lbAnt.Visible = true;
                lb1.Visible = true;
                lb2.Visible = true;
                lb3.Visible = true;
                lb4.Visible = true;
                lb5.Visible = true;
                lbPost.Visible = true;
                lbPagUltima.Visible = true;
            }
        }

        protected void ddlPag_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _fator = Funcoes.Funcoes.ConvertePara.Int(ViewState["fator"]);

            ViewState["pagTotal"] = cardapioBll.TotalPaginas(
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
                Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue),
                Funcoes.Funcoes.ConvertePara.Int(ddlAnimais.SelectedValue), _fator,
                Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
            ViewState["pagAtual"] = 1;
            ViewState["pagTamanho"] = Funcoes.Funcoes.ConvertePara.Int(
                ddlPag.SelectedValue);

            lb1.CommandName = Funcoes.Funcoes.ConvertePara.String(1);
            lb1.Text = "<b><u>" + lb1.CommandName + "</u></b>";

            lb2.CommandName = Funcoes.Funcoes.ConvertePara.String(2);
            lb2.Text = lb2.CommandName;

            lb3.CommandName = Funcoes.Funcoes.ConvertePara.String(3);
            lb3.Text = lb3.CommandName;

            lb4.CommandName = Funcoes.Funcoes.ConvertePara.String(4);
            lb4.Text = lb4.CommandName;

            lb5.CommandName = Funcoes.Funcoes.ConvertePara.String(5);
            lb5.Text = lb5.CommandName;

            Paginar(1);
        }

        protected void lbPagPrimeira_Click(object sender, EventArgs e)
        {
            lbAnt.Visible = false;
            lbPost.Visible = true;

            lb1.CommandName = Funcoes.Funcoes.ConvertePara.String(1);
            lb1.Text = "<b><u>" + lb1.CommandName + "</u></b>";

            lb2.CommandName = Funcoes.Funcoes.ConvertePara.String(2);
            lb2.Text = lb2.CommandName;

            lb3.CommandName = Funcoes.Funcoes.ConvertePara.String(3);
            lb3.Text = lb3.CommandName;

            lb4.CommandName = Funcoes.Funcoes.ConvertePara.String(4);
            lb4.Text = lb4.CommandName;

            lb5.CommandName = Funcoes.Funcoes.ConvertePara.String(5);
            lb5.Text = lb5.CommandName;

            Paginar(1);
        }

        protected void lbPagUltima_Click(object sender, EventArgs e)
        {
            lbAnt.Visible = true;
            lbPost.Visible = false;

            int _pagTotal = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]);

            lb1.CommandName = Funcoes.Funcoes.ConvertePara.String(_pagTotal - 4);
            lb1.Text = lb1.CommandName;

            lb2.CommandName = Funcoes.Funcoes.ConvertePara.String(_pagTotal - 3);
            lb2.Text = lb2.CommandName;

            lb3.CommandName = Funcoes.Funcoes.ConvertePara.String(_pagTotal - 2);
            lb3.Text = lb3.CommandName;

            lb4.CommandName = Funcoes.Funcoes.ConvertePara.String(_pagTotal - 1);
            lb4.Text = lb4.CommandName;

            lb5.CommandName = Funcoes.Funcoes.ConvertePara.String(_pagTotal);
            lb5.Text = "<b><u>" + lb5.CommandName + "</u></b>";

            Paginar(_pagTotal);
        }

        protected void lb1_Click(object sender, EventArgs e)
        {
            lb1.Text = "<b><u>" + lb1.CommandName + "</u></b>";
            lb2.Text = lb2.CommandName;
            lb3.Text = lb3.CommandName;
            lb4.Text = lb4.CommandName;
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName));
        }

        protected void lb2_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = "<b><u>" + lb2.CommandName + "</u></b>";
            lb3.Text = lb3.CommandName;
            lb4.Text = lb4.CommandName;
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb2.CommandName));
        }

        protected void lb3_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = lb2.CommandName;
            lb3.Text = "<b><u>" + lb3.CommandName + "</u></b>";
            lb4.Text = lb4.CommandName;
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb3.CommandName));
        }

        protected void lb4_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = lb2.CommandName;
            lb3.Text = lb3.CommandName;
            lb4.Text = "<b><u>" + lb4.CommandName + "</u></b>";
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb4.CommandName));
        }

        protected void lb5_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = lb2.CommandName;
            lb3.Text = lb3.CommandName;
            lb4.Text = lb4.CommandName;
            lb5.Text = "<b><u>" + lb5.CommandName + "</u></b>";

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName));
        }

        protected void lbPost_Click(object sender, EventArgs e)
        {
            lbAnt.Visible = true;

            if (Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName) + 1 <=
                Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]))
            {
                lb1.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName) + 1);
                lb1.Text = lb1.CommandName;

                lb2.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb2.CommandName) + 1);
                lb2.Text = lb2.CommandName;

                lb3.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb3.CommandName) + 1);
                lb3.Text = lb3.CommandName;

                lb4.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb4.CommandName) + 1);
                lb4.Text = lb4.CommandName;

                lb5.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName) + 1);
                lb5.Text = "<b><u>" + lb5.CommandName + "</u></b>";
            }
            else
            {
                lbPost.Enabled = false;
            }

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName));
        }

        protected void lbAnt_Click(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName) > 1)
            {
                lb1.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName) - 1);
                lb1.Text = "<b><u>" + lb1.CommandName + "</u></b>";

                lb2.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb2.CommandName) - 1);
                lb2.Text = lb2.CommandName;

                lb3.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb3.CommandName) - 1);
                lb3.Text = lb3.CommandName;

                lb4.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb4.CommandName) - 1);
                lb4.Text = lb4.CommandName;

                lb5.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName) - 1);
                lb5.Text = lb5.CommandName;
            }
            else
            {
                lbAnt.Visible = false;
            }

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName));
        }

        protected void rptCardapios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        cardapioTO = (TOCardapioBll)e.Item.DataItem;
                        DropDownList ddlFatorEnergiaBal = (DropDownList)e.Item.FindControl(
                            "ddlFatorEnergiaBal");

                        Label lblFatorEnergiaBalanco = (Label)e.Item.FindControl("lblFatorEnergiaBalanco");
                        //Label lblCbxBalDiet = (Label)e.Item.FindControl("lblCbxBalDiet"); 
                        if ((Funcoes.Funcoes.ConvertePara.Int(ddlAnimais.SelectedValue) > 0) &&
                            (Funcoes.Funcoes.ConvertePara.Int(ViewState["TotalItens"]) > 1))
                        {
                            List<TOCardapioBll> _fatores = cardapioBll.FiltroFatorEnergia(
                                Funcoes.Funcoes.ConvertePara.Int(ddlAnimais.SelectedValue));
                            int _fator = Funcoes.Funcoes.ConvertePara.Int(ViewState["fator"]);

                            ddlFatorEnergiaBal.Visible = true;
                            lblFatorEnergiaBalanco.Visible = false;
                            //lblCbxBalDiet.Visible = true;

                            ddlFatorEnergiaBal.DataTextField = "FatorEnergia";
                            ddlFatorEnergiaBal.DataValueField = "FatorEnergia";
                            ddlFatorEnergiaBal.DataSource = _fatores;
                            ddlFatorEnergiaBal.DataBind();

                            ddlFatorEnergiaBal.Items.Insert(0, new ListItem("Fator", "0"));

                            Funcoes.Funcoes.ControlForm.SetComboBox(ddlFatorEnergiaBal, _fator);
                        }
                        else
                        {
                            ddlFatorEnergiaBal.Visible = false;
                            lblFatorEnergiaBalanco.Visible = true;
                            //lblCbxBalDiet.Visible = false;
                        }

                        break;
                    }
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        CheckBox cbxBaldiet = (CheckBox)e.Item.FindControl("cbxBaldiet");
                        Label lblDataCardapio = (Label)e.Item.FindControl("lblDataCardapio");
                        LinkButton lbCopiar = (LinkButton)e.Item.FindControl("lbCopiar");
                        LinkButton lbExcluir = (LinkButton)e.Item.FindControl("lbExcluir");
                        Label lblFator = (Label)e.Item.FindControl("lblFator");
                        int _fator = Funcoes.Funcoes.ConvertePara.Int(ViewState["fator"]);

                        cardapioTO = (TOCardapioBll)e.Item.DataItem;

                        lblDataCardapio.Text = cardapioTO.DtCardapio.ToString("dd/MM/yyyy");
                        lblFator.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            cardapioTO.FatorEnergia) > 0 ? string.Format("{0:#,##0}",
                            cardapioTO.FatorEnergia) : "0");

                        if (Funcoes.Funcoes.ConvertePara.Int(cardapioTO.IdAnimal) <= 0)
                        {
                            lbCopiar.Visible = false;
                            lbExcluir.Visible = false;
                        }
                        else
                        {
                            lbCopiar.Visible = true;
                            lbExcluir.Visible = true;
                        }

                        if ((Funcoes.Funcoes.ConvertePara.Int(ddlAnimais.SelectedValue) > 0) &&
                            (Funcoes.Funcoes.ConvertePara.Int(ViewState["TotalItens"]) > 1))
                        {
                            cbxBaldiet.Visible = true;
                            cbxBaldiet.Checked = ((_fator > 0) &&
                                (cardapioTO.FatorEnergia == _fator) ? true : false);

                            lbtnBalDieta.CssClass = "btn btn-sm btn-primary-nutrovet m-t-n-xs";
                            lbtnBalDieta.Attributes.Remove("disabled");
                            lbtnBalDieta.Enabled = true;
                        }
                        else
                        {
                            cbxBaldiet.Visible = false;
                            lbtnBalDieta.CssClass = "btn btn-sm btn-primary-nutrovet m-t-n-xs";
                            lbtnBalDieta.Attributes["disabled"] = "disabled";
                            lbtnBalDieta.Enabled = false;
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        protected void ddlAnimais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState.Remove("fator");

            Paginar(1);


            if ((Funcoes.Funcoes.ConvertePara.Int(ddlAnimais.SelectedValue) > 0) &&
                (Funcoes.Funcoes.ConvertePara.Int(ViewState["TotalItens"]) > 1))
            {
                lbtnBalDieta.CssClass = "btn btn-sm btn-primary-nutrovet m-t-n-xs";
                lbtnBalDieta.Enabled = true;
            }
            else
            {
                lbtnBalDieta.CssClass = "btn btn-sm btn-primary-nutrovet m-t-n-xs";
                lbtnBalDieta.Enabled = false;
            }
        }

        protected void btnBalDieta_Click(object sender, EventArgs e)
        {
            if ((ItensChecked() > 1) && (FatorSelecionado()))
            {
                ArrayList _idCardapios = new ArrayList();

                foreach (RepeaterItem item in rptCardapios.Items)
                {
                    CheckBox cbxBaldiet = (CheckBox)item.FindControl("cbxBaldiet");
                    HiddenField hfIdCardapio = (HiddenField)item.FindControl("hfIdCardapio");

                    if (cbxBaldiet.Checked)
                    {
                        if (Funcoes.Funcoes.ConvertePara.Int(hfIdCardapio.Value) > 0)
                        {
                            _idCardapios.Add(Funcoes.Funcoes.ConvertePara.Int(hfIdCardapio.Value));
                        }
                    }
                }

                Session["ArrayCardapios"] = _idCardapios;

                Response.Redirect("~/Cardapio/CardapioBalanceamento.aspx");
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this, Funcoes.Funcoes.Toastr.ToastType.Warning,
                "No Mínimo DOIS Itens devem ser marcados, e o Filtro do FATOR deve ser selecionado!!!",
                "Nutrovet - Informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                true);
            }
        }

        private int ItensChecked()
        {
            int _count = 0;

            foreach (RepeaterItem item in rptCardapios.Items)
            {
                CheckBox cbxBaldiet = (CheckBox)item.FindControl("cbxBaldiet");

                if (cbxBaldiet.Checked)
                {
                    _count += 1;
                }
            }

            return _count;
        }

        private bool FatorSelecionado()
        {
            int _count = 0;

            DropDownList ddlFatorEnergiaBal = rptCardapios.Controls[0].Controls[0].FindControl(
                "ddlFatorEnergiaBal") as DropDownList;

            _count = Funcoes.Funcoes.ConvertePara.Int(ddlFatorEnergiaBal.SelectedValue);

            return Funcoes.Funcoes.ConvertePara.Bool(_count);
        }

        protected void ddlFatorEnergiaBal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;

            ViewState["fator"] = Funcoes.Funcoes.ConvertePara.Int(ddl.SelectedValue);

            Paginar(1);
        }
    }
}