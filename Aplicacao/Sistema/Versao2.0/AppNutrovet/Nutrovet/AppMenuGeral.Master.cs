using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using DCL;
using BLL;

namespace Nutrovet
{
    public partial class AppMenuGeral : System.Web.UI.MasterPage
    {
        protected clAcessosBll logonBll = new clAcessosBll();
        protected Acesso logonDcl = new Acesso();
        protected clPessoasBll pessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected clNutraceuticosBll nutraBll = new clNutraceuticosBll();
        protected Nutraceutico nutraDcl;
        protected TONutraceuticosBll nutraTO;
        protected clAnimaisAuxEspeciesBll especiesBll = new
            clAnimaisAuxEspeciesBll();
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clAcessosVigenciaPlanosBll planosBll = new clAcessosVigenciaPlanosBll();
        protected AcessosVigenciaPlano planoDcl;
        protected TOAcessosVigenciaPlanosBll planoTO;

        private string _tela = "";
        private int? _idPessoa = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    if (Session["_dadosBasicos"] != null)
                    {
                        pessoaDcl = (Pessoa)Session["_dadosBasicos"];
                        lblUsuario.Text = pessoaDcl.Nome;
                        lblTpPessoa.Text =
                            Funcoes.Funcoes.CarregarEnumNome<DominiosBll.PessoasAuxTipos>(
                                pessoaDcl.IdTpPessoa);
                        _idPessoa = pessoaDcl.IdOperador;

                        PopulaMenu(pessoaDcl.IdPessoa);
                        PopulaEspecie();
                        PopulaImagemPerfil(pessoaDcl.IdPessoa);
                    }
                    else
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                }
            }
        }

        private void PopulaImagemPerfil(int idPessoa)
        {
            if ((imgUserIcon.ImageUrl == "") || (imgUserIcon.ImageUrl == "~/Imagens/user1.png"))
            {
                imgUserIcon.ImageUrl = pessoaBll.CarregarImagem(idPessoa);
            }
        }

        private void PopulaGrid(int _idEspecie)
        {
            List<TONutraceuticosBll> racasListagem = nutraBll.Listar(_idEspecie);

            rptListNutra.DataSource = racasListagem;
            rptListNutra.DataBind();
        }

        private void PopulaMenu(int idPessoa)
        {
            acessosDcl = acessosBll.Carregar(idPessoa);

            if (acessosDcl != null)
            {
                if (!Funcoes.Funcoes.ConvertePara.Bool(acessosDcl.SuperUser))
                {
                    if (planosBll.PlanoEstaNaVigencia(idPessoa))
                    {
                        hlAdministracao.Visible = false;
                    }
                    else
                    {
                        menuLateral1.Visible = false;
                        menuLateral2.Visible = false;
                        menuLateral3.Visible = false;
                        menuLateral4.Visible = false;
                        menuLateral5.Visible = false;
                        menuLateral6.Visible = false;
                        menuLateral8.Visible = false;
                        menuLateral7.Visible = false;
                        menuLateral9.Visible = false;
                        menuLateral10.Visible = false;

                    }
                }
            }
        }

        private void PopulaEspecie()
        {
            //ddlEspecie.DataValueField = "Id";
            //ddlEspecie.DataTextField = "Nome";
            //ddlEspecie.DataSource = especiesBll.Listar();
            //ddlEspecie.DataBind();

            //ddlEspecie.Items.Insert(0, new ListItem("-- Selecione --", "0"));

            rblEspecie.DataValueField = "Id";
            rblEspecie.DataTextField = "Nome";
            rblEspecie.DataSource = especiesBll.Listar();
            rblEspecie.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("1.1.1");

            MudaTela(_tela);
        }

        protected void MudaTela(string _tela)
        {
            Response.Redirect("~/TabAux/Tela3.aspx?Tela=" + _tela);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("1.1.2");

            MudaTela(_tela);
        }

        protected void lbRaca_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("1.3.1");

            Response.Redirect("~/TabAux/RacasSelecao.aspx");
        }

        protected void lbEspecie_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("1.3.2");

            MudaTela(_tela);
        }

        protected void lbIndicacoesNutrientes_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("1.4.2");

            MudaTela(_tela);
        }

        protected void lbAlimentos_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("2.0");

            Response.Redirect("~/Cadastros/AlimentosSelecao.aspx");
        }

        protected void lbSairSistema_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            Funcoes.Funcoes.FecharJanela(this);
            Response.Redirect("~/Login.aspx");
        }

        protected void lbGuposAlimentos_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("1.4.5");

            MudaTela(_tela);
        }

        protected void lbCategoriasAlimentos_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("1.4.7");

            MudaTela(_tela);
        }

        protected void lbFontesAlimentos_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("1.4.6");

            MudaTela(_tela);
        }

        protected void lbTutores_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("3.0");

            Response.Redirect("~/Cadastros/TutoresSelecao.aspx");
        }

        protected void lbPacientes_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("4.0");

            Response.Redirect("~/Cadastros/PacientesSelecao.aspx");
        }

        protected void lbDietas_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("5.0");

            Response.Redirect("~/Cadastros/DietasSelecao.aspx");
        }

        protected void lbCardapios_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("6.0");

            Response.Redirect("~/Cardapio/CardapioSelecao.aspx");
        }


        protected void lbReceituario_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("11.0");

            Response.Redirect("~/Modulos/ReceituarioPacienteSelecao.aspx");
        }


        protected void lbTutoriais_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("7.0");

            Response.Redirect("~/Tutoriais.aspx");
        }

        protected void lbBiblioteca_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("10.0.3");

            Response.Redirect("~/Biblioteca/BibliotecaVisualizar.aspx");
        }

        protected void hlAlimentos_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("1.5.1");

            Response.Redirect("~/Cadastros/AlimentosGerencia.aspx");
        }

        protected void hlMostrarTermoUso_Click(object sender, EventArgs e)
        {
            mdlTermo.Show();
        }

        protected void lbMensagens_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracao/MensagensPortalSelecao.aspx");
        }

        protected void lbPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Perfil/Perfil.aspx");
        }

        protected void lbInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MenuGeral.aspx");
        }

        protected void lbAssinantes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracao/AssinantesSelecao.aspx");
        }

        protected void lbCupons_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracao/CuponsSelecao.aspx");
        }

        protected void lbSecoes_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("10.0.1");
            MudaTela(_tela);
        }

        protected void lbArquivosSecoes_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("10.0.2");
            //tabela Arquivos Biblioteca
            Response.Redirect("~/Biblioteca/BibliotecaCadastro.aspx");
        }

        protected void lbNutraceuticos_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("9.0");
            //tabela Nutracêuticos
            Response.Redirect("~/Administracao/NutraceuticosSelecao.aspx");
        }

        protected void lbPrescr_Click(object sender, EventArgs e)
        {
            _tela = Funcoes.Funcoes.Seguranca.Criptografar("1.2.1");

            MudaTela(_tela);
        }

        //protected void ddlEspecie_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    PopulaGrid(Funcoes.Funcoes.ConvertePara.Int(ddlEspecie.SelectedValue));
        //    modalNutra.Show();
        //}

        protected void lbTabelaNutraceuticos_Click(object sender, EventArgs e)
        {
            modalNutra.Show();
        }

        protected void rptListNutra_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        nutraTO = (TONutraceuticosBll)e.Item.DataItem;
                        Label lblDoseMinima = (Label)e.Item.FindControl(
                            "lblDoseMinima");
                        Label lblDoseMaxima = (Label)e.Item.FindControl(
                            "lblDoseMaxima");

                        lblDoseMinima.Text = string.Format("{0:0.0}",
                            (nutraTO.DoseMin != null ? nutraTO.DoseMin.Value : 0));
                        lblDoseMaxima.Text = string.Format("{0:0.0}",
                            (nutraTO.DoseMax != null ? nutraTO.DoseMax.Value : 0));

                        break;
                    }
            }
        }

        protected void lbGrpNutri_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TabAux/NutrientesAuxGrupos.aspx");
        }

        protected void lbNutrientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracao/Nutrientes.aspx");
        }

        protected void rblEspecie_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaGrid(Funcoes.Funcoes.ConvertePara.Int(rblEspecie.SelectedValue));
            modalNutra.Show();
        }

        protected void btnModalAlimFechar_Click(object sender, EventArgs e)
        {
            LimpaModalNutr();
        }

        private void LimpaModalNutr()
        {
            rblEspecie.SelectedIndex = -1;

            rptListNutra.DataSource = null;
            rptListNutra.DataBind();
        }

        protected void lbLogsSistema_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracao/LogsSelecao.aspx");
        }

        protected void lbReceituario_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Receituario/ReceituarioSelecao.aspx");
        }

        protected void lbNutraDietas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracao/NutracDietasCadastro.aspx");
        }
    }
}