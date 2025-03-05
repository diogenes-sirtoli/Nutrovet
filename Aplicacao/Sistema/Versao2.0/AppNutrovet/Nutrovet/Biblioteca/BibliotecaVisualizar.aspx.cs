using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;
using System.Web.Caching;
using System.IO;
using AjaxControlToolkit;

namespace Nutrovet.Biblioteca
{
    public partial class BibliotecaVisualizar : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll PessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected clBibliotecaBll biblioBll = new clBibliotecaBll();
        protected DCL.Biblioteca biblioDcl;
        protected TOBibliotecaBll biblioTO;
        protected clBibliotecaAuxSecoesBll secoesBll = new
            clBibliotecaAuxSecoesBll();
        protected BibliotecaAuxSecoe secoesDcl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (acessosBll.Permissao(
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "10.0.2",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        PopulaAcordeonSecoes();
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaAcordeonSecoes()
        {
            List<TOTela3Bll> _listagem = secoesBll.Listar();

            acSecoes.DataSource = _listagem;
            acSecoes.DataBind();
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MenuGeral.aspx");
        }

        protected void acSecoes_ItemDataBound(object sender, AjaxControlToolkit.AccordionItemEventArgs e)
        {
            if (e.ItemType == AccordionItemType.Header)
            {
                HiddenField hfId = (HiddenField)e.AccordionItem.FindControl("hfId");

                ViewState["Id"] = Funcoes.Funcoes.ConvertePara.Int(hfId.Value);
            }

            if (e.ItemType == AccordionItemType.Content)
            {
                Repeater rptArquivos = (Repeater)e.AccordionItem.FindControl(
                    "rptArquivos");
                List<TOBibliotecaBll> _listagem = (
                    Funcoes.Funcoes.ConvertePara.Bool(ViewState["pesquisa"]) ? 
                    biblioBll.Listar(Funcoes.Funcoes.ConvertePara.Int(ViewState["Id"]), 
                        tbPesq.Text) :
                    biblioBll.Listar(Funcoes.Funcoes.ConvertePara.Int(ViewState["Id"])));

                rptArquivos.DataSource = _listagem;
                rptArquivos.DataBind();
            }
        }

        protected void rptArquivos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            biblioTO = (TOBibliotecaBll)e.Item.DataItem;
            HyperLink hlNomeArq = (HyperLink)e.Item.FindControl("hlNomeArq");
            Label lblNomeArq = (Label)e.Item.FindControl("lblNomeArq");

            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        string _patch = Server.MapPath(biblioTO.Caminho);

                        if (File.Exists(_patch))
                        {
                            lblNomeArq.Visible = false;

                            hlNomeArq.Visible = true;
                            hlNomeArq.NavigateUrl = biblioTO.Caminho; 
                        }
                        else
                        {
                            lblNomeArq.Visible = true;

                            hlNomeArq.Visible = false;
                            hlNomeArq.NavigateUrl = "";
                        }

                        break;
                    }
            }
        }

        protected void lbPesq_Click(object sender, EventArgs e)
        {
            if (tbPesq.Text != "")
            {
                acSecoes.SelectedIndex = 0;

                List<TOBibliotecaBll> _listagem = biblioBll.Listar(0, tbPesq.Text);

                if (_listagem.Count() > 0)
                {
                    List<TOTela3Bll> _listagemAcSecoes = biblioBll.ListarSecoes(_listagem);
                    ViewState["pesquisa"] = true;

                    acSecoes.DataSource = _listagemAcSecoes;
                    acSecoes.DataBind();
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Info,
                        "A Pesquisa não Retornou Nenhum Resultado!",
                        "NutroVET Informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Efetue a Pesquisa por Ano, Título, Descrição ou Autor!",
                    "NutroVET Informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void lbRemoveFiltro_Click(object sender, EventArgs e)
        {
            ViewState["pesquisa"] = false;
            tbPesq.Text = "";

            PopulaAcordeonSecoes();
        }
    }
}