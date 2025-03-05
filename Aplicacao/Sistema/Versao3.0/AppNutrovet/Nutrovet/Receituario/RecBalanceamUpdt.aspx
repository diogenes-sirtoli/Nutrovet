<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="RecBalanceamUpdt.aspx.cs" Inherits="Nutrovet.Receituario.RecBalanceamUpdt"
    ValidateRequest="false" MaintainScrollPositionOnPostback="true" %>

<%@ OutputCache Duration="1" NoStore="true" VaryByParam="none" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="">
        <div class="page-title">
            <div class="clearfix"></div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="row wrapper border-bottom white-bg page-heading">
                        <div class="col-lg-1">
                            <asp:HyperLink ID="HyperLink12" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
                        </div>
                        <h2>&nbsp;Receituário</h2>
                        <div class="col-lg-4">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlMenuGeral" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <asp:HyperLink ID="hlCardapio" NavigateUrl="~/Receituario/ReceituarioSelecao.aspx" runat="server"><i class="fas fa-book-medical fa-lg" aria-hidden="true"></i> Receituário</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <strong>
                                        <i class="fas fa-file-medical fa-lg" aria-hidden="true"></i>
                                        <asp:Label ID="Label1" runat="server" Text=" Suplementação"></asp:Label>
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

            <script type="text/javascript">
                Sys.Application.add_load(BindEvents);
            </script>

            <asp:UpdateProgress ID="UpdateProgress" class="divProgress" runat="server" DisplayAfter="2000">
                <ProgressTemplate>
                    <div class="semipolar-spinner">
                        <div class="ring"></div>
                        <div class="ring"></div>
                        <div class="ring"></div>
                        <div class="ring"></div>
                        <div class="ring"></div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <div class="row wrapper border-bottom white-bg page-heading">
                <div>
                    <div class="ibox-title">
                        <h5>
                            <asp:Label ID="lblSubTitulo" runat="server" Text="Receituário de Balanceamento de Dietas"></asp:Label>
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
                                                <asp:Image ID="imgLogo" ImageUrl="~/Perfil/Logotipos/logo_receita.png" alt="logotipo" runat="server" Style="width: 40%;" />
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
                                                    <asp:Label ID="lblPaciente" runat="server" class="control-label text-left" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbPesoPacienteReceita" runat="server" class="control-label text-left" Text="Peso:"></asp:Label>
                                                    <asp:Label ID="lblPeso" runat="server" class="control-label text-left" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-8 b-r">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbEspeciePacienteReceita" runat="server" class="control-label text-left" Text="Espécie:"></asp:Label>
                                                    <asp:Label ID="lblEspecie" runat="server" class="control-label text-left" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbSexoPacienteReceita" runat="server" class="control-label text-left" Text="Sexo:"></asp:Label>
                                                    <asp:Label ID="lblSexo" runat="server" class="control-label text-left" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-8 b-r">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lbRacaPacienteReceita" runat="server" class="control-label text-left" Text="Raça:"></asp:Label>
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblRaca" runat="server" class="control-label text-left" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lbIdadePacienteReceita" runat="server" class="control-label text-left" Text="Idade:"></asp:Label>
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblIdade" runat="server" class="control-label text-left" Text="Label"></asp:Label>
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
                                                <asp:Label ID="lblTutor" runat="server" class="control-label text-left" Text="Label"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbEMailTutorReceita" runat="server" class="control-label text-left" Text="E-Mail:"></asp:Label>
                                                <asp:Label ID="lblEMailTutor" runat="server" class="control-label text-left" Text="Label"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbTelefoneTutorReceita" runat="server" class="control-label text-left" Text="Telefone:"></asp:Label>
                                                <asp:Label ID="lblFoneTutor" runat="server" class="control-label text-left" Text="Label"></asp:Label>
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
                                        <h5>Prescrição - Exigências Nutricionais:&nbsp;</h5>
                                        <asp:Label ID="lblTblExigNutr" runat="server" Text="Label" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                    </div>
                                    <div id="gridRepeater" class="ibox-content" style="overflow: auto;" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="x_content table-responsive">
                                                    <!-- start project list -->
                                                    <ajaxToolkit:Accordion ID="acGrupos" runat="server" FadeTransitions="false"
                                                        FramesPerSecond="40" RequireOpenedPane="False" SuppressHeaderPostbacks="True"
                                                        TransitionDuration="200" OnItemDataBound="acGrupos_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <div class="panel-heading">
                                                                <asp:HyperLink ID="hlcollapse" NavigateUrl="#" runat="server">
                                                                    <div class="pull-left">
                                                                        <i class="fas fa-folder-open" style="color: #00ba97"></i>
                                                                    </div>
                                                                    <h4 class="panel-title ">
                                                                        <asp:HiddenField ID="hfExigNutr" runat="server" Value='<%# Eval("IdGrupo") %>' />
                                                                        &nbsp;
                                                                        <asp:Label ID="lblGrupo" Style="color: #2c3f51" runat="server" Text='<%# Eval("Grupo") %>'></asp:Label>
                                                                    </h4>
                                                                </asp:HyperLink>
                                                            </div>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <asp:Repeater ID="rptTblNutr_II" runat="server" OnItemDataBound="rptTblNutr_II_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <table class="table table-striped projects dataTables-example" id="table-exigencias-nutricionais">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="border-style: solid solid solid solid; border-width: thin; border-color: #C6D5FF; text-align: center;">Nutriente&nbsp;
                                                                                </th>
                                                                                <th colspan="4" style="border: thin solid #C6D5FF; text-align: center;">
                                                                                    <asp:Label ID="lblEnergia" runat="server" Text="xxx" CssClass="badge badge-primary "></asp:Label>
                                                                                </th>
                                                                                <th colspan="3" style="border: thin solid #C6D5FF; text-align: center;">Cardápio</th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border-style: none solid none solid; border-width: thin; border-color: #C6D5FF;">&nbsp;</td>
                                                                                <td class="TableNutrSubT">Mínimo</td>
                                                                                <td class="TableNutrSubT">Máximo</td>
                                                                                <td class="TableNutrSubT">Adequado</td>
                                                                                <td class="TableNutrSubT">Recomendado</td>
                                                                                <td class="TableNutrSubT">Consta</td>
                                                                                <td class="TableNutrSubT">Falta</td>
                                                                                <td class="TableNutrSubT">Sobra</td>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="TableNutrLeft">
                                                                            <asp:Label ID="lblNutriente" runat="server" Text='<%# Eval("Nutriente") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="TableNutr">
                                                                            <asp:Label ID="lblValMin" runat="server" Text='<%# Eval("DoseMin") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="TableNutr">
                                                                            <asp:Label ID="lblValMax" runat="server" Text='<%# Eval("DoseMax") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="TableNutr">
                                                                            <asp:Label ID="lblValAdeq" runat="server" Text='<%# Eval("Adequado") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="TableNutr">
                                                                            <asp:Label ID="lblValRecomend" runat="server" Text='<%# Eval("Recomendado") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="TableNutr">
                                                                            <asp:Label ID="lblValEmCardapio" runat="server" Text='<%# Eval("Consta") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="TableNutr">
                                                                            <asp:Label ID="lblValFalta" runat="server" Text='<%# Eval("Falta") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="TableNutr">
                                                                            <asp:Label ID="lblValSobra" runat="server" Text='<%# Eval("Sobra") %>'></asp:Label>
                                                                        </td>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </tbody>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:Accordion>
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
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="ibox-title">
                                                <h5>Observações</h5>
                                            </div>
                                            <div class="col-sm-12">
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
                                            <div class="pagina-footer col-lg-12 text-right">
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

                                                    <asp:LinkButton ID="lbSalvaReceSupl" runat="server" title="Salva as Informações da Receita de Suplementação" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvaReceita_Click" Visible="True"><i class='far fa-save' aria-hidden="true"></i>&nbsp;Salvar</asp:LinkButton>
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
            <div id="myModal" runat="server" style="display: none">
                <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                    <!--Content-->
                    <div class="modal-content">
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
                                    <asp:HyperLink ID="hlkPerfil" runat="server" NavigateUrl="~/Perfil/Perfil.aspx">Link para a Tela de Perfil</asp:HyperLink>
                                </div>
                            </div>
                        </asp:Panel>

                        <!--Footer-->
                        <div class="modal-footer justify-content-center">
                            <div class="btn-group" role="group">
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="btnFechar" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!--/.Content-->
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="popUpModal" runat="server"
                PopupControlID="myModal" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblPopUp" OkControlID="btnFechar" CancelControlID="btnFechar">
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
