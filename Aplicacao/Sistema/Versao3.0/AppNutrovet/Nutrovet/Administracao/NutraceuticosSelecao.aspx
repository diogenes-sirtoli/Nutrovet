<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master"
    AutoEventWireup="true" CodeBehind="NutraceuticosSelecao.aspx.cs"
    Inherits="Nutrovet.Administracao.NutraceuticosSelecao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

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
                <h2>Nutracêuticos</h2>
                <div class="col-lg-4">
                    <ol class="breadcrumb">
                        <li>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                        </li>
                        <li class="active">
                            <i class="fas fa-capsules"></i><strong>&nbsp;Seleção de Nutracêuticos</strong>
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
            <div class="page-title">
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row wrapper border-bottom white-bg page-heading">
                            <div class="ibox-content">
                                <div class="search-form">
                                    <div class="input-group col-lg-12" data-toggle="tooltip" data-placement="top" title="Pesquisar por Nutracêutico">
                                        <asp:TextBox ID="tbPesq" runat="server" placeholder="Pesquisar por Nutracêutico" CssClass="form-control input-md bg-muted"></asp:TextBox>
                                        <div class="input-group-btn">
                                            <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <div class="col-lg-12">
                                        <div class="input-group m-b">
                                            <span class="input-group-addon"><i class="fas fa-dog fa-flip-horizontal"></i><i class="fas fa-cat"></i></span>
                                            <asp:DropDownList ID="ddlEspecie" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecie_SelectedIndexChanged" data-toggle="tooltip" data-placement="top" title="Escolha a Espécie">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="x_content table-responsive col-lg-12">
                                <asp:HiddenField ID="hfID" runat="server" />
                                <!-- start nutraceuticos list -->
                                <asp:Repeater ID="rptListNutra" runat="server" OnItemCommand="rptListNutra_ItemCommand" OnItemDataBound="rptListNutra_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-striped projects table-hover dataTables-nutraceuticos">
                                            <thead>
                                                <tr>
                                                    <th style="width: 25%" class="text-left">
                                                        <asp:Label ID="Label8" runat="server" Text="Nutracêutico"></asp:Label>
                                                    </th>
                                                    <th style="width: 8%" class="text-center">
                                                        <asp:Label ID="Label1" runat="server" Text="Espécie"></asp:Label>
                                                    </th>
                                                    <th style="width: 10%" class="text-center">
                                                        <asp:Label ID="Label2" runat="server" Text="Dose Mínima"></asp:Label>
                                                    </th>
                                                    <th style="width: 8%" class="text-center">
                                                        <asp:Label ID="Label3" runat="server" Text="Unidade"></asp:Label>
                                                    </th>
                                                    <th style="width: 10%" class="text-center">
                                                        <asp:Label ID="Label4" runat="server" Text="Dose Máxima"></asp:Label>
                                                    </th>
                                                    <th style="width: 8%" class="text-center">
                                                        <asp:Label ID="Label5" runat="server" Text="Unidade"></asp:Label>
                                                    </th>
                                                    <th style="width: 8%" class="text-center">
                                                        <asp:Label ID="Label6" runat="server" Text="Interv&nbsp;1"></asp:Label>
                                                    </th>
                                                    <th style="width: 8%" class="text-center">
                                                        <asp:Label ID="Label7" runat="server" Text="Interv&nbsp;2"></asp:Label>
                                                    </th>
                                                    <th style="width: 15%" class="text-center">Ação
                                                        <asp:LinkButton ID="lbInserirTutor" runat="server" CommandName="inserir" CssClass="btn btn-primary-nutrovet btn-xs" data-toggle="tooltip" data-placement="top" title="Incluir Nutracêutico"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="gradeA">
                                            <td>
                                                <asp:Label ID="lblNutraceutico" runat="server" Text='<%# Eval("Nutriente") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblEspecie" runat="server" Text='<%# Eval("Especie") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblDoseMinima" runat="server" Text='<%# Eval("DoseMin") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblUnDoseMin" runat="server" Text='<%# Eval("UnidadeMin") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblDoseMaxima" runat="server" Text='<%# Eval("DoseMax") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblUnDoseMaxima" runat="server" Text='<%# Eval("UnidadeMax") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblIntervalo1" runat="server" Text='<%# Eval("TpPrescr1") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblIntervalo2" runat="server" Text='<%# Eval("TpPrescr2") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="lbEditar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdNutrac") %>' data-toggle="tooltip" data-placement="top" title="Editar Nutracêutico"><i class="fas fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdNutrac") %>' data-toggle="tooltip" data-placement="top" title="Excluir Nutracêutico"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Deseja Realmente Excluir Este Registro?" TargetControlID="lbExcluir" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                            </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <!-- end nutraceuticos list -->
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
