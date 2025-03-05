<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master"
    AutoEventWireup="true" CodeBehind="AlimentosGerenciaCad.aspx.cs"
    Inherits="Nutrovet.Cadastros.AlimentosGerenciaCad" ValidateRequest="false" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {
                $("#tbPercentualCarboidrato").TouchSpin({
                    min: 0,
                    max: 100000,
                    step: 0.05,
                    decimals: 2,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet'
                });
                $("#tbNdb").TouchSpin({
                    min: 0,
                    max: 100000,
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

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <div class="col-lg-1">
                <asp:HyperLink ID="hlkMinimalize" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
            </div>
            <h2>
                <asp:Label ID="lblPagina" runat="server" Text=""></asp:Label></h2>
            <div class="col-lg-8">
                <ol class="breadcrumb">
                    <li>
                        <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                    </li>
                    <li class="active">
                        <asp:HyperLink ID="hlAlimentos" NavigateUrl="~/Cadastros/AlimentosGerencia.aspx" runat="server"><i class="fas fa-utensils"></i>&nbsp;<strong>&nbsp;Gerenciamento</strong></asp:HyperLink>
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
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
            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            <strong>
                                <asp:Label ID="lblSubTitulo" runat="server" Text=""></asp:Label></strong>
                        </h5>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-lg-2 control-label">Fonte</label>
                            <div class="col-lg-6">
                                <asp:DropDownList ID="ddlFonte" class="form-control m-b" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-lg-2 control-label">Grupo</label>
                            <div class="col-lg-6">
                                <asp:DropDownList ID="ddlGrupo" class="form-control m-b" runat="server" TabIndex="1">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-lg-2 control-label">Alimento</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="tbAlimento" class="form-control m-b" placeholder="Alimento" runat="server" TabIndex="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class="i-checks" id="divCompartilhar" runat="server">
                                <br />
                                <label class="col-lg-2 control-label p-sm">Compartilhar </label>
                                <div class="btn-group col-lg-6 p-xs m-xs" role="group" aria-label="...">
                                    <asp:Button ID="btnSim" runat="server" CssClass="btn btn-default" Text="Sim" OnClick="btnSim_Click" />
                                    <asp:Button ID="btnNao" runat="server" CssClass="btn btn-primary-nutrovet" Text="Não" OnClick="btnNao_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-8">
                            <div class="ibox col">
                                <div class="ibox-title">
                                    <h4><strong>Nutrientes</strong></h4>
                                </div>
                                <div class="panel-group payments-method" id="accordion">
                                    <%-- Início Acordeon --%>
                                    <div class="panel panel-default">
                                        <ajaxToolkit:Accordion ID="acGrupos" runat="server" FadeTransitions="false"
                                            FramesPerSecond="40" RequireOpenedPane="False" SuppressHeaderPostbacks="True"
                                            TransitionDuration="200" OnItemDataBound="acGrupos_ItemDataBound" SelectedIndex="0">
                                            <HeaderTemplate>
                                                <div class="panel-heading">
                                                    <asp:HyperLink ID="hlcollapse" NavigateUrl="#" runat="server">
                                                        <div class="pull-left">
                                                            <i class="fas fa-folder-open" style="color: #00ba97"></i>
                                                        </div>
                                                        <h4 class="panel-title ">
                                                            <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("IdGrupo") %>' />
                                                            <asp:Label ID="lblGrupo" Style="color: #2c3f51" runat="server" Text='<%# Eval("Grupo") %>'></asp:Label>
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
                                                                <asp:Repeater ID="rptNutrientes" runat="server" OnItemCommand="rptNutrientes_ItemCommand"
                                                                    OnItemDataBound="rptNutrientes_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <table class="table table-striped projects dataTables-example">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th style="width: 40%">
                                                                                        <asp:Label ID="lblCampo" Style="color: #2c3f51" runat="server" Text="Nutriente"></asp:Label>
                                                                                    </th>
                                                                                    <th style="width: 15%">
                                                                                        <asp:Label ID="lblValor" Style="color: #2c3f51" runat="server" Text="Valor"></asp:Label>
                                                                                    </th>
                                                                                    <th style="width: 15%">
                                                                                        <asp:Label ID="lblUnidade" Style="color: #2c3f51" runat="server" Text="Unidade"></asp:Label>
                                                                                    </th>
                                                                                    <th>Ação</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:HiddenField ID="hfIdNutr" runat="server" Value='<%# Eval("IdNutr") %>' />
                                                                                <asp:Label ID="lblNutriente" runat="server"
                                                                                    Text='<%# Eval("Nutriente") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 12%">
                                                                                <cc1:MEdit ID="meValor" class="form-control" runat="server"
                                                                                    Mascara="Float" Style="width: 80px"></cc1:MEdit>
                                                                            </td>
                                                                            <td style="width: 10%">
                                                                                <asp:DropDownList ID="ddlUnidade" class="form-control btn btn-default" Style="width: 80px"
                                                                                    runat="server" Enabled="False">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="lbSalvar" class="form-control" runat="server"
                                                                                    CssClass="btn  btn-primary-nutrovet" CommandName="salvar"
                                                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdNutr") %>'>
                                                                                    <i class="fas fa-save"></i> Salvar </asp:LinkButton>
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
                                    <asp:LinkButton runat="server" ID="lbCancelar" CssClass="btn btn-sm btn-default m-t-n-xs" OnClick="lbFechar_Click"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbSalvar" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" OnClick="lbSalvar_Click"> <i class='far fa-save'></i> Salvar</asp:LinkButton>
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
