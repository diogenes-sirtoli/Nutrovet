<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptCardapio.aspx.cs" 
    Inherits="Nutrovet.Cardapio.Impressao.RptCardapio" %>
<%@ OutputCache Duration="1" NoStore="true" VaryByParam="none" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" 
    TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <rsweb:ReportViewer ID="rvCardapio" runat="server" Width="100%" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Height="800px">
                <LocalReport ReportPath="Cardapio\Impressao\rptCardapio.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="odcListaAlimentos" Name="dsListaAlimentos" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
        <asp:ObjectDataSource ID="odcListaAlimentos" runat="server" SelectMethod="ListarTO" TypeName="BLL.clCardapioAlimentosBll">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="_idCard" QueryStringField="_idCard" Type="Int32" />
                <asp:QueryStringParameter DefaultValue="0" Name="_idPessoa" QueryStringField="_idPessoa" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
