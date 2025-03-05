using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DCL;

namespace Nutrovet.BtnPayPal
{
    public partial class GerarVoucher : System.Web.UI.Page
    {
        protected clAcessosVigenciaCupomBll cupomBll = new clAcessosVigenciaCupomBll();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            cupomBll.GerarVouchers(3000, 3601, 
                (int)DominiosBll.CupomDescontoTipos.Percentual, 5, 180);
        }
    }
}