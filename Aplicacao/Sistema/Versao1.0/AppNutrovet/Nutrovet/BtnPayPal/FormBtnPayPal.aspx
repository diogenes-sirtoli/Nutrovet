<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormBtnPayPal.aspx.cs" 
    Inherits="Nutrovet.BtnPayPal.FormBtnPayPal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
  <script
    src="https://www.paypal.com/sdk/js?client-id=AV8WxuojyKzifnN-MtfujAN5KBetsJENfy0LUh7pr4zOYnzD_NUQ21uAKJBH4WAzfREJXzegc43KF-Oc" data-csp-nonce="xyz-123"> // Required. Replace SB_CLIENT_ID with your sandbox client ID.
  </script>
    <form id="form1" runat="server" action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_top">
        <div>
            <input type="hidden" name="cmd" value="_s-xclick" />
            <input runat="server" type="hidden" name="hosted_button_id" value="" id="hosted_button_id" />
            <input type="image" src="https://www.paypalobjects.com/pt_BR/BR/i/btn/btn_subscribeCC_LG.gif" style="border:0" name="submit" 
                alt="PayPal - A maneira fácil e segura de enviar pagamentos online!" />
            <img alt="" border="0" src="https://www.paypalobjects.com/pt_BR/i/scr/pixel.gif" 
                width="1" height="1" />
        </div>
    </form>
</body>
</html>
