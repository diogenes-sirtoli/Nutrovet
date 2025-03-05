<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="CardapioBalanceamento.aspx.cs" Inherits="Nutrovet.Cardapio.CardapioBalanceamento"
    ValidateRequest="false" MaintainScrollPositionOnPostback="true" %>

<%@ OutputCache Duration="1" NoStore="true" VaryByParam="none" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <div class="col-lg-1">
                <asp:HyperLink ID="hlkMinimalize" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
            </div>
            <h2>
                <asp:Label ID="lblPagina" runat="server" Text=""></asp:Label></h2>
            <div class="col-lg-6">
                <ol class="breadcrumb">
                    <li>
                        <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="hlCardapioSelecao" NavigateUrl="~/Cardapio/CardapioSelecao.aspx" runat="server"><i class="fas fa-balance-scale"></i> Cardápios</asp:HyperLink>
                    </li>
                    <li class="active">
                        <i class="fas fa-weight"></i><strong>&nbsp;Balanceamento de Cardápios</strong>
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
                        </strong>
                    </li>

                </ol>
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
            <div class="panel-body">
                <strong>Tabelas de Exigências Nutricionais</strong>&nbsp;&nbsp;
                <asp:DropDownList ID="ddlTblExigNutr" runat="server" class="btn btn-default" AutoPostBack="True" OnSelectedIndexChanged="ddlTblExigNutr_SelectedIndexChanged"></asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
            </div>
            <div>
                <div class="x_content table-responsive">
                    <div class="panel panel-default">
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
                                <asp:Repeater ID="rptTabelasNutricionais" runat="server" OnItemDataBound="rptTabelasNutricionais_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-striped projects dataTables-example" id="table-exigencias-nutricionais-min">
                                            <thead>
                                                <tr>
                                                    <th colspan="3" style="border-style: solid solid solid solid; border-width: thin; border-color: #C6D5FF; text-align: center">Nutriente
                                                    </th>

                                                    <th colspan="3" style="border: thin solid #C6D5FF; text-align: center;">Cardápio</th>
                                                </tr>
                                                <tr>
                                                    <td style="border-style: none solid none solid; border-width: thin; border-color: #C6D5FF;">&nbsp;</td>
                                                    <td class="TableNutrSubT">Mínimo</td>
                                                    <td class="TableNutrSubT">Máximo</td>
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
                                                <asp:Label ID="lblValMin" runat="server" Text='<%# Eval("Minimo") %>'></asp:Label>
                                            </td>
                                            <td class="TableNutr">
                                                <asp:Label ID="lblValMax" runat="server" Text='<%# Eval("Maximo") %>'></asp:Label>
                                            </td>
                                            <td class="TableNutrConsta">
                                                <asp:Label ID="lblValEmCardapio" runat="server" Text='<%# Eval("EmCardapio") %>'></asp:Label>
                                            </td>
                                            <td id="idTdFaltaI" class="TableNutr">
                                                <asp:Label ID="lblValFalta" runat="server" Text='<%# Eval("Falta") %>'></asp:Label>
                                            </td>
                                            <td class="TableNutr">
                                                <asp:Label ID="lblValSobra" runat="server" Text='<%# Eval("Sobra") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                           </table>
                                    </FooterTemplate>
                                </asp:Repeater>
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
                                                <asp:Label ID="lblValMin" runat="server" Text='<%# Eval("Minimo") %>'></asp:Label>
                                            </td>
                                            <td class="TableNutr">
                                                <asp:Label ID="lblValMax" runat="server" Text='<%# Eval("Maximo") %>'></asp:Label>
                                            </td>
                                            <td class="TableNutr">
                                                <asp:Label ID="lblValAdeq" runat="server" Text='<%# Eval("Adequado") %>'></asp:Label>
                                            </td>
                                            <td class="TableNutr">
                                                <asp:Label ID="lblValRecomend" runat="server" Text='<%# Eval("Recomendado") %>'></asp:Label>
                                            </td>
                                            <td class="TableNutr">
                                                <asp:Label ID="lblValEmCardapio" runat="server" Text='<%# Eval("EmCardapio") %>'></asp:Label>
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
                        <div class="ibox ">
                            <div class="ibox-title">
                                <h5>Observações</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <%--<div class="col-lg-12">--%>
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
                                        Language="pt-PT" PasteMode="Default" ReadOnly="True" RemoveScriptNameFromBookmarks="True"
                                        RemoveServerNameFromUrls="True" RenderMode="NotSet" ScriptMode="External"
                                        ShowTagPath="False" SslUrl="/." StartMode="DesignMode" StripAllScripting="False"
                                        SupportFolder="/aspnet_client/FreeTextBox/" TabIndex="-1" TabMode="InsertSpaces" Text=""
                                        TextDirection="LeftToRight" ToolbarBackColor="Transparent" ToolbarBackgroundImage="True"
                                        ToolbarImagesLocation="InternalResource" ToolbarLayout="FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList|Cut,Copy,Paste;Undo,Redo"
                                        ToolbarStyleConfiguration="Office2003" UpdateToolbar="True" UseToolbarBackGroundImage="True"
                                        Width="100%">
                                    </FTB:FreeTextBox>
                                    <%--</div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="pull-right">
                                <asp:LinkButton ID="lbFechar" runat="server" CssClass="btn btn-default" data-dismiss="modal" OnClick="lbFecharBalCard_Click"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                <asp:HyperLink ID="lbImpressaoCardapio" runat="server" title="Impressão do Balanceamento entre dietas" CssClass="btn btn-info" Target="_blank" Enabled="False"><i class="fas fa-print"></i> imprimir</asp:HyperLink>
                                <asp:LinkButton ID="lbSalvaReceita" runat="server" title="Salva as Informações da Receita" CssClass="btn btn-info" OnClick="lbSalvaReceita_Click" Visible="True" Enabled="false"><i class='far fa-save' aria-hidden="true"></i>&nbsp;Salvar</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
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
