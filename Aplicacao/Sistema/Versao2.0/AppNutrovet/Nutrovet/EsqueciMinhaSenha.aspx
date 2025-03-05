<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/Logon.Master" AutoEventWireup="true"
    CodeBehind="EsqueciMinhaSenha.aspx.cs" Inherits="Nutrovet.EsqueciMinhaSenha"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav id="main-nav" class="navbar navbar-default navbar-fixed-top" role="navigation">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                    <span class="sr-only">O <strong>NutroVET</strong> é um sistema on-line desenvolvido para o suporte dos profissionais que trabalham com alimentação natural de cães e gatos. É uma ferramenta que vai lhe auxiliar na elaboração de dietas e na prescrição de nutracêuticos, se tornando uma peça fundamental no seu trabalho.</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <img src="Imagens/logo.png" alt="logo" onclick="location.href='PortalNutrovet.aspx'" />
            </div>
            <div class="collapse navbar-collapse navbar-right" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav">
                    <li><a data-toggle="collapse" data-target=".in" href="PortalNutrovet.aspx"><i class="fa fa-home"></i>&nbsp;Portal</a></li>
                    <li><a data-toggle="collapse" data-target=".in" href="PortalNutrovet.aspx#features"><i class="fas fa-clipboard-list"></i>&nbsp;Funcionalidades</a></li>
                    <li><a data-toggle="collapse" data-target=".in" href="PortalNutrovet.aspx#about"><i class="fas fa-desktop"></i>&nbsp;O Sistema</a></li>
                    <li><a data-toggle="collapse" data-target=".in" href="PortalNutrovet.aspx#contact-us"><i class="fa fa-envelope"></i>&nbsp;Contato</a></li>
                    <li><a data-toggle="collapse" data-target=".in" href="./Plano/EscolherPlano.aspx"><i class="fas fa-handshake"></i>&nbsp;Planos</a></li>
                    <li><a data-toggle="collapse" data-target=".in" href="Login.aspx"><i class="fa fa-door-closed"></i>&nbsp;Login</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="container" id="login-block">
        <div class="row">
            <div class="col-sm-8 col-sm-offset-3">
                <div class="login-box clearfix flipInY">
                    <div class="page-icon animated flip">
                        <asp:Image ID="imgQuestionmarkIcon" ImageUrl="Imagens/login-questionmark-icon.png" runat="server" />
                    </div>
                    <div class="login-logo">
                        <asp:HyperLink ID="hlkloginLogo" runat="server" NavigateUrl="#">
                            <asp:Image ID="imgLoginLogo" src="Imagens/login-logoMaster.png" runat="server" />
                        </asp:HyperLink>
                    </div>
                    <hr />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="login-form" style="padding: 0 10px  10px 10px;">
                                <!-- Start Error box -->
                                <div runat="server" id="alertas">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                    <h4>
                                        <asp:Label ID="lblAlerta" runat="server" Text=""></asp:Label>
                                    </h4>
                                </div>
                                <!-- End Error box -->
                                <div class="col-lg-12" style="color: #fff; border-left: groove; border-bottom: groove; border-right: groove; padding-bottom: 20px">
                                    <h4 class="media-heading text-center">Recuperação de Senha</h4>
                                    Informe e-mail de cadastro. Enviaremos sua Senha!
                                <asp:TextBox ID="txbEmail" placeholder="E-mail" CssClass="btn btn-block text-left" required="true" runat="server" TextMode="Email" Width="100%"></asp:TextBox>
                                    <asp:Button ID="btnSubmit" CssClass="btn btn-login btn-block" runat="server" Text="Enviar Senha" ToolTip="Enviar Senha" OnClick="btnSubmit_Click"></asp:Button>
                                </div>
                                <div class="login-links">
                                    <asp:HyperLink ID="hlkCriarConta" runat="server" NavigateUrl="~/Plano/EscolherAssinatura.aspx">Ainda não tem uma conta? <strong>Criar uma</strong></asp:HyperLink>
                                    <br />
                                    <asp:HyperLink ID="hlkLogin" runat="server" NavigateUrl="~/Login.aspx">Já tem uma conta? <strong>Fazer Login</strong></asp:HyperLink>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
