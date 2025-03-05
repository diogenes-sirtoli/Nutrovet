<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="NutracDietasCadastro.aspx.cs" Inherits="Nutrovet.Administracao.NutracDietasCadastro" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
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
                <h2>Nutracêuticos x Dietas</h2>
                <div class="col-lg-4">
                    <ol class="breadcrumb">
                        <li>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                        </li>
                        <li class="active">
                            <i class="fas fa-capsules"></i><i class="fas fa-drumstick-bite"></i><strong>&nbsp;Nutracêuticos x Dietas</strong>
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
            <div>
                <div class="page-title">
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class=" col-lg-12">
                            <div class="row wrapper border-bottom white-bg page-heading">
                                <div class="ibox-content">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                        <div class="panel panel-warning">
                                            <b title="Selecione a Espécie">
                                                <asp:Label ID="lblSelecaoEspecie" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Selecione a Espécie:"></asp:Label></b>
                                            <div class="panel-heading">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fas fa-dog fa-flip-horizontal"></i><i class="fas fa-cat"></i></span>
                                                    <asp:DropDownList ID="ddlEspecie" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEspecie_SelectedIndexChanged" data-toggle="tooltip" data-placement="top" title="Escolha a Espécie">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                        <div class="panel panel-warning">
                                            <b title="Selecione Nutracêutico">
                                                <asp:Label ID="lblSelecaoNutraceutico" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Selecione Nutracêutico:"></asp:Label></b>
                                            <div class="panel-heading">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fas fa-capsules fa-flip-horizontal"></i></i></span>
                                                    <asp:DropDownList ID="ddlNutrac" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNutrac_SelectedIndexChanged" data-toggle="tooltip" data-placement="top" title="Escolha o Nutracêutico">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5">
                                        <div class="panel panel-info">
                                            <b title="Listagem Geral de Dietas">
                                                <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Listagem Geral de Dietas"></asp:Label></b>
                                            <div class="panel-heading">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fas fa-file-signature fa-2x"></i></span>
                                                    <asp:ListBox ID="lbxDietas" runat="server" Height="350px" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-2">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <div class="text-center">
                                                    <asp:Button ID="btnVai" runat="server" Text="&#xf2f5;" CssClass="btn btn-primary-nutrovet fa" ToolTip="Incluir Dietas" OnClick="btnVai_Click" />
                                                    <br />
                                                    <asp:Button ID="btnVem" runat="server" Text="&#xf2f5;" CssClass="btn btn-danger fa  fa-flip-horizontal" ToolTip="Excluir Dietas" OnClick="btnVem_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5">
                                        <div class="panel panel-success">
                                            <b title="Listagem de Dietas do Nutracêutico">
                                                <asp:Label ID="Label2" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Listagem de Dietas do Nutracêutico"></asp:Label>
                                                <asp:Label ID="lblNutraceutico" runat="server" Text=""></asp:Label>
                                            </b>
                                            <div class="panel-heading">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fas fa-fill-drip fa-2x"></i></span>
                                                    <asp:ListBox ID="lbxCadastro" runat="server" Height="350px" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
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
                |&nbsp;&nbsp;;<a href="https://www.youtube.com/channel/UCPk1NVPuAgVPjf6eQOI5qeg?view_as=public" target="_blank"><i class="fab fa-youtube"></i></a>&nbsp;
                |&nbsp;&nbsp;<a href="https://www.facebook.com/nutrovetonline/" target="_blank" class="facebook"><i class="fab fa-facebook"></i></a>&nbsp;
                |&nbsp;<a href="https://www.instagram.com/nutrovetonline/" target="_blank" class="instagram"><i class="fab fa-instagram"></i></a>&nbsp;|
            </div>
            <div class="pull-right">
                NutroVET by <strong>SICORP &copy;<asp:Label ID="lblAno" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </div>
</asp:Content>
