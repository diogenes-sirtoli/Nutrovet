using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;
using System.Security.Cryptography;

namespace Nutrovet.Cadastros
{
    public partial class PacientesSelecao : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll PessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected clAnimaisBll animaisBll = new clAnimaisBll();
        protected Animai animaisDcl;
        protected clLogsSistemaBll logsBll = new clLogsSistemaBll();
        protected LogsSistema logsDcl;
        protected clTutoresBll tutorBll = new clTutoresBll();
        protected Tutore tutorDcl;
        protected TOTutoresBll tutorTO;
        protected clConfigReceituarioBll configReceitBll = new 
            clConfigReceituarioBll();
        protected ConfigReceituario configReceitDcl;

        [Serializable]
        private struct Dados
        {
            public int IdAnimal { get; set; }
            public int QuantPesos { get; set; }
            public int QuantCardapios { get; set; }
        }

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
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "4.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        TOToastr _toastr = (TOToastr)Session["ToastrPacientes"];

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
            List<TOAnimaisBll> animaisListagem = animaisBll.Listar(_pesquisa,
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
                _pagTamanho, _pagAtual);

            rptAnimais.DataSource = animaisListagem;
            rptAnimais.DataBind();
        }

        protected void rptAnimais_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);
            bllRetorno _permissao = configReceitBll.PermissaoAcessoLinhaTempo(
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));

            switch (e.CommandName)
            {
                case "inserir":
                    {
                        bllRetorno _podeCadastrar = animaisBll.PodeCadastrarAnimal(
                            Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));

                        if (_podeCadastrar.retorno)
                        {
                            if (_permissao.retorno)
                            {
                                Response.Redirect("~/Cadastros/PacientesCadastroLT.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/Cadastros/PacientesCadastro.aspx");
                            }
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                _podeCadastrar.mensagem, "NutroVET informa",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }

                        break;
                    }
                case "editar":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(_id);

                        if (_permissao.retorno)
                        {
                            Response.Redirect(
                                "~/Cadastros/PacientesCadastroLT.aspx?_idPaciente=" +
                                    Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));
                        }
                        else
                        {
                            Response.Redirect(
                                "~/Cadastros/PacientesCadastro.aspx?_idPaciente=" +
                                    Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));
                        }

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
            Dados _dadosPaciente = new Dados();
            animaisDcl = animaisBll.Carregar(_id);
            Animai paciente = animaisDcl;

            _dadosPaciente.IdAnimal = animaisDcl.IdAnimal;
            _dadosPaciente.QuantCardapios = animaisBll.TotalCardapiosDoAnimal(
                animaisDcl.IdAnimal);
            _dadosPaciente.QuantPesos = animaisBll.TotalPesosDoAnimal(animaisDcl.IdAnimal);

            lblQtPesos.Text = Funcoes.Funcoes.ConvertePara.String(
                _dadosPaciente.QuantPesos);
            lblQtdCard.Text = Funcoes.Funcoes.ConvertePara.String(
                _dadosPaciente.QuantCardapios);

            if ((_dadosPaciente.QuantPesos > 0) || (_dadosPaciente.QuantCardapios > 0))
            {
                ViewState["_dadosPaciente"] = _dadosPaciente;
                mdExclusaoPaciente.Show();
            }
            else
            {
                tutorTO = tutorBll.CarregarTO(animaisDcl.IdTutores);
                bllRetorno ret = animaisBll.Excluir(animaisDcl);

                if (ret.retorno)
                {
                    LogExclusao(tutorTO, paciente, 0, 0);

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

        private void LogExclusao(TOTutoresBll _tutor, Animai _paciente, int _qtdpeso, 
            int _qtdCardap)
        {
            logsDcl = new LogsSistema();
            string _mensagem = "";

            logsDcl.IdPessoa = _tutor.IdCliente;
            logsDcl.IdTabela = (int)DominiosBll.LogTabelas.Animais;
            logsDcl.IdAcao = (int)DominiosBll.AcoesCrud.Excluir;

            if ((_qtdpeso == 0) && (_qtdCardap == 0))
            {
                _mensagem = string.Format("Assinante {0}, efetuou a Exclusão do Paciente {1}, " +
                    "pertencente ao Tutor {2}, em {3}", _tutor.Cliente, _paciente.Nome,
                    _tutor.Tutor, DateTime.Today.ToString("dd/MM/yyyy"));
            }
            else if ((_qtdpeso > 0) && (_qtdCardap <= 0))
            {
                _mensagem = string.Format("Assinante {0}, efetuou a Exclusão do Paciente {1}, " +
                    "pertencente ao Tutor {2} e por referência {3} Peso(s) do Histórico de Pesos, " +
                    "em {4}", _tutor.Cliente, _paciente.Nome, _tutor.Tutor, _qtdpeso, 
                    DateTime.Today.ToString("dd/MM/yyyy"));
            }
            else if ((_qtdpeso <= 0) && (_qtdCardap > 0))
            {
                _mensagem = string.Format("Assinante {0}, efetuou a Exclusão do Paciente {1}, " +
                    "pertencente ao Tutor {2} e por referência {3} Cardápio(s), em {4}", 
                    _tutor.Cliente, _paciente.Nome, _tutor.Tutor, _qtdCardap, 
                    DateTime.Today.ToString("dd/MM/yyyy"));
            }
            else if ((_qtdpeso > 0) && (_qtdCardap > 0))
            {
                _mensagem = string.Format("Assinante {0}, efetuou a Exclusão do Paciente {1}, " +
                    "pertencente ao Tutor {2} e por referência {3} Peso(s) do Histórico de Pesos" +
                    " e {4} Cardápio(s), em {5}", _tutor.Cliente, _paciente.Nome, _tutor.Tutor,
                    _qtdpeso, _qtdCardap, DateTime.Today.ToString("dd/MM/yyyy"));
            }

            logsDcl.Justificativa = tbConcessao.Text;
            logsDcl.Mensagem = _mensagem;
            logsDcl.Datahora = DateTime.Now;

            bllRetorno bllRetorno = logsBll.Inserir(logsDcl);

            tbConcessao.Text = "";
        }

        protected void ddlPag_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["pagTotal"] = animaisBll.TotalPaginas(tbPesq.Text,
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
            ViewState["pagTotal"] = animaisBll.TotalPaginas(tbPesq.Text,
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
            Dados _dadosPaciente = (Dados)ViewState["_dadosPaciente"];
            animaisDcl = animaisBll.Carregar(_dadosPaciente.IdAnimal);
            Animai paciente = animaisDcl;
            tutorTO = tutorBll.CarregarTO(animaisDcl.IdTutores);

            if (tbConcessao.Text != "")
            {
                bllRetorno ret = animaisBll.Excluir(animaisDcl);

                if (ret.retorno)
                {
                    LogExclusao(tutorTO, paciente, _dadosPaciente.QuantPesos,
                        _dadosPaciente.QuantCardapios);
                    ViewState.Remove("_dadosPaciente");

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
                mdExclusaoPaciente.Show();

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

            Session.Remove("ToastrPacientes");
        }
    }
}