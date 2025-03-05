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
using System.Security;
using System.Reflection.Emit;

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
        protected TOPessoasBll pessoasTO;
        protected clTutoresBll tutoresBll = new clTutoresBll();
        protected Tutore tutoresDcl;
        protected TOTutoresBll tutoresTO;
        protected clAnimaisPesoHistoricoBll pesoHistBll = new clAnimaisPesoHistoricoBll();
        protected AnimaisPesoHistorico pesoHistDcl;
        protected clLogsSistemaBll logsBll = new clLogsSistemaBll();
        protected LogsSistema logsDcl;
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

        private void PopulaTela(int _idPaciente)
        {
            
            //Realiza a verificação para exibir ou ocultar o modal a respeito da funcionalidade Linha do Tempo
            bllRetorno _permissao = configReceitBll.PermissaoAcessoLinhaTempo(
            Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
                
            hfDtInicioLT.Value = "";
            if(_permissao.objeto != null )
            {
                if (_permissao.objeto.Count == 1)
                {
                    ConfigReceituario configReceitDcl = (ConfigReceituario)_permissao.objeto[0];
                    if (configReceitDcl != null && configReceitDcl.DtIniUsoLT != null)
                    {
                        hfDtInicioLT.Value = configReceitDcl.DtIniUsoLT.ToString();
                    }
                }
            }

            if (_idPaciente > 0)
            {

                animaisTO = animaisBll.CarregarTO(_idPaciente);

                lblTitulo.Text = "Alteração de Paciente";
                lblPagina.Text = "Alterar Paciente";
                //lblSubTitulo.Text = "Altere aqui os dados do paciente!";

                Funcoes.Funcoes.ControlForm.SetComboBox(ddlTutor, animaisTO.IdTutor);
                tbNomePaciente.Text = animaisTO.Animal;

                if (Funcoes.Funcoes.ConvertePara.Int(animaisTO.IdRaca) > 0)
                {
                    Funcoes.Funcoes.ControlForm.SetComboBox(ddlEspecie,
                        animaisTO.IdEspecie);
                    PopulaRaca(Funcoes.Funcoes.ConvertePara.Int(animaisTO.IdEspecie));
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

        //Verificar
        private void PopularTutor(int _idCliente)
        {
            List<TOTutoresBll> listagem = tutoresBll.Listar(true, _idCliente);

            ddlTutor.DataTextField = "Tutor";
            ddlTutor.DataValueField = "IdTutor";
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
            if (Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue) > 0)
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
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Selecione um Tutor!!!",
                    "NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
            }
        }

        private void LogInsercaoSucesso(Tutore _tutor, Animai _paciente)
        {
            tutoresTO = tutoresBll.CarregarTO(_tutor.IdTutores);
            logsDcl = new LogsSistema();

            logsDcl.IdPessoa = _tutor.IdCliente;
            logsDcl.IdTabela = (int)DominiosBll.LogTabelas.Animais;
            logsDcl.IdAcao = (int)DominiosBll.AcoesCrud.Inserir;
            logsDcl.Justificativa = "";
            logsDcl.Mensagem = string.Format(
                "Assinante {0} efetuou a inserção do Paciente {1}, pertencente ao Tutor {2}, " +
                "em {3}", tutoresTO.Cliente, _paciente.Nome, tutoresTO.Tutor,
                DateTime.Today.ToString("dd/MM/yyyy"));
            logsDcl.Datahora = DateTime.Now;

            bllRetorno bllRetorno = logsBll.Inserir(logsDcl);
        }

        private void LogInsercaoErro(Tutore _tutor, Animai _paciente, string _msgErro)
        {
            tutoresTO = tutoresBll.CarregarTO(_tutor.IdTutores);
            logsDcl = new LogsSistema();

            logsDcl.IdPessoa = _tutor.IdCliente;
            logsDcl.IdTabela = (int)DominiosBll.LogTabelas.Animais;
            logsDcl.IdAcao = (int)DominiosBll.AcoesCrud.Inserir;
            logsDcl.Justificativa = "";
            logsDcl.Mensagem = string.Format(
                "Assinante {0} tentou inserir do Paciente {1}, pertencente ao Tutor {2}, " +
                "em {3}, com a seguinte mensagem de erro: {4}", tutoresTO.Cliente,
                _paciente.Nome, tutoresTO.Tutor, DateTime.Today.ToString("dd/MM/yyyy"), _msgErro);
            logsDcl.Datahora = DateTime.Now;

            bllRetorno bllRetorno = logsBll.Inserir(logsDcl);
        }

        private void LogAlteracao(Tutore _tutor, Animai _paciente)
        {
            tutoresTO = tutoresBll.CarregarTO(_tutor.IdTutores);
            logsDcl = new LogsSistema();

            if (tutoresTO != null)
            {
                logsDcl.IdPessoa = tutoresTO.IdCliente;
                logsDcl.IdTabela = (int)DominiosBll.LogTabelas.Animais;
                logsDcl.IdAcao = (int)DominiosBll.AcoesCrud.Alterar;
                logsDcl.Justificativa = "";
                logsDcl.Mensagem = string.Format(
                    "Assinante {0} efetuou a alteração do Paciente {1}, pertencente ao " +
                    "Tutor {2}, em {3}", tutoresTO.Cliente, _paciente.Nome, tutoresTO.Tutor,
                    DateTime.Today.ToString("dd/MM/yyyy"));
                logsDcl.Datahora = DateTime.Now;

                bllRetorno bllRetorno = logsBll.Inserir(logsDcl);
            }
        }

        private void Inserir()
        {
            animaisDcl = new Animai();

            tutoresDcl = tutoresBll.Carregar(
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
                Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue));

            animaisDcl.IdTutores = tutoresDcl.IdTutores;
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
                LogInsercaoSucesso(tutoresDcl, animaisDcl);

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
                string _msg = "";

                if (inserirRet.objeto != null)
                {
                    _msg = inserirRet.objeto[0].ToString();
                }

                tbIdadeAnos.Text = (animaisDcl.DtNascim != null ?
                    Funcoes.Funcoes.ConvertePara.String(Funcoes.Funcoes.Datas.CalculaIdade(
                        animaisDcl.DtNascim.Value)) : "0");

                LogInsercaoErro(tutoresDcl, animaisDcl,
                    (_msg != "" ? _msg : inserirRet.mensagem));

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
            tutoresDcl = tutoresBll.Carregar(
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
                Funcoes.Funcoes.ConvertePara.Int(ddlTutor.SelectedValue));

            decimal? _pesoIdeal = animaisDcl.PesoIdeal;

            animaisDcl.IdTutores = tutoresDcl.IdTutores;
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
                LogAlteracao(tutoresDcl, animaisDcl);

                TOToastr _tostr = new TOToastr
                {
                    Tipo = 'S',
                    Titulo = "NutroVET informa - Alteração",
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
                DateTime.Today.AddYears(-1));
            meDtInicial.Text = _ini.Value.ToString("dd/MM/yyyy");

            DateTime? _fim = (meDtFinal.Text != "" ? DateTime.Parse(meDtFinal.Text) :
                DateTime.Today);
            meDtFinal.Text = _fim.Value.ToString("dd/MM/yyyy");

            BindChart(_ini, _fim);

            mdHistoricoPesoAtual.Show();
        }

        protected void lbPesq_Click(object sender, EventArgs e)
        {
            DateTime? _ini = (meDtInicial.Text != "" ? DateTime.Parse(meDtInicial.Text) :
                DateTime.Today.AddYears(-1));
            DateTime? _fim = (meDtFinal.Text != "" ? DateTime.Parse(meDtFinal.Text) :
                DateTime.Today);

            BindChart(_ini, _fim);

            mdHistoricoPesoAtual.Show();
        }

        public void BindChart(DateTime? _ini, DateTime? _fim)
        {
            chartPesoHist.ImageLocation = "~/Imagens/Graficos/PesoHist_" +
                Funcoes.Funcoes.ConvertePara.Int(ViewState["_idPaciente"]) + ".png";
            List<AnimaisPesoHistorico> listagem = animaisBll.GeraGrafico(
                Funcoes.Funcoes.ConvertePara.Int(ViewState["_idPaciente"]), _ini, _fim);

            decimal[] y = new decimal[listagem.Count];
            string[] x = new string[listagem.Count];

            for (int i = 0; i < listagem.Count; i++)
            {
                x[i] = (listagem[i].DataHistorico == null ?
                            DateTime.Today.ToString("dd/MM/yyyy") :
                            listagem[i].DataHistorico.Value.ToString("dd/MM/yyyy"));
                y[i] = listagem[i].Peso;
            }

            chartPesoHist.Series["Default"].Points.DataBindXY(x, y);
            chartPesoHist.Series["Default"].ChartType = SeriesChartType.Line;
            chartPesoHist.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -50;
            chartPesoHist.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font(
                "Verdana", 7, FontStyle.Bold);
            chartPesoHist.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font(
                "Verdana", 7, FontStyle.Bold);
            chartPesoHist.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
            chartPesoHist.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor =
                ColorTranslator.FromHtml("#e5e5e5");
            chartPesoHist.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = true;
            chartPesoHist.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor =
                ColorTranslator.FromHtml("#e5e5e5");
            chartPesoHist.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font(
                "Tahoma", 6, FontStyle.Bold);
            chartPesoHist.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new Font(
                "Tahoma", 6, FontStyle.Bold);
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

        protected void lbkPerfil_Click(object sender, EventArgs e)
        {
            Session.Remove("ToastrPacientes");
            Response.Redirect("~/Perfil/Perfil.aspx");
        }
    }
}
