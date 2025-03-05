<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true" CodeBehind="CuponsSelecao.aspx.cs" Inherits="Nutrovet.Administracao.CumponsSelecao" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

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
                <h2>Cupoms</h2>
                <div class="col-lg-4">
                    <ol class="breadcrumb">
                        <li>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                        </li>
                        <li class="active">
                            <i class="fas fa-ticket-alt"></i><strong>&nbsp;Cupons</strong>
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
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-3">
                                            <div class="panel panel-warning">
                                                <b title="Filtrar por assinaturas a vencer">
                                                    <asp:Label ID="lblPesquisarNumeroCupom" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Pesquisar por Cupom:"></asp:Label></b>
                                                <div class="panel-heading">
                                                    <asp:TextBox ID="tbPesq" runat="server" placeholder="Número do Cupom" CssClass="form-control"></asp:TextBox>
                                                    <hr />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-3">
                                            <div class="panel panel-info">
                                                <b title="Filtrar por assinaturas a vencer">
                                                    <asp:Label ID="lblSelecaoProfessor" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Selecione Professor:"></asp:Label></b>
                                                <div class="panel-heading">
                                                    <asp:DropDownList ID="ddlProfessores" runat="server" placeholder="Pesquisar por Professor" CssClass="form-control input-md bg-muted"></asp:DropDownList>
                                                    <hr />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5">
                                            <div class="panel panel-success">
                                                <b title="Filtrar por assinaturas a vencer">
                                                    <asp:Label ID="lblFiltroTipoAssinaturas" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Situação:"></asp:Label></b>
                                                <div class="panel-heading">
                                                    <asp:RadioButtonList CssClass="form-control input-md bg-muted" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" TextAlign="right" ID="rblUsados">
                                                        <asp:ListItem Value="0" Selected="True">&nbsp;Sem&nbsp;&nbsp;Filtro&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="1">&nbsp;Usados&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Não&nbsp;Usados&nbsp;</asp:ListItem>
                                                    </asp:RadioButtonList>
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
                            <div class="x_content table-responsive col-lg-12">
                                <!-- start project list -->
                                <asp:Repeater ID="rptCupom" runat="server" OnItemCommand="rptCupom_ItemCommand" OnItemDataBound="rptCupom_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-striped table-hover">
                                            <thead>
                                                <tr>
                                                    <th style="width: 10%">
                                                        <asp:Label ID="lblNrCupom" runat="server" Text="Nº&nbsp;Cupom"></asp:Label>
                                                    </th>
                                                    <th style="width: 15%">
                                                        <asp:Label ID="lblTipoPlano" runat="server" Text="Tipo&nbsp;Plano"></asp:Label>
                                                    </th>
                                                    <th style="width: 10%" class="text-center">
                                                        <asp:Label ID="lblDtInicio" runat="server" Text="Início"></asp:Label>
                                                    </th>
                                                    <th style="width: 10%" class="text-center">
                                                        <asp:Label ID="lblDtFim" runat="server" Text="Final"></asp:Label>
                                                    </th>
                                                    <th style="width: 10%" class="text-center">
                                                        <asp:Label ID="lblProfessor" runat="server" Text="Professor"></asp:Label>
                                                    </th>
                                                    <th style="width: 10%" class="text-center">
                                                        <asp:Label ID="lblUsado" runat="server" Text="Utilizado"></asp:Label>
                                                    </th>
                                                    <th style="width: 10%" class="text-center">
                                                        <asp:Label ID="lblAcessoLib" runat="server" Text="Aces.&nbsp;Lib."></asp:Label>
                                                    </th>
                                                    <th style="width: 15%" class="text-center">Ação
                                                        <asp:LinkButton ID="lbInserir" runat="server" CommandName="inserir" CssClass="btn btn-primary-nutrovet btn-xs" data-toggle="tooltip" data-placement="top" title="Incluir Cupom"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbMultiVoucher" runat="server" CssClass="btn btn-warning btn-xs" CommandName="gerar"><i class="fas fa-copy" data-toggle="tooltip" data-placement="top" title="Gerar Cupons"></i></asp:LinkButton>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNrCupomTemp" runat="server" Text='<%# Eval("NrCupom") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoPlanoTemp" runat="server" Text='<%# Eval("TipoPlano") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblDtInicioTemp" runat="server" Text='<%# Eval("DtInicial") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblDtFimTemp" runat="server" Text='<%# Eval("DtFinal") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblProfessorTemp" runat="server" Text='<%# Eval("Professor") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblUsadoTemp" runat="server" Text='<%# Eval("fUsado") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblAcessoLibTemp" runat="server" Text='<%# Eval("fAcessoLiberado") %>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="lbEditarCupom" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCupom") %>'> <i class="fas fa-edit" data-toggle="tooltip" data-placement="top" title="Editar Cupom"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCupom") %>'><i class="far fa-trash-alt" data-toggle="tooltip" data-placement="top" title="Excluir Cupom"></i></asp:LinkButton>
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
            <!--modal edição/alteração   -->
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
                            <div class="row" id="divNrCupom" runat="server">
                                <label id="lblNrCupom" class="col-sm-3 control-label">Cupom</label>
                                <div class="col-sm-9">
                                    <div class="input-group">
                                        <span class="input-group-addon" style=""><i class="fas fa-ticket-alt"></i></span>
                                        <asp:TextBox ID="tbxCupom" runat="server" placeholder="Cupom" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Cupom" Enabled="False"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divQuantCupons" runat="server">
                                <label id="lblQuantCupons" class="col-sm-3 control-label">Quantidade de Cupons</label>
                                <div class="col-sm-9">
                                    <div class="input-group">
                                        <span class="input-group-addon" style=""><i class="fas fa-money-bill-wave-alt"></i></span>
                                        <cc1:MEdit ID="meQuantCupons" runat="server" placeholder="Quantidade de Cupons" Mascara="Inteiro" CssClass="form-control" data-toggle="tooltip" data-placement="top"></cc1:MEdit>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 form-group "></div>
                                <label id="lblTpDesconto" class="col-sm-3 col-lg-3 control-label">Tipo de Plano</label>
                                <div class="col-sm-5 col-lg-5 form-group">
                                    <asp:DropDownList ID="ddlTpPlano" class="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 form-group "></div>
                                <label id="lblPlano" class="col-sm-3 col-lg-3 control-label">Validade Inicial</label>
                                <div class="col-sm-5 col-lg-5 form-group">
                                    <cc1:MEdit ID="meDtIni" runat="server" placeholder="Validade Inicial" Mascara="Data" DataMaior="True" CssClass="form-control" data-toggle="tooltip" data-placement="top"></cc1:MEdit>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 form-group "></div>
                                <label id="lblValidFinal" class="col-sm-3 col-lg-3 control-label">Validade Final</label>
                                <div class="col-sm-5 col-lg-5 form-group ">
                                    <cc1:MEdit ID="meDtFinal" runat="server" placeholder="Validade Final" Mascara="Data" DataMaior="True" CssClass="form-control" data-toggle="tooltip" data-placement="top"></cc1:MEdit>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 form-group "></div>
                                <label id="lblProfessor" class="col-sm-3 col-lg-3 control-label">Professor</label>
                                <div class="col-sm-5 col-lg-5 form-group ">
                                    <asp:TextBox ID="txtProfessor" runat="server" placeholder="Professor" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Professor"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 form-group "></div>
                                <div class="col-sm-5 col-lg-5 form-group ">
                                    <asp:CheckBox ID="cbxAcesLib" runat="server" Text="&nbsp;&nbsp;Acesso&nbsp;Liberado" />
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
                |&nbsp;&nbsp;       |&nbsp;&nbsp;<a href="https://www.youtube.com/channel/UCPk1NVPuAgVPjf6eQOI5qeg?view_as=public" target="_blank"><i class="fab fa-youtube"></i></a>&nbsp;
                |&nbsp;&nbsp;<a href="https://www.facebook.com/nutrovetonline/" target="_blank" class="facebook"><i class="fab fa-facebook"></i></a>&nbsp;
                |&nbsp;<a href="https://www.instagram.com/nutrovetonline/" target="_blank" class="instagram"><i class="fab fa-instagram"></i></a>&nbsp;|
            </div>
            <div class="pull-right">
                NutroVET by <strong>RD Sistemas &copy;<asp:Label ID="lblAno" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </div>
</asp:Content>
