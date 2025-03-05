using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using System.IO;

namespace Nutrovet.Cardapio.Impressao
{
    public partial class RptBalancoCardapio : System.Web.UI.Page
    {
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;
        protected clCardapioAlimentosBll cardapioAlimentosBll =
            new clCardapioAlimentosBll();
        protected TOCardapioResumoBll resumoTO;
        protected clReceituarioBll receituarioBll = new clReceituarioBll();
        protected DCL.Receituario receituarioDcl;
        protected TOReceituarioBll receituarioTO;
        protected clConfigReceituarioBll configReceitBll = new clConfigReceituarioBll();
        protected ConfigReceituario configReceitDcl;
        protected clPessoasBll assinanteBll = new clPessoasBll();
        protected Pessoa assinanteDCL;

        protected string fileName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string _tabela = Funcoes.Funcoes.ConvertePara.String(
                    Request.QueryString["_tabela"]);
                int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                    Request.QueryString["_idCard"]);
                int _idRec = Funcoes.Funcoes.ConvertePara.Int(
                    Request.QueryString["_idRec"]);
                int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                string _quantDietas = Funcoes.Funcoes.ConvertePara.String(
                    Request.QueryString["_quantDietas"]);
                PoPulaTpImpressao(Funcoes.Funcoes.ConvertePara.String(
                    Request.QueryString["_tpImpr"]));

                if ((_idRec > 0) && (_idCardapio > 0))
                {
                    PopulaParametros(_idCardapio, _idRec, _idPessoa, _tabela, 
                        _quantDietas);
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Uma Receita e um Cardápio Devem ser Selecionados!!!",
                        "NutroVET Informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
                }
            }
        }

        private void PoPulaTpImpressao(string _tpImpr)
        {
            string _ret = "";

            switch (_tpImpr)
            {
                default:
                case "pdf":
                    {
                        _ret = "PDF";

                        break;
                    }
                case "word":
                    {
                        _ret = "Word";

                        break;
                    }
                case "excel":
                    {
                        _ret = "Excel";

                        break;
                    }
            }

            ViewState["_tpImpr"] = _ret;
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gerarPDF(Funcoes.Funcoes.ConvertePara.String(ViewState["_tpImpr"]));
            }
        }

        private void PopulaParametros(int idCardapio, int idReceita, int idPessoa, 
            string _tabela, string _quantDietas)
        {
            configReceitDcl = configReceitBll.Carregar(idPessoa);
            assinanteDCL = assinanteBll.Carregar(idPessoa);
            receituarioDcl = receituarioBll.Carregar(idReceita);
            cardapioTO = cardapioBll.CarregarTO(receituarioDcl.IdCardapio);

            ReportParameter prmTabelaNutr;

            if (Funcoes.Funcoes.ConvertePara.Int(_quantDietas) > 1)
            {
                prmTabelaNutr = new ReportParameter("prmTabelaNutr", _tabela); 
            }
            else
            {
                prmTabelaNutr = new ReportParameter("prmTabelaNutr",
                    "Exigências Nutricionais - " + _tabela);
            }

            fileName = ColocaExtensao(receituarioDcl.Arquivo);
            string dataLonga = (configReceitDcl.Logr_Cidade != "" ?
                configReceitDcl.Logr_Cidade + ", " : "") + DateTime.Today.ToString("dd") +
                    " de " + DateTime.Today.ToString("MMMM") + " de " +
                        DateTime.Today.ToString("yyyy");
            string _pathLogo = configReceitBll.CarregarImgLogoImpr(idPessoa);
            string _pathAssin = configReceitBll.CarregarImgAssinaturaImpr(idPessoa);

            //Assinante e Clínica
            ReportParameter prmAssinante = new ReportParameter("prmAssinante",
                assinanteDCL.Nome);
            ReportParameter prmFRodape = new ReportParameter("prmFRodape",
                (Funcoes.Funcoes.ConvertePara.Bool(configReceitDcl.fLivreRodape) ? "1" :
                "0"));
            ReportParameter prmRodape = new ReportParameter("prmRodape",
                configReceitDcl.LivreRodape);
            ReportParameter prmFCabecalho = new ReportParameter("prmFCabecalho",
                (Funcoes.Funcoes.ConvertePara.Bool(configReceitDcl.fLivreCabecalho) ? "1" :
                "0"));
            ReportParameter prmCabecalho = new ReportParameter("prmCabecalho",
                configReceitDcl.LivreCabecalho);
            ReportParameter prmCRM = new ReportParameter("prmCRM",
                (configReceitDcl.CrmvUf != null && configReceitDcl.CrmvUf != "" &&
                    configReceitDcl.CrmvUf != "0" ? configReceitDcl.CrmvUf + " " : "") +
                        configReceitDcl.CRMV);
            ReportParameter prmNomeClinica = new ReportParameter("prmNomeClinica",
                configReceitDcl.NomeClinica);
            ReportParameter prmSlogan = new ReportParameter("prmSlogan",
                configReceitDcl.Slogan);
            ReportParameter prmEndereco = new ReportParameter("prmEndereco",
                configReceitBll.MontaCamposEndereco(configReceitDcl));
            ReportParameter prmEMail = new ReportParameter("prmEMail",
                configReceitDcl.Email);
            ReportParameter prmImgLogo = new ReportParameter("prmImgLogo", _pathLogo);
            ReportParameter prmImgAssin = new ReportParameter("prmImgAssin", _pathAssin);
            ReportParameter prmTelefone = new ReportParameter("prmTelefone",
                (configReceitDcl.Telefone != "" ? configReceitDcl.Telefone +
                    (configReceitDcl.Celular != "" ? " / " + configReceitDcl.Celular : "") :
                        (configReceitDcl.Celular != "" ? configReceitDcl.Celular : "")));
            ReportParameter prmData = new ReportParameter("prmData", dataLonga);

            //Paciente/PET
            ReportParameter prmPaciente = new ReportParameter("prmPaciente",
                cardapioTO.Animal);
            ReportParameter prmEspecie = new ReportParameter("prmEspecie",
                cardapioTO.Especie);
            ReportParameter prmRaca = new ReportParameter("prmRaca", cardapioTO.Raca);
            ReportParameter prmPesoAtual = new ReportParameter("prmPesoAtual", 
                string.Format("{0:##0.00#}", cardapioTO.PesoAtual) + " (Kg)");
            ReportParameter prmSexo = new ReportParameter("prmSexo", cardapioTO.Sexo);
            ReportParameter prmIdade = new ReportParameter("prmIdade",
                cardapioTO.Idade.ToString() + " ano(s)");

            //Tutor
            ReportParameter prmTutor = new ReportParameter("prmTutor",
                cardapioTO.Tutor);
            ReportParameter prmEMailTutor = new ReportParameter("prmEMailTutor",
                cardapioTO.TutorEMail);
            ReportParameter prmFoneTutor = new ReportParameter("prmFoneTutor",
                cardapioTO.TutorFone);

            //Receita
            ReportParameter prmDieta = new ReportParameter("prmDieta",
                cardapioTO.Dieta);
            ReportParameter prmFator = new ReportParameter("prmFator",
                string.Format("{0:##,##0}", cardapioTO.FatorEnergia));
            ReportParameter prmNEM = new ReportParameter("prmNEM",
                string.Format("{0:##,##0} Kcal", cardapioTO.NEM));
            ReportParameter prmObservacoes = new ReportParameter("prmObservacoes",
                receituarioDcl.Observacao);
            ReportParameter prmQuantDietas = new ReportParameter("prmQuantDietas", 
                _quantDietas);

            List<ReportParameter> param = new List<ReportParameter>
            {
                prmTabelaNutr,
                prmAssinante,
                prmCRM,
                prmNomeClinica,
                prmSlogan,
                prmEndereco,
                prmEMail,
                prmImgLogo,
                prmImgAssin,
                prmTelefone,
                prmData,
                prmFCabecalho,
                prmCabecalho,
                prmFRodape,
                prmRodape,

                prmPaciente,
                prmEspecie,
                prmRaca,
                prmIdade,
                prmPesoAtual,
                prmSexo,

                prmTutor,
                prmEMailTutor,
                prmFoneTutor,

                prmDieta,
                prmFator,
                prmNEM,
                prmObservacoes,
                prmQuantDietas
            };

            rvBalancodieta.LocalReport.EnableExternalImages = true;
            rvBalancodieta.LocalReport.EnableHyperlinks = true;
            rvBalancodieta.LocalReport.SetParameters(param);
            rvBalancodieta.LocalReport.Refresh();
        }

        private string ColocaExtensao(string arquivo)
        {
            string _ret = "";
            string _extensao = Funcoes.Funcoes.ConvertePara.String(ViewState["_tpImpr"]);

            switch (_extensao)
            {
                case "PDF":
                    {
                        _ret = arquivo;

                        break;
                    }
                case "Word":
                    {
                        _ret = arquivo.Replace(".pdf", "") + ".doc";

                        break;
                    }
                case "Excel":
                    {
                        _ret = arquivo.Replace(".pdf", "") + ".xls";

                        break;
                    }
            }

            return _ret;
        }

        private void gerarPDF(string _tpImpr)
        {
            byte[] pdfContent = rvBalancodieta.LocalReport.Render(_tpImpr);
            bool _severLocal = Conexao.ServidorLocal();
            string _path = (_severLocal ? "~/PrintFiles/Avaliacao/Receitas/" :
                "~/PrintFiles/Producao/Receitas/");
            string FileName = _path + fileName;

            SalvaPDFDiretorio(pdfContent);

            Response.Redirect(FileName);
        }

        private void SalvaPDFDiretorio(byte[] arquivo)
        {
            string directoryName = GeraDiretorio() + fileName;

            using (FileStream stream = new FileStream(directoryName, FileMode.Create))
            {
                stream.Write(arquivo, 0, arquivo.Length);
            }
        }

        private string GeraDiretorio()
        {
            bool _severLocal = Conexao.ServidorLocal();
            string _path = (_severLocal ? "~/PrintFiles/Avaliacao/Receitas/" :
                "~/PrintFiles/Producao/Receitas/");
            string _partialPath = Server.MapPath(_path);

            DirectoryInfo dir = new DirectoryInfo(_partialPath);

            if (!dir.Exists)
            {
                dir.Create();
            }

            return _partialPath;
        }
    }
}