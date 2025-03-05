<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="DietasCad.aspx.cs" Inherits="Nutrovet.Cadastros.DietasCad"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {
                $("#tbCarboidrato").TouchSpin({
                    min: 0,
                    max: 100,
                    step: 1,
                    decimals: 0,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet'
                });

                $("#tbProteina").TouchSpin({
                    min: 0,
                    max: 100,
                    step: 1,
                    decimals: 0,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet'
                });

                $("#tbGordura").TouchSpin({
                    min: 0,
                    max: 100,
                    step: 1,
                    decimals: 0,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet'
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
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <div class="col-lg-1">
                <asp:HyperLink ID="hlkMinimalize" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
            </div>
            <h2>
                <asp:Label ID="lblPagina" runat="server" Text=""></asp:Label></h2>
            <div class="col-lg-4">
                <ol class="breadcrumb">
                    <li>
                        <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server">Início</asp:HyperLink>
                    </li>
                    <li class="active">
                        <asp:HyperLink ID="hlTutoresSelecao" NavigateUrl="~/Cadastros/DietasSelecao.aspx" runat="server"> Dietas</asp:HyperLink>
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblTitulo" runat="server" Text="CadAlt"></asp:Label>
                        </strong>
                    </li>
                </ol>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(BindEvents);
            </script>
            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            <asp:Label ID="lblSubTitulo" runat="server" Text="Insira ou altere os dados da Dieta"></asp:Label>
                        </h5>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group">
                            <label id="lblEspecie" class="col-lg-3 control-label">Espécie</label>
                            <div class="input-group col-lg-6">
                                <span class="input-group-addon"><i class="fas fa-dog fa-flip-horizontal"></i><i class="fas fa-cat"></i></span>
                                <asp:DropDownList ID="ddlEspecie" runat="server" AutoPostBack="False" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label id="lblNomeDieta" class="col-lg-3 control-label">Nome da Dieta</label>
                            <div class="input-group col-lg-6">
                                <span class="input-group-addon" style=""><i class="fa fa-balance-scale fa-fw"></i></span>
                                <asp:TextBox ID="tbDieta" runat="server" placeholder="Dieta" CssClass="form-control m-b" data-toggle="tooltip" data-placement="top" title="Dieta" autofocus="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label id="lblAlimentosIndicados" class="col-lg-3 control-label">Alimentos Indicados</label>
                            <div class="input-group col-lg-6">
                                <asp:TextBox ID="tbPesqIndic" runat="server" placeholder="Pesquisar Alimentos Indicados" CssClass="form-control m-b "></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btnPesqIndic" runat="server" CssClass="btn btn-md btn-primary-nutrovet form-control" OnClick="btnPesqIndic_Click"> <i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div id="divPesqAlimIndic" runat="server" class="x_content table-responsive col-lg-offset-3 col-lg-6 col-lg-offset-3">
                                    <asp:ListBox ID="ltbAlimIndic" runat="server" Style="overflow-x: auto;" class="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    <asp:LinkButton ID="lbFechaPesqAlimIndic" runat="server" CssClass="btn btn-sm btn-primary-nutrovet pull-right " OnClick="lbFechaPesqAlimIndic_Click"><i class='fas fa-check-square'></i> Incluir</asp:LinkButton>
                                </div>
                                <div class="x_content table-responsive col-lg-offset-3 col-sm-6">
                                    <!-- start project list -->
                                    <asp:Repeater ID="rptAlimIndicados" runat="server" OnItemCommand="rptAlimIndicados_ItemCommand">
                                        <HeaderTemplate>
                                            <table class="table table-striped projects dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 70%">
                                                            <asp:Label ID="lblAlimento" runat="server" Text="Alimento"></asp:Label>
                                                        </th>
                                                        <th style="width: 5%">Ação</th>
                                                        <th>&nbsp;
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAlimentoReg" runat="server" Text='<%# Eval("Alimento") %>'></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdAlimento") %>' TargetControlID="lbExcluir"><i class="fas fa-trash-alt"></i> Excluir </asp:LinkButton>
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

                        <div class="form-group">
                            <label id="lblAlimentosContraindicados" class="col-lg-3 control-label">Alimentos Contraindicados</label>
                            <div class="input-group col-lg-6">
                                <asp:TextBox ID="tbPesqContra" runat="server" placeholder="Pesquisar Alimentos Contraindicados" CssClass="form-control m-b"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btnPesqContra" runat="server" CssClass="btn btn-md btn-primary-nutrovet form-control" OnClick="btnPesqContra_Click"> <i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                            </div>

                            <div class="row">
                                <div id="divPesqAlimContra" runat="server" class="x_content table-responsive col-lg-offset-3 col-lg-6 col-lg-offset-3">
                                    <asp:ListBox ID="ltbAlimContra" runat="server" Style="overflow-x: auto;" class="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    <asp:LinkButton ID="lbFechaPesqAlimContra" runat="server" CssClass="btn btn-sm btn-primary-nutrovet pull-right " OnClick="lbFechaPesqAlimContra_Click"><i class='fas fa-check-square'></i> Incluir</asp:LinkButton>
                                </div>
                                <div class="x_content table-responsive col-lg-offset-3 col-sm-6">
                                    <!-- start project list -->
                                    <asp:Repeater ID="rptAlimContra" runat="server" OnItemCommand="rptAlimContra_ItemCommand">
                                        <HeaderTemplate>
                                            <table class="table table-striped projects dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 70%">
                                                            <asp:Label ID="lblAlimentoContra" runat="server" Text="Alimento"></asp:Label>
                                                        </th>
                                                        <th style="width: 5%">Ação</th>
                                                        <th>&nbsp;
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAlimentoReg" runat="server" Text='<%# Eval("Alimento") %>'></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdAlimento") %>' TargetControlID="lbExcluir"><i class="fas fa-trash-alt"></i> Excluir </asp:LinkButton>
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
                        <div class="form-group">
                            <label id="lblPercentualCarboidrato" class="col-lg-3 control-label">% de Carboidrato</label>
                            <div class="input-group col-lg-3">
                                <asp:TextBox ID="tbCarboidrato" ClientIDMode="Static" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="% de Carboidrato"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label id="lblPercentualProteina" class="col-lg-3 control-label">% de Proteína</label>
                            <div class="input-group col-lg-3">
                                <asp:TextBox ID="tbProteina" ClientIDMode="Static" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="% de Proteína"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label id="lblPercentualGordura" class="col-lg-3 control-label">% de Gordura</label>
                            <div class="input-group col-lg-3">
                                <asp:TextBox ID="tbGordura" ClientIDMode="Static" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="% de Gordura"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer col-sm-9 col-lg-9">
                        <div class="btn-group" role="group">
                            <asp:LinkButton runat="server" ID="lbFechar" CssClass="btn btn-default" OnClick="lbFechar_Click"><i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbSalvar" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvar_Click"><i class='far fa-save'></i> Salvar</asp:LinkButton>
                        </div>
                    </div>

                </div>
            </div>

            <div class="hr-line-dashed"></div>
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
