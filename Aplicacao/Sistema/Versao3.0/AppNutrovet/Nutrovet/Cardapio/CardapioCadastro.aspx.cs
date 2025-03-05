using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Hosting;
using DCL;
using BLL;
using AjaxControlToolkit;
using MaskEdit;
using Gauge;
using System.IO;
using System.Drawing.Imaging;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Security.Cryptography;
using Org.BouncyCastle.Ocsp;

namespace Nutrovet.Cardapio
{
    public partial class CardapioCadastro : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clAnimaisBll animaisBll = new clAnimaisBll();
        protected Animai animaisDcl;
        protected clAnimaisPesoHistoricoBll pesoHistBll = new clAnimaisPesoHistoricoBll();
        protected AnimaisPesoHistorico pesoHistDcl;
        protected clAnimaisAuxEspeciesBll especiesBll = new clAnimaisAuxEspeciesBll();
        protected clAnimaisAuxRacasBll racasBll = new clAnimaisAuxRacasBll();
        protected AnimaisAuxRaca racasDcl;
        protected clPessoasBll pessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected clTutoresBll tutoresBll = new clTutoresBll();
        protected Tutore tutoresDcl;
        protected clDietasBll dietasBll = new clDietasBll();
        protected Dietas dietasDcl;
        protected clDietasAlimentosBll dietasAlimentosBll = new clDietasAlimentosBll();
        protected DietasAlimento dietasAlimentoDcl;
        protected TODietasAlimentosBll dietasAlimentosTO;
        protected clAlimentosBll alimentoBll = new clAlimentosBll();
        protected Alimentos alimentoDcl;
        protected TOAlimentosBll alimentoTO;
        protected clAlimentosNutrientesBll alimentoNutrBll = new clAlimentosNutrientesBll();
        protected AlimentoNutriente alimentoNutrDcl;
        protected TOAlimentoNutrientesBll alimentoNutrTO;
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl, cardapioOriginalDcl;
        protected clCardapioAlimentosBll cardapioAlimentosBll = new
            clCardapioAlimentosBll();
        protected CardapiosAlimento cardapioAlimentosDcl;
        protected TOCardapioResumoBll cardapioAlimentosTO;
        protected clExigenciasNutrBll exigenciasNutrBll = new clExigenciasNutrBll();
        protected ExigenciasNutricionai exigenciasNutrDcl;
        protected clNutrientesAuxGruposBll gruposNutrientesBll = new
            clNutrientesAuxGruposBll();
        protected clNutrientesBll nutrientesBll = new clNutrientesBll();
        protected TONutrientesBll nutrientesTO;
        protected TOReceituarioNutrientesBll receitExigNutrTO;
        protected clReceituarioNutrientesBll receitNutrBll = new clReceituarioNutrientesBll();
        protected List<TOReceituarioNutrientesBll> listReceitExigNutrTO;
        protected clAcessosVigenciaPlanosBll acessosVigenciaPlanosBLL = new 
            clAcessosVigenciaPlanosBll();
        protected AcessosVigenciaPlano acessosVigenciaPlanoDCL;
        protected TOAcessosVigenciaPlanosBll planoTO;
        protected clConfigReceituarioBll configReceitBll = new clConfigReceituarioBll();
        protected ConfigReceituario configReceitDcl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (acessosBll.Permissao(
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "6.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name);
                        int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                            Funcoes.Funcoes.Seguranca.Descriptografar(
                                Funcoes.Funcoes.ConvertePara.String(
                                    Request.QueryString["_idCardapio"])));
                        Session.Remove("BalancoDieta");
                        Session.Remove("Receituario");

                        PopularTutor(_idPessoa);
                        PopulaTela(_idCardapio);
                        PopulaTblsExigNutr();
                        Page.Form.DefaultFocus = ddlTutor.ClientID;
                    }

                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        protected void aDadosPac_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 0;
            liDadosPac.Attributes["class"] = "active";
            liCompCard.Attributes["class"] = "";
            liNutrCard.Attributes["class"] = "";

            Session.Remove("Receituario");
        }

        protected void aCompCard_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 1;
            liDadosPac.Attributes["class"] = "";
            liCompCard.Attributes["class"] = "active";
            liNutrCard.Attributes["class"] = "";

            Session.Remove("Receituario");

        }

        protected void aNutrCard_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 2;
            liDadosPac.Attributes["class"] = "";
            liCompCard.Attributes["class"] = "";
            liNutrCard.Attributes["class"] = "active";
        }

        private void LimpaTela()
        {
            btnSimGestante.CssClass = "btn btn-default";
            btnSimLactante.CssClass = "btn btn-default";
            btnNaoGestante.CssClass = "btn btn-primary-nutrovet";
            btnNaoLactante.CssClass = "btn btn-primary-nutrovet";

            btnSimLactante.Enabled = true;
            btnNaoLactante.Enabled = true;
            btnSimGestante.Enabled = true;
            btnNaoGestante.Enabled = true;

            ViewState["Gestante"] = false;
            ViewState["Lactante"] = false;

            divGestante.Visible = false;
            divLactante.Visible = false;


            tbNrFilhotes.Text = "";
            tbSemanLact.Text = "";
            tbEspeciePaciente.Text = "";
            tbRacaPaciente.Text = "";
            tbIdadeAnos.Text = "";
            tbPesoAtual.Text = "";
            tbPesoIdeal.Text = "";
            tbFator.Text = "";
            tbNEM.Text = "";
            txbTituloCardapio.Text = "";

            //Funcoes.Funcoes.ControlForm.SetComboBox(ddlSugestaoDieta, 0);
            Session.Remove("dadosPaciente");
        }

        //Verificar
        private void PopularTutor(int _idPessoa)
        {
            List<TOTutoresBll> listagem = tutoresBll.Listar(true, _idPessoa);

            ddlTutor.DataTextField = "Tutor";
            ddlTutor.DataValueField = "IdTutor";
            ddlTutor.DataSource = listagem;
            ddlTutor.DataBind();

            ddlTutor.Items.Insert(0, new ListItem("-- Selecione --", "0"));
            ddlPaciente.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaPaciente(int _idTutor)
        {
            List<TOAnimaisBll> listagem = animaisBll.Listar(_idTutor,
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
                DominiosBll.ListarAnimaisPor.Tutor);

            ddlPaciente.DataTextField = "Animal";
            ddlPaciente.DataValueField = "IdAnimal";
            ddlPaciente.DataSource = listagem;
            ddlPaciente.DataBind();

            ddlPaciente.Items.Insert(0, new ListItem("-- Selecione --", "0"));

            if (ddlTutor.SelectedIndex != 0)
            {
                ddlPaciente.Focus();
            }
            else
            {
                ddlTutor.Focus();
            }

        }

        private void DadosPaciente(int _idAnimal)
        {
            TOAnimaisBll dadosPaciente = animaisBll.CarregarTO(_idAnimal);

            if (dadosPaciente != null)
            {
                PopularDietas(Funcoes.Funcoes.ConvertePara.Int(dadosPaciente.IdEspecie));

                DominiosBll.ExigenciasNutrAuxIndicacoes situacaoDoAnimal =
                        animaisBll.CrescimentoAnimal(dadosPaciente,
                        Funcoes.Funcoes.ConvertePara.Bool(ViewState["Gestante"]),
                        Funcoes.Funcoes.ConvertePara.Bool(ViewState["Lactante"]));

                Session["dadosPaciente"] = dadosPaciente;

                if (dadosPaciente != null)
                {
                    switch (dadosPaciente.IdSexo)
                    {
                        case (int?)DominiosBll.Sexo.Macho:
                            {
                                divGestante.Visible = false;
                                divLactante.Visible = false;

                                break;
                            }

                        case (int?)DominiosBll.Sexo.Fêmea:
                            {
                                divGestante.Visible = true;
                                divLactante.Visible = true;

                                break;
                            }
                    }

                    tbFator.Text = (((situacaoDoAnimal ==
                        DominiosBll.ExigenciasNutrAuxIndicacoes.CresInicial) || (
                        situacaoDoAnimal ==
                        DominiosBll.ExigenciasNutrAuxIndicacoes.CresFinal)) ? "138" :
                        (tbFator.Text != "" ? tbFator.Text : ""));

                    tbEspeciePaciente.Text = dadosPaciente.Especie;
                    tbRacaPaciente.Text = dadosPaciente.Raca;
                    tbIdadeAnos.Text = Funcoes.Funcoes.ConvertePara.String(
                        dadosPaciente.IdadeAno);
                    tbIdadeMeses.Text = Funcoes.Funcoes.ConvertePara.String(
                        dadosPaciente.IdadeMes);
                    tbIdadeDias.Text = Funcoes.Funcoes.ConvertePara.String(
                        dadosPaciente.IdadeDia);
                    tbPesoAtual.Text = Funcoes.Funcoes.ConvertePara.String(
                        dadosPaciente.PesoAtual);
                    tbPesoIdeal.Text = Funcoes.Funcoes.ConvertePara.String(
                        dadosPaciente.PesoIdeal);
                }
            }
        }

        private void PopularDietas(int _idEspecie)
        {
            List<Dietas> listagem = dietasBll.Listar(_idEspecie);

            ddlSugestaoDieta.DataTextField = "Dieta";
            ddlSugestaoDieta.DataValueField = "IdDieta";
            ddlSugestaoDieta.DataSource = listagem;
            ddlSugestaoDieta.DataBind();
            ddlSugestaoDieta.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaTblsExigNutr()
        {
            ddlTblExigNutr.Items.AddRange(exigenciasNutrBll.ListarTabNutr());
            ddlTblExigNutr.DataBind();

            ddlTblExigNutr.Items.Insert(0, new ListItem("--- Selecione ---", "0"));

            Funcoes.Funcoes.ControlForm.SetComboBox(ddlTblExigNutr, 2);

            int _idcardapio = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idCardapio"]);

            string _url = "~/Relatorios/RptBalancoCardapio.aspx?_tabela=" +
                ddlTblExigNutr.SelectedItem.Text + "&_idCard=" + _idcardapio +
                "&_quantDietas=" + 1;

            hlImpressaoTbNutr.NavigateUrl = _url;
            hlImpressaoTbNutr.Enabled = true;
            hlImpressaoTbNutr.CssClass = "btn btn-primary-nutrovet";
        }

        private void PopulaTela(int _idCardapio)
        {
            if (_idCardapio > 0)
            {
                //lbImpressaoCardapio.NavigateUrl =
                //    "~/Cardapio/Impressao/CardapioImpressao.aspx?_idCardapio=" + 
                //        Funcoes.Funcoes.Seguranca.Criptografar(
                //            Funcoes.Funcoes.ConvertePara.String(_idCardapio));
                //"~/Relatorios/RptCardapio.aspx?_idCard=" + _idCardapio;

                ViewState["_idCardapio"] = _idCardapio;

                lblTitulo.Text = "Alteração do Cardápio Nutricional";
                lblPagina.Text = "Alterar Cardápio";
                lblSubTitulo.Text = "";

                PopulaDadosPaciente(_idCardapio);
                PopulaAlimento(_idCardapio);
                AtualizarDadosCardapio(_idCardapio);
            }
            else
            {
                ViewState["Gestante"] = false;
                ViewState["Lactante"] = false;

                aCompCard.Visible = false;
                aNutrCard.Visible = false;

                pnlEM.Attributes["class"] = "panel panel-info";

                lblTitulo.Text = "Inserção do Cardápio Nutricional";
                lblPagina.Text = "Inserir Cardápio";
                lblSubTitulo.Text = "";
            }
        }

        private void AtualizarDadosCardapio(int idCardapio)
        {
            cardapioDcl = cardapioBll.Carregar(idCardapio);

            if (Funcoes.Funcoes.ConvertePara.Int(cardapioDcl.NrCardapio) <= 0)
            {
                cardapioDcl.NrCardapio = cardapioBll.GerarNumeroArquivo();
                cardapioDcl.Arquivo = "Cardapio_" + cardapioDcl.NrCardapio + "_" +
                    DateTime.Today.ToString("yy") + ".pdf";

                cardapioDcl.Ativo = true;
                cardapioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                cardapioDcl.DataCadastro = DateTime.Now;
                cardapioDcl.IP = Request.UserHostAddress;

                bllRetorno alterarRet = cardapioBll.Alterar(cardapioDcl);

                if (alterarRet.retorno)
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Success,
                        "Informações da Dieta Atualizadas com Sucesso!",
                        "NutroVET informa - Alteração",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
        }

        private void PopulaDadosPaciente(int idCardapio)
        {
            cardapioDcl = cardapioBll.Carregar(idCardapio);

            if (Funcoes.Funcoes.ConvertePara.Int(cardapioDcl.IdAnimal) > 0)
            {
                animaisDcl = animaisBll.Carregar(Funcoes.Funcoes.ConvertePara.Int(
                        cardapioDcl.IdAnimal));
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlTutor, animaisDcl.Tutore.IdTutor);
                PopulaPaciente(animaisDcl.Tutore.IdTutor);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlPaciente, cardapioDcl.IdAnimal);
                DadosPaciente(Funcoes.Funcoes.ConvertePara.Int(cardapioDcl.IdAnimal));

                pesoHistDcl = pesoHistBll.Carregar(Funcoes.Funcoes.ConvertePara.Int(
                        cardapioDcl.IdAnimal));
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlSugestaoDieta, cardapioDcl.IdDieta);
                lblNomeDieta.Text = ddlSugestaoDieta.SelectedItem.Text;

                tbPesoIdeal.Text = Funcoes.Funcoes.ConvertePara.String(animaisDcl.PesoIdeal);
                tbNrFilhotes.Text = Funcoes.Funcoes.ConvertePara.String(cardapioDcl.NrFilhotes);
                tbSemanLact.Text = Funcoes.Funcoes.ConvertePara.String(
                    cardapioDcl.LactacaoSemanas);
                tbNEM.Text = string.Format("{0:#,###}", Math.Round(
                    Funcoes.Funcoes.ConvertePara.Decimal(cardapioDcl.NEM), 0));
                lblNemDoCardapio.Text = string.Format("{0:#,###}", Math.Round(
                    Funcoes.Funcoes.ConvertePara.Decimal(cardapioDcl.NEM), 0)) + " Kcal";

                if (Funcoes.Funcoes.ConvertePara.Bool(animaisDcl.RecalcularNEM))
                {
                    lblRecalculo.Text =
                    @"Peso Ideal alterado na Tela de Pacientes. 
                        Não esqueça de RECALCULAR o NEM e 
                            Clicar no Botão <Button ID='lblExemplo' CssClass='btn btn-sm btn - primary - nutrovet m - t - n - xs'><i class='fas fa-hand - point - right'></i>&nbsp;Próximo</Button>";
                    btnInfoRecalc.Visible = true;
                }
                else
                {
                    lblRecalculo.Text = "";
                    btnInfoRecalc.Visible = false;
                }

                if (Funcoes.Funcoes.ConvertePara.Bool(cardapioDcl.Gestante))
                {
                    btnSimGestante.CssClass = "btn btn-primary-nutrovet";
                    btnNaoGestante.CssClass = "btn btn-default";
                    btnSimLactante.Enabled = false;
                    btnNaoLactante.Enabled = false;

                    ViewState["Gestante"] = true;
                    ViewState["Lactante"] = false;
                }
                else
                {
                    btnSimGestante.CssClass = "btn btn-default";
                    btnNaoGestante.CssClass = "btn btn-primary-nutrovet";
                    btnSimLactante.Enabled = true;
                    btnNaoLactante.Enabled = true;
                    btnSimLactante.Focus();
                    ViewState["Gestante"] = false;
                    ViewState["Lactante"] = false;
                }

                if (Funcoes.Funcoes.ConvertePara.Bool(cardapioDcl.Lactante))
                {
                    btnSimLactante.CssClass = "btn btn-primary-nutrovet";
                    btnNaoLactante.CssClass = "btn btn-default";
                    btnSimGestante.Enabled = false;
                    btnNaoGestante.Enabled = false;

                    ViewState["Gestante"] = false;
                    ViewState["Lactante"] = true;
                }
                else
                {
                    btnSimLactante.CssClass = "btn btn-default";
                    btnNaoLactante.CssClass = "btn btn-primary-nutrovet";
                    btnSimGestante.Enabled = true;
                    btnNaoGestante.Enabled = true;
                    btnSimGestante.Focus();
                    ViewState["Gestante"] = false;
                    ViewState["Lactante"] = false;
                }

                aCompCard.Visible = true;
                aNutrCard.Visible = true;
                divAjuste.Visible = false;
            }
            else
            {
                aCompCard.Visible = false;
                aNutrCard.Visible = false;
                divAjuste.Visible = true;

                btnSimGestante.CssClass = "btn btn-default";
                btnNaoGestante.CssClass = "btn btn-primary-nutrovet";
                btnSimLactante.CssClass = "btn btn-default";
                btnNaoLactante.CssClass = "btn btn-primary-nutrovet";

                btnSimLactante.Enabled = true;
                btnNaoLactante.Enabled = true;
                btnSimLactante.Focus();
                ViewState["Gestante"] = false;
                ViewState["Lactante"] = false;
            }

            Funcoes.Funcoes.ControlForm.SetComboBox(ddlSugestaoDieta, cardapioDcl.IdDieta);
            PopulaGraficosDieta(Funcoes.Funcoes.ConvertePara.Int(cardapioDcl.IdDieta));
            txbTituloCardapio.Text = cardapioDcl.Descricao;
            tbFator.Text = Funcoes.Funcoes.ConvertePara.String(cardapioDcl.FatorEnergia);
        }

        private void PopulaAlimento(int _idCardapio)
        {
            rptListagemAlimentos.DataSource = cardapioAlimentosBll.ListarTO(
                _idCardapio, Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
            rptListagemAlimentos.DataBind();

            PopulaBICardapio(_idCardapio);
            AtualizaSubTotais(_idCardapio);
        }

        private void PopulaBICardapio(int _idCardapio)
        {
            rptListagemDistCat.DataSource = cardapioAlimentosBll.BICardapioAlimento(
                _idCardapio);
            rptListagemDistCat.DataBind();
        }

        private void PopulaListaAlimentos(string _alimento)
        {
            ltbAlimentos.DataTextField = "AlimentoFonte";
            ltbAlimentos.DataValueField = "IdAlimento";
            ltbAlimentos.DataSource = alimentoBll.Listar(_alimento,
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
            ltbAlimentos.DataBind();

            if (ltbAlimentos.Items.Count > 0)
            {
                ltbAlimentos.Visible = true;
                ltbAlimentos.Focus();
                ltbAlimentos.SelectedIndex = 0;
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Info,
                    @"<b>O Alimento Informado NÃO foi Encontrado</b> !",
                    "NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void PopulaTabelaNutricional()
        {
            acGrupos.DataSource = gruposNutrientesBll.Listar(true);
            acGrupos.DataBind();
        }

        private void PopulaNutrientesAlim(int _idCardapAlim)
        {
            cardapioAlimentosDcl = cardapioAlimentosBll.Carregar(_idCardapAlim);
            lblTituloModalAlimento.Text = cardapioAlimentosDcl.Alimentos.Alimento;

            List<TOAlimentoNutrientesBll> listagem = alimentoNutrBll.ListarTO(
                cardapioAlimentosDcl.IdCardapio, cardapioAlimentosDcl.IdAlimento);

            ViewState["QuantListNutr"] = (listagem.Count > 0 ? listagem[0].Quant : 0);

            rptListagemNutrientes.DataSource = listagem;
            rptListagemNutrientes.DataBind();
        }

        private void PopulaExigenciasNutricionais(int _tabela)
        {
            TOAnimaisBll dadosPaciente = (TOAnimaisBll)Session["dadosPaciente"] ??
                animaisBll.CarregarTO(Funcoes.Funcoes.ConvertePara.Int(
                ddlPaciente.SelectedValue));

            bool _lactante = Funcoes.Funcoes.ConvertePara.Bool(ViewState["Lactante"]);
            bool _gestante = Funcoes.Funcoes.ConvertePara.Bool(ViewState["Gestante"]);

            DominiosBll.ExigenciasNutrAuxIndicacoes situacaoDoAnimal =
                animaisBll.CrescimentoAnimal(dadosPaciente, _gestante, _lactante);

            List<TOExigNutrTabelasBll> listagem = exigenciasNutrBll.ListarExigNutr(
                _tabela, Funcoes.Funcoes.ConvertePara.Int(dadosPaciente.IdEspecie),
                (int)situacaoDoAnimal);

            switch (_tabela)
            {
                case 1:
                case 3:
                    {
                        rptModalTabelasNutr.DataSource = listagem;
                        rptModalTabelasNutr.DataBind();
                        rptModalTabelasNutr.Visible = false;

                        rptModalTabNut_II.Visible = true;
                        rptModalTabNut_II.DataSource = listagem;
                        rptModalTabNut_II.DataBind();

                        break;
                    }
                case 2:
                    {
                        rptModalTabNut_II.DataSource = null;
                        rptModalTabNut_II.DataBind();
                        rptModalTabNut_II.Visible = false;

                        rptModalTabelasNutr.Visible = true;
                        rptModalTabelasNutr.DataSource = listagem;
                        rptModalTabelasNutr.DataBind();

                        break;
                    }
            }
        }

        private void PopulaGraficosDieta(int _idDieta)
        {
            dietasDcl = dietasBll.Carregar(_idDieta);

            if (dietasDcl != null)
            {
                imgDietaCarboidrato.ImageUrl = GerarGrafico(
                    Funcoes.Funcoes.ConvertePara.Double(dietasDcl.Carboidrato),
                    "CarboDieta", "#F5AA56");
                imgDietaProteina.ImageUrl = GerarGrafico(
                    Funcoes.Funcoes.ConvertePara.Double(dietasDcl.Proteina),
                    "ProtDieta", "#1C82C3");
                imgDietaGordura.ImageUrl = GerarGrafico(
                    Funcoes.Funcoes.ConvertePara.Double(dietasDcl.Gordura),
                    "GordDieta", "#E85362");
            }
            else
            {
                imgDietaCarboidrato.ImageUrl = GerarGrafico(0, "CarboDieta", "#F5AA56");
                imgDietaProteina.ImageUrl = GerarGrafico(0, "ProtDieta", "#1C82C3");
                imgDietaGordura.ImageUrl = GerarGrafico(0, "GordDieta", "#E85362");
            }
        }

        private void PopulaGraficosCardapio(decimal _carbo, decimal _prot,
            decimal _gord)
        {
            imgCardapioCarboidrato.ImageUrl = GerarGrafico(
                Funcoes.Funcoes.ConvertePara.Double(_carbo),
                "Carboidrato", "#F5AA56");
            imgCardapioProteina.ImageUrl = GerarGrafico(
                Funcoes.Funcoes.ConvertePara.Double(_prot),
                "Proteina", "#1C82C3");
            imgCardapioGordura.ImageUrl = GerarGrafico(
                Funcoes.Funcoes.ConvertePara.Double(_gord),
                "Gordura", "#E85362");
        }

        private void PopulaResumo(int _idCardapio)
        {
            rptResumoCardapio.DataSource = cardapioAlimentosBll.ListarResumoCardapio(
                _idCardapio, Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
            rptResumoCardapio.DataBind();
        }

        private void AtualizaSubTotais(int _idCardapio)
        {
            TOCardapioResumoBll resumo = cardapioBll.CardapioResumo(_idCardapio,
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));

            lblCardapioCarboidrato.Text = resumo.CarboG;
            lblCardapioProteina.Text = resumo.ProtG;
            lblCardapioGordura.Text = resumo.GordG;

            lblFibrasDoCardapio.Text = resumo.FibrasG;
            lblUmidadeDoCardapio.Text = resumo.UmidageG;
            lblEnergiaDoCardapio.Text = resumo.EnergiaKcal;

            ViewState["EnergiaDoCardapio"] = Math.Round(
                Funcoes.Funcoes.ConvertePara.Decimal(resumo.Energia), 0);

            CalculaEnergia(Funcoes.Funcoes.ConvertePara.Decimal(resumo.Energia),
                Math.Round(Funcoes.Funcoes.ConvertePara.Decimal(resumo.NEM), 0));

            PopulaGraficosCardapio(Funcoes.Funcoes.ConvertePara.Decimal(
                resumo.CarboPerc), Funcoes.Funcoes.ConvertePara.Decimal(
                    resumo.ProtPerc), Funcoes.Funcoes.ConvertePara.Decimal(
                        resumo.GordPerc));
        }

        private void CalculaEnergia(decimal _em, decimal _nem)
        {
            decimal _umPercNEM = Math.Round(_nem / 100, 0);

            if (_em < (_nem - _umPercNEM))
            {
                pnlEM.Attributes["class"] = "panel panel-warning";
            }
            else if (_em > (_nem + _umPercNEM))
            {
                pnlEM.Attributes["class"] = "panel panel-danger";
            }
            else if ((_em >= (_nem - _umPercNEM)) && (_em <= (_nem + _umPercNEM)))
            {
                pnlEM.Attributes["class"] = "panel panel-success";
            }
            else
            {
                pnlEM.Attributes["class"] = "panel panel-info";
            }
        }

        private void CalcularNEM(bool _mantemFator)
        {
            TOAnimaisBll dadosPaciente = (TOAnimaisBll)Session["dadosPaciente"] ??
                animaisBll.CarregarTO(Funcoes.Funcoes.ConvertePara.Int(
                    ddlPaciente.SelectedValue));

            if (dadosPaciente != null)
            {
                double _fator = Funcoes.Funcoes.ConvertePara.Double(tbFator.Text, true);
                double _pesoIdeal = Funcoes.Funcoes.ConvertePara.Double(tbPesoIdeal.Text, true);
                bool _lactante = Funcoes.Funcoes.ConvertePara.Bool(ViewState["Lactante"]);
                bool _gestante = Funcoes.Funcoes.ConvertePara.Bool(ViewState["Gestante"]);
                int _nrFilhotes = Funcoes.Funcoes.ConvertePara.Int(tbNrFilhotes.Text);
                int _semanLact = Funcoes.Funcoes.ConvertePara.Int(tbSemanLact.Text);
                double _nem = 0;

                DominiosBll.ExigenciasNutrAuxIndicacoes situacaoDoAnimal =
                    animaisBll.CrescimentoAnimal(dadosPaciente, _gestante, _lactante);

                switch (situacaoDoAnimal)
                {
                    case DominiosBll.ExigenciasNutrAuxIndicacoes.CresInicial://ok
                        {
                            switch (dadosPaciente.IdEspecie)
                            {
                                case 1:
                                case 2:
                                    {
                                        if (!_fator.Equals(138) && _mantemFator)
                                        {
                                            lblMsgModal.Text = @"
                                            É Recomendado que este Valor NÃO seja alterado !
                                            </br>
                                            Deseja Manter o Fator em 138?";

                                            ViewState["_valorFator"] = 138;

                                            popUpModal.Show();
                                        }
                                        else
                                        {
                                            _nem = cardapioBll.CalculoNEM(dadosPaciente,
                                                _lactante, _gestante, _nrFilhotes, _semanLact,
                                                _pesoIdeal, _fator);
                                        }

                                        break;
                                    }
                            }

                            break;
                        }
                    case DominiosBll.ExigenciasNutrAuxIndicacoes.CresFinal://ok
                        {
                            switch (dadosPaciente.IdEspecie)
                            {
                                case 1:
                                case 2:
                                    {
                                        if (!_fator.Equals(138) && _mantemFator)
                                        {
                                            lblMsgModal.Text = @"
                                            É Recomendado que este Valor NÃO seja alterado !
                                            </br>
                                            Deseja Manter o Fator em 138?";

                                            ViewState["_valorFator"] = 138;

                                            popUpModal.Show();
                                        }
                                        else
                                        {
                                            _nem = cardapioBll.CalculoNEM(dadosPaciente,
                                                _lactante, _gestante, _nrFilhotes, _semanLact,
                                                _pesoIdeal, _fator);
                                        }

                                        break;
                                    }
                            }

                            break;
                        }
                    case DominiosBll.ExigenciasNutrAuxIndicacoes.Adulto://ok
                        {
                            switch (dadosPaciente.IdEspecie)
                            {
                                case 1:
                                case 2:
                                    {
                                        if ((_fator > 0) && (_mantemFator))
                                        {
                                            _nem = cardapioBll.CalculoNEM(dadosPaciente,
                                                _lactante, _gestante, _nrFilhotes, _semanLact,
                                                _pesoIdeal, _fator);
                                        }
                                        else
                                        {
                                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                                Funcoes.Funcoes.Toastr.ToastType.Warning,
                                                "Campo FATOR deve ser Preenchido !",
                                                "Nutrovet Informa",
                                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                                true);
                                        }

                                        break;
                                    }
                            }

                            break;
                        }
                    case DominiosBll.ExigenciasNutrAuxIndicacoes.Gestante://ok
                        {
                            switch (dadosPaciente.IdEspecie)
                            {
                                case 1:
                                    {
                                        if (!_fator.Equals(130) && _mantemFator)
                                        {
                                            lblMsgModal.Text = @"
                                            É Recomendado que este Valor NÃO seja alterado !
                                            </br>
                                            Deseja Manter o Fator em 130?";

                                            ViewState["_valorFator"] = 130;

                                            popUpModal.Show();
                                        }
                                        else
                                        {
                                            _nem = cardapioBll.CalculoNEM(dadosPaciente,
                                                _lactante, _gestante, _nrFilhotes, _semanLact,
                                                _pesoIdeal, _fator);
                                        }

                                        break;
                                    }
                                case 2:
                                    {
                                        if (!_fator.Equals(140) && _mantemFator)
                                        {
                                            lblMsgModal.Text = @"
                                            É Recomendado que este Valor NÃO seja alterado !
                                            </br>
                                            Deseja Manter o Fator em 140?";

                                            ViewState["_valorFator"] = 140;

                                            popUpModal.Show();
                                        }
                                        else
                                        {
                                            _nem = cardapioBll.CalculoNEM(dadosPaciente,
                                                _lactante, _gestante, _nrFilhotes, _semanLact,
                                                _pesoIdeal, _fator);
                                        }

                                        break;
                                    }
                            }

                            break;
                        }
                    case DominiosBll.ExigenciasNutrAuxIndicacoes.Lactante://ok
                        {
                            switch (dadosPaciente.IdEspecie)
                            {
                                case 1://Cães
                                    {
                                        if (!_fator.Equals(145) && _mantemFator)
                                        {
                                            lblMsgModal.Text = @"
                                            É Recomendado que este Valor NÃO seja alterado !
                                            </br>
                                            Deseja Manter o Fator em 145?";

                                            ViewState["_valorFator"] = 145;

                                            popUpModal.Show();
                                        }
                                        else
                                        {
                                            if ((_nrFilhotes > 0) &&
                                                (_semanLact >= 1 && _semanLact <= 7))
                                            {
                                                _nem = cardapioBll.CalculoNEM(
                                                    dadosPaciente, _lactante, _gestante,
                                                    _nrFilhotes, _semanLact, _pesoIdeal,
                                                    _fator);
                                            }
                                            else
                                            {
                                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                                                    @"<b>Número de Filhotes e </br>Semanas de Lactação</br>Devem Ser Preenchidos</b> !",
                                                    "NutroVET informa",
                                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                                    true);
                                            }
                                        }

                                        break;
                                    }
                                case 2://Gatos
                                    {
                                        if (!_fator.Equals(100) && _mantemFator)
                                        {
                                            lblMsgModal.Text = @"
                                            É Recomendado que este Valor NÃO seja alterado !
                                            </br>
                                            Deseja Manter o Fator em 100?";

                                            ViewState["_valorFator"] = 100;

                                            popUpModal.Show();
                                        }
                                        else
                                        {
                                            if ((_nrFilhotes > 0) &&
                                                (_semanLact >= 1 && _semanLact <= 7))
                                            {
                                                _nem = cardapioBll.CalculoNEM(
                                                    dadosPaciente, _lactante, _gestante,
                                                    _nrFilhotes, _semanLact, _pesoIdeal,
                                                    _fator);
                                            }
                                            else
                                            {
                                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                                                    @"<b>Número de Filhotes e </br>Semanas de Lactação</br>Devem Ser Preenchidos</b> !",
                                                    "NutroVET informa",
                                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                                    true);
                                            }
                                        }

                                        break;
                                    }
                            }

                            break;
                        }
                }

                tbNEM.Text = (_nem > 0 ? string.Format("{0:#,###}", Math.Round(_nem, 0)) : "0");
                lblNemDoCardapio.Text = tbNEM.Text + " Kcal";
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Para Calcular o NEM é Necessário Selecionar um Tutor e um Paciente!",
                    "NutroVET Informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private string GerarGrafico(double _valor, string _nameFile, string _cor)
        {
            var data = new GaugeData
            {
                Ranges = new[]
                    {
                        new Range{Color = _cor, MinValue = 0, MaxValue = _valor},
                        new Range{Color = "#E9E6E6", MinValue = (_valor + 1),
                            MaxValue = 100}
                    },
                Value = _valor,
                Label = ""
            };

            string _path = HostingEnvironment.MapPath("~/Imagens/gauge/");

            string _file = _nameFile + "_" + Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name) + ".png";
            string _fileName = _path + _file;

            //if (File.Exists(_fileName))
            //{
            //    File.Delete(_fileName);
            //}

            File.WriteAllBytes(_fileName, data.Generate(80, ImageFormat.Png));

            return "~/Imagens/gauge/" + _file + "?time=" + DateTime.Now.ToString();
        }

        protected void Salvar(int _id)
        {
            if (_id > 0)
            {
                Alterar(_id);
            }
            else
            {
                Inserir();
            }
        }

        private void Alterar(int id)
        {
            cardapioDcl = cardapioBll.Carregar(id);
            decimal _nemOriginal = 0, _nemCopia = 0;

            if (Funcoes.Funcoes.ConvertePara.Int(cardapioDcl.IdCardCopia) > 0)
            {
                cardapioOriginalDcl = cardapioBll.Carregar(
                    Funcoes.Funcoes.ConvertePara.Int(cardapioDcl.IdCardCopia));

                _nemOriginal = Funcoes.Funcoes.ConvertePara.Decimal(
                    cardapioOriginalDcl.NEM);
                _nemCopia = Funcoes.Funcoes.ConvertePara.Decimal(tbNEM.Text);
            }

            animaisDcl = animaisBll.Carregar(Funcoes.Funcoes.ConvertePara.Int(
                ddlPaciente.SelectedValue));

            cardapioDcl.IdAnimal = Funcoes.Funcoes.ConvertePara.Int(
                ddlPaciente.SelectedValue);
            cardapioDcl.DtCardapio = DateTime.Today;

            #region Atualização do Peso

            decimal _pesoAtual = Funcoes.Funcoes.ConvertePara.Decimal(
                tbPesoAtual.Text);
            decimal _pesoIdeal = Funcoes.Funcoes.ConvertePara.Decimal(
                tbPesoIdeal.Text);

            //Cadastra o Peso Atual no Histórico
            if ((animaisDcl.PesoAtual != _pesoAtual) || (!pesoHistBll.PesoExiste(animaisDcl)))
            {
                pesoHistDcl = new AnimaisPesoHistorico();

                pesoHistDcl.IdAnimal = animaisDcl.IdAnimal;
                pesoHistDcl.Peso = Funcoes.Funcoes.ConvertePara.Decimal(
                    tbPesoAtual.Text);
                pesoHistDcl.DataHistorico = DateTime.Today;

                pesoHistDcl.Ativo = true;
                pesoHistDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                pesoHistDcl.DataCadastro = DateTime.Now;
                pesoHistDcl.IP = Request.UserHostAddress;

                bllRetorno insereHistorico = pesoHistBll.Inserir(pesoHistDcl);
            }

            //Atualiza os Pesos na Tabela de Animais
            if ((animaisDcl.PesoAtual != _pesoAtual) ||
                (animaisDcl.PesoIdeal != _pesoIdeal) ||
                (animaisDcl.RecalcularNEM == true))
            {
                animaisDcl.PesoAtual = _pesoAtual;
                animaisDcl.PesoIdeal = _pesoIdeal;
                animaisDcl.RecalcularNEM = false;

                animaisDcl.Ativo = true;
                animaisDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                animaisDcl.DataCadastro = DateTime.Now;
                animaisDcl.IP = Request.UserHostAddress;

                bllRetorno alteraAnimais = animaisBll.Alterar(animaisDcl);
            }

            #endregion

            cardapioDcl.FatorEnergia = Funcoes.Funcoes.ConvertePara.Decimal(
                tbFator.Text);
            cardapioDcl.NEM = Funcoes.Funcoes.ConvertePara.Decimal(tbNEM.Text);
            cardapioDcl.Gestante = Funcoes.Funcoes.ConvertePara.Bool(
                ViewState["Gestante"]);
            cardapioDcl.Lactante = Funcoes.Funcoes.ConvertePara.Bool(
                ViewState["Lactante"]);
            cardapioDcl.NrFilhotes = Funcoes.Funcoes.ConvertePara.Int(
                tbNrFilhotes.Text);
            cardapioDcl.LactacaoSemanas = Funcoes.Funcoes.ConvertePara.Int(
                tbSemanLact.Text);
            cardapioDcl.IdDieta = Funcoes.Funcoes.ConvertePara.Int(
                ddlSugestaoDieta.SelectedValue);
            cardapioDcl.Descricao = txbTituloCardapio.Text;
            cardapioDcl.Observacao = "";

            if (Funcoes.Funcoes.ConvertePara.Int(cardapioDcl.NrCardapio) <= 0)
            {
                cardapioDcl.NrCardapio = cardapioBll.GerarNumeroArquivo();
                cardapioDcl.Arquivo = "Cardapio_" + cardapioDcl.NrCardapio + "_" +
                    DateTime.Today.ToString("yy") + ".pdf";
            }

            cardapioDcl.Ativo = true;
            cardapioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            cardapioDcl.DataCadastro = DateTime.Now;
            cardapioDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = cardapioBll.Alterar(cardapioDcl);

            if (alterarRet.retorno)
            {
                ViewState["_idCardapio"] = cardapioDcl.IdCardapio;
                

                aCompCard.Visible = true;
                aNutrCard.Visible = true;

                mvTabControl.ActiveViewIndex = 1;
                liDadosPac.Attributes["class"] = "";
                liCompCard.Attributes["class"] = "active";
                liNutrCard.Attributes["class"] = "";

                lblRecalculo.Text = "";
                lblNomeDieta.Text = ddlSugestaoDieta.SelectedItem.Text;

                if ((cbxAjuste.Checked) && (_nemOriginal > 0) && (_nemCopia > 0) &&
                    (_nemCopia != _nemOriginal))
                {
                    AtalizarValoresComposicao(_nemOriginal, _nemCopia, 
                        cardapioDcl.IdCardapio);
                }

                PopulaGraficosDieta(Funcoes.Funcoes.ConvertePara.Int(
                    ddlSugestaoDieta.SelectedValue));
                PopulaAlimento(cardapioDcl.IdCardapio);

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Alteração Efetuada com Sucesso !",
                    "NutroVET informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, alterarRet.mensagem,
                    "NutroVET informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private bllRetorno AtalizarValoresComposicao(decimal nemOriginal, decimal nemCopia, 
            int idCardapio)
        {
            bllRetorno retornoDados = cardapioAlimentosBll.AtualizarValoresComposicao(
                nemOriginal, nemCopia, idCardapio);

            return retornoDados;
        }

        private void Inserir()
        {
            cardapioDcl = new DCL.Cardapio();
            animaisDcl = animaisBll.Carregar(Funcoes.Funcoes.ConvertePara.Int(
                ddlPaciente.SelectedValue));

            cardapioDcl.IdPessoa = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            cardapioDcl.IdAnimal = Funcoes.Funcoes.ConvertePara.Int(
                ddlPaciente.SelectedValue);
            cardapioDcl.DtCardapio = DateTime.Today;

            #region Atualização do Peso

            decimal _pesoAtual = Funcoes.Funcoes.ConvertePara.Decimal(
                tbPesoAtual.Text);
            decimal _pesoIdeal = Funcoes.Funcoes.ConvertePara.Decimal(
                tbPesoIdeal.Text);

            if (animaisDcl != null)
            {
                //Cadastra o Peso Atual no Histórico
                if ((animaisDcl.PesoAtual != _pesoAtual) || (!pesoHistBll.PesoExiste(animaisDcl)))
                {
                    pesoHistDcl = new AnimaisPesoHistorico();

                    pesoHistDcl.IdAnimal = animaisDcl.IdAnimal;
                    pesoHistDcl.Peso = Funcoes.Funcoes.ConvertePara.Decimal(
                        tbPesoAtual.Text);
                    pesoHistDcl.DataHistorico = DateTime.Today;

                    pesoHistDcl.Ativo = true;
                    pesoHistDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                    pesoHistDcl.DataCadastro = DateTime.Now;
                    pesoHistDcl.IP = Request.UserHostAddress;

                    bllRetorno insereHistorico = pesoHistBll.Inserir(pesoHistDcl);

                }

                //Atualiza os Pesos na Tabela de Animais
                if ((animaisDcl.PesoAtual != _pesoAtual) ||
                    (animaisDcl.PesoIdeal != _pesoIdeal) ||
                    (animaisDcl.RecalcularNEM == true))
                {
                    animaisDcl.PesoAtual = _pesoAtual;
                    animaisDcl.PesoIdeal = _pesoIdeal;
                    animaisDcl.RecalcularNEM = false;

                    animaisDcl.Ativo = true;
                    animaisDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                    animaisDcl.DataCadastro = DateTime.Now;
                    animaisDcl.IP = Request.UserHostAddress;

                    bllRetorno alteraAnimais = animaisBll.Alterar(animaisDcl);
                }
            }

            #endregion

            cardapioDcl.FatorEnergia = Funcoes.Funcoes.ConvertePara.Decimal(
                tbFator.Text);
            cardapioDcl.NEM = Funcoes.Funcoes.ConvertePara.Decimal(tbNEM.Text);
            cardapioDcl.Gestante = Funcoes.Funcoes.ConvertePara.Bool(
                ViewState["Gestante"]);
            cardapioDcl.Lactante = Funcoes.Funcoes.ConvertePara.Bool(
                ViewState["Lactante"]);
            cardapioDcl.NrFilhotes = Funcoes.Funcoes.ConvertePara.Int(
                tbNrFilhotes.Text);
            cardapioDcl.LactacaoSemanas = Funcoes.Funcoes.ConvertePara.Int(
                tbSemanLact.Text);
            cardapioDcl.IdDieta = Funcoes.Funcoes.ConvertePara.Int(
                ddlSugestaoDieta.SelectedValue);
            cardapioDcl.Descricao = txbTituloCardapio.Text;
            cardapioDcl.Observacao = "";
            cardapioDcl.NrCardapio = cardapioBll.GerarNumeroArquivo();
            cardapioDcl.Arquivo = "Cardapio_" + cardapioDcl.NrCardapio + "_" + 
                DateTime.Today.ToString("yy") + ".pdf";
            cardapioDcl.Ativo = true;
            cardapioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            cardapioDcl.DataCadastro = DateTime.Now;
            cardapioDcl.IP = Request.UserHostAddress;

            bllRetorno inserirRet = cardapioBll.Inserir(cardapioDcl, false);

            if (inserirRet.retorno)
            {
                ViewState["_idCardapio"] = cardapioDcl.IdCardapio;
                //lbImpressaoCardapio.NavigateUrl =
                //    "~/Relatorios/RptCardapio.aspx?_idCard=" + cardapioDcl.IdCardapio;

                aCompCard.Visible = true;
                aNutrCard.Visible = true;

                mvTabControl.ActiveViewIndex = 1;
                liDadosPac.Attributes["class"] = "";
                liCompCard.Attributes["class"] = "active";
                liNutrCard.Attributes["class"] = "";

                lblNomeDieta.Text = ddlSugestaoDieta.SelectedItem.Text;
                PopulaGraficosDieta(Funcoes.Funcoes.ConvertePara.Int(
                    ddlSugestaoDieta.SelectedValue));
                PopulaGraficosCardapio(0, 0, 0);

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Inserção Efetuada com Sucesso !",
                    "NutroVET Informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                    "NutroVET Informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void ExcluirAlim(int _idCardapAlim)
        {
            int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                ViewState["_idCardapio"]);

            cardapioAlimentosDcl = cardapioAlimentosBll.Carregar(_idCardapAlim);
            cardapioAlimentosDcl.Ativo = false;
            cardapioAlimentosDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            cardapioAlimentosDcl.DataCadastro = DateTime.Now;
            cardapioAlimentosDcl.IP = Request.UserHostAddress;

            bllRetorno ret = cardapioAlimentosBll.Excluir(cardapioAlimentosDcl);

            if (ret.retorno)
            {
                PopulaAlimento(_idCardapio);

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    ret.mensagem, "Exclusão",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    ret.mensagem, "Exclusão",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }

        }

        private void EditarAlim(int _id, decimal _newQuant)
        {
            int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                ViewState["_idCardapio"]);

            cardapioAlimentosDcl = cardapioAlimentosBll.Carregar(_id);
            cardapioAlimentosDcl.Quant = _newQuant;
            cardapioAlimentosDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            cardapioAlimentosDcl.Ativo = true;
            cardapioAlimentosDcl.DataCadastro = DateTime.Now;
            cardapioAlimentosDcl.IP = Request.UserHostAddress;

            bllRetorno ret = cardapioAlimentosBll.Alterar(cardapioAlimentosDcl);

            if (ret.retorno)
            {
                PopulaAlimento(_idCardapio);

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    ret.mensagem, "Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    ret.mensagem, "Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void ddlTutor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                ViewState["_idCardapio"]);

            if (_idCardapio <= 0)
            {
                LimpaTela();
            }

            PopulaPaciente(Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue));

            if (ddlTutor.SelectedIndex != 0)
            {
                ddlPaciente.Focus();
            }
        }

        protected void ddlPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                ViewState["_idCardapio"]);

            if (_idCardapio <= 0)
            {
                LimpaTela();
            }

            DadosPaciente(Funcoes.Funcoes.ConvertePara.Int(
                ddlPaciente.SelectedValue));

            if (ddlPaciente.SelectedIndex != 0)
            {
                tbPesoIdeal.Focus();
            }
            else
            {
                ddlTutor.Focus();
            }
        }

        protected void btnSimGestante_Click(object sender, EventArgs e)
        {
            TOAnimaisBll dadosPaciente = (TOAnimaisBll)Session["dadosPaciente"];

            btnSimGestante.CssClass = "btn btn-primary-nutrovet";
            btnNaoGestante.CssClass = "btn btn-default";
            btnSimLactante.Enabled = false;
            btnNaoLactante.Enabled = false;

            divNrFilhotes.Visible = false;
            tbNrFilhotes.Text = "";
            divSemanasLactacao.Visible = false;
            tbSemanLact.Text = "";
            tbFator.Text = (dadosPaciente != null ?
                (dadosPaciente.IdEspecie == 1 ? "130" :
                    (dadosPaciente.IdEspecie == 2 ? "140" : "0")) : "0");

            ViewState["Gestante"] = true;
            ViewState["Lactante"] = false;
        }

        protected void btnNaoGestante_Click(object sender, EventArgs e)
        {
            btnSimGestante.CssClass = "btn btn-default";
            btnNaoGestante.CssClass = "btn btn-primary-nutrovet";
            btnSimLactante.Enabled = true;
            btnNaoLactante.Enabled = true;

            divNrFilhotes.Visible = false;
            tbNrFilhotes.Text = "";
            divSemanasLactacao.Visible = false;
            tbSemanLact.Text = "";
            tbFator.Text = "";

            ViewState["Gestante"] = false;
            ViewState["Lactante"] = false;
        }

        protected void btnSimLactante_Click(object sender, EventArgs e)
        {
            TOAnimaisBll dadosPaciente = (TOAnimaisBll)Session["dadosPaciente"];

            btnSimLactante.CssClass = "btn btn-primary-nutrovet";
            btnNaoLactante.CssClass = "btn btn-default";
            btnSimGestante.Enabled = false;
            btnNaoGestante.Enabled = false;

            divNrFilhotes.Visible = true;
            divSemanasLactacao.Visible = true;

            tbFator.Text = (dadosPaciente != null ?
                (dadosPaciente.IdEspecie == 1 ? "145" :
                    (dadosPaciente.IdEspecie == 2 ? "100" : "0")) : "0");

            ViewState["Gestante"] = false;
            ViewState["Lactante"] = true;
        }

        protected void btnNaoLactante_Click(object sender, EventArgs e)
        {
            btnSimLactante.CssClass = "btn btn-default";
            btnNaoLactante.CssClass = "btn btn-primary-nutrovet";
            btnSimGestante.Enabled = true;
            btnNaoGestante.Enabled = true;

            divNrFilhotes.Visible = false;
            tbNrFilhotes.Text = "";
            divSemanasLactacao.Visible = false;
            tbSemanLact.Text = "";
            tbFator.Text = "";

            ViewState["Gestante"] = false;
            ViewState["Lactante"] = false;
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                ViewState["_idCardapio"]);

            Salvar(_idCardapio);
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Session.Remove("ToastrCardapios");
            Response.Redirect("~/Cardapio/CardapioSelecao.aspx");
        }

        protected void btnPesqAlimentos_Click(object sender, EventArgs e)
        {
            if (tbPesqAlimentos.Text.Count() >= 3)
            {
                PopulaListaAlimentos(tbPesqAlimentos.Text);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Info,
                    @"São Necessários mais de <b>3</b> Caracteres !",
                    "NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void btnCalculaNEM_Click(object sender, EventArgs e)
        {
            CalcularNEM(true);
        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            int _fator = Funcoes.Funcoes.ConvertePara.Int(ViewState["_valorFator"]);

            tbFator.Text = (_fator > 0 ? Funcoes.Funcoes.ConvertePara.String(_fator) : "0");

            CalcularNEM(true);

            ViewState.Remove("_valorFator");
        }

        protected void btnNão_Click(object sender, EventArgs e)
        {
            CalcularNEM(false);

            ViewState.Remove("_valorFator");
        }

        protected void btnResumoMaisDetalhes_Click(object sender, EventArgs e)
        {
            int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                ViewState["_idCardapio"]);

            if (_idCardapio > 0)
            {
                PopulaResumo(_idCardapio);
                modalResumoCardapio.Show();

                Session.Remove("cardAlimTotais");
            }
        }

        protected void btnIncluirNoCardapio_Click(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idCardapio"]) > 0)
            {
                int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["_idCardapio"]);

                foreach (ListItem item in ltbAlimentos.Items)
                {
                    if (item.Selected)
                    {
                        cardapioAlimentosDcl = new CardapiosAlimento();

                        cardapioAlimentosDcl.IdCardapio = _idCardapio;
                        cardapioAlimentosDcl.IdAlimento = Funcoes.Funcoes.ConvertePara.Int(
                            item.Value);
                        cardapioAlimentosDcl.Quant = Funcoes.Funcoes.ConvertePara.Decimal(
                            tbQuantidadeAlimento.Text);

                        cardapioAlimentosDcl.Ativo = true;
                        cardapioAlimentosDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name);
                        cardapioAlimentosDcl.DataCadastro = DateTime.Now;
                        cardapioAlimentosDcl.IP = Request.UserHostAddress;

                        bllRetorno inserirRet = cardapioAlimentosBll.Inserir(
                            cardapioAlimentosDcl);

                        if (inserirRet.retorno)
                        {
                            //AtualizaGráficos
                            tbPesqAlimentos.Text = "";
                            tbQuantidadeAlimento.Text = "";

                            ltbAlimentos.Visible = false;

                            PopulaAlimento(_idCardapio);

                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Success,
                                item.Text + ", </br>Inserido com Sucesso !",
                                "NutroVET informa - Inserção",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                                "NutroVET informa - Inserção",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }
                    }
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    @"<b>Selecione um Cardápio na Tela Principal</b> !",
                    "NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void rptListagemAlimentos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            MEdit tbQuant = (MEdit)e.Item.FindControl("meBodyQuant");

            int IdCardapAlim = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);
            decimal quant = Funcoes.Funcoes.ConvertePara.Decimal(tbQuant.Text);

            switch (e.CommandName)
            {
                case "infoNutr":
                    {
                        PopulaNutrientesAlim(IdCardapAlim);
                        modalAlimento.Show();

                        break;
                    }
                case "excluir":
                    {
                        if (IdCardapAlim > 0)
                        {
                            ExcluirAlim(IdCardapAlim);
                        }

                        break;
                    }
                case "editar":
                    {
                        if (IdCardapAlim > 0)
                        {
                            EditarAlim(IdCardapAlim, quant);
                        }

                        break;
                    }
            }
        }

        protected void rptListagemNutrientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        Label lblQuant = (Label)e.Item.FindControl("lblQuant");


                        lblQuant.Text = "(" + Funcoes.Funcoes.ConvertePara.String(
                                ViewState["QuantListNutr"]) + " g)";

                        break;
                    }
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        TOAlimentoNutrientesBll alimNutrTO =
                            (TOAlimentoNutrientesBll)e.Item.DataItem;

                        Label lblNutriente = (Label)e.Item.FindControl("lblNutriente");
                        Label lblValorNutr = (Label)e.Item.FindControl("lblValorNutr");
                        Label lblQuantNutr = (Label)e.Item.FindControl("lblQuantNutr");
                        Label lblTotal = (Label)e.Item.FindControl("lblTotal");

                        lblValorNutr.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            alimNutrTO.Valor) > 0 ? string.Format("{0:#,##0.00}",
                            alimNutrTO.Valor) : "0");
                        lblTotal.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            alimNutrTO.ValCalc) > 0 ? string.Format("{0:#,##0.00}",
                            alimNutrTO.ValCalc) : "0");

                        break;
                    }
            }
        }

        protected void rptModalTabelasNutr_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        Label lblTabelaExigNutr = (Label)e.Item.FindControl("lblTabelaExigNutr");

                        lblTabelaExigNutr.Text = ddlTblExigNutr.SelectedItem.Text;
                        break;
                    }
            }
        }

        protected void rptModalTabNut_II_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        Label lblTabelaExigNutr = (Label)e.Item.FindControl("lblTabelaExigNutr");

                        lblTabelaExigNutr.Text = ddlTblExigNutr.SelectedItem.Text;
                        break;
                    }
            }
        }

        protected void rptResumoCardapio_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) ||
                (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                TOCardapioResumoBll Totais =
                    (TOCardapioResumoBll)Session["cardAlimTotais"] ?? new TOCardapioResumoBll();

                cardapioAlimentosTO = (TOCardapioResumoBll)e.Item.DataItem;
                decimal _quant = (Funcoes.Funcoes.ConvertePara.Decimal(
                    cardapioAlimentosTO.Quant) / 100);

                Label lblResQuant = (Label)e.Item.FindControl("lblResQuant");
                Label lblResCarbo = (Label)e.Item.FindControl("lblResCarbo");
                Label lblResProt = (Label)e.Item.FindControl("lblResProt");
                Label lblResGord = (Label)e.Item.FindControl("lblResGord");
                Label lblResFibras = (Label)e.Item.FindControl("lblResFibras");

                lblResQuant.Text = string.Format("{0:#,##0.00}", cardapioAlimentosTO.Quant);
                lblResCarbo.Text = string.Format("{0:#,##0.00}", Math.Round(
                    (cardapioAlimentosTO.Carboidrato != null ?
                    cardapioAlimentosTO.Carboidrato.Value : 0) * _quant, 2));
                lblResProt.Text = string.Format("{0:#,##0.00}",
                    Math.Round((cardapioAlimentosTO.Proteina != null ?
                    cardapioAlimentosTO.Proteina.Value : 0) * _quant, 2));
                lblResGord.Text = string.Format("{0:#,##0.00}",
                    Math.Round((cardapioAlimentosTO.Gordura != null ?
                    cardapioAlimentosTO.Gordura.Value : 0) * _quant, 2));
                lblResFibras.Text = string.Format("{0:#,##0.00}",
                    Math.Round((cardapioAlimentosTO.Fibras != null ?
                    cardapioAlimentosTO.Fibras.Value : 0) * _quant, 2));

                Totais.Quant += cardapioAlimentosTO.Quant;
                Totais.Carboidrato += Math.Round((cardapioAlimentosTO.Carboidrato != null ?
                    cardapioAlimentosTO.Carboidrato.Value : 0) * _quant, 2);
                Totais.Proteina += Math.Round((cardapioAlimentosTO.Proteina != null ?
                    cardapioAlimentosTO.Proteina.Value : 0) * _quant, 2);
                Totais.Gordura += Math.Round((cardapioAlimentosTO.Gordura != null ?
                    cardapioAlimentosTO.Gordura.Value : 0) * _quant, 2);
                Totais.Fibras += Math.Round((cardapioAlimentosTO.Fibras != null ?
                    cardapioAlimentosTO.Fibras.Value : 0) * _quant, 2);

                Session["cardAlimTotais"] = Totais;
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                TOCardapioResumoBll Totais = (TOCardapioResumoBll)Session["cardAlimTotais"];

                Label lblTotQuant = (Label)e.Item.FindControl("lblTotQuant");
                Label lblTotCarb = (Label)e.Item.FindControl("lblTotCarb");
                Label lblTotProt = (Label)e.Item.FindControl("lblTotProt");
                Label lblTotGord = (Label)e.Item.FindControl("lblTotGord");
                Label lblTotFibras = (Label)e.Item.FindControl("lblTotFibras");

                lblTotQuant.Text = string.Format("{0:#,##0.00}",
                    Funcoes.Funcoes.ConvertePara.Decimal(Totais.Quant));
                lblTotCarb.Text = string.Format("{0:#,##0.00}",
                    Funcoes.Funcoes.ConvertePara.Decimal(Totais.Carboidrato));
                lblTotProt.Text = string.Format("{0:#,##0.00}",
                    Funcoes.Funcoes.ConvertePara.Decimal(Totais.Proteina));
                lblTotGord.Text = string.Format("{0:#,##0.00}",
                    Funcoes.Funcoes.ConvertePara.Decimal(Totais.Gordura));
                lblTotFibras.Text = string.Format("{0:#,##0.00}",
                    Funcoes.Funcoes.ConvertePara.Decimal(Totais.Fibras));
            }
        }

        protected void ddlTblExigNutr_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("BalancoDieta");
            Session.Remove("Receituario");

            PopulaTabelaNutricional();

            if (Funcoes.Funcoes.ConvertePara.Int(ddlTblExigNutr.SelectedValue) > 0)
            {
                int _idcardapio = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idCardapio"]);

                string _url = "~/Relatorios/RptBalancoCardapio.aspx?_tabela=" +
                    ddlTblExigNutr.SelectedItem.Text + "&_idCard=" + _idcardapio +
                    "&_quantDietas=" + 1;

                hlImpressaoTbNutr.NavigateUrl = _url;
                hlImpressaoTbNutr.Enabled = true;
                hlImpressaoTbNutr.CssClass = "btn btn-primary-nutrovet";
            }
            else
            {
                hlImpressaoTbNutr.NavigateUrl = "";
                hlImpressaoTbNutr.Enabled = false;
                hlImpressaoTbNutr.CssClass = "btn btn-info";
            }

            acGrupos.SelectedIndex = 0;
        }

        protected void btnInfoNutr_Click(object sender, EventArgs e)
        {
            PopulaExigenciasNutricionais(Funcoes.Funcoes.ConvertePara.Int(
                ddlTblExigNutr.SelectedValue));
            mdTabelasNutr.Show();
        }

        protected void btnHistPesoAtual_Click(object sender, EventArgs e)
        {
            DateTime? _ini = (meDtInicial.Text != "" ? DateTime.Parse(meDtInicial.Text) :
                DateTime.Today.AddYears(-1));
            meDtInicial.Text = _ini.Value.ToString("dd/MM/yyyy");

            DateTime? _fim = (meDtFinal.Text != "" ? DateTime.Parse(meDtFinal.Text) :
                DateTime.Today);
            meDtFinal.Text = _fim.Value.ToString("dd/MM/yyyy");

            BindChart(_ini, _fim);
        }

        protected void lbPesq_Click(object sender, EventArgs e)
        {
            DateTime? _ini = (meDtInicial.Text != "" ? DateTime.Parse(meDtInicial.Text) :
                DateTime.Today.AddYears(-1));
            DateTime? _fim = (meDtFinal.Text != "" ? DateTime.Parse(meDtFinal.Text) :
                DateTime.Today);

            BindChart(_ini, _fim);
        }

        public void BindChart(DateTime? _ini, DateTime? _fim)
        {
            int _idAnimal = Funcoes.Funcoes.ConvertePara.Int(ddlPaciente.SelectedValue);

            if (_idAnimal > 0)
            {
                chartPesoHist.ImageLocation = "~/Imagens/Graficos/PesoHist_" + _idAnimal + ".png";
                List<AnimaisPesoHistorico> listagem = animaisBll.GeraGrafico(_idAnimal, _ini, _fim);

                decimal[] y = new decimal[listagem.Count];
                string[] x = new string[listagem.Count];

                for (int i = 0; i < listagem.Count; i++)
                {
                    x[i] = (listagem[i].DataHistorico == null ? DateTime.Today.ToString("dd/MM/yyyy") :
                        listagem[i].DataHistorico.Value.ToString("dd/MM/yyyy"));
                    y[i] = listagem[i].Peso;
                }

                chartPesoHist.Series["Default"].Points.DataBindXY(x, y);
                chartPesoHist.Series["Default"].ChartType = SeriesChartType.Line;
                chartPesoHist.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -50;
                chartPesoHist.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Verdana", 7, FontStyle.Bold);
                chartPesoHist.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("Verdana", 7, FontStyle.Bold);
                chartPesoHist.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                chartPesoHist.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#e5e5e5");
                chartPesoHist.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = true;
                chartPesoHist.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#e5e5e5");
                chartPesoHist.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font("Tahoma", 6, FontStyle.Bold);
                chartPesoHist.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new Font("Tahoma", 6, FontStyle.Bold);
                chartPesoHist.Series["Default"].IsValueShownAsLabel = true;
                chartPesoHist.ChartAreas["ChartArea1"].AxisY.Title = "Peso Atual";
                chartPesoHist.ChartAreas["ChartArea1"].AxisX.Title = "Data do Histórico";
                chartPesoHist.Width = 540;
                chartPesoHist.Height = 282;

                mdHistoricoPesoAtual.Show();
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Selecione um Paciente para visualizar seu Peso!!!",
                    "NutroVET informa - Gráfico de Peso",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void acGrupos_ItemDataBound(object sender, AccordionItemEventArgs e)
        {
            int _tabNutr = Funcoes.Funcoes.ConvertePara.Int(
                ddlTblExigNutr.SelectedValue);

            switch (e.ItemType)
            {
                case AccordionItemType.Header:
                    {
                        HiddenField hfExigNutr =
                            (HiddenField)e.AccordionItem.FindControl("hfExigNutr");

                        ViewState["IdGrupo"] = Funcoes.Funcoes.ConvertePara.Int(
                            hfExigNutr.Value);

                        break;
                    }
                case AccordionItemType.Content:
                    {
                        Repeater rptTabelasNutricionais = (Repeater)
                            e.AccordionItem.FindControl("rptTabelasNutricionais");

                        Repeater rptTblNutr_II = (Repeater)
                            e.AccordionItem.FindControl("rptTblNutr_II");

                        int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                            ViewState["_idCardapio"]);
                        decimal _energiaDoCardapio = Funcoes.Funcoes.ConvertePara.Decimal(
                            ViewState["EnergiaDoCardapio"]);

                        if (_idCardapio > 0)
                        {
                            TOAnimaisBll dadosPaciente =
                                (TOAnimaisBll)Session["dadosPaciente"] ??
                                    animaisBll.CarregarTO(
                                        Funcoes.Funcoes.ConvertePara.Int(
                                            ddlPaciente.SelectedValue));

                            bool _lactante = Funcoes.Funcoes.ConvertePara.Bool(
                                ViewState["Lactante"]);
                            bool _gestante = Funcoes.Funcoes.ConvertePara.Bool(
                                ViewState["Gestante"]);

                            DominiosBll.ExigenciasNutrAuxIndicacoes situacaoDoAnimal =
                                animaisBll.CrescimentoAnimal(dadosPaciente, _gestante,
                                _lactante);

                            List<TOExigNutrTabelasBll> listagem =
                                cardapioAlimentosBll.ListarCardapioExigNutr(
                                    _energiaDoCardapio, _idCardapio, _tabNutr,
                                Funcoes.Funcoes.ConvertePara.Int(
                                        dadosPaciente.IdEspecie),
                                (int)situacaoDoAnimal,
                                Funcoes.Funcoes.ConvertePara.Int(ViewState["IdGrupo"]));

                            Session["BalancoDieta"] = cardapioAlimentosBll.GeraRelatorio(
                                (List<TOExigNutrTabelasBll>)Session["BalancoDieta"],
                                listagem);

                            switch (_tabNutr)
                            {
                                case 1:
                                case 3:
                                    {
                                        rptTblNutr_II.DataSource = null;
                                        rptTblNutr_II.DataBind();
                                        rptTblNutr_II.Visible = false;

                                        rptTabelasNutricionais.Visible = true;
                                        rptTabelasNutricionais.DataSource = listagem;
                                        rptTabelasNutricionais.DataBind();

                                        break;
                                    }
                                case 2:
                                    {
                                        rptTabelasNutricionais.DataSource = null;
                                        rptTabelasNutricionais.DataBind();
                                        rptTabelasNutricionais.Visible = false;

                                        rptTblNutr_II.Visible = true;
                                        rptTblNutr_II.DataSource = listagem;
                                        rptTblNutr_II.DataBind();

                                        break;
                                    }
                            }
                        }

                        break;
                    }
            }
        }

        protected void rptTabelasNutricionais_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        Label lblEnergia = (Label)e.Item.FindControl("lblEnergia");

                        lblEnergia.Text = string.Format("{0:0}", Math.Round(
                            Funcoes.Funcoes.ConvertePara.Decimal(
                                ViewState["EnergiaDoCardapio"]), 0)) + " Kcal";

                        break;
                    }
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        TOExigNutrTabelasBll exigNutrTabelasTO =
                            (TOExigNutrTabelasBll)e.Item.DataItem;
                        
                        Label lblValMin = (Label)e.Item.FindControl("lblValMin");
                        Label lblValMax = (Label)e.Item.FindControl("lblValMax");
                        Label lblValEmCardapio = (Label)e.Item.FindControl("lblValEmCardapio");
                        Label lblValFalta = (Label)e.Item.FindControl("lblValFalta");
                        Label lblValSobra = (Label)e.Item.FindControl("lblValSobra");

                        lblValMin.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Minimo) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Minimo) : "");
                        lblValMax.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Maximo) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Maximo) : "");
                        lblValEmCardapio.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.EmCardapio) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.EmCardapio) : "");
                        lblValFalta.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Falta) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Falta) : "");

                        if (Funcoes.Funcoes.ConvertePara.Decimal(exigNutrTabelasTO.Falta) > 0)
                        {
                            lblValFalta.CssClass = "TableNutrFalta";
                        }
                        else
                        {
                            lblValFalta.CssClass = "";
                        }

                        if (Funcoes.Funcoes.ConvertePara.Decimal(exigNutrTabelasTO.Sobra) > 0)
                        {
                            lblValSobra.CssClass = "TableNutrSobra";
                        }
                        else
                        {
                            lblValSobra.CssClass = "";
                        }

                        lblValSobra.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Sobra) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Sobra) : "");

                        //Popula Lista para a receita de Suplementação
                        //if ((exigNutrTabelasTO.EmCardapio > 0) || (exigNutrTabelasTO.Falta > 0))
                        //{
                            listReceitExigNutrTO =
                                (List<TOReceituarioNutrientesBll>)Session["Receituario"];

                            if (listReceitExigNutrTO == null)
                            {
                                listReceitExigNutrTO = new List<TOReceituarioNutrientesBll>
                                {
                                    receitNutrBll.ConverterTO(exigNutrTabelasTO)
                                };
                            }
                            else
                            {
                                listReceitExigNutrTO.Add(receitNutrBll.ConverterTO(
                                    exigNutrTabelasTO));
                            }

                            Session["Receituario"] = listReceitExigNutrTO;
                        //}

                        break;
                    }
            }
        }

        protected void rptTblNutr_II_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        Label lblEnergia = (Label)e.Item.FindControl("lblEnergia");

                        lblEnergia.Text = string.Format("{0:0}", Math.Round(
                            Funcoes.Funcoes.ConvertePara.Decimal(
                                ViewState["EnergiaDoCardapio"]), 0)) + " Kcal";

                        break;
                    }
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        TOExigNutrTabelasBll exigNutrTabelasTO =
                            (TOExigNutrTabelasBll)e.Item.DataItem;

                        Label lblValMin = (Label)e.Item.FindControl("lblValMin");
                        Label lblValMax = (Label)e.Item.FindControl("lblValMax");
                        Label lblValAdeq = (Label)e.Item.FindControl("lblValAdeq");
                        Label lblValRecomend = (Label)e.Item.FindControl("lblValRecomend");
                        Label lblValEmCardapio = (Label)e.Item.FindControl("lblValEmCardapio");
                        Label lblValFalta = (Label)e.Item.FindControl("lblValFalta");
                        Label lblValSobra = (Label)e.Item.FindControl("lblValSobra");

                        lblValMin.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Minimo) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Minimo) : "");
                        lblValMax.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Maximo) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Maximo) : "");
                        lblValAdeq.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Adequado) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Adequado) : "");
                        lblValRecomend.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Recomendado) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Recomendado) : "");
                        lblValEmCardapio.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.EmCardapio) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.EmCardapio) : "");
                        lblValFalta.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Falta) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Falta) : "");

                        if (Funcoes.Funcoes.ConvertePara.Decimal(exigNutrTabelasTO.Falta) > 0)
                        {
                            lblValFalta.CssClass = "TableNutrFalta";
                        }
                        else
                        {
                            lblValFalta.CssClass = "";
                        }

                        if (Funcoes.Funcoes.ConvertePara.Decimal(exigNutrTabelasTO.Sobra) > 0)
                        {
                            lblValSobra.CssClass = "TableNutrSobra";
                        }
                        else
                        {
                            lblValSobra.CssClass = "";
                        }

                        lblValSobra.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            exigNutrTabelasTO.Sobra) > 0 ? string.Format("{0:#,##0.00}",
                            exigNutrTabelasTO.Sobra) : "");

                        //Popula Lista para a receita de Suplementação
                        //if ((exigNutrTabelasTO.EmCardapio > 0) || (exigNutrTabelasTO.Falta > 0))
                        //{
                            listReceitExigNutrTO =
                                (List<TOReceituarioNutrientesBll>)Session["Receituario"];

                            if (listReceitExigNutrTO == null)
                            {
                                listReceitExigNutrTO = new List<TOReceituarioNutrientesBll>
                                {
                                    receitNutrBll.ConverterTO(exigNutrTabelasTO)
                                };
                            }
                            else
                            {
                                listReceitExigNutrTO.Add(receitNutrBll.ConverterTO(
                                    exigNutrTabelasTO));
                            }

                            Session["Receituario"] = listReceitExigNutrTO;
                        //}

                        break;
                    }
            }
        }

        protected void mvTabControl_ActiveViewChanged(object sender, EventArgs e)
        {
            switch (mvTabControl.ActiveViewIndex)
            {
                case 2:
                    {
                        PopulaTabelaNutricional();

                        break;
                    }
            }
        }

        protected void rptListagemAlimentos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        Label lblBodyAlimento = (Label)e.Item.FindControl("lblBodyAlimento");
                        MEdit meBodyQuant = (MEdit)e.Item.FindControl("meBodyQuant");
                        TOCardapioAlimentosBll _cardAlim = (TOCardapioAlimentosBll)e.Item.DataItem;

                        List<TODietasAlimentosBll> listarIndic = dietasAlimentosBll.ListarTO(
                            Funcoes.Funcoes.ConvertePara.Int(ddlSugestaoDieta.SelectedValue),
                            (int)DominiosBll.DietasAlimentosRecomendacao.Indicado);
                        List<TODietasAlimentosBll> listarContraIndic = dietasAlimentosBll.ListarTO(
                            Funcoes.Funcoes.ConvertePara.Int(ddlSugestaoDieta.SelectedValue),
                            (int)DominiosBll.DietasAlimentosRecomendacao.Contraindicado);

                        int _indic = (from i in listarIndic
                                      where i.IdAlimento == _cardAlim.IdAlimento
                                      select i).Count();
                        int _contraIndic = (from i in listarContraIndic
                                            where i.IdAlimento == _cardAlim.IdAlimento
                                            select i).Count();

                        meBodyQuant.Text = string.Format("{0:#,##0.00}", _cardAlim.Quant);

                        if (_contraIndic > 0)
                        {
                            lblBodyAlimento.Font.Bold = true;
                            lblBodyAlimento.CssClass = "text-danger";
                        }
                        else if (_indic > 0)
                        {
                            lblBodyAlimento.Font.Bold = true;
                            lblBodyAlimento.CssClass = "text-success";
                        }
                        else if ((_indic <= 0) && (_indic <= 0))
                        {
                            lblBodyAlimento.Font.Bold = false;
                            lblBodyAlimento.CssClass = "text-body";
                        }

                        break;
                    }
                case ListItemType.Footer:
                    {
                        Label lblTotQuant = (Label)e.Item.FindControl("lblTotQuant");
                        decimal _total = cardapioAlimentosBll.SomaQuantidadesDoCardapio(
                                Funcoes.Funcoes.ConvertePara.Int(
                                    ViewState["_idCardapio"]));

                        lblTotQuant.Text = (_total > 0 ? string.Format(
                            "{0:#,##0.00} g", _total) : "");

                        break;
                    }
            }
        }

        protected void rptListagemDistCat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        BICardapioAlimentosBll biCardAlim =
                            (BICardapioAlimentosBll)e.Item.DataItem;

                        Label lblBodyPerc = (Label)e.Item.FindControl("lblBodyPerc");
                        Label lblBodyQuant = (Label)e.Item.FindControl("lblBodyQuant");

                        lblBodyQuant.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            biCardAlim.SomaQuant) > 0 ?
                                string.Format("{0:#,##0.00}",
                                    biCardAlim.SomaQuant) : "");
                        lblBodyPerc.Text = (Funcoes.Funcoes.ConvertePara.Decimal(
                            biCardAlim.PercentQuant) > 0 ?
                                string.Format("{0:#,##0.00}",
                                    biCardAlim.PercentQuant) : "");

                        break;
                    }
            }
        }

        protected void btnCancelaAlimentos_Click(object sender, EventArgs e)
        {
            tbPesqAlimentos.Text = "";
            tbQuantidadeAlimento.Text = "";

            ltbAlimentos.DataSource = null;
            ltbAlimentos.DataBind();
            ltbAlimentos.Visible = false;
        }

        protected void ltbAlimentos_DataBound(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
            List<TODietasAlimentosBll> listarIndic = dietasAlimentosBll.ListarTO(
                Funcoes.Funcoes.ConvertePara.Int(ddlSugestaoDieta.SelectedValue),
                (int)DominiosBll.DietasAlimentosRecomendacao.Indicado);
            List<TODietasAlimentosBll> listarContraIndic = dietasAlimentosBll.ListarTO(
                Funcoes.Funcoes.ConvertePara.Int(ddlSugestaoDieta.SelectedValue),
                (int)DominiosBll.DietasAlimentosRecomendacao.Contraindicado);

            foreach (ListItem item in lb.Items)
            {
                int _indic = (from i in listarIndic
                              where i.IdAlimento == Funcoes.Funcoes.ConvertePara.Int(item.Value)
                              select i).Count();
                int _contraIndic = (from i in listarContraIndic
                                    where i.IdAlimento == Funcoes.Funcoes.ConvertePara.Int(item.Value)
                                    select i).Count();

                if (_contraIndic > 0)
                {
                    item.Attributes["style"] = "font-weight: bold;";
                    item.Attributes["class"] = "text-danger";
                }
                else if (_indic > 0)
                {
                    item.Attributes["style"] = "font-weight: bold;";
                    item.Attributes["class"] = "text-success";
                }
                else if ((_indic <= 0) && (_indic <= 0))
                {
                    item.Attributes["style"] = "font-weight: none;";
                    item.Attributes["class"] = "text-body";
                }
            }
        }

        protected void ddlReceituario_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _receitaTp = Funcoes.Funcoes.ConvertePara.Int(ddlReceituario.SelectedValue);
            string _idCardapio = Funcoes.Funcoes.ConvertePara.String(ViewState["_idCardapio"]);
            bllRetorno _permissao = configReceitBll.PermissaoAcessoReceituario(
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));

            if (Session["Receituario"] != null)
            {
                if (_permissao.retorno)
                {
                    switch (_receitaTp)
                    {
                        case 1:
                            {
                                Response.Redirect("~/Receituario/RecSuplementacao.aspx?" +
                                    "_idCardapio=" + Funcoes.Funcoes.Seguranca.Criptografar(
                                        _idCardapio));

                                break;
                            }
                        case 2:
                            {
                                Response.Redirect("~/Receituario/RecNutraceuticos.aspx?" +
                                    "_idCardapio=" + Funcoes.Funcoes.Seguranca.Criptografar(
                                        _idCardapio));

                                break;
                            }
                        case 3:
                            {
                                Response.Redirect("~/Receituario/RecBranco.aspx?" +
                                    "_idCardapio=" + Funcoes.Funcoes.Seguranca.Criptografar(
                                        _idCardapio));

                                break;
                            }
                    }
                }
                else
                {
                    lblDescricaoReceit.Text = _permissao.mensagem;

                    popUpReceituario.Show();
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "</br>É Necessário que seja Elaborada uma Composição de Dieta!!!",
                    "NutroVET Informa - Receituário",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void btnImpressaoCardapio_Click(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idCardapio"]) > 0)
            {
                string _url = "~/Cardapio/Impressao/CardapioImpressao.aspx?_idCardapio=" +
                Funcoes.Funcoes.Seguranca.Criptografar(
                    Funcoes.Funcoes.ConvertePara.String(ViewState["_idCardapio"]));

                Response.Redirect(_url);
            }
        }
    }
}