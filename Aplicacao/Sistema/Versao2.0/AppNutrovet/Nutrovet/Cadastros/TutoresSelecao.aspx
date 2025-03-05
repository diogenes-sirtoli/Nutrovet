<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="TutoresSelecao.aspx.cs" Inherits="Nutrovet.Cadastros.TutoresSelecao"
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
    <div class="">
        <div class="page-title">
            <div class="clearfix"></div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="row wrapper border-bottom white-bg page-heading">
                        <div class="col-lg-1">
                            <asp:HyperLink ID="HyperLink12" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
                        </div>
                        <h2>Tutores</h2>
                        <div class="col-lg-4">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <i class="fas fa-user"></i><strong>Tutores</strong>
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
                                                <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-md btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="x_content table-responsive">
                                    <asp:HiddenField ID="hfID" runat="server" />
                                    <!-- start project list -->
                                    <asp:Repeater ID="rptListagemTutores" runat="server" OnItemCommand="rptListagemTutores_ItemCommand">
                                        <HeaderTemplate>
                                            <table class="table table-striped  table-hover">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 24%">
                                                            <asp:Label ID="lblNomeTutor" runat="server" Text="Nome"></asp:Label>
                                                        </th>
                                                        <th style="width: 20%">
                                                            <asp:Label ID="lblEmailTutor" runat="server" Text="E-mail"></asp:Label>
                                                        </th>
                                                        <th style="width: 20%">
                                                            <asp:Label ID="lblTelefoneTutor" runat="server" Text="Telefone"></asp:Label>
                                                        </th>
                                                        <th style="width: 20%">
                                                            <asp:Label ID="lblCelularTutor" runat="server" Text="Celular"></asp:Label>
                                                        </th>
                                                        <th style="width: 15%" class="text-center">Ação
                                                            <asp:LinkButton ID="lbInserirTutor" runat="server" CommandName="inserir" CssClass="btn btn-primary-nutrovet btn-xs" data-toggle="tooltip" data-placement="top" title="Incluir Tutor"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="gradeA">
                                                <td>
                                                    <asp:Label ID="lblNomeTutorRegistro" runat="server" Text='<%# Eval("tNome") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEmailTutorRegistro" runat="server" Text='<%# Eval("tEmail") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTelefoneTutorRegistro" runat="server" Text='<%# Eval("tTelefone") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCelularTutorRegistro" runat="server" Text='<%# Eval("tCelular") %>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:LinkButton ID="lbEditarTutor" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdTutores") %>'> <i class="fas fa-edit" data-toggle="tooltip" data-placement="top" title="Editar Tutor"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbExcluirTutor" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdTutores") %>'><i class="far fa-trash-alt" data-toggle="tooltip" data-placement="top" title="Excluir Tutor"></i></asp:LinkButton>
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
                <div class="">
                    <div class="page-title">
                        <div class="clearfix"></div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="row wrapper border-bottom white-bg page-heading">
                                        <div class="col-lg-2 form-group ">
                                            <h5>Registros por página </h5>
                                        </div>
                                        <div class="col-lg-1 form-group ">
                                            <asp:DropDownList ID="ddlPag" runat="server" CssClass="btn btn-default form-control" AutoPostBack="True" Style="width: 70px">
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
            <!-- Modal Exclusão Tutor -->
            <div id="modalExclusaoTutor" runat="server" class="modal-dialog modalControle" style="display: none">
                <div class="modal-content animated fadeIn">
                    <div class="modal-header modal-header-danger">
                
                        <asp:LinkButton runat="server" ID="LinkButton1" CssClass="close" data-dismiss="modal" ForeColor="White"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></asp:LinkButton>
                        <h3 class="modal-title"><i class="fa fa-user-times fa-2x" aria-hidden="true"></i>
                            <asp:Label ID="Label11" runat="server" Text=" - Processo de Exclusão de Tutor"></asp:Label>
                        </h3>
                    </div>
                    <div class="modal-body" id="bodymodalExclusaoTutor">

                        <strong> <h3 class="no-margins">Registros Legados que também serão EXCLUÍDOS</h3></strong>
                        <div class="ibox-content">
                            <div class="form-group">
                                <label id="lbl1" class="col-lg-8">Quantidade Pacientes: <asp:Label CssClass="badge badge-danger text-center" ID="lblQtdAnimais" runat="server" Text="Label"></asp:Label></label>
                                <label id="lbl2" class="col-lg-14">Quantidade Cardápios: <asp:Label CssClass="badge badge-danger text-center" ID="lblQtdCard" runat="server" Text="Label"></asp:Label></label>
                            </div>
                        </div>
                        
                        <strong> <h3 class="no-margins">Permissão para Exclusão (Obrigatório)</h3></strong>
                        <div class="ibox-content">
                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                            <div class="form-group">
                                <label id="lblDescricao" class="col-lg-10 control-label">Escreva uma justificativa, consentindo a Exclusão.</label>
                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                <div class="col-lg-12">
                                    <div class="input-group">
                                        <span class="input-group-addon" style="background-color:#F0F0F0"><i class="fas fa-edit fa-fw"></i></span>
                                        <asp:TextBox ID="tbConcessao" runat="server" clientidmode="Static" placeholder="Escreva aqui" cssclass="form-control" data-toggle="tooltip" data-placement="top" title="Escreva aqui sua justificativa, consentindo a Exclusão." Rows="3" MaxLength="100" Height="170px" BackColor="#FFFAFA" TextMode="MultiLine" Font-Bold="True" BorderStyle="Outset"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="pull-left">
                            <asp:LinkButton ID="lbCancelar" runat="server" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Cancelar </asp:LinkButton>
                        </div>
                        <div class="pull-right">
                            <asp:LinkButton ID="lbExcluir" runat="server" OnClick="lbExcluir_Click" CssClass="btn btn-danger" ><i class="fas fa-trash-alt"> </i> <strong> Excluir</strong></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="mdExclusaoTutor" runat="server"
                PopupControlID="modalExclusaoTutor" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblExclusaoTutor" CancelControlID="lbCancelar">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblExclusaoTutor" runat="server" Text=""></asp:Label>
            <!-- Modal Exclusão Tutor -->

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
