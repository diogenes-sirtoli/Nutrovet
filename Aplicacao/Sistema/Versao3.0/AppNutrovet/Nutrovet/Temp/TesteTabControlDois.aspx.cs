using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DCL;

namespace Nutrovet.Temp
{
    public partial class TesteTabControlDois : System.Web.UI.Page
    {
        protected clAnimaisBll animaisBll = new clAnimaisBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            //long teste = animaisBll.CrescimentoAnimal(0, "11/11/1970");

            //Label1.Text = Funcoes.Funcoes.ConvertePara.String(teste);
        }
    }
}