<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master"
    AutoEventWireup="true" CodeBehind="MensagensPortalSelecao.aspx.cs"
    Inherits="Nutrovet.Administracao.MensagensPortalSelecao" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
                        <h2>&nbsp;Gerenciamento</h2>
                        <div class="col-lg-4">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <i class="fas fa-comments"></i><strong>&nbsp;Mensagens do Portal</strong>
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
            <div>
                <div class="page-title">
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row wrapper border-bottom white-bg page-heading">
                                <div class="ibox-content">
                                    <div class="search-form">
                                        <div class="input-group col-lg-12">
                                            <asp:TextBox ID="tbPesq" runat="server" placeholder="Pesquisar por Tutor" CssClass="form-control input-md bg-muted"></asp:TextBox>
                                            <div class="input-group-btn">
                                                <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-md btn-primary-nutrovet"><i class="fa fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="gridRepeater" class="ibox-content" style="height: 600px; overflow: auto;" runat="server">
                                    <div class="x_content table-responsive">
                                        <asp:HiddenField ID="hfID" runat="server" />
                                        <!-- start project list -->
                                        <asp:Repeater ID="rptListagemMensagens" runat="server" OnItemCommand="rptListagemMensagens_ItemCommand" OnItemDataBound="rptListagemMensagens_ItemDataBound">
                                            <HeaderTemplate>
                                                <table class="table table-striped  table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 20%">
                                                                <asp:Label ID="lblNome" runat="server" Text="Contato"></asp:Label>
                                                            </th>
                                                            <th style="width: 20%">
                                                                <asp:Label ID="lblEmail" runat="server" Text="E-mail"></asp:Label>
                                                            </th>
                                                            <th style="width: 20%">
                                                                <asp:Label ID="lblAssunto" runat="server" Text="Assunto"></asp:Label>
                                                            </th>
                                                            <th style="width: 8%">
                                                                <asp:Label ID="lblDtEnvio" runat="server" Text="Enviada"></asp:Label>
                                                            </th>
                                                            <th style="width: 4%">
                                                                <asp:Label ID="lblSituacao" runat="server" Text="Situação"></asp:Label>
                                                            </th>
                                                            <th style="text-align: center; width: 4%">
                                                                <asp:Label ID="lblMensagem" runat="server" Text="Mensagem"></asp:Label>
                                                            </th>
                                                            <th style="text-align: center; width: 15%">Ação</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNomeRegistro" runat="server" Text='<%# Eval("NomeContato") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEmailRegistro" runat="server" Text='<%# Eval("EmailContato") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAssuntoRegistro" runat="server" Text='<%# Eval("Assunto") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEnvioRegistro" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td>
                                                        <div class="badge badge-success" id="divSituacao" runat="server" style="width: 70px;">
                                                            <asp:Label ID="lblSituacaoRegistro" runat="server" Text='<%# Eval("Situacao") %>'></asp:Label>
                                                        </div>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:LinkButton ID="lbVisualizar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="visualizar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdContato") %>' ToolTip="Visualizar Mensagem"><i class="far fa-eye"></i></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:LinkButton ID="lbResponder" runat="server" CssClass="btn btn-info btn-xs" CommandName="responder" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdContato") %>' ToolTip="Enviar Mensagem-Resposta"><i class="fas fa-file-export"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbArquivar" runat="server" CssClass="btn btn-info btn-xs" CommandName="arquivar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdContato") %>' ToolTip="Arquivar Mensagem"><i class="fas fa-archive"></i></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                    </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <!-- end project list -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <!-- Modal Visualizar Mensagem -->
            <div id="modalMsg" runat="server" style="display: none">
                <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                    <!--Content-->
                    <div class="modal-content">
                        <!--Header-->
                        <div class="modal-header modal-header-success">
                            <div class="col-md-9 col-sm-9 col-xs-9">
                                <i class="fa fa fa-file-text fa-2x" aria-hidden="true"></i>
                                <asp:Label ID="lblTituloModal" class="heading lead" runat="server" Text="Mensagem"></asp:Label>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <asp:LinkButton runat="server" ID="lbFecharModal" CssClass="close" data-dismiss="modal" ForeColor="White"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-body">
                            <table class=" table table-striped table-hover table-sm">
                                <tbody>
                                    <tr>
                                        <td>
                                            <div>
                                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </div>

                        <!--Footer-->
                        <div class="modal-footer ">
                            <div class="btn-group" role="group">
                                <asp:LinkButton runat="server" ID="btnFechar" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!--/.Content-->
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="popUpModal" runat="server"
                PopupControlID="modalMsg" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize" CancelControlID="btnFechar"
                TargetControlID="lblPopUp">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblPopUp" runat="server" Text=""></asp:Label>
            <!-- Modal Visualizar Mensagem -->

            <!-- Modal Visualizar Mensagem -->
            <div id="modalEnvaiEmail" runat="server" style="display: none">
                <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                    <!--Content-->
                    <div class="modal-content">
                        <!--Header-->
                        <div class="modal-header modal-header-success">
                            <div class="col-md-9 col-sm-9 col-xs-9">
                                <i class="fa fa fa-file-text fa-2x" aria-hidden="true"></i>
                                <asp:Label ID="Label1" class="heading lead" runat="server" Text="Envio de Mensagem"></asp:Label>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <asp:LinkButton runat="server" ID="lbClose" CssClass="close" data-dismiss="modal" ForeColor="White"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-body">
                            <table class=" table table-striped table-hover table-sm">
                                <tbody>
                                    <tr>
                                        <th style="width: 20%">
                                            <asp:Label ID="Label4" runat="server" Text="De: "></asp:Label>
                                        </th>
                                        <th style="width: 70%">
                                            <asp:Label ID="lblDe" runat="server" Text=""></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th style="width: 20%">
                                            <asp:Label ID="Label2" runat="server" Text="Para: "></asp:Label>
                                        </th>
                                        <th style="width: 70%">
                                            <asp:Label ID="lblPara" runat="server" Text=""></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th style="width: 20%">
                                            <asp:Label ID="Label3" runat="server" Text="Assunto: "></asp:Label>
                                        </th>
                                        <th style="width: 70%">
                                            <asp:TextBox ID="tbxAssunto" runat="server" CssClass="form-control" placeholder="Assunto" required="true"></asp:TextBox>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th style="width: 20%">
                                            <asp:Label ID="Label5" runat="server" Text="Mensagem: "></asp:Label>
                                        </th>
                                        <th style="width: 70%">
                                            <asp:TextBox ID="tbxMsg" runat="server" CssClass="form-control" Rows="8" placeholder="Mensagem" required="true" TextMode="MultiLine"></asp:TextBox>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                        </div>

                        <!--Footer-->
                        <div class="modal-footer ">
                            <div class="btn-group" role="group">
                                <asp:LinkButton runat="server" ID="lbFechar" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lbEnviar" CssClass="btn btn-primary-nutrovet" OnClick="lbEnviar_Click"><i class='far fa-envelope'></i> Enviar </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!--/.Content-->
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="popupEnviaEmail" runat="server"
                PopupControlID="modalEnvaiEmail" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize" CancelControlID="lbFechar"
                TargetControlID="lblEnvaiEmail">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblEnvaiEmail" runat="server" Text=""></asp:Label>
            <!-- Modal Visualizar Mensagem -->
            <asp:HiddenField ID="hdfIdMsg" runat="server" />
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
