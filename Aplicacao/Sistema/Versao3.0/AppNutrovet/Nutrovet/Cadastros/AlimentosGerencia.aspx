<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="AlimentosGerencia.aspx.cs" Inherits="Nutrovet.Cadastros.AlimentosGerencia"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        $('body').on('keydown', 'input, select, textarea', function (e) {
            var self = $(this)
                , form = self.parents('form:eq(0)')
                , focusable
                , next
                ;
            if (e.keyCode == 13) {
                focusable = form.find('input, a, select, button, textarea').filter(':visible');
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
    <div>
        <div class="page-title">
            <div class="clearfix"></div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="row wrapper border-bottom white-bg page-heading">
                        <div class="col-lg-1">
                            <asp:hyperlink id="HyperLink1" navigateurl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i></asp:hyperlink>
                        </div>
                        <h2>&nbsp;Gerenciamento</h2>
                        <div class="col-lg-4">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:hyperlink id="HyperLink2" navigateurl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:hyperlink>
                                </li>
                                <li class="active">
                                    <i class="fas fa-utensils"></i>&nbsp;<strong>&nbsp;Alimentos</strong>
                                </li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:updatepanel id="UpdatePanel1" runat="server">
        <ContentTemplate>

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
            <div>
                <div class="page-title">
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row wrapper border-bottom white-bg page-heading">
                                <div class="ibox-content">
                                    <div class="search-form">
                                        <div class="input-group col-lg-12">
                                            <asp:TextBox ID="tbPesq" runat="server" placeholder="Pesquisar por Alimento, Grupo, Fonte ou Categoria" CssClass="form-control input-md bg-muted" OnTextChanged="tbPesq_TextChanged"></asp:TextBox>
                                            <div class="input-group-btn">
                                            </div>
                                            <%--<asp:TextBox ID="tbPesqNutriente" runat="server" placeholder="Agregar Nutriente na Pesquisa" CssClass="form-control input-md bg-muted" OnTextChanged="tbPesq_TextChanged"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlNutr" runat="server" placeholder="Agregar Nutriente na Pesquisa" CssClass="form-control input-md bg-muted"></asp:DropDownList>
                                            <div class="input-group-btn">
                                                <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="x_content table-responsive">
                                    <!-- start project list -->
                                    <asp:Repeater ID="rptListagem" runat="server" OnItemCommand="rptListagem_ItemCommand" OnItemDataBound="rptListagem_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="table table-striped projects table-hover dataTables-alimentos">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 25%">
                                                            <asp:Label ID="lblUsuario" runat="server" Text="Usuário"></asp:Label>
                                                        </th>
                                                        <th style="width: 15%">
                                                            <asp:Label ID="lblFonte" runat="server" Text="Fonte"></asp:Label>
                                                        </th>
                                                        <th style="width: 20%">
                                                            <asp:Label ID="lblGrupo" runat="server" Text="Grupo"></asp:Label>
                                                        </th>
                                                        <th style="width: 20%">
                                                            <asp:Label ID="lblCampo" runat="server" Text="Alimento"></asp:Label>
                                                        </th>
                                                        <th style="width: 5%">
                                                            <asp:Label ID="lblShared" runat="server" Text="Compartilhado"></asp:Label>
                                                        </th>
                                                        <th style="width: 5%">
                                                            <asp:Label ID="lblSituacao" runat="server" Text="Situação"></asp:Label>
                                                        </th>
                                                        <th style="width: 20%" class="text-center">Ação&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbInserir" runat="server" CommandName="inserir" CssClass="btn btn-primary-nutrovet btn-xs" data-toggle="tooltip" data-placement="top" title="Incluir Alimento"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="gradeA">
                                                <td>
                                                    <asp:Label ID="lblUsuarioRegistro" runat="server" Text='<%# Eval("Pessoa") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblfonteRegistro" runat="server" Text='<%# Eval("Fonte") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGrupoRegistro" runat="server" Text='<%# Eval("GrupoAlimento") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNome" runat="server" Text='<%# Eval("Alimento") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblSharedReg" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSituacaoRegistro" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:LinkButton ID="lbEditar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdAlimento") %>' data-toggle="tooltip" data-placement="top" title="Editar Alimento"><i class="fas fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbHomolog" runat="server" CssClass="btn btn-warning btn-xs" CommandName="homologar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdAlimento") %>' data-toggle="tooltip" data-placement="top" title="Homologar Alimento"><i class="fas fa-file-signature"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdAlimento") %>' data-toggle="tooltip" data-placement="top" title="Excluir Alimento"><i class="fas fa-trash-alt"></i></asp:LinkButton>
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
                                <div class="page-title">
                                    <div class="clearfix"></div>
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">
                                            <div class="row wrapper border-bottom white-bg page-heading">
                                                <div class="hr-line"></div>
                                                <div class="col-lg-2 form-group ">
                                                    <h5>Registros por página </h5>
                                                </div>
                                                <div class="col-lg-1 form-group ">
                                                    <asp:DropDownList ID="ddlPag" runat="server" CssClass="form-control btn btn-default" AutoPostBack="True" OnSelectedIndexChanged="ddlPag_SelectedIndexChanged" Style="width: 70px">
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
                </div>
            </div>
        </ContentTemplate>
    </asp:updatepanel>
    <div class="pagina-footer navbar-fixed-bottom" role="banner">
        <div class="container">
            <div class="pull-left">
                |&nbsp;&nbsp;<a href="https://www.youtube.com/channel/UCPk1NVPuAgVPjf6eQOI5qeg?view_as=public" target="_blank"><i class="fab fa-youtube"></i></a>&nbsp;
                |&nbsp;&nbsp;<a href="https://www.facebook.com/nutrovetonline/" target="_blank" class="facebook"><i class="fab fa-facebook"></i></a>&nbsp;
                |&nbsp;<a href="https://www.instagram.com/nutrovetonline/" target="_blank" class="instagram"><i class="fab fa-instagram"></i></a>&nbsp;|
            </div>
            <div class="pull-right">
                NutroVET by <strong>RD Sistemas &copy;<asp:label id="lblAno" runat="server" text=""></asp:label></strong>
            </div>
        </div>
    </div>
</asp:Content>
