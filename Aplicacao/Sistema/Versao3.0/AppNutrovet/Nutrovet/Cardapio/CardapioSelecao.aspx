<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="CardapioSelecao.aspx.cs" Inherits="Nutrovet.Cardapio.CardapioSelecao"
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

        $(function () {
            $('.btn-text-pop').popover({ html: true });
            $('.btn-text-too').tooltip();
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
                        <h2>Cardápios</h2>
                        <div class="col-lg-4">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <i class="fas fa-balance-scale"></i><strong>&nbsp;Cardápios</strong>
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
            <div class="row">
                <div class="col-lg-12">
                    <div class="row wrapper border-bottom white-bg page-heading">
                        <div class="ibox-content">
                            <div class="search-form">
                                <div class="form-group">
                                    <label id="lblTutor" class="col-sm-2 control-label">Tutor</label>
                                    <div class="col-lg-8">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fas fa-user fa-fw"></i></span>
                                            <asp:DropDownList ID="ddlTutor" runat="server" AutoPostBack="True" class="form-control" data-toggle="tooltip" data-placement="top" title="Selecione o Tutor" OnSelectedIndexChanged="ddlTutores_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 form-group "></div>
                                <div class="form-group">
                                    <label id="lblNomePaciente" class="col-sm-2 control-label">Paciente</label>
                                    <div class="col-lg-8">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fas fa-paw fa-fw"></i></span>
                                            <asp:DropDownList ID="ddlAnimais" runat="server" class="form-control" data-toggle="tooltip" data-placement="top" title="Selecione o Paciente" AutoPostBack="True" OnSelectedIndexChanged="ddlAnimais_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 form-group "></div>
                                <div class="form-group">
                                    <div class="col-lg-2">
                                    </div>
                                    <div class="col-lg-8">
                                        <div class="input-group pull-right">
                                            <asp:LinkButton runat="server" ID="lbtnBalDieta" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs btn-text-too" data-toggle="tooltip" data-html="true" title="Para realizar o balanceamento entre dietas, 
                                                selecione o tutor e o paciente, em seguida 
                                                escolha o fator e após selecione os cardápios."
                                                OnClick="btnBalDieta_Click" Enabled="False"><i class="fas fa-weight"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbInserirCardapio" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" data-toggle="tooltip" data-placement="top" title="Inserir Cardápio" OnClick="lbInserir_Click"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 form-group "></div>
                            </div>
                        </div>
                        <div class="x_content table-responsive col-lg-12">
                            <asp:HiddenField ID="hfID" runat="server" />
                            <!-- start project list -->
                            <asp:Repeater ID="rptCardapios" runat="server" OnItemCommand="rptCardapios_ItemCommand" OnItemDataBound="rptCardapios_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="table table-striped table-hover dataTables-cardapios">
                                        <thead>
                                            <tr>
                                                <th style="width: 20%">
                                                    <asp:Label ID="lblTutorTh" runat="server" Text="Tutor"></asp:Label>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:Label ID="lblPacienteTh" runat="server" Text="Paciente"></asp:Label>
                                                </th>
                                                <th style="width: 5%">
                                                    <asp:Label ID="lblEspecieTh" runat="server" Text="Espécie"></asp:Label>
                                                </th>
                                                <th style="width: 10%">
                                                    <asp:Label ID="lblRacaTh" runat="server" Text="Raça"></asp:Label>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:Label ID="lblDietaTh" runat="server" Text="Dieta"></asp:Label>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:Label ID="lblTituloCardapio" runat="server" Text="Título"></asp:Label>
                                                </th>
                                                <th style="width: 5%">
                                                    <asp:Label ID="lblFatorEnergiaBalanco" runat="server" Text="Fator"></asp:Label>
                                                    <asp:DropDownList ID="ddlFatorEnergiaBal" runat="server" class="btn btn-default form-control" data-toggle="tooltip" data-placement="top" title="Filtra por Fator e permite selecionar as dietas para participarem do balanceamento." Style="width: 80px" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlFatorEnergiaBal_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </th>
                                                <th style="width: 5%" class="text-center">
                                                    <asp:Label ID="lblDataCardapioTh" runat="server" Text="Data&nbsp;Cardápio"></asp:Label>
                                                </th>
                                                <th style="width: 10%" class="text-center">Ação</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTutor" runat="server" Text='<%# Eval("Tutor") %>'></asp:Label>
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
                                        <td>
                                            <asp:Label ID="lblDieta" runat="server" Text='<%# Eval("Dieta") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescricao" runat="server" Text='<%# Eval("Descricao") %>'></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:HiddenField ID="hfIdCardapio" runat="server" Value='<%# Eval("IdCardapio") %>' />
                                            <asp:CheckBox ID="cbxBaldiet" runat="server" CssClass="custom-control custom-checkbox" Visible="False" />
                                            <asp:Label ID="lblFator" runat="server" Text='<%# Eval("FatorEnergia") %>'></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="lblDataCardapio" runat="server" Text='<%# Eval("DtCardapio") %>'></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lbEditar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCardapio") %>' data-toggle="tooltip" data-placement="top" title="Editar Cardápio"> <i class="fas fa-edit"></i></asp:LinkButton>
                                            <asp:HyperLink ID="hlVisualizar" runat="server" CssClass="btn btn-warning btn-xs" Target="_blank" ToolTip="Visualizar Cardápio"><i class="far fa-eye" title="Visualizar Cardápio"></i></asp:HyperLink>
                                            <asp:LinkButton ID="lbCopiar" runat="server" CssClass="btn btn-info btn-xs" CommandName="copiar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCardapio") %>' data-toggle="tooltip" data-placement="top" title="Copiar Cardápio"> <i class="fas fa-paste"></i></asp:LinkButton>
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Deseja Realmente Copiar esse Cardápio?" TargetControlID="lbCopiar" />
                                            <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCardapio") %>' TargetControlID="lbExcluirTutor" data-toggle="tooltip" data-placement="top" title="Excluir Cardápio"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Deseja Realmente Excluir Este Registro?" TargetControlID="lbExcluir" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
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
                                                <asp:DropDownList ID="ddlPag" runat="server" CssClass="btn btn-default form-control" AutoPostBack="True" Style="width: 70px" OnSelectedIndexChanged="ddlPag_SelectedIndexChanged">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
