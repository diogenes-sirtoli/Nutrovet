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
        protected TOPessoasBll pessoaTO;
        protected clTutoresBll tutorBll = new clTutoresBll();
        protected Tutore tutorDcl;
        protected TOTutoresBll tutorTO;
        protected clLogsSistemaBll logsBll = new clLogsSistemaBll();
        protected LogsSistema logsDcl;

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
                                    Request.QueryString["_idTutores"])));
                        ViewState["_idTutores"] = _idTutor;

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

        private void PopulaTutor(int idTutores)
        {
            if (idTutores > 0)
            {
                tutorTO = tutorBll.CarregarTO(idTutores);

                tbNomeTutor.Text = tutorTO.Tutor;
                tbEmailTutor.Text = tutorTO.tEmail;

                switch (Funcoes.Funcoes.ConvertePara.Int(tutorTO.dTpEntidade))
                {
                    case 1:
                        {
                            rblPfPj.SelectedValue = "1";
                            meCpfCnpj.Text = tutorTO.CPF;

                            break;
                        }
                    case 2:
                        {
                            rblPfPj.SelectedValue = "2";
                            meCpfCnpj.Text = tutorTO.CNPJ;

                            break;
                        }
                }

                rblPfPj.Enabled = false;
                tbTelefoneTutor.Text = tutorTO.Telefone;
                tbCelularTutor.Text = tutorTO.Celular;
            }
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            int _idTutores = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idTutores"]);

            Salvar(_idTutores);
        }

        protected void Salvar(int _idTutores)
        {
            if (_idTutores > 0)
            {
                tutorDcl = tutorBll.Carregar(_idTutores);

                if ((tutorDcl != null) && (tutorDcl.IdTutor != tutorDcl.IdCliente))
                {
                    Alterar(tutorDcl.IdTutor);
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
                InserirTutorNovo();
            }
        }

        private void InserirTutorNovo()
        {
            //Pessoa
            pessoaDcl = new Pessoa();
            pessoaDcl.Nome = tbNomeTutor.Text;
            pessoaDcl.dTpEntidade = Funcoes.Funcoes.ConvertePara.Int(rblPfPj.SelectedValue);
            pessoaDcl.IdTpPessoa = (int)DominiosBll.PessoasAuxTipos.Tutor;
            pessoaDcl.Email = tbEmailTutor.Text;

            switch (Funcoes.Funcoes.ConvertePara.Int(rblPfPj.SelectedValue))
            {
                case 1:
                    {
                        pessoaDcl.CPF = meCpfCnpj.Text;

                        break;
                    }
                case 2:
                    {
                        pessoaDcl.CNPJ = meCpfCnpj.Text;

                        break;
                    }
            }

            pessoaDcl.Telefone = tbTelefoneTutor.Text;
            pessoaDcl.Celular = tbCelularTutor.Text;
            pessoaDcl.Ativo = true;
            pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            pessoaDcl.DataCadastro = DateTime.Now;
            pessoaDcl.IP = Request.UserHostAddress;

            //Tutor
            tutorDcl = new Tutore();
            tutorDcl.IdCliente = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            tutorDcl.Ativo = true;
            tutorDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            tutorDcl.DataCadastro = DateTime.Now;
            tutorDcl.IP = Request.UserHostAddress;

            pessoaDcl.Tutores1.Add(tutorDcl);

            bllRetorno inserirRet = PessoaBll.InserirTutor(pessoaDcl);

            if (inserirRet.retorno)
            {
                LogInsercao(tutorDcl);
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

        private void LogInsercao(Tutore _tutor)
        {
            tutorTO = tutorBll.CarregarTO(_tutor.IdTutores);
            logsDcl = new LogsSistema();

            logsDcl.IdPessoa = _tutor.IdCliente;
            logsDcl.IdTabela = (int)DominiosBll.LogTabelas.Tutores;
            logsDcl.IdAcao = (int)DominiosBll.AcoesCrud.Inserir;
            logsDcl.Justificativa = "";
            logsDcl.Mensagem = string.Format(
                "Assinante {0} efetuou a inserção do Tutor {1}, em {2}", 
                    tutorTO.Cliente, tutorTO.Tutor, DateTime.Today.ToString("dd/MM/yyyy"));
            logsDcl.Datahora = DateTime.Now;

            bllRetorno bllRetorno = logsBll.Inserir(logsDcl);
        }

        private void LogAlteracao(Pessoa _tutor)
        {
            tutorTO = tutorBll.CarregarTOPorTutor(_tutor.IdPessoa);
            logsDcl = new LogsSistema();

            if (tutorTO != null)
            {
                logsDcl.IdPessoa = tutorTO.IdCliente;
                logsDcl.IdTabela = (int)DominiosBll.LogTabelas.Tutores;
                logsDcl.IdAcao = (int)DominiosBll.AcoesCrud.Alterar;
                logsDcl.Justificativa = "";
                logsDcl.Mensagem = string.Format(
                    "Assinante {0} efetuou a alteração do Tutor {1}, em {2}",
                        tutorTO.Cliente, tutorTO.Tutor, DateTime.Today.ToString("dd/MM/yyyy"));
                logsDcl.Datahora = DateTime.Now;

                bllRetorno bllRetorno = logsBll.Inserir(logsDcl); 
            }
        }

        private void Alterar(int _id)
        {
            pessoaDcl = PessoaBll.Carregar(_id);

            pessoaDcl.Nome = tbNomeTutor.Text;
            pessoaDcl.Email = tbEmailTutor.Text;
            pessoaDcl.dTpEntidade = Funcoes.Funcoes.ConvertePara.Int(rblPfPj.SelectedValue);

            switch (Funcoes.Funcoes.ConvertePara.Int(rblPfPj.SelectedValue))
            {
                case 1:
                    {
                        pessoaDcl.CPF = meCpfCnpj.Text;

                        break;
                    }
                case 2:
                    {
                        pessoaDcl.CNPJ = meCpfCnpj.Text;

                        break;
                    }
            }

            pessoaDcl.Telefone = tbTelefoneTutor.Text;
            pessoaDcl.Celular = tbCelularTutor.Text;

            pessoaDcl.Ativo = true;
            pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            pessoaDcl.DataCadastro = DateTime.Now;
            pessoaDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = PessoaBll.AlterarTutor(pessoaDcl);

            if (alterarRet.retorno)
            {
                LogAlteracao(pessoaDcl);

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
                    Funcoes.Funcoes.Toastr.ToastType.Error, alterarRet.mensagem,
                    "Informe NutroVET - Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Cancelar()
        {
            ViewState.Remove("_idTutores");
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

        protected void rblPfPj_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Funcoes.Funcoes.ConvertePara.Int(rblPfPj.SelectedValue))
            {
                case 1:
                    {
                        LimparCampos();

                        lblNomeTutor.Text = "Nome&nbsp;Completo";
                        lblCpfCnpj.Text = "CPF";
                        meCpfCnpj.Mascara = MEdit.TpMascara.CPF;
                        meCpfCnpj.Attributes["placeholder"] = "CPF";
                        meCpfCnpj.Attributes["title"] = "CPF do Assinante";

                        break;
                    }
                case 2:
                    {
                        LimparCampos();

                        lblNomeTutor.Text = "Razão&nbsp;Social";
                        lblCpfCnpj.Text = "CNPJ";
                        meCpfCnpj.Mascara = MEdit.TpMascara.CNPJ;
                        meCpfCnpj.Attributes["placeholder"] = "CNPJ";
                        meCpfCnpj.Attributes["title"] = "CNPJ do Assinante";

                        break;
                    }
            }
        }

        private void LimparCampos()
        {
            tbNomeTutor.Text = "";
            tbEmailTutor.Text = "";
            meCpfCnpj.Text = "";
            tbTelefoneTutor.Text = "";
            tbCelularTutor.Text = "";
        }
    }
}