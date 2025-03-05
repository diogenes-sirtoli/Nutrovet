<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/Logon.Master" AutoEventWireup="true"
    CodeBehind="EscolherPlanoRetorno.aspx.cs" Inherits="Nutrovet.EscolherPlanoRetorno"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <nav id="main-nav" class="navbar navbar-default navbar-fixed-top" role="banner">
        <div class="container">
            <div class="navbar-header ">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">O <strong>NutroVET</strong> é um sistema on-line desenvolvido para o suporte dos profissionais que trabalham com alimentação natural de cães e gatos. É uma ferramenta que vai lhe auxiliar na elaboração de dietas e na prescrição de nutracêuticos, se tornando uma peça fundamental no seu trabalho.</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="PortalNutrovet.aspx">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/logo.png" ToolTip="logo" />
                </a>
            </div>
            <div class="collapse navbar-collapse navbar-right">
                <ul class="nav navbar-nav">
                    <li class="scroll"><a href="PortalNutrovet.aspx"><i class="fa fa-home"></i>&nbsp;Página Inicial</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="container" id="login-block">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-sm-offset-3 col-md-offset-4">
                <div class="login-box clearfix flipInY">
                    <div class="login-logo">
                        <asp:HyperLink ID="hlkloginLogo" runat="server" NavigateUrl="#">
                            <asp:Image ID="imgLoginLogo" alt="Logo NutroVET" runat="server" ImageUrl="~/Imagens/login-logoMaster.png" />
                        </asp:HyperLink>
                    </div>
                    <hr />
                    <div class="login-form " style="padding: 0 5px 0 5px;">
                        <div class="col-lg-12" style="color: #fff; border-left: groove; border-bottom: groove; border-right: groove;">
                            <h4 class="media-heading text-center"><i class="fas fa-info-circle"></i>&nbsp;INFORMAÇÃO&nbsp;<i class="fas fa-info-circle"></i></h4>
                            <p>Agora envie um comprovante de sua situação profissional para o e-mail contato@nutrovet.com.br</p>
                            <p>Pode ser:</p>
                            <ul>
                                <li>imagem da sua carteirinha profissional no CRMV</li>
                                <li>diploma de conclusão de curso em Veterinária ou Zootecnia, ou ainda</li>
                                <li>seu comprovante de matrícula atualizado na faculdade de Veterinária ou Zootecnia</li>
                            </ul>
                            <p>Depois, aguarde a liberação do sistema, que será informada por e-mail.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
