<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormBtnPagSeguro.aspx.cs" Inherits="Nutrovet.BtnPagSeguro.FormBtnPagSeguro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%: Page.Title %>Nutrologia ddVeterinária - NutroVET</title>
    <link href="../CSS/portal/style_p.css" rel="stylesheet" />
    <base target="_parent"/>
</head>
<body >

<!-- INICIO FORMULARIO BOTAO PAGSEGURO: NAO EDITE OS COMANDOS DAS LINHAS ABAIXO -->
    
<form action="https://pagseguro.uol.com.br/pre-approvals/request.html" method="post" style="color:red">

    <input type="hidden" name="code" value="<%=_codigoPagSeguro%>"/>
    <input type="hidden" name="iot" value="button" />
    <input type="image" src="https://stc.pagseguro.uol.com.br/public/img/botoes/assinaturas/209x48-assinar-assina.gif" name="submit" alt="Pague com PagSeguro - É rápido, grátis e seguro!" style="margin-left:28%"/>
  
</form>


<!-- FINAL FORMULARIO BOTAO PAGSEGURO -->

    
</body>
</html>
