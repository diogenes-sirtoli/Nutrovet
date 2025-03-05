using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nutrovet.BtnPagSeguro
{
    public partial class FormBtnPagSeguro : System.Web.UI.Page
    {
        public string _codigoPagSeguro = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                string _PLANO = Funcoes.Funcoes.ConvertePara.String(
                    Request.QueryString["_plano"]);
                _codigoPagSeguro = ComposicaoPlano(_PLANO);
                //code.Attributes["value"] = ComposicaoPlano(_PLANO);

                //hosted_button_id.Value = ComposicaoPlano(_PLANO);
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
                        _value = "9035EAFD8F8F51C554036FACB55151A3";
                        //_value = "33EC87E0D0D0267AA4E7FFA89A6219C4";
                        break;
                    }
                case "BMdp05":
                    {
                        _value = "D9017D010202095DD425BFBBBE30CA9A";

                        break;
                    }
                case "BMdp10":
                    {
                        _value = "FC7C23F49797F2CCC427BFBFE88A93B3";

                        break;
                    }
                case "BMdm1":
                    {
                        _value = "";

                        break;
                    }
                case "BA":
                    {
                        _value = "7723F6E50E0EC1E224EBDFABF1E37DDE";
                        //_value = "95BE95BBE5E502D224759F98CCAEC188";
                        break;
                    }
                case "BAdp05":
                    {
                        _value = "D1B1E473A1A1D18FF498EF97381B53D5";

                        break;
                    }
                case "BAdp10":
                    {
                        _value = "D2977F36DFDF364DD4FBEFA196710979";

                        break;
                    }
                case "BAda12":
                    {
                        _value = "";

                        break;
                    }
                #endregion
                #region Plano Intermediário
                case "IM":
                    {
                        _value = "1D88A98DC3C3BBB994741F8F216CAB88";
                        //_value = "C4D6A6590D0D2FAAA4B00FA4800C34E2";
                        break;
                    }
                case "IMdp05":
                    {
                        _value = "E1E4EC1733338BDAA4AF9F9597A9481F";

                        break;
                    }
                case "IMdp10":
                    {
                        _value = "56F8DA331A1AB7C004709F99AF6C4F0C";

                        break;
                    }
                case "IMdm1":
                    {
                        _value = "";

                        break;
                    }
                case "IA":
                    {
                        _value = "F129B5EA75759699940C9FAF821CE5FA";
                        //_value = "E2876A01F9F94C0EE49FDFA4C30BF37B";
                        break;
                    }
                case "IAdp05":
                    {
                        _value = "F2EC14E5ABAB32A0043FDF8A043FD837";

                        break;
                    }
                case "IAdp10":
                    {
                        _value = "2162C374ECECD39EE4598FAE9B62FC8F";

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
                        _value = "B7A3FDD8C3C32E9CC4518FA4D983E201";
                        //_value = "FB53F6CFCECEB5833482EFAD176E75B2";
                        break;
                    }
                case "CMdp05":
                    {
                        _value = "C69EACEA3B3B1C2994520FACF2DD5012";

                        break;
                    }
                case "CMdp10":
                    {
                        _value = "D2B49884818189F9947A8F84320FB0A0";

                        break;
                    }
                case "CMdm1":
                    {
                        _value = "";

                        break;
                    }
                case "CA":
                    {
                        _value = "3BFBA3D5FDFD453BB4718FBBE8C88817";
                        //_value = "F73D6CF92C2CC46FF4658F8132E40EE0";
                        break;
                    }
                case "CAdp05":
                    {
                        _value = "2DC6D0B98787BE3FF4CE3F9DF70523D9";

                        break;
                    }
                case "CAdp10":
                    {
                        _value = "EB879379A6A6BD3884F99FA5487E7F10";

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