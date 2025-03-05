using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;

namespace Nutrovet.Administracao
{
    public partial class NutraceuticosSelecao : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clNutraceuticosBll nutraBll = new clNutraceuticosBll();
        protected Nutraceutico nutraDcl;
        protected TONutraceuticosBll nutraTO;
        protected clAnimaisAuxEspeciesBll especiesBll = new 
            clAnimaisAuxEspeciesBll();

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
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "9.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        TOToastr _toastr = (TOToastr)Session["ToastrNutraceuticos"];
                        int _idEspecie = Funcoes.Funcoes.ConvertePara.Int(
                            Session["especieSelecao"]);

                        if (_toastr != null)
                        {
                            MostraToastr(_toastr);
                        }

                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        ViewState["pagAtual"] = 1;
                        ViewState["pagTamanho"] = 10;

                        PopulaEspecie();

                        if (_idEspecie > 0)
                        {
                            Funcoes.Funcoes.ControlForm.SetComboBox(ddlEspecie, 
                                _idEspecie);
                            Paginar(1);
                        }
                        else
                        {
                            Paginar(0);
                        }

                        Page.Form.DefaultFocus = tbPesq.ClientID;
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        protected void lbPesq_Click(object sender, EventArgs e)
        {
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

        protected void Paginar(int _nrPag)
        {
            if (_nrPag > 0)
            {
                ViewState["pagTotal"] = nutraBll.TotalPaginas(tbPesq.Text,
                        Funcoes.Funcoes.ConvertePara.Int(ddlEspecie.SelectedValue),
                        Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
                ViewState["pagAtual"] = _nrPag;

                int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["pagAtual"]);
                int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["pagTamanho"]);

                ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

                PopulaGrid(tbPesq.Text, Funcoes.Funcoes.ConvertePara.Int(
                    ddlEspecie.SelectedValue), _pagAtual, _pagTamanho);
            }
            else
            {
                ViewState["pagTotal"] = 0;
                ViewState["pagAtual"] = _nrPag;

                int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["pagAtual"]);
                int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["pagTamanho"]);

                ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

                rptListNutra.DataSource = null;
                rptListNutra.DataBind();
            }
        }

        private void PopulaGrid(string _pesquisa, int _idEsp, int _pagAtual,
            int _pagTamanho)
        {
            List<TONutraceuticosBll> racasListagem = nutraBll.Listar(_pesquisa,
                _idEsp, _pagTamanho, _pagAtual);

            rptListNutra.DataSource = racasListagem;
            rptListNutra.DataBind();
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

        private void PopulaEspecie()
        {
            ddlEspecie.DataValueField = "Id";
            ddlEspecie.DataTextField = "Nome";
            ddlEspecie.DataSource = especiesBll.Listar();
            ddlEspecie.DataBind();

            ddlEspecie.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        protected void ddlEspecie_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _pagina = (Funcoes.Funcoes.ConvertePara.Int(
                ddlEspecie.SelectedValue) > 0 ? 1 : 0);

            Session["especieSelecao"] = Funcoes.Funcoes.ConvertePara.Int(
                ddlEspecie.SelectedValue);

            Paginar(_pagina);
        }

        private void MostraToastr(TOToastr toastr)
        {
            switch (toastr.Tipo)
            {
                case 'E':
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Error, toastr.Mensagem,
                            toastr.Titulo, 
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);

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

            Session.Remove("ToastrNutraceuticos");
        }

        protected void rptListNutra_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "inserir":
                    {
                        Response.Redirect(
                            "~/Administracao/NutraceuticosCadastro.aspx?_idEspecie=" +
                                Funcoes.Funcoes.Seguranca.Criptografar(
                                    ddlEspecie.SelectedValue));

                        break;
                    }
                case "editar":
                    {
                        Response.Redirect(
                            "~/Administracao/NutraceuticosCadastro.aspx?_idNutrac=" +
                            Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()) +
                            "&_idEspecie=" + Funcoes.Funcoes.Seguranca.Criptografar(
                                ddlEspecie.SelectedValue));

                        break;
                    }
                case "excluir":
                    {
                        hfID.Value = "";

                        Excluir(_id);

                        break;
                    }
            }
        }

        private void Excluir(int _id)
        {
            nutraDcl = nutraBll.Carregar(_id);

            nutraDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            nutraDcl.Ativo = false;
            nutraDcl.DataCadastro = DateTime.Now;
            nutraDcl.IP = Request.UserHostAddress;

            bllRetorno ret = nutraBll.Excluir(nutraDcl);

            if (ret.retorno)
            {
                Paginar(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    ret.mensagem, "NutroVET informa - Exclusão",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    ret.mensagem, "NutroVET informa - Exclusão",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void rptListNutra_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        nutraTO = (TONutraceuticosBll)e.Item.DataItem;
                        Label lblDoseMinima = (Label)e.Item.FindControl(
                            "lblDoseMinima");
                        Label lblDoseMaxima = (Label)e.Item.FindControl(
                            "lblDoseMaxima");

                        lblDoseMinima.Text = string.Format("{0:0.0}", 
                            (nutraTO.DoseMin != null ? nutraTO.DoseMin.Value : 0));
                        lblDoseMaxima.Text = string.Format("{0:0.0}",
                            (nutraTO.DoseMax != null ? nutraTO.DoseMax.Value : 0));

                        break;
                    }
            }
        }

        protected void ddlPag_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["pagTotal"] = nutraBll.TotalPaginas(tbPesq.Text,
                Funcoes.Funcoes.ConvertePara.Int(ddlEspecie.SelectedValue),
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
    }
}