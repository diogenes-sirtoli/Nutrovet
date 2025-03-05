using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DCL;
using BLL;


namespace Nutrovet
{
    public partial class EscolherPlanoRetorno : System.Web.UI.Page
    {
        protected clPessoasBll userBll = new clPessoasBll();
        protected TOPessoasBll userTO = new TOPessoasBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Response.AppendHeader("Access-Control-Allow-Origin",
                //    "https://sandbox.pagseguro.uol.com.br");
                //string code = Funcoes.Funcoes.ConvertePara.String(
                //    Request.QueryString["code"]);
                //ViewState["_pessoa"] = (TOPessoasBll)Session["pessoa"];

                //if (code != "")
                //{
                //    SearchCode(code);
                //}
                //else
                //{
                //    alertas.Attributes["class"] =
                //            "alert alert-danger alert-dismissible";
                //    lblAlerta.Text = @"Não foi possível receber o Código de Retorno !";
                //    alertas.Visible = true;
                //}
            }
        }


    }
}