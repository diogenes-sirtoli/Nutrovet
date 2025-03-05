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
    public partial class BibliotecaCadastro : System.Web.UI.Page
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
                        PopulaDdlSecoes();
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaDdlSecoes()
        {
            ddlSecao.DataTextField = "Nome";
            ddlSecao.DataValueField = "Id";
            ddlSecao.DataSource = secoesBll.Listar();
            ddlSecao.DataBind();

            ddlSecao.Items.Insert(0, new ListItem("-- Selecione --", "0"));
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

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            mdInserirArquivoSecao.Show();
        }

        protected void btnSalvarInserirArquivoSecao_Click(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idBiblio"]) > 0)
            {
                Alterar(Funcoes.Funcoes.ConvertePara.Int(ViewState["_idBiblio"]));
            }
            else
            {
                Inserir();
            }
        }

        private void Inserir()
        {
            biblioDcl = new DCL.Biblioteca();

            biblioDcl.IdSecao = Funcoes.Funcoes.ConvertePara.Int(
                ddlSecao.SelectedValue);
            biblioDcl.NomeArq = tbxTituloArquivo.Text;
            biblioDcl.Descricao = tbxDescricao.Text;
            biblioDcl.Autor = tbxAutor.Text;
            biblioDcl.Ano = Funcoes.Funcoes.ConvertePara.Int(meAnoPublic.Text);
            biblioDcl.Ordenador = biblioBll.Ordenador(Funcoes.Funcoes.ConvertePara.Int(
                ddlSecao.SelectedValue));
            biblioDcl.Caminho = (ViewState["_arqvioEnviado"] != null ? Funcoes.Funcoes.ConvertePara.String(
                ViewState["_arqvioEnviado"]) : "");

            biblioDcl.Ativo = true;
            biblioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            biblioDcl.DataCadastro = DateTime.Now;
            biblioDcl.IP = Request.UserHostAddress;

            CacheRemove();
            bllRetorno inserirRet = biblioBll.Inserir(biblioDcl);

            if (inserirRet.retorno)
            {
                PopulaAcordeonSecoes();
                LimparCampos();
                SelecionaAbaAcordeon(Funcoes.Funcoes.ConvertePara.Int(biblioDcl.IdSecao));

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Inserção Efetuada com Sucesso !",
                    "NutroVET informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                mdInserirArquivoSecao.Show();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                    "NutroVET informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
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

        private void LimparCampos()
        {
            ViewState.Remove("_arqvioEnviado");
            ViewState.Remove("_idBiblio");

            Funcoes.Funcoes.ControlForm.SetComboBox(ddlSecao, 0);
            tbxTituloArquivo.Text = "";
            tbxDescricao.Text = "";
            tbxAutor.Text = "";
            meAnoPublic.Text = "";
            tbxTituloArquivo.Text = "";

            ddlSecao.Enabled = true;
            fuInserirArquivoSecao.Enabled = true;
            lbEnviarArquivo.Enabled = true;
        }

        protected void lbEnviarArquivo_Click(object sender, EventArgs e)
        {
            if (fuInserirArquivoSecao.HasFile)
            {
                if (fuInserirArquivoSecao.PostedFile.ContentLength < 3145728)
                {
                    string _extensao = Path.GetExtension(fuInserirArquivoSecao.FileName);
                    string _arquivo = ddlSecao.SelectedItem.Text + "_" + biblioBll.Ordenador(
                        Funcoes.Funcoes.ConvertePara.Int(ddlSecao.SelectedValue)) + _extensao;

                    if (_extensao == ".pdf")
                    {
                        try
                        {
                            ViewState["_arqvioEnviado"] = "~/Biblioteca/Arquivos/" + _arquivo;

                            fuInserirArquivoSecao.SaveAs(Server.MapPath("~/Biblioteca/Arquivos/") +
                                 _arquivo);

                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Success,
                                "Arquivo Enviado com Sucesso!!!<br/>Clique no Botão << SALVAR >>",
                                "Informe NutroVET - Upload",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
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

                        mdInserirArquivoSecao.Show();
                    }
                    else
                    {
                        mdInserirArquivoSecao.Show();

                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Warning,
                            "Somente Arquivos PDF podem ser Enviados!!!",
                            "Informe NutroVET - Upload",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
                else
                {
                    mdInserirArquivoSecao.Show();

                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Tamanho do Arquivo Inválido!!!<br />Máx: 3 Mb",
                        "Informe NutroVET - Upload",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                mdInserirArquivoSecao.Show();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Arquivo não Selecionado!!!",
                    "Informe NutroVET - Upload",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
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

        protected void rptArquivos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "editar":
                    {
                        ViewState["_idBiblio"] = _id;

                        PopulaPopUp(_id);

                        break;
                    }
                case "excluir":
                    {
                        Excluir(_id);

                        break;
                    }
            }
        }

        private void PopulaPopUp(int _id)
        {
            biblioDcl = biblioBll.Carregar(_id);

            Funcoes.Funcoes.ControlForm.SetComboBox(ddlSecao, biblioDcl.IdSecao);
            //ddlSecao.Enabled = false;
            fuInserirArquivoSecao.Enabled = false;
            lbEnviarArquivo.Enabled = false;

            tbxTituloArquivo.Text = biblioDcl.NomeArq;
            tbxDescricao.Text = biblioDcl.Descricao;
            tbxAutor.Text = biblioDcl.Autor;
            meAnoPublic.Text = Funcoes.Funcoes.ConvertePara.String(biblioDcl.Ano);
            tbxTituloArquivo.Text = biblioDcl.NomeArq;

            mdInserirArquivoSecao.Show();
        }

        private void Alterar(int _id)
        {
            biblioDcl = biblioBll.Carregar(_id);

            biblioDcl.IdSecao = Funcoes.Funcoes.ConvertePara.Int(
                ddlSecao.SelectedValue);
            biblioDcl.NomeArq = tbxTituloArquivo.Text;
            biblioDcl.Descricao = tbxDescricao.Text;
            biblioDcl.Autor = tbxAutor.Text;
            biblioDcl.Ano = Funcoes.Funcoes.ConvertePara.Int(meAnoPublic.Text);

            biblioDcl.Ativo = true;
            biblioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            biblioDcl.DataCadastro = DateTime.Now;
            biblioDcl.IP = Request.UserHostAddress;

            CacheRemove();
            bllRetorno inserirRet = biblioBll.Alterar(biblioDcl);

            if (inserirRet.retorno)
            {
                PopulaAcordeonSecoes();
                LimparCampos();
                SelecionaAbaAcordeon(Funcoes.Funcoes.ConvertePara.Int(biblioDcl.IdSecao));

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Alteração Efetuada com Sucesso !",
                    "NutroVET informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                mdInserirArquivoSecao.Show();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                    "NutroVET informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Excluir(int _id)
        {
            biblioDcl = biblioBll.Carregar(_id);

            bllRetorno ret = biblioBll.Excluir(biblioDcl);

            if (ret.retorno)
            {
                biblioBll.ExcluirArquivo(biblioDcl);
                PopulaAcordeonSecoes();
                SelecionaAbaAcordeon(Funcoes.Funcoes.ConvertePara.Int(biblioDcl.IdSecao));

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
}