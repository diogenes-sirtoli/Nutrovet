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
    public partial class Nutrientes : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clNutrientesBll nutriBll = new clNutrientesBll();
        protected Nutriente nutriDcl;
        protected clNutrientesAuxGruposBll gruposBll = new clNutrientesAuxGruposBll();

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
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "1.4.4",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        ViewState["pagAtual"] = 1;
                        ViewState["pagTamanho"] = 10;

                        PopulaGrupos();
                        PopulaUnidade();

                        Paginar(1);
                        Page.Form.DefaultFocus = tbxNutri.ClientID;
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaUnidade()
        {
            ddlUnid.Items.AddRange(nutriBll.ListarUnidades());
            ddlUnid.DataBind();

            ddlUnid.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaGrupos()
        {
            ddlGrupo.DataValueField = "IdGrupo";
            ddlGrupo.DataTextField = "Grupo";
            ddlGrupo.DataSource = gruposBll.Listar();
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaGrid(string _pesquisa, int _pagAtual, int _pagTamanho)
        {
            List<TONutrientesBll> racasListagem = nutriBll.Listar(_pesquisa, false,
                _pagTamanho, _pagAtual);

            rptNutri.DataSource = racasListagem;
            rptNutri.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar(Funcoes.Funcoes.ConvertePara.Int(hfID.Value));

            popUpModal.Show();
        }

        protected void rptNutri_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);
            
            switch (e.CommandName)
            {
                case "inserir":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(0);
                        Cancelar();

                        lblTituloModal.Text = "Inserindo Novo Nutriente";

                        popUpModal.Show();

                        break;
                    }
                case "editar":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(_id);
                        nutriDcl = nutriBll.Carregar(_id);

                        ViewState["_lista"] = Funcoes.Funcoes.ConvertePara.Bool(
                            nutriDcl.ListarEmAlim);
                        tbxNutri.Text = nutriDcl.Nutriente1;
                        Funcoes.Funcoes.ControlForm.SetComboBox(ddlGrupo, 
                            nutriDcl.IdGrupo);
                        Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnid, 
                            nutriDcl.IdUnidade);

                        if (Funcoes.Funcoes.ConvertePara.Bool(nutriDcl.ListarEmAlim))
                        {
                            BtnSim();
                        }
                        else
                        {
                            BtnNao();
                        }

                        lblTituloModal.Text = "Editando Nutriente Existente";

                        popUpModal.Show();

                        break;
                    }
                case "excluir":
                    {
                        hfID.Value = "";
                        Cancelar();

                        Excluir(_id);

                        break;
                    }
            }
        }

        private void Cancelar()
        {
            tbxNutri.Text = "";
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlGrupo, 0);
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnid, 0);
        }

        private void Excluir(int _id)
        {
            //if ((Funcoes.Funcoes.ConvertePara.Bool(Session["Excluir"])) ||
            //    (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
            //{
                nutriDcl = nutriBll.Carregar(_id);
                nutriDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                nutriDcl.Ativo = false;
                nutriDcl.DataCadastro = DateTime.Now;
                nutriDcl.IP = Request.UserHostAddress;

                bllRetorno ret = nutriBll.Excluir(nutriDcl);

                if (ret.retorno)
                {
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
            //}
            //else
            //{
            //    Funcoes.Funcoes.Toastr.ShowToast(this,
            //            Funcoes.Funcoes.Toastr.ToastType.Info,
            //            "Selecione um ITEM na grade para efetuar a EXCLUSÃO!", "Exclusão",
            //            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
            //            true);
            //}
        }

        protected void Salvar(int _id)
        {
            if (_id > 0)
            {
                //if ((Funcoes.Funcoes.ConvertePara.Bool(Session["Alterar"])) ||
                //    (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                //{
                    Alterar(_id);
                //}
                //else
                //{
                //    Funcoes.Funcoes.Toastr.ShowToast(this,
                //        Funcoes.Funcoes.Toastr.ToastType.Warning,
                //        "Você não possui permissão para ALTERAR!",
                //        "Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                //        true);
                //}
            }
            else
            {
                //if ((Funcoes.Funcoes.ConvertePara.Bool(Session["Inserir"])) ||
                //    (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                //{
                    Inserir();
                //}
                //else
                //{
                //    Funcoes.Funcoes.Toastr.ShowToast(this,
                //        Funcoes.Funcoes.Toastr.ToastType.Warning,
                //        "Você não possui permissão para INSERIR!",
                //        "Inserção", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                //        true);
                //}
            }
        }

        private void Inserir()
        {
            nutriDcl = new Nutriente();

            nutriDcl.IdGrupo = Funcoes.Funcoes.ConvertePara.Int(
                ddlGrupo.SelectedValue);
            nutriDcl.Nutriente1 = tbxNutri.Text;
            nutriDcl.IdUnidade = Funcoes.Funcoes.ConvertePara.Int(ddlUnid.Text);
            nutriDcl.ListarEmAlim = Funcoes.Funcoes.ConvertePara.Bool(
                ViewState["_lista"]);

            nutriDcl.Ativo = true;
            nutriDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            nutriDcl.DataCadastro = DateTime.Now;
            nutriDcl.IP = Request.UserHostAddress;

            bllRetorno inserirRet = nutriBll.Inserir(nutriDcl);

            if (inserirRet.retorno)
            {
                Paginar(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                Cancelar();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, inserirRet.mensagem,
                    "Inserção", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                    "Inserção", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Alterar(int _id)
        {
            nutriDcl = nutriBll.Carregar(_id);

            nutriDcl.IdGrupo = Funcoes.Funcoes.ConvertePara.Int(
                ddlGrupo.SelectedValue);
            nutriDcl.Nutriente1 = tbxNutri.Text;
            nutriDcl.IdUnidade = Funcoes.Funcoes.ConvertePara.Int(ddlUnid.Text);
            nutriDcl.ListarEmAlim = Funcoes.Funcoes.ConvertePara.Bool(
                ViewState["_lista"]);

            nutriDcl.Ativo = true;
            nutriDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            nutriDcl.DataCadastro = DateTime.Now;
            nutriDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = nutriBll.Alterar(nutriDcl);

            if (alterarRet.retorno)
            {
                Paginar(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                Cancelar();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, alterarRet.mensagem,
                    "Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, alterarRet.mensagem,
                    "Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void lbPesq_Click(object sender, EventArgs e)
        {
            Paginar(1);
        }

        protected void Paginar(int _nrPag)
        {
            if (_nrPag > 0)
            {
                ViewState["pagTotal"] = nutriBll.TotalPaginas(tbPesq.Text, false,
                        Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
                ViewState["pagAtual"] = _nrPag;

                int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]);
                int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTamanho"]);

                ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

                PopulaGrid(tbPesq.Text, _pagAtual, _pagTamanho);
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

                rptNutri.DataSource = null;
                rptNutri.DataBind();
            }
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
            else if ((_totalPag >= 2) && (_totalPag <= 6))
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
            else if (_totalPag > 6)
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

        protected void ddlPag_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["pagTotal"] = nutriBll.TotalPaginas(tbPesq.Text, false,
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

        protected void btnSim_Click(object sender, EventArgs e)
        {
            BtnSim();

            popUpModal.Show();
        }

        protected void btnNao_Click(object sender, EventArgs e)
        {
            BtnNao();

            popUpModal.Show();
        }

        protected void BtnSim()
        {
            btnSim.CssClass = "btn btn-primary-nutrovet";
            btnNao.CssClass = "btn btn-default";

            ViewState["_lista"] = true;
        }

        protected void BtnNao()
        {
            btnSim.CssClass = "btn btn-default";
            btnNao.CssClass = "btn btn-primary-nutrovet";

            ViewState["_lista"] = false;
        }
    }
}