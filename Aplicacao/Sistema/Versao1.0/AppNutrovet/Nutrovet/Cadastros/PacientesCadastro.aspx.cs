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
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace Nutrovet.Cadastros
{
    public partial class PacientesCadastro : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clAnimaisBll animaisBll = new clAnimaisBll();
        protected Animai animaisDcl;
        protected TOAnimaisBll animaisTO;
        protected TOAnimaisBll animaisTOPesoHistorico;
        protected clAnimaisAuxEspeciesBll especiesBll = new clAnimaisAuxEspeciesBll();
        protected clAnimaisAuxRacasBll racasBll = new clAnimaisAuxRacasBll();
        protected AnimaisAuxRaca racasDcl;
        protected clPessoasBll PessoaBll = new clPessoasBll();
        protected clAnimaisPesoHistoricoBll pesoHistBll = new clAnimaisPesoHistoricoBll();
        protected AnimaisPesoHistorico pesoHistDcl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (acessosBll.Permissao(
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "4.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        int _idPaciente = Funcoes.Funcoes.ConvertePara.Int(
                            Funcoes.Funcoes.Seguranca.Descriptografar(
                                Funcoes.Funcoes.ConvertePara.String(
                                    Request.QueryString["_idPaciente"])));
                        ViewState["_idPaciente"] = _idPaciente;

                        PopularTutor(Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name));
                        PopulaEspecie();
                        PopulaSexo();
                        PopulaTela(_idPaciente);
                        //SituacaoPlano();

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

        //private void SituacaoPlano()
        //{
        //    if (!animaisBll.PodeCadastrarAnimal(Funcoes.Funcoes.ConvertePara.Int(
        //            User.Identity.Name)) && (Funcoes.Funcoes.ConvertePara.Int(
        //                Session["situacaoPlano"]) <= 1))
        //    {
        //        if (Funcoes.Funcoes.ConvertePara.Int(Session["situacaoPlano"]) <= 1)
        //        {
        //            Session["situacaoPlano"] = Funcoes.Funcoes.ConvertePara.Int(
        //                Session["situacaoPlano"]) + 1;
        //        }

        //        lblSituacaoPlano.Text = @"Você atingiu o limite de cadastros de pacientes para o seu Plano!!!
        //            </br>
        //            Faça uma migração de Plano que permita aumentar o limite ou contate o Administrador!!!";
        //        mdSituacaoPlano.Show();
        //    }
        //}

        private void PopulaTela(int _idPaciente)
        {
            if (_idPaciente > 0)
            {
                animaisTO = animaisBll.CarregarTO(_idPaciente);

                lblTitulo.Text = "Alteração de Paciente";
                lblPagina.Text = "Alterar Paciente";
                lblSubTitulo.Text = "Altere aqui os dados do paciente!";

                Funcoes.Funcoes.ControlForm.SetComboBox(ddlTutor, animaisTO.IdPessoa);
                tbNomePaciente.Text = animaisTO.Animal;

                if (Funcoes.Funcoes.ConvertePara.Int(animaisTO.IdRaca) > 0)
                {
                    racasDcl = racasBll.Carregar(animaisTO.IdRaca.Value);
                    Funcoes.Funcoes.ControlForm.SetComboBox(ddlEspecie,
                        racasDcl.IdEspecie);
                    PopulaRaca(racasDcl.IdEspecie);
                    Funcoes.Funcoes.ControlForm.SetComboBox(ddlRaca,
                        animaisTO.IdRaca);
                }
                else
                {
                    Funcoes.Funcoes.ControlForm.SetComboBox(ddlEspecie, 0);
                    PopulaRaca(0);
                }


                meDtNasc.Text = (animaisTO.DtNascim != null ?
                    animaisTO.DtNascim.Value.ToString("dd/MM/yyyy") : "");

                tbIdadeAnos.Text = Funcoes.Funcoes.ConvertePara.String(
                    animaisTO.IdadeAno);
                tbIdadeMeses.Text = Funcoes.Funcoes.ConvertePara.String(
                    animaisTO.IdadeMes);
                tbIdadeDias.Text = Funcoes.Funcoes.ConvertePara.String(
                    animaisTO.IdadeDia);

                Funcoes.Funcoes.ControlForm.SetComboBox(ddlSexo, animaisTO.IdSexo);
                tbPesoAtual.Text = Funcoes.Funcoes.ConvertePara.String(
                    animaisTO.PesoAtual);
                tbPesoIdeal.Text = Funcoes.Funcoes.ConvertePara.String(
                    animaisTO.PesoIdeal);

                ftbObs.Text = animaisTO.Observacoes;
            }
            else
            {
                lblTitulo.Text = "Inserção de Paciente";
                lblPagina.Text = "Inserir Paciente";
                lblSubTitulo.Text = "Insira aqui os dados do paciente!";
            }
        }

        private void PopulaSexo()
        {
            ddlSexo.Items.AddRange(animaisBll.ListarSexo());
            ddlSexo.DataBind();

            ddlSexo.Items.Insert(0, new ListItem("- Selecione -", "0"));
        }

        private void PopulaEspecie()
        {
            ddlEspecie.DataTextField = "Nome";
            ddlEspecie.DataValueField = "Id";
            ddlEspecie.DataSource = especiesBll.Listar();
            ddlEspecie.DataBind();

            ddlEspecie.Items.Insert(0, new ListItem("- Selecione -", "0"));
            ddlRaca.Items.Insert(0, new ListItem("- Selecione -", "0"));
        }

        private void PopulaRaca(int _idEspecie)
        {
            ddlRaca.DataTextField = "Raca";
            ddlRaca.DataValueField = "IdRaca";
            ddlRaca.DataSource = racasBll.Listar(_idEspecie);
            ddlRaca.DataBind();

            ddlRaca.Items.Insert(0, new ListItem("- Selecione -", "0"));
        }

        private void PopularTutor(int _idCliente)
        {
            List<TOPessoasBll> listagem = PessoaBll.CachePessoas().
                Where(p => p.IdCliente == _idCliente).ToList();

            ddlTutor.DataTextField = "Nome";
            ddlTutor.DataValueField = "IdPessoa";
            ddlTutor.DataSource = listagem;
            ddlTutor.DataBind();

            ddlTutor.Items.Insert(0, new ListItem("- Selecione -", "0"));
        }

        protected void ddlEspecie_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaRaca(Funcoes.Funcoes.ConvertePara.Int(ddlEspecie.SelectedValue));
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            int _idPaciente = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idPaciente"]);

            Salvar(_idPaciente);
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

        private void Inserir()
        {
            animaisDcl = new Animai();

            animaisDcl.IdPessoa = Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue);
            animaisDcl.Nome = tbNomePaciente.Text;
            animaisDcl.IdRaca = Funcoes.Funcoes.ConvertePara.Int(ddlRaca.SelectedValue);
            animaisDcl.DtNascim = (meDtNasc.Text != "" ?
                DateTime.Parse(meDtNasc.Text) :
                DateTime.Parse("01/01/1910"));
            animaisDcl.Sexo = Funcoes.Funcoes.ConvertePara.Int(ddlSexo.SelectedValue);

            #region Cadastro do Peso

            if (Funcoes.Funcoes.ConvertePara.Decimal(tbPesoAtual.Text) > 0)
            {
                pesoHistDcl = new AnimaisPesoHistorico();

                pesoHistDcl.Peso = Funcoes.Funcoes.ConvertePara.Decimal(tbPesoAtual.Text);
                animaisDcl.PesoAtual = Funcoes.Funcoes.ConvertePara.Decimal(
                    tbPesoAtual.Text);
                pesoHistDcl.DataHistorico = DateTime.Today;
                pesoHistDcl.Ativo = true;
                pesoHistDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                pesoHistDcl.DataCadastro = DateTime.Now;
                pesoHistDcl.IP = Request.UserHostAddress;

                animaisDcl.AnimaisPesoHistoricos.Add(pesoHistDcl);
            }
            else
            {
                animaisDcl.PesoAtual = 0;
            }

            #endregion

            animaisDcl.PesoIdeal = Funcoes.Funcoes.ConvertePara.Decimal(tbPesoIdeal.Text);
            animaisDcl.Observacoes = ftbObs.Text;
            animaisDcl.RecalcularNEM = true;

            animaisDcl.Ativo = true;
            animaisDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            animaisDcl.DataCadastro = DateTime.Now;
            animaisDcl.IP = Request.UserHostAddress;

            bllRetorno inserirRet = animaisBll.Inserir(animaisDcl);

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
                Response.Redirect("~/Cadastros/PacientesSelecao.aspx");
            }
            else
            {

                tbIdadeAnos.Text = (animaisDcl.DtNascim != null ?
                    Funcoes.Funcoes.ConvertePara.String(
                        Funcoes.Funcoes.Datas.CalculaIdade(animaisDcl.DtNascim.Value)) : "0");

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                    "NutroVET informa - Inserção", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Cancelar()
        {
            ViewState.Remove("_idPaciente");
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlTutor, 0);
            tbNomePaciente.Text = "";
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlEspecie, 0);
            PopulaRaca(0);
            meDtNasc.Text = "";
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlSexo, 0);
            tbPesoAtual.Text = "";
            tbPesoIdeal.Text = "";
        }

        private void Alterar(int _id)
        {
            animaisDcl = animaisBll.Carregar(_id);

            decimal? _pesoIdeal = animaisDcl.PesoIdeal;

            animaisDcl.IdPessoa = Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue);
            animaisDcl.Nome = tbNomePaciente.Text;
            animaisDcl.IdRaca = Funcoes.Funcoes.ConvertePara.Int(ddlRaca.SelectedValue);
            animaisDcl.DtNascim = (meDtNasc.Text != "" ?
                DateTime.Parse(meDtNasc.Text) :
                DateTime.Parse("01/01/1910"));

            animaisDcl.Sexo = Funcoes.Funcoes.ConvertePara.Int(ddlSexo.SelectedValue);

            #region Cadastro do Peso

            decimal _pesoAtual = Funcoes.Funcoes.ConvertePara.Decimal(
                tbPesoAtual.Text);

            if ((animaisDcl.PesoAtual != _pesoAtual) || (!pesoHistBll.PesoExiste(animaisDcl)))
            {
                pesoHistDcl = new AnimaisPesoHistorico();
                animaisDcl.PesoAtual = Funcoes.Funcoes.ConvertePara.Decimal(
                    tbPesoAtual.Text);

                pesoHistDcl.IdAnimal = animaisDcl.IdAnimal;
                pesoHistDcl.Peso = Funcoes.Funcoes.ConvertePara.Decimal(
                    tbPesoAtual.Text);
                pesoHistDcl.DataHistorico = DateTime.Today;
                pesoHistDcl.Ativo = true;
                pesoHistDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                pesoHistDcl.DataCadastro = DateTime.Now;
                pesoHistDcl.IP = Request.UserHostAddress;

                pesoHistBll.Inserir(pesoHistDcl);
            }

            #endregion

            animaisDcl.PesoIdeal = Funcoes.Funcoes.ConvertePara.Decimal(
                tbPesoIdeal.Text);
            animaisDcl.Observacoes = ftbObs.Text;
            animaisDcl.RecalcularNEM = (animaisDcl.PesoIdeal != _pesoIdeal ? true : false);

            animaisDcl.Ativo = true;
            animaisDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            animaisDcl.DataCadastro = DateTime.Now;
            animaisDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = animaisBll.Alterar(animaisDcl);

            if (alterarRet.retorno)
            {
                TOToastr _tostr = new TOToastr
                {
                    Tipo = 'S',
                    Titulo = "NutroVET informa - Inserção",
                    Mensagem = alterarRet.mensagem
                };

                Session["ToastrPacientes"] = _tostr;
                Response.Redirect("~/Cadastros/PacientesSelecao.aspx");
            }
            else
            {
                tbIdadeAnos.Text = (animaisDcl.DtNascim != null ?
                    Funcoes.Funcoes.ConvertePara.String(
                        Funcoes.Funcoes.Datas.CalculaIdade(animaisDcl.DtNascim.Value)) : "0");

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, alterarRet.mensagem,
                    "NutroVET informa - Alteração", Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void btnHistPesoAtual_Click(object sender, EventArgs e)
        {
            DateTime? _ini = (meDtInicial.Text != "" ? DateTime.Parse(meDtInicial.Text) :
                DateTime.Parse("01/01/" + DateTime.Today.Year));
            DateTime? _fim = (meDtFinal.Text != "" ? DateTime.Parse(meDtFinal.Text) :
                DateTime.Parse("31/12/" + DateTime.Today.Year));

            BindChart(_ini, _fim);

            mdHistoricoPesoAtual.Show();
        }

        protected void lbPesq_Click(object sender, EventArgs e)
        {
            DateTime? _ini = (meDtInicial.Text != "" ? DateTime.Parse(meDtInicial.Text) :
                DateTime.Parse("01/01/" + DateTime.Today.Year));
            DateTime? _fim = (meDtFinal.Text != "" ? DateTime.Parse(meDtFinal.Text) :
                DateTime.Parse("31/12/" + DateTime.Today.Year));

            BindChart(_ini, _fim);

            mdHistoricoPesoAtual.Show();
        }

        public void BindChart(DateTime? _ini, DateTime? _fim)
        {
            chartPesoHist.ImageLocation = "~/Imagens/Graficos/PesoHist_" +
                Funcoes.Funcoes.ConvertePara.Int(ViewState["_idPaciente"]) + ".png";
            List<AnimaisPesoHistorico> listagem = animaisBll.GeraGrafico(Funcoes.Funcoes.ConvertePara.Int(
                ViewState["_idPaciente"]), _ini, _fim);

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
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Session.Remove("ToastrPacientes");
            Response.Redirect("~/Cadastros/PacientesSelecao.aspx");
        }
    }
}
