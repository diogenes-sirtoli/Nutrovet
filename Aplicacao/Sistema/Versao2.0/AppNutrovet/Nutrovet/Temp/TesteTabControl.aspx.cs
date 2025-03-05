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

namespace Nutrovet.Temp
{
    public partial class TesteTabControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                TabName.Value = Request.Form[TabName.UniqueID];
            }
        }

        protected void btnSimGestante_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnNaoGestante_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSimLactante_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnNaoLactante_Click(object sender, EventArgs e)
        {
            
        }

        protected void ddlTutor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnPesqAlimentos_Click(object sender, EventArgs e)
        {
            
        }

        protected void lbFechaListaAlimmentos_Click(object sender, EventArgs e)
        {
            
        }
    }
}