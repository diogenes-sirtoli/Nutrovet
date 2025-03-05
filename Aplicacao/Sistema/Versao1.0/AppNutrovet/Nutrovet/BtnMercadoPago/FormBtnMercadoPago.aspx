<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormBtnMercadoPago.aspx.cs" Inherits="Nutrovet.BtnMercadoPago.FormBtnPayPal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center">
            <asp:HyperLink mp-mode="dftl" ID="hosted_button_id" runat="server" name="MP-payButton" class="green-ar-l-ov-bron">Assinar Via Mercado Pago </asp:HyperLink>
    
            <script type="text/javascript">
                (function () {
                    function $MPC_load() {
                        window.$MPC_loaded !== true && (function () {
                            var s = document.createElement("script");
                            s.type = "text/javascript";
                            s.async = true;
                            s.src = document.location.protocol + "//secure.mlstatic.com/mptools/render.js";
                            var x = document.getElementsByTagName('script')[0];
                            x.parentNode.insertBefore(s, x);
                            window.$MPC_loaded = true;
                        })();
                    } window.$MPC_loaded !== true ? (window.attachEvent ? window.attachEvent('onload', $MPC_load) : window.addEventListener('load', $MPC_load, false)) : null;
                })();


            </script>
        </div>
    </form>
</body>
</html>
