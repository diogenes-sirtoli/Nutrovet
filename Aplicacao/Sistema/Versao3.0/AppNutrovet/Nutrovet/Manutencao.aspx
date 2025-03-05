<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manutencao.aspx.cs"
    Inherits="Nutrovet.Manutencao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %>Nutrologia Veterinária - NutroVET</title>
    <asp:PlaceHolder runat="server">
        <link href="<%=ResolveClientUrl("~/CSS/portal/bootstrap.min.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/font-awesome/css/fontawesome.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/font-awesome/css/all.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/font-awesome/css/brands.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/font-awesome/css/regular.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/font-awesome/css/solid.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/portal/animate.min.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/portal/prettyPhoto.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/portal/style_p.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/plugins/iziModal/iziModal.min.css")%>" rel="stylesheet" />

        <script src="<%=ResolveClientUrl("~/Scripts/portal/jquery.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/bootstrap.min.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/mousescroll.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/smoothscroll.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/jquery.prettyPhoto.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/jquery.isotope.min.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/jquery.inview.min.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/wow.min.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/custom-scripts.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/plugins/iziModal/iziModal.min.js")%>" type="text/javascript"></script>

        <link href="<%=ResolveClientUrl("~/Imagens/favicon.ico")%>" rel="shortcut icon" type="image/x-icon" />
    </asp:PlaceHolder>
    <style type="text/css">
        .img-responsive {
            height: 261px;
            width: 614px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title wow fadeInDown">MANUTENÇÃO</h2>
            </div>
            <div class="row text-center">
                <div class="ibox float-e-margins">
                    <div >
                        <asp:Image ID="Image1" runat="server" class="img-responsive" Height="500px" ImageUrl="~/Imagens/Manutencao.jpg" Width="100%"/>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
