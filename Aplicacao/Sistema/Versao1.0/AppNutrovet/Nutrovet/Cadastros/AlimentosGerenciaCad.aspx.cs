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
    public partial class AlimentosGerenciaCad : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clAlimentosBll alimentoBll = new clAlimentosBll();
        protected Alimentos alimentoDcl;
        protected clAlimentosAuxFontesBll fontesAlimentosBll = new clAlimentosAuxFontesBll();
        protected clAlimentosAuxGruposBll gruposAlimentosBll = new
            clAlimentosAuxGruposBll();
        protected clAlimentosNutrientesBll alimNutrBll = new clAlimentosNutrientesBll();
        protected AlimentoNutriente alimNutrDcl;
        protected clNutrientesAuxGruposBll gruposNutrientesBll = new
            clNutrientesAuxGruposBll();
        protected clNutrientesBll nutrientesBll = new clNutrientesBll();
        protected TONutrientesBll nutrientesTO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (acessosBll.Permissao(
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "2.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        int _idAlimento = Funcoes.Funcoes.ConvertePara.Int(
                            Funcoes.Funcoes.Seguranca.Descriptografar(
                                Funcoes.Funcoes.ConvertePara.String(
                                    Request.QueryString["_idAlimento"])));
                        ViewState["_idAlimento"] = _idAlimento;
                        ViewState["_compartilha"] = false;

                        PopulaTitulo(_idAlimento);
                        PopulaFonte();
                        PopulaGrupo();
                        PopulaAlimento(_idAlimento);
                        PopulaNutrientesGrupos();
                        Page.Form.DefaultFocus = ddlFonte.ClientID;
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaTitulo(int idAlimento)
        {
            if (idAlimento > 0)
            {
                lblTitulo.Text = "Alteração de Alimentos";
                lblPagina.Text = "Gerenciamento";
                lblSubTitulo.Text = "Altere aqui os dados do alimento";
            }
            else
            {
                lblTitulo.Text = "Inserção de Alimentos";
                lblPagina.Text = "Gerenciamento";
                lblSubTitulo.Text = "Insira aqui os dados do alimento que deseja cadastrar";
            }
        }

        private void PopulaAlimento(int idAlimento)
        {
            divCompartilhar.Visible = (Funcoes.Funcoes.ConvertePara.Bool(
                Session["SuperUser"]) ? true : false);

            if (idAlimento > 0)
            {
                alimentoDcl = alimentoBll.Carregar(idAlimento);

                Funcoes.Funcoes.ControlForm.SetComboBox(ddlFonte, alimentoDcl.IdFonte);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlGrupo, alimentoDcl.IdGrupo);
                tbAlimento.Text = alimentoDcl.Alimento;
                //tbNdb.Text = Funcoes.Funcoes.ConvertePara.String(alimentoDcl.NDB_No);

                if (Funcoes.Funcoes.ConvertePara.Bool(alimentoDcl.Compartilhar))
                {
                    CompartilharSim();
                }
                else
                {
                    CompartilharNao();
                }

                acGrupos.SelectedIndex = -1;
            }
        }

        private void PopulaNutrientesGrupos()
        {
            acGrupos.DataSource = gruposNutrientesBll.Listar(true);
            acGrupos.DataBind();
        }

        private void PopulaGrupo()
        {
            ddlFonte.DataValueField = "Id";
            ddlFonte.DataTextField = "Nome";
            ddlFonte.DataSource = fontesAlimentosBll.Listar();
            ddlFonte.DataBind();

            ddlFonte.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaFonte()
        {
            ddlGrupo.DataValueField = "Id";
            ddlGrupo.DataTextField = "Nome";
            ddlGrupo.DataSource = gruposAlimentosBll.Listar();
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        protected void acGrupos_ItemDataBound(object sender, AccordionItemEventArgs e)
        {
            if (e.ItemType == AccordionItemType.Header)
            {
                HiddenField hfId = (HiddenField)e.AccordionItem.FindControl("hfId");

                ViewState["IdGrupo"] = Funcoes.Funcoes.ConvertePara.Int(hfId.Value);
            }

            if (e.ItemType == AccordionItemType.Content)
            {
                Repeater rptNutrientes = (Repeater)e.AccordionItem.FindControl(
                    "rptNutrientes");

                rptNutrientes.DataSource = nutrientesBll.Listar(
                    Funcoes.Funcoes.ConvertePara.Int(ViewState["_idAlimento"]),
                    Funcoes.Funcoes.ConvertePara.Int(ViewState["IdGrupo"]), true);
                rptNutrientes.DataBind();
            }
        }

        protected void rptNutrientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) ||
                (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DropDownList ddlUnidade = (DropDownList)e.Item.FindControl("ddlUnidade");
                LinkButton lbSalvar = (LinkButton)e.Item.FindControl("lbSalvar");
                MEdit meValor = (MEdit)e.Item.FindControl("meValor");
                HiddenField hfIdNutr = (HiddenField)e.Item.FindControl("hfIdNutr");

                int _idNutr = Funcoes.Funcoes.ConvertePara.Int(hfIdNutr.Value);
                int _idAlim = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idAlimento"]);

                nutrientesTO = (TONutrientesBll)e.Item.DataItem;
                
                meValor.Text = (Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["_idAlimento"]) > 0 ? 
                    Funcoes.Funcoes.ConvertePara.String(nutrientesTO.Valor) : "");

                ddlUnidade.Items.AddRange(nutrientesBll.ListarUnidades());
                ddlUnidade.DataBind();
                ddlUnidade.Items.Insert(0, new ListItem("-- Selecione --", "0"));

                if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idAlimento"]) > 0)
                {
                    alimNutrDcl = alimNutrBll.Carregar(_idAlim, _idNutr);

                    ddlUnidade.SelectedValue = (alimNutrDcl != null ?
                        Funcoes.Funcoes.ConvertePara.String(alimNutrDcl.IdUnidade) : 
                        Funcoes.Funcoes.ConvertePara.String(nutrientesTO.IdUnidade));
                }
                else
                {
                    ddlUnidade.SelectedValue = Funcoes.Funcoes.ConvertePara.String(
                        nutrientesTO.IdUnidade);
                }

                lbSalvar.Visible = (Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["_idAlimento"]) > 0 ? true : false);
            }
        }

        protected void rptNutrientes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lbSalvar = (LinkButton)e.Item.FindControl("lbSalvar");
            DropDownList ddlUnidade = (DropDownList)e.Item.FindControl("ddlUnidade");
            MEdit meValor = (MEdit)e.Item.FindControl("meValor");

            int _idNutr = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);
            int _idAlim = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idAlimento"]);

            alimNutrDcl = alimNutrBll.Carregar(_idAlim, _idNutr);

            switch (e.CommandName)
            {
                case "salvar":
                    {
                        alimentoDcl = alimentoBll.Carregar(_idAlim);

                        if (alimNutrDcl != null)
                        {
                            if (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"]) ||
                                ((!Funcoes.Funcoes.ConvertePara.Bool(alimentoDcl.fHomologado)) &&
                                (!Funcoes.Funcoes.ConvertePara.Bool(alimentoDcl.Compartilhar)) &&
                                (alimentoDcl.IdPessoa == Funcoes.Funcoes.ConvertePara.Int(
                                    User.Identity.Name))))
                            {
                                alimNutrDcl.IdUnidade = Funcoes.Funcoes.ConvertePara.Int(
                                                            ddlUnidade.SelectedValue);
                                alimNutrDcl.Valor = Funcoes.Funcoes.ConvertePara.Decimal(
                                    meValor.Text);

                                alimNutrDcl.Ativo = true;
                                alimNutrDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                                    User.Identity.Name);
                                alimNutrDcl.DataCadastro = DateTime.Now;
                                alimNutrDcl.IP = Request.UserHostAddress;

                                bllRetorno _insertRet = alimNutrBll.Alterar(alimNutrDcl);

                                if (_insertRet.retorno)
                                {
                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Success,
                                        _insertRet.mensagem, "NutroVET informa - Alteração",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                                else
                                {
                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Error,
                                        _insertRet.mensagem, "NutroVET informa - Alteração",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                            }
                            else
                            {
                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                                    "Você não possui permissão para ALTERAR!",
                                    "Alteraçâo - NutroVET informa",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);
                            }
                        }
                        else
                        {
                            if (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"]) ||
                                ((!Funcoes.Funcoes.ConvertePara.Bool(alimentoDcl.fHomologado)) &&
                                (!Funcoes.Funcoes.ConvertePara.Bool(alimentoDcl.Compartilhar)) &&
                                (alimentoDcl.IdPessoa == Funcoes.Funcoes.ConvertePara.Int(
                                    User.Identity.Name))))
                            {
                                alimNutrDcl = new AlimentoNutriente();

                                alimNutrDcl.IdAlimento = _idAlim;
                                alimNutrDcl.IdNutr = _idNutr;
                                alimNutrDcl.IdUnidade = Funcoes.Funcoes.ConvertePara.Int(
                                        ddlUnidade.SelectedValue);
                                alimNutrDcl.Valor = Funcoes.Funcoes.ConvertePara.Decimal(
                                    meValor.Text);

                                alimNutrDcl.Ativo = true;
                                alimNutrDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                                    User.Identity.Name);
                                alimNutrDcl.DataCadastro = DateTime.Now;
                                alimNutrDcl.IP = Request.UserHostAddress;

                                bllRetorno _insertRet = alimNutrBll.Inserir(alimNutrDcl);

                                if (_insertRet.retorno)
                                {
                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Success,
                                        _insertRet.mensagem, "NutroVET informa - Inserção",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                                else
                                {
                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Error,
                                        _insertRet.mensagem, "NutroVET informa - Inserção",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                            }
                            else
                            {
                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                                    "Você não possui permissão para INSERIR!",
                                    "Inserção - NutroVET informa",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);
                            }
                        }

                        break;
                    }
            }
        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            CompartilharSim();
        }

        private void CompartilharSim()
        {
            btnSim.CssClass = "btn btn-primary";
            btnNao.CssClass = "btn btn-default";

            ViewState["_compartilha"] = true;
        }

        protected void btnNao_Click(object sender, EventArgs e)
        {
            CompartilharNao();
        }

        private void CompartilharNao()
        {
            btnSim.CssClass = "btn btn-default";
            btnNao.CssClass = "btn btn-primary";

            ViewState["_compartilha"] = false;
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cadastros/AlimentosGerencia.aspx");
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idAlimento"]) > 0)
            {
                Alterar(Funcoes.Funcoes.ConvertePara.Int(ViewState["_idAlimento"]));
            }
            else
            {
                Inserir(Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"]));
            }
        }

        private void Alterar(int _id)
        {
            bllRetorno updateRet;

            alimentoDcl = alimentoBll.Carregar(_id);

            if (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"]) ||
                ((!Funcoes.Funcoes.ConvertePara.Bool(alimentoDcl.fHomologado)) && 
                (!Funcoes.Funcoes.ConvertePara.Bool(alimentoDcl.Compartilhar)) && 
                (alimentoDcl.IdPessoa == Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name))))
            {
                alimentoDcl.IdGrupo = Funcoes.Funcoes.ConvertePara.Int(ddlGrupo.SelectedValue);
                alimentoDcl.IdFonte = Funcoes.Funcoes.ConvertePara.Int(ddlFonte.SelectedValue);
                alimentoDcl.Compartilhar = (
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"]) ?
                        Funcoes.Funcoes.ConvertePara.Bool(ViewState["_compartilha"]) : false);
                //alimentoDcl.NDB_No = Funcoes.Funcoes.ConvertePara.Int(tbNdb.Text);
                alimentoDcl.Alimento = tbAlimento.Text;

                alimentoDcl.Ativo = true;
                alimentoDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
                alimentoDcl.DataCadastro = DateTime.Now;
                alimentoDcl.IP = Request.UserHostAddress;

                updateRet = alimentoBll.Alterar(alimentoDcl);

                if (updateRet.retorno)
                {
                    TOToastr _tostr = new TOToastr
                    {
                        Tipo = 'S',
                        Titulo = "NutroVET informa - Alteração",
                        Mensagem = updateRet.mensagem
                    };
                    Session["ToastrAlimentos"] = _tostr;
                    Response.Redirect("~/Cadastros/AlimentosGerencia.aspx");
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Error, updateRet.mensagem,
                        "NutroVET informa - Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                } 
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Você não possui permissão para ALTERAR!",
                    "Alteraçâo - NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Inserir(bool _superUser)
        {
            bllRetorno insertRet;

            alimentoDcl = new Alimentos();

            alimentoDcl.IdPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            alimentoDcl.IdGrupo = Funcoes.Funcoes.ConvertePara.Int(ddlGrupo.SelectedValue);
            alimentoDcl.IdFonte = Funcoes.Funcoes.ConvertePara.Int(ddlFonte.SelectedValue);
            alimentoDcl.Compartilhar = (_superUser ?
                    Funcoes.Funcoes.ConvertePara.Bool(ViewState["_compartilha"]) : false);
            alimentoDcl.fHomologado = (_superUser ? true : false);
            alimentoDcl.DataHomol = (_superUser ? DateTime.Today : 
                DateTime.Parse("01/01/1910"));
            alimentoDcl.Alimento = tbAlimento.Text;

            alimentoDcl.Ativo = true;
            alimentoDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            alimentoDcl.DataCadastro = DateTime.Now;
            alimentoDcl.IP = Request.UserHostAddress;

            #region Popula Nutrientes
            foreach (AccordionPane acPaneis in acGrupos.Panes)
            {
                foreach (var itemPane in acPaneis.ContentContainer.Controls)
                {
                    if (itemPane.GetType() == typeof(Repeater))
                    {
                        Repeater rpNutrientes = (Repeater)itemPane;
                        HiddenField hfIdNutr;
                        MEdit meValor;
                        DropDownList ddlUnidade;

                        foreach (RepeaterItem item in rpNutrientes.Items)
                        {
                            meValor = (MEdit)item.FindControl("meValor");
                            hfIdNutr = (HiddenField)item.FindControl("hfIdNutr");
                            ddlUnidade = (DropDownList)item.FindControl("ddlUnidade");

                            if (meValor.Text != "")
                            {
                                alimNutrDcl = new AlimentoNutriente();

                                alimNutrDcl.IdNutr = Funcoes.Funcoes.ConvertePara.Int(
                                    hfIdNutr.Value);
                                alimNutrDcl.Valor = Funcoes.Funcoes.ConvertePara.Decimal(
                                    meValor.Text);
                                alimNutrDcl.IdUnidade = Funcoes.Funcoes.ConvertePara.Int(
                                    ddlUnidade.SelectedValue);

                                alimNutrDcl.Ativo = true;
                                alimNutrDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                                    User.Identity.Name);
                                alimNutrDcl.DataCadastro = DateTime.Now;
                                alimNutrDcl.IP = Request.UserHostAddress;

                                alimentoDcl.AlimentoNutriente.Add(alimNutrDcl);
                            }
                        }
                    }
                }
            }
            #endregion

            insertRet = alimentoBll.Inserir(alimentoDcl);

            if (insertRet.retorno)
            {
                TOToastr _tostr = new TOToastr
                {
                    Tipo = 'S',
                    Titulo = "NutroVET informa - Inserção",
                    Mensagem = insertRet.mensagem
                };
                Session["ToastrAlimentos"] = _tostr;
                Response.Redirect("~/Cadastros/AlimentosGerencia.aspx");
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, insertRet.mensagem,
                    "Inserção", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }
    }
}