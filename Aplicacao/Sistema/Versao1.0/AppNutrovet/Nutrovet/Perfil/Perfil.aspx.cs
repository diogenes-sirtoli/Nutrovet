using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;
using DCL;
using BLL;
using AjaxControlToolkit;
using MaskEdit;
using System.IO;

namespace Nutrovet.Perfil
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected clAcessosBll logonBll = new clAcessosBll();
        protected Acesso logonDcl = new Acesso();
        protected clPessoasBll pessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;

        protected clPortalContatosBll contatosBll = new clPortalContatosBll();
        protected PortalContato contatosDcl;

        protected clAcessosVigenciaPlanosBll acessosVigenciaPlanosBLL = new clAcessosVigenciaPlanosBll();
        protected AcessosVigenciaPlano acessosVigenciaPlanoDCL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                   // fileUpload.Attributes.Add(
                     //   "onclick", "document.getElementById('" + 
                       //     FileUpload1.ClientID + "').click();");
                    lblAno.Text = DateTime.Today.ToString("yyyy");

                    int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
                    populaTelaPerfil(_idPessoa);
                    imgFoto.ImageUrl = CarregarImagem(_idPessoa);
                }
            }
        }

        private void populaTelaPerfil(int idPessoa)
        {
            pessoaDcl = pessoaBll.Carregar(idPessoa);

            lblUsuario.Text = pessoaDcl.Nome;
            lblTpPessoa.Text = pessoaBll.RetornaTipoPessoa(pessoaDcl.IdTpPessoa);
            tbxNomeUsuario.Text = pessoaDcl.Nome;
            tbxEmailUsuario.Text = pessoaDcl.Email;
            tbTelefone.Text = pessoaDcl.Telefone;
            tbCelular.Text = pessoaDcl.Celular;
            txbSenhaAtual.Text = Funcoes.Funcoes.Seguranca.Descriptografar(
                pessoaDcl.Senha);
            txbNovaSenha.Text = "";
            txbConfirmacaoSenha.Text = "";
            tbxName.Text = pessoaDcl.Nome;
            tbxEmail.Text = pessoaDcl.Email;

            List<TOAcessosVigenciaPlanosBll> acessosVigenciaPlano =
                acessosVigenciaPlanosBLL.ListarTO(idPessoa);

            if (acessosVigenciaPlano.Count > 0)
            {
                lblDescPlano.Text = acessosVigenciaPlano[0].Plano;
                lblPeriodoDesc.Text = acessosVigenciaPlano[0].PeriodoDescr;
                lblSituacaoDescr.Text = acessosVigenciaPlano[0].Situacao;
            }
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            Alterar(_idPessoa);
        }

        private void Alterar(int _id)
        {
            pessoaDcl = pessoaBll.Carregar(_id);

            pessoaDcl.Nome = tbxNomeUsuario.Text;
            pessoaDcl.Email = tbxEmailUsuario.Text;
            pessoaDcl.Telefone = tbTelefone.Text;
            pessoaDcl.Celular = tbCelular.Text;

            pessoaDcl.Ativo = true;
            pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            pessoaDcl.DataCadastro = DateTime.Now;
            pessoaDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = pessoaBll.Alterar(pessoaDcl);

            if (alterarRet.retorno)
            {
                Pessoa usuarioDcl = (Pessoa)Session["_dadosBasicos"];
                usuarioDcl.Nome = pessoaDcl.Nome;
                Session["_dadosBasicos"] = usuarioDcl;

                populaTelaPerfil(_id);

                Funcoes.Funcoes.Toastr.ShowToast(this,
                  Funcoes.Funcoes.Toastr.ToastType.Success, alterarRet.mensagem,
                  "Informe NutroVET - Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                  true);

            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning, alterarRet.mensagem,
                    "Informe NutroVET - Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void lbSalvarNovaSenha_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            AlterarSenha(_idPessoa);
        }

        private void AlterarSenha(int _id)
        {
            if (txbNovaSenha.Text != "" && txbConfirmacaoSenha.Text != "")
            {
                if (txbNovaSenha.Text == txbConfirmacaoSenha.Text)
                {
                    pessoaDcl = pessoaBll.Carregar(_id);
                    pessoaDcl.IP = Request.UserHostAddress;
                    pessoaDcl.Senha = Funcoes.Funcoes.Seguranca.Criptografar(txbNovaSenha.Text);

                    bllRetorno alterarRet = pessoaBll.Alterar(pessoaDcl);

                    if (alterarRet.retorno)
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                          Funcoes.Funcoes.Toastr.ToastType.Success,
                          alterarRet.mensagem,
                          "Informe NutroVET - Alteração de Senhas",
                          Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                          true);
                        populaTelaPerfil(_id);
                    }
                    else
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Warning,
                            alterarRet.mensagem,
                            "Informe NutroVET - Alteração de Senhas",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Senhas não Conferem!",
                        "Informe NutroVET - Alteração de Senhas",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                    populaTelaPerfil(_id);
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Senhas não podem estar em Branco!",
                    "Informe NutroVET - Alteração de Senhas",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
                populaTelaPerfil(_id);
            }
        }

        protected void lbEmail_Click(object sender, EventArgs e)
        {
            contatosDcl = new PortalContato();

            contatosDcl.Assunto = tbxAssunto.Text;
            contatosDcl.NomeContato = tbxName.Text;
            contatosDcl.EmailContato = tbxEmail.Text;
            contatosDcl.Mensagem = tbxMsg.Text;
            contatosDcl.DataEnvio = DateTime.Today;
            contatosDcl.DataResposta = DateTime.Parse("01/01/1910");
            contatosDcl.MsgSituacao = (int)DominiosBll.PortalContatoAuxSituacao.Enviada;

            bllRetorno _ret = contatosBll.Inserir(contatosDcl);

            if (_ret.retorno)
            {
                LimpaTela();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                  Funcoes.Funcoes.Toastr.ToastType.Success, _ret.mensagem,
                  "Mensagem", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                  true);

            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning, _ret.mensagem,
                    "Mensagem", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void LimpaTela()
        {
            tbxAssunto.Text = "";
            tbxName.Text = "";
            tbxEmail.Text = "";
            tbxMsg.Text = "";
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Session.Remove("ToastrPessoa");
            Response.Redirect("~/MenuGeral.aspx");
        }

        protected void lbEnviaImagem_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.PostedFile.ContentLength < 512000)
                {
                    string _extensao = Path.GetExtension(FileUpload1.FileName);
                    string _cliente = "Cliente_" + User.Identity.Name + _extensao;

                    try
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/Perfil/Fotos/") +
                             _cliente);
                        lblFileUpload.Text = "Arquivo Gravado como: " + _cliente;

                        imgFoto.ImageUrl = CarregarImagem(Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name));
                    }
                    catch (Exception ex)
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Error,
                            "ERRO: " + ex.Message.ToString(),
                            "Informe NutroVET - Upload",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    } 
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Tamanho do Arquivo Inválido!!!<br />Máx: 500 Kb",
                        "Informe NutroVET - Upload",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Arquivo não Selecionado!!!",
                    "Informe NutroVET - Upload",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private string CarregarImagem(int idPessoa)
        {
            string path = Server.MapPath("~/Perfil/Fotos/");
            DirectoryInfo _directorio = new DirectoryInfo(path);

            string search = (from d in _directorio.GetFiles("Cliente_" + idPessoa + ".*")
                             orderby d.LastAccessTime descending
                             select d.Name).FirstOrDefault();

            return "~/Perfil/Fotos/" + search + "?time=" + DateTime.Now.ToString();
        }
    }
}

