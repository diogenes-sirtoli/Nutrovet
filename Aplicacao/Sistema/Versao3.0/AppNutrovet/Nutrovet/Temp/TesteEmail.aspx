<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TesteEmail.aspx.cs" Inherits="Nutrovet.Temp.TesteEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 132px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lblMsgTexto" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label1" runat="server" Text="De"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDe" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label2" runat="server" Text="Para"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbPara" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label3" runat="server" Text="Assunto"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbAssunto" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label4" runat="server" Text="Mensagem"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbMsg" runat="server" Width="400px" Height="109px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label6" runat="server" Text="SMTP"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSmtp" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label7" runat="server" Text="Porta"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbPorta" runat="server" Width="400px" TextMode="Number"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label8" runat="server" Text="Conta"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbConta" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label9" runat="server" Text="Senha"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSenha" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label10" runat="server" Text="SSL"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbxSSL" runat="server" Text="&amp;nbsp;&amp;nbsp;SSL" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" Text="Enviar E-Mail" />
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
