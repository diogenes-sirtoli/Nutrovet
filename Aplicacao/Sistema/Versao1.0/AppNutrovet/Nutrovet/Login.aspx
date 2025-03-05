<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/Logon.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Nutrovet.Login" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function chamarIzi() {
             $("#modal-demo").iziModal({
                    title: '<i class="fas fa-exclamation-circle"></i>  NUTROVET INFORMA',
                    subtitle: '<strong>Manutenção de Servidores</strong>',
                    headerColor: '#FF8C00',
                    background: null,
                    theme: '',  // light
                    icon: null,
                    iconText: null,
                    iconColor: '#000000',
                    rtl: false,
                    width: 400,
                    top: null,
                    bottom: null,
                    borderBottom: true,
                    padding: 15,
                    radius: 4,
                    zindex: 999,
                    iframe: false,
                    iframeHeight: 400,
                    iframeURL: null,
                    focusInput: true,
                    group: '',
                    loop: false,
                    arrowKeys: true,
                    navigateCaption: true,
                    navigateArrows: true, // Boolean, 'closeToModal', 'closeScreenEdge'
                    history: false,
                    restoreDefaultContent: false,
                    autoOpen: 0, // Boolean, Number
                    bodyOverflow: false,
                    fullscreen: false,
                    openFullscreen: false,
                    closeOnEscape: true,
                    closeButton: true,
                    appendTo: 'body', // or false
                    appendToOverlay: 'body', // or false
                    overlay: true,
                    overlayClose: true,
                    overlayColor: 'rgba(0, 0, 0, 0.8)',
                    timeout: 10000,
                    timeoutProgressbar: true,
                    pauseOnHover: true,
                    timeoutProgressbarColor: 'rgba(0,0,0,0.5)',
                    transitionIn: 'comingIn',   // comingIn, bounceInDown, bounceInUp, fadeInDown, fadeInUp, fadeInLeft, fadeInRight, flipInX
                    transitionOut: 'comingOut', // comingOut, bounceOutDown, bounceOutUp, fadeOutDown, fadeOutUp, , fadeOutLeft, fadeOutRight, flipOutX
                    transitionInOverlay: 'fadeIn',
                    transitionOutOverlay: 'fadeOut',
                });
            $('#modal-demo').iziModal('close');
        }
    </script>
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
                    <li><a data-toggle="collapse" data-target=".in" href="Plano/EscolherPlano.aspx"><i class="fas fa-handshake"></i>&nbsp;Planos</a></li>
                    <li><a data-toggle="collapse" data-target=".in" href="Login.aspx"><i class="fa fa-door-closed"></i>&nbsp;Login</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="container animated fadeInDown" id="login-block">
        <div class="row">
            <div class="col-sm-8 col-sm-offset-3">
                <div class="login-box clearfix flipInY">
                    <div class="page-icon animated flip">
                        <asp:Image ID="imgUserIcon" src="Imagens/user-icon.png" alt="Key icon" runat="server" />
                    </div>
                    <div class="login-logo">
                        <asp:HyperLink ID="hlkloginLogo" runat="server" NavigateUrl="#">
                            <asp:Image ID="imgLoginLogo" src="Imagens/login-logoMaster.png" alt="Logo NutroVET" runat="server" />
                        </asp:HyperLink>
                    </div>
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
                                <asp:TextBox ID="tbUser" placeholder="E-Mail" CssClass="input-field" required="true" runat="server"></asp:TextBox>
                                <asp:TextBox ID="tbSenha" placeholder="Senha" CssClass="input-field" required="true" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:Button ID="btnSubmit" CssClass="btn btn-login" runat="server" Text="Login" ToolTip="Login" OnClick="btnSubmit_Click"></asp:Button>
                                <div class="login-links">
                                    <asp:HyperLink ID="hlkEsqueciMinhaSenha" runat="server" NavigateUrl="./EsqueciMinhaSenha.aspx">Esqueceu a senha? <strong>Clique aqui</strong></asp:HyperLink>
                                    <br />
                                    <asp:HyperLink ID="hlkCriarConta" runat="server" NavigateUrl="./Plano/Registrar.aspx">Ainda não tem uma conta? <strong>Criar uma</strong></asp:HyperLink>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modal-demo" class="iziModal">
        Entre os dias 19 e 21 de abril de 2021 o<br />
        Sistema NutroVET poderá passar por momentos<br />
        de instabilidade e indisponibilidade, em<br />
        virtude de manutenção a ser realizada nos<br />
        Servidores pelo Provedor.<br /><br />
        Agradecemos a compreensão!
    </div>
</asp:Content>
