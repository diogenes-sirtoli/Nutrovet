using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using System.Web.Security;

namespace Nutrovet.TabAux
{
    public partial class Tela3 : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Pessoa pessoasDcl;
        protected clTela3Bll tela3Bll = new clTela3Bll();
        protected TOTela3Bll tela3To;
        protected ListItemCollection validacaoTela;

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
                string _tela = Funcoes.Funcoes.Seguranca.Descriptografar(
                    Funcoes.Funcoes.ConvertePara.String(Request.QueryString["Tela"]));

                ViewState["Tela"] = _tela;
                validacaoTela = tela3Bll.PopulaTitulos(Funcoes.Funcoes.ConvertePara.String(
                    ViewState["Tela"]));

                if (acessosBll.Permissao(Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name),
                    validacaoTela.FindByText("PermTela").Value,
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        PopulaTela(validacaoTela);
                        Page.Form.DefaultFocus = tbCampo.ClientID;
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" + 
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaTela(ListItemCollection _tela)
        {
            string _nrTela = Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]);

            
            lblTitulo.Text = retornarTituloTela3();


            lblTituloBread.Text = _tela.FindByText("Titulo").Value;


            tbPesq.Attributes.Add("placeholder", "Pesquisar por " + retornarTituloTela3());

            ViewState["pagAtual"] = 1;
            ViewState["pagTamanho"] = 10;

            Paginar(_nrTela, 1);
        }

        private void PopulaGrid(string _nrTela, string _pesquisa, int _pagAtual, 
            int _pagTamanho)
        {
            rptListagem.DataSource = tela3Bll.Listar(_nrTela, _pesquisa, 
                _pagTamanho, _pagAtual);
            rptListagem.DataBind();
        }

        protected void Paginar(string _nrTela, int _nrPag)
        {
            ViewState["pagTotal"] = tela3Bll.TotalPaginas(_nrTela, tbPesq.Text,
                Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
            ViewState["pagAtual"] = _nrPag;

            int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]);
            int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTamanho"]);

            ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

            PopulaGrid(_nrTela, tbPesq.Text, _pagAtual, _pagTamanho);
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

        protected void lbPesq_Click(object sender, EventArgs e)
        {
            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), 1);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string camppo = tbCampo.Text;
            Salvar(Funcoes.Funcoes.ConvertePara.Int(hfID.Value));
        }

        protected void rptListagem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            validacaoTela = tela3Bll.PopulaTitulos(Funcoes.Funcoes.ConvertePara.String(
                    ViewState["Tela"]));
            Label lblNome = (Label) e.Item.FindControl("lblNome");
            Label lblSigla = (Label)e.Item.FindControl("lblSigla");

            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "inserir":
                    {
                        lblTituloModal.Text = "Inserindo " + retornarTituloTela3(); 

                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(0);
                        tbCampo.Text = "";

                        if (validacaoTela.FindByText("Sigla").Value != "sem sigla")
                        {
                            lblSiglaModal.Text = "";
                            lblSiglaModal.Text = validacaoTela.FindByText("Sigla").Value;
                            tbSigla.Text = "";
                            tbSigla.Attributes["placeholder"] =
                                validacaoTela.FindByText("Sigla").Value;
                            mostraSigla.Visible = true;
                        }

                        popUpModal.Show();

                        break;
                    }
                case "editar":
                    {

                        lblTituloModal.Text = "Alterando " + retornarTituloTela3();

                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(_id);
                        tbCampo.Text = lblNome.Text;

                        if (validacaoTela.FindByText("Sigla").Value != "sem sigla")
                        {
                            lblSiglaModal.Text = "";
                            lblSiglaModal.Text = validacaoTela.FindByText("Sigla").Value;
                            tbSigla.Text = lblSigla.Text;
                            tbSigla.Attributes["placeholder"] =
                                validacaoTela.FindByText("Sigla").Value;
                            mostraSigla.Visible = true;
                        }

                        popUpModal.Show();

                        break;
                    }
                case "excluir":
                    {
                        hfID.Value = "";
                        tbCampo.Text = "";
                        Excluir(_id);

                        break;
                    }
            }
        }

        protected void rptListagem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                validacaoTela = tela3Bll.PopulaTitulos(Funcoes.Funcoes.ConvertePara.String(
                    ViewState["Tela"]));
                Label lblCampo = (Label)e.Item.FindControl("lblCampo");
                Label lblSiglaTitulo = (Label)e.Item.FindControl("lblSiglaTitulo");

                lblCampo.Text = validacaoTela.FindByText("Nome").Value;
                lblSiglaTitulo.Text = (validacaoTela.FindByText("Sigla").Value !=
                    "sem sigla" ? validacaoTela.FindByText("Sigla").Value : "");
            }
        }

        private void Excluir(int _id)
        {
            if ((Funcoes.Funcoes.ConvertePara.Bool(Session["Excluir"])) ||
                (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
            {
                tela3To = new TOTela3Bll();

                tela3To.Id = Funcoes.Funcoes.ConvertePara.Int(_id);
                tela3To.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                tela3To.DataCadastro = DateTime.Now;
                tela3To.IP = Request.UserHostAddress;

                bllRetorno ret = tela3Bll.Excluir(Funcoes.Funcoes.ConvertePara.String(
                    ViewState["Tela"]), tela3To);

                if (ret.retorno)
                {
                    Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]),
                        Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                    Cancelar();
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Error, ret.mensagem,
                        "NutroVET Informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Selecione um ITEM na grade para efetuar a EXCLUSÃO!",
                    "NutroVET Informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
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
                //        "NutroVET Informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
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
                //        "NutroVET Informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                //        true);
                //}
            }
        }

        private void Inserir()
        {
            tela3To = new TOTela3Bll();
            tela3To.Nome = tbCampo.Text;
            tela3To.Sigla = tbSigla.Text;
            tela3To.Ativo = true;
            tela3To.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            tela3To.DataCadastro = DateTime.Now;
            tela3To.IP = Request.UserHostAddress;

            bllRetorno ret = tela3Bll.Inserir(Funcoes.Funcoes.ConvertePara.String(
                    ViewState["Tela"]), tela3To);

            if (ret.retorno)
            {
                Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]),
                    Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                Cancelar();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, ret.mensagem,
                    "NutroVET Informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, ret.mensagem,
                    "NutroVET Informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Alterar(int _id)
        {
            tela3To = new TOTela3Bll();

            tela3To.Id = _id;
            tela3To.Nome = tbCampo.Text;
            tela3To.Sigla = tbSigla.Text;
            tela3To.Ativo = true;
            tela3To.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            tela3To.DataCadastro = DateTime.Now;
            tela3To.IP = Request.UserHostAddress;

            bllRetorno ret = tela3Bll.Alterar(Funcoes.Funcoes.ConvertePara.String(
                ViewState["Tela"]), tela3To);

            if (ret.retorno)
            {
                Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]),
                    Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                Cancelar();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, ret.mensagem,
                    "NutroVET Informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, ret.mensagem,
                    "NutroVET Informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Cancelar()
        {
            tbCampo.Text = "";
            lblSiglaModal.Text = "";
            tbSigla.Text = "";
        }

        protected void lb1_Click(object sender, EventArgs e)
        {
            lb1.Text = "<b><u>" + lb1.CommandName + "</u></b>";
            lb2.Text = lb2.CommandName;
            lb3.Text = lb3.CommandName;
            lb4.Text = lb4.CommandName;
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), 
                Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName));
        }

        protected void lb2_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = "<b><u>" + lb2.CommandName + "</u></b>";
            lb3.Text = lb3.CommandName;
            lb4.Text = lb4.CommandName;
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]),
                Funcoes.Funcoes.ConvertePara.Int(lb2.CommandName));
        }

        protected void lb3_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = lb2.CommandName;
            lb3.Text = "<b><u>" + lb3.CommandName + "</u></b>";
            lb4.Text = lb4.CommandName;
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), 
                Funcoes.Funcoes.ConvertePara.Int(lb3.CommandName));
        }

        protected void lb4_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = lb2.CommandName;
            lb3.Text = lb3.CommandName;
            lb4.Text = "<b><u>" + lb4.CommandName + "</u></b>";
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), 
                Funcoes.Funcoes.ConvertePara.Int(lb4.CommandName));
        }

        protected void lb5_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = lb2.CommandName;
            lb3.Text = lb3.CommandName;
            lb4.Text = lb4.CommandName;
            lb5.Text = "<b><u>" + lb5.CommandName + "</u></b>";

            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), 
                Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName));
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

            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), 
                Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName));
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

            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), 
                Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName));
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

            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), 1);
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

            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), _pagTotal);
        }

        protected void ddlPag_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["pagTotal"] = tela3Bll.TotalPaginas(
                Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), tbPesq.Text,
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

            Paginar(Funcoes.Funcoes.ConvertePara.String(ViewState["Tela"]), 1);
        }

        protected string retornarTituloTela3()
        {
            int found = validacaoTela.FindByText("Titulo").Value.IndexOf("Cadastro ");
            return validacaoTela.FindByText("Titulo").Value.Substring(found + 12);
        }

    }
}