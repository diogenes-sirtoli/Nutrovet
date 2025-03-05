using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;

namespace Nutrovet.Cadastros
{
    public partial class DietasCad : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll PessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected clAlimentosBll alimentoBll = new clAlimentosBll();
        protected Alimentos alimentoDcl;
        protected clDietasBll dietasBll = new clDietasBll();
        protected Dietas dietasDcl;
        protected clDietasAlimentosBll dietasAlimBll = new clDietasAlimentosBll();
        protected DietasAlimento dietasAlimDcl;
        protected AnimaisAuxRaca racasDcl;
        protected clAnimaisAuxEspeciesBll especiesBll = new clAnimaisAuxEspeciesBll();

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
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "5.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        int _idDieta = Funcoes.Funcoes.ConvertePara.Int(
                            Funcoes.Funcoes.Seguranca.Descriptografar(
                                Funcoes.Funcoes.ConvertePara.String(
                                    Request.QueryString["_idDieta"])));
                        ViewState["_idDieta"] = _idDieta;

                        PopulaEspecie();
                        PopulaTela(_idDieta);
                        Page.Form.DefaultFocus = tbDieta.ClientID;
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaEspecie()
        {
            ddlEspecie.DataValueField = "Id";
            ddlEspecie.DataTextField = "Nome";
            ddlEspecie.DataSource = especiesBll.Listar();
            ddlEspecie.DataBind();

            ddlEspecie.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaTela(int _idDieta)
        {
            if (_idDieta > 0)
            {
                dietasDcl = dietasBll.Carregar(_idDieta);

                lblTitulo.Text = "Alterar Dieta";
                lblPagina.Text = "Tipos de Dietas";
                lblSubTitulo.Text = "Altere aqui os dados da dieta!";

                CarregaListaIndicados(_idDieta);
                CarregaListaContraindicados(_idDieta);

                Funcoes.Funcoes.ControlForm.SetComboBox(ddlEspecie, dietasDcl.IdEspecie);
                tbDieta.Text = dietasDcl.Dieta;
                tbCarboidrato.Text = Funcoes.Funcoes.ConvertePara.String(
                    dietasDcl.Carboidrato);
                tbProteina.Text = Funcoes.Funcoes.ConvertePara.String(
                    dietasDcl.Proteina);
                tbGordura.Text = Funcoes.Funcoes.ConvertePara.String(
                    dietasDcl.Gordura);
            }
            else
            {
                lblTitulo.Text = "Inserir Dieta";
                lblPagina.Text = "Tipos de Dietas";
                lblSubTitulo.Text = "Insira aqui os dados da dieta!";
            }

            divPesqAlimIndic.Visible = false;
            divPesqAlimContra.Visible = false;
        }

        private void CarregaListaContraindicados(int idDieta)
        {
            List<TODietasAlimentosBll> _contraindicadosBanco = dietasAlimBll.ListarTO(
                idDieta, Funcoes.Funcoes.ConvertePara.Int(
                    DominiosBll.DietasAlimentosRecomendacao.Contraindicado));

            List<Alimentos> alimentosContra = (
                (List<Alimentos>)Session["alimentosContra"] != null ?
                (List<Alimentos>)Session["alimentosContra"] :
                new List<Alimentos>());

            if (_contraindicadosBanco.Count > 0)
            {
                foreach (TODietasAlimentosBll item in _contraindicadosBanco)
                {
                    alimentoDcl = new Alimentos();

                    alimentoDcl.IdAlimento = item.IdAlimento;
                    alimentoDcl.Alimento = item.Alimento;

                    alimentosContra.Add(alimentoDcl);
                }

                Session["alimentosContra"] = alimentosContra;

                PopulaRptAlimContraindicados(alimentosContra);
            }
        }

        private void CarregaListaIndicados(int idDieta)
        {
            List<TODietasAlimentosBll> _indicadosBanco = dietasAlimBll.ListarTO(idDieta, 
                Funcoes.Funcoes.ConvertePara.Int(
                    DominiosBll.DietasAlimentosRecomendacao.Indicado));

            List<Alimentos> alimentosIndicados = (
                (List<Alimentos>)Session["alimentosIndicados"] != null ?
                (List<Alimentos>)Session["alimentosIndicados"] :
                new List<Alimentos>());

            if (_indicadosBanco.Count > 0)
            {
                foreach (TODietasAlimentosBll item in _indicadosBanco)
                {
                    alimentoDcl = new Alimentos();

                    alimentoDcl.IdAlimento = item.IdAlimento;
                    alimentoDcl.Alimento = item.Alimento;

                    alimentosIndicados.Add(alimentoDcl);
                }

                Session["alimentosIndicados"] = alimentosIndicados;

                PopulaRptAlimIndicados(alimentosIndicados);
            }
        }

        private void PopulaListaIndicados(string _alimento)
        {
            ltbAlimIndic.DataTextField = "Alimento";
            ltbAlimIndic.DataValueField = "IdAlimento";
            ltbAlimIndic.DataSource = alimentoBll.Listar(_alimento,
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
            ltbAlimIndic.DataBind();

            if (ltbAlimIndic.Items.Count > 0)
            {
                divPesqAlimIndic.Visible = true;
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    @"<b>O Alimento Informado NÃO foi Encontrado</b> !",
                    "Alimentos Indicados",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void PopulaListaContraindicados(string _alimento)
        {
            ltbAlimContra.DataTextField = "Alimento";
            ltbAlimContra.DataValueField = "IdAlimento";
            ltbAlimContra.DataSource = alimentoBll.Listar(_alimento,
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
            ltbAlimContra.DataBind();

            if (ltbAlimContra.Items.Count > 0)
            {
                divPesqAlimContra.Visible = true;
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    @"<b>O Alimento Informado NÃO foi Encontrado</b> !",
                    "Alimentos Contraindicados",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Session.Remove("ToastrDietas");
            Session.Remove("alimentosIndicados");
            Session.Remove("alimentosContra");

            Response.Redirect("~/Cadastros/DietasSelecao.aspx");
        }

        protected void btnPesqIndic_Click(object sender, EventArgs e)
        {
            if (tbPesqIndic.Text.Count() > 3)
            {
                PopulaListaIndicados(tbPesqIndic.Text);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Info,
                    @"São Necessários mais de <b>3</b> Caracteres !",
                    "Alimentos Indicados",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void btnPesqContra_Click(object sender, EventArgs e)
        {
            if (tbPesqContra.Text.Count() > 3)
            {
                PopulaListaContraindicados(tbPesqContra.Text);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Info,
                    @"São Necessários mais de <b>3</b> Caracteres !",
                    "Alimentos Contraindicados",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void PopulaRptAlimIndicados(List<Alimentos> alimentosListagem)
        {
            rptAlimIndicados.DataSource = alimentosListagem.OrderBy(
                    o => o.Alimento).ToList();
            rptAlimIndicados.DataBind();
        }

        private void PopulaRptAlimContraindicados(List<Alimentos> alimentosListagem)
        {
            rptAlimContra.DataSource = alimentosListagem.OrderBy(
                    o => o.Alimento).ToList();
            rptAlimContra.DataBind();
        }

        protected void lbFechaPesqAlimIndic_Click(object sender, EventArgs e)
        {
            List<Alimentos> alimentosIndicados = (
                (List<Alimentos>)Session["alimentosIndicados"] != null ? 
                (List<Alimentos>)Session["alimentosIndicados"] : 
                new List<Alimentos>());

            foreach (ListItem item in ltbAlimIndic.Items)
            {
                if (item.Selected)
                {
                    if (alimentosIndicados.Where(w => w.Alimento == item.Text).Count() <= 0)
                    {
                        alimentoDcl = new Alimentos();

                        alimentoDcl.Alimento = item.Text;
                        alimentoDcl.IdAlimento = Funcoes.Funcoes.ConvertePara.Int(
                            item.Value);

                        if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idDieta"]) > 0)
                        {
                            dietasAlimDcl = new DietasAlimento();

                            dietasAlimDcl.IdDieta = Funcoes.Funcoes.ConvertePara.Int(
                                ViewState["_idDieta"]);
                            dietasAlimDcl.IdAlimento = alimentoDcl.IdAlimento;
                            dietasAlimDcl.IdTpIndicacao = Funcoes.Funcoes.ConvertePara.Int(
                                DominiosBll.DietasAlimentosRecomendacao.Indicado);

                            dietasAlimDcl.Ativo = true;
                            dietasAlimDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                                User.Identity.Name);
                            dietasAlimDcl.DataCadastro = DateTime.Now;
                            dietasAlimDcl.IP = Request.UserHostAddress;

                            bllRetorno _ret = dietasAlimBll.Inserir(dietasAlimDcl);

                            if (_ret.retorno)
                            {
                                alimentosIndicados.Add(alimentoDcl);
                            }
                        }
                        else
                        {
                            alimentosIndicados.Add(alimentoDcl);
                        }
                    }
                }
            }

            if (alimentosIndicados.Count > 0)
            {
                divPesqAlimIndic.Visible = false;
                rptAlimIndicados.Visible = true;
                tbPesqIndic.Text = "";

                Session["alimentosIndicados"] = alimentosIndicados;

                PopulaRptAlimIndicados(alimentosIndicados);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Info,
                    @"Não Foram Selecionados Alimentos !",
                    "Alimentos Indicados",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void rptAlimIndicados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _idAlimento = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "excluir":
                    {
                        if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idDieta"]) > 0)
                        {
                            ExcluirIndicados(
                                Funcoes.Funcoes.ConvertePara.Int(ViewState["_idDieta"]), 
                                _idAlimento);
                        }
                        else
                        {
                            ExcluirOffLineIndicados(_idAlimento);
                        }

                        break;
                    }
            }
        }

        private void ExcluirIndicados(int _idDieta, int _idAlimento)
        {
            bllRetorno _ret = dietasAlimBll.Excluir(_idDieta, _idAlimento);

            if (_ret.retorno)
            {
                Session.Remove("alimentosIndicados");

                CarregaListaIndicados(_idDieta);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Não Foi Possível EXCLUIR o Registro",
                    "Exclusão - NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void ExcluirOffLineIndicados(int id)
        {
            List<Alimentos> alimentosIndicados = (
                (List<Alimentos>)Session["alimentosIndicados"] != null ?
                (List<Alimentos>)Session["alimentosIndicados"] :
                new List<Alimentos>());

            if (alimentosIndicados != null)
            {
                alimentosIndicados.Remove(alimentosIndicados.First(w => w.IdAlimento == id));

                PopulaRptAlimIndicados(alimentosIndicados); 
            }
        }

        private void ExcluirOffLineContraindicados(int id)
        {
            List<Alimentos> alimentosContra = (
                (List<Alimentos>)Session["alimentosContra"] != null ?
                (List<Alimentos>)Session["alimentosContra"] :
                new List<Alimentos>());

            if (alimentosContra != null)
            {
                alimentosContra.Remove(alimentosContra.First(w => w.IdAlimento == id));

                PopulaRptAlimContraindicados(alimentosContra);
            }
        }

        protected void lbFechaPesqAlimContra_Click(object sender, EventArgs e)
        {
            List<Alimentos> alimentosContra = (
                (List<Alimentos>)Session["alimentosContra"] != null ?
                (List<Alimentos>)Session["alimentosContra"] :
                new List<Alimentos>());

            foreach (ListItem item in ltbAlimContra.Items)
            {
                if (item.Selected)
                {
                    if (alimentosContra.Where(w => w.Alimento == item.Text).Count() <= 0)
                    {
                        alimentoDcl = new Alimentos();

                        alimentoDcl.Alimento = item.Text;
                        alimentoDcl.IdAlimento = Funcoes.Funcoes.ConvertePara.Int(
                            item.Value);

                        if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idDieta"]) > 0)
                        {
                            dietasAlimDcl = new DietasAlimento();

                            dietasAlimDcl.IdDieta = Funcoes.Funcoes.ConvertePara.Int(
                                ViewState["_idDieta"]);
                            dietasAlimDcl.IdAlimento = alimentoDcl.IdAlimento;
                            dietasAlimDcl.IdTpIndicacao = Funcoes.Funcoes.ConvertePara.Int(
                                DominiosBll.DietasAlimentosRecomendacao.Contraindicado);

                            dietasAlimDcl.Ativo = true;
                            dietasAlimDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                                User.Identity.Name);
                            dietasAlimDcl.DataCadastro = DateTime.Now;
                            dietasAlimDcl.IP = Request.UserHostAddress;

                            bllRetorno _ret = dietasAlimBll.Inserir(dietasAlimDcl);

                            if (_ret.retorno)
                            {
                                alimentosContra.Add(alimentoDcl);
                            }
                        }
                        else
                        {
                            alimentosContra.Add(alimentoDcl);
                        }
                    }
                }
            }

            if (alimentosContra.Count > 0)
            {
                divPesqAlimContra.Visible = false;
                rptAlimContra.Visible = true;
                tbPesqContra.Text = "";

                Session["alimentosContra"] = alimentosContra;

                PopulaRptAlimContraindicados(alimentosContra);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Info,
                    @"Não Foram Selecionados Alimentos !",
                    "Alimentos Indicados",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void rptAlimContra_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _idAlimento = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "excluir":
                    {
                        if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idDieta"]) > 0)
                        {
                            ExcluirContraindicados(
                                Funcoes.Funcoes.ConvertePara.Int(ViewState["_idDieta"]),
                                _idAlimento);
                        }
                        else
                        {
                            ExcluirOffLineContraindicados(_idAlimento);
                        }
                        

                        break;
                    }
            }
        }

        private void ExcluirContraindicados(int _idDieta, int _idAlimento)
        {
            bllRetorno _ret = dietasAlimBll.Excluir(_idDieta, _idAlimento);

            if (_ret.retorno)
            {
                Session.Remove("alimentosContra");

                CarregaListaContraindicados(_idDieta);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Não Foi Possível EXCLUIR o Registro",
                    "Exclusão - NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idDieta"]) > 0)
            {
                //if ((Funcoes.Funcoes.ConvertePara.Bool(Session["Alterar"])) ||
                //    (Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                //{
                    Alterar(Funcoes.Funcoes.ConvertePara.Int(ViewState["_idDieta"]));
                //}
                //else
                //{
                //    Funcoes.Funcoes.Toastr.ShowToast(this,
                //        Funcoes.Funcoes.Toastr.ToastType.Warning,
                //        "Você não possui permissão para ALTERAR!",
                //        "Alteraçâo - NutroVET informa", 
                //        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
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
                //Funcoes.Funcoes.Toastr.ShowToast(this,
                //    Funcoes.Funcoes.Toastr.ToastType.Warning,
                //    "Você não possui permissão para INSERIR!",
                //    "Inserção - NutroVET informa", 
                //    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                //    true);
                //}
            }
        }

        private void Inserir()
        {
            bllRetorno ret = new bllRetorno();
            dietasDcl = new Dietas();
            List<Alimentos> alimentosIndicados = 
                (List<Alimentos>)Session["alimentosIndicados"];
            List<Alimentos> alimentosContra = 
                (List<Alimentos>)Session["alimentosContra"];

            dietasDcl.IdEspecie = Funcoes.Funcoes.ConvertePara.Int(ddlEspecie.SelectedValue);
            dietasDcl.Dieta = tbDieta.Text;
            dietasDcl.Carboidrato = Funcoes.Funcoes.ConvertePara.Int(
                tbCarboidrato.Text);
            dietasDcl.Proteina = Funcoes.Funcoes.ConvertePara.Int(
                tbProteina.Text);
            dietasDcl.Gordura = Funcoes.Funcoes.ConvertePara.Int(
                tbGordura.Text);

            dietasDcl.Ativo = true;
            dietasDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            dietasDcl.DataCadastro = DateTime.Now;
            dietasDcl.IP = Request.UserHostAddress;

            if (alimentosIndicados != null)
            {
                foreach (Alimentos item in alimentosIndicados)
                {
                    dietasAlimDcl = new DietasAlimento();

                    dietasAlimDcl.IdAlimento = item.IdAlimento;
                    dietasAlimDcl.IdTpIndicacao = Funcoes.Funcoes.ConvertePara.Int(
                        DominiosBll.DietasAlimentosRecomendacao.Indicado);
                    dietasAlimDcl.Quant = 0;

                    dietasAlimDcl.Ativo = true;
                    dietasAlimDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                    dietasAlimDcl.DataCadastro = DateTime.Now;
                    dietasAlimDcl.IP = Request.UserHostAddress;

                    dietasDcl.DietasAlimentos.Add(dietasAlimDcl);
                }
            }

            if (alimentosContra != null)
            {
                foreach (Alimentos item in alimentosContra)
                {
                    dietasAlimDcl = new DietasAlimento();

                    dietasAlimDcl.IdAlimento = item.IdAlimento;
                    dietasAlimDcl.IdTpIndicacao = Funcoes.Funcoes.ConvertePara.Int(
                        DominiosBll.DietasAlimentosRecomendacao.Contraindicado);
                    dietasAlimDcl.Quant = 0;

                    dietasAlimDcl.Ativo = true;
                    dietasAlimDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                    dietasAlimDcl.DataCadastro = DateTime.Now;
                    dietasAlimDcl.IP = Request.UserHostAddress;

                    dietasDcl.DietasAlimentos.Add(dietasAlimDcl);
                }
            }

            ret = dietasBll.Inserir(dietasDcl);

            if (ret.retorno)
            {
                TOToastr _tostr = new TOToastr();

                Session.Remove("alimentosIndicados");
                Session.Remove("alimentosContra");

                _tostr.Tipo = 'S';
                _tostr.Titulo = "Inserção - NutroVET informa";
                _tostr.Mensagem = ret.mensagem;

                Session["ToastrDietas"] = _tostr;

                Response.Redirect("~/Cadastros/DietasSelecao.aspx");
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, ret.mensagem,
                    "Inserção - NutroVET informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Alterar(int _idDieta)
        {
            bllRetorno ret = new bllRetorno();
            dietasDcl = dietasBll.Carregar(_idDieta);

            dietasDcl.IdEspecie = Funcoes.Funcoes.ConvertePara.Int(ddlEspecie.SelectedValue);
            dietasDcl.Dieta = tbDieta.Text;
            dietasDcl.Carboidrato = Funcoes.Funcoes.ConvertePara.Int(
                tbCarboidrato.Text);
            dietasDcl.Proteina = Funcoes.Funcoes.ConvertePara.Int(
                tbProteina.Text);
            dietasDcl.Gordura = Funcoes.Funcoes.ConvertePara.Int(
                tbGordura.Text);

            dietasDcl.Ativo = true;
            dietasDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            dietasDcl.DataCadastro = DateTime.Now;
            dietasDcl.IP = Request.UserHostAddress;

            ret = dietasBll.Alterar(dietasDcl);

            if (ret.retorno)
            {
                TOToastr _tostr = new TOToastr();

                Session.Remove("alimentosIndicados");
                Session.Remove("alimentosContra");

                _tostr.Tipo = 'S';
                _tostr.Titulo = "Alteração - NutroVET informa";
                _tostr.Mensagem = ret.mensagem;

                Session["ToastrDietas"] = _tostr;

                Response.Redirect("~/Cadastros/DietasSelecao.aspx");
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, ret.mensagem,
                    "Alteração - NutroVET informa", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }
    }
}