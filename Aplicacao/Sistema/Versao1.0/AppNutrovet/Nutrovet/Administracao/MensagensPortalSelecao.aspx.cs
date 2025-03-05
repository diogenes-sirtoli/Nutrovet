using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace Nutrovet.Administracao
{
    public partial class MensagensPortalSelecao : System.Web.UI.Page
    {
        protected clPortalContatosBll contatosBll = new clPortalContatosBll();
        protected PortalContato contatoDcl;
        protected TOPortalContatoBll contatoTO;
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
                        Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "1.5.2",
                        Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                    {
                        if (!Page.IsPostBack)
                        {
                            TOToastr _toastr = (TOToastr)Session["ToastrAlimentos"];

                            if (_toastr != null)
                            {
                                MostraToastr(_toastr);
                            }

                            lblAno.Text = DateTime.Today.ToString("yyyy");

                            PopulaLista();
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

        private void PopulaLista()
        {
            rptListagemMensagens.DataSource = contatosBll.Listar();
            rptListagemMensagens.DataBind();
        }

        protected void rptListagemMensagens_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            contatoDcl = contatosBll.Carregar(_id);
            hdfIdMsg.Value = Funcoes.Funcoes.ConvertePara.String(contatoDcl.IdContato);

            switch (e.CommandName)
            {
                case "responder":
                    {
                        if (Funcoes.Funcoes.ConvertePara.Int(contatoDcl.MsgSituacao) == 3)
                        {
                            lblDe.Text = "contato@nutrovet.com.br";
                            lblPara.Text = contatoDcl.EmailContato;

                            popupEnviaEmail.Show();
                        }

                        break;
                    }
                case "arquivar":
                    {
                        contatoDcl.MsgSituacao =
                            (int)DominiosBll.PortalContatoAuxSituacao.Arquivada;
                        contatoDcl.Observacoes += @"
                            Registro Arquivado em: " + 
                                DateTime.Today.ToString("dd/MM/yyyy");

                        bllRetorno retorno = contatosBll.Alterar(contatoDcl);

                        if (retorno.retorno)
                        {
                            PopulaLista();

                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Success, 
                                "Mensagem Arquivada com Sucesso!!!", 
                                "NutroVET Informa", 
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                retorno.mensagem, "NutroVET Informa", 
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }

                        break;
                    }
                case "visualizar":
                    {
                        if (Funcoes.Funcoes.ConvertePara.Int(contatoDcl.MsgSituacao) < 3)
                        {
                            contatoDcl.MsgSituacao =
                                (int)DominiosBll.PortalContatoAuxSituacao.Lida;
                            contatoDcl.Observacoes += @"
                            Registro Visualizado em: " +
                                    DateTime.Today.ToString("dd/MM/yyyy");

                            bllRetorno retorno = contatosBll.Alterar(contatoDcl);

                            if (retorno.retorno)
                            {
                                lblMsg.Text = contatoDcl.Mensagem;

                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Success,
                                    "Mensagem Marcada como Lida!!!",
                                    "NutroVET Informa",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);

                                PopulaLista();
                                popUpModal.Show();
                            }
                            else
                            {
                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Error,
                                    retorno.mensagem, "NutroVET Informa",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);
                            }
                        }
                        else
                        {
                            lblMsg.Text = contatoDcl.Mensagem;

                            popUpModal.Show();
                        }

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
                        Label lblEnvioRegistro = (Label)e.Item.FindControl(
                            "lblEnvioRegistro");
                        HtmlGenericControl divSituacao = (HtmlGenericControl)e.Item.FindControl(
                            "divSituacao");

                        contatoTO = (TOPortalContatoBll)e.Item.DataItem;

                        lblEnvioRegistro.Text = (contatoTO.DataEnvio != null ?
                            contatoTO.DataEnvio.Value.ToString("dd/MM/yyyy") : "");
                        divSituacao.Attributes["class"] = SituacaoCor(
                            contatoTO.MsgSituacao);

                        break;
                    }
            }
        }

        private string SituacaoCor(int? _idSituacao)
        {
            string _retorno = "";

            switch (Funcoes.Funcoes.ConvertePara.Int(_idSituacao))
            {
                case 1:
                    {
                        _retorno = "badge badge-danger";

                        break;
                    }
                case 2:
                case 3:
                    {
                        _retorno = "badge badge-warning";

                        break;
                    }
                case 4:
                    {
                        _retorno = "badge badge-success";

                        break;
                    }
                case 5:
                    {
                        _retorno = "badge badge-secondary";

                        break;
                    }
                default:
                    {
                        _retorno = "badge badge-primary";

                        break;
                    }
            }

            return _retorno;
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

            Session.Remove("ToastrAlimentos");
        }

        protected void lbEnviar_Click(object sender, EventArgs e)
        {
            Funcoes.Funcoes.fncRetorno fncRetornoBll = new
                    Funcoes.Funcoes.fncRetorno();

            contatoDcl = contatosBll.Carregar(Funcoes.Funcoes.ConvertePara.Int(
                hdfIdMsg.Value));

            Funcoes.Funcoes.EMail.EmailDe = "contato@nutrovet.com.br";
            Funcoes.Funcoes.EMail.EmailPara = contatoDcl.EmailContato;
            Funcoes.Funcoes.EMail.Mensagem = tbxMsg.Text;
            Funcoes.Funcoes.EMail.Assunto = tbxAssunto.Text;

            Funcoes.Funcoes.EMail.SMTP = "dedrelay.secureserver.net";
            Funcoes.Funcoes.EMail.Porta = 25;
            Funcoes.Funcoes.EMail.Conta = "contato@nutrovet.com.br";
            Funcoes.Funcoes.EMail.Senha = "m@r5ped9";
            Funcoes.Funcoes.EMail.SSL = false;

            fncRetornoBll = Funcoes.Funcoes.EMail.Enviar();

            if (fncRetornoBll.retorno)
            {
                contatoDcl.MsgSituacao =
                    (int)DominiosBll.PortalContatoAuxSituacao.Respondida;
                contatoDcl.DataResposta = DateTime.Today;
                contatoDcl.Observacoes += @"
                Registro Respondido em: " +
                        DateTime.Today.ToString("dd/MM/yyyy");

                bllRetorno retorno = contatosBll.Alterar(contatoDcl);
                
                if (retorno.retorno)
                {
                    PopulaLista();

                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Success,
                        "Mensagem Enviada com Sucesso!!!",
                        "NutroVET Informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Error,
                        retorno.mensagem, "NutroVET Informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                } 
            }
        }
    }
}