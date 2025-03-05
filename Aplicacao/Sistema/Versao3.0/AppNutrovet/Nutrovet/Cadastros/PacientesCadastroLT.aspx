<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="PacientesCadastroLT.aspx.cs" Inherits="Nutrovet.Cadastros.PacientesCadastroLT"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

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

                $('#meDtFinal').datepicker({
                    format: "dd/mm/yyyy",
                    clearBtn: true,
                    language: "pt-BR",
                    daysOfWeekHighlighted: "0,6",
                    autoclose: true,
                    todayHighlight: true
                });

                $('#meDtNasc').change(function () {
                    calcularIdade();
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
                    buttondown_class: 'btn btn-primary-nutrovet'
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
                <asp:HyperLink ID="hlkMinimalize" NavigateUrl="#" CssClass="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i></asp:HyperLink>
            </div>
            <h2>
                <asp:Label ID="lblPagina" runat="server" Text=""></asp:Label>
            </h2>
            <div class="col-lg-4">
                <ol class="breadcrumb">
                    <li>
                        <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server">Início</asp:HyperLink>
                    </li>
                    <li class="active">
                        <asp:HyperLink ID="hlTutoresSelecao" NavigateUrl="~/Cadastros/PacientesSelecao.aspx" runat="server">Pacientes</asp:HyperLink>
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

                function calculateAge(dateFrom) {
                    var dateNow = moment();
                    var dateBirthday = moment(dateFrom);
                    var years = dateNow.diff(dateBirthday, "year");
                    dateBirthday.add(years, "years");
                    var months = dateNow.diff(dateBirthday, "months");
                    dateBirthday.add(months, "months");
                    var days = dateNow.diff(dateBirthday, "days");

                    return { years: years, months: months, days: days };
                }

                function calcularIdade() {
                    var data = document.getElementById('<%=meDtNasc.ClientID %>').value;

                    if (data != '') {
                        var partes = data.split("/");
                        var junta = partes[2] + "-" + partes[1] + "-" + partes[0];

                        document.getElementById('<%=tbIdadeAnos.ClientID %>').value =
                            calculateAge(junta).years;
                        document.getElementById('<%=tbIdadeMeses.ClientID %>').value =
                            calculateAge(junta).months;
                        document.getElementById('<%=tbIdadeDias.ClientID %>').value =
                            calculateAge(junta).days;
                    }
                }
                function ClicarFileUpload() {

                    document.getElementById('<%=fuFileUpload.ClientID %>').click();
                    document.getElementById('<%= lbEnviarImagem.ClientID %>').style.display = 'block';
                }
            </script>

            <div class="row wrapper border-bottom white-bg page-heading">


                <div class="col-lg-6">
                    <div class="ibox-title">
                        <h5>Linha do Tempo</h5>
                        <div class="ibox-tools">
                            <asp:LinkButton runat="server" ID="LinkButton1" CssClass="btn btn-primary-nutrovet" OnClick="btnAtendimentoLT_Click"><i class="fa-solid fa-hand-holding-medical"></i>&nbsp;&nbsp;Atendimento </asp:LinkButton>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div id="colLinhaTempo" class="x_content table-responsive">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:Repeater ID="rptLTAnos" runat="server" OnItemDataBound="rptLTAnos_ItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="product-desc">
                                        <div class="row">
                                            <div class="widget-head-color-box orange-bg text-center col-md-12 col-sm-12 col-lg-12 col-xs-12">
                                                <h2>
                                                    <asp:Label ID="lblAno" runat="server" Text=""></asp:Label>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Repeater ID="rptLinhaTempo" runat="server" OnItemDataBound="rptLinhaTempo_ItemDataBound" OnItemCommand="rptLinhaTempo_ItemCommand">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div id="vertical-timeline" class="vertical-container dark-timeline ">
                                                <div runat="server" class="vertical-timeline-icon lazur-bg" id="iconeLinhadotempo">
                                                    <asp:HyperLink ID="hlIconeLT" runat="server" CssClass="fa-solid fa-file-medical" ForeColor="White" NavigateUrl="#"></asp:HyperLink>
                                                </div>
                                                <div class="vertical-timeline-content">
                                                    <h2>
                                                        <asp:Label ID="lblTituloLinhaTempo" runat="server" Text='<%# Eval("Classificacao") %>' ></asp:Label>
                                                    </h2>
                                                    <h4>
                                                        <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("Tipo") %>'></asp:Label>
                                                    </h4>
                                                    <small>
                                                        <asp:Label ID="lblDescricaoLinhaTempo" runat="server" Text='<%# Eval("Titulo") %>'></asp:Label>
                                                        <br>
                                                        </br>
                                                                <asp:Label ID="lblDataLinhaTempo" runat="server" Text='<%# Eval("Data") %>'></asp:Label>
                                                    </small>
                                                    <asp:LinkButton runat="server" ID="lbMais" CommandName='<%# DataBinder.Eval(Container.DataItem, "IdClassif") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CssClass="btn btn-primary-nutrovet btn-xs" data-toggle="tooltip" data-placement="top" title="Editar o Item"><i class="fa-solid fa-file-pen"></i></asp:LinkButton>
                                                    <asp:HyperLink ID="hlVisualizar" runat="server" CssClass="btn btn-warning btn-xs" Target="_blank" ToolTip="Visualizar Arquivo"><i class="fa-solid fa-eye" title="Visualizar Arquivo"></i></asp:HyperLink>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>

                        </div>

                    </div>
                <div class="modal-footer col-sm-12 col-lg-12" style="text-align: left;">
                    <div class="btn-group" role="group">
                        <asp:LinkButton runat="server" ID="lbDetalhe" CssClass="btn btn-lg btn-primary-nutrovet" OnClick="btnAtendimentoLT_Click"><i class="fa-solid fa-hand-holding-medical"></i>&nbsp;&nbsp;Atendimento </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="wrapper wrapper-content fadeInRight ibox-content">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="ibox">
                                <div class="ibox-content product-box active">
                                    <div class="product-desc-menor">
                                        <div class="product-name">
                                            PACIENTE
                                        </div>
                                        <div class="product-name">
                                            <strong>
                                                <asp:Label ID="lbCardNomePaciente" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                        </div>
                                    </div>
                                    <div class="widget-head-color-box gray-bg p-xs-todo text-center" data-toggle="tooltip" data-placement="top" title="Clique na imagem para atualizar a foto do paciente">
                                        <asp:UpdatePanel ID="UpdatePanelUpld" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <label id="imagemLabel" for="fuFileUpload" onclick="ClicarFileUpload();">
                                                        <asp:Image ID="imgCardFotoPaciente" ImageUrl="~/Imagens/Pacientes/petpadrao.png" alt="imagem do pet" class="img-thumbnail-paciente m-b-none" runat="server" Style="cursor: pointer;" data-toggle="tooltip" data-placement="top" title="Clique na imagem para atualizar a foto do paciente" />
                                                    </label>
                                                    <div class="product-desc-menor">
                                                        <asp:FileUpload ID="fuFileUpload" runat="server" accept=".png,.jpg,.jpeg" Style="display: none;" />
                                                        <asp:LinkButton ID="lbEnviarImagem" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbEnviarImagem_Click" Style="display: none;"><i class="fas fa-cloud-upload-alt"></i> Enviar Imagem</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lbEnviarImagem" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="product-desc">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbCardNomeTutor" runat="server" class="control-label text-left" Text="Tutor:"></asp:Label>
                                                <strong>
                                                    <asp:Label ID="lbCardNomeTutorValor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbCardEspeciePaciente" runat="server" class="control-label text-left" Text="Espécie:"></asp:Label>
                                                <strong>
                                                    <asp:Label ID="lbCardEspeciePacienteValor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbCardRacaPaciente" runat="server" class="control-label text-left" Text="Raça:"></asp:Label>
                                                <strong>
                                                    <asp:Label ID="lbCardRacaPacienteValor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                            </div>
                                            <%--<div class="col-sm-12">
                                                            <asp:Label ID="lbCardSexoPaciente" runat="server" class="control-label text-left" Text="Sexo:"></asp:Label>
                                                            <strong>
                                                                <asp:Label ID="lbCardSexoPacienteValor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                        </div>
                                                        <div class="col-sm-12">
                                                            <asp:Label ID="lbCardDataNascPaciente" runat="server" class="control-label text-left" Text="Data Nascimento:"></asp:Label>
                                                            <strong>
                                                                <asp:Label ID="lbCardDataNascPacienteValor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                        </div>--%>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbCardIdadePaciente" runat="server" class="control-label text-left" Text="Idade:"></asp:Label>
                                                <strong>
                                                    <asp:Label ID="lbCardIdadePacienteValor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                            </div>
                                            <%--<div class="col-sm-12">
                                                            <asp:Label ID="lbCardRGPaciente" runat="server" class="control-label text-left" Text="RG:"></asp:Label>
                                                            <strong>
                                                                <asp:Label ID="lbCardRGPacienteValor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                        </div>
                                                        <div class="col-sm-12">
                                                            <asp:Label ID="lbCardPesoAtualPaciente" runat="server" class="control-label text-left" Text="Peso Atual (kg):"></asp:Label>
                                                            <strong>
                                                                <asp:Label ID="lbCardPesoAtualPacienteValor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                        </div>
                                                        <div class="col-sm-12">
                                                            <asp:Label ID="lbCardPesoIdealPaciente" runat="server" class="control-label text-left" Text="Peso Ideal (kg):"></asp:Label>
                                                            <strong>
                                                                <asp:Label ID="lbCardPesoIdealPacienteValor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                        </div>
                                                        <div class="col-sm-12">
                                                            <asp:Label ID="lbCardObsPaciente" runat="server" class="control-label text-left" Text="Observação:"></asp:Label>
                                                            <strong>
                                                                <asp:Label ID="lbCardObsPacienteValor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                        </div>--%>
                                            <div class="modal-footer-card col-sm-8 col-lg-12">
                                                <asp:LinkButton runat="server" ID="lbCardEditarPaciente" CssClass="btn btn-primary-nutrovet" OnClick="lbEditarDadosPaciente_Click"><i class="fas fa-edit"></i>&nbsp;&nbsp;Editar </asp:LinkButton>
                                            </div>
                                            <asp:HiddenField ID="hfidPacienteCard" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="wrapper wrapper-content fadeInRight ibox-content">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12">
                            <div class="ibox">
                                <div class="ibox-content product-box active">
                                    <div class="widget-head-color-box navy-bg p-xs-todo text-center">
                                        <div class="product-desc-menor">
                                            <h3><i class="fas fa-chart-line modal-icon"></i>&nbsp;HISTÓRICO DE ALTERAÇÃO DO PESO ATUAL</h3>
                                        </div>
                                    </div>
                                    <div class="product-desc">
                                        <h4>Informe o período de pesquisa</h4>
                                        <div class="form-group">
                                            <label id="lblDataInicialPesquisa" class="col-lg-3 control-label">Data Inicial</label>
                                            <div class="col-lg-5">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style=""><i class="fa fa-calendar fa-fw"></i></span>
                                                    <cc1:MEdit ID="meDtInicial" name="meDtInicial" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Data Inicial da Pesquisa"></cc1:MEdit>
                                                </div>
                                            </div>
                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>

                                            <label id="lblDataFinalPesquisa" class="col-lg-3 control-label">Data Final</label>
                                            <div class="col-lg-5">
                                                <div class="input-group">
                                                    <span class="input-group-addon" style=""><i class="fa fa-calendar fa-fw"></i></span>
                                                    <cc1:MEdit ID="meDtFinal" name="meDtFinal" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Data Final da Pesquisa"></cc1:MEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-md btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                        <div class="row">
                                            <div class="widget-head-color-box gray-bg p-xs-todo text-center table-responsive col-md-12 col-sm-12 col-lg-12 col-xs-12">
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Central Modal Alterando Dados do Paciente -->
            <div id="modalAlterandoDadosPaciente" runat="server" style="display: block">
                <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                    <!--Content-->
                    <div class="modal-content">
                        <!--Header-->
                        <div class="modal-header modal-header-success">
                            <div class="col-md-11 col-sm-11 col-xs-11">
                                <i class="fas fa-edit fa-xl"></i>
                                <asp:Label ID="tituloModalCadastroPaciente" runat="server" Text="&nbsp;&nbsp;Alterando dados do Paciente" class="heading lead"></asp:Label>
                            </div>
                            <div class="col-md-1 col-sm-1 col-xs-1">
                                <asp:LinkButton runat="server" ID="LinkButton2" CssClass="close" data-dismiss="modal"> <i class='fa fa-times-circle'></i></asp:LinkButton>
                            </div>
                        </div>
                        <!--Body-->
                        <div class="modal-body">
                            <div class="row">
                                <div class="ibox-content">
                                    <div class="form-group">
                                        <label id="lblTutor" class="col-lg-2 control-label">Tutor</label>
                                        <div class="col-lg-10">
                                            <div class="input-group">
                                                <span class="input-group-addon" style=""><i class="fas fa-user fa-fw"></i></span>
                                                <asp:DropDownList ID="ddlTutor" runat="server" AutoPostBack="False" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                    <div class="form-group">
                                        <label id="lblNomePaciente" class="col-lg-2 control-label">Paciente</label>
                                        <div class="col-lg-10">
                                            <div class="input-group">
                                                <span class="input-group-addon" style=""><i class="fas fa-paw fa-fw"></i></span>
                                                <asp:TextBox ID="tbNomePaciente" runat="server" placeholder="Paciente" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Nome do Paciente"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                    <div class="form-group">
                                        <label id="lblEspecie" class="col-lg-2 control-label">Espécie</label>
                                        <div class="col-lg-10">
                                            <div class="input-group">
                                                <span class="input-group-addon" style=""><i class="fas fa-dna fa-fw"></i></span>
                                                <asp:DropDownList ID="ddlEspecie" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlEspecie_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                    <div class="form-group">
                                        <label id="lblRaca" class="col-lg-2 control-label">Raça</label>
                                        <div class="col-lg-10">
                                            <div class="input-group">
                                                <span class="input-group-addon" style=""><i class="fas fa-bone fa-fw"></i></span>
                                                <asp:DropDownList ID="ddlRaca" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                    <div class="form-group">
                                        <label id="lblSexo" class="col-lg-2 control-label">Sexo</label>
                                        <div class="col-lg-10">
                                            <div class="input-group">
                                                <span class="input-group-addon" style=""><i class="fas fa-transgender fa-fw"></i></span>
                                                <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                    <div class="form-group">
                                        <label id="lblDataNAscimento" class="col-lg-2 control-label">Data de Nascimento</label>
                                        <div class="col-lg-10">
                                            <div class="input-group">
                                                <span class="input-group-addon" style=""><i class="fa fa-calendar fa-fw"></i></span>
                                                <cc1:MEdit ID="meDtNasc" name="meDtNasc" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Data de Nascimento"></cc1:MEdit>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                    <div class="form-group">
                                        <label id="lblRgPet" class="col-lg-2 control-label">RG Pet</label>
                                        <div class="col-lg-10">
                                            <div class="input-group">
                                                <span class="input-group-addon" style=""><i class="far fa-newspaper fa-fw"></i></span>
                                                <asp:TextBox ID="tbRgPet" runat="server" placeholder="RG Pet" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="RG do Paciente"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                    <div class="form-group">
                                        <label id="lblIdade" class="col-lg-2 control-label">Idade</label>
                                        <div class="col-lg-10" style="overflow-x: auto;">
                                            <div class="input-group">
                                                <span class="input-group-addon" style="width: 5px"><i class="fa fa-birthday-cake fa-fw"></i></span>
                                                <asp:TextBox ID="tbIdadeAnos" Style="width: 65px" runat="server" CssClass="form-control col-lg-6" data-toggle="tooltip" data-placement="top" title="Ano de Nascimento" ReadOnly="True"></asp:TextBox>
                                                <span class="input-group-addon" style="width: 5px">Anos</span>
                                                <asp:TextBox ID="tbIdadeMeses" Style="width: 65px" runat="server" CssClass="form-control col-lg-6" data-toggle="tooltip" data-placement="top" title="Mês de Nascimento" ReadOnly="True"></asp:TextBox>
                                                <span class="input-group-addon" style="width: 5px">Meses</span>
                                                <asp:TextBox ID="tbIdadeDias" Style="width: 65px" runat="server" CssClass="form-control col-lg-6" data-toggle="tooltip" data-placement="top" title="Dia de Nascimento" ReadOnly="True"></asp:TextBox>
                                                <span class="input-group-addon" style="width: 5px">Dias</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>

                                    <div class="form-group">
                                        <label id="lblAtual" class="col-12 col-xs-12 col-sm-12 col-md-12 col-lg-2 control-label">Peso Atual</label>
                                        <div class="col-9 col-xs-9 col-sm-9 col-md-9 col-lg-4 ">
                                            <div class="input-group">
                                                <asp:TextBox ID="tbPesoAtual" ClientIDMode="Static" runat="server" placeholder="0,00" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Peso Atual"></asp:TextBox>
                                            </div>
                                        </div>
                                        <strong>kg</strong>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                    <div class="form-group">
                                        <label id="lblPesoIdeal" class="col-12 col-xs-12 col-sm-12 col-md-12 col-lg-2 control-label">Peso Ideal</label>
                                        <div class="col-9 col-xs-9 col-sm-9 col-md-9 col-lg-4 ">
                                            <asp:TextBox ID="tbPesoIdeal" ClientIDMode="Static" runat="server" placeholder="0,00" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Peso Ideal"></asp:TextBox>
                                        </div>
                                        <strong>kg</strong>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                    <div class="form-group">
                                        <label id="lblObs" class="col-12 col-xs-12 col-sm-12 col-md-12 col-lg-2 control-label">Observações</label>
                                        <div class="col-lg-10">
                                            <FTB:FreeTextBox ID="ftbObs" runat="server" AllowHtmlMode="False"
                                                AssemblyResourceHandlerPath="" AutoConfigure="" AutoGenerateToolbarsFromString="True"
                                                AutoHideToolbar="True" AutoParseStyles="True" BackColor="158, 190, 245" BaseUrl=""
                                                BreakMode="Paragraph" ButtonDownImage="False" ButtonFileExtention="gif"
                                                ButtonFolder="Images" ButtonHeight="20" ButtonImagesLocation="InternalResource"
                                                ButtonOverImage="False" ButtonPath="" ButtonSet="Office2003" ButtonWidth="21"
                                                ClientSideTextChanged="" ConvertHtmlSymbolsToHtmlCodes="False"
                                                DesignModeBodyTagCssClass="" DesignModeCss="" DisableIEBackButton="False"
                                                DownLevelCols="50" DownLevelMessage="" DownLevelMode="TextArea" DownLevelRows="10"
                                                EditorBorderColorDark="Gray" EditorBorderColorLight="Gray" EnableHtmlMode="False"
                                                EnableSsl="False" EnableToolbars="True" Focus="False" FormatHtmlTagsToXhtml="True"
                                                GutterBackColor="129, 169, 226" GutterBorderColorDark="Gray"
                                                GutterBorderColorLight="White" Height="200px" HelperFilesParameters="" HelperFilesPath=""
                                                HtmlModeCss="" HtmlModeDefaultsToMonoSpaceFont="True" ImageGalleryPath="~/imagens/"
                                                ImageGalleryUrl="ftb.imagegallery.aspx?rif={0}&amp;cif={0}"
                                                InstallationErrorMessage="InlineMessage" JavaScriptLocation="InternalResource"
                                                Language="pt-PT" PasteMode="Default" ReadOnly="False" RemoveScriptNameFromBookmarks="True"
                                                RemoveServerNameFromUrls="True" RenderMode="NotSet" ScriptMode="External"
                                                ShowTagPath="False" SslUrl="/." StartMode="DesignMode" StripAllScripting="False"
                                                SupportFolder="/aspnet_client/FreeTextBox/" TabIndex="-1" TabMode="InsertSpaces" Text=""
                                                TextDirection="LeftToRight" ToolbarBackColor="Transparent" ToolbarBackgroundImage="True"
                                                ToolbarImagesLocation="InternalResource" ToolbarLayout="FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList|Cut,Copy,Paste;Undo,Redo"
                                                ToolbarStyleConfiguration="Office2003" UpdateToolbar="True" UseToolbarBackGroundImage="True"
                                                Width="100%">
                                            </FTB:FreeTextBox>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hfID" runat="server" />
                                    <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                </div>
                            </div>
                        </div>
                        <!--Footer-->

                        <div class="modal-footer justify-content-center">
                            <div class="btn-group" role="group">
                                <asp:LinkButton runat="server" ID="btnAlterandoDadosPacienteFechar" CssClass="btn btn-default" data-dismiss="modal" OnClick="btnAlterandoDadosPacienteFechar_Click"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnAlterandoDadosPacienteSalvar" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvar_Click"> <i class='far fa-save'></i> Salvar </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!--/.Content-->
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="mdAlterandoDadosPaciente" runat="server"
                PopupControlID="modalAlterandoDadosPaciente" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblPopUpAlterandoDadosPaciente">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblPopUpAlterandoDadosPaciente" runat="server" Text=""></asp:Label>
            <!-- Central Modal Medium Success-->

            <!-- Modal Alerta Limite Plano -->
            <div id="modalLimitePlano" runat="server" class="modal-dialog modal-md" role="document" style="display: none">
                <div class="modal-content rotateInDownLeft">
                    <div class="modal-header bg-warning">
                        <asp:LinkButton runat="server" ID="LinkButton3" CssClass="close" data-dismiss="modal" ForeColor="Black"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></asp:LinkButton>
                        <h4 class="modal-title text-center"><i class="fas fa-info-circle"></i>&nbsp;SITUAÇÃO DO PLANO</h4>
                    </div>
                    <div class="modal-body-menor" id="bodymodalSituacaoPlano">
                        <div class="container-fluid">
                            <div class="row">
                                <asp:Label ID="lblSituacaoPlano" CssClass="col-lg-12 control-label" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lbFechar_Click" CssClass="btn btn-default" data-dismiss="modal"><i class='fas fa-door-open'></i>Fechar </asp:LinkButton>
                    </div>
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="mdSituacaoPlano" runat="server"
                PopupControlID="modalLimitePlano" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblLimitePlano" CancelControlID="LinkButton4">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblLimitePlano" runat="server" Text=""></asp:Label>
            <!-- Modal Alerta Limite Plano -->

            <!-- Central Modal Atendimento -->
            <div id="modalAtendimentoLT" runat="server" style="display: block">
                <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                    <!--Content-->
                    <div class="modal-content">
                        <!--Header-->
                        <div class="modal-header modal-header-success">
                            <div class="col-md-9 col-sm-9 col-xs-9">
                                <i class="fa-solid fa-house-medical-flag fa-xl" aria-hidden="true"></i>
                                <asp:Label ID="lblTituloModal" runat="server" Text="&nbsp;&nbsp;Atendimento" class="heading lead"></asp:Label>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <asp:LinkButton runat="server" ID="lbFecharModal" CssClass="close" data-dismiss="modal"> <i class='fa fa-times-circle'></i></asp:LinkButton>
                            </div>
                        </div>

                        <!--Body-->
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group">
                                    <label id="lblTipoAtendimentoLT" class="col-lg-2 control-label">Tipo de Atendimento </label>
                                    <div class="col-lg-10">
                                        <div class="input-group">
                                            <span class="input-group-addon" style=""><i class="fa-solid fa-hand-holding-medical"></i></span>
                                            <asp:DropDownList ID="ddlTipoAtendimentoLT" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                <div class="form-group">
                                    <label id="lblDescricaoLT" class="col-lg-2 control-label">Descrição</label>
                                    <div class="col-lg-10">
                                        <div class="input-group">
                                            <span class="input-group-addon" style=""><i class="fa-solid fa-laptop-medical"></i></span>
                                            <asp:TextBox ID="txbDescricaoLT" runat="server" placeholder="Descrição" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Descricao"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>

                                <div class="form-group">
                                    <label id="lblDataAtendimentoLT" class="col-lg-2 control-label">Data</label>
                                    <div class="col-lg-4">
                                        <div class="input-group">
                                            <span class="input-group-addon" style=""><i class="fa fa-calendar fa-fw"></i></span>
                                            <cc1:MEdit ID="meDataAtendimentoLT" name="meDtAtendLT" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Data do Atendimento" ReadOnly="true"></cc1:MEdit>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label id="lblHoraAtendimentoLT" class="col-lg-2 control-label">Hora</label>
                                    <div class="col-lg-4">
                                        <div class="input-group">
                                            <span class="input-group-addon" style=""><i class="fa-solid fa-clock"></i></span>
                                            <cc1:MEdit ID="meHoraAtendimentoLT" name="meHrAtendLT" ClientIDMode="Static" runat="server" Mascara="Hora" placeholder="hh:mm" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Hora do Atendimento" ReadOnly="true"></cc1:MEdit>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                <div class="form-group">
                                    <label id="lblAtendimentoLT" class="col-12 col-xs-12 col-sm-12 col-md-12 col-lg-2 control-label">Atendimento</label>
                                    <div class="col-lg-10">
                                        <FTB:FreeTextBox ID="ftbAtendimentoLT" runat="server" AllowHtmlMode="False"
                                            AssemblyResourceHandlerPath="" AutoConfigure="" AutoGenerateToolbarsFromString="True"
                                            AutoHideToolbar="True" AutoParseStyles="True" BackColor="158, 190, 245" BaseUrl=""
                                            BreakMode="Paragraph" ButtonDownImage="False" ButtonFileExtention="gif"
                                            ButtonFolder="Images" ButtonHeight="20" ButtonImagesLocation="InternalResource"
                                            ButtonOverImage="False" ButtonPath="" ButtonSet="Office2003" ButtonWidth="21"
                                            ClientSideTextChanged="" ConvertHtmlSymbolsToHtmlCodes="False"
                                            DesignModeBodyTagCssClass="" DesignModeCss="" DisableIEBackButton="False"
                                            DownLevelCols="50" DownLevelMessage="" DownLevelMode="TextArea" DownLevelRows="10"
                                            EditorBorderColorDark="Gray" EditorBorderColorLight="Gray" EnableHtmlMode="False"
                                            EnableSsl="False" EnableToolbars="True" Focus="False" FormatHtmlTagsToXhtml="True"
                                            GutterBackColor="129, 169, 226" GutterBorderColorDark="Gray"
                                            GutterBorderColorLight="White" Height="200px" HelperFilesParameters="" HelperFilesPath=""
                                            HtmlModeCss="" HtmlModeDefaultsToMonoSpaceFont="True" ImageGalleryPath="~/imagens/"
                                            ImageGalleryUrl="ftb.imagegallery.aspx?rif={0}&amp;cif={0}"
                                            InstallationErrorMessage="InlineMessage" JavaScriptLocation="InternalResource"
                                            Language="pt-PT" PasteMode="Default" ReadOnly="False" RemoveScriptNameFromBookmarks="True"
                                            RemoveServerNameFromUrls="True" RenderMode="NotSet" ScriptMode="External"
                                            ShowTagPath="False" SslUrl="/." StartMode="DesignMode" StripAllScripting="False"
                                            SupportFolder="/aspnet_client/FreeTextBox/" TabIndex="-1" TabMode="InsertSpaces" Text=""
                                            TextDirection="LeftToRight" ToolbarBackColor="Transparent" ToolbarBackgroundImage="True"
                                            ToolbarImagesLocation="InternalResource" ToolbarLayout="FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList|Cut,Copy,Paste;Undo,Redo"
                                            ToolbarStyleConfiguration="Office2003" UpdateToolbar="True" UseToolbarBackGroundImage="True"
                                            Width="100%">
                                        </FTB:FreeTextBox>
                                    </div>
                                </div>
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                            </div>
                        </div>
                        <!--Footer-->

                        <div class="modal-footer justify-content-center">
                            <div class="btn-group" role="group">
                                <asp:LinkButton runat="server" ID="btnAtendFechar" CssClass="btn btn-default" data-dismiss="modal" OnClick="btnAtendFechar_Click"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnAtendSalvar" CssClass="btn btn-primary-nutrovet" OnClick="btnAtendSalvar_Click"> <i class='far fa-save'></i> Salvar </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!--/.Content-->
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="mdAtendimentoLT" runat="server"
                PopupControlID="modalAtendimentoLT" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblPopUpAtendimentoLT">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblPopUpAtendimentoLT" runat="server" Text=""></asp:Label>
            <!-- Central Modal Medium Success-->


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
