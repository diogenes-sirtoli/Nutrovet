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

                        int _idBiblio = Funcoes.Funcoes.ConvertePara.Int(
                            Funcoes.Funcoes.Seguranca.Descriptografar(
                                Funcoes.Funcoes.ConvertePara.String(
                                    Request.QueryString["_idBiblio"])));
                        ViewState["_idBiblio"] = _idBiblio;

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
            Cache _Cache = HttpContext.Current.Cache;

            List<TOTela4Bll> _indicesLista = (List<TOTela4Bll>)_Cache["CacheIndices"];
            TOTela4Bll _itemIndice;
            List<TOTela3Bll> _listagem = secoesBll.Listar();
            int _indice = 0;

            if (_indicesLista == null)
            {
                _indicesLista = new List<TOTela4Bll>();

                foreach (TOTela3Bll item in _listagem)
                {
                    _itemIndice = new TOTela4Bll();

                    _itemIndice.IdRef = _indice;
                    _itemIndice.Id = item.Id;

                    _indicesLista.Add(_itemIndice);

                    _indice += 1;
                }

                _Cache.Insert("CacheIndices", _indicesLista, null,
                    DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            }

            acSecoes.DataSource = _listagem;
            acSecoes.DataBind();
        }

        public void CacheRemove()
        {
            Cache _Cache = HttpContext.Current.Cache;
            _Cache.Remove("CacheIndices");
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MenuGeral.aspx");
        }

         private void SelecionaAbaAcordeon(int _idSeção)
        {
            Cache _Cache = HttpContext.Current.Cache;

            List<TOTela4Bll> _indicesLista = (List<TOTela4Bll>)_Cache["CacheIndices"];

            if (_indicesLista != null)
            {
                int _indice = _indicesLista.Find(f => f.Id == _idSeção).IdRef;
                acSecoes.SelectedIndex = _indice;
            }
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

                rptArquivos.DataSource = biblioBll.Listar(
                    Funcoes.Funcoes.ConvertePara.Int(ViewState["Id"]));
                rptArquivos.DataBind();
            }
        }

        protected void rptArquivos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            biblioTO = (TOBibliotecaBll)e.Item.DataItem;
            HyperLink hlNomeArq = (HyperLink)e.Item.FindControl("hlNomeArq");

            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        hlNomeArq.NavigateUrl = biblioTO.Caminho;

                        break;
                    }
            }
        }
    }
}