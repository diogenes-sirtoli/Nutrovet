using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;
using AjaxControlToolkit;
using MaskEdit;

namespace Nutrovet.Cadastros
{
    public partial class TutoresCadastro : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll PessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;

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
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        int _idTutor = Funcoes.Funcoes.ConvertePara.Int(
                            Funcoes.Funcoes.Seguranca.Descriptografar(
                                Funcoes.Funcoes.ConvertePara.String(
                                    Request.QueryString["_idTutor"])));
                        ViewState["_idTutor"] = _idTutor;

                        PopulaTitulo(_idTutor);
                        PopulaTutor(_idTutor);
                        //tbNomeTutor.Focus();
                        Page.Form.DefaultFocus = tbNomeTutor.ClientID;
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaTitulo(int idTutor)
        {
            if (idTutor > 0)
            {
                lblTitulo.Text = "Alteração de Tutor";
                lblPagina.Text = "Alterar Tutor";
                lblSubTitulo.Text = "Altere aqui os dados do Tutor";
            }
            else
            {
                lblTitulo.Text = "Inserção de Tutor";
                lblPagina.Text = "Inserir Tutor";
                lblSubTitulo.Text = "Insira aqui o Tutor a cadastrar";
            }
        }

        private void PopulaTutor(int idTutor)
        {
            if (idTutor > 0)
            {
                pessoaDcl = PessoaBll.Carregar(idTutor);

                tbNomeTutor.Text = pessoaDcl.Nome;
                tbEmailTutor.Text = pessoaDcl.Email;
                tbTelefoneTutor.Text = pessoaDcl.Telefone;
                tbCelularTutor.Text = pessoaDcl.Celular;
            }
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            int _idTutor = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idTutor"]);

            Salvar(_idTutor);
        }

        protected void Salvar(int _id)
        {
            if (_id > 0)
            {
                if (!PessoaBll.IsClient(_id, true))
                {
                    Alterar(_id);
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Esta é uma Conta Cliente !</br>Não É Possível Alterá-la!",
                        "NutroVET informa - Alteração",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                Inserir();
            }
        }

        private void Inserir()
        {
            pessoaDcl = new Pessoa();

            pessoaDcl.Nome = tbNomeTutor.Text;
            pessoaDcl.IdTpPessoa = 3;
            pessoaDcl.IdCliente = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            pessoaDcl.Email = tbEmailTutor.Text;
            pessoaDcl.Telefone = tbTelefoneTutor.Text;
            pessoaDcl.Celular = tbCelularTutor.Text;

            pessoaDcl.Ativo = true;
            pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            pessoaDcl.DataCadastro = DateTime.Now;
            pessoaDcl.IP = Request.UserHostAddress;

            bllRetorno inserirRet = PessoaBll.Inserir(pessoaDcl);

            if (inserirRet.retorno)
            {
                Cancelar();
                TOToastr _tostr = new TOToastr
                {
                    Tipo = 'S',
                    Titulo = "Informe NutroVET - Inserção",
                    Mensagem = inserirRet.mensagem
                };
                Session["ToastrTutores"] = _tostr;
                Response.Redirect("~/Cadastros/TutoresSelecao.aspx");
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                    "Informe NutroVET - Inserção", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Alterar(int _id)
        {
            pessoaDcl = PessoaBll.Carregar(_id);

            pessoaDcl.Nome = tbNomeTutor.Text;
            pessoaDcl.Email = tbEmailTutor.Text;
            pessoaDcl.Telefone = tbTelefoneTutor.Text;
            pessoaDcl.Celular = tbCelularTutor.Text;

            pessoaDcl.Ativo = true;
            pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            pessoaDcl.DataCadastro = DateTime.Now;
            pessoaDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = PessoaBll.Alterar(pessoaDcl);

            if (alterarRet.retorno)
            {
                TOToastr _tostr = new TOToastr
                {
                    Tipo = 'S',
                    Titulo = "Informe NutroVET - Alteração",
                    Mensagem = alterarRet.mensagem
                };
                Session["ToastrTutores"] = _tostr;
                Response.Redirect("~/Cadastros/TutoresSelecao.aspx");
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning, alterarRet.mensagem,
                    "Informe NutroVET - Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Cancelar()
        {
            ViewState.Remove("_idTutor");
            tbNomeTutor.Text = "";
            tbEmailTutor.Text = "";
            tbTelefoneTutor.Text = "";
            tbCelularTutor.Text = "";
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Session.Remove("ToastrTutores");
            Response.Redirect("~/Cadastros/TutoresSelecao.aspx");
        }
    }
}