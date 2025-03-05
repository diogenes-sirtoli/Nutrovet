using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;
using System.Drawing;
using Org.BouncyCastle.Ocsp;

namespace Nutrovet.Cadastros
{
    public partial class TutoresSelecao : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll PessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected clTutoresBll tutorBll = new clTutoresBll();
        protected Tutore tutorDcl;
        protected TOTutoresBll tutorTO;
        protected clLogsSistemaBll logsBll = new clLogsSistemaBll();
        protected LogsSistema logsDcl;

        [Serializable]
        private struct Dados
        {
            public int IdTutores { get; set; }
            public int QuantAnimais { get; set; }
            public int QuantCardapios { get; set; }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = "~/AppMenuGeral.Master";
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
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "3.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        TOToastr _toastr = (TOToastr)Session["ToastrTutores"];

                        if (_toastr != null)
                        {
                            MostraToastr(_toastr);
                        }


                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        ViewState["pagAtual"] = 1;
                        ViewState["pagTamanho"] = 10;

                        Paginar(1);
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

        private void PopulaGrid(string _pesquisa, int _pagAtual, int _pagTamanho)
        {
            List<TOPessoasBll> tutoresListagem = tutorBll.ListarTutores(_pesquisa,
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
                 _pagTamanho, _pagAtual);

            rptListagemTutores.DataSource = tutoresListagem;
            rptListagemTutores.DataBind();
        }

        protected void rptListagemTutores_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "inserir":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(0);

                        Response.Redirect("~/Cadastros/TutoresCadastro.aspx");

                        break;
                    }
                case "editar":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(_id);

                        Response.Redirect("~/Cadastros/TutoresCadastro.aspx?_idTutores=" +
                            Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));

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

        private void Excluir(int _idTutores)
        {
            Dados _dadosTutores = new Dados();
            tutorDcl = tutorBll.Carregar(_idTutores);

            if ((tutorDcl != null) && (tutorDcl.IdTutor != tutorDcl.IdCliente))
            {
                _dadosTutores.IdTutores = tutorDcl.IdTutores;
                _dadosTutores.QuantCardapios = tutorBll.TotalCardapiosDoTutor(tutorDcl.IdTutor,
                    tutorDcl.IdCliente);
                _dadosTutores.QuantAnimais = tutorBll.TotalAnimaisDoTutor(tutorDcl.IdTutor,
                    tutorDcl.IdCliente);

                lblQtdAnimais.Text = Funcoes.Funcoes.ConvertePara.String(
                    _dadosTutores.QuantAnimais);
                lblQtdCard.Text = Funcoes.Funcoes.ConvertePara.String(
                    _dadosTutores.QuantCardapios);

                if ((_dadosTutores.QuantAnimais > 0) || (_dadosTutores.QuantCardapios > 0))
                {
                    ViewState["_dadosTutores"] = _dadosTutores;
                    mdExclusaoTutor.Show();
                }
                else
                {
                    tutorTO = tutorBll.CarregarTO(tutorDcl.IdTutores);
                    bllRetorno ret = tutorBll.Excluir(tutorDcl);

                    if (ret.retorno)
                    {
                        LogExclusao(tutorTO, 0, 0);

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
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Esta é uma Conta Cliente !</br>Não É Possível Excluí-la!",
                    "NutroVET informa - Exclusão",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void LogExclusao(TOTutoresBll _tutores, int _qtdAnimais, int _qtdCardap)
        {
            logsDcl = new LogsSistema();
            string _mensagem = "";

            logsDcl.IdPessoa = _tutores.IdCliente;
            logsDcl.IdTabela = (int)DominiosBll.LogTabelas.Tutores;
            logsDcl.IdAcao = (int)DominiosBll.AcoesCrud.Excluir;

            if ((_qtdAnimais == 0) && (_qtdCardap == 0))
            {
                _mensagem = string.Format(
                    "Assinante {0}, efetuou a Exclusão do Tutor {1}, em {2}",
                        _tutores.Cliente, _tutores.Tutor,
                            DateTime.Today.ToString("dd/MM/yyyy"));
            }
            else if ((_qtdAnimais > 0) && (_qtdCardap <= 0))
            {
                _mensagem = string.Format("Assinante {0}, efetuou a Exclusão do Tutor {1} e " +
                    "por referência {2} Paciente(s), em {3}",
                       _tutores.Cliente, _tutores.Tutor, _qtdAnimais,
                           DateTime.Today.ToString("dd/MM/yyyy"));
            }
            else if ((_qtdAnimais > 0) && (_qtdCardap > 0))
            {
                _mensagem = string.Format("Assinante {0}, efetuou a Exclusão do Tutor {1} e " +
                    "por referência {2} Paciente(s) e {3} Cardápio(s), em {4}",
                       _tutores.Cliente, _tutores.Tutor, _qtdAnimais, _qtdCardap,
                           DateTime.Today.ToString("dd/MM/yyyy"));
            }

            logsDcl.Justificativa = tbConcessao.Text;
            logsDcl.Mensagem = _mensagem;
            logsDcl.Datahora = DateTime.Now;

            bllRetorno bllRetorno = logsBll.Inserir(logsDcl);

            tbConcessao.Text = "";
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

        protected void ddlPag_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["pagTotal"] = tutorBll.TotalPaginas(tbPesq.Text,
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
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

        protected void Paginar(int _nrPag)
        {
            ViewState["pagTotal"] = tutorBll.TotalPaginas(tbPesq.Text,
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
                Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
            ViewState["pagAtual"] = _nrPag;

            int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]);
            int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTamanho"]);

            ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

            PopulaGrid(tbPesq.Text, _pagAtual, _pagTamanho);
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

        protected void lbExcluir_Click(object sender, EventArgs e)
        {
            Dados _dadosTutores = (Dados)ViewState["_dadosTutores"];
            tutorTO = tutorBll.CarregarTO(_dadosTutores.IdTutores);
            tutorDcl = tutorBll.Carregar(_dadosTutores.IdTutores);

            if (tbConcessao.Text != "")
            {
                bllRetorno ret = tutorBll.Excluir(tutorDcl);

                if (ret.retorno)
                {
                    LogExclusao(tutorTO, _dadosTutores.QuantAnimais,
                        _dadosTutores.QuantCardapios);
                    ViewState.Remove("_dadosTutores");

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
            else
            {
                mdExclusaoTutor.Show();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Para Realizar esta Ação, é Necessário sua Autorização por Escrito!!!", 
                    "NutroVET informa - Exclusão",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
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

            Session.Remove("ToastrTutores");
        }
    }
}