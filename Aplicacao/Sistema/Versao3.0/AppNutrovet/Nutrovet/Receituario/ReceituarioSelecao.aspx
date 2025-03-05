<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="ReceituarioSelecao.aspx.cs" Inherits="Nutrovet.Receituario.ReceituarioSelecao"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

    <script>
        function BindEvents() {
            $(document).ready(function () {
                $('#meDtInicial').datepicker({
                    format: "dd/mm/yyyy",
                    clearBtn: true,
                    language: "pt-BR",
                    daysOfWeekHighlighted: "0,6",
                    autoclose: true,
                    todayHighlight: true
                });

                $('#meDtFinal').datepicker({
                    format: "dd/mm/yyyy",
                    clearBtn: true,
                    language: "pt-BR",
                    daysOfWeekHighlighted: "0,6",
                    autoclose: true,
                    todayHighlight: true
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
                            <asp:HyperLink ID="HyperLink12" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i></asp:HyperLink>
                        </div>
                        <h2>Receitas Elaboradas</h2>
                        <div class="col-lg-4">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i>Início</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <i class="fas fa-book-medical fa-lg"></i><strong>&nbsp;Receitas Elaboradas</strong>
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
            <div>
                <div class="page-title">
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row wrapper border-bottom white-bg page-heading">
                                <div class="ibox-content">
                                    <div class="search-form">
                                        <div class="input-group col-lg-12">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-11">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                    <div class="panel panel-warning">
                                                        <b title="Filtrar por Tutor">
                                                            <asp:Label ID="lblPesquisarTabela" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Selecionar Tutor:"></asp:Label></b>
                                                        <div class="panel-heading">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><i class="fas fa-user fa-fw"></i></span>
                                                                <asp:DropDownList ID="ddlTutor" runat="server" AutoPostBack="True" class="form-control" data-toggle="tooltip" data-placement="top" title="Selecione o Tutor" OnSelectedIndexChanged="ddlTutores_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                    <div class="panel panel-danger">
                                                        <b title="Filtar por Paciente">
                                                            <asp:Label ID="lblPesquisarAcoes" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Selecionar Paciente:"></asp:Label></b>
                                                        <div class="panel-heading">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><i class="fas fa-paw fa-fw"></i></span>
                                                                <asp:DropDownList ID="ddlAnimais" runat="server" class="form-control" data-toggle="tooltip" data-placement="top" title="Selecione o Paciente">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                    <div class="panel panel-info">
                                                        <b title="Filtrar por Tipo de Receita">
                                                            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Selecionar Tipo de Receita:"></asp:Label></b>
                                                        <div class="panel-heading">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><i class="fas fa-book-medical fa-fw"></i></span>
                                                                <asp:DropDownList ID="ddlTpReceita" runat="server" class="form-control" data-toggle="tooltip" data-placement="top" title="Selecione o Tipo de Receita">
                                                                    <asp:ListItem Value="0">-- Receituário Inteligente --</asp:ListItem>
                                                                    <asp:ListItem Value="1">Suplementação</asp:ListItem>
                                                                    <asp:ListItem Value="2">Nutracêuticos</asp:ListItem>
                                                                    <asp:ListItem Value="3">Receita&nbsp;em&nbsp;Branco</asp:ListItem>
                                                                    <asp:ListItem Value="3">Balanceamento&nbsp;de&nbsp;Dietas</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-11">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                                    <div class="panel panel-success">
                                                        <b title="Filtrar por Tutor">
                                                            <asp:Label ID="lblFiltroPeriodoInicial" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Período Inicial:"></asp:Label></b>
                                                        <div class="panel-heading">
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style=""><i class="fa fa-calendar fa-fw"></i></span>
                                                                <cc1:MEdit ID="meDtInicial" name="meDtInicial" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Período Inicial"></cc1:MEdit>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                                    <div class="panel panel-success">
                                                        <b title="Período Inicial">
                                                            <asp:Label ID="lblFiltroPeriodoFinal" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Período Final:"></asp:Label></b>
                                                        <div class="panel-heading">
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style=""><i class="fa fa-calendar fa-fw"></i></span>
                                                                <cc1:MEdit ID="meDtFinal" name="meDtFinal" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Período Final"></cc1:MEdit>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-1 col-lg-1">
                                                <div class="input-group-btn">
                                                    <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-md btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="x_content table-responsive">
                                    <asp:HiddenField ID="hfID" runat="server" />
                                    <!-- start project list -->
                                    <asp:Repeater ID="rptReceituario" runat="server" OnItemCommand="rptReceituario_ItemCommand" OnItemDataBound="rptReceituario_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="table table-striped table-hover dataTables-pacientes">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 20%">
                                                            <asp:Label ID="lblTutor" runat="server" Text="Tutor"></asp:Label>
                                                        </th>
                                                        <th style="width: 15%">
                                                            <asp:Label ID="lblPacientes" runat="server" Text="Paciente"></asp:Label>
                                                        </th>
                                                        <th style="width: 5%">
                                                            <asp:Label ID="lblEspecie" runat="server" Text="Espécie"></asp:Label>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:Label ID="lblRaca" runat="server" Text="Raça"></asp:Label>
                                                        </th>
                                                        <th style="width: 13%">
                                                            <asp:Label ID="lbTpReceita" runat="server" Text="Tipo&nbsp;Receita"></asp:Label>
                                                        </th>
                                                        <th style="width: 15%">
                                                            <asp:Label ID="lbTituloReceita" runat="server" Text="Uso"></asp:Label>
                                                        </th>
                                                        <th style="width: 10%">
                                                            <asp:Label ID="lbDataReceita" runat="server" Text="Data&nbsp;Receita"></asp:Label>
                                                        </th>
                                                        <th style="width: 10%" class="text-center">Ação
                                                &nbsp;
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="gradeA">
                                                <td>
                                                    <asp:Label ID="lblTutorReg" runat="server" Text='<%# Eval("Tutor") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPacienteReg" runat="server" Text='<%# Eval("Animal") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEspecieReg" runat="server" Text='<%# Eval("Especie") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRacaReg" runat="server" Text='<%# Eval("Raca") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbTpReceitaReg" runat="server" Text='<%# Eval("TipoReceita") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbTituloReceitaReg" runat="server" Text='<%# Eval("Titulo") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbDataReceitaReg" runat="server" Text='<%# Eval("DataReceita") %>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:LinkButton ID="lbEditar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdReceita") %>' data-toggle="tooltip" data-placement="top" title="Editar Receita"><i class="fas fa-edit"></i></asp:LinkButton>
                                                    <asp:HyperLink ID="hlVisualizar" runat="server" CssClass="btn btn-warning btn-xs" Target="_blank" ToolTip="Visualizar Receita"><i class="far fa-eye" title="Visualizar Receita"></i></asp:HyperLink>
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



                                <div>
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
