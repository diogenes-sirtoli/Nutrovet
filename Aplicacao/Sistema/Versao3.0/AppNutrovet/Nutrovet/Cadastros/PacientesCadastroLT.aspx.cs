using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.UI.HtmlControls;

namespace Nutrovet.Cadastros
{
    public partial class PacientesCadastroLT : System.Web.UI.Page
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
        protected clAtendimentoBll atendBll = new clAtendimentoBll();
        protected Atendimento atendDcl;
        protected TOAtendimentoBll atendTO;
        protected TOLinhaTempoBll linhaTempoTO;
        protected clAtendimentoAuxTiposBll tpAtendBll = new clAtendimentoAuxTiposBll();
        protected AtendimentoAuxTipo tpAtendDcl;
        protected clReceituarioBll receituarioBll = new clReceituarioBll();
        protected DCL.Receituario receituarioDcl;
        protected TOReceituarioBll receituarioTO;
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;

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
                        int _idCliente = Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name);
                        ViewState["_idPaciente"] = _idPaciente;

                        PopularTutor(_idCliente);
                        PopulaEspecie();
                        PopulaSexo();
                        PopulaTiposAtendimentos();
                        PopulaTela(_idPaciente);
                        PopulaLinhaTempoAnos(_idCliente, _idPaciente);

                        PopulaHistPesoAtual();

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

        private void PopulaLinhaTempoAnos(int idCliente, int idPaciente)
        {
            List<TOLinhaTempoBll> _listLT = atendBll.ListarLinhaTempo(idCliente,
                idPaciente);
            List<int> _anosLT = atendBll.ListarAnosLinhaTempo(_listLT);
            ViewState["_listLT"] = _listLT;

            rptLTAnos.DataSource = _anosLT;
            rptLTAnos.DataBind();
        }

        private void PopulaTela(int _idPaciente)
        {
            if (_idPaciente > 0)
            {
                animaisTO = animaisBll.CarregarTO(_idPaciente);

                lblTitulo.Text = "Alteração de Paciente";
                lblPagina.Text = "Alterar Paciente";
                //lblSubTitulo.Text = "Altere aqui os dados do paciente!";
                tituloModalCadastroPaciente.Text = "Altere" +
                    " nos campos abaixo os dados do paciente";

                Funcoes.Funcoes.ControlForm.SetComboBox(ddlTutor, animaisTO.IdTutor);
                tbNomePaciente.Text = animaisTO.Animal;

                lbCardNomePaciente.Text = animaisTO.Animal;
                lbCardNomeTutorValor.Text = animaisTO.tNome;
                lbCardEspeciePacienteValor.Text = animaisTO.Especie;
                lbCardRacaPacienteValor.Text = animaisTO.Raca;
                //lbCardSexoPacienteValor.Text = animaisTO.Sexo;
                //lbCardDataNascPacienteValor.Text = (animaisTO.DtNascim != null ?
                //    animaisTO.DtNascim.Value.ToString("dd/MM/yyyy") : "");
                lbCardIdadePacienteValor.Text = string.Concat(
                    Funcoes.Funcoes.ConvertePara.String(animaisTO.IdadeAno), animaisTO.IdadeAno == 1 ? " ano " : " anos ",
                    Funcoes.Funcoes.ConvertePara.String(animaisTO.IdadeMes), animaisTO.IdadeMes == 1 ? " mês " : " meses ",
                    Funcoes.Funcoes.ConvertePara.String(animaisTO.IdadeDia), animaisTO.IdadeDia == 1 ? " dia ": " dias ");
                //lbCardRGPacienteValor.Text = animaisTO.RgPet;
                //lbCardPesoAtualPacienteValor.Text = Funcoes.Funcoes.ConvertePara.String(
                //    animaisTO.PesoAtual);
                //lbCardPesoIdealPacienteValor.Text = Funcoes.Funcoes.ConvertePara.String(
                //    animaisTO.PesoIdeal);
                //lbCardObsPacienteValor.Text = animaisTO.Observacoes;
                hfidPacienteCard.Value = Funcoes.Funcoes.ConvertePara.String(animaisTO.IdAnimal);
                imgCardFotoPaciente.ImageUrl = CarregarImagem(animaisTO.IdAnimal);


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

                tbRgPet.Text = animaisTO.RgPet;
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
                lbCardNomePaciente.Text = "";
                lbCardNomeTutorValor.Text = "";
                lbCardEspeciePacienteValor.Text = "";
                lbCardRacaPacienteValor.Text = "";
                //lbCardSexoPacienteValor.Text = "";
                //lbCardDataNascPacienteValor.Text = ("");
                lbCardIdadePacienteValor.Text = "";
                //lbCardRGPacienteValor.Text = "";
                //lbCardPesoAtualPacienteValor.Text = "";
                //lbCardPesoIdealPacienteValor.Text = "";
                //lbCardObsPacienteValor.Text = "";
                hfidPacienteCard.Value = "";

                lblTitulo.Text = "Inserção de Paciente";
                lblPagina.Text = "Inserir Paciente";
                //lblSubTitulo.Text = "Insira aqui os dados do paciente!";
                tituloModalCadastroPaciente.Text = "Insira nos campos abaixo os dados do paciente";
                mdAlterandoDadosPaciente.Show();
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

        private void PopulaTiposAtendimentos()
        {
            ddlTipoAtendimentoLT.DataTextField = "Nome";
            ddlTipoAtendimentoLT.DataValueField = "Id";
            ddlTipoAtendimentoLT.DataSource = tpAtendBll.Listar();
            ddlTipoAtendimentoLT.DataBind();

            ddlTipoAtendimentoLT.Items.Insert(0, new ListItem("- Selecione -", "0"));
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
            mdAlterandoDadosPaciente.Show();
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
            animaisDcl.RgPet = tbRgPet.Text;
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
                ViewState["_idPaciente"] = animaisDcl.IdAnimal;
                LogInsercaoSucesso(tutoresDcl, animaisDcl);
                PopulaTela(animaisDcl.IdAnimal);
                PopulaHistPesoAtual();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, 
                    inserirRet.mensagem,
                    "NutroVET informa - Inserção", 
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                string _msg = "";

                if (inserirRet.objeto != null)
                {
                    _msg = inserirRet.objeto[0].ToString();
                }

                tbIdadeAnos.Text = (animaisDcl.DtNascim != null ?
                    Funcoes.Funcoes.ConvertePara.String(
                        Funcoes.Funcoes.Datas.CalculaIdade(
                            animaisDcl.DtNascim.Value)) : "0");

                LogInsercaoErro(tutoresDcl, animaisDcl,
                    (_msg != "" ? _msg : inserirRet.mensagem));

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, 
                    inserirRet.mensagem,
                    "NutroVET informa - Inserção", 
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
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
            animaisDcl.RgPet = tbRgPet.Text;
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
                PopulaHistPesoAtual();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    alterarRet.mensagem,
                    "NutroVET informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                tbIdadeAnos.Text = (animaisDcl.DtNascim != null ?
                    Funcoes.Funcoes.ConvertePara.String(
                        Funcoes.Funcoes.Datas.CalculaIdade(
                            animaisDcl.DtNascim.Value)) : "0");

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, 
                    alterarRet.mensagem,
                    "NutroVET informa - Alteração", 
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void PopulaHistPesoAtual()
        {
            DateTime? _ini = (meDtInicial.Text != "" ? DateTime.Parse(meDtInicial.Text) :
                DateTime.Today.AddYears(-1));
            meDtInicial.Text = _ini.Value.ToString("dd/MM/yyyy");

            DateTime? _fim = (meDtFinal.Text != "" ? DateTime.Parse(meDtFinal.Text) :
                DateTime.Today);
            meDtFinal.Text = _fim.Value.ToString("dd/MM/yyyy");

            BindChart(_ini, _fim);
        }

        protected void btnAtendimentoLT_Click(object sender, EventArgs e)
        {
            ViewState.Remove("IdAtend");
            meDataAtendimentoLT.Text = DateTime.Today.ToString("dd/MM/yyyy");
            meHoraAtendimentoLT.Text = DateTime.Now.ToString("HH:mm");
            txbDescricaoLT.Text = "";
            ftbAtendimentoLT.Text = "";

            Funcoes.Funcoes.ControlForm.SetComboBox(ddlTipoAtendimentoLT, 0);

            mdAtendimentoLT.Show();
            PopulaHistPesoAtual();
        }

        protected void lbEditarDadosPaciente_Click(object sender, EventArgs e)
        {
            mdAlterandoDadosPaciente.Show();
            PopulaHistPesoAtual();
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

        protected void rptLinhaTempo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        TOLinhaTempoBll lt = (TOLinhaTempoBll)e.Item.DataItem;
                        bool _severLocal = Conexao.ServidorLocal();

                        Label lblDataLinhaTempo =
                            (Label)e.Item.FindControl("lblDataLinhaTempo");
                        HyperLink hlIconeLT = (HyperLink)e.Item.FindControl("hlIconeLT");
                        HyperLink hlVisualizar = (HyperLink)e.Item.FindControl("hlVisualizar");

                        lblDataLinhaTempo.Text = "Em " + (lt.Data != null ?
                            lt.Data.Value.ToString("dd/MM/yyyy") + " às " +
                            lt.Hora : "");

                        HtmlGenericControl iconeLinhaTempo = 
                            (HtmlGenericControl)e.Item.FindControl("iconeLinhadotempo");

                        switch (lt.IdClassif)
                        {
                            case 1://1 - Receituário
                                {
                                    string _path = (_severLocal ?
                                        "~/PrintFiles/Avaliacao/Receitas/" :
                                            "~/PrintFiles/Producao/Receitas/");
                                    bool _existeFile = receituarioBll.ExisteArquivoReceita(
                                        lt.Anexo);

                                    hlVisualizar.NavigateUrl = (_existeFile ? _path +
                                       lt.Anexo : "");
                                    hlVisualizar.CssClass = (_existeFile ?
                                        "btn btn-warning btn-xs" :
                                            "btn btn-secondary btn-xs");
                                    hlVisualizar.ToolTip = "Visualizar Receita";

                                    iconeLinhaTempo.Attributes["class"] = 
                                        "vertical-timeline-icon lazur-bg";
                                    hlIconeLT.CssClass = "fa-solid fa-book-medical";

                                    break;
                                }
                            case 2://2 - Atendimento
                                {
                                    //string _path = (_severLocal ?
                                    //    "~/PrintFiles/Avaliacao/Atendimentos/" :
                                    //        "~/PrintFiles/Producao/Atendimentos/");
                                    //bool _existeFile = atendBll.ExisteArquivoReceita(
                                    //    lt.Anexo);

                                    hlVisualizar.NavigateUrl = "";
                                    hlVisualizar.CssClass = "";
                                    hlVisualizar.ToolTip = "";
                                    hlVisualizar.Visible = false;

                                    iconeLinhaTempo.Attributes["class"] = 
                                        "vertical-timeline-icon verdenutro-bg";
                                    hlIconeLT.CssClass = "fa-solid fa-house-medical-flag";

                                    break;
                                }
                            case 3://3 - Cardápio
                                {
                                    string _path = (_severLocal ?
                                        "~/PrintFiles/Avaliacao/Cardapios/" :
                                            "~/PrintFiles/Producao/Cardapios/");
                                    bool _existeFile = cardapioBll.ExisteArquivoReceita(
                                        lt.Anexo);

                                    hlVisualizar.NavigateUrl = (_existeFile ? _path +
                                        lt.Anexo : "");
                                    hlVisualizar.CssClass = (_existeFile ?
                                        "btn btn-warning btn-xs" :
                                            "btn btn-secondary btn-xs");
                                    hlVisualizar.ToolTip = "Visualizar Cardápio";

                                    iconeLinhaTempo.Attributes["class"] = 
                                        "vertical-timeline-icon blue-escuro-bg";
                                    hlIconeLT.CssClass = "fa-solid fa-balance-scale";

                                    break;
                                }
                        }

                        break;
                    }
            }
        }

        protected void btnAtendSalvar_Click(object sender, EventArgs e)
        {
            int _idAtend = Funcoes.Funcoes.ConvertePara.Int(ViewState["IdAtend"]);

            if (_idAtend > 0)
            {
                AlterarAtendimento(_idAtend);
            }
            else
            {
                InserirAtendimento();
            }
        }

        private void InserirAtendimento()
        {
            atendDcl = new Atendimento();

            atendDcl.IdPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            atendDcl.IdAnimal = Funcoes.Funcoes.ConvertePara.Int(
                ViewState["_idPaciente"]);
            atendDcl.IdTpAtend = Funcoes.Funcoes.ConvertePara.Int(
                ddlTipoAtendimentoLT.SelectedValue);
            atendDcl.DtHrAtend = DateTime.Parse(meDataAtendimentoLT.Text + " " +
                meHoraAtendimentoLT.Text);
            atendDcl.Descricao = txbDescricaoLT.Text;
            atendDcl.Atendimento1 = (ftbAtendimentoLT.Text == "\r\n" ? string.Empty : 
                ftbAtendimentoLT.Text);

            atendDcl.Ativo = true;
            atendDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            atendDcl.DataCadastro = DateTime.Now;
            atendDcl.IP = Request.UserHostAddress;

            bllRetorno inserirRet = atendBll.Inserir(atendDcl);

            if (inserirRet.retorno)
            {
                PopulaLinhaTempoAnos(atendDcl.IdPessoa, atendDcl.IdAnimal);
                PopulaHistPesoAtual();
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    inserirRet.mensagem,
                    "NutroVET informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);

                mdAtendimentoLT.Show();
            }
        }

        private void AlterarAtendimento(int idAtend)
        {
            atendDcl = atendBll.Carregar(idAtend);

            atendDcl.IdTpAtend = Funcoes.Funcoes.ConvertePara.Int(
                ddlTipoAtendimentoLT.SelectedValue);
            atendDcl.Descricao = txbDescricaoLT.Text;
            atendDcl.Atendimento1 = (ftbAtendimentoLT.Text == "\r\n" ? string.Empty : 
                ftbAtendimentoLT.Text);

            atendDcl.Ativo = true;
            atendDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            atendDcl.DataCadastro = DateTime.Now;
            atendDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = atendBll.Alterar(atendDcl);

            if (alterarRet.retorno)
            {
                ViewState.Remove("IdAtend");
                PopulaLinhaTempoAnos(atendDcl.IdPessoa, atendDcl.IdAnimal);
                PopulaHistPesoAtual();
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    alterarRet.mensagem,
                    "NutroVET informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);

                mdAtendimentoLT.Show();
            }
        }

        protected void rptLinhaTempo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "1":
                    {
                        receituarioDcl = receituarioBll.Carregar(_id);

                        switch (receituarioDcl.dTpRec)
                        {
                            case 1:
                                {
                                    Response.Redirect("~/Receituario/RecSuplementUpdt.aspx?_idReceita=" +
                                        Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));

                                    break;
                                }
                            case 2:
                                {
                                    Response.Redirect("~/Receituario/RecNutraceuticosUpdt.aspx?_idReceita=" +
                                        Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));

                                    break;
                                }
                            case 3:
                                {
                                    Response.Redirect("~/Receituario/RecBrancoUpdt.aspx?_idReceita=" +
                                        Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));

                                    break;
                                }
                            case 4:
                                {
                                    Response.Redirect("~/Receituario/RecBalanceamUpdt.aspx?_idReceita=" +
                                        Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));

                                    break;
                                }
                        }

                        break;
                    }
                case "2":
                    {
                        atendDcl = atendBll.Carregar(_id);
                        ViewState["IdAtend"] = atendDcl.IdAtend;

                        if ((atendDcl != null) && (atendDcl.IdAtend > 0))
                        {
                            meDataAtendimentoLT.Text =
                                atendDcl.DtHrAtend.Value.ToString("dd/MM/yyyy");
                            meHoraAtendimentoLT.Text =
                                atendDcl.DtHrAtend.Value.ToString("HH:mm");
                            txbDescricaoLT.Text = atendDcl.Descricao;
                            ftbAtendimentoLT.Text = atendDcl.Atendimento1;

                            Funcoes.Funcoes.ControlForm.SetComboBox(ddlTipoAtendimentoLT,
                                atendDcl.IdTpAtend);

                            mdAtendimentoLT.Show();
                        }

                        break;
                    }
                case "3":
                    {
                        Response.Redirect("~/Cardapio/CardapioCadastro.aspx?_idCardapio=" +
                            Funcoes.Funcoes.Seguranca.Criptografar(_id.ToString()));

                        break;
                    }
            }
        }

        protected void lbEnviarImagem_Click(object sender, EventArgs e)
        {
            if (fuFileUpload.HasFile)
            {
                if (fuFileUpload.PostedFile.ContentLength < 512000)
                {
                    string _extensao = Path.GetExtension(fuFileUpload.FileName);
                    string _paciente = "Paciente_" + hfidPacienteCard.Value + _extensao;

                    try
                    {
                        fuFileUpload.SaveAs(Server.MapPath("~/Imagens/pacientes/") +
                             _paciente);
                        //lblFileUpload.Text = "Arquivo Gravado como: " + _paciente;

                        imgCardFotoPaciente.ImageUrl = CarregarImagem(
                            Funcoes.Funcoes.ConvertePara.Int(
                            hfidPacienteCard.Value));
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
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Tamanho do Arquivo Inválido!!!<br />Máx: 500 Kb",
                        "Informe NutroVET - Upload",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Arquivo não Selecionado!!!",
                    "Informe NutroVET - Upload",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        public string CarregarImagem(int idPaciente)
        {
            HttpServerUtility server = HttpContext.Current.Server;
            string path = server.MapPath("~/Imagens/pacientes/");
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            string fileName = directoryInfo.GetFiles("Paciente_" + idPaciente + ".*")
                .OrderByDescending(d => d.LastAccessTime)
                .Select(d => d.Name)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "petpadrao.png";
            }

            return Path.Combine("~/Imagens/pacientes/", fileName) + "?time=" + DateTime.Now.ToString();
        }

        protected void rptLTAnos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        int _ano = (int)e.Item.DataItem;
                        List<TOLinhaTempoBll> _listLT =
                            (List<TOLinhaTempoBll>)ViewState["_listLT"];
                        List<TOLinhaTempoBll> _listaPorAno =
                            atendBll.ListarLinhaTempoPorAno(_listLT, _ano);
                        Label lblAno = (Label)e.Item.FindControl("lblAno");
                        Repeater rptLinhaTempo = (Repeater)e.Item.FindControl(
                            "rptLinhaTempo");

                        lblAno.Text = Funcoes.Funcoes.ConvertePara.String(_ano);

                        rptLinhaTempo.DataSource = _listaPorAno;
                        rptLinhaTempo.DataBind();

                        break;
                    }
            }
        }

        protected void btnAtendFechar_Click(object sender, EventArgs e)
        {
            PopulaHistPesoAtual();
        }

        protected void btnAlterandoDadosPacienteFechar_Click(object sender, EventArgs e)
        {
            PopulaHistPesoAtual();
        }
    }
}
