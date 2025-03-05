<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="CardapioImpressao.aspx.cs" Inherits="Nutrovet.Cardapio.Impressao.CardapioImpressao"
    ValidateRequest="false" %>

<%@ OutputCache Duration="1" NoStore="true" VaryByParam="none" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $('.dropdown-toggle').dropdown();
        })

        $('body').on('keydown', 'input, select, textarea', function (e) {
            var self = $(this)
                , form = self.parents('form:eq(0)')
                , focusable
                , next
                ;
            if (e.keyCode == 13) {
                focusable = form.find('input,a,select,button,textarea').filter(':visible');
                next = focusable.eq(focusable.index(this) + 1);
                if (next.length) {
                    next.focus();
                } else {
                    form.submit();
                }
                return false;
            }
        });

    </script>
    <div class="">
        <div class="page-title">
            <div class="clearfix"></div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="row wrapper border-bottom white-bg page-heading">
                        <div class="col-lg-1">
                            <asp:HyperLink ID="HyperLink12" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
                        </div>
                        <h2>&nbsp;Receita Nutricional</h2>
                        <div class="col-lg-8">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlMenuGeral" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <asp:HyperLink ID="hlCardapio" NavigateUrl="~/Cardapio/CardapioCadastro.aspx" runat="server"><i class="fas fa-balance-scale"></i> Cardápios</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <strong>
                                        <i class="fas fa-print"></i>
                                        <asp:Label ID="Label1" runat="server" Text=" Impressão do Cardápio"></asp:Label>
                                    </strong>
                                </li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row wrapper border-bottom white-bg page-heading">
                <div>
                    <div class="ibox-title">
                        <h5>
                            <asp:Label ID="lblSubTitulo" runat="server" Text="Receituário Nutricional"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12">
                    <div class="wrapper wrapper-content fadeInRight">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-sm-4" style="justify-content: center; flex-direction: column; align-items: flex-start;">
                                                <asp:Image ID="imgLogo" ImageUrl="~/Perfil/Logotipos/logo_receita.png" alt="logotipo" runat="server" Style="width: 50%;" />
                                            </div>
                                            <div class="col-sm-8" id="divCabecalhoGrande" name="divCabecalhoGrande" runat="server" visible="true">
                                                <h3>
                                                    <asp:Label ID="lblNomeClin" runat="server" class="col-lg-12 control-label text-right" Text="Nome do Profissional ou Clínica"></asp:Label>
                                                </h3>
                                                <h5>
                                                    <asp:Label ID="lblSlogan" runat="server" class="col-lg-12 control-label text-right" Text="Slogan, site ou rede social"></asp:Label>
                                                </h5>
                                                <h5>
                                                    <asp:Label ID="lblEndereco" runat="server" class="col-lg-12 control-label text-right" Text="Endereço"></asp:Label>
                                                </h5>
                                                <h5>
                                                    <asp:Label ID="lblEMail" runat="server" class="col-lg-12 control-label text-right" Text="E-Mail"></asp:Label>
                                                </h5>
                                                <h5>
                                                    <asp:Label ID="lblTelefone" runat="server" class="col-lg-12 control-label text-right" Text="Telefone"></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="col-sm-8" id="divCabecalhoSlim" name="divCabecalhoSlim" runat="server" visible="false">
                                                <asp:Label ID="lblCabecalhoSlim" runat="server" class="col-lg-12 control-label text-right" Text="Cabecalho"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-7">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h4>DADOS DO PACIENTE</h4>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-sm-8 b-r">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbNomePacienteReceita" runat="server" class="control-label text-left" Text="Paciente:"></asp:Label>
                                                    <strong>
                                                        <asp:Label ID="lblPaciente" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbPesoPacienteReceita" runat="server" class="control-label text-left" Text="Peso:"></asp:Label>
                                                    <strong>
                                                        <asp:Label ID="lblPeso" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                            <div class="col-sm-8 b-r">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbEspeciePacienteReceita" runat="server" class="control-label text-left" Text="Espécie:"></asp:Label>
                                                    <strong>
                                                        <asp:Label ID="lblEspecie" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbSexoPacienteReceita" runat="server" class="control-label text-left" Text="Sexo:"></asp:Label>
                                                    <strong>
                                                        <asp:Label ID="lblSexo" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                            <div class="col-sm-8 b-r">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbRacaPacienteReceita" runat="server" class="control-label text-left" Text="Raça:"></asp:Label>
                                                    <strong>
                                                        <asp:Label ID="lblRaca" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbIdadePacienteReceita" runat="server" class="control-label text-left" Text="Idade:"></asp:Label>
                                                    <strong>
                                                        <asp:Label ID="lblIdade" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h4>DADOS DO TUTOR</h4>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbNomeTutorReceita" runat="server" class="control-label text-left" Text="Tutor:"></asp:Label>
                                                <strong>
                                                    <asp:Label ID="lblTutor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                            </div>

                                            <div class="col-sm-12">
                                                <asp:Label ID="lbEMailTutorReceita" runat="server" class="control-label text-left" Text="E-Mail:"></asp:Label>
                                                <strong>
                                                    <asp:Label ID="lblEMailTutor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                            </div>

                                            <div class="col-sm-12">
                                                <asp:Label ID="lbTelefoneTutorReceita" runat="server" class="control-label text-left" Text="Telefone:"></asp:Label>
                                                <strong>
                                                    <asp:Label ID="lblFoneTutor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h5>ALIMENTOS</h5>
                                    </div>
                                    <div id="gridRepeater" class="ibox-content" style="height: 300px; overflow: auto;" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="x_content table-responsive">
                                                    <!-- start project list -->
                                                    <table class="table table-striped table-hover">
                                                        <asp:Repeater ID="rptCardapImpressao" runat="server" OnItemDataBound="rptCardapImpressao_ItemDataBound">
                                                            <HeaderTemplate>
                                                                <thead>
                                                                    <tr>
                                                                        <th style="width: 85%">
                                                                            <asp:Label ID="lblNutr" runat="server" Text="Alimento"></asp:Label>
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            <asp:Label ID="lblConsta" runat="server" Text="Quant. (g)"></asp:Label>
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblAlimento" runat="server" Text='<%# Eval("Alimento") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <asp:Label ID="lblValor" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        <asp:Label ID="lblTotal" runat="server" Text="Total&nbsp;&nbsp;&nbsp;"></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <asp:Label ID="lblTotalValor" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                    <!-- end project list -->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h5>DISTRIBUIÇÃO CALÓRICA</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <table class="auto-style1">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td colspan="2" style="border-style: solid; text-align: center; font-weight: bold;">
                                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Carboidrato"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td colspan="2" style="border-style: solid; text-align: center">
                                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Proteína"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td colspan="2" style="border-style: solid; text-align: center">
                                                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Text="Gordura"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid">
                                                            <asp:Label ID="lblCarbVal" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2" style="border-style: solid">
                                                            <asp:Label ID="lblCarbPorcent" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid">
                                                            <asp:Label ID="lblProtVal" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2" style="border-style: solid">
                                                            <asp:Label ID="lblProtPorcent" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid">
                                                            <asp:Label ID="lblGordVal" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2" style="border-style: solid">
                                                            <asp:Label ID="lblGordPorcent" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h5>DETALHES</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <table class="auto-style1">
                                                    <tr>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid; width: 150px">
                                                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Fibra"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid; width: 150px">
                                                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Text="Umidade"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid; width: 150px">
                                                            <asp:Label ID="Label14" runat="server" Font-Bold="True" Text="Energia Requerida"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid; width: 150px">
                                                            <asp:Label ID="Label15" runat="server" Font-Bold="True" Text="Energia Presente"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid">
                                                            <asp:Label ID="lblFibraVal" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid">
                                                            <asp:Label ID="lblUmidVal" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid">
                                                            <asp:Label ID="lblEnReq" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                        <td class="auto-style2" style="border-style: solid">
                                                            <asp:Label ID="lblEnPresente" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h5>OBSERVAÇÕES</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <FTB:FreeTextBox ID="ftbObservacao" runat="server" AllowHtmlMode="False"
                                                    AssemblyResourceHandlerPath="" AutoConfigure="" AutoGenerateToolbarsFromString="True"
                                                    AutoHideToolbar="True" AutoParseStyles="True" BackColor="158, 190, 245" BaseUrl=""
                                                    BreakMode="Paragraph" ButtonDownImage="False" ButtonFileExtention="gif"
                                                    ButtonFolder="Images" ButtonHeight="20" ButtonImagesLocation="InternalResource"
                                                    ButtonOverImage="False" ButtonPath="" ButtonSet="Office2003" ButtonWidth="21"
                                                    ClientSideTextChanged="" ConvertHtmlSymbolsToHtmlCodes="False"
                                                    DesignModeBodyTagCssClass="" DesignModeCss="" DisableIEBackButton="False"
                                                    DownLevelCols="50" DownLevelMessage="" DownLevelMode="TextArea" DownLevelRows="10"
                                                    EditorBorderColorDark="Gray" EditorBorderColorLight="Gray" EnableHtmlMode="False"
                                                    EnableSsl="False" EnableToolbars="True" Focus="False" FormatHtmlTagsToXhtml="True"
                                                    GutterBackColor="129, 169, 226" GutterBorderColorDark="Gray"
                                                    GutterBorderColorLight="White" Height="100px" HelperFilesParameters="" HelperFilesPath=""
                                                    HtmlModeCss="" HtmlModeDefaultsToMonoSpaceFont="True" ImageGalleryPath="~/imagens/"
                                                    ImageGalleryUrl="ftb.imagegallery.aspx?rif={0}&amp;cif={0}"
                                                    InstallationErrorMessage="InlineMessage" JavaScriptLocation="InternalResource"
                                                    Language="pt-PT" PasteMode="Default" ReadOnly="False" RemoveScriptNameFromBookmarks="True"
                                                    RemoveServerNameFromUrls="True" RenderMode="NotSet" ScriptMode="External"
                                                    ShowTagPath="False" SslUrl="/." StartMode="DesignMode" StripAllScripting="False"
                                                    SupportFolder="/aspnet_client/FreeTextBox/" TabIndex="-1" TabMode="InsertSpaces" Text=""
                                                    TextDirection="LeftToRight" ToolbarBackColor="Transparent" ToolbarBackgroundImage="True"
                                                    ToolbarImagesLocation="InternalResource" ToolbarLayout="FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList|Cut,Copy,Paste;Undo,Redo"
                                                    ToolbarStyleConfiguration="Office2003" UpdateToolbar="True" UseToolbarBackGroundImage="True"
                                                    Width="100%">
                                                </FTB:FreeTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox-content" style="margin-bottom: 5px">
                                    <div class="btn-group" role="group" style="display: flex; justify-content: flex-end">
                                        <div id="blocoAssinatura" style="display: flex; justify-content: center; flex-direction: column; align-items: center; width: 25%">
                                            <asp:Label ID="lblLocalData" runat="server" class="control-label text-right" Text="Local e Data automática"></asp:Label>
                                            <asp:Image ID="imgAssinatura" ImageUrl="~/Perfil/Assinaturas/assinatura_receita.png" alt="imagem assinatura digitalizada" class="img-fluid rounded mx-auto d-block imgAssinatura" Style="width: 100%;" runat="server" />
                                            <asp:Label ID="lblNomeVeterinario" runat="server" class="control-label text-center" Text="Nome Médico Veterinário"></asp:Label>
                                            <asp:Label ID="lblTituloECRMV" runat="server" class="control-label text-center" Text="Médico Veterinário - "></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class=" col-lg-12 text-right">
                                                <div class="btn-group" role="group">
                                                    <asp:LinkButton runat="server" ID="lbFechar" CssClass="btn btn-default" OnClick="lbFechar_Click"><i class="fas fa-door-open"></i>&nbsp;Fechar </asp:LinkButton>
                                                    <div class="btn-group dropup">
                                                        <a class="btn btn-success dropdown-toggle" data-placement="top" data-toggle="dropdown"><i class="fas fa-print"></i>&nbsp;imprimir</a>
                                                        <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                                            <li>
                                                                <asp:HyperLink ID="hlImprCardapioPdf" runat="server" title="Impressão do Cardápio em PDF" CssClass="btn btn-ligth" Target="_blank" Enabled="False"><i class="far fa-file-pdf"></i>&nbsp;PDF</asp:HyperLink>
                                                            </li>
                                                            <li>
                                                                <hr class="dropdown-divider"></hr>
                                                            </li>
                                                            <li>
                                                                <asp:HyperLink ID="hlImprCardapioWord" runat="server" title="Impressão do Cardápio em Word" CssClass="btn btn-secondary" Target="_blank" Enabled="False"><i class="far fa-file-word"></i>&nbsp;Word</asp:HyperLink>
                                                            </li>
                                                            <li>
                                                                <asp:HyperLink ID="hlImprCardapioExcel" runat="server" title="Impressão do Cardápio em Excel" CssClass="btn btn-secondary" Target="_blank" Enabled="False"><i class="far fa-file-excel"></i>&nbsp;Excel</asp:HyperLink>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <asp:LinkButton ID="lbSalvaReceita" runat="server" title="Salva as Informações do Cardápio para Impressão" CssClass="btn btn-primary-nutrovet " OnClick="lbSalvaReceita_Click" Visible="True"><i class='far fa-save' aria-hidden="true"></i>&nbsp;Salvar</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Central Modal Medium Success -->
            <div id="myModal" runat="server" class="modal-dialog modalControle" style="display: none">
                <!--Content-->
                <div class="modal-content fadeIn">
                    <!--Header-->
                    <div class="modal-header modal-header-success">
                        <div class="col-md-9 col-sm-9 col-xs-9">
                            <i class="fa fa-cogs fa-2x" aria-hidden="true"></i>
                            <asp:Label ID="lblTituloModal" runat="server" Text="Receituário - Nutracêuticos" class="heading lead"></asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-3">
                            <asp:LinkButton runat="server" ID="LinkButton8" CssClass="close" data-dismiss="modal"> <i class='fa fa-times-circle'></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body">
                        <!--Body-->
                        <asp:Panel ID="pnlDescricao" runat="server" Height="120px">
                            <div class="row">
                                <div class="col-sm-12 col-lg-12 form-group ">
                                    <asp:Label ID="lblDescricao" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-9 col-lg-9 form-group ">
                                    &nbsp;&nbsp;&nbsp;
                                        <asp:HyperLink ID="hlkPerfil" runat="server" CssClass="btn btn-primary-nutrovet" NavigateUrl="~/Perfil/Perfil.aspx">Link para a Tela de Perfil</asp:HyperLink>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <!--Footer-->
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="lbClose" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                    </div>
                </div>
                <!--/.Content-->

            </div>
            <ajaxToolkit:ModalPopupExtender ID="popUpModal" runat="server"
                PopupControlID="myModal" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblPopUp" OkControlID="lbClose" CancelControlID="lbClose">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblPopUp" runat="server" Text=""></asp:Label>
            <!-- Central Modal Medium Success-->

        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="pagina-footer navbar-fixed-bottom" role="banner">
        <div class="container">
            <div class="pull-left">
                |&nbsp;&nbsp;<a href="https://www.youtube.com/channel/UCPk1NVPuAgVPjf6eQOI5qeg?view_as=public" target="_blank"><i class="fab fa-youtube"></i></a>&nbsp;
                |&nbsp;&nbsp;<a href="https://www.facebook.com/nutrovetonline/" target="_blank" class="facebook"><i class="fab fa-facebook"></i></a>&nbsp;
                |&nbsp;<a href="https://www.instagram.com/nutrovetonline/" target="_blank" class="instagram"><i class="fab fa-instagram"></i></a>&nbsp;|
            </div>
            <div class="pull-right">
                NutroVET by <strong>RD Sistemas &copy;<asp:Label ID="lblAno" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </div>
</asp:Content>
