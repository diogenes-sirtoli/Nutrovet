using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DCL;
using Microsoft.Reporting.WebForms;

namespace Nutrovet.Receituario.Impressao
{
    public partial class RptBranco : System.Web.UI.Page
    {
        protected clConfigReceituarioBll configReceitBll = new clConfigReceituarioBll();
        protected ConfigReceituario configReceitDcl;
        protected clPessoasBll assinanteBll = new clPessoasBll();
        protected Pessoa assinanteDCL;
        protected clReceituarioBll receituarioBll = new clReceituarioBll();
        protected DCL.Receituario receituarioDcl;
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;

        protected string fileName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int _idReceituario = Funcoes.Funcoes.ConvertePara.Int(
                    Request.QueryString["_idRec"]);
                int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                    Request.QueryString["_idCardapio"]);
                int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                PoPulaTpImpressao(Funcoes.Funcoes.ConvertePara.String(
                    Request.QueryString["_tpImpr"]));

                if (_idReceituario > 0)
                {
                    PopulaParametros(_idReceituario, _idPessoa, _idCardapio);
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

        private void PopulaParametros(int _idRec, int idPessoa, int idCardapio)
        {
            configReceitDcl = configReceitBll.Carregar(idPessoa);
            assinanteDCL = assinanteBll.Carregar(idPessoa);
            cardapioTO = cardapioBll.CarregarTO(idCardapio);
            receituarioDcl = receituarioBll.Carregar(_idRec);

            fileName = ColocaExtensao(receituarioDcl.Arquivo);
            string dataLonga = (configReceitDcl.Logr_Cidade != "" ? 
                configReceitDcl.Logr_Cidade + ", ": "") + DateTime.Today.ToString("dd") + 
                    " de " + DateTime.Today.ToString("MMMM") + " de " + 
                        DateTime.Today.ToString("yyyy");
            string _pathLogo = configReceitBll.CarregarImgLogoImpr(idPessoa);
            string _pathAssin = configReceitBll.CarregarImgAssinaturaImpr(idPessoa);

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

            ReportParameter prmTutor = new ReportParameter("prmTutor",
                cardapioTO.Tutor);
            ReportParameter prmEMailTutor = new ReportParameter("prmEMailTutor",
                cardapioTO.TutorEMail);
            ReportParameter prmFoneTutor = new ReportParameter("prmFoneTutor", 
                cardapioTO.TutorFone);

            ReportParameter prmPrescricao = new ReportParameter("prmPrescricao",
                receituarioDcl.Prescricao);

            List<ReportParameter> param = new List<ReportParameter>
            {
                prmAssinante,
                prmCRM,
                prmNomeClinica,
                prmSlogan,
                prmEndereco,
                prmEMail,
                prmTelefone,
                prmFCabecalho,
                prmCabecalho,
                prmFRodape,
                prmRodape,

                prmPaciente,
                prmEspecie,
                prmRaca,
                prmPeso,
                prmSexo,
                prmIdade,
                prmTutor,
                prmEMailTutor,
                prmFoneTutor,
                prmPrescricao,
                prmData,
                prmImgLogo,
                prmImgAssin
            };

            rvRecBranco.LocalReport.EnableExternalImages = true;
            rvRecBranco.LocalReport.EnableHyperlinks = true;
            rvRecBranco.LocalReport.SetParameters(param);
            rvRecBranco.LocalReport.Refresh();
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
            byte[] pdfContent = rvRecBranco.LocalReport.Render(_tpImpr);
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