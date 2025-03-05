<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="CadastroTabelasNutr.aspx.cs" Inherits="Nutrovet.Temp.CadastroTabelasNutr"
    ValidateRequest="false" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Tabela&amp;nbsp;Nutricional"></asp:Label>
                    </td>
                    <td colspan="7">
                        <asp:DropDownList ID="ddlTabNutr" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Espécie"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlEspecie" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Indicação"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlIndic" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Tipo&amp;nbsp;de&amp;nbsp;Valor"></asp:Label>
                    </td>
                    <td colspan="7">
                        <asp:DropDownList ID="ddlTpValor" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Nutriente&amp;nbsp;1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlNutr1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNutr1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Unidade&amp;nbsp;1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlUnd1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnd1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Valor&amp;nbsp;1"></asp:Label>
                    </td>
                    <td>
                        <cc1:MEdit ID="meValor1" runat="server" Mascara="Float"></cc1:MEdit>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Proporção&amp;nbsp;1"></asp:Label>
                    </td>
                    <td>
                        <cc1:MEdit ID="meProp1" runat="server" Mascara="Float" Enabled="False"></cc1:MEdit>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Nutriente&amp;nbsp;2"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNutr2" runat="server" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="ddlNutr2_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Unidade&amp;nbsp;2"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlUnd2" runat="server" Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Valor&amp;nbsp;2"></asp:Label>
                    </td>
                    <td>
                        <cc1:MEdit ID="meValor2" runat="server" Enabled="False" Mascara="Float"></cc1:MEdit>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Proporção&amp;nbsp;2"></asp:Label>
                    </td>
                    <td>
                        <cc1:MEdit ID="meProp2" runat="server" Mascara="Float" Enabled="False"></cc1:MEdit>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Inserir" OnClick="Button1_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Alterar" OnClick="Button2_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="Button3" runat="server" Text="Excluir" OnClick="Button3_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Panel ID="pnlListagem" runat="server" Height="600px" ScrollBars="Vertical" Width="100%">
                            <asp:GridView ID="dgExigNutri" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="IdExigNutr" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="dgExigNutri_SelectedIndexChanged" Width="100%">
                                <RowStyle Font-Size="9px" />
                                <Columns>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Temp/RadioUncheck.jpg" ShowSelectButton="True">
                                        <ItemStyle Width="25px" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="TabelaNutricional" HeaderText="Tab Nutri">
                                        <ItemStyle Width="30" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Especie" HeaderText="Espécie">
                                        <ItemStyle Width="50" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Indicacao" HeaderText="Indicação">
                                        <ItemStyle Width="100" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TipoValor" HeaderText="Tipo Valor">
                                        <ItemStyle Width="60" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nutriente1" HeaderText="Nutriente 1">
                                        <ItemStyle Width="100" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Unidade1" HeaderText="Und 1">
                                        <ItemStyle Width="50" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Valor1" HeaderText="Valor 1" DataFormatString="{0:n3}">
                                        <ItemStyle Width="50" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalProporcao" HeaderText="TOTAL" DataFormatString="{0:n3}">
                                        <ItemStyle Width="50" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <PagerStyle BackColor="#999999" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" Font-Size="14px" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
