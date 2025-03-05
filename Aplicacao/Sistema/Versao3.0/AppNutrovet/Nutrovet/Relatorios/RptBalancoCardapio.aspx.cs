using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;

namespace Nutrovet.Relatorios
{
    public partial class RptBalancoCardapio : System.Web.UI.Page
    {
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;
        protected clCardapioAlimentosBll cardapioAlimentosBll =
            new clCardapioAlimentosBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string _tabela = Funcoes.Funcoes.ConvertePara.String(
                    Request.QueryString["_tabela"]);
                int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                    Request.QueryString["_idCard"]);
                string _quantDietas = Funcoes.Funcoes.ConvertePara.String(
                    Request.QueryString["_quantDietas"]);

                if (_idCardapio > 0)
                {
                    PopulaParametros(_idCardapio, _tabela, _quantDietas);
                }
            }
        }

        private void PopulaParametros(int idCardapio, string _tabela, string _quantDietas)
        {
            cardapioTO = cardapioBll.CarregarTO(idCardapio);

            ReportParameter prmBalDieta;

            if (Funcoes.Funcoes.ConvertePara.Int(_quantDietas) > 1)
            {
                prmBalDieta = new ReportParameter("prmTabelaNutr",
                    "Balanceamento da Dieta - " + _tabela); 
            }
            else
            {
                prmBalDieta = new ReportParameter("prmTabelaNutr",
                    "Exigências Nutricionais - " + _tabela);
            }

            ReportParameter prmQuantDietas = new ReportParameter("prmQuantDietas", _quantDietas);
            ReportParameter prmTutor = new ReportParameter("prmTutor", cardapioTO.Tutor);
            ReportParameter prmPaciente = new ReportParameter("prmPaciente", cardapioTO.Animal);
            ReportParameter prmEspecie = new ReportParameter("prmEspecie", cardapioTO.Especie);
            ReportParameter prmRaca = new ReportParameter("prmRaca", cardapioTO.Raca);
            ReportParameter prmIdade = new ReportParameter("prmIdade",
                Funcoes.Funcoes.ConvertePara.String(cardapioTO.Idade));
            ReportParameter prmPesoAtual = new ReportParameter("prmPesoAtual",
                Funcoes.Funcoes.ConvertePara.String(cardapioTO.PesoAtual));
            ReportParameter prmPesoIdeal = new ReportParameter("prmPesoIdeal",
                Funcoes.Funcoes.ConvertePara.String(cardapioTO.PesoIdeal));
            ReportParameter prmFator = new ReportParameter("prmFator",
                Funcoes.Funcoes.ConvertePara.String(cardapioTO.FatorEnergia));
            ReportParameter prmNEM = new ReportParameter("prmNEM",
                Funcoes.Funcoes.ConvertePara.String(cardapioTO.NEM));
            
            List<ReportParameter> param = new List<ReportParameter>();

            param.Add(prmBalDieta);
            param.Add(prmQuantDietas);
            param.Add(prmTutor);
            param.Add(prmPaciente);
            param.Add(prmEspecie);
            param.Add(prmRaca);
            param.Add(prmIdade);
            param.Add(prmPesoAtual);
            param.Add(prmPesoIdeal);
            param.Add(prmFator);
            param.Add(prmNEM);

            rvBalancodieta.LocalReport.SetParameters(param);
            rvBalancodieta.LocalReport.Refresh();
        }
    }
}