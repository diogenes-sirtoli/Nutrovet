using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nutrovet
{
    public partial class Logon : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblAno.Text = DateTime.Today.ToString("yyyy");
            }
          
        }

        protected void lbSairSistema_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            Funcoes.Funcoes.FecharJanela(this);
            Response.Redirect("~/Login.aspx");
        }

    }
}