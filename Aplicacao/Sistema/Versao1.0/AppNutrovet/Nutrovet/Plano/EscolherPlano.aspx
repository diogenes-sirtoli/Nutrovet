<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EscolherPlano.aspx.cs"
    Inherits="Nutrovet.Plano.EscolherPlano" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <title><%: Page.Title %>Nutrologia Veterinária - NutroVET</title>
    <link href="<%=ResolveClientUrl("~/CSS/bootstrap.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveClientUrl("~/CSS/all.css")%>" rel="stylesheet" />
    <link href="<%=ResolveClientUrl("~/CSS/portal/style_p.css")%>" rel="stylesheet" />
    <script src="<%=ResolveClientUrl("~/Scripts/jquery-3.3.1.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/bootstrap.min.js")%>" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/Imagens/favicon.ico")%>" rel="shortcut icon" type="image/x-icon" />
    <!-- Mainly scripts -->
</head>
<body>
    <header id="header">

        <nav id="main-nav" class="navbar navbar-default navbar-fixed-top" role="navigation">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span class="sr-only">O <strong>NutroVET</strong> é um sistema on-line desenvolvido para o suporte dos profissionais que trabalham com alimentação natural de cães e gatos. É uma ferramenta que vai lhe auxiliar na elaboração de dietas e na prescrição de nutracêuticos, se tornando uma peça fundamental no seu trabalho.</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <img src="../Imagens/logo.png" alt="logo" onclick="location.href='PortalNutrovet.aspx'" />
                </div>
                <div class="collapse navbar-collapse navbar-right" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav">
                        <li><a data-toggle="collapse" data-target=".in" href="../PortalNutrovet.aspx"><i class="fa fa-home"></i>&nbsp;Portal</a></li>
                        <li><a data-toggle="collapse" data-target=".in" href="../PortalNutrovet.aspx#features"><i class="fas fa-clipboard-list"></i>&nbsp;Funcionalidades</a></li>
                        <li><a data-toggle="collapse" data-target=".in" href="../PortalNutrovet.aspx#about"><i class="fas fa-desktop"></i>&nbsp;O Sistema</a></li>
                        <li><a data-toggle="collapse" data-target=".in" href="../PortalNutrovet.aspx#contact-us"><i class="fa fa-envelope"></i>&nbsp;Contato</a></li>
                        <li><a data-toggle="collapse" data-target=".in" href="EscolherPlano.aspx"><i class="fas fa-handshake"></i>&nbsp;Planos</a></li>
                        <li><a data-toggle="collapse" data-target=".in" href="../Login.aspx"><i class="fa fa-door-closed"></i>&nbsp;Login</a></li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    <!-- Start Error box -->
    <div runat="server" id="alertas">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <h4>
            <asp:Label ID="lblAlerta" runat="server" Text=""></asp:Label>
        </h4>
    </div>
    <!-- End Error box -->
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <section class="EscolherPlano">
            <h1 class=" wow fadeInDown">NutroVET</h1>
            <h3><strong>Nutrologia Veterinária Inteligente</strong></h3>
        </section>
        <section id="pricing" style="background: #F0F0F0;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="container">
                        <div class="section-header">
                            <h2 class="section-title wow fadeInDown">Planos</h2>
                            <h4><small>O sistema oferece espaços diferentes para você cadastrar seus pacientes, de forma que você pode pagar de acordo com o espaço que precisará utilizar. Independentemente se você vai pagar por ano ou por mês, o espaço que você estará contratando é o mesmo, então ao atingir o seu número de pacientes limite, basta migrar para um plano com maior espaço.</small></h4>
                        </div>
                        <div id="generic_price_table">
                            <div class="container">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="generic_content clearfix">
                                            <div class="generic_head_price clearfix">
                                                <div class="generic_head_content clearfix">
                                                    <div class="head_bg_green"></div>
                                                    <div class="head">
                                                        <span>Básico</span>
                                                    </div>
                                                </div>
                                                <div class="generic_price_tag clearfix">
                                                    <span class="price">
                                                        <span class=" cent label label-success">até 10 pacientes</span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="generic_feature_list">
                                                <ul>
                                                    <li>
                                                        <div class="radio radio-primary">
                                                            <h4>
                                                                <asp:RadioButton ID="rbBasicoMensal" runat="server" CssClass="alert" Text="R$ 20,00/mês" AutoPostBack="True" GroupName="grRB" OnCheckedChanged="rbBasicoMensal_CheckedChanged" />
                                                            </h4>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <div class="radio radio-primary">
                                                            <h4>
                                                                <asp:RadioButton ID="rbBasicoAnual" runat="server" CssClass="alert" Text="R$ 200,00/ano" AutoPostBack="True" GroupName="grRB" OnCheckedChanged="rbBasicoAnual_CheckedChanged" />
                                                            </h4>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="generic_price_btn clearfix">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="generic_content clearfix">
                                            <div class="generic_head_price clearfix">
                                                <div class="generic_head_content clearfix">
                                                    <div class="head_bg_orange"></div>
                                                    <div class="head">
                                                        <span>Intermediário</span>
                                                    </div>
                                                </div>
                                                <div class="generic_price_tag clearfix">
                                                    <span class="price">
                                                        <span class="cent label label-warning">até 20 pacientes</span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="generic_feature_list">
                                                <ul>
                                                    <li>
                                                        <div class="radio radio-primary">
                                                            <h4>
                                                                <asp:RadioButton ID="rbIntermediarioMensal" runat="server" CssClass="alert" Text="R$ 40,00/mês" AutoPostBack="True" GroupName="grRB" OnCheckedChanged="rbIntermediarioMensal_CheckedChanged" />
                                                            </h4>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <div class="radio radio-primary">
                                                            <h4>
                                                                <asp:RadioButton ID="rbIntermediarioAnual" runat="server" CssClass="alert" Text="R$ 400,00/ano" AutoPostBack="True" GroupName="grRB" OnCheckedChanged="rbIntermediarioAnual_CheckedChanged" />
                                                            </h4>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="generic_price_btn clearfix">
                                            </div>
                                        </div>
                                        <div class="generic_price_btn clearfix">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="generic_content clearfix">
                                            <div class="generic_head_price clearfix">
                                                <div class="generic_head_content clearfix">
                                                    <div class="head_bg_red"></div>
                                                    <div class="head">
                                                        <span>Completo</span>
                                                    </div>
                                                </div>
                                                <div class="generic_price_tag clearfix">
                                                    <span class="price">
                                                        <span class="cent label label-danger">+ de 20 pacientes</span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="generic_feature_list">
                                                <ul>
                                                    <li>
                                                        <div class="radio radio-primary ">
                                                            <h4>
                                                                <asp:RadioButton ID="rbCompletoMensal" runat="server" CssClass="alert" Text="R$ 60,00/mês" AutoPostBack="True" GroupName="grRB" OnCheckedChanged="rbCompletoMensal_CheckedChanged" />
                                                            </h4>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <div class="radio radio-primary ">
                                                            <h4>
                                                                <asp:RadioButton ID="rbCompletoAnual" runat="server" CssClass="alert" Text="R$ 600,00/ano" AutoPostBack="True" GroupName="grRB" OnCheckedChanged="rbCompletoAnual_CheckedChanged" />
                                                            </h4>
                                                        </div>

                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="generic_price_btn clearfix">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <fieldset disabled="disabled">
                        <div class="container">
                            <div class="section-header">
                                <h2 class="section-title wow fadeInDown">Módulos Adicionais</h2>
                                <h4><small>Em breve serão disponibilizados dois novos módulos adicionais, que você poderá optar por assinar ou não, de acordo com a sua necessidade. O <strong>Receituário Inteligente</strong> será uma ferramenta para auxiliar na elaboração de receitas de forma automatizada, e o <strong>Prontuário</strong> será uma forma de organizar as dietas, receitas, informações das consultas e demais detalhes, de forma prática e organizada como uma linha do tempo.</small></h4>
                            </div>
                            <div id="generic_price_table3">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="generic_content clearfix">
                                                <div class="generic_head_price clearfix">
                                                    <div class="generic_head_content clearfix">
                                                        <div class="head_bg_green"></div>
                                                        <div class="head">
                                                            <span>Receituário Inteligente</span>
                                                        </div>
                                                    </div>
                                                    <div class="generic_feature_list" id="divReceituario" runat="server">
                                                        <ul>
                                                            <li>
                                                                <div id="recint" class="radio radio-primary">
                                                                    <h4>
                                                                        <asp:RadioButton ID="rbReceituarioInteligenteMensal" runat="server" CssClass="alert" Text="R$ 10,00/mês" AutoPostBack="True" GroupName="grRI" OnCheckedChanged="rbReceituarioInteligenteMensal_CheckedChanged" />
                                                                    </h4>
                                                                </div>
                                                            </li>
                                                            <li>
                                                                <div class="radio radio-primary">
                                                                    <h4>
                                                                        <asp:RadioButton ID="rbReceituarioInteligenteAnual" runat="server" CssClass="alert" Text="R$ 100,00/ano" AutoPostBack="True" GroupName="grRI" OnCheckedChanged="rbReceituarioInteligenteAnual_CheckedChanged" />
                                                                    </h4>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="generic_content clearfix">
                                                <div class="generic_head_price clearfix">
                                                    <div class="generic_head_content clearfix">
                                                        <div class="head_bg_green"></div>
                                                        <div class="head">
                                                            <span>Prontuário</span>
                                                        </div>
                                                    </div>
                                                    <div class="generic_feature_list" id="divProntuario" runat="server">
                                                        <ul>
                                                            <li>
                                                                <div class="radio radio-primary">
                                                                    <h4>
                                                                        <asp:RadioButton ID="rbProntuarioMensal" runat="server" CssClass="alert" Text="R$ 15,00/mês" AutoPostBack="True" GroupName="grAD" OnCheckedChanged="rbProntuarioMensal_CheckedChanged" />
                                                                    </h4>
                                                                </div>
                                                            </li>

                                                            <li>
                                                                <div class="radio radio-primary">
                                                                    <h4>
                                                                        <asp:RadioButton ID="rbProntuarioAnual" runat="server" CssClass="alert" Text="R$ 150,00/ano" AutoPostBack="True" GroupName="grAD" OnCheckedChanged="rbProntuarioAnual_CheckedChanged" />
                                                                    </h4>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <div class="container">
                        <div class="section-header">
                            <h2 class="section-title wow fadeInDown">Pagamento via
                        <img src="../Imagens/pagseguro.png" alt="logo" /></h2>
                        </div>

                        <div class="section-header">
                            <h1 class="section-title wow fadeInDown"><small>Depois de escolher o plano desejado, basta clicar em assinar agora para realizar seu cadastro e escolher a forma de pagamento.</small></h1>
                        </div>


                        <div id="generic_price_table4">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="generic_content clearfix">
                                            <div class="generic_head_price clearfix">
                                                <div class="generic_head_content clearfix">
                                                    <div class="head_bg_green"></div>
                                                    <div class="head">
                                                        <span>Digite aqui seu Voucher!</span>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="pnlVoucher" runat="server" DefaultButton="btnCalcula">
                                                    <div class="col-lg-12 ">
                                                        <h2 class="section-title wow fadeInDown">
                                                            <asp:TextBox ID="txbVoucher" runat="server" class="form-control  mx-sm-3 mb-2" placeholder="Informe aqui" TextMode="Number" ToolTip="Informe o número do VOUCHER" OnTextChanged="txbVoucher_TextChanged"></asp:TextBox>
                                                        </h2>
                                                    </div>
                                                    <asp:Button ID="btnCalcula" runat="server" BackColor="#F6F6F6" BorderColor="#F6F6F6" BorderStyle="None" OnClick="btnCalcula_Click" Height="5px" Width="10px" />
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="generic_content clearfix">
                                            <div class="generic_head_price clearfix">
                                                <div class="generic_head_content clearfix">
                                                    <div class="head_bg_green"></div>
                                                    <div class="head">
                                                        <span>Total:
                                                            <asp:Label ID="lblValor" CssClass="label label-light m-md" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                                                        </span>
                                                    </div>
                                                </div>

                                                <div class="col-lg-12 ">
                                                    <div class="generic_feature_list">
                                                        <h2 class="section-title wow fadeInDown">
                                                            <asp:LinkButton runat="server" ID="lbSalvar" CssClass="btn btn-success" OnClick="btnPgto_Click"> <i class="fas fa-hands-helping"></i> ASSINAR AGORA</asp:LinkButton>
                                                        </h2>
                                                    </div>
                                                    <asp:Button ID="Button1" runat="server" BackColor="#F6F6F6" BorderColor="#F6F6F6" BorderStyle="None" Height="5px" Width="10px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="modalPopUp" class="modal-dialog modal-xs" runat="server" style="display: none">
                        <div class="modal-content animated fadeIn">
                            <div class="modal-header modal-header-warning">
                                <h4 class="modal-title">
                                    <asp:Label ID="Label1" runat="server"><i class="fas fa-hand-pointer"></i>&nbsp; Escolha dos Planos</asp:Label>
                                </h4>
                            </div>
                            <div class="modal-body" id="content-resumo-cardapio">
                                <span>
                                    <asp:Label ID="lblTexto" runat="server"
                                        Text="Funcionalidade disponível somente em 20 de Abril de 2019."></asp:Label>
                                </span>
                            </div>
                            <div class="modal-footer">
                                <button type="button" id="btnFechaResumo" class="btn btn-sm btn-white pull-right m-t-n-xs" data-dismiss="modal"><i class='fas fa-door-open'></i>&nbsp;Fechar</button>
                            </div>
                        </div>
                    </div>
                    <ajaxToolkit:ModalPopupExtender ID="modal" runat="server"
                        PopupControlID="modalPopUp" BackgroundCssClass="modalBackground"
                        TargetControlID="lblResumoCardapio" CancelControlID="btnFechaResumo">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Label ID="lblResumoCardapio" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </form>
    <div class="pagina-footer navbar-fixed-bottom" role="banner">
        <div class="container">
            <div class="pull-left">
                |&nbsp;&nbsp;<a href="https://www.youtube.com/channel/UCPk1NVPuAgVPjf6eQOI5qeg?view_as=public" target="_blank"><i class="fab fa-youtube"></i></a>&nbsp;
                |&nbsp;&nbsp;<a href="https://www.facebook.com/nutrovetonline/" target="_blank" class="facebook"><i class="fab fa-facebook"></i></a>&nbsp;
                |&nbsp;<a href="https://www.instagram.com/nutrovetonline/" target="_blank" class="instagram"><i class="fab fa-instagram"></i></a>&nbsp;|
            </div>
            <div class="pull-right">
                NutroVET by <strong>SICORP &copy;<asp:Label ID="lblAno" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </div>
    <!-- Google Tag Manager (noscript) -->
    <noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-MPQDXWC"
    height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
    <!-- End Google Tag Manager (noscript) -->
</body>
</html>
