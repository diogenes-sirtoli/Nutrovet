using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;


namespace Nutrovet.TabAux
{
    public partial class NutrientesAuxGrupos : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clNutrientesAuxGruposBll gruposBll = new clNutrientesAuxGruposBll();
        protected NutrientesAuxGrupo gruposDcl;

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
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "1.4.3",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        ViewState["pagAtual"] = 1;
                        ViewState["pagTamanho"] = 10;

                        Paginar(1);
                        Page.Form.DefaultFocus = tbxGrupo.ClientID;
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaGrid(string _pesquisa, int _pagAtual, int _pagTamanho)
        {
            List<NutrientesAuxGrupo> gruposListagem = gruposBll.Listar(_pesquisa, 
                _pagTamanho, _pagAtual);

            rptListagem.DataSource = gruposListagem;
            rptListagem.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar(Funcoes.Funcoes.ConvertePara.Int(hfID.Value));
        }

        protected void rptListagem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "inserir":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(0);
                        Cancelar();

                        lblTituloModal.Text = "Inserindo novo Grupo";

                        popUpModal.Show();

                        break;
                    }
                case "editar":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(_id);
                        gruposDcl = gruposBll.Carregar(_id);

                        tbxGrupo.Text = gruposDcl.Grupo;
                        meOrdem.Text = Funcoes.Funcoes.ConvertePara.String(
                            gruposDcl.Ordem);

                        if (Funcoes.Funcoes.ConvertePara.Bool(gruposDcl.ListarEmAlim))
                        {
                            BtnSim();
                        }
                        else
                        {
                            BtnNao();
                        }

                        ViewState["_lista"] = Funcoes.Funcoes.ConvertePara.Bool(
                            gruposDcl.ListarEmAlim);

                        lblTituloModal.Text = "Editando Grupo de Nutriente";

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
            tbxGrupo.Text = "";
            meOrdem.Text = "";
            ViewState.Remove("_lista");
        }

        private void Excluir(int _id)
        {
            //if ((Funcoes.Funcoes.ConvertePara.Bool(Session["Excluir"])) ||
            //    (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
            //{
                gruposDcl = gruposBll.Carregar(_id);
                gruposDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                gruposDcl.Ativo = false;
                gruposDcl.DataCadastro = DateTime.Now;
                gruposDcl.IP = Request.UserHostAddress;

                bllRetorno ret = gruposBll.Excluir(gruposDcl);

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
            gruposDcl = new NutrientesAuxGrupo();

            gruposDcl.Grupo = tbxGrupo.Text;
            gruposDcl.Ordem = Funcoes.Funcoes.ConvertePara.Int(meOrdem.Text);
            gruposDcl.ListarEmAlim = Funcoes.Funcoes.ConvertePara.Bool(
                ViewState["_lista"]);
            
            gruposDcl.Ativo = true;
            gruposDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            gruposDcl.DataCadastro = DateTime.Now;
            gruposDcl.IP = Request.UserHostAddress;

            bllRetorno inserirRet = gruposBll.Inserir(gruposDcl);

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
            gruposDcl = gruposBll.Carregar(_id);

            gruposDcl.Grupo = tbxGrupo.Text;
            gruposDcl.Ordem = Funcoes.Funcoes.ConvertePara.Int(meOrdem.Text);
            gruposDcl.ListarEmAlim = Funcoes.Funcoes.ConvertePara.Bool(
                ViewState["_lista"]);

            gruposDcl.Ativo = true;
            gruposDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            gruposDcl.DataCadastro = DateTime.Now;
            gruposDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = gruposBll.Alterar(gruposDcl);

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
                ViewState["pagTotal"] = gruposBll.TotalPaginas(tbPesq.Text,
                        Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
                ViewState["pagAtual"] = _nrPag;

                int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["pagAtual"]);
                int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["pagTamanho"]);

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

                rptListagem.DataSource = null;
                rptListagem.DataBind();
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
            ViewState["pagTotal"] = gruposBll.TotalPaginas(tbPesq.Text,
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