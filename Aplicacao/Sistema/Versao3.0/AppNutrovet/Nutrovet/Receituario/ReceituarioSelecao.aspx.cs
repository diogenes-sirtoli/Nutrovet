using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;

namespace Nutrovet.Receituario
{
    public partial class ReceituarioSelecao : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;

        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;
        protected clTutoresBll tutoresBll = new clTutoresBll();
        protected clAnimaisBll animaisBll = new clAnimaisBll();
        protected Animai animaisDcl;
        protected TOAcessosVigenciaPlanosBll planoTO;
        protected clAcessosVigenciaPlanosBll planosBll = new clAcessosVigenciaPlanosBll();
        protected AcessosVigenciaPlano planoDcl;
        protected clReceituarioBll receituarioBll = new clReceituarioBll();
        protected DCL.Receituario receituarioDcl;
        protected TOReceituarioBll receituarioTO;

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
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "11.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        if ((Request.QueryString["perm"] != null) &&
                            (Funcoes.Funcoes.Seguranca.Descriptografar(
                                Funcoes.Funcoes.ConvertePara.String(
                                    Request.QueryString["perm"])) != "True"))
                        {
                            Response.Redirect("~/MenuGeral.aspx?perm=" +
                                Funcoes.Funcoes.Seguranca.Criptografar("False"));
                        }
                        else
                        {
                            TOToastr _toastr = (TOToastr)Session["ToastrReceituarios"];
                            Session.Remove("Receituario");

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
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

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

        private void PopulaGrid(int _idTutor, int _idTpRec, int _idAnimal, 
            DateTime? _dtIni, DateTime? _dtFim, int _pagAtual, int _pagTamanho)
        {
            int _idPessoa = (_idTutor > 0 ? 0 : 
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
            
            List<TOReceituarioBll> receitasListagem = receituarioBll.Listar(_idPessoa,
                _idTutor, _idTpRec, _idAnimal, _dtIni, _dtFim, _pagTamanho, _pagAtual);

            rptReceituario.DataSource = receitasListagem;
            rptReceituario.DataBind();
        }

        protected void Paginar(int _nrPag)
        {
            int _idTutor = Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue);
            int _idPessoa = (_idTutor > 0 ? 0 :
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
            int _idTpRec = Funcoes.Funcoes.ConvertePara.Int(ddlTpReceita.SelectedValue);
            int _idAnimal = Funcoes.Funcoes.ConvertePara.Int(ddlAnimais.SelectedValue);
            DateTime? _dtIni = null;
            DateTime? _dtFim = null;

            if (meDtInicial.Text != "")
            {
                _dtIni = DateTime.Parse(meDtInicial.Text);
            }

            if (meDtFinal.Text != "")
            {
                _dtFim = DateTime.Parse(meDtFinal.Text);
            }

            ViewState["pagTotal"] = receituarioBll.TotalPaginas(_idPessoa,
                _idTutor, _idTpRec, _idAnimal, _dtIni, _dtFim,
                Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
            ViewState["pagAtual"] = _nrPag;

            int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]);
            int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTamanho"]);

            ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

            PopulaGrid(_idTutor, _idTpRec, _idAnimal, _dtIni, _dtFim, 
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

            Session.Remove("ToastrReceituarios");
        }

        protected void ddlTutores_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopularAnimais(Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue),
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
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
            int _idTutor = Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue);
            int _idPessoa = (_idTutor > 0 ? 0 :
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
            int _idTpRec = Funcoes.Funcoes.ConvertePara.Int(ddlTpReceita.SelectedValue);
            int _idAnimal = Funcoes.Funcoes.ConvertePara.Int(ddlAnimais.SelectedValue);
            DateTime? _dtIni = null;
            DateTime? _dtFim = null;

            if (meDtInicial.Text != "")
            {
                _dtIni = DateTime.Parse(meDtInicial.Text);
            }

            if (meDtFinal.Text != "")
            {
                _dtFim = DateTime.Parse(meDtFinal.Text);
            }

            ViewState["pagTotal"] = receituarioBll.TotalPaginas(_idPessoa,
                _idTutor, _idTpRec, _idAnimal, _dtIni, _dtFim,
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

        protected void rptReceituario_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);
            receituarioDcl = receituarioBll.Carregar(_id);

            switch (e.CommandName)
            {
                case "editar":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(_id);

                        switch (receituarioDcl.dTpRec)
                        {
                            case 1:
                                {
                                    Response.Redirect("~/Receituario/RecSuplementUpdt.aspx?_idReceita=" +
                                        Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));

                                    break;
                                }
                            case 2:
                                {
                                    Response.Redirect("~/Receituario/RecNutraceuticosUpdt.aspx?_idReceita=" +
                                        Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));

                                    break;
                                }
                            case 3:
                                {
                                    Response.Redirect("~/Receituario/RecBrancoUpdt.aspx?_idReceita=" +
                                        Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));

                                    break;
                                }
                            case 4:
                                {
                                    Response.Redirect("~/Receituario/RecBalanceamUpdt.aspx?_idReceita=" +
                                        Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));

                                    break;
                                }
                        }

                        break;
                    }
            }
        }

        protected void rptReceituario_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            bool _severLocal = Conexao.ServidorLocal();
            string _path = (_severLocal ? "~/PrintFiles/Avaliacao/Receitas/" :
                "~/PrintFiles/Producao/Receitas/");

            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        HyperLink hlVisualizar = (HyperLink)e.Item.FindControl("hlVisualizar");
                        Label lbDataReceitaReg = (Label)e.Item.FindControl("lbDataReceitaReg");

                        receituarioTO =(TOReceituarioBll)e.Item.DataItem;

                        bool _existeFile = receituarioBll.ExisteArquivoReceita(
                            receituarioTO.Arquivo);

                        lbDataReceitaReg.Text = string.Format("{0:d}", receituarioTO.DataReceita);
                        hlVisualizar.NavigateUrl = (_existeFile ? _path + receituarioTO.Arquivo : "");
                        hlVisualizar.CssClass = (_existeFile ? "btn btn-warning btn-xs" :
                            "btn btn-secondary btn-xs");
                        hlVisualizar.ToolTip = "Visualizar Receita";

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        protected void lbPesq_Click(object sender, EventArgs e)
        {
            Paginar(1);
        }
    }
}