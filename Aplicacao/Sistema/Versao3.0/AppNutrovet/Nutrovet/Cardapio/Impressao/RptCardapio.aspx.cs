using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using Microsoft.Reporting.WebForms;
using Nutrovet.Receituario;
using System.IO;

namespace Nutrovet.Cardapio.Impressao
{
    public partial class RptCardapio : System.Web.UI.Page
    {
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;
        protected clCardapioAlimentosBll cardapioAlimentosBll = 
            new clCardapioAlimentosBll();
        protected TOCardapioResumoBll resumoTO;
        protected clConfigReceituarioBll configReceitBll = new clConfigReceituarioBll();
        protected ConfigReceituario configReceitDcl;
        protected clPessoasBll assinanteBll = new clPessoasBll();
        protected Pessoa assinanteDCL;

        protected string fileName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                    Request.QueryString["_idCard"]);
                int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                PoPulaTpImpressao(Funcoes.Funcoes.ConvertePara.String(
                    Request.QueryString["_tpImpr"]));

                if (_idCardapio > 0)
                {
                    PopulaParametros(_idPessoa, _idCardapio);
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

        private void PopulaParametros(int idPessoa, int idCardapio)
        {
            configReceitDcl = configReceitBll.Carregar(idPessoa);
            assinanteDCL = assinanteBll.Carregar(idPessoa);
            cardapioTO = cardapioBll.CarregarTO(idCardapio);
            resumoTO = cardapioBll.CardapioResumo(idCardapio, idPessoa);

            fileName = ColocaExtensao(cardapioTO.Arquivo);
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
            ReportParameter prmPeso = new ReportParameter("prmPesoAtual", string.Format(
                "{0:##0.00#}", cardapioTO.PesoAtual) + " (Kg)");
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
                cardapioTO.Observacao);
            ReportParameter prmCarboG = new ReportParameter(
                "prmCarboG", resumoTO.CarboG);
            ReportParameter prmProtG = new ReportParameter(
                "prmProtG", resumoTO.ProtG);
            ReportParameter prmGordG = new ReportParameter(
                "prmGordG", resumoTO.GordG);
            ReportParameter prmCarboP = new ReportParameter(
                "prmCarboP", resumoTO.CarboP);
            ReportParameter prmProtP = new ReportParameter(
                "prmProtP", resumoTO.ProtP);
            ReportParameter prmGordP = new ReportParameter(
                "prmGordP", resumoTO.GordP);
            ReportParameter prmFibra = new ReportParameter(
                "prmFibra", resumoTO.FibrasG);
            ReportParameter prmUmidade = new ReportParameter(
                "prmUmidade", resumoTO.UmidageG);
            ReportParameter prmEnReq = new ReportParameter(
                "prmEnReq", string.Format("{0:##,##0} Kcal", cardapioTO.NEM));
            ReportParameter prmEnPres = new ReportParameter(
                "prmEnPres", resumoTO.EnergiaKcal);

            List<ReportParameter> param = new List<ReportParameter>
            {
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
                prmPeso,
                prmSexo,

                prmTutor,
                prmEMailTutor,
                prmFoneTutor,

                prmDieta,
                prmFator,
                prmNEM,
                prmObservacoes,
                prmCarboG,
                prmProtG,
                prmGordG,
                prmCarboP,
                prmProtP,
                prmGordP,
                prmFibra,
                prmUmidade,
                prmEnReq,
                prmEnPres
            };

            rvCardapio.LocalReport.EnableExternalImages = true;
            rvCardapio.LocalReport.EnableHyperlinks = true;
            rvCardapio.LocalReport.SetParameters(param);
            rvCardapio.LocalReport.Refresh();
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
            byte[] pdfContent = rvCardapio.LocalReport.Render(_tpImpr);
            bool _severLocal = Conexao.ServidorLocal();
            string _path = (_severLocal ? "~/PrintFiles/Avaliacao/Cardapios/" :
                "~/PrintFiles/Producao/Cardapios/");
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
            string _path = (_severLocal ? "~/PrintFiles/Avaliacao/Cardapios/" :
                "~/PrintFiles/Producao/Cardapios/");
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