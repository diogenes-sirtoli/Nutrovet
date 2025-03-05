<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="Tela3.aspx.cs" Inherits="Nutrovet.TabAux.Tela3" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

    <script type="text/javascript">

        function BindEvents() {
            $(document).ready(function () {
                var x = document.getElementById('cphBody_tbCampo');
                if (window.getComputedStyle(x).visibility !== 'hidden') {
                    document.getElementById('cphBody_tbCampo').focus();
                }
            });
        }

        $('body').on('keydown', 'input, select, textarea', function(e) {
            var self = $(this)
              , form = self.parents('form:eq(0)')
              , focusable
              , next
              ;
            if (e.keyCode == 13) {
                focusable = form.find('input,a,select,button,textarea').filter(':visible');
                next = focusable.eq(focusable.index(this)+1);
                if (next.length) {
                    next.focus();
                } else {
                    form.submit();
                }
                return false;
            }
        });

        /*function TABEnter(oEvent) {
            var oEvent = (oEvent) ? oEvent : event;
            var elementComFoco = '';
            var txtControl = '';
            if (oEvent.keyCode == 13) {
                switch (oEvent.target.id) {
                    case 'cphBody_tbCampo':
                        if($("#cphBody_tbSigla").is(":visible")) {
                            txtControl = document.getElementById('cphBody_tbSigla');
                        } else {
                            txtControl = document.getElementById('cphBody_btnSalvar');
                        }
                        break;
                    case 'cphBody_tbSigla':
                        txtControl = document.getElementById('cphBody_btnSalvar');
                        break;
                }
                txtControl.focus();
            }
        }*/
    </script>

    <div class="row">
        <div class="col-lg-12">
            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="col-lg-1">
                    <asp:HyperLink ID="hlMinimize" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
                </div>
                <h2>
                    <asp:Label ID="lblTitulo" runat="server" Text="Título"></asp:Label></h2>
                <div class="col-lg-6">
                    <ol class="breadcrumb">
                        <li>
                            <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                        </li>
                        <li class="active">
                            <strong>
                                <asp:Label ID="lblTituloBread" runat="server" Text="Título"></asp:Label>
                            </strong>
                        </li>
                    </ol>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(BindEvents);
            </script>
            <div class="clearfix"></div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="row wrapper border-bottom white-bg page-heading">
                        <div class="ibox-content">
                            <div class="search-form">
                                <div class="input-group  col-lg-12">
                                    <asp:TextBox ID="tbPesq" runat="server" placeholder="Pesquisar por" CssClass="form-control input-md bg-muted"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-md btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="x_content table-responsive">
                            <!-- start project list -->
                            <asp:Repeater ID="rptListagem" runat="server" OnItemCommand="rptListagem_ItemCommand" OnItemDataBound="rptListagem_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="table table-striped projects dataTables-example">
                                        <thead>
                                            <tr>
                                                <th style="width: 50%">
                                                    <asp:Label ID="lblCampo" runat="server" Text="Nome..."></asp:Label>
                                                </th>
                                                <th style="width: 10%">
                                                    <asp:Label ID="lblSiglaTitulo" runat="server" Text="Sigla..."></asp:Label>
                                                </th>
                                                <th style="width: 15%" class="text-center">Ação
                                                    <asp:LinkButton ID="lbInserir" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="inserir" data-toggle="tooltip" data-placement="top" title="Incluir"><i class="fas fa-plus-square"></i></asp:LinkButton>
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
                                        <td class="text-center">
                                            <asp:Label ID="lblSigla" runat="server" Text='<%# Eval("Sigla") %>'></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lbEditar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' data-toggle="tooltip" data-placement="top" title="Editar"><i class="fas fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' TargetControlID="lbExcluir" data-toggle="tooltip" data-placement="top" title="Excluir"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Deseja Realmente Excluir Este Registro?" TargetControlID="lbExcluir" />
                                        </td>
                                    </tr>
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
            <div class="">
                <div class="page-title">
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="row wrapper border-bottom white-bg page-heading">
                                    <!--<div class="hr-line"></div>-->
                                    <div class="col-lg-2 form-group ">
                                        <h5>Registros por página </h5>
                                    </div>
                                    <div class="col-lg-1 form-group ">
                                        <asp:DropDownList ID="ddlPag" runat="server" CssClass="btn btn-default" AutoPostBack="True" OnSelectedIndexChanged="ddlPag_SelectedIndexChanged" Style="width: 70px">
                                            <asp:ListItem Selected="True">10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-9 form-group text-right">
                                        <asp:LinkButton runat="server" ID="lbPagPrimeira" CssClass="btn btn-default" ToolTip="Primeira" OnClick="lbPagPrimeira_Click"> <i class='fa fa-backward'></i> </asp:LinkButton>
                                        <%--<asp:LinkButton runat="server" ID="lbPagAnterior" CssClass="btn btn-default"  OnClick="lbPagAnterior_Click"> <i class='fa fa-step-backward'></i> </asp:LinkButton>--%>
                                        <asp:LinkButton runat="server" ID="lbAnt" CssClass="btn btn-default" Visible="False" OnClick="lbAnt_Click" ToolTip="Anterior"><i class='fa fa-step-backward'></i></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lb1" CssClass="btn btn-default" CommandName="1" OnClick="lb1_Click"> <b><u>1</u></b> </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lb2" CssClass="btn btn-default" CommandName="2" OnClick="lb2_Click"> 2 </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lb3" CssClass="btn btn-default" CommandName="3" OnClick="lb3_Click"> 3 </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lb4" CssClass="btn btn-default" CommandName="4" OnClick="lb4_Click"> 4 </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lb5" CssClass="btn btn-default" CommandName="5" OnClick="lb5_Click"> 5 </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbPost" CssClass="btn btn-default" ToolTip="Próxima" OnClick="lbPost_Click"><i class='fa fa-step-forward'></i></asp:LinkButton>
                                        <%--<asp:LinkButton runat="server" ID="lbPagProxima" CssClass="btn btn-default">  </asp:LinkButton>--%>
                                        <asp:LinkButton runat="server" ID="lbPagUltima" CssClass="btn btn-default" ToolTip="Última" OnClick="lbPagUltima_Click"> <i class='fa fa-forward'></i> </asp:LinkButton>
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
                                <asp:Label ID="lblTituloModal" runat="server" Text="Editando Registro" class="heading lead"></asp:Label>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <asp:LinkButton runat="server" ID="LinkButton8" CssClass="close" data-dismiss="modal"> <i class='fa fa-times-circle'></i></asp:LinkButton>
                            </div>
                        </div>

                        <!--Body-->
                        <div class="modal-body">
                            <div class="row">
                                <label id="lblDescricaoModal" class="col-sm-3 col-lg-3 control-label">Descrição</label>
                                <div class="col-sm-9 col-lg-9 form-group ">
                                    <asp:TextBox ID="tbCampo" runat="server" placeholder="Descrição" CssClass="form-control" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                </div>

                                <div id="mostraSigla" runat="server" visible="false">

                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                    <strong>
                                        <asp:Label ID="lblSiglaModal" runat="server" class="col-lg-3 control-label" Text="Sigla"></asp:Label>
                                    </strong>
                                    <div class="col-lg-9 form-group ">
                                        <asp:TextBox ID="tbSigla" runat="server" placeholder="Sigla" CssClass="form-control" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfID" runat="server" />
                            </div>
                        </div>
                        <!--Footer-->
                        <div class="modal-footer justify-content-center">
                            <div class="btn-group" role="group">
                                <asp:LinkButton runat="server" ID="btnFechar" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSalvar" CssClass="btn btn-primary-nutrovet" OnClick="btnSalvar_Click"> <i class='far fa-save'></i> Salvar </asp:LinkButton>
                            </div>
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
