<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="TesteTabControlDois.aspx.cs" Inherits="Nutrovet.Temp.TesteTabControlDois"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <table class="table table-striped projects dataTables-example" id="table-exigencias-nutricionais">
        <thead>
            <tr>
                <td>Nutriente</td>
                <td colspan="2">1000 Kcal</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Mínimo</td>
                <td>Máximo</td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="lblNutriente" runat="server" Text="  "></asp:Label>
                    asdfas</td>
                <td>
                    <asp:Label ID="lblValMin" runat="server" Text="  "></asp:Label>
                    asdfasd</td>
                <td>
                    <asp:Label ID="lblValMax" runat="server" Text="  "></asp:Label>
                    sdfasdf</td>
            </tr>
        </tbody>
    </table>
</asp:Content>
