<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="TesteTabControl.aspx.cs" Inherits="Nutrovet.Temp.TesteTabControl"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        $(function () {
            SetTabs();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetTabs();
                }
            });
        };

        function SetTabs() {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tabDadosPaciente";

            $('#Tabs a[href="#' + tabName + '"]').tab('show');

            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        };

        function BindEvents() {
            $(document).ready(function () {
                var g1 = new JustGage({
                    id: 'g1',
                    value: 10,
                    min: 0,
                    max: 100,
                    label: "Carboidrato",
                    symbol: '%',
                    pointer: true,
                    pointerOptions: {
                        toplength: -15,
                        bottomlength: 10,
                        bottomwidth: 12,
                        color: '#8e8e93',
                        stroke: '#ffffff',
                        stroke_width: 3,
                        stroke_linecap: 'round'
                    },
                    gaugeWidthScale: 0.7,
                    counter: true,
                    relativeGaugeSize: true
                });

                var g2 = new JustGage({
                    id: 'g2',
                    value: 25,
                    min: 0,
                    max: 100,
                    label: "Proteína",
                    symbol: '%',
                    pointer: true,
                    pointerOptions: {
                        toplength: -15,
                        bottomlength: 10,
                        bottomwidth: 12,
                        color: '#8e8e93',
                        stroke: '#ffffff',
                        stroke_width: 3,
                        stroke_linecap: 'round'
                    },
                    gaugeWidthScale: 0.7,
                    counter: true,
                    relativeGaugeSize: true
                });

                var g3 = new JustGage({
                    id: 'g3',
                    value: 40,
                    min: 0,
                    max: 100,
                    symbol: '%',
                    label: "Gordura",
                    pointer: true,
                    pointerOptions: {
                        toplength: -15,
                        bottomlength: 10,
                        bottomwidth: 12,
                        color: '#8e8e93',
                        stroke: '#ffffff',
                        stroke_width: 3,
                        stroke_linecap: 'round'
                    },
                    gaugeWidthScale: 0.7,
                    counter: true,
                    relativeGaugeSize: true
                });

                var g4 = new JustGage({
                    id: 'g4',
                    value: 58,
                    min: 0,
                    max: 100,
                    label: "Carboidrato",
                    title: "downloas",
                    titleFontSize: 12,
                    titlePosition: "below",
                    symbol: '%',
                    pointer: true,
                    pointerOptions: {
                        toplength: -15,
                        bottomlength: 10,
                        bottomwidth: 12,
                        color: '#8e8e93',
                        stroke: '#ffffff',
                        stroke_width: 3,
                        stroke_linecap: 'round'
                    },
                    gaugeWidthScale: 0.7,
                    counter: true,
                    relativeGaugeSize: true
                });

                var g5 = new JustGage({
                    id: 'g5',
                    value: 18,
                    min: 0,
                    max: 100,
                    label: "Proteína",
                    symbol: '%',
                    pointer: true,
                    pointerOptions: {
                        toplength: -15,
                        bottomlength: 10,
                        bottomwidth: 12,
                        color: '#8e8e93',
                        stroke: '#ffffff',
                        stroke_width: 3,
                        stroke_linecap: 'round'
                    },
                    gaugeWidthScale: 0.7,
                    counter: true,
                    relativeGaugeSize: true
                });

                var g6 = new JustGage({
                    id: 'g6',
                    value: 71,
                    min: 0,
                    max: 100,
                    symbol: '%',
                    label: "Gordura",
                    pointer: true,
                    pointerOptions: {
                        toplength: -15,
                        bottomlength: 10,
                        bottomwidth: 12,
                        color: '#8e8e93',
                        stroke: '#ffffff',
                        stroke_width: 3,
                        stroke_linecap: 'round'
                    },
                    gaugeWidthScale: 0.7,
                    counter: true,
                    relativeGaugeSize: true
                });

                setInterval(function () {
                    g1.refresh(getRandomInt(0, 100));
                    g2.refresh(getRandomInt(0, 100));
                    g3.refresh(getRandomInt(0, 100));
                    g4.refresh(getRandomInt(0, 100));
                    g5.refresh(getRandomInt(0, 100));
                    g6.refresh(getRandomInt(0, 100));
                }, 2000); // update the charts every 5 seconds.

                $("#tbPesoIdeal").TouchSpin({
                    min: 0,
                    max: 200,
                    step: 0.05,
                    decimals: 2,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary',
                    buttondown_class: 'btn btn-primary',
                });

                $("#tbPesoAtual").TouchSpin({
                    min: 0,
                    max: 200,
                    step: 0.05,
                    decimals: 2,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary',
                    buttondown_class: 'btn btn-primary',
                });

                $("#tbFator").TouchSpin({
                    min: 0,
                    max: 2000,
                    step: 1,
                    decimals: 0,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary',
                    buttondown_class: 'btn btn-primary',
                });

                $("#tbQuantidadeAlimento").TouchSpin({
                    min: 0,
                    max: 1500,
                    step: 1,
                    decimals: 0,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary',
                    buttondown_class: 'btn btn-primary',
                });

                $("#tbNrFilhotes").TouchSpin({
                    min: 0,
                    max: 30,
                    step: 1,
                    decimals: 0,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary',
                    buttondown_class: 'btn btn-primary',
                });
            });
        }
    </script>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <div class="col-lg-1">
                <asp:HyperLink ID="hlkMinimalize" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-white m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
            </div>
            <h2>
                <asp:Label ID="lblPagina" runat="server" Text=""></asp:Label></h2>
            <div class="col-lg-4">
                <ol class="breadcrumb">
                    <li>
                        <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"> Início</asp:HyperLink>
                    </li>
                    <li class="active">
                        <asp:HyperLink ID="hlCardapioSelecao" NavigateUrl="~/Cardapio/CardapioSelecao.aspx" runat="server"> Cardápios</asp:HyperLink>
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
            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            <asp:Label ID="lblSubTitulo" runat="server" Text="Insira ou altere os dados do Cardápio"></asp:Label>
                        </h5>
                    </div>
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div id="Tabs" role="tabpanel" class="tabs-container">
                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs" role="tablist">
                                    <li>
                                        <a href="#tabDadosPaciente" aria-controls="tabDadosPaciente" role="tab" data-toggle="tab"><i class="fa fa-paw"></i>Dados do Paciente</a>
                                    </li>
                                    <li>
                                        <a href="#tabComposicaoCardapio" aria-controls="tabDadosPaciente" role="tab" data-toggle="tab"><i class="fa fa-list-ol"></i>Composição do Cardápio</a>
                                    </li>
                                    <li>
                                        <a href="#tabNutrientesCardapio" aria-controls="tabDadosPaciente" role="tab" data-toggle="tab"><i class="fa fa-book"></i>Nutrientes do Cardápio</a>
                                    </li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content">
                                    <div role="tabpanel" class="tab-pane active" id="tabDadosPaciente">
                                        <div class="panel-body">
                                            <div class="ibox-content">
                                                <div class="form-group">
                                                    <label id="lblTutor" class="col-sm-2 control-label">Tutor</label>
                                                    <div class="col-sm-5">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style=""><i class="fas fa-user fa-fw"></i></span>
                                                            <asp:DropDownList ID="ddlTutor" runat="server" AutoPostBack="True" class="form-control" data-toggle="tooltip" data-placement="top" title="Selecione o Tutor" OnSelectedIndexChanged="ddlTutor_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 form-group ">
                                                    <asp:Chart ID="Chart1" runat="server">
                                                        <series>
                                                            <asp:Series ChartType="Point" Name="Series1">
                                                            </asp:Series>
                                                        </series>
                                                        <chartareas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                            </asp:ChartArea>
                                                        </chartareas>
                                                    </asp:Chart>
                                                </div>
                                                <div class="form-group">
                                                    <label id="lblNomePaciente" class="col-sm-2 control-label">Paciente</label>
                                                    <div class="col-lg-5">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style=""><i class="fas fa-paw fa-fw"></i></span>
                                                            <asp:DropDownList ID="ddlPaciente" runat="server" AutoPostBack="true" class="form-control" data-toggle="tooltip" data-placement="top" title="Selecione o Paciente" OnSelectedIndexChanged="ddlPaciente_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 form-group "></div>
                                                <div class="form-group">
                                                    <label id="lblIdade" class="col-lg-2 control-label">Idade</label>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><i class="fa fa-birthday-cake fa-fw"></i></span>
                                                            <asp:TextBox ID="tbIdadeAnos" name="tbIdadeAnos" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Idade em anos" ReadOnly="True"></asp:TextBox>
                                                            <span class="input-group-addon" style="">anos</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="divEspacoGestante" class="col-lg-12 form-group" runat="server" visible="false"></div>
                                                <div id="divGestante" class="form-group" runat="server" visible="false">
                                                    <label id="lblGestante" class="col-sm-2 control-label">Gestante</label>
                                                    <div class="btn-group col-lg-5" role="group" aria-label="...">
                                                        <asp:Button ID="btnSimGestante" runat="server" CssClass="btn btn-default" Text="Sim" OnClick="btnSimGestante_Click" />
                                                        <asp:Button ID="btnNaoGestante" runat="server" CssClass="btn btn-success" Text="Não" OnClick="btnNaoGestante_Click" />
                                                    </div>
                                                </div>
                                                <div id="divEspacoLactante" class="col-lg-12 form-group " runat="server" visible="false"></div>
                                                <div id="divLactante" class="form-group" runat="server" visible="false">
                                                    <label id="lblLactante" class="col-sm-2 control-label">Lactante</label>
                                                    <div class="btn-group col-lg-5" role="group" aria-label="...">
                                                        <asp:Button ID="btnSimLactante" runat="server" CssClass="btn btn-default" Text="Sim" OnClick="btnSimLactante_Click" />
                                                        <asp:Button ID="btnNaoLactante" runat="server" CssClass="btn btn-success" Text="Não" OnClick="btnNaoLactante_Click" />
                                                    </div>
                                                    <div class="col-lg-12 form-group "></div>
                                                    <div class="form-group">
                                                        <label id="lblNrFilhotes" class="col-lg-2 control-label">Nr Filhotes</label>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox ID="tbNrFilhotes" ClientIDMode="Static" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Nr Filhotes"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 form-group "></div>
                                                <div class="form-group">
                                                    <label id="lblAtual" class="col-lg-2 control-label">Peso Atual</label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="tbPesoAtual" ClientIDMode="Static" runat="server" placeholder="0,00" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Peso Atual"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 form-group "></div>
                                                <div class="form-group">
                                                    <label id="lblPesoIdeal" class="col-lg-2 control-label">Peso Ideal</label>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="tbPesoIdeal" ClientIDMode="Static" runat="server" placeholder="0,00" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Peso ideal (kg)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="hfID" runat="server" />
                                                <%--<div class="col-lg-12 form-group "></div>--%>
                                            </div>
                                            <div class="ibox-content">
                                                <div class="form-group">
                                                    <label id="lblSugestaoDieta" class="col-sm-2 control-label">Sugestão de Dieta</label>
                                                    <div class="col-sm-5">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" style=""><i class="fas fa-balance-scale fa-fw"></i></span>
                                                            <asp:DropDownList ID="ddlSugestaoDieta" runat="server" AutoPostBack="False" class="form-control" data-toggle="tooltip" data-placement="top" title="Sugestão de Dieta">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 form-group "></div>

                                                <div class="form-group">
                                                    <label id="lblFator" class="col-lg-2 control-label">Fator</label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="tbFator" ClientIDMode="Static" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Fator"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 form-group "></div>

                                                <div class="form-group">
                                                    <label id="lblNem" class="col-lg-2 control-label">NEM</label>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="tbNEM" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="NEM" ReadOnly="true"></asp:TextBox>
                                                            <span class="input-group-addon" style="">kcal</span>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-12 form-group "></div>
                                            </div>
                                            <div class="ibox-content col--8">
                                                <asp:LinkButton runat="server" ID="lbSalvar" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" OnClick="lbSalvar_Click"><i class="far fa-save"></i> Salvar</asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbFechar" CssClass="btn btn-sm btn-white pull-right m-t-n-xs" OnClick="lbFechar_Click"><i class="fas fa-door-open"></i> Fechar </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="tabComposicaoCardapio">
                                        <div class="panel-body">
                                            <!-- cardapio.painel-cardapio-->
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="ibox float-e-margins">
                                                        <div class="ibox-title">
                                                            <h4>Cardápio total diário
                                                            </h4>
                                                        </div>
                                                        <div class="ibox-content">
                                                            <div class="form-group">
                                                                <div class="form-group">
                                                                    <label id="lblAlimentos" class="col-lg-3 control-label">Alimentos</label>
                                                                    <div class="input-group col-lg-9">
                                                                        <asp:TextBox ID="tbPesqAlimentos" runat="server" placeholder="Pesquisar Alimentos" CssClass="form-control m-b "></asp:TextBox>
                                                                        <div class="input-group-btn">
                                                                            <asp:Button ID="btnPesqIndic" runat="server" Text="Button" CssClass="form-control" OnClick="btnPesqAlimentos_Click" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div id="divListaAlimentos" runat="server" class="x_content table-responsive col-lg-offset-3 col-lg-9 " visible="false">
                                                                            <asp:ListBox ID="ltbAlimmentos" runat="server" class="form-control" SelectionMode="Multiple"></asp:ListBox>
                                                                            <asp:Button ID="lbFechaPesqAlimmentos" runat="server" Text="OK" CssClass="btn btn-sm btn-primary pull-right " OnClick="lbFechaListaAlimmentos_Click" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="flexbox">
                                                                <div class="box col-lg-3">
                                                                    <label id="lblQuantidade" class="control-label">Quantidade <sub>(g)</sub></label>
                                                                </div>
                                                                <div class="box col-lg-4 col-span-lg-1">
                                                                    <asp:TextBox ID="tbQuantidadeAlimento" ClientIDMode="Static" runat="server" placeholder="0" CssClass="col-lg-3 form-control" data-toggle="tooltip" data-placement="top" title="Quantidade do alimento em gramas"></asp:TextBox>
                                                                </div>
                                                                <div class="box col-lg-4 pull-right">
                                                                    <asp:Button ID="lbIncluirNoCardapio" runat="server" Text="Incluir no Cardápio" CssClass="btn btn-sm btn-primary" />
                                                                </div>
                                                            </div>
                                                            <div class="sk-spinner sk-spinner-double-bounce">
                                                                <div class="sk-double-bounce1"></div>
                                                                <div class="sk-double-bounce2"></div>
                                                            </div>
                                                            <table class="table table-stripped" id="table-alimentos-cardapio">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="width: 60%">Alimento</th>
                                                                        <th style="width: 25%">Quantidade <sub>(g)</sub></th>
                                                                        <th style="width: 15%">&nbsp;</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr class="linha-alimento" data-id-alimento="17">
                                                                        <td>
                                                                            <span>Batata Doce Cozida</span><br />
                                                                            <asp:HyperLink ID="hlInfoNutr" runat="server" data-toggle="modal" data-target="#modal-detalhe-alimento-17"><i class="fas fa-search-plus"></i><small>Informações nutricionais</small></asp:HyperLink>

                                                                            <div class="modal inmodal" id="modal-detalhe-alimento-17" tabindex="-1" role="dialog" aria-hidden="true">
                                                                                <div class="modal-dialog">
                                                                                    <div class="modal-content animated fadeIn">
                                                                                        <div class="modal-header">
                                                                                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>
                                                                                            <i class="fa fa-bar-chart-o modal-icon"></i>
                                                                                            <h4 class="modal-title">Batata Doce Cozida</h4>
                                                                                            <small>Informações Nutricionais</small>
                                                                                        </div>
                                                                                        <div class="modal-body" id="content-informacoes-nutricionais-17">
                                                                                            <div class="scroll_content">
                                                                                                <table class="table">
                                                                                                    <thead>
                                                                                                        <tr>
                                                                                                            <th>Nutriente</th>
                                                                                                            <th style="width: 100px;">Vl.</th>
                                                                                                        </tr>
                                                                                                    </thead>
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td>ENERGIA</td>
                                                                                                            <td><span class="badge badge-success">105.000 kcal</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>UMIDADE</td>
                                                                                                            <td><span class="badge badge-success">72.800 </span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>CARBOIDRATO</td>
                                                                                                            <td><span class="badge badge-success">24.300 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>PROTEÍNA</td>
                                                                                                            <td><span class="badge badge-success">1.660 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>GORDURA TOT</td>
                                                                                                            <td><span class="badge badge-success">0.300 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>GORDURA POLI</td>
                                                                                                            <td><span class="badge badge-success">0.130 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>GORDURA MONO</td>
                                                                                                            <td><span class="badge badge-success">0.010 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>GORDURA SAT</td>
                                                                                                            <td><span class="badge badge-success">0.080 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>GORDURA TRANS</td>
                                                                                                            <td><span class="badge badge-success">0.000 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>COLESTEROL</td>
                                                                                                            <td><span class="badge badge-success">0.000 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>FIBRA TOT</td>
                                                                                                            <td><span class="badge badge-success">2.300 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>FIBRA SOL</td>
                                                                                                            <td><span class="badge badge-success">1.100 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>FIBRA INS</td>
                                                                                                            <td><span class="badge badge-success">1.200 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>VIT A</td>
                                                                                                            <td><span class="badge badge-success">1705.000 UI</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>VIT D</td>
                                                                                                            <td><span class="badge badge-success">0.000 UI</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>VIT E</td>
                                                                                                            <td><span class="badge badge-success">3.500 UI</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>ÁC FÓLICO</td>
                                                                                                            <td><span class="badge badge-success">11.100 mcg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>VIT C</td>
                                                                                                            <td><span class="badge badge-success">17.100 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>VIT B1</td>
                                                                                                            <td><span class="badge badge-success">0.050 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>VIT B2</td>
                                                                                                            <td><span class="badge badge-success">0.140 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>VIT B6</td>
                                                                                                            <td><span class="badge badge-success">0.240 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>VIT B12</td>
                                                                                                            <td><span class="badge badge-success">0.000 mcg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>NIACINA</td>
                                                                                                            <td><span class="badge badge-success">0.640 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>ÁC PANTOTÊNICO</td>
                                                                                                            <td><span class="badge badge-success">0.530 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>CALCIO</td>
                                                                                                            <td><span class="badge badge-success">21.000 g</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>COBRE</td>
                                                                                                            <td><span class="badge badge-success">0.160 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>FERRO</td>
                                                                                                            <td><span class="badge badge-success">0.560 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>IODO</td>
                                                                                                            <td><span class="badge badge-success">2.000 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>MAGNÉSIO</td>
                                                                                                            <td><span class="badge badge-success">10.000 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>MANGANÊS</td>
                                                                                                            <td><span class="badge badge-success">0.340 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>POTÁSSIO</td>
                                                                                                            <td><span class="badge badge-success">1.840 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>FÓSFORO</td>
                                                                                                            <td><span class="badge badge-success">27.000 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>SELÊNIO</td>
                                                                                                            <td><span class="badge badge-success">0.700 mcg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>SÓDIO</td>
                                                                                                            <td><span class="badge badge-success">13.000 mg</span></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>ZINCO</td>
                                                                                                            <td><span class="badge badge-success">0.270 mg</span></td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="modal-footer">
                                                                                            <button type="button" class="btn btn-white" data-dismiss="modal">Fechar</button>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="ibox float-e-margins">
                                                        <div class="ibox-title">
                                                            <h4>Detalhes do Cardápio</h4>
                                                        </div>
                                                        <div class="ibox-content">
                                                            <h5>Sugestão para <span id="name-suggest1"></span>: </h5>
                                                            <div class="wrapper">
                                                                <div class="flexbox">
                                                                    <div class="box col-lg-4">
                                                                        <div id="g1" class="gauge"></div>
                                                                    </div>
                                                                    <div class="box col-lg-4">
                                                                        <div id="g2" class="gauge"></div>
                                                                    </div>
                                                                    <div class="box col-lg-4">
                                                                        <div id="g3" class="gauge"></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <h5>Distribuição dos nutrientes da Dieta </h5>
                                                            <div class="wrapper">
                                                                <div class="flexbox">
                                                                    <div class="box col-lg-4">
                                                                        <div id="g4" class="gauge"></div>
                                                                    </div>
                                                                    <div class="box col-lg-4">
                                                                        <div id="g5" class="gauge"></div>
                                                                    </div>
                                                                    <div class="box col-lg-4">
                                                                        <div id="g6" class="gauge"></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox float-e-margins">
                                                        <div class="ibox">
                                                            <div class="ibox-title">
                                                                <h5>Balanceamento Geral</h5>
                                                            </div>
                                                            <div class="ibox-content">
                                                                <div class="row">
                                                                    <div class="col-lg-6">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-heading text-center">
                                                                                <i class="fas fa-tablets"></i>Fibra
                                                                            </div>
                                                                            <div class="panel-body text-center">
                                                                                <span class="label label-success vlFibras text-center">g</span>
                                                                                <span class="label label-success percFibras text-center">%</span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-6">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-heading text-center">
                                                                                <i class="fas fa-fill-drip"></i>Umidade
                                                                            </div>
                                                                            <div class="panel-body text-center">
                                                                                <span class="label label-success vlUmidade text-center">g</span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-lg-6">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-heading text-center">
                                                                                <i class="fas fa-bolt"></i>Energia Requerida (NEM)
                                                                            </div>
                                                                            <div class="panel-body text-center">
                                                                                <span id="nemDoCardapio" class="label label-primary">000 kCal</span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-6">
                                                                        <div class="panel panel-danger">
                                                                            <div class="panel-heading text-center">
                                                                                <i class="fas fa-atom"></i>Energia Presente (EM)
                                                                            </div>
                                                                            <div class="panel-body text-center">
                                                                                <span id="emDoCardapio" class="label label-primary emDoCardapio">Kcal</span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="ibox-content col-lg-12">
                                                                <asp:Button ID="lbImpressaoCardapio" runat="server" Text=" Imprimir Cardápio " CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" />
                                                                <asp:HyperLink ID="lbResumoMaisDetalhes" runat="server" data-toggle="modal" data-target="#modal-resumo-cardapio" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs"><i class="fas fa-search-plus"></i><small> Mais Detalhes </small></asp:HyperLink>
                                                            </div>
                                                        </div>

                                                        <div class="modal inmodal" id="modal-resumo-cardapio" tabindex="-1" role="dialog" aria-hidden="true">
                                                            <div class="modal-dialog">
                                                                <div class="modal-content animated fadeIn">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>
                                                                        <i class="fa fa-bar-chart-o modal-icon"></i>
                                                                        <h4 class="modal-title">Resumo do Cardápio</h4>
                                                                    </div>
                                                                    <div class="modal-body" id="content-resumo-cardapio">
                                                                        <div class="scroll_content">
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-white" data-dismiss="modal">Fechar</button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="Button2" runat="server" Text="Button" />
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="tabNutrientesCardapio">
                                        <div class="panel-body">
                                            <strong>Nutrientes do Cardápio</strong>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="TabName" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="footer">
        <div class="pull-right">
            NutroVET by 
              <strong>SICORP &copy;
                  <asp:Label ID="lblAno" runat="server" Text=""></asp:Label>
              </strong>
        </div>
    </div>
</asp:Content>
