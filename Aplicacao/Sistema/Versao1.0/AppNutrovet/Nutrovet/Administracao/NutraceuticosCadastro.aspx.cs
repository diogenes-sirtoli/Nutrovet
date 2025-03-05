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
using System.Web.Services;

namespace Nutrovet.Administracao
{
    public partial class NutraceuticosCadastro : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clAnimaisAuxEspeciesBll especiesBll = new clAnimaisAuxEspeciesBll();
        protected AnimaisAuxEspecy especiesDcl;
        protected NutrientesAuxGrupo grupoNutriDcl;
        protected clNutrientesAuxGruposBll grupoNutriBll = new
            clNutrientesAuxGruposBll();
        protected Nutriente nutriDcl;
        protected clNutrientesBll nutriBll = new clNutrientesBll();
        protected clPrescricaoAuxTiposBll prescrBll = new clPrescricaoAuxTiposBll();
        protected clNutraceuticosBll nutraBll = new clNutraceuticosBll();
        protected Nutraceutico nutraDcl;
        protected TONutraceuticosBll nutraTO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (acessosBll.Permissao(
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "9.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        int _idNutrac = Funcoes.Funcoes.ConvertePara.Int(
                            Funcoes.Funcoes.Seguranca.Descriptografar(
                                Funcoes.Funcoes.ConvertePara.String(
                                    Request.QueryString["_idNutrac"])));
                        int _idEspecie = Funcoes.Funcoes.ConvertePara.Int(
                            Funcoes.Funcoes.Seguranca.Descriptografar(
                                Funcoes.Funcoes.ConvertePara.String(
                                    Request.QueryString["_idEspecie"])));

                        ViewState["_idNutrac"] = _idNutrac;
                        ViewState["_idEspecie"] = _idEspecie;

                        tbEspecie.Text = especiesBll.Carregar(_idEspecie).Especie;

                        PopulaGrupoNutri();
                        PopulaUnidMin();
                        PopulaUnidMax();
                        PopulaInterval1();
                        PopulaInterval2();

                        PopulaTela(_idNutrac);
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaInterval2()
        {
            ddlMIntervalo2.DataValueField = "Id";
            ddlMIntervalo2.DataTextField = "Nome";
            ddlMIntervalo2.DataSource = prescrBll.Listar();
            ddlMIntervalo2.DataBind();

            ddlMIntervalo2.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaInterval1()
        {
            ddlMIntervalo1.DataValueField = "Id";
            ddlMIntervalo1.DataTextField = "Nome";
            ddlMIntervalo1.DataSource = prescrBll.Listar();
            ddlMIntervalo1.DataBind();

            ddlMIntervalo1.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaUnidMax()
        {
            ddlMUnDoseMaxima.Items.AddRange(nutraBll.ListarUnidades());
            ddlMUnDoseMaxima.DataBind();

            ddlMUnDoseMaxima.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaUnidMin()
        {
            ddlMUnDoseMinima.Items.AddRange(nutraBll.ListarUnidades());
            ddlMUnDoseMinima.DataBind();

            ddlMUnDoseMinima.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaTela(int _idNutrac)
        {
            if (_idNutrac > 0)
            {
                nutraTO = nutraBll.CarregarTO(_idNutrac);

                lblTitulo.Text = "Alteração de Nutracêuticos";
                lblPagina.Text = "Alterar Nutracêutico";
                lblSubTitulo.Text = "Altere aqui os dados do Nutracêutico!";

                Funcoes.Funcoes.ControlForm.SetComboBox(ddlGrpNutri,
                    nutraTO.IdGrupo);
                ddlGrpNutri.Visible = false;
                tbGrpNutri.Text = ddlGrpNutri.SelectedItem.Text;
                tbGrpNutri.Visible = true;

                PopulaNutri(nutraTO.IdGrupo);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlNutri,
                    nutraTO.IdNutr);
                ddlNutri.Visible = false;
                tbNutri.Text = ddlNutri.SelectedItem.Text;
                tbNutri.Visible = true;

                meMDoseMinima.Text = Funcoes.Funcoes.ConvertePara.String(
                    nutraTO.DoseMin);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlMUnDoseMinima,
                    nutraTO.IdUnidMin);
                meMDoseMaxima.Text = Funcoes.Funcoes.ConvertePara.String(
                    nutraTO.DoseMax);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlMUnDoseMaxima,
                    nutraTO.IdUnidMax);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlMIntervalo1,
                    nutraTO.IdPrescr1);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlMIntervalo2,
                    nutraTO.IdPrescr2);
                tbxObs.Text = nutraTO.Obs;
            }
            else
            {
                lblTitulo.Text = "Inserção de Nutracêuticos";
                lblPagina.Text = "Inserir Nutracêutico";
                lblSubTitulo.Text = "Insira aqui os dados do Nutracêutico!";
            }
        }

        private void PopulaGrupoNutri()
        {
            ddlGrpNutri.DataValueField = "IdGrupo";
            ddlGrpNutri.DataTextField = "Grupo";
            ddlGrpNutri.DataSource = grupoNutriBll.Listar();
            ddlGrpNutri.DataBind();

            ddlGrpNutri.Items.Insert(0, new ListItem("-- Selecione --", "0"));
            ddlNutri.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaNutri(int _idGrp)
        {
            ddlNutri.DataValueField = "IdNutr";
            ddlNutri.DataTextField = "Nutriente";
            ddlNutri.DataSource = nutriBll.Listar(0, _idGrp, false);
            ddlNutri.DataBind();

            ddlNutri.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        protected void ddlGrpNutri_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaNutri(Funcoes.Funcoes.ConvertePara.Int(
                ddlGrpNutri.SelectedValue));
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            int _idNutrac = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idNutrac"]);

            Salvar(_idNutrac);
        }

        protected void Salvar(int _id)
        {
            if (_id > 0)
            {
                //if ((Funcoes.Funcoes.ConvertePara.Bool(Session["Alterar"])) ||
                //    (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                //{
                    Alterar(_id);
                //}
                //else
                //{
                //    Funcoes.Funcoes.Toastr.ShowToast(this,
                //        Funcoes.Funcoes.Toastr.ToastType.Warning,
                //        "Você não possui permissão para ALTERAR!",
                //        "NutroVET informa - Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                //        true);
                //}
            }
            else
            {
                //if ((Funcoes.Funcoes.ConvertePara.Bool(Session["Inserir"])) ||
                //    (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                //{
                    Inserir();
                //}
                //else
                //{
                //    Funcoes.Funcoes.Toastr.ShowToast(this,
                //        Funcoes.Funcoes.Toastr.ToastType.Warning,
                //        "Você não possui permissão para INSERIR!",
                //        "NutroVET informa - Inserção", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                //        true);
                //}
            }
        }

        private void Inserir()
        {
            nutraDcl = new Nutraceutico();

            nutraDcl.IdEspecie = Funcoes.Funcoes.ConvertePara.Int(
                ViewState["_idEspecie"]);
            nutraDcl.IdNutr = Funcoes.Funcoes.ConvertePara.Int(ddlNutri.SelectedValue);
            nutraDcl.DoseMin = Funcoes.Funcoes.ConvertePara.Decimal(meMDoseMinima.Text);
            nutraDcl.IdUnidMin = Funcoes.Funcoes.ConvertePara.Int(
                ddlMUnDoseMinima.SelectedValue);
            nutraDcl.DoseMax = Funcoes.Funcoes.ConvertePara.Decimal(meMDoseMaxima.Text);
            nutraDcl.IdUnidMax = Funcoes.Funcoes.ConvertePara.Int(
                ddlMUnDoseMaxima.SelectedValue);
            nutraDcl.IdPrescr1 = Funcoes.Funcoes.ConvertePara.Int(
                ddlMIntervalo1.SelectedValue);
            nutraDcl.IdPrescr2 = Funcoes.Funcoes.ConvertePara.Int(
                ddlMIntervalo2.SelectedValue);

            nutraDcl.Obs = tbxObs.Text;

            nutraDcl.Ativo = true;
            nutraDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            nutraDcl.DataCadastro = DateTime.Now;
            nutraDcl.IP = Request.UserHostAddress;

            bllRetorno inserirRet = nutraBll.Inserir(nutraDcl);

            if (inserirRet.retorno)
            {
                Cancelar();

                TOToastr _tostr = new TOToastr
                {
                    Tipo = 'S',
                    Titulo = "NutroVET informa - Inserção",
                    Mensagem = inserirRet.mensagem
                };
                Session["ToastrPacientes"] = _tostr;
                Response.Redirect("~/Administracao/NutraceuticosSelecao.aspx");
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                    "NutroVET informa - Inserção", 
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
            }
        }

        private void Cancelar()
        {
            ViewState.Remove("_idNutrac");
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlGrpNutri, 0);
            PopulaNutri(0);
            meMDoseMinima.Text = "";
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlMUnDoseMinima, 0);
            meMDoseMaxima.Text = "";
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlMUnDoseMaxima, 0);
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlMIntervalo1, 0);
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlMIntervalo2, 0);
            tbxObs.Text = "";
        }

        private void Alterar(int _id)
        {
            nutraDcl = nutraBll.Carregar(_id);

            nutraDcl.DoseMin = Funcoes.Funcoes.ConvertePara.Decimal(meMDoseMinima.Text);
            nutraDcl.IdUnidMin = Funcoes.Funcoes.ConvertePara.Int(
                ddlMUnDoseMinima.SelectedValue);
            nutraDcl.DoseMax = Funcoes.Funcoes.ConvertePara.Decimal(meMDoseMaxima.Text);
            nutraDcl.IdUnidMax = Funcoes.Funcoes.ConvertePara.Int(
                ddlMUnDoseMaxima.SelectedValue);
            nutraDcl.IdPrescr1 = Funcoes.Funcoes.ConvertePara.Int(
                ddlMIntervalo1.SelectedValue);
            nutraDcl.IdPrescr2 = Funcoes.Funcoes.ConvertePara.Int(
                ddlMIntervalo2.SelectedValue);
            nutraDcl.Obs = tbxObs.Text;
            nutraDcl.Ativo = true;
            nutraDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            nutraDcl.DataCadastro = DateTime.Now;
            nutraDcl.IP = Request.UserHostAddress;

            bllRetorno inserirRet = nutraBll.Alterar(nutraDcl);

            if (inserirRet.retorno)
            {
                Cancelar();

                TOToastr _tostr = new TOToastr
                {
                    Tipo = 'S',
                    Titulo = "NutroVET informa - Alteração",
                    Mensagem = inserirRet.mensagem
                };
                Session["ToastrNutraceuticos"] = _tostr;
                Response.Redirect("~/Administracao/NutraceuticosSelecao.aspx");
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                    "NutroVET informa - Alteração", 
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
            }
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Session.Remove("ToastrNutraceuticos");
            Response.Redirect("~/Administracao/NutraceuticosSelecao.aspx");
        }
    }
}
