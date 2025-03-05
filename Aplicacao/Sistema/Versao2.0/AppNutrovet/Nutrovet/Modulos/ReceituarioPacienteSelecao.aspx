<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true" CodeBehind="ReceituarioPacienteSelecao.aspx.cs" Inherits="Nutrovet.Modulos.ReceituuarioPacienteSelecao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {
                $('.dataTables-pacientes').DataTable({
                    pageLength: 25,
                    responsive: true,
                    "paging": false,
                    "info": false,
                    dom: '<"html5buttons"B>lTfgitp',
                    buttons: [
                        { extend: 'copy' },
                        { extend: 'csv' },
                        { extend: 'excel', title: 'NutroVET - Pacientes' },
                        { extend: 'pdf', title: 'NutroVET - Pacientes' },
                        {
                            extend: 'print',
                            customize: function (win) {
                                $(win.document.body).addClass('white-bg');
                                $(win.document.body).css('font-size', '10px');

                                $(win.document.body).find('table')
                                    .addClass('compact')
                                    .css('font-size', 'inherit');
                            }

                        }
                    ],
                    "columnDefs": [
                        {
                            "targets": [4],
                            "visible": true,
                            "searchable": false,
                            "ordering": false

                        },
                        {
                            "targets": [5],
                            "visible": true,
                            "searchable": false,
                            "ordering": false
                        }
                    ],
                    "aoColumns": [
                        null,
                        null,
                        null,
                        null
                    ],
                    "bFilter": true,
                    "language": {
                        "search": "Filtrar Registros",
                    }


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
                next = focusable.eq(focusable.index(this)+1);
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
                        <h2>Receituário</h2>
                        <div class="col-lg-6">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i>&nbsp;Início</asp:HyperLink>
                                </li>
                                <li>
                                    <i class="fa fa-medkit" aria-hidden="true"></i>&nbsp;Módulos
                                </li>
                                <li>
                                    <i class="fa fa-heartbeat" aria-hidden="true"></i>&nbsp;Receituário
                                </li>
                                <li class="active">
                                    <i class="fa fa-check-square" aria-hidden="true"></i><strong>&nbsp;Seleção Pacientes</strong>
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
            <div class="">
                <div class="page-title">
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row wrapper border-bottom white-bg page-heading">
                                <div class="ibox-content">
                                    <div class="search-form">
                                        <div class="input-group col-lg-12">
                                            <asp:TextBox ID="tbPesq" runat="server" placeholder="Pesquisar por Paciente ou por Tutor" CssClass="form-control input-md bg-muted"></asp:TextBox>
                                            <div class="input-group-btn">
                                                <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-md btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="x_content table-responsive">
                                    <asp:HiddenField ID="hfID" runat="server" />
                                    <!-- start project list -->
                                    <asp:Repeater ID="rptAnimais" runat="server" OnItemCommand="rptAnimais_ItemCommand">
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
                                                        <th style="width: 10%">
                                                            <asp:Label ID="lblEspecie" runat="server" Text="Espécie"></asp:Label>
                                                        </th>
                                                        <th style="width: 25%">
                                                            <asp:Label ID="lblRaca" runat="server" Text="Raça"></asp:Label>
                                                        </th>
                                                        <th style="width: 15%" class="text-center">Ação

                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="gradeA">
                                                <td>
                                                    <asp:Label ID="lblTutor" runat="server" Text='<%# Eval("tNome") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPaciente" runat="server" Text='<%# Eval("Animal") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEspecie" runat="server" Text='<%# Eval("Especie") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRaca" runat="server" Text='<%# Eval("Raca") %>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:LinkButton ID="lbSelecionar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="selecionar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdAnimal") %>' data-toggle="tooltip" data-placement="top" title="Selecionar Paciente"><i class="fa fa-square" aria-hidden="true"></i></asp:LinkButton>
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
            <div class="row">
                <div class="col-md-12">
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
                                                <asp:DropDownList ID="ddlPag" runat="server" CssClass="form-control btn btn-default" AutoPostBack="True" OnSelectedIndexChanged="ddlPag_SelectedIndexChanged" Style="width: 70px">
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