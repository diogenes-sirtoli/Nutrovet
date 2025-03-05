<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#"
    MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true" CodeBehind="LogsSelecao.aspx.cs"
    Inherits="Nutrovet.Administracao.LogsSelecao" ValidateRequest="false" %>

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
                        <h2>&nbsp;Gerenciamento</h2>
                        <div class="col-lg-4">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <i class="fas fa-exclamation-triangle"></i><strong>&nbsp;Mensagens</strong>
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
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-3">
                                                <div class="panel panel-warning">
                                                    <b title="Filtrar por Tabelas">
                                                        <asp:Label ID="lblPesquisarTabela" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Pesquisar por Tabelas Sistema:"></asp:Label></b>
                                                    <div class="panel-heading">
                                                        <asp:DropDownList ID="ddlTabelas" runat="server" placeholder="Pesquisar por Tabelas Sistema" CssClass="form-control input-md bg-muted"></asp:DropDownList>
                                                        <hr />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-3">
                                                <div class="panel panel-info">
                                                    <b title="Filtrar por Ações">
                                                        <asp:Label ID="lblPesquisarAcoes" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Pesquisar por Ações:"></asp:Label></b>
                                                    <div class="panel-heading">
                                                        <asp:DropDownList ID="ddlAcoes" runat="server" placeholder="Pesquisar por Ações" CssClass="form-control input-md bg-muted"></asp:DropDownList>
                                                        <hr />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5">
                                                <div class="panel panel-success">
                                                    <b title="Filtrar por Período">
                                                        <asp:Label ID="lblFiltroPeriodo" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Período:"></asp:Label></b>
                                                    <div class="panel-heading">
                                                        <cc1:MEdit ID="meDtIni" ClientIDMode="Static" runat="server" placeholder="Data Inicial" CssClass="form-control" Mascara="Data"></cc1:MEdit>
                                                        <hr />
                                                    </div>
                                                    <div class="panel-heading">
                                                        <cc1:MEdit ID="meDtFim" ClientIDMode="Static" runat="server" placeholder="Data Final" CssClass="form-control" Mascara="Data"></cc1:MEdit>
                                                        <hr />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-1 col-lg-1">
                                                <div class="input-group-btn">
                                                    <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="x_content table-responsive">
                                    <asp:HiddenField ID="hfID" runat="server" />
                                    <!-- start project list -->
                                    <asp:Repeater ID="rptListagemLogs" runat="server" OnItemCommand="rptListagemMensagens_ItemCommand" OnItemDataBound="rptListagemMensagens_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="table table-striped  table-hover">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 35%">
                                                            <asp:Label ID="lblNome" runat="server" Text="Assinante"></asp:Label>
                                                        </th>
                                                        <th style="width: 20%">
                                                            <asp:Label ID="lblTabela" runat="server" Text="Tabela"></asp:Label>
                                                        </th>
                                                        <th style="width: 15%">
                                                            <asp:Label ID="lblAcao" runat="server" Text="Ação"></asp:Label>
                                                        </th>
                                                        <th style="width: 15%">
                                                            <asp:Label ID="lblDataHora" runat="server" Text="Data/Hora"></asp:Label>
                                                        </th>
                                                        <th style="text-align: center; width: 5%">
                                                            <asp:Label ID="lblMensagem" runat="server" Text="Mensagem"></asp:Label>
                                                        </th>
                                                        <th style="text-align: center; width: 10%">Ação</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNomeRegistro" runat="server" Text='<%# Eval("Assinante") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTabelaRegistro" runat="server" Text='<%# Eval("Tabela") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAcaoRegistro" runat="server" Text='<%# Eval("Acao") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDataHoraRegistro" runat="server" Text='<%# Eval("Datahora") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbVisualizar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="visualizar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdLog") %>' ToolTip="Visualizar Log"><i class="far fa-eye"></i></asp:LinkButton>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdLog") %>'><i class="far fa-trash-alt" data-toggle="tooltip" data-placement="top" title="Excluir Log"></i></asp:LinkButton>
                                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Deseja Realmente Excluir Este Registro?" TargetControlID="lbExcluir" />
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
                                <div class="clearfix"></div>
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <div class="row wrapper border-bottom white-bg page-heading">
                                            <div class="hr-line"></div>
                                            <div class="col-lg-2 form-group ">
                                                <h5>Registros por página </h5>
                                            </div>
                                            <div class="col-lg-1 form-group ">
                                                <asp:DropDownList ID="ddlPag" runat="server" CssClass="form-control btn-default" AutoPostBack="True" Style="width: 70px" OnSelectedIndexChanged="ddlPag_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-9 text-right">
                                                <asp:LinkButton runat="server" ID="lbPagPrimeira" CssClass="btn btn-default" ToolTip="Primeira" OnClick="lbPagPrimeira_Click"> <i class='fa fa-backward'></i> </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbAnt" CssClass="btn btn-default" Visible="False" OnClick="lbAnt_Click" ToolTip="Anterior"><i class='fa fa-step-backward'></i></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lb1" CssClass="btn btn-default" CommandName="1" OnClick="lb1_Click"> <b><u>1</u></b> </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lb2" CssClass="btn btn-default" CommandName="2" OnClick="lb2_Click"> 2 </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lb3" CssClass="btn btn-default" CommandName="3" OnClick="lb3_Click"> 3 </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lb4" CssClass="btn btn-default" CommandName="4" OnClick="lb4_Click"> 4 </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lb5" CssClass="btn btn-default" CommandName="5" OnClick="lb5_Click"> 5 </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbPost" CssClass="btn btn-default" ToolTip="Próxima" OnClick="lbPost_Click"><i class='fa fa-step-forward'></i></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbPagUltima" CssClass="btn btn-default" ToolTip="Última" OnClick="lbPagUltima_Click"> <i class='fa fa-forward'></i> </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
                        <!-- Modal Visualizar Mensagem -->
            <div id="modalMsg" runat="server" style="display: none">

                <div class="modal-dialog modal-notify modal-default modal-dialog-centered" role="document">
                    <!--Content-->
                    <div class="modal-content">
                        <!--Header-->
                        <div class="modal-header-nutro modal-header-default">
                            <asp:Label ID="lblTituloModal" class="heading lead" runat="server" Text="<i class='fas fa-info-circle'></i>&nbsp;Informação do Sistema"></asp:Label>
                            <asp:LinkButton runat="server" ID="lbFecharModal" CssClass="close" data-dismiss="modal"> <i class='fa fa-times-circle'></i></asp:LinkButton>
                        </div>
                        <div class="modal-nutro">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </div>
                        <!--Footer-->
                        <div class="modal-footer-nutro">
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
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblPopUp">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblPopUp" runat="server" Text=""></asp:Label>
            <!-- Modal Visualizar Mensagem -->

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
                NutroVET by <strong>SICORP &copy;<asp:Label ID="lblAno" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </div>
</asp:Content>
