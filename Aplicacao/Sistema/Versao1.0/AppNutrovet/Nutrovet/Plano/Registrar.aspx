<%@ Page Title="" Language="C#" MasterPageFile="~/Logon.Master" AutoEventWireup="true"
    CodeBehind="Registrar.aspx.cs" Inherits="Nutrovet.Plano.Registrar" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-MPQDXWC"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->
    <header id="header">
        <!-- Google Tag Manager -->
        <script>(function (w, d, s, l, i) {
                w[l] = w[l] || []; w[l].push({
                    'gtm.start':
                        new Date().getTime(), event: 'gtm.js'
                }); var f = d.getElementsByTagName(s)[0],
                    j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                        'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
            })(window, document, 'script', 'dataLayer', 'GTM-MPQDXWC');</script>
        <!-- End Google Tag Manager -->
        <nav id="main-nav" class="navbar navbar-default navbar-fixed-top" role="banner">
            <div class="container">
                <div class="navbar-header ">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="sr-only">O <strong>NutroVET</strong> é um sistema on-line desenvolvido para o suporte dos profissionais que trabalham com alimentação natural de cães e gatos. É uma ferramenta que vai lhe auxiliar na elaboração de dietas e na prescrição de nutracêuticos, se tornando uma peça fundamental no seu trabalho.</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="../PortalNutrovet.aspx">
                        <img src="../Imagens/logo.png" alt="logo" />
                    </a>
                </div>
                <div class="collapse navbar-collapse navbar-right">
                    <ul class="nav navbar-nav">
                        <li class="scroll"><a href="../PortalNutrovet.aspx"><i class="fa fa-home"></i>&nbsp;Página Inicial</a></li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    <div class="container" id="login-block">
        <div class="row">
            <div class="col-sm-8 col-sm-offset-3">
                <div class="login-box clearfix flipInY">
                    <div class="page-icon animated flip">
                        <asp:Image ID="imgQuestionmarkIcon" src="../Imagens/register-icon.png" alt="Questionmark icon" runat="server" />
                    </div>
                    <div class="login-logo">
                        <asp:HyperLink ID="hlkloginLogo" runat="server" NavigateUrl="#">
                            <asp:Image ID="imgLoginLogo" src="../Imagens/login-logoMaster.png" alt="Logo NutroVET" runat="server" />
                        </asp:HyperLink>
                    </div>
                    <hr />
                    <div class="login-form">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <!-- Start Error box -->
                                <div runat="server" id="alertas">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                    <h4>
                                        <asp:Label ID="lblAlerta" runat="server" Text=""></asp:Label>
                                    </h4>
                                </div>
                                <!-- End Error box -->
                                <asp:TextBox ID="tbNome" placeholder="Nome" CssClass="input-field" required="true" runat="server"></asp:TextBox>
                                <asp:TextBox ID="tbEmail" placeholder="E-mail" CssClass="input-field" required="true" runat="server" TextMode="Email"></asp:TextBox>
                                <asp:TextBox ID="tbSenha" placeholder="Senha" CssClass="input-field" required="true" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:TextBox ID="tbConfirmarSenha" placeholder="Confirmar Senha" CssClass="input-field" required="true" runat="server" TextMode="Password"></asp:TextBox>

                                <div id="divRegistrar" runat="server">
                                    <asp:Button ID="btnSubmit" CssClass="btn btn-login" runat="server" Text="Registrar" ToolTip="Enviar Registro" OnClick="btnSubmit_Click"></asp:Button>
                                </div>
                                <div id="divFrame" runat="server">
                                    <iframe runat="server" name="frmBtnPagSeguro" id="frmBtnPagSeguro" width="480" height="75" frameborder="0"></iframe>
                                </div>
                                <div class="login-links">
                                    <asp:HyperLink ID="hlkEsqueciMinhaSenha" runat="server" NavigateUrl="~/EsqueciMinhaSenha.aspx">Esqueci minha senha? <strong>Clique aqui</strong></asp:HyperLink>
                                    <br />
                                    <asp:HyperLink ID="hlkLogin" runat="server" NavigateUrl="~/Login.aspx">Já tem uma conta? <strong>Fazer Login</strong></asp:HyperLink>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#tbDtNascim').datepicker({
                format: "dd/mm/yyyy",
                clearBtn: true,
                language: "pt-BR",
                daysOfWeekHighlighted: "0,6",
                autoclose: true,
                todayHighlight: true
            });
        });
    </script>
</asp:Content>

