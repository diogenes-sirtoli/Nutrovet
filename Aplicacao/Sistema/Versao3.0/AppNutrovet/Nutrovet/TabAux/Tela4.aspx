<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" 
    AutoEventWireup="true" CodeBehind="Tela4.aspx.cs" 
    Inherits="Nutrovet.TabAux.Tela4" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="">
                <div class="page-title">
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="row wrapper border-bottom white-bg page-heading">
                                    <div class="col-lg-1">
                                        <asp:HyperLink ID="hlMinimize" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
                                    </div>
                                    <h2>
                                        <asp:Label ID="lblTitulo" runat="server" Text="Título"></asp:Label></h2>
                                    <div class="col-lg-4">
                                        <ol class="breadcrumb">
                                            <li>
                                                <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server">Início</asp:HyperLink>
                                            </li>
                                            <li class="active">
                                                <strong>
                                                    <asp:Label ID="lblTituloBread" runat="server" Text="Título"></asp:Label></strong>
                                            </li>
                                        </ol>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="search-form">
                                            <div class="input-group">
                                                <asp:TextBox ID="tbPesquisar" runat="server" placeholder="Pesquisar por" CssClass="form-control input-md"></asp:TextBox>
                                                <div class="input-group-btn">
                                                    <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-md btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
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
            <div class="row">
                <div class="col-md-12">
                    <div class="x_panel">
                        <div class="x_content table-responsive">
                            <!-- start project list -->
                            <asp:Repeater ID="rptListagem" runat="server" OnItemCommand="rptListagem_ItemCommand" OnItemDataBound="rptListagem_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="table table-striped projects dataTables-example">
                                        <thead>
                                            <tr>
                                                <th style="width: 65%">
                                                    <asp:Label ID="lblCampo" runat="server" Text="Nome..."></asp:Label>
                                                </th>
                                                <th style="width: 20%">
                                                    <asp:Label ID="lblSiglaTitulo" runat="server" Text="Sigla..."></asp:Label>
                                                </th>
                                                <th style="width: 5%">Ação</th>
                                                <th>
                                                    <asp:LinkButton ID="lbInserir" runat="server" CssClass="btn btn-success btn-xs" CommandName="inserir"><i class="fa fa-plus-circle"></i> Inserir </asp:LinkButton>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNome" runat="server" Text='<%# Eval("Nome") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSigla" runat="server" Text='<%# Eval("Sigla") %>'></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:LinkButton ID="lbEditar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'><i class="fa fa-pencil"></i> Editar </asp:LinkButton>
                                            <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' TargetControlID="lbExcluir"><i class="fa fa-trash-o"></i> Excluir </asp:LinkButton>
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
                        <div class="">
                            <div class="page-title">
                                <div class="clearfix"></div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">
                                            <div class="row wrapper border-bottom white-bg page-heading">
                                                <div class="col-md-4 col-sm-4 col-lg-2 col-xs-2 form-group ">
                                                    <h5>Registros por página </h5>
                                                </div>
                                                <div class="col-md-4 col-sm-4 col-lg-2 col-xs-2 ">
                                                    <asp:DropDownList ID="ddlPag" runat="server" CssClass="btn btn-white form-control">
                                                        <asp:ListItem Selected="True">10</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-6 col-sm-6 col-lg-8 col-xs-8 text-right">
                                                    <asp:LinkButton runat="server" ID="lbPagPrimeira" CssClass="btn btn-white" ToolTip="Primeira"> <i class='fa fa-backward'></i> </asp:LinkButton>
                                                    <%--<asp:LinkButton runat="server" ID="lbPagAnterior" CssClass="btn btn-white"  OnClick="lbPagAnterior_Click"> <i class='fa fa-step-backward'></i> </asp:LinkButton>--%>
                                                    <asp:LinkButton runat="server" ID="lbAnt" CssClass="btn btn-white" Visible="False" ToolTip="Anterior"><i class='fa fa-step-backward'></i></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lb1" CssClass="btn btn-white" CommandName="1"> <b><u>1</u></b> </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lb2" CssClass="btn btn-white" CommandName="2"> 2 </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lb3" CssClass="btn btn-white" CommandName="3"> 3 </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lb4" CssClass="btn btn-white" CommandName="4"> 4 </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lb5" CssClass="btn btn-white" CommandName="5"> 5 </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lbPost" CssClass="btn btn-white" ToolTip="Próxima"><i class='fa fa-step-forward'></i></asp:LinkButton>
                                                    <%--<asp:LinkButton runat="server" ID="lbPagProxima" CssClass="btn btn-white">  </asp:LinkButton>--%>
                                                    <asp:LinkButton runat="server" ID="lbPagUltima" CssClass="btn btn-white" ToolTip="Última"> <i class='fa fa-forward'></i> </asp:LinkButton>
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
                        <div class="modal-header">
                            <div class="col-md-9 col-sm-9 col-xs-9">
                                <p class="heading lead">Editando Registro</p>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <asp:LinkButton runat="server" ID="lbDismissModal" CssClass="close" data-dismiss="modal"> <i class='fa fa-times-circle'></i></asp:LinkButton>
                            </div>
                        </div>

                        <!--Body-->
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group ">
                                    <asp:DropDownList ID="ddlLista" runat="server" CssClass="btn btn-white form-control">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="tbCampo" runat="server" placeholder="Descrição" CssClass="form-control" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                    <br />
                                    <asp:TextBox ID="tbSigla" runat="server" placeholder="Sigla" CssClass="form-control" data-toggle="tooltip" data-placement="top" Visible="False"></asp:TextBox>
                                    <asp:HiddenField ID="hfID" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!--Footer-->
                        <div class="modal-footer justify-content-center">
                            <asp:LinkButton runat="server" ID="btnCancelar" CssClass="btn btn-warning" data-dismiss="modal"> <i class='fa fa-times-circle'></i>Cancelar </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnSalvar" CssClass="btn btn-success" OnClick="btnSalvar_Click"> <i class='fa fa-save'></i>Salvar </asp:LinkButton>
                        </div>
                    </div>
                    <!--/.Content-->
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="popUpModal" runat="server"
                PopupControlID="myModal" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblPopUp">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblPopUp" runat="server" Text=""></asp:Label>
            <!-- Central Modal Medium Success-->
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="pagina-footer navbar-fixed-bottom" role="banner">
        <div class="container">
            <div class="pull-right">
                NutroVET by 
                  <strong>RD Sistemas &copy;
                      <asp:Label ID="lblAno" runat="server" Text=""></asp:Label>
                  </strong>
            </div>
        </div>
    </div>
</asp:Content>
