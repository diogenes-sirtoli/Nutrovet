<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true" CodeBehind="BibliotecaVisualizar.aspx.cs" Inherits="Nutrovet.Biblioteca.BibliotecaVisualizar" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {

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
                <h2>Biblioteca - Arquivos</h2>
                <div class="col-lg-4">
                    <ol class="breadcrumb">
                        <li>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                        </li>
                        <li class="active">
                            <i class="fas fa-archive"></i><strong>&nbsp;Arquivos - Seções</strong>
                        </li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
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
                                <asp:TextBox ID="tbPesq" runat="server" placeholder="Pesquisar por Ano, Título, Descrição ou Autor" CssClass="form-control input-md bg-muted"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="lbPesq" runat="server" ToolTip="Executa a Pesquisa!" CssClass="btn btn-primary-nutrovet" OnClick="lbPesq_Click"> <i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="lbRemoveFiltro" runat="server" ToolTip="Remove todo o Filtro!" CssClass="btn btn-danger" OnClick="lbRemoveFiltro_Click"> <i class="fas fa-trash-alt"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox col">
                                <div class="ibox-title">
                                    <span class="pull-left">
                                        <h4><strong>Seções </strong></h4>
                                    </span>
                                </div>
                                <div class="panel-group payments-method" id="accordion">
                                    <%-- Início Acordeon --%>
                                    <div class="panel panel-default">
                                        <ajaxToolkit:Accordion ID="acSecoes" runat="server" FadeTransitions="false"
                                            FramesPerSecond="40" RequireOpenedPane="False" SuppressHeaderPostbacks="True"
                                            TransitionDuration="200" OnItemDataBound="acSecoes_ItemDataBound" SelectedIndex="-1">
                                            <HeaderTemplate>
                                                <div class="panel-heading">
                                                    <asp:HyperLink ID="hlcollapse" NavigateUrl="#" runat="server">
                                                        <div class="pull-left">
                                                            <small><i class="fas fa-folder-open fa-lg" style="color: #00ba97; font-size: 18px"></i></small>&nbsp;&nbsp;
                                                        </div>
                                                        <h4 class="panel-title pull-left">
                                                            <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("Id") %>' />
                                                            <asp:Label ID="lblSecao" Style="color: #2c3f51" runat="server" Text='<%# Eval("Nome") %>'></asp:Label>
                                                        </h4>
                                                    </asp:HyperLink>
                                                </div>
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class=" col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                            <div class="ibox table-responsive">
                                                                <!-- Repeater Filho -->
                                                                <asp:Repeater ID="rptArquivos" runat="server" OnItemDataBound="rptArquivos_ItemDataBound">

                                                                    <HeaderTemplate>
                                                                        <table id="tblBiblioteca" class="table table-striped projects dataTables-example">
                                                                            <thead class="bg-info">
                                                                                <tr>
                                                                                    <th style="width: 35%">
                                                                                        <asp:Label ID="lblCampo" Style="color: #2c3f51" runat="server" Text="Título"></asp:Label>
                                                                                    </th>
                                                                                    <th style="width: 35%">
                                                                                        <asp:Label ID="Label1" Style="color: #2c3f51" runat="server" Text="Descrição"></asp:Label>
                                                                                    </th>
                                                                                    <th style="width: 20%">
                                                                                        <asp:Label ID="Label2" Style="color: #2c3f51" runat="server" Text="Autor"></asp:Label>
                                                                                    </th>
                                                                                    <th style="width: 10%" colspan="2">
                                                                                        <asp:Label ID="Label3" Style="color: #2c3f51" runat="server" Text="Ano"></asp:Label>
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:HiddenField ID="hfIdArquivo" runat="server" Value='<%# Eval("IdBiblio") %>' />
                                                                                <asp:HyperLink ID="hlNomeArq" runat="server" Target="_blank" Text='<%# Eval("NomeArq") %>'></asp:HyperLink>
                                                                                <asp:Label ID="lblNomeArq" runat="server" Text='<%# Eval("NomeArq") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblDescricao" runat="server" Text='<%# Eval("Descricao") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblAutor" runat="server" Text='<%# Eval("Autor") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblAno" runat="server" Text='<%# Eval("Ano") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                                <!-- end Repeater Filho -->
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </ajaxToolkit:Accordion>
                                    </div>
                                    <%-- Fim Acordeon --%>
                                </div>
                            </div>
                            <div class="modal-footer col-sm-12 col-lg-12">
                                <div class="btn-group" role="group">
                                    <asp:LinkButton runat="server" ID="lbCancelar" CssClass="btn btn-default" OnClick="lbCancelar_Click"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
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
                NutroVET by <strong>SICORP &copy;<asp:Label ID="lblAno" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </div>

</asp:Content>
