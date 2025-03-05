using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using Microsoft.Reporting.WebForms;

namespace Nutrovet.Relatorios
{
    public partial class RptCardapio : System.Web.UI.Page
    {
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;
        protected clCardapioAlimentosBll cardapioAlimentosBll = 
            new clCardapioAlimentosBll();
        protected TOCardapioResumoBll resumoTO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                    Request.QueryString["_idCard"]);

                if (_idCardapio > 0)
                {
                    PopulaParametros(_idCardapio);
                }
            }
        }

        private void PopulaParametros(int idCardapio)
        {
            cardapioTO = cardapioBll.CarregarTO(idCardapio);
            resumoTO = cardapioBll.CardapioResumo(idCardapio);

            ReportParameter prmTutor = new ReportParameter(
                "prmTutor", cardapioTO.Tutor);
            ReportParameter prmPaciente = new ReportParameter(
                "prmPaciente", cardapioTO.Animal);
            ReportParameter prmEspecie = new ReportParameter(
                "prmEspecie", cardapioTO.Especie);
            ReportParameter prmRaca = new ReportParameter(
                "prmRaca", cardapioTO.Raca);
            ReportParameter prmIdade = new ReportParameter("prmIdade", 
                Funcoes.Funcoes.ConvertePara.String(cardapioTO.Idade));
            ReportParameter prmPesoAtual = new ReportParameter("prmPesoAtual",
                Funcoes.Funcoes.ConvertePara.String(cardapioTO.PesoAtual));
            ReportParameter prmPesoIdeal = new ReportParameter("prmPesoIdeal",
                Funcoes.Funcoes.ConvertePara.String(cardapioTO.PesoIdeal));
            ReportParameter prmDieta = new ReportParameter("prmDieta",
                cardapioTO.Dieta);
            ReportParameter prmFator = new ReportParameter("prmFator",
                Funcoes.Funcoes.ConvertePara.String(cardapioTO.FatorEnergia));
            ReportParameter prmNEM = new ReportParameter("prmNEM",
                Funcoes.Funcoes.ConvertePara.String(cardapioTO.NEM));

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
                "prmEnReq", Funcoes.Funcoes.ConvertePara.String(cardapioTO.NEM));
            ReportParameter prmEnPres = new ReportParameter(
                "prmEnPres", resumoTO.EnergiaKcal);

            List <ReportParameter> param = new List<ReportParameter>();
            param.Add(prmTutor);
            param.Add(prmPaciente);
            param.Add(prmEspecie);
            param.Add(prmRaca);
            param.Add(prmIdade);
            param.Add(prmPesoAtual);
            param.Add(prmPesoIdeal);
            param.Add(prmDieta);
            param.Add(prmFator);
            param.Add(prmNEM);

            param.Add(prmCarboG);
            param.Add(prmProtG);
            param.Add(prmGordG);
            param.Add(prmCarboP);
            param.Add(prmProtP);
            param.Add(prmGordP);
            param.Add(prmFibra);
            param.Add(prmUmidade);
            param.Add(prmEnReq);
            param.Add(prmEnPres);

            rvCardapio.LocalReport.SetParameters(param);
            rvCardapio.LocalReport.Refresh();
        }
    }
}