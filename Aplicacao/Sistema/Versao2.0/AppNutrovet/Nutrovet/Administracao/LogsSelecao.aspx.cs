using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using System.Web.Security;

namespace Nutrovet.Administracao
{
    public partial class LogsSelecao : System.Web.UI.Page
    {
        protected clLogsSistemaBll logsBll = new clLogsSistemaBll();
        protected LogsSistema logsDcl;
        protected TOLogsSistemaBll logsTO;
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                else
                {
                    if (acessosBll.Permissao(
                        Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "1.5.3",
                        Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                    {
                        if (!Page.IsPostBack)
                        {
                            lblAno.Text = DateTime.Today.ToString("yyyy");
                            ViewState["pagAtual"] = 1;
                            ViewState["pagTamanho"] = 10;

                            PopulaTabelas();
                            PopulaAcoes();
                            Paginar(1);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/MenuGeral.aspx?perm=" +
                            Funcoes.Funcoes.Seguranca.Criptografar("False"));
                    }
                }
            }
        }

        private void PopulaAcoes()
        {
            ddlAcoes.Items.AddRange(logsBll.ListarAcoes());
            ddlAcoes.DataBind();

            ddlAcoes.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaTabelas()
        {
            ddlTabelas.Items.AddRange(logsBll.ListarTabelas());
            ddlTabelas.DataBind();

            ddlTabelas.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        protected void lbPesq_Click(object sender, EventArgs e)
        {
            Paginar(1);
        }

        private void PopulaGrid(int? _pesqTabelas, int? _pesqAcoes, DateTime? _dtIni, 
            DateTime? _dtFim, int _pagAtual, int _pagTamanho)
        {
            List<TOLogsSistemaBll> logsListagem = logsBll.Listar(_pesqTabelas, _pesqAcoes,
                 _dtIni, _dtFim, _pagTamanho, _pagAtual);

            rptListagemLogs.DataSource = logsListagem;
            rptListagemLogs.DataBind();
        }

        protected void Paginar(int _nrPag)
        {
            if (_nrPag > 0)
            {
                DateTime? _dtIni = null;
                DateTime? _dtFim = null;
                int? _tabelas = Funcoes.Funcoes.ConvertePara.Int(ddlTabelas.SelectedValue);
                int? _acoes = Funcoes.Funcoes.ConvertePara.Int(ddlAcoes.SelectedValue);

                if (meDtIni.Text != "")
                {
                    _dtIni = DateTime.Parse(meDtIni.Text);
                }

                if (meDtFim.Text != "")
                {
                    _dtFim = DateTime.Parse(meDtFim.Text);
                }

                ViewState["pagTotal"] = logsBll.TotalPaginas(_tabelas, _acoes, _dtIni, _dtFim,
                    Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
                ViewState["pagAtual"] = _nrPag;

                int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]);
                int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTamanho"]);

                ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

                PopulaGrid(_tabelas, _acoes, _dtIni, _dtFim, _pagAtual, _pagTamanho);
            }
            else
            {
                ViewState["pagTotal"] = 0;
                ViewState["pagAtual"] = _nrPag;

                ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

                rptListagemLogs.DataSource = null;
                rptListagemLogs.DataBind();
            }
        }

        protected void rptListagemMensagens_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            logsDcl = logsBll.Carregar(_id);

            switch (e.CommandName)
            {
                case "visualizar":
                    {
                        lblMsg.Text = logsDcl.Mensagem;

                        popUpModal.Show();

                        break;
                    }
            }
        }

        protected void rptListagemMensagens_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        Label lblDataHoraRegistro = (Label)e.Item.FindControl(
                            "lblDataHoraRegistro");

                        logsTO = (TOLogsSistemaBll)e.Item.DataItem;

                        //lblDataHoraRegistro.Text = (logsTO.DataHora != null ?
                        //    logsTO.DataHora.Value.ToString("dd/MM/yyyy") : "");

                        break;
                    }
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
            DateTime? _dtIni = null;
            DateTime? _dtFim = null;
            int? _tabelas = Funcoes.Funcoes.ConvertePara.Int(ddlTabelas.SelectedValue);
            int? _acoes = Funcoes.Funcoes.ConvertePara.Int(ddlAcoes.SelectedValue);

            if (meDtIni.Text != "")
            {
                _dtIni = DateTime.Parse(meDtIni.Text);
            }

            if (meDtFim.Text != "")
            {
                _dtFim = DateTime.Parse(meDtFim.Text);
            }

            ViewState["pagTotal"] = logsBll.TotalPaginas(_tabelas, _acoes, _dtIni, _dtFim,
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
    }
}