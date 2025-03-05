<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptBalancoCardapio.aspx.cs" 
    Inherits="Nutrovet.Cardapio.Impressao.RptBalancoCardapio" %>
<%@ OutputCache Duration="1" NoStore="true" VaryByParam="none" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <rsweb:ReportViewer ID="rvBalancodieta" runat="server" Width="100%" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Height="800px">
                <LocalReport ReportPath="Cardapio\Impressao\rptBalancoDieta.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="odcBalancodieta" Name="dsBalancoDieta" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
        <asp:ObjectDataSource ID="odcBalancodieta" runat="server" SelectMethod="ImprimeRelatorio" TypeName="BLL.clCardapioAlimentosBll">
            <SelectParameters>
                <asp:SessionParameter Name="_listagem" SessionField="BalancoDieta" Type="Object" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
