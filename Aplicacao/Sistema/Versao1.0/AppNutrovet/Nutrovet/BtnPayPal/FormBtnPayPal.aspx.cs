using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nutrovet.BtnPayPal
{
    public partial class FormBtnPayPal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string _PLANO = Funcoes.Funcoes.ConvertePara.String(
                    Request.QueryString["_plano"]);

                hosted_button_id.Value = ComposicaoPlano(_PLANO);
            }
        }

        private string ComposicaoPlano(string _plano)
        {
            string _value = "";

            switch (_plano)
            {
                #region Plano Básico
                case "BM":
                    {
                        _value = "5N62JDLPKP2RY";

                        break;
                    }
                case "BMdp05":
                    {
                        _value = "5SZEFVHXV64UN";

                        break;
                    }
                case "BMdp10":
                    {
                        _value = "K5R6RPRW5DXG6";

                        break;
                    }
                case "BMdm1":
                    {
                        _value = "HU33RNWM5K4CE";

                        break;
                    }
                case "BA":
                    {
                        _value = "XHSVYSVQF2TFN";

                        break;
                    }
                case "BAdp05":
                    {
                        _value = "WUWD3DZKX9788";

                        break;
                    }
                case "BAdp10":
                    {
                        _value = "L32WJLMQY5RCA";

                        break;
                    }
                case "BAda12":
                    {
                        _value = "8CME796HB7PNW";

                        break;
                    }
                #endregion
                #region Plano Intermediário
                case "IM":
                    {
                        _value = "W3DGZWKCSNW2A";

                        break;
                    }
                case "IMdp05":
                    {
                        _value = "XZT7PV3PX38J2";

                        break;
                    }
                case "IMdp10":
                    {
                        _value = "UKR3QMPV58BZS";

                        break;
                    }
                case "IMdm1":
                    {
                        _value = "";

                        break;
                    }
                case "IA":
                    {
                        _value = "CG8SRLJDR3686";

                        break;
                    }
                case "IAdp05":
                    {
                        _value = "4ZA9BJQJEUJT6";

                        break;
                    }
                case "IAdp10":
                    {
                        _value = "6NSJB85S5CYSW";

                        break;
                    }
                case "IAda12":
                    {
                        _value = "";

                        break;
                    }
                #endregion
                #region Plano Completo
                case "CM":
                    {
                        _value = "VRWSTEJ2RJU56";

                        break;
                    }
                case "CMdp05":
                    {
                        _value = "49EWXJ2KCJ7BE";

                        break;
                    }
                case "CMdp10":
                    {
                        _value = "RQRUGRPYEM4R2";

                        break;
                    }
                case "CMdm1":
                    {
                        _value = "";

                        break;
                    }
                case "CA":
                    {
                        _value = "XGY8MQ4VDB6CG";

                        break;
                    }
                case "CAdp05":
                    {
                        _value = "EL6CPMVAZ6XXU";

                        break;
                    }
                case "CAdp10":
                    {
                        _value = "DHTAHLT6GNBNS";

                        break;
                    }
                case "CAda12":
                    {
                        _value = "";

                        break;
                    }
                    #endregion
            }

            return _value;
        }
    }
}