using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// SDK de Mercado Pago
using MercadoPago;


namespace Nutrovet.BtnMercadoPago
{
    public partial class FormBtnPayPal : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string _PLANO = Funcoes.Funcoes.ConvertePara.String(
                    Request.QueryString["_plano"]);

                hosted_button_id.NavigateUrl = ComposicaoPlano(_PLANO);
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
                        _value = "javascript:onclick=window.open('https://www.mercadopago.com/mlb/debits/new?preapproval_plan_id=43c0e23f14474402b6cb4b06c7f37c29', '_parent')";
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
                        _value = "javascript:onclick=window.open('https://www.mercadopago.com.br/checkout/v1/redirect?pref_id=489388454-6014dfe2-9368-4d0d-9f76-4a2d15411111', '_blank')";

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
                        _value = "javascript:onclick=window.open('https://www.mercadopago.com.br/checkout/v1/redirect?pref_id=489388454-086ee3d4-0263-4baa-ab7d-c1cd2e2b8c83', '_blank')";

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
                        _value = "javascript:onclick=window.open('https://www.mercadopago.com.br/checkout/v1/redirect?pref_id=489388454-555108a4-db79-4ff8-9940-8df28e856ee7', '_blank')";

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
                        _value = "javascript:onclick=window.open('https://www.mercadopago.com.br/checkout/v1/redirect?pref_id=489388454-834094ab-55e4-414f-bc48-82d32f5c955d', '_blank')";

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
                        _value = "javascript:onclick=window.open('https://www.mercadopago.com.br/checkout/v1/redirect?pref_id=489388454-71f29639-72d9-4886-b636-160bec34185e', '_blank')";

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