<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="NutrientesAuxGrupos.aspx.cs" Inherits="Nutrovet.TabAux.NutrientesAuxGrupos"
    ValidateRequest="false" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {
                $("#meOrdem").TouchSpin({
                    min: 0,
                    max: 10000,
                    step: 1.00,
                    decimals: 0,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet',
                    postfix: ''
                });
            });
        }

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

    <div class="row">
        <div class="col-lg-12">
            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="col-lg-1">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i></asp:HyperLink>
                </div>
                <h2>Grupos de Nutrientes</h2>
                <div class="col-lg-4">
                    <ol class="breadcrumb">
                        <li>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                        </li>
                        <li class="active">
                            <i class="fas fa-dog fa-flip-horizontal"></i><i class="fas fa-cat"></i><strong>&nbsp;Grupos de Nutrientes</strong>
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
            <div class="page-title">
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row wrapper border-bottom white-bg page-heading">
                            <div class="ibox-content">
                                <div class="search-form">
                                    <div class="input-group col-lg-12">
                                        <asp:TextBox ID="tbPesq" runat="server" placeholder="Pesquisar por Grupos de Nutrientes" CssClass="form-control input-md bg-muted"></asp:TextBox>
                                        <div class="input-group-btn">
                                            <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbPesq_Click"> <i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="x_content table-responsive col-lg-12">
                                <!-- start project list -->
                                <asp:Repeater ID="rptListagem" runat="server" OnItemCommand="rptListagem_ItemCommand">
                                    <HeaderTemplate>
                                        <table class="table table-striped table-hover">
                                            <thead>
                                                <tr>
                                                    <th style="width: 40%" >
                                                        <asp:Label ID="lblTGrupo" runat="server" Text="Grupo"></asp:Label>
                                                    </th>
                                                    <th style="width: 15%" class="text-center">
                                                        <asp:Label ID="lblTOrdem" runat="server" Text="Ordenamento"></asp:Label>
                                                    </th>
                                                    <th style="width: 20%" class="text-center">
                                                        <asp:Label ID="lblTLista" runat="server" Text="Em&nbsp;Alimentos"></asp:Label>
                                                    </th>
                                                    <th style="width: 15%" class="text-center">Ação
                                                        <asp:LinkButton ID="lbInserir" runat="server" CommandName="inserir" CssClass="btn btn-primary-nutrovet btn-xs" data-toggle="tooltip" data-placement="top" title="Incluir Grupo de Nutriente"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblGrupo" runat="server" Text='<%# Eval("Grupo") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblOrdem" runat="server" Text='<%# Eval("Ordem") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblListar" runat="server" Text='<%# Eval("ListarEmAlim") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="lbEditar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdGrupo") %>' data-toggle="tooltip" data-placement="top" title="Editar Grupo de Nutriente"> <i class="fas fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdGrupo") %>' TargetControlID="lbExcluir" data-toggle="tooltip" data-placement="top" title="Excluir Grupo de Nutriente"><i class="far fa-trash-alt"></i></asp:LinkButton>
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
            </div>


            <!--modal edição/alteração-->
            <div id="myModal" runat="server" style="display: none">
                <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                    <!--Content-->
                    <div class="modal-content">
                        <!--Header-->
                        <div class="modal-header modal-header-success">
                            <div class="col-md-9 col-sm-9 col-xs-9">
                                <i class="fa fa-cogs fa-2x" aria-hidden="true"></i>
                                <asp:Label ID="lblTituloModal" class="heading lead" runat="server" Text="Registro"></asp:Label>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <asp:LinkButton runat="server" ID="lbFecharModal" CssClass="close" data-dismiss="modal"> <i class='fa fa-times-circle'></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <label id="lblMGrupo" class="col-sm-5 control-label">Grupo de Nutriente</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <span class="input-group-addon" style=""><i class="fas fa-bone fa-fw"></i></span>
                                        <asp:TextBox ID="tbxGrupo" runat="server" placeholder="Grupo" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Grupo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 form-group "></div>
                                <label id="lblMOrdem" class="col-sm-5 col-lg-5 control-label">Ordem</label>
                                <div class="col-sm-5 col-lg-5 form-group">
                                    <cc1:MEdit ID="meOrdem" ClientIDMode="Static" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" title="Ordenação - Preencha a Ordem de Listagem" data-placement="top" Mascara="Inteiro"></cc1:MEdit>
                                </div>
                            </div>
                            <div class="row">
                                <label id="lblMListar" class="col-sm-5 col-lg-5 control-label">Listar no Cadastro de Alimentos?</label>
                                <div class="col-sm-5 col-lg-5 form-group ">
                                    <div class="btn-group" role="group">
                                        <asp:Button ID="btnSim" runat="server" CssClass="btn btn-default" Text="Sim" OnClick="btnSim_Click" />
                                        <asp:Button ID="btnNao" runat="server" CssClass="btn btn-primary-nutrovet" Text="Não" OnClick="btnNao_Click" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <asp:HiddenField ID="hfID" runat="server" />
                        </div>

                        <!--Footer-->
                        <div class="modal-footer ">
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

