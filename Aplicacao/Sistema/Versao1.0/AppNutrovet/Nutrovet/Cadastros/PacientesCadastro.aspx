<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="PacientesCadastro.aspx.cs" Inherits="Nutrovet.Cadastros.PacientesCadastro"
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
                <asp:HyperLink ID="hlkMinimalize" NavigateUrl="#" CssClass="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
            </div>
            <h2>
                <asp:Label ID="lblPagina" runat="server" Text=""></asp:Label></h2>
            <div class="col-lg-4">
                <ol class="breadcrumb">
                    <li>
                        <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server">Início</asp:HyperLink>
                    </li>
                    <li class="active">
                        <asp:HyperLink ID="hlTutoresSelecao" NavigateUrl="~/Cadastros/PacientesSelecao.aspx" runat="server"> Pacientes</asp:HyperLink>
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
            </script>

            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            <asp:Label ID="lblSubTitulo" runat="server" Text="Insira ou altere os dados do Paciente"></asp:Label>
                        </h5>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group">
                            <label id="lblTutor" class="col-lg-2 control-label">Tutor</label>
                            <div class="col-lg-6">
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
                            <div class="col-lg-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style=""><i class="fas fa-paw fa-fw"></i></span>
                                    <asp:TextBox ID="tbNomePaciente" runat="server" placeholder="Paciente" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Nome do Paciente"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblEspecie" class="col-lg-2 control-label">Espécie</label>
                            <div class="col-lg-6">
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
                            <div class="col-lg-6">
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
                            <div class="col-lg-4">
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
                            <div class="col-lg-4">
                                <div class="input-group">
                                    <span class="input-group-addon" style=""><i class="fa fa-calendar fa-fw"></i></span>
                                    <cc1:MEdit ID="meDtNasc" name="meDtNasc" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Data de Nascimento"></cc1:MEdit>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblIdade" class="col-lg-2 control-label">Idade</label>
                            <div class="col-lg-6" style="overflow-x: auto;">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 5px"><i class="fa fa-birthday-cake fa-fw"></i></span>
                                    <asp:TextBox ID="tbIdadeAnos" Style="width: 45px" runat="server" CssClass="form-control col-lg-6" data-toggle="tooltip" data-placement="top" title="Ano de Nascimento" ReadOnly="True"></asp:TextBox>
                                    <span class="input-group-addon" style="width: 5px">Anos</span>
                                    <asp:TextBox ID="tbIdadeMeses" Style="width: 45px" runat="server" CssClass="form-control col-lg-6" data-toggle="tooltip" data-placement="top" title="Mês de Nascimento" ReadOnly="True"></asp:TextBox>
                                    <span class="input-group-addon" style="width: 5px">Meses</span>
                                    <asp:TextBox ID="tbIdadeDias" Style="width: 45px" runat="server" CssClass="form-control col-lg-6" data-toggle="tooltip" data-placement="top" title="Dia de Nascimento" ReadOnly="True"></asp:TextBox>
                                    <span class="input-group-addon" style="width: 5px">Dias</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblAtual" class="col-lg-2 control-label">Peso Atual</label>
                            <div class="col-lg-3">
                                <asp:TextBox ID="tbPesoAtual" ClientIDMode="Static" runat="server" placeholder="0,00" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Peso Atual"></asp:TextBox>
                            </div>
                            <div class="col-lg-3 form-group">
                                <asp:Button ID="btnChama" runat="server" CssClass="btn input-buttontotext fa" Style="width: 32px; height: 32px" OnClick="btnHistPesoAtual_Click" Text="&#xf201;" data-toggle="tooltip" data-placement="top" title="Histórico Peso Atual" />
                            </div>
                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblPesoIdeal" class="col-lg-2 control-label">Peso Ideal</label>
                            <div class="col-lg-3">
                                <asp:TextBox ID="tbPesoIdeal" ClientIDMode="Static" runat="server" placeholder="0,00" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Peso Ideal"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                        <div class="form-group">
                            <label id="lblObs" class="col-lg-2 control-label">Observações</label>
                            <div class="col-lg-6">
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
                                    GutterBorderColorLight="White" Height="100px" HelperFilesParameters="" HelperFilesPath=""
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
                    <div class="modal-footer col-sm-8 col-lg-8">
                        <div class="btn-group" role="group">
                            <asp:LinkButton runat="server" ID="lbFechar" CssClass="btn btn-sm btn-default m-t-n-xs" OnClick="lbFechar_Click"><i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbSalvar" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" OnClick="lbSalvar_Click"><i class='far fa-save'></i> Salvar</asp:LinkButton>
                        </div>
                    </div>
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
                </div>
                <ajaxToolkit:ModalPopupExtender ID="mdHistoricoPesoAtual" runat="server"
                    PopupControlID="modalHistoricoPesoAtual" BackgroundCssClass="modalBackground"
                    RepositionMode="RepositionOnWindowResize"
                    TargetControlID="lblModalHistoricoPesoAtual" CancelControlID="LinkButton2">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Label ID="lblModalHistoricoPesoAtual" runat="server" Text=""></asp:Label>
                <!-- Modal Histórico Peso Atual -->
                
                <!-- Modal Alerta Limite Plano -->
                <div id="modalLimitePlano" runat ="server" class="modal-dialog modal-md" role="document" style="display: none">
                    <div class="modal-content animated rotateInDownLeft">
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
                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lbFechar_Click" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                            

                        </div>
                    </div>
                </div>
                <ajaxToolkit:ModalPopupExtender ID="mdSituacaoPlano" runat="server"
                    PopupControlID="modalLimitePlano" BackgroundCssClass="modalBackground"
                    RepositionMode="RepositionOnWindowResize"
                    TargetControlID="lblLimitePlano" CancelControlID="LinkButton4">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Label ID="lblLimitePlano" runat="server" Text=""></asp:Label>
                <!-- Modal Histórico Peso Atual -->

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
