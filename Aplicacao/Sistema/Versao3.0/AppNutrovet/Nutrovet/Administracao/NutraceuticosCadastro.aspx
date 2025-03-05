<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="NutraceuticosCadastro.aspx.cs" Inherits="Nutrovet.Administracao.NutraceuticosCadastro"
    ValidateRequest="false" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {
                $("#meMDoseMinima").TouchSpin({
                    min: 0,
                    max: 100000,
                    step: 0.1,
                    decimals: 1,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet'
                });
                $("#meMDoseMaxima").TouchSpin({
                    min: 0,
                    max: 100000,
                    step: 0.1,
                    decimals: 1,
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
                    <li >
                         <asp:HyperLink ID="hlTitulo" NavigateUrl="~/Administracao/NutraceuticosSelecao.aspx" runat="server"><i class="fas fa-capsules"></i>&nbsp;Seleção de Nutracêuticos</asp:HyperLink>
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblTitulo" runat="server" Text="Insira ou altere os dados dos Nutracêuticos"></asp:Label>
                            
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
                            <asp:Label ID="lblSubTitulo" runat="server" Text="Insira ou altere os dados dos Nutracêuticos"></asp:Label>
                        </h5>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group">
                            <label class="col-lg-2 control-label">Espécie</label>
                            <div class="col-lg-6" data-toggle="tooltip" data-placement="top" title="Espécie">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fas fa-dog fa-flip-horizontal"></i><i class="fas fa-cat"></i></span>
                                    <asp:TextBox ID="tbEspecie" runat="server" CssClass="form-control col-lg-6" ReadOnly="True" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblTutor" class="col-lg-2 control-label">Grupo&nbsp;de&nbsp;Nutriente</label>
                            <div class="col-lg-6" data-toggle="tooltip" data-placement="top" title="Grupo de Nutriente">
                                <div class="input-group">
                                    <span class="input-group-addon" style=""><i class="fas fa-columns"></i></span>
                                    <asp:DropDownList ID="ddlGrpNutri" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrpNutri_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="tbGrpNutri" runat="server" CssClass="form-control col-lg-6" ReadOnly="True" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblNomePaciente" class="col-lg-2 control-label">Nutriente</label>
                            <div class="col-lg-6" data-toggle="tooltip" data-placement="top" title="Nutriente">
                                <div class="input-group">
                                    <span class="input-group-addon" style=""><i class="fas fa-drumstick-bite"></i></span>
                                    <asp:DropDownList ID="ddlNutri" class="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="tbNutri" runat="server" CssClass="form-control col-lg-6" ReadOnly="True" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblMDoseMinima" class="col-lg-2 control-label">Dose Mínima</label>
                            <div class="col-lg-3" data-toggle="tooltip" title="Valor da dose mínima do nutriente" data-placement="top">
                                <div class="input-group">
                                    <cc1:MEdit ID="meMDoseMinima" ClientIDMode="Static" runat="server" placeholder="0.0" CssClass="form-control" Mascara="Float"></cc1:MEdit>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblSexo" class="col-lg-2 control-label">Unidade</label>
                            <div class="col-lg-3" data-toggle="tooltip" title="Unidade da dose mínima do nutriente" data-placement="top">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fas fa-boxes"></i></span>
                                    <asp:DropDownList ID="ddlMUnDoseMinima" class="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblMDoseMaxima" class="col-lg-2 control-label">Dose Máxima</label>
                            <div class="col-lg-3" data-toggle="tooltip" title="Valor da dose máxima do nutriente" data-placement="top">
                                <div class="input-group">
                                    <cc1:MEdit ID="meMDoseMaxima" ClientIDMode="Static" runat="server" placeholder="0.0" CssClass="form-control" data-toggle="tooltip" title="Valor Dose Máxima" data-placement="top" Mascara="Float"></cc1:MEdit>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblDoseMax" class="col-lg-2 control-label">Unidade</label>
                            <div class="col-lg-3" data-toggle="tooltip" title="Unidade da dose máxima do nutriente" data-placement="top">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fas fa-boxes"></i></span>
                                    <asp:DropDownList ID="ddlMUnDoseMaxima" class="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblPesoIdeal" class="col-lg-2 control-label">Intervalo 1</label>
                            <div class="col-lg-3">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fas fa-layer-group"></i></span>
                                    <asp:DropDownList ID="ddlMIntervalo1" class="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblAtual" class="col-lg-2 control-label">Intervalo 2</label>
                            <div class="col-lg-3">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fas fa-layer-group"></i></span>
                                    <asp:DropDownList ID="ddlMIntervalo2" class="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblObs" class="col-lg-2 control-label">Observação</label>
                            <div class="col-lg-3">
                                <div class="input-group">
                                    <asp:TextBox ID="tbxObs" runat="server" CssClass="form-control" Rows="5" placeholder="Observação" required="true" TextMode="MultiLine" Width="540px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfID" runat="server" />
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                    </div>
                    <div class="modal-footer col-sm-8 col-lg-8">
                        <div class="btn-group" role="group">
                            <asp:LinkButton runat="server" ID="lbFechar" CssClass="btn btn-default" OnClick="lbFechar_Click"><i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbSalvar" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvar_Click"><i class='far fa-save'></i> Salvar</asp:LinkButton>
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
