<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="CardapioCadastro.aspx.cs" Inherits="Nutrovet.Cardapio.CardapioCadastro"
    ValidateRequest="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {

                $('#meDtNasc').datepicker({
                    format: "dd/mm/yyyy",
                    clearBtn: true,
                    language: "pt-BR",
                    daysOfWeekHighlighted: "0,6",
                    autoclose: true,
                    todayHighlight: true
                });

                $('#meDtInicial').datepicker({
                    format: "dd/mm/yyyy",
                    clearBtn: true,
                    language: "pt-BR",
                    daysOfWeekHighlighted: "0,6",
                    autoclose: true,
                    todayHighlight: true
                });

                $("#tbPesoIdeal").TouchSpin({
                    min: 0,
                    max: 200,
                    step: 0.05,
                    decimals: 2,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet',
                });

                $("#tbPesoAtual").TouchSpin({
                    min: 0,
                    max: 200,
                    step: 0.05,
                    decimals: 2,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet',
                });

                //$("#meBodyQuant").TouchSpin({
                //    min: 0,
                //    max: 200,
                //    step: 0.05,
                //    decimals: 2,
                //    boostat: 5,
                //    maxboostedstep: 10,
                //    mousewheel: true,
                //    buttonup_class: 'btn btn-primary-nutrovet',
                //    buttondown_class: 'btn btn-primary-nutrovet',
                //});

                $("#tbSemanLact").TouchSpin({
                    min: 0,
                    max: 7,
                    step: 1,
                    decimals: 0,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet',
                });

                $("#tbFator").TouchSpin({
                    min: 0,
                    max: 2000,
                    step: 1,
                    decimals: 0,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet',
                });

                $("#tbQuantidadeAlimento").TouchSpin({
                    min: 0,
                    max: 1500,
                    step: 0.05,
                    decimals: 2,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet',
                });

                $("#tbNrFilhotes").TouchSpin({
                    min: 0,
                    max: 8,
                    step: 1,
                    decimals: 0,
                    boostat: 5,
                    maxboostedstep: 10,
                    mousewheel: true,
                    buttonup_class: 'btn btn-primary-nutrovet',
                    buttondown_class: 'btn btn-primary-nutrovet',
                });

                $('#btnInfoNEM').on('click', function () {
                    $('#modalInfoNEM').iziModal('open');
                });

                $('#btnInfoLactante').on('click', function () {
                    $('#modalInfoLactante').iziModal('open');
                });

                //$('#btnInfoATAC').on('click', function () {
                //    //$('#modalInfoATAC').iziModal('open');
                //    chamarIzi();
                //});

                $('#btnInfoRecalc').on('click', function () {
                    $('#modalInfoPesoAtualAlterado').iziModal('open');
                });

                if ($('#modalInfoNEM').length) {
                    $('#modalInfoNEM').iziModal({
                        title: '<i class="fas fa-capsules"></i> Fórmula para o cálculo da Necessidade Energética Metabolizável (NEM)',
                        padding: '20px',
                        headerColor: '#2c3f51',
                        iconColor: 'red',
                        width: '60%',
                        overlayColor: 'rgba(0, 0, 0, 0.5)',
                        fullscreen: true,
                        transitionIn: 'bounceInDown',
                        transitionOut: 'fadeOutUp',
                        //timeout: 10000,
                        pauseOnHover: true,
                        timeoutProgressbar: true,
                        appendTo: 'body',
                        rtl: false,
                        bodyOverflow: false,
                        openFullscreen: false,
                        zindex: 9000
                    });
                }

                if ($('#modalInfoLactante').length) {
                    $('#modalInfoLactante').iziModal({
                        title: '<i class="fas fa-info-circle"></i> Informação sobre Lactação',
                        padding: '20px',
                        headerColor: '#2c3f51',
                        iconColor: 'red',
                        width: '60%',
                        overlayColor: 'rgba(0, 0, 0, 0.5)',
                        fullscreen: true,
                        transitionIn: 'bounceInDown',
                        transitionOut: 'fadeOutUp',
                        //timeout: 5000,
                        pauseOnHover: true,
                        timeoutProgressbar: true,
                        appendTo: 'body',
                        rtl: false,
                        bodyOverflow: false,
                        openFullscreen: false,
                        zindex: 9000
                    });
                }

                if ($('#modalInfoATAC').length) {
                    $('#modalInfoATAC').iziModal({
                        title: '<i class="fas fa-exclamation-triangle"></i>&nbsp;&nbsp;&nbsp;<strong>ATENÇÃO</strong>',
                        subtitle: 'Esta mensagem irá fechar automaticamente após 5 segundos, para interromper passe o mouse sobre a mensagem',
                        padding: '20px',
                        theme: 'light',
                        headerColor: '#FF8C00',
                        width: '50%',
                        overlayColor: 'rgba(0, 0, 0, 0.5)',
                        overlayClose: true,
                        fullscreen: true,
                        transitionIn: 'bounceInDown',
                        transitionOut: 'fadeOutUp',
                        timeout: 4000,
                        pauseOnHover: true,
                        autoOpen: false,
                        overlay: true,
                        timeoutProgressbar: true,
                        timeoutProgressbarColor: '#4183D7',
                        appendTo: 'body',
                        rtl: false,
                        bodyOverflow: true,
                        openFullscreen: false,
                        radius: 3,
                        zindex: 9000
                    });
                }

                if ($('#modalInfoPesoAtualAlterado').length) {
                    $('#modalInfoPesoAtualAlterado').iziModal({
                        title: '<i class="fas fa-info-circle"></i> AVISO - PESO ATUAL MODIFICADO',
                        padding: '20px',
                        headerColor: '#2c3f51',
                        iconColor: 'red',
                        width: '60%',
                        overlayColor: 'rgba(0, 0, 0, 0.5)',
                        fullscreen: true,
                        transitionIn: 'bounceInDown',
                        transitionOut: 'fadeOutUp',
                        //timeout: 5000,
                        pauseOnHover: true,
                        timeoutProgressbar: true,
                        appendTo: 'body',
                        rtl: false,
                        bodyOverflow: false,
                        openFullscreen: false,
                        zindex: 9000
                    });
                }
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

        function chamarIzi() {
            $('#modalInfoATAC').iziModal('open');
        }
    </script>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <div class="col-lg-1">
                <asp:HyperLink ID="hlkMinimalize" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
            </div>
            <h2>
                <asp:Label ID="lblPagina" runat="server" Text=""></asp:Label></h2>
            <div class="col-lg-6">
                <ol class="breadcrumb">
                    <li>
                        <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                    </li>
                    <li class="active">
                        <asp:HyperLink ID="hlCardapioSelecao" NavigateUrl="~/Cardapio/CardapioSelecao.aspx" runat="server"><i class="fas fa-balance-scale"></i> Cardápios</asp:HyperLink>
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
                <div>
                    <div class="ibox-title">
                        <h5>
                            <asp:Label ID="lblSubTitulo" runat="server" Text=""></asp:Label>
                        </h5>
                    </div>
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div id="Tabs" role="tabpanel" class="">
                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs" role="tablist">
                                    <li id="liDadosPac" runat="server" role="presentation" class="active">
                                        <asp:LinkButton ID="aDadosPac" class="text-center" runat="server" OnClick="aDadosPac_Click"><i class="fa fa-paw"></i>&nbsp;Paciente</asp:LinkButton>
                                    </li>
                                    <li id="liCompCard" runat="server" role="presentation">
                                        <asp:LinkButton ID="aCompCard" class="text-center" runat="server" OnClick="aCompCard_Click"><i class="fa fa-list-ol"></i>&nbsp;Composição</asp:LinkButton>
                                    </li>
                                    <li id="liNutrCard" runat="server" role="presentation">
                                        <asp:LinkButton ID="aNutrCard" class="text-center" runat="server" OnClick="aNutrCard_Click"><i class="fa fa-book"></i>&nbsp;Nutrientes</asp:LinkButton>
                                    </li>
                                </ul>
                                <!-- /Nav tabs -->
                                <!-- MultiView tabs -->
                                <div class="tab-content">
                                    <asp:MultiView ID="mvTabControl" runat="server" ActiveViewIndex="0" OnActiveViewChanged="mvTabControl_ActiveViewChanged">
                                        <asp:View ID="tabDadosPaciente" runat="server">
                                            <div class="panel-body">
                                                <div class="ibox-content col-lg-12">
                                                    <div class="form-group">
                                                        <label id="lblTutor" class="col-lg-2 control-label">Tutor</label>
                                                        <div class="col-lg-6 input-group">
                                                            <span class="input-group-addon" style=""><i class="fas fa-user fa-fw"></i></span>
                                                            <asp:DropDownList ID="ddlTutor" runat="server" AutoPostBack="True" class="form-control btn btn-default" data-toggle="tooltip" data-placement="top" title="Selecione o Tutor" OnSelectedIndexChanged="ddlTutor_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label id="lblNomePaciente" class="col-lg-2 control-label">Paciente</label>
                                                        <div class="col-lg-6 input-group">
                                                            <span class="input-group-addon" style=""><i class="fas fa-paw fa-fw"></i></span>
                                                            <asp:DropDownList ID="ddlPaciente" runat="server" AutoPostBack="true" class="form-control btn btn-default" data-toggle="tooltip" data-placement="top" title="Selecione o Paciente" OnSelectedIndexChanged="ddlPaciente_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label id="lblEspeciePaciente" class="col-lg-2 control-label">Espécie</label>
                                                        <div class="col-lg-6 input-group">
                                                            <span class="input-group-addon" style=""><i class="fas fa-dna fa-fw"></i></span>
                                                            <asp:TextBox ID="tbEspeciePaciente" runat="server" placeholder="Espécie" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Espécie do Paciente" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label id="lblraca" class="col-lg-2 control-label">Raça</label>
                                                        <div class="col-lg-6 input-group">
                                                            <span class="input-group-addon" style=""><i class="fas fa-bone fa-fw"></i></span>
                                                            <asp:TextBox ID="tbRacaPaciente" runat="server" placeholder="Raça" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Raça do Paciente" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label id="lblIdade" class="col-lg-2 control-label">Idade</label>
                                                        <div class="col-lg-6 input-group" style="overflow-x: auto;">
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="width: 5px"><i class="fa fa-birthday-cake fa-fw"></i></span>
                                                                <asp:TextBox ID="tbIdadeAnos" runat="server" Style="width: 45px" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Ano de Nascimento" ReadOnly="True"></asp:TextBox>
                                                                <span class="input-group-addon" style="width: 5px">Anos</span>
                                                                <asp:TextBox ID="tbIdadeMeses" runat="server" Style="width: 45px" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Mês de Nascimento" ReadOnly="True"></asp:TextBox>
                                                                <span class="input-group-addon" style="width: 5px">Meses</span>
                                                                <asp:TextBox ID="tbIdadeDias" runat="server" Style="width: 45px" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Dia de Nascimento" ReadOnly="True"></asp:TextBox>
                                                                <span class="input-group-addon" style="width: 5px">Dias</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divGestante" class="form-group" runat="server" visible="false">
                                                        <label id="lblGestante" class="col-lg-2 control-label">Gestante</label>
                                                        <div class="btn-group col-lg-10 input-group" role="group" aria-label="...">
                                                            <asp:Button ID="btnSimGestante" runat="server" CssClass="btn btn-nutroB" Text="Sim" OnClick="btnSimGestante_Click" />
                                                            <asp:Button ID="btnNaoGestante" runat="server" CssClass="btn btn-nutroS" Text="Não" OnClick="btnNaoGestante_Click" />
                                                        </div>
                                                    </div>
                                                    <div id="divLactante" class="form-group" runat="server" visible="false">
                                                        <label id="lblLactante" class="col-lg-2 control-label">
                                                            Lactante
                                                        <button type="button" id="btnInfoLactante" class="btn input-buttontotext fa" title='Informação sobre a lactação'>&#xf05a;</button>
                                                        </label>
                                                        <div class="btn-group col-lg-2 input-group" role="group" aria-label="...">
                                                            <asp:Button ID="btnSimLactante" runat="server" CssClass="btn btn-nutroB" Text="Sim" OnClick="btnSimLactante_Click" />
                                                            <asp:Button ID="btnNaoLactante" runat="server" CssClass="btn btn-nutroS" Text="Não" OnClick="btnNaoLactante_Click" />
                                                        </div>
                                                        <div id="modalInfoLactante" style="display: none">
                                                            <p>O cálculo da lactação considera o período até a sétima semana de lactação, depois desse período a dieta deve ser formulada normalmente sem a seleção do botão “lactante!</p>
                                                        </div>
                                                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                        <div id="divNrFilhotes" class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group" runat="server" visible="false">
                                                            <label id="lblNrFilhotes" class="col-lg-2 control-label">Nr Filhotes</label>
                                                            <div class="col-lg-4 input-group">
                                                                <asp:TextBox ID="tbNrFilhotes" ClientIDMode="Static" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Nr Filhotes"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                        <div id="divSemanasLactacao" class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group" runat="server" visible="false">
                                                            <label id="lblSemLact" class="col-lg-2 control-label">Semanas de Lactação</label>
                                                            <div class="col-lg-4 input-group">
                                                                <asp:TextBox ID="tbSemanLact" ClientIDMode="Static" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Semanas de Lactação"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label id="lblAtual" class="col-lg-2 control-label">Peso Atual</label>
                                                        <div class="col-lg-3">
                                                            <asp:TextBox ID="tbPesoAtual" ClientIDMode="Static" runat="server" placeholder="0,00" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Peso Atual (Kg)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-3 form-group">
                                                            <asp:Button ID="btnChama" runat="server" CssClass="btn input-buttontotext fa" Style="width: 32px; height: 32px;" OnClick="btnHistPesoAtual_Click" Text="&#xf201;" data-toggle="tooltip" data-placement="top" title="Histórico Peso Atual" />
                                                        </div>
                                                        <!-- Modal Historico Peso Atual -->
                                                        <div id="modalHistoricoPesoAtual" runat="server" class="modal-dialog modalControle" style="display: none">
                                                            <div class="modal-content animated fadeIn">
                                                                <div class="modal-header modal-header-success">
                                                                    <asp:LinkButton runat="server" ID="LinkButton1" CssClass="close" data-dismiss="modal" ForeColor="White"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></asp:LinkButton>
                                                                    <h4 class="modal-title"><i class="fas fa-chart-line modal-icon"></i>
                                                                        <asp:Label ID="Label11" runat="server" Text=" - HISTÓRICO DE ALTERAÇÃO DO PESO ATUAL"></asp:Label>
                                                                    </h4>
                                                                </div>
                                                                <div class="modal-body" id="bodymodalHistoricoPesoAtual">
                                                                    <h4>Informe o período de pesquisa</h4>
                                                                    <div class="ibox-content">
                                                                        <div class="form-group">
                                                                            <label id="lblDataInicialPesquisa" class="col-lg-2 control-label">Data Inicial</label>
                                                                            <div class="col-lg-4">
                                                                                <div class="input-group">
                                                                                    <span class="input-group-addon" style=""><i class="fa fa-calendar fa-fw"></i></span>
                                                                                    <cc1:MEdit ID="meDtInicial" name="meDtInicial" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Data Inicial da Pesquisa"></cc1:MEdit>
                                                                                </div>
                                                                            </div>
                                                                            <label id="lblDataFinalPesquisa" class="col-lg-2 control-label">Data Final</label>
                                                                            <div class="col-lg-4">
                                                                                <div class="input-group">
                                                                                    <span class="input-group-addon" style=""><i class="fa fa-calendar fa-fw"></i></span>
                                                                                    <cc1:MEdit ID="meDtFinal" name="meDtFinal" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Data Final da Pesquisa"></cc1:MEdit>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <asp:Chart ID="chartPesoHist" runat="server" ImageStorageMode="UseImageLocation" ImageLocation="~/Imagens/Graficos/PesoHist.jpg">
                                                                        <Series>
                                                                            <asp:Series Name="Default" BorderColor="Black" BorderWidth="1" ChartType="Line"
                                                                                XValueMember="DataHistorico" YValueMembers="Peso">
                                                                            </asp:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <asp:ChartArea Name="ChartArea1" BorderWidth="0" BackColor="white">
                                                                            </asp:ChartArea>
                                                                        </ChartAreas>
                                                                    </asp:Chart>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <div class="pull-left">
                                                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                                                    </div>
                                                                    <div class="pull-right">
                                                                        <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-md btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <ajaxToolkit:ModalPopupExtender ID="mdHistoricoPesoAtual" runat="server"
                                                                PopupControlID="modalHistoricoPesoAtual" BackgroundCssClass="modalBackground"
                                                                RepositionMode="RepositionOnWindowResize"
                                                                TargetControlID="lblModalHistoricoPesoAtual" CancelControlID="LinkButton2">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                            <asp:Label ID="lblModalHistoricoPesoAtual" runat="server" Text=""></asp:Label>
                                                            <!-- Modal Histórico Peso Atual -->
                                                        </div>
                                                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>

                                                        <div class="form-group">
                                                            <label id="lblPesoIdeal" class="col-lg-2 control-label">Peso Ideal</label>
                                                            <div class="col-lg-3">
                                                                <asp:TextBox ID="tbPesoIdeal" ClientIDMode="Static" runat="server" placeholder="0,00" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Peso Ideal (Kg)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <asp:HiddenField ID="hfID" runat="server" />
                                                    </div>
                                                    <div class="ibox-content col-lg-8">
                                                        <div class="form-group">
                                                            <label id="lblTituloCardapio" class="col-lg-3 control-label">Título</label>
                                                            <div class="col-lg-9 input-group" data-toggle="tooltip" data-placement="top" title="Título do Cardápio">
                                                                <span class="input-group-addon" style=""><i class="fas fa-sign"></i></span>
                                                                <asp:TextBox ID="txbTituloCardapio" runat="server" placeholder="Título do Cardápio" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label id="lblSugestaoDieta" class="col-lg-3 control-label">Alvo da Dieta</label>
                                                            <div class="col-lg-9 input-group" data-toggle="tooltip" data-placement="top" title="Alvo da Dieta">
                                                                <span class="input-group-addon" style=""><i class="fas fa-balance-scale fa-fw"></i></span>
                                                                <asp:DropDownList ID="ddlSugestaoDieta" runat="server" AutoPostBack="False" class="form-control btn btn-default" data-toggle="tooltip" data-placement="top" title="Alvo da Dieta">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label id="lblFator" class="col-sm-3 col-lg-3 control-label">Fator</label>
                                                            <div class="col-sm-5 col-lg-5 input-group">
                                                                <asp:TextBox ID="tbFator" ClientIDMode="Static" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Fator"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label id="lblNem" class="col-sm-3 col-lg-3 control-label">
                                                                NEM
                                                            <button type="button" id="btnInfoNEM" class="btn input-buttontotext fa" title='Informação sobre Cálculo do NEM'>&#xf05a;</button>
                                                            </label>

                                                            <div class="col-sm-8 col-lg-8 input-group">
                                                                <asp:TextBox ID="tbNEM" runat="server" placeholder="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="NEM" ReadOnly="true"></asp:TextBox>
                                                                <span class="input-group-addon" style="">kcal</span>
                                                                <span class="input-group-addon btn btn-default">
                                                                    <asp:LinkButton runat="server" ID="lbCalculaNEM" CssClass="" OnClick="btnCalculaNEM_Click">
                                                                        <i class="fas fa-calculator"></i>&nbsp; Calcular
                    
                                                                    <button type="button" id="btnInfoRecalc" clientidmode="Static" runat="server" visible="false" class="btn btn_asp_icon-danger fa" title='Em caso de altaração do peso atual'>&#xf05a;</button>
                                                                    </asp:LinkButton></span>


                                                            </div>



                                                            <div id="modalInfoPesoAtualAlterado" style="display: none">
                                                                <asp:Label ID="lblRecalculo" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>



                                                            </div>


                                                            <!-- Central Modal -->
                                                            <div id="myModal" runat="server" class="modal-dialog modal-lg" style="display: none">
                                                                <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                                                                    <!--Content-->
                                                                    <div class="modal-content animated flipInY">
                                                                        <!--Header-->
                                                                        <div class="modal-header">
                                                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                                                <h4 class="modal-title"><i class="fas fa-info-circle fa-fw"></i>
                                                                                    <asp:Label ID="lblTituloModal" class="heading lead" runat="server" Text="Nutrovet Informa"></asp:Label></h4>
                                                                            </div>
                                                                            <div class="col-md-3 col-sm-3 col-xs-3">
                                                                                <asp:LinkButton runat="server" ID="lbFecharModal" CssClass="close" data-dismiss="modal"> <i class='fa fa-times-circle'></i></asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <!--/Header-->
                                                                        <!--Body-->
                                                                        <div class="modal-body">
                                                                            <div class="row">
                                                                                <asp:Label ID="lblMsgModal" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                                                                            </div>
                                                                            <br />
                                                                        </div>
                                                                        <!--/Body-->
                                                                        <!--Footer-->
                                                                        <div class="modal-footer justify-content-center">
                                                                            <asp:LinkButton runat="server" ID="btnSim" CssClass="btn btn-sm btn-primary-nutrovet pull-right m-t-n-xs" OnClick="btnSim_Click"> <i class='far fa-save'></i>&nbsp;Sim </asp:LinkButton>
                                                                            <asp:LinkButton runat="server" ID="btnNão" CssClass="btn btn-sm btn-default pull-right m-t-n-xs" data-dismiss="modal" OnClick="btnNão_Click"> <i class='fas fa-door-open'></i>&nbsp;Não </asp:LinkButton>
                                                                        </div>
                                                                        <!--/Footer-->
                                                                    </div>
                                                                    <!--/Content-->
                                                                </div>
                                                            </div>
                                                            <ajaxToolkit:ModalPopupExtender ID="popUpModal" runat="server"
                                                                PopupControlID="myModal" BackgroundCssClass="modalBackground"
                                                                RepositionMode="RepositionOnWindowResize"
                                                                TargetControlID="lblPopUp">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                            <asp:Label ID="lblPopUp" runat="server" Text=""></asp:Label>
                                                            <!-- Central Modal -->
                                                        </div>
                                                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                    </div>
                                                    <div class="col-lg-8 modal-footer ">
                                                        <div class="btn-group" role="group">
                                                            <asp:LinkButton runat="server" ID="lbSalvar" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" OnClick="lbSalvar_Click"><i class="fas fa-hand-point-right"></i>&nbsp;Próximo</asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lbFechar" CssClass="btn btn-sm btn-default m-t-n-xs" OnClick="lbFechar_Click"><i class="fas fa-door-open"></i>&nbsp;Fechar </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>
                                        <asp:View ID="tabComposicaoCardapio" runat="server">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6">
                                                        <div class="ibox float-e-margins">
                                                            <div class="ibox-content">
                                                                <div class="form-group">
                                                                    <div class="form-group">
                                                                        <asp:Panel ID="pnlListaAlim" runat="server" DefaultButton="btnPesqAlimentos">
                                                                            <div class="input-group col-lg-12">
                                                                                <asp:TextBox ID="tbPesqAlimentos" runat="server" placeholder="Pesquisar por Alimentos, Grupos, Fontes ou Categorias" CssClass="form-control m-b " data-toggle="tooltip" data-placement="top" title="Pesquisar por Alimentos, Grupos, Fontes ou Categorias"></asp:TextBox>
                                                                                <div class="input-group-btn">
                                                                                    <asp:LinkButton ID="btnPesqAlimentos" CssClass="btn btn-primary-nutrovet" OnClick="btnPesqAlimentos_Click" runat="server" ToolTip="Executar a Pesquisa por alimentos, fontes, grupos ou categorias"><i class="fa fa-search"></i></asp:LinkButton>
                                                                                </div>
                                                                                <div class="input-group-btn">
                                                                                    <asp:LinkButton ID="btnCancelaAlimentos" CssClass="btn btn-danger" runat="server" OnClick="btnCancelaAlimentos_Click" ToolTip="Limpar a Pesquisa"><i class="far fa-trash-alt"></i></asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <asp:ListBox ID="ltbAlimentos" runat="server" Style="overflow-x: auto;" CssClass="x_content table-responsive col-lg-2 form-control" Visible="false" SelectionMode="Single" OnDataBound="ltbAlimentos_DataBound"></asp:ListBox>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>

                                                                <div class="flexbox">
                                                                    <div class="box col-lg-3">
                                                                        <label id="lblQuantidade" class="control-label">Quantidade&nbsp;<sub>(g)</sub></label>
                                                                    </div>
                                                                    <div class="box col-lg-5">
                                                                        <asp:TextBox ID="tbQuantidadeAlimento" ClientIDMode="Static" runat="server" placeholder="0,00" CssClass="col-lg-3 form-control" data-toggle="tooltip" data-placement="top" title="Quantidade do alimento em gramas"></asp:TextBox>
                                                                    </div>
                                                                    <div class="box col-lg-4">
                                                                        <%--<asp:LinkButton ID="lbIncluirNoCardapio" ToolTip="Incluir no Cardápio" CssClass="btn btn-primary-nutrovet"  runat="server"><i class="fas fa-plus-square"></i></asp:LinkButton>--%>
                                                                        <asp:Button ID="btnIncluirNoCardapio" runat="server" Text="&#xf0fe;" ToolTip="Incluir no Cardápio" CssClass="btn btn-primary-nutrovet fa" OnClick="btnIncluirNoCardapio_Click" />
                                                                    </div>
                                                                </div>
                                                                <div class="page-title">
                                                                    <div class="clearfix"></div>
                                                                    <div class="x_content table-responsive">
                                                                        <asp:Repeater ID="rptListagemAlimentos" runat="server" OnItemCommand="rptListagemAlimentos_ItemCommand" OnItemDataBound="rptListagemAlimentos_ItemDataBound">
                                                                            <HeaderTemplate>
                                                                                <table class="table table-striped projects dataTables-example" id="table-alimentos-cardapio">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th style="width: 1%">
                                                                                                <asp:Label ID="lblInfo" runat="server" Text="Info"></asp:Label>
                                                                                            </th>
                                                                                            <th style="width: 70%">
                                                                                                <asp:Label ID="lblHeaderAlimento" runat="server" Text="Alimento"></asp:Label>
                                                                                            </th>
                                                                                            <th style="width: 6%">
                                                                                                <asp:Label ID="lblHeaderQuant" runat="server" Text="Quantidade&nbsp;<sub>(g)</sub>"></asp:Label>
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr class="linha-alimento" data-id-alimento="17">
                                                                                    <td>
                                                                                        <asp:Button ID="btnInfoNutr" runat="server" title="Informações Nutricionais" CssClass="btn input-buttontotext fa" Text="&#xf05a;" CommandName="infoNutr" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCardapAlim") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblBodyAlimento" runat="server" Text='<%# Eval("Alimento") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <cc1:MEdit ID="meBodyQuant" class="form-control" ClientIDMode="Static" runat="server" placeholder="0,00" Mascara="Float" Text='<%# Eval("Quant") %>'></cc1:MEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="btnEditarAlim" title="Salvar alteração de quantidade" CssClass="btn btn_asp_icon far" Text="&#xf0c7;" runat="server" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCardapAlim") %>' />

                                                                                        <asp:Button ID="btnExcluirAlim" title="Excluir alimento" CssClass="btn btn_asp_icon-danger far" Text="&#xf2ed;" runat="server" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCardapAlim") %>' />
                                                                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Deseja Realmente Excluir Este Registro?" TargetControlID="btnExcluirAlim" />
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </tbody>
                                                                                    <tfoot>
                                                                                        <td colspan="2" style="text-align: center">
                                                                                            <span class="badge badge-primary" style="width: 70px">
                                                                                                <asp:Label ID="Label7" runat="server" Text="Total"></asp:Label>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td style="text-align: center">
                                                                                            <span class="badge badge-success" style="width: 70px">
                                                                                                <asp:Label ID="lblTotQuant" runat="server" Text="000"></asp:Label>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td>&nbsp;
                                                                                        </td>
                                                                                    </tfoot>
                                                                                </table>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>
                                                                <!-- Modal Alimentos -->
                                                                <div id="modalAlimentos" runat="server" class="modal-dialog modalControle" style="display: none">
                                                                    <div class="modal-content animated fadeIn">
                                                                        <div class="modal-header modal-header-success">
                                                                            <asp:LinkButton runat="server" ID="LinkButton3" CssClass="close" data-dismiss="modal" ForeColor="White"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></asp:LinkButton>
                                                                            <h4 class="modal-title"><i class="fa fa-bar-chart-o modal-icon"></i>
                                                                                <asp:Label ID="lblTituloModalAlimento" runat="server" Text=""></asp:Label><small style="color: #55f3d5;"> - Informações Nutricionais</small>
                                                                            </h4>
                                                                        </div>
                                                                        <div class="modal-body" id="content-informacoes-nutricionais-17">
                                                                            <asp:Repeater ID="rptListagemNutrientes" runat="server" OnItemDataBound="rptListagemNutrientes_ItemDataBound">
                                                                                <HeaderTemplate>
                                                                                    <table class="table">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th style="width: 70%">Nutriente
                                                                                                </th>
                                                                                                <th style="width: 10%">Valor<br />
                                                                                                    <span class="badge badge-warning" style="width: 70px">(100 g)
                                                                                                    </span>
                                                                                                </th>
                                                                                                <th style="width: 10%">Cardapio
                                                                                                            <asp:Label ID="lblQuant" class="badge badge-danger" Style="width: 70px" runat="server" Text=""></asp:Label>
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblNutriente" runat="server" Text='<%# Eval("Nutriente") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="badge badge-success" style="width: 70px">
                                                                                                <asp:Label ID="lblValorNutr" runat="server" Text='<%# Eval("Valor") %>'></asp:Label>&nbsp;<asp:Label ID="lblUnidade" runat="server" Text='<%# Eval("Unidade") %>'></asp:Label>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="badge badge-success" style="width: 70px">
                                                                                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("ValCalc") %>'></asp:Label>&nbsp;<asp:Label ID="Label8" runat="server" Text='<%# Eval("Unidade") %>'></asp:Label>
                                                                                            </span>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    </tbody>
                                                                                            </table>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <div class="modal-footer">
                                                                            <asp:LinkButton ID="btnModalAlimFechar" runat="server" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <ajaxToolkit:ModalPopupExtender ID="modalAlimento" runat="server"
                                                                    PopupControlID="modalAlimentos" BackgroundCssClass="modalBackground"
                                                                    RepositionMode="RepositionOnWindowResize"
                                                                    TargetControlID="lblModalAlimento" CancelControlID="btnModalAlimFechar">
                                                                </ajaxToolkit:ModalPopupExtender>
                                                                <asp:Label ID="lblModalAlimento" runat="server" Text=""></asp:Label>
                                                                <!-- Modal Alimentos -->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 col-lg-6">
                                                        <div class="ibox float-e-margins">
                                                            <div class="ibox-content">
                                                                <div class="text-center">
                                                                    <h5>Sugestão para<span id="name-suggest1"></span>: 
                                                                <asp:Label ID="lblNomeDieta" runat="server" Text=""></asp:Label></h5>
                                                                </div>
                                                                <div class="col-lg-4 text-center">
                                                                    <div class="hr-line-dashed"></div>
                                                                    <asp:Label ID="lblCarboidrato" runat="server" Text="Carboidrato&amp;nbsp;(%)" Font-Size="7pt"></asp:Label>
                                                                    <div class="panel">
                                                                        <asp:Image ID="imgDietaCarboidrato" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 text-center">
                                                                    <div class="hr-line-dashed"></div>
                                                                    <asp:Label ID="lblProteina" runat="server" Text="Proteina&amp;nbsp;(%)" Font-Size="7pt"></asp:Label>
                                                                    <div class="panel">
                                                                        <asp:Image ID="imgDietaProteina" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 text-center">
                                                                    <div class="hr-line-dashed"></div>
                                                                    <asp:Label ID="lblGordura" runat="server" Text="Gordura&amp;nbsp;(%)" Font-Size="7pt"></asp:Label>
                                                                    <div class="panel">
                                                                        <asp:Image ID="imgDietaGordura" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="text-center">
                                                                    <h5>Distribuição Calórica da Dieta </h5>
                                                                </div>
                                                                <div class="col-lg-4 text-center">
                                                                    <div class="hr-line-dashed"></div>
                                                                    <asp:Label ID="Label9" runat="server" Text="Carboidrato&amp;nbsp;(%)" Font-Size="7pt"></asp:Label>
                                                                    <div class="panel">
                                                                        <asp:Image ID="imgCardapioCarboidrato" runat="server" />
                                                                    </div>
                                                                    <asp:Label ID="lblCardapioCarboidrato" runat="server" Text="" Font-Size="8pt"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-4 text-center">
                                                                    <div class="hr-line-dashed"></div>
                                                                    <asp:Label ID="Label3" runat="server" Text="Proteina&amp;nbsp;(%)" Font-Size="8pt"></asp:Label>
                                                                    <div class="panel">
                                                                        <asp:Image ID="imgCardapioProteina" runat="server" />
                                                                    </div>
                                                                    <asp:Label ID="lblCardapioProteina" runat="server" Text="" Font-Size="8pt"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-4 text-center">
                                                                    <div class="hr-line-dashed"></div>
                                                                    <asp:Label ID="Label10" runat="server" Text="Gordura&amp;nbsp;(%)" Font-Size="8pt"></asp:Label>
                                                                    <div class="panel">
                                                                        <asp:Image ID="imgCardapioGordura" runat="server" />
                                                                    </div>
                                                                    <asp:Label ID="lblCardapioGordura" runat="server" Text="" Font-Size="8pt"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="ibox float-e-margins">
                                                            <div class="ibox">
                                                                <div class="ibox-content">
                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading text-center">
                                                                                    <i class="fas fa-tablets"></i>&nbsp;Fibra
                                                                                </div>
                                                                                <div class="panel-body text-center">
                                                                                    <asp:Label ID="lblFibrasDoCardapio" runat="server" Text="g" CssClass="badge badge-success vlFibras text-center" Style="width: 90px"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading text-center">
                                                                                    <i class="fas fa-fill-drip"></i>&nbsp;Umidade
                                                                                </div>
                                                                                <div class="panel-body text-center">
                                                                                    <asp:Label ID="lblUmidadeDoCardapio" runat="server" Text="g" CssClass="badge badge-success vlUmidade text-center" Style="width: 90px"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading text-center">
                                                                                    <i class="fas fa-bolt"></i>&nbsp;Energia Requerida (NEM)
                                                                                </div>
                                                                                <div class="panel-body text-center">
                                                                                    <asp:Label ID="lblNemDoCardapio" runat="server" Text="Label" CssClass="badge badge-primary" Style="width: 90px"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <div class="panel panel-danger" id="pnlEM" runat="server">
                                                                                <div class="panel-heading text-center">
                                                                                    <i class="fas fa-atom"></i>&nbsp;Energia Presente (EM)
                                                                                </div>
                                                                                <div class="panel-body text-center">
                                                                                    <asp:Label ID="lblEnergiaDoCardapio" runat="server" Text="kcal" CssClass="badge badge-primary emDoCardapio" Style="width: 90px"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="pagina-footer col-lg-9 text-left">
                                                                    <div class="text-center">
                                                                        <h5><strong>Distribuição por Categoria</strong></h5>
                                                                    </div>
                                                                    <div class="page-title">
                                                                        <div class="clearfix"></div>
                                                                        <div class="x_content table-responsive">
                                                                            <asp:Repeater ID="rptListagemDistCat" runat="server" OnItemDataBound="rptListagemDistCat_ItemDataBound">
                                                                                <HeaderTemplate>
                                                                                    <table class="table table-striped table-sm table-hover" id="table-distribuicao-categoria">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th style="width: 12%">
                                                                                                    <asp:Label ID="lblInfo" runat="server" Text="Categoria"></asp:Label>
                                                                                                </th>
                                                                                                <th style="width: 3%">
                                                                                                    <asp:Label ID="lblHeaderAlimento" runat="server" Text="Quantidade&nbsp;<sub>(g)</sub>"></asp:Label>
                                                                                                </th>
                                                                                                <th style="width: 3%">
                                                                                                    <asp:Label ID="lblHeaderQuant" runat="server" Text="Percentual&nbsp;<sub>(%)</sub>"></asp:Label>
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblBodyCategoria" runat="server" Text='<%# Eval("Categoria") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td class="text-right">
                                                                                            <asp:Label ID="lblBodyQuant" runat="server" Text='<%# Eval("SomaQuant") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td class="text-right">
                                                                                            <asp:Label ID="lblBodyPerc" runat="server" Text='<%# Eval("PercentQuant") %>'></asp:Label>
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
                                                                <div class="pagina-footer col-lg-3 text-right">
                                                                    <!--<asp:HyperLink ID="hlkDefinirComoModelo" runat="server" title="Definir como Modelo" CssClass="btn btn-primary-nutrovet" Target="_blank"><i class="fas fa-award"></i> Definir como Modelo</asp:HyperLink>-->
                                                                    <asp:HyperLink ID="lbImpressaoCardapio" runat="server" title="Impressão do Cardápio" CssClass="btn btn-primary-nutrovet" Target="_blank"><i class="fas fa-print"></i>&nbsp;imprimir</asp:HyperLink>
                                                                    <asp:LinkButton ID="btnResumoMaisDetalhes" title="Detalhes do Cardápio" CssClass="btn btn-primary-nutrovet" OnClick="btnResumoMaisDetalhes_Click" runat="server"><i class="fas fa-info-circle"></i> detalhes</asp:LinkButton>
                                                                </div>

                                                            </div>
                                                            <%--Resumo do Cardápio--%>
                                                            <div id="modalResumoCardapios" class="modal-dialog modalControle" runat="server" style="display: none;">
                                                                <div class="modal-content animated fadeIn">
                                                                    <div class="modal-header modal-header-success">
                                                                        <asp:LinkButton runat="server" ID="LinkButton4" CssClass="close" data-dismiss="modal" ForeColor="White"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></asp:LinkButton>
                                                                        <h4 class="modal-title">
                                                                            <i class="fa fa-bar-chart-o modal-icon"></i>
                                                                            <asp:Label ID="Label1" runat="server" Text=" Detalhes da Distribuição Calórica do Cardápio"></asp:Label>
                                                                        </h4>
                                                                    </div>
                                                                    <div class="modal-body" id="content-resumo-cardapio">
                                                                        <asp:Repeater ID="rptResumoCardapio" runat="server" OnItemDataBound="rptResumoCardapio_ItemDataBound">
                                                                            <HeaderTemplate>

                                                                                <table class=" table table-striped table-hover table-sm">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Alimento
                                                                                            </th>
                                                                                            <th>Quant. <sub>(g)</sub>
                                                                                            </th>
                                                                                            <th>Carboidrato <sub>(g)</sub>
                                                                                            </th>
                                                                                            <th>Proteina <sub>(g)</sub>
                                                                                            </th>
                                                                                            <th>Gordura <sub>(g)</sub>
                                                                                            </th>
                                                                                            <th>Fibras <sub>(g)</sub>
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblResAlimento" runat="server" Text='<%# Eval("Alimento") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-center">
                                                                                        <span class="badge badge-success">
                                                                                            <asp:Label ID="lblResQuant" runat="server" Text='<%# Eval("Quant") %>'></asp:Label>
                                                                                        </span>
                                                                                    </td>
                                                                                    <td class="text-center">
                                                                                        <span class="badge badge-success">
                                                                                            <asp:Label ID="lblResCarbo" runat="server" Text='<%# Eval("Carboidrato") %>'></asp:Label>
                                                                                        </span>
                                                                                    </td>
                                                                                    <td class="text-center">
                                                                                        <span class="badge badge-success">
                                                                                            <asp:Label ID="lblResProt" runat="server" Text='<%# Eval("Proteina") %>'></asp:Label>
                                                                                        </span>
                                                                                    </td>
                                                                                    <td class="text-center">
                                                                                        <span class="badge badge-success">
                                                                                            <asp:Label ID="lblResGord" runat="server" Text='<%# Eval("Gordura") %>'></asp:Label>
                                                                                        </span>
                                                                                    </td>
                                                                                    <td class="text-center">
                                                                                        <span class="badge badge-success">
                                                                                            <asp:Label ID="lblResFibras" runat="server" Text='<%# Eval("Fibras") %>'></asp:Label>
                                                                                        </span>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </tbody>
                                                                                    <tfoot>
                                                                                        <td>
                                                                                            <span class="label label-primary ">
                                                                                                <asp:Label ID="Label7" runat="server" Text="Total"></asp:Label>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td class="text-center">
                                                                                            <span class="badge badge-success">
                                                                                                <asp:Label ID="lblTotQuant" runat="server" Text="000"></asp:Label>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td class="text-center">
                                                                                            <span class="badge badge-success">
                                                                                                <asp:Label ID="lblTotCarb" runat="server" Text="000"></asp:Label>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td class="text-center">
                                                                                            <span class="badge badge-success">
                                                                                                <asp:Label ID="lblTotProt" runat="server" Text="000"></asp:Label>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td class="text-center">
                                                                                            <span class="badge badge-success">
                                                                                                <asp:Label ID="lblTotGord" runat="server" Text="000"></asp:Label>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td class="text-center">
                                                                                            <span class="badge badge-success">
                                                                                                <asp:Label ID="lblTotFibras" runat="server" Text="000"></asp:Label>
                                                                                            </span>
                                                                                        </td>
                                                                                    </tfoot>
                                                                                </table>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <asp:LinkButton ID="btnFechaResumo" runat="server" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                        </div>
                                                        <ajaxToolkit:ModalPopupExtender ID="modalResumoCardapio" runat="server"
                                                            PopupControlID="modalResumoCardapios" BackgroundCssClass="modalBackground"
                                                            TargetControlID="lblResumoCardapio" CancelControlID="btnFechaResumo" RepositionMode="RepositionOnWindowResizeAndScroll">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                        <asp:Label ID="lblResumoCardapio" runat="server" Text=""></asp:Label>
                                                        <%--Resumo do Cardápio--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>
                                        <asp:View ID="tabNutrientesCardapio" runat="server">
                                            <div class="panel-body">
                                                <button type="button" id="btnInfoATAC" onclick="chamarIzi()" class="btn btn_asp_icon-warning fa" title='Atenção - Aviso'>&#xf071;</button>
                                                <strong>Tabelas de Exigências Nutricionais</strong>&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlTblExigNutr" runat="server" class="btn btn-default" AutoPostBack="True" OnSelectedIndexChanged="ddlTblExigNutr_SelectedIndexChanged"></asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnInfoNutr" runat="server" title="Informações Nutricionais" CssClass="btn input-buttontotext fa" Text="&#xf05a;" OnClick="btnInfoNutr_Click" />
                                            </div>
                                            <div>
                                                <div class="x_content table-responsive">
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
                                                                            <asp:HiddenField ID="hfExigNutr" runat="server" Value='<%# Eval("IdGrupo") %>' />
                                                                            &nbsp;
                                                                        <asp:Label ID="lblGrupo" Style="color: #2c3f51" runat="server" Text='<%# Eval("Grupo") %>'></asp:Label>
                                                                        </h4>
                                                                    </asp:HyperLink>
                                                                </div>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>

                                                                <asp:Repeater ID="rptTabelasNutricionais" runat="server" OnItemDataBound="rptTabelasNutricionais_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <table class="table table-striped projects dataTables-example" id="table-exigencias-nutricionais-min">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th style="border-style: solid solid solid solid; border-width: thin; border-color: #C6D5FF; text-align: center">Nutriente&nbsp;
                                                                                    </th>
                                                                                    <th colspan="2" style="border: thin solid #C6D5FF; text-align: center;">
                                                                                        <asp:Label ID="lblEnergia" runat="server" Text="xxx" CssClass="badge badge-primary "></asp:Label>
                                                                                    </th>
                                                                                    <th colspan="3" style="border: thin solid #C6D5FF; text-align: center;">Cardápio</th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border-style: none solid none solid; border-width: thin; border-color: #C6D5FF;">&nbsp;</td>
                                                                                    <td class="TableNutrSubT">Mínimo</td>
                                                                                    <td class="TableNutrSubT">Máximo</td>
                                                                                    <td class="TableNutrSubT">Consta</td>
                                                                                    <td class="TableNutrSubT">Falta</td>
                                                                                    <td class="TableNutrSubT">Sobra</td>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td class="TableNutrLeft">
                                                                                <asp:Label ID="lblNutriente" runat="server" Text='<%# Eval("Nutriente") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutr">
                                                                                <asp:Label ID="lblValMin" runat="server" Text='<%# Eval("Minimo") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutr">
                                                                                <asp:Label ID="lblValMax" runat="server" Text='<%# Eval("Maximo") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutrConsta">
                                                                                <asp:Label ID="lblValEmCardapio" runat="server" Text='<%# Eval("EmCardapio") %>'></asp:Label>
                                                                            </td>
                                                                            <td id="idTdFaltaI">
                                                                                <asp:Label ID="lblValFalta" runat="server" Text='<%# Eval("Falta") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutr">
                                                                                <asp:Label ID="lblValSobra" runat="server" Text='<%# Eval("Sobra") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                                <asp:Repeater ID="rptTblNutr_II" runat="server" OnItemDataBound="rptTblNutr_II_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <table class="table table-striped projects dataTables-example" id="table-exigencias-nutricionais">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th style="border-style: solid solid solid solid; border-width: thin; border-color: #C6D5FF; text-align: center;">Nutriente&nbsp;
                                                                                    </th>
                                                                                    <th colspan="4" style="border: thin solid #C6D5FF; text-align: center;">
                                                                                        <asp:Label ID="lblEnergia" runat="server" Text="xxx" CssClass="badge badge-primary "></asp:Label>
                                                                                    </th>
                                                                                    <th colspan="3" style="border: thin solid #C6D5FF; text-align: center;">Cardápio</th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border-style: none solid none solid; border-width: thin; border-color: #C6D5FF;">&nbsp;</td>
                                                                                    <td class="TableNutrSubT">Mínimo</td>
                                                                                    <td class="TableNutrSubT">Máximo</td>
                                                                                    <td class="TableNutrSubT">Adequado</td>
                                                                                    <td class="TableNutrSubT">Recomendado</td>
                                                                                    <td class="TableNutrSubT">Consta</td>
                                                                                    <td class="TableNutrSubT">Falta</td>
                                                                                    <td class="TableNutrSubT">Sobra</td>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td class="TableNutrLeft">
                                                                                <asp:Label ID="lblNutriente" runat="server" Text='<%# Eval("Nutriente") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutr">
                                                                                <asp:Label ID="lblValMin" runat="server" Text='<%# Eval("Minimo") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutr">
                                                                                <asp:Label ID="lblValMax" runat="server" Text='<%# Eval("Maximo") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutr">
                                                                                <asp:Label ID="lblValAdeq" runat="server" Text='<%# Eval("Adequado") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutr">
                                                                                <asp:Label ID="lblValRecomend" runat="server" Text='<%# Eval("Recomendado") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutr">
                                                                                <asp:Label ID="lblValEmCardapio" runat="server" Text='<%# Eval("EmCardapio") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutr">
                                                                                <asp:Label ID="lblValFalta" runat="server" Text='<%# Eval("Falta") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="TableNutr">
                                                                                <asp:Label ID="lblValSobra" runat="server" Text='<%# Eval("Sobra") %>'></asp:Label>
                                                                            </td>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>

                                                            </ContentTemplate>
                                                        </ajaxToolkit:Accordion>
                                                        <div class="modal-footer">
                                                            <div class="pull-right">
                                                                <asp:HyperLink ID="hlImpressaoTbNutr" runat="server" title="Impressão das Exigências Nutricionais" CssClass="btn btn-info" Target="_blank" Enabled="False"><i class="fas fa-print"></i> imprimir</asp:HyperLink>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <!-- Modal Tabelas Nutricionais -->
                                                <div id="modalTabelasNutr" runat="server" class="modal-dialog modalControle" style="display: none">
                                                    <div class="modal-content animated fadeIn">
                                                        <div class="modal-header modal-header-success">
                                                            <asp:LinkButton runat="server" ID="LinkButton5" CssClass="close" data-dismiss="modal" ForeColor="White"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></asp:LinkButton>
                                                            <h4 class="modal-title"><i class="fa fa-bar-chart-o modal-icon"></i>
                                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label><small style="color: #fff;"> - Informações Nutricionais</small>
                                                            </h4>
                                                        </div>

                                                        <div class="modal-body" id="content-tabelas-nutricionais">
                                                            <asp:Panel ID="Panel2" runat="server" CssClass="scroll_content" Height="500px" ScrollBars="None">
                                                                <%--Completo--%>
                                                                <asp:Repeater ID="rptModalTabelasNutr" runat="server" OnItemDataBound="rptModalTabelasNutr_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <table class="table table-striped projects dataTables-example" id="table-exigencias-nutricionais">
                                                                            <thead>
                                                                                <tr>
                                                                                    <td colspan="6" class="text-center">
                                                                                        <asp:Label ID="lblTabelaExigNutr" runat="server" Text="222"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" class="text-center">Nutriente</td>
                                                                                    <td colspan="4" class="text-center">Exigências para 1000 kcal</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">&nbsp;</td>
                                                                                    <td class="text-center">Mínimo</td>
                                                                                    <td class="text-center">Máximo</td>
                                                                                    <td class="text-center">Adequado</td>
                                                                                    <td class="text-center">Recomendado</td>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:Label ID="lblNutriente" runat="server" Text='<%# Eval("Nutriente") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="text-center">
                                                                                <asp:Label ID="lblValMin" runat="server" Text='<%# Eval("Minimo") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="text-center">
                                                                                <asp:Label ID="lblValMax" runat="server" Text='<%# Eval("Maximo") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="text-center">
                                                                                <asp:Label ID="lblValAdeq" runat="server" Text='<%# Eval("Adequado") %>'></asp:Label>
                                                                            </td>
                                                                            <td class="text-center">
                                                                                <asp:Label ID="lblValRecomend" runat="server" Text='<%# Eval("Recomendado") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                            </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                                <%--Só Max And Min--%>
                                                                <asp:Repeater ID="rptModalTabNut_II" runat="server" OnItemDataBound="rptModalTabNut_II_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <table class="table table-striped projects dataTables-example" id="table-exigencias-nutricionais">
                                                                            <thead>
                                                                                <tr>
                                                                                    <td colspan="3" class="text-center">
                                                                                        <asp:Label ID="lblTabelaExigNutr" runat="server" Text="222"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Nutriente</td>
                                                                                    <td colspan="2">1000 kcal</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>Mínimo</td>
                                                                                    <td>Máximo</td>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Nutriente") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("Minimo") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("Maximo") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                            </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:LinkButton ID="btnModalTabelasNutr" runat="server" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <ajaxToolkit:ModalPopupExtender ID="mdTabelasNutr" runat="server"
                                                    PopupControlID="modalTabelasNutr" BackgroundCssClass="modalBackground"
                                                    RepositionMode="RepositionOnWindowResize"
                                                    TargetControlID="lblTabelasNutr" CancelControlID="btnModalTabelasNutr">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <asp:Label ID="lblTabelasNutr" runat="server" Text=""></asp:Label>
                                                <!-- Modal Alimentos -->



                                                <div id="modalInfoATAC" style="display: none">

                                                    <p>Na eventualidade de sua dieta não conter algum nutriente que você imaginava que conteria, ou então conter quantidades muito baixas,verifique se os alimentos que você inseriu para a dieta na tela anterior estão adicionando esse nutriente na dieta.</p>
                                                    <p>Caso uma determinada tabela de alimentos não forneça o referido nutriente, logicamente ele aparecerá na totalização da dieta.Por exemplo: alimentos provenientes de <strong>TACO</strong> ou <strong>TUCUNDUVA</strong> não informam quantidades de aminoácidos.</p>
                                                    <p>Você pode verificar os nutrientes dos alimentos inseridos na dieta clicando no "i" que está na linha do alimento, no lado esquerdo.</p>
                                                </div>











                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                    <!-- /MultiView tabs -->
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
    <div id="modalInfoNEM" style="display: none">
        <p><strong>1. Cães machos ou fêmeas fora de gestação/lactação </strong><i class="fas fa-arrow-right"></i>NEM (kcal) = FATOR * (PESO IDEAL)<sup>0,75</sup></p>
        <p>&nbsp;&nbsp;&nbsp;&nbsp;O <i>FATOR</i> deve ser definido pelo usuário. Sugestão o seguinte:</p>
        <ul>
            <li>Adultos ativos = 130</li>
            <li>Adultos jovens e ativos = 140</li>
            <li>Adultos de raças grandes ativos = 200</li>
            <li>Terriers adultos ativos = 180</li>
            <li>Sedentários = 95</li>
            <li>Idosos = 105</li>
        </ul>
        <p>&nbsp;&nbsp;&nbsp;&nbsp;<strong><i>Fonte:</i></strong> Adaptado De Saad (2015). Aspectos nutricionais de cães e gatos em várias fases fisiológicas. Doi: 10.13140/2.1.2767.6487. <a href="http://www.researchgate.net/publication/270508665" target="_blank">www.researchgate.net/publication/270508665</a>. Acesso em: 12/9/2017.</p>
        <p><strong>2. Gatos machos ou fêmeas fora de gestação/lactação </strong><i class="fas fa-arrow-right"></i>NEM (Kcal) = FATOR x (PESO IDEAL)<sup>0,67</sup></p>
        <p>&nbsp;&nbsp;&nbsp;&nbsp;O <i>FATOR</i> deve ser definido pelo usuário. Sugestão o seguinte:</p>
        <ul>
            <li>Escore corporal de magro a ideal = 100</li>
            <li>Escore corporal obeso = Para animais obesos a fórmula de cálculo não foi implementada nessa versão, mas na próxima atualização do sistema iremos considerar o calculo conforme orienta a autora do estudo citado na fonte (NEM (Kcal) = FATOR x (PESO IDEAL)<sup>0,67</sup> com FATOR sugerido de 100)</li>
        </ul>
        <p>&nbsp;&nbsp;&nbsp;&nbsp;<strong><i>Fonte:</i></strong> Adaptado De Saad (2015). Aspectos nutricionais de cães e gatos em várias fases fisiológicas. Doi: 10.13140/2.1.2767.6487. <a href="http://www.researchgate.net/publication/270508665" target="_blank">www.researchgate.net/publication/270508665</a>. Acesso em: 12/9/2017.</p>
        <p><strong>3. Cadelas em gestação </strong><i class="fas fa-arrow-right"></i>NEM (Kcal) = 130 x (peso ideal)<sup>0,75</sup> + (26 * peso ideal) - Sugere-se que o FATOR seja 130</p>
        <p>&nbsp;&nbsp;&nbsp;&nbsp;<strong><i>Fonte:</i></strong> Adaptado de Caorfi (2017). Disponível: <a href="http://www.fcav.unesp.br/Home/departamentos/clinicacv/AULUSCAVALIERICARCIOFI/anexo-exercicio-para-casa-1-2017.pdf" target="_blank">http://www.fcav.unesp.br/Home/departamentos/clinicacv/AULUSCAVALIERICARCIOFI/anexo-exercicio-para-casa-1-2017.pdf</a>. Acesso em 20/9/2017.</p>

        <p><strong>4. Gatas em gestação </strong><i class="fas fa-arrow-right"></i>NEM (Kcal) = 140 x (peso ideal)<sup>0,67</sup> - Sugere-se que o FATOR seja 130</p>
        <p>&nbsp;&nbsp;&nbsp;&nbsp;<strong><i>Fonte:</i></strong> Adaptado de Caorfi (2017). Disponível: <a href="http://www.fcav.unesp.br/Home/departamentos/clinicacv/AULUSCAVALIERICARCIOFI/anexo-exercicio-para-casa-1-2017.pdf" target="_blank">http://www.fcav.unesp.br/Home/departamentos/clinicacv/AULUSCAVALIERICARCIOFI/anexo-exercicio-para-casa-1-2017.pdf</a>. Acesso em 20/9/2017.</p>

        <p><strong>5. Cadelas em lactação </strong><i class="fas fa-arrow-right"></i>NEM(Kcal) = 145 x (peso ideal)<sup>0,75</sup> + variante - Sugere-se que o FATOR seja 145</p>

        <ul style="list-style-type: none">
            <li>
                <i>variante</i> = peso ideal x (24n +12m) x L, onde:
            </li>
            <ul style="list-style-type: none">
                <li>n = número de filhotes entre 1 e 4</li>
                <li>m = número de filhotes entre 5 e 8 (se "m" menor que 5 então o "m" será 0)</li>
                <li>L = fator de correção de acordo com a semana de lactação, onde:</li>
            </ul>
            <ul style="list-style-type: none">
                <li>semana 1 = 0,75</li>
                <li>semana 2 = 0,95</li>
                <li>semana 3 = 1,1</li>
                <li>semana 4 = 1,2</li>
                <li>semana 5 até 7 = desconsiderar o "L" no cálculo</li>
            </ul>
        </ul>
        <p>&nbsp;&nbsp;&nbsp;&nbsp;<strong><i>Fonte:</i></strong> Adaptado De Saad (2015). Aspectos nutricionais de cães e gatos em várias fases fisiológicas. Doi: 10.13140/2.1.2767.6487. <a href="http://www.researchgate.net/publication/270508665" target="_blank">www.researchgate.net/publication/270508665</a>. Acesso em: 12/9/2017.</p>

        <p><strong>6. Gatas em lactação </strong><i class="fas fa-arrow-right"></i></p>
        <ul style="list-style-type: none">
            <li>Para < 3 filhotes: NEM(Kcal) = (100 x (peso ideal)<sup>0,67</sup>) + (18 x peso ideal x L) - Sugere-se que o FATOR seja 100</li>
            <li>Para 3 a 4 filhotes: NEM(Kcal) = 100 x (peso ideal)<sup>0,67</sup>) + (60 x peso ideal x L) - Sugere-se que o FATOR seja 100</li>
            <li>Para > 4 filhotes: NEM(Kcal) = 100 x (peso ideal)<sup>0,67</sup>) + (70 x peso ideal x L) - Sugere-se que o FATOR seja 100</li>
            <ul style="list-style-type: none">
                <li>
                    <i>onde:</i>
                </li>
                <ul style="list-style-type: none">
                    <li>L = fator de correção de acordo com a semana de lactação:</li>
                    <ul style="list-style-type: none">
                        <li>semana 1 e 2 = 0,9</li>
                        <li>semana 3 e 4 = 1,2</li>
                        <li>semana 5 = 1,1</li>
                        <li>semana 6 = 1,0</li>
                        <li>semana 7 = 0,8</li>
                    </ul>
                </ul>
            </ul>
        </ul>
        <p>&nbsp;&nbsp;&nbsp;&nbsp;<strong><i>Fonte:</i></strong> Adaptado de Caorfi (2017). Disponível: <a href="http://www.fcav.unesp.br/Home/departamentos/clinicacv/AULUSCAVALIERICARCIOFI/anexo-exercicio-para-casa-1-2017.pdf" target="_blank">www.fcav.unesp.br/Home/departamentos/clinicacv/AULUSCAVALIERICARCIOFI/anexo-exercicio-para-casa-1-2017.pdf</a>. Acesso em: 20/9/2017.</p>
    </div>
</asp:Content>
