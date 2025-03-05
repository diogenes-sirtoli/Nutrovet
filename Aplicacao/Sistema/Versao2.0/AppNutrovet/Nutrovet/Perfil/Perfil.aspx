<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" ValidateRequest="false"
    AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Nutrovet.Perfil.Perfil" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {
                $('#FileUpload1').change(function () {
                    var path = $(this).val();
                    if (path != '' && path != null) {
                        var q = path.substring(path.lastIndexOf('\\') + 1);
                        $('#lblFileUpload').html(q);
                    }
                })

                $('#txbDataNascimentoAssinantePerfil').datepicker({
                    format: "dd/mm/yyyy",
                    clearBtn: true,
                    language: "pt-BR",
                    daysOfWeekHighlighted: "0,6",
                    autoclose: true,
                    todayHighlight: true
                });
            });
        }

        function up(url) {
            document.getElementById('uprl').value = url;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(BindEvents);
            </script>
            <div class="">
                <div class="page-title">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row wrapper border-bottom white-bg page-heading">
                                <div class="col-lg-1">
                                    <asp:HyperLink ID="HyperLink12" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
                                </div>
                                <h2>Perfil do Usuário</h2>
                                <div class="col-lg-4">
                                    <ol class="breadcrumb">
                                        <li>
                                            <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i>&nbsp;Início</asp:HyperLink>
                                        </li>
                                        <li class="active">
                                            <i class="fas fa-portrait"></i><strong>&nbsp;Perfil</strong>
                                        </li>
                                    </ol>
                                </div>
                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
                                <div class="col-lg-12">
                                    <div class="panel panel-default">
                                        <div id="Tabs" role="tabpanel" class="">
                                            <!-- Nav tabs -->
                                            <ul class="nav  nav-pills nav-justified" role="tablist">
                                                <li id="liMeusDadosPerfil" runat="server" role="presentation" class="active">
                                                    <asp:LinkButton ID="aMeusDadosPerfil" class="text-center" runat="server" OnClick="aMeusDadosPerfil_Click"><i class="fas fa-portrait fa-fw"></i>&nbsp;Dados</asp:LinkButton>
                                                </li>
                                                <li id="liMeuPlanoPerfil" runat="server" role="presentation">
                                                    <asp:LinkButton ID="aMeuPlanoPerfil" class="text-center" runat="server" OnClick="aMeuPlanoPerfil_Click"><i class="fas fa-handshake fa-fw"></i>&nbsp;Plano</asp:LinkButton>
                                                </li>
                                                <li id="liTrocaSenhaPerfil" runat="server" role="presentation">
                                                    <asp:LinkButton ID="aTrocarSenhaPerfil" class="text-center" runat="server" OnClick="aTrocarSenhaPerfil_Click"><i class="fas fa-user-lock fa-fw"></i>&nbsp;Acesso</asp:LinkButton>
                                                </li>
                                                <li id="liImagemPerfil" runat="server" role="presentation">
                                                    <asp:LinkButton ID="aImagemPerfil" class="text-center" runat="server" OnClick="aImagemPerfil_Click"><i class="fa fa-camera fa-fw"></i>&nbsp;Imagem</asp:LinkButton>
                                                </li>
                                                <li id="liMensagemPerfil" runat="server" role="presentation">
                                                    <asp:LinkButton ID="aMensagemPerfil" class="text-center" runat="server" OnClick="aMensagemPerfil_Click"><i class="fa fa-comments fa-fw"></i>&nbsp;Mensagem</asp:LinkButton>
                                                </li>
                                                <li id="liReceituarioPerfil" runat="server" role="presentation">
                                                    <asp:LinkButton ID="aReceituario" class="text-center" runat="server" OnClick="aReceituarioPerfil_Click" Visible="False"><i class="fas fa-book-medical" aria-hidden="true"></i>&nbsp;Receituário</asp:LinkButton>
                                                </li>
                                            </ul>
                                            <!-- /Nav tabs -->
                                            <!-- MultiView tabs -->
                                            <div class="tab-content">
                                                <asp:MultiView ID="mvTabControl" runat="server" ActiveViewIndex="0">
                                                    <asp:View ID="tabMeusDadosPerfil" runat="server">
                                                        <div class="panel-body">
                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="lblDadosAssinantePerfil" runat="server" Text="<i class='fa fa-thumb-tack'></i> Dados do Assinante"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 "></div>
                                                            <div class="ibox-content">
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbNomeAssinantePerfil" Text="Nome"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-user fa-fw"></i></span>
                                                                                <span class="input-group-addon">
                                                                                    <asp:Label runat="server" CssClass=" control-label" ID="lbCodAssiante" Text="Nome"></asp:Label></span>
                                                                                <asp:TextBox ID="tbxNomeAssinantePerfil" runat="server" CssClass="form-control" placeholder="Nome do Assinante" MaxLength="300"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbEmailAssinantePerfil" Text="E-mail"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fa fa-at fa-fw"></i></span>
                                                                                <asp:TextBox ID="tbxEmailAssinantePerfil" runat="server" CssClass="form-control" placeholder="E-Mail do Assinante" MaxLength="200"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbNacionalidadeAssinantePerfil" Text="Nacionalidade"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-globe-americas"></i></span>
                                                                                <asp:DropDownList ID="ddlNacionalidadeAssinante" runat="Server" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Nacionalidade do Assinante" AutoPostBack="True" OnSelectedIndexChanged="ddlNacionalidadeAssinante_SelectedIndexChanged">
                                                                                    <asp:ListItem Selected="True" Value="0">-- Selecione --</asp:ListItem>
                                                                                    <asp:ListItem Text="Brasileira" Value="1" />
                                                                                    <asp:ListItem Text="Estrangeira" Value="2" />
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lblTipoPessoaAssinantePerfil" Text="Tipo&amp;nbsp;de&amp;nbsp;Pessoa"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-user-cog"></i></span>
                                                                                <asp:DropDownList ID="ddlTipoPessoaAssinante" runat="Server" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Tipo de Pessoa" AutoPostBack="True" OnSelectedIndexChanged="ddlTpPessoaAssinante_SelectedIndexChanged">
                                                                                    <asp:ListItem Text="Pessoa Física" Value="1" Selected="True" />
                                                                                    <asp:ListItem Text="Pessoa Jurídica" Value="2" />
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbTituloTipoPessoaAssinantePerfil" Text="CPF"></asp:Label>
                                                                        </h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-id-card"></i></span>
                                                                                <cc1:MEdit ID="meCnpjCpfAssinantePerfil" runat="server" placeholder="CPF" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="CPF do Assinante" Mascara="CPF" MaxLength="100"></cc1:MEdit>
                                                                                <asp:TextBox ID="tbxDocumentoPerfilAssinante" runat="server" CssClass="form-control" placeholder="Documento" Visible="false" MaxLength="200"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbTituloRGAssinantePerfil" Text="RG"></asp:Label>
                                                                        </h5>
                                                                        <div class="col-lg-5" runat="server" id="divRGAssinantePerfil">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-id-card"></i></span>
                                                                                <asp:TextBox ID="tbxRGAssinantePerfil" runat="server" CssClass="form-control" placeholder="RG" MaxLength="100"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                        </div>
                                                                        <div id="divDataNascimentoAssinantePerfil" runat="server">
                                                                            <h5>
                                                                                <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbDataNascimentoAssinantePerfil" Text="Data Nascimento"></asp:Label></h5>
                                                                            <div class="col-lg-5">
                                                                                <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="fas fa-birthday-cake"></i></span>
                                                                                    <cc1:MEdit ID="txbDataNascimentoAssinantePerfil" name="txbDataNascimentoAssinantePerfil" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Data de Nascimento"></cc1:MEdit>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbTelefoneAssinantePerfil" Text="Telefone"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fa fa-phone-square fa-fw"></i></span>
                                                                                <cc1:MEdit ID="tbxTelefoneAssinantePerfil" runat="server" Mascara="Telefone" placeholder="(xx) xxxx-xxxx" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Telefone"></cc1:MEdit>
                                                                            </div>
                                                                        </div>
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbCelularAssinantePerfil" Text="Celular"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-mobile-alt fa-fw"></i></span>
                                                                                <cc1:MEdit ID="tbxCelularAssinantePerfil" runat="server" Mascara="Celular" placeholder="(xx) xxxxx-xxxx" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Celular"></cc1:MEdit>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="ibox float-e-margins">
                                                                <div class="ibox-titlePI">
                                                                    <h3 class="no-margins">
                                                                        <asp:Label ID="lblEnderecoAssinantePerfi" runat="server" Text="<i class='fa fa-thumb-tack'></i> Endereço do Assinante"></asp:Label>
                                                                        <i class='fa fa-thumb-tack pull-right'></i>
                                                                        <asp:Label ID="Label10" runat="server" Text="" class="pull-right"></asp:Label>
                                                                    </h3>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
                                                                <div class="ibox-content">
                                                                    <asp:MultiView ID="mvTabControlEndereco" runat="server" ActiveViewIndex="0">
                                                                        <asp:View ID="tabCepEnderecoAssinante" runat="server">
                                                                            <div class="row">
                                                                                <div class="form-group">

                                                                                    <h5>
                                                                                        <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbCEPAssinantePerfil" Text="CEP"></asp:Label></h5>
                                                                                    <div class="col-lg-5">
                                                                                        <div class="input-group">
                                                                                            <span class="input-group-addon"><i class="fas fa-map-marker-alt"></i></span>
                                                                                            <cc1:MEdit ID="meCEPAssinantePerfil" runat="server" placeholder="xxxxx-xxx" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="CEP" AutoPostBack="true" OnTextChanged="tbCEP_TextChanged" Mascara="CEP"></cc1:MEdit>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:View>
                                                                        <asp:View ID="tabEnderecoEmpresa" runat="server">
                                                                            <div class="row">
                                                                                <div class="form-group">
                                                                                    <h5>
                                                                                        <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbVoucher" Text=""></asp:Label></h5>
                                                                                    <div class="col-lg-5">
                                                                                        <h5>
                                                                                            <i class="fas fa-map-marker-alt"></i>&nbsp;
                                                                                            <asp:Label runat="server" ID="lbCEPInformado" Text="CEP Informado"></asp:Label>
                                                                                            <asp:LinkButton ID="lbCorrigirCEPInformado" runat="server" CssClass="text-muted small" OnClick="lbCorrigirCEPInformado_Click">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-reply"></i>&nbsp;Corrigir</asp:LinkButton>
                                                                                        </h5>
                                                                                    </div>
                                                                                    <h5>
                                                                                        <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbPaisAssinantePerfil1" Text="País"></asp:Label></h5>
                                                                                    <div class="col-lg-5">
                                                                                        <div class="input-group">
                                                                                            <span class="input-group-addon"><i class="fas fa-flag"></i></span>
                                                                                            <asp:DropDownList ID="ddlPais" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="top" title="País"></asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="form-group">
                                                                                    <h5>
                                                                                        <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbLogradouroAssinantePerfil" Text="Logradouro"></asp:Label></h5>
                                                                                    <div class="col-lg-5">
                                                                                        <div class="input-group">
                                                                                            <span class="input-group-addon"><i class="fas fa-map-signs"></i></span>
                                                                                            <asp:TextBox ID="tbxLogradouroAssinantePerfil" runat="server" placeholder="Logradouro" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Logradouro" MaxLength="300"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <h5>
                                                                                        <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbNumeroLogradouroAssinantePerfil" Text="Número"></asp:Label></h5>
                                                                                    <div class="col-lg-5">
                                                                                        <div class="input-group">
                                                                                            <span class="input-group-addon" style=""><i class="fas fa-map-marker"></i></span>
                                                                                            <asp:TextBox ID="tbxNumeroLogradouroAssinantePerfil" runat="server" placeholder="Número do Logradouro" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Número do Logradouro" MaxLength="10"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="form-group">
                                                                                    <h5>
                                                                                        <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbComplementoLogradouroPerfil" Text="Complemento"></asp:Label></h5>
                                                                                    <div class="col-lg-5">
                                                                                        <div class="input-group">
                                                                                            <span class="input-group-addon" style=""><i class="fas fa-map-marker"></i></span>
                                                                                            <asp:TextBox ID="tbxComplementoLogradouroPerfil" runat="server" placeholder="Complemento do Logradouro" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Número do Logradouro" MaxLength="100"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <h5>
                                                                                        <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbBairroAssinantePerfil" Text="Bairro"></asp:Label></h5>
                                                                                    <div class="col-lg-5">
                                                                                        <div class="input-group">
                                                                                            <span class="input-group-addon" style=""><i class="fas fa-map-pin"></i></span>
                                                                                            <asp:TextBox ID="tbxBairroAssinantePerfil" runat="server" placeholder="Bairro" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Bairro" MaxLength="200"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">

                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbCidadeAssinantePerfil" Text="Cidade"></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon" style=""><i class="fas fa-map-marked-alt"></i></span>
                                                                                        <asp:TextBox ID="tbxCidadeAssinantePerfil" runat="server" placeholder="Cidade" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Cidade" MaxLength="200"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbEstadoAssinantePerfil" Text="Estado"></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon" style=""><i class="fas fa-map"></i></span>
                                                                                        <asp:TextBox ID="tbxEstadoAssinantePerfil" runat="server" placeholder="Estado" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Estado" MaxLength="10"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </asp:View>
                                                                    </asp:MultiView>

                                                                    <div class="modal-footer col-lg-12">
                                                                        <div class="" role="group">
                                                                            <asp:LinkButton ID="lbSalvarDados" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvar_Click"><i class="far fa-save"></i> Salvar</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:View>
                                                    <asp:View ID="tabMeuPlanoPerfil" runat="server">
                                                        <div class="panel-body">
                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="lblMinhaAssinatura" runat="server" Text="<i class='fa fa-thumb-tack'></i> Minha Assinatura"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                    <asp:Label ID="lbDataFinalAssinaturaAssinantePerfil" runat="server" Text="" class="label label-default pull-right"></asp:Label>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                            <div>
                                                                <asp:LinkButton ID="lbAlterarPlanoAssinantePerfil" runat="server" CssClass="btn btn-primary-nutrovet pull-right" OnClick="lbAlterarPlano_Click" Enabled="True"><i class="fas fa-exchange-alt" ></i> Renovar</asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group"></div>
                                                            <div>
                                                                <h5>
                                                                    <asp:Label ID="lblTituloPlanoAssinantePerfil" CssClass="col-lg-2 control-label" runat="server" Text="Plano"></asp:Label></h5>
                                                                <div class="col-lg-4">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><i class="fas fa-book-open"></i></span>
                                                                        <asp:Label ID="lblPlanoAssinantePerfil" CssClass="form-control" runat="server" Text="" readonly="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <h5>
                                                                    <asp:Label ID="lbTituloValorPlanoAssinantePerfil" CssClass="col-lg-2 control-label" runat="server" Text="Valor"></asp:Label></h5>
                                                                <div class="col-lg-4">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><i class="fas fa-wallet"></i></span>
                                                                        <asp:Label ID="lblValorPlanoAssinantePerfil" CssClass="form-control" runat="server" Text="" readonly="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                                <h5>
                                                                    <asp:Label ID="lbTituloCodigoAssinaturaAssinantePerfil" CssClass="col-lg-2 control-label" runat="server" Text="Código Assinatura" readonly="true"></asp:Label></h5>
                                                                <div class="col-lg-4">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><i runat="server" id="i1" clientidmode="Static" class="fas fa-user-tag"></i></span>
                                                                        <asp:Label ID="lbValorCodigoAssinaturaAssinantePerfil" CssClass="form-control" runat="server" Text="" readonly="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <h5>
                                                                    <asp:Label ID="lbTituloStatusAssinaturaAssinantePerfil" CssClass="col-lg-2 control-label" runat="server" Text="Status"></asp:Label></h5>
                                                                <div class="col-lg-4">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><i runat="server" id="iStatusAssinatura" clientidmode="Static" class="fas fa-battery-full"></i></span>
                                                                        <asp:Label ID="lbStatusAssinaturaAssinantePerfil" CssClass="form-control" runat="server" Text="" readonly="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                                <h5>
                                                                    <asp:Label ID="lbTituloDataCadastroAssinaturaAssinantePerfil" CssClass="col-lg-2 control-label" runat="server" Text="Data Cadastro"></asp:Label></h5>
                                                                <div class="col-lg-4">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><i class="fas fa-calendar"></i></span>
                                                                        <asp:Label ID="lbDataCadastroAssinaturaAssinantePerfil" CssClass="form-control" runat="server" Text="" readonly="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <h5>
                                                                    <asp:Label ID="lbTituloDataInicialAssinaturaAssinantePerfil" CssClass="col-lg-2 control-label" runat="server" Text="Início Assinatura"></asp:Label></h5>
                                                                <div class="col-lg-4">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><i class="fas fa-hourglass-end"></i></span>
                                                                        <asp:Label ID="lbDataInicialAssinaturaAssinantePerfil" CssClass="form-control" runat="server" Text="" readonly="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                                <h3>Histórico de Assinaturas</h3>
                                                                <div class="x_content table-responsive" style="width: auto">
                                                                    <asp:Repeater ID="rptAssinaturas" runat="server" OnItemDataBound="rptAssinaturas_ItemDataBound" OnItemCommand="rptAssinaturas_ItemCommand">
                                                                        <HeaderTemplate>
                                                                            <table class="table table-striped table-responsive table-hover">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th style="max-width: 80px; min-width: 80px; width: 80px;" class="text-center">Ação
                                                                                        </th>
                                                                                        <th style="max-width: 300px; min-width: 300px; width: 300px;">
                                                                                            <asp:Label ID="lblNomePlano" runat="server" Text="Plano"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblIdSubscription" runat="server" Text="Código"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblValor" runat="server" Text="Valor"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblInicio" runat="server" Text="Início"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblFim" runat="server" Text="Fim"></asp:Label>
                                                                                        </th>

                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr class="gradeA">
                                                                                <td class="text-center">
                                                                                    <asp:LinkButton ID="lbUpgradeAssinatura" runat="server" CommandName="upgrade" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdVigencia") %>' CssClass="btn btn-primary-nutrovet btn-xs" data-toggle="tooltip" data-placement="top" title="Fazer Upgrade na Assinatura"><i class="fas fa-exchange-alt"></i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="lbCancelarAssinatura" runat="server" CommandName="cancelar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdVigencia") %>' CssClass="btn btn-warning btn-xs" data-toggle="tooltip" data-placement="top" title="Cancelar sua Assinatura"><i class="fa fa-times-circle"></i></asp:LinkButton>
                                                                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Ao realizar o Cancelamento seu acesso será imediatamente bloqueado, mesmo que ainda reste tempo útil até o próximo vencimento. DESEJA REALMENTE CANCELAR SUA ASSINATURA?" TargetControlID="lbCancelarAssinatura" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblNomePlanoReg" runat="server" Text='<%# Eval("NamePlan") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblIdSubscriptionReg" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblValorReg" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblStatusReg" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblInicioReg" runat="server" Text='<%# Eval("CurrentPeriodStart") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblFimReg" runat="server" Text='<%# Eval("CurrentPeriodEnd") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </tbody>
                                                                                    </table>
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                                <div class="stat-percent font-bold text-danger"></div>
                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="Label1" runat="server" Text="<i class='fa fa-thumb-tack'></i> Cartão de Crédito Vinculado à Assinatura"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                    <asp:Label ID="Label5" runat="server" Text="" class="pull-right"></asp:Label>
                                                                </h3>
                                                            </div>

                                                            <h5>
                                                                <asp:Label ID="lbTituloNumeroCartaoCreditoAssinantePerfil" CssClass="col-lg-2 control-label" runat="server" Text="Número"></asp:Label></h5>
                                                            <div class="col-lg-4">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><i class="fas fa-credit-card"></i></span>
                                                                    <asp:Label ID="lbNumeroCartaoCreditoAssinantePerfil" CssClass="form-control" runat="server" Text="0000 0000 0000 0000" data-toggle="tooltip" data-placement="top" title="Número do Cartão de Crédito" readonly="true"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <h5>
                                                                <asp:Label ID="lbTituloVencimentoCartaoCreditoAssinantePerfil" CssClass="col-lg-2 control-label" runat="server" Text="VALIDADE <small>(Mês/Ano)</small>"></asp:Label></h5>
                                                            <div class="col-lg-4">
                                                                <div class="input-append date input-group" id="dataVencimentoCartaoCreditoAssinantePerfil" data-date-format="mmyy">
                                                                    <span class="input-group-addon"><i class="fas fa-calendar-day"></i></span>
                                                                    <asp:Label ID="lbVencimentoCartaoCreditoAssinantePerfil" CssClass="form-control" runat="server" Text="mmaa" data-toggle="tooltip" data-placement="top" title="Validade do Cartão de Crédito" readonly="true"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                            <h5>
                                                                <asp:Label ID="lbTituloNomeCartaoCreditoAssinantePerfil" CssClass="col-lg-2 control-label" runat="server" Text="Nome&amp;nbsp;no Cartão"></asp:Label></h5>
                                                            <div class="col-lg-10 ">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><i class="far fa-id-card"></i></span>
                                                                    <asp:Label ID="lbNomeCartaoCreditoAssinantePerfil" CssClass="form-control" runat="server" Text="Nome que consta no Cartão" data-toggle="tooltip" data-placement="top" title="Nome que consta no Cartão" readonly="true"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                            <h3>Cartões de Crédito Cadastrados</h3>
                                                            <div class="x_content table-responsive" style="width: auto">
                                                                <asp:Repeater ID="rptListagemCartoesCredito" runat="server" OnItemCommand="rptListagemCartoesCredito_ItemCommand" OnItemDataBound="rptListagemCartoesCredito_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <table class="table table-striped  table-hover" id="tableRepeaterCartaoCredito">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th style="width: 5%">Ação
                                                                                    </th>
                                                                                    <th style="width: 5%">
                                                                                        <asp:Label ID="lblListaNumeroCartaoCredito" runat="server" Text="Número"></asp:Label>
                                                                                    </th>
                                                                                    <th style="width: 10%">
                                                                                        <asp:Label ID="lblListaValidadeCartaoCredito" runat="server" Text="Validade"></asp:Label>
                                                                                    </th>
                                                                                    <th style="width: 35%">
                                                                                        <asp:Label ID="lblListaNomeCartaoCredito" runat="server" Text="Nome"></asp:Label>
                                                                                    </th>
                                                                                    <th style="width: 10%">
                                                                                        <asp:Label ID="lblCartaoVinculado" runat="server" Text="Vinculado"></asp:Label>
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr class="gradeA">
                                                                            <td class="text-center">
                                                                                <asp:LinkButton ID="lbVincularCCRegistro" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="vincular" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCartao") %>' data-toggle="tooltip" data-placement="top" title="Vincular Cartão de Crédito à Assinatura"><i class="far fa-circle"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbEditarCC" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCartao") %>'> <i class="fas fa-edit" data-toggle="tooltip" data-placement="top" title="Editar Cartão de Crédito"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbExcluirCartaoCredito" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCartao") %>' data-toggle="tooltip" data-placement="top" title="Excluir Cartão de Crédito"><i class="far fa-trash-alt"></i></asp:LinkButton>
                                                                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Deseja Realmente Excluir este Cartão de Crédito?" TargetControlID="lbExcluirCartaoCredito" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblNumeroCartaoCreditoRegistro" runat="server" Text='<%# Eval("NrCartao") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblValidadeCartaoCreditoRegistro" runat="server" Text='<%# Eval("VencimCartao") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblNomeCartaoCreditoRegistro" runat="server" Text='<%# Eval("NomeCartao") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblCartaoVinculadoRegistro" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                                    </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
                                                            <div class="modal-footer col-lg-12">
                                                                <div class="btn-group" role="group">
                                                                    <asp:LinkButton ID="lbInserirCartao" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbInserirCartao_Click" data-toggle="tooltip" data-placement="top" title="Inclui um Novo Cartão de Crédito"><i class="fas fa-plus-square"></i> Incluir novo Cartão</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </asp:View>
                                                    <asp:View ID="tabTrocarSenhaPerfil" runat="server">
                                                        <div class="panel-body">
                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="lblAlterarUsuarioAcesso" runat="server" Text="<i class='fa fa-thumb-tack'></i> Alterar Usuário de Acesso"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                    <asp:Label ID="Label2" runat="server" Text="Realizando a troca do usuário." class="label label-default pull-right"></asp:Label>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 "></div>
                                                            <div class="ibox-content">
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label ID="Label3" CssClass="col-lg-1 control-label" runat="server" Text="Atual"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-user-tag fa-fw"></i></span>
                                                                                <asp:TextBox ID="tbxUsuarioAtualAssinantePerfil" runat="server" placeholder="Usuário Atual" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Usuário Atual" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <h5>
                                                                            <asp:Label ID="Label4" CssClass="col-lg-1 control-label" runat="server" Text="Novo"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-user-tag fa-fw"></i></span>
                                                                                <asp:TextBox ID="tbxUsuarioNovoAssinantePerfil" runat="server" placeholder="Novo Usuário" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Novo Usuário" MaxLength="200"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group"></div>
                                                                <div class="modal-footer col-lg-12">
                                                                    <div class="btn-group" role="group">
                                                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvarAlteracaoUsuarioAcesso_Click"><i class="far fa-save"></i> Alterar Usuário</asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="lblTrocaSenhaAcessoPerfil" runat="server" Text="<i class='fa fa-thumb-tack'></i> Alterar Senha de Acesso"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                    <asp:Label ID="Label13" runat="server" Text="Realizando a troca da senha." class="label label-default pull-right"></asp:Label>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 "></div>
                                                            <div class="ibox-content">
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label ID="lblSenhaAtualAssinantePerfil" CssClass="col-lg-1 control-label" runat="server" Text="Atual"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-unlock-alt fa-fw"></i></span>
                                                                                <asp:TextBox ID="tbxSenhaAtualAssinantePerfil" runat="server" placeholder="Senha Atual" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Senha Atual" ReadOnly="True"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group"></div>
                                                                        <h5>
                                                                            <asp:Label ID="lbNovaSenhaAssinantePerfil" CssClass="col-lg-1 control-label" runat="server" Text="Nova"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-key fa-fw"></i></span>
                                                                                <asp:TextBox ID="tbxNovaSenhaAssinantePerfil" runat="server" placeholder="Nova Senha" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Nova Senha" MaxLength="50"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <h5>
                                                                            <asp:Label ID="lbConfirmacaoSenhaAssinantePerfil" CssClass="col-lg-1 control-label" runat="server" Text="Confirmação"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-key fa-fw"></i></span>
                                                                                <asp:TextBox ID="tbxConfirmacaoSenhaAssinantePerfil" runat="server" placeholder="Confirmação da Senha" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Confirmação Nova Senha" MaxLength="50"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group"></div>
                                                                <div class="modal-footer col-lg-12">
                                                                    <div class="btn-group" role="group">
                                                                        <asp:LinkButton ID="lbSalvaNovaSenhaAssinantePerfil" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvarAlteracaoSenhaAcesso_Click"><i class="far fa-save"></i> Alterar Senha</asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:View>
                                                    <asp:View ID="tabImagemPerfil" runat="server">
                                                        <div class="ibox-content col-lg-12">
                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="lblImagemAssinantePerfil" runat="server" Text="<i class='fa fa-thumb-tack'></i> Imagem do Perfil"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                    <asp:Label ID="Label11" runat="server" Text="Selecione o arquivo a partir do seu computador." class="label label-default pull-right"></asp:Label>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
                                                            <div class="ibox-content">
                                                                <asp:UpdatePanel ID="UpdatePanelUpld" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="row">
                                                                            <div class="col-lg-2">
                                                                                <asp:Image ID="imgFoto" ImageUrl="~/Imagens/user1.png" alt="..." class="img-thumbnail profile_img" runat="server" />
                                                                            </div>
                                                                            <div class="col-lg-8">
                                                                                <div class="input-group">
                                                                                    <span class="input-group-addon">
                                                                                        <asp:FileUpload ID="FileUpload1" runat="server" accept=".png,.jpg,.jpeg" />
                                                                                    </span>
                                                                                    <span class="input-group-addon"></span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                            <div class="modal-footer col-lg-12">
                                                                                <div class="btn-group" role="group">
                                                                                    <asp:LinkButton ID="lbEnviaImagem" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbEnviaImagem_Click"><i class="fas fa-cloud-upload-alt"></i> Enviar</asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                        <h3>Arquivo Enviado</h3>
                                                                        <div class="list-group pull-center">
                                                                            <asp:Label ID="lblFileUpload" runat="server" class="list-group-item list-group-item-success"></asp:Label>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="lbEnviaImagem" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </asp:View>
                                                    <asp:View ID="tabMensagemPerfil" runat="server">

                                                        <div class="ibox-content col-lg-12">

                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="lblEnviarMensagemPerfil" runat="server" Text="<i class='fa fa-thumb-tack'></i> Mensagem NutroVET"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                    <asp:Label ID="Label12" runat="server" Text="Envie mensagem para o administrador." class="label label-default pull-right"></asp:Label>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
                                                            <div class="ibox-content">
                                                                <div class="contact-form">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="tbxNome" runat="server" CssClass="form-control" placeholder="Nome" required="true" MaxLength="250"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="tbxEmail" runat="server" CssClass="form-control" placeholder="E-mail" required="true" TextMode="Email" MaxLength="250"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="tbxAssunto" runat="server" CssClass="form-control" placeholder="Assunto" required="true" MaxLength="250"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="tbxMsg" runat="server" CssClass="form-control" Rows="4" placeholder="Mensagem" required="true" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                    <div class="modal-footer col-lg-12">
                                                                        <div class="btn-group" role="group">
                                                                            <asp:LinkButton ID="lbEmail" runat="server" CssClass="btn btn-primary-nutrovet btn-outline" OnClick="lbEmail_Click"><i class="fas fa-paper-plane"></i>&nbsp;Enviar</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:View>
                                                    <asp:View ID="tabReceituarioPerfil" runat="server">
                                                        <div class="panel-body">
                                                            <div class="ibox-content col-lg-12">
                                                                <div class="flipInX col-lg-12">
                                                                    <div class="ibox float-e-margins">
                                                                        <div class="ibox-titlePI">
                                                                            <h3 class="no-margins">
                                                                                <asp:Label ID="Label7" runat="server" Text="<i class='fas fa-book-medical'></i> Receituário - <span class='label label-default'></span> <i class='fa fa-thumb-tack pull-right'></i>"></asp:Label>
                                                                            </h3>
                                                                        </div>

                                                                        <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                                        <div class="ibox-content">
                                                                            <div class="contact-form">
                                                                                <div class="form-group">
                                                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Nome" required="true"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                                <div class="modal-footer col-lg-12">
                                                                                    <div class="btn-group" role="group">
                                                                                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-primary-nutrovet btn-outline"><i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;Salvar</asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:View>
                                                </asp:MultiView>
                                            </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            </div>
            <!-- Modal Cartão de Crédito -->
            <div id="myModal" runat="server" style="display: none">
                <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                    <!--Content-->
                    <div class="modal-content">
                        <!--Header-->
                        <div class="modal-header modal-header-success">
                            <div class="col-md-9 col-sm-9 col-xs-9">
                                <asp:Label ID="lblTituloModal" runat="server" Text="Editando Registro" class="heading lead"></asp:Label>
                            </div>
                            <div class="col-md-3 col-sm-2 col-xs-2">
                                <asp:LinkButton runat="server" ID="LinkButton8" CssClass="close" data-dismiss="modal"><i class='fa fa-times-circle'></i></asp:LinkButton>
                            </div>
                        </div>
                        <!--Body-->
                        <div class="modal-body">
                            <div class="form-group">
                                <label id="lblNumeroCartaoCredito" class="col-sm-2 col-lg-2 control-label">Número</label>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fas fa-credit-card"></i></span>
                                        <cc1:MEdit ID="meNumeroCartaoCredito" runat="server" placeholder="0000 0000 0000 0000" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Número do Cartão de Crédito" Mascara="Inteiro"></cc1:MEdit>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                            <div class="form-group">
                                <label id="lblCodigoSegurancaCartaoCredito" class="col-sm-2 col-lg-2 control-label">CVV</label>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fas fa-key"></i></span>
                                        <cc1:MEdit ID="meCodigoSegurancaCartaoCredito" runat="server" placeholder="0000" min="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Código de Segurança" MaxLength="4" Mascara="Inteiro"></cc1:MEdit>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                            <div class="form-group">
                                <label id="lblValidadeCartaoCredito" class="col-sm-2 col-lg-2 control-label">Validade</label>
                                <div class="col-md-9">
                                    <div class="input-append date input-group" id="datepicker" data-date-format="mmyy">
                                        <span class="input-group-addon"><i class="fas fa-calendar"></i></span>
                                        <asp:TextBox ID="tbValidadeCartaoCreditoModal" runat="server" name="date" placeholder="mmaa" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" TextMode="SingleLine" title="Mês e Ano de Validade do Cartão" MaxLength="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                            <div class="form-group">
                                <label id="lblNomeCartaoCredito" class="col-sm-2 col-lg-2 control-label">Nome</label>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="far fa-id-card"></i></span>
                                        <asp:TextBox ID="tbNomeCartaoCredito" runat="server" placeholder="Nome que consta no Cartão de Crédito" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Nome que consta no Cartão de Crédito"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        </div>
                        <!--Footer-->
                        <div class="modal-footer justify-content-center">
                            <asp:LinkButton runat="server" ID="btnFechar" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnSalvar" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvarCartaoCredito_Click"> <i class='far fa-save'></i> Salvar </asp:LinkButton>
                        </div>
                    </div>
                    <!--/.Content-->
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="popUpModal" runat="server"
                PopupControlID="myModal" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblPopUp">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblPopUp" runat="server" Text=""></asp:Label>
            <!-- Modal Cartão de Crédito-->
            <!-- Modal Plano de Renovação -->
            <div id="modalEscolhaPlanoRenovacao" runat="server" style="display: none">
                <%--<div id="modalEscolhaPlanoRenovacao" runat="server" >--%>
                <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                    <!--Content-->
                    <div class="modal-content">
                        <!--Header-->
                        <div class="modal-header modal-header-success">
                            <div class="col-md-9 col-sm-9 col-xs-9">
                                <asp:Label ID="lblTituloModalRenovarAssinatura" runat="server" Text="Escolher Plano" class="heading lead"></asp:Label>
                            </div>
                            <div class="col-md-3 col-sm-2 col-xs-2">
                                <asp:LinkButton runat="server" ID="LinkButton1" CssClass="close" data-dismiss="modal"><i class='fa fa-times-circle'></i></asp:LinkButton>
                            </div>
                        </div>
                        <!--Body-->
                        <div class="modal-body">
                            <div class="table-responsive">
                                <table class="table shoping-cart-table">
                                    <tbody>
                                        <tr>
                                            <td class="desc">
                                                <asp:Panel ID="pnlVoucher" runat="server" DefaultButton="lbVerificarVoucher">
                                                    <h4><strong>
                                                        <asp:Label runat="server" CssClass="col-lg-2 control-label" ID="Label6" Text="Voucher"></asp:Label></h4>
                                                    </strong>
                                                    <div class="col-lg-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><i class="fas fa-gift"></i></span>
                                                            <cc1:MEdit ID="meVoucher" runat="server" CssClass="form-control mx-sm-3 mb-2" placeholder="Informe aqui seu Voucher para receber o benefício" min="0" ToolTip="Informe o número do VOUCHER" Mascara="Inteiro"></cc1:MEdit>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-1 ">
                                                        <asp:LinkButton runat="server" ID="lbVerificarVoucher" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" OnClick="lbVerificarVoucher_Click"><i class="fas fa-check-double"></i>&nbsp;Validar</asp:LinkButton>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-md-4">
                                <div class="card-content" id="cardBasico" runat="server">
                                    <div class="card-img">
                                        <img class="img-card" src="../Imagens/aviaopapel - menor.png" />
                                    </div>
                                    <p></p>
                                    <h4 class="card-title">BÁSICO</h4>
                                    <h6>
                                        <asp:Label ID="lblDescrB" runat="server" Text=""></asp:Label>
                                    </h6>
                                    <h5 class="badge"><i class="fas fa-paper-plane"></i>&nbsp;até 10 pacientes</h5>
                                    <div class="radio radio-primary">
                                        <asp:RadioButtonList ID="rblBasico" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblBasico_SelectedIndexChanged">
                                            <asp:ListItem Value="1">R$ 20,00 por mês<br></br></asp:ListItem>
                                            <asp:ListItem Value="2">R$ 200,00 por ano</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <p></p>
                            </div>
                            <div class="col-md-4 pull-center">
                                <div class="card-content" id="cardIntermediario" runat="server">
                                    <div class="card-img">
                                        <img class="img-card" src="../Imagens/aviao - menor.png" />
                                    </div>
                                    <p></p>
                                    <h4 class="card-title align-center">INTERMEDIÁRIO</h4>
                                    <h6>
                                        <asp:Label ID="lblDescrI" runat="server" Text=""></asp:Label>
                                    </h6>
                                    <h5 class="badge"><i class="fas fa-plane fa-flip-horizontal"></i>&nbsp;até 20 pacientes</h5>
                                    <div class="radio radio-primary">
                                        <asp:RadioButtonList ID="rblIntermediario" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblIntermediario_SelectedIndexChanged">
                                            <asp:ListItem Value="1">R$ 40,00 por mês<br></br></asp:ListItem>
                                            <asp:ListItem Value="2">R$ 400,00 por ano</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <p></p>
                            </div>
                            <div class="col-md-4 pull-center">
                                <div class="card-content" id="cardCompleto" runat="server">
                                    <div class="card-img">
                                        <img class="img-card" src="../Imagens/aviaopapel - menor.png" />
                                    </div>
                                    <p></p>
                                    <h4 class="card-title pull-center">COMPLETO</h4>
                                    <h6>
                                        <asp:Label ID="lblDescrC" runat="server" Text=""></asp:Label>
                                    </h6>
                                    <h5 class="badge"><i class="fas fa-rocket"></i>&nbsp;acima de 20 pacientes</h5>
                                    <div class="radio radio-primary ">
                                        <asp:RadioButtonList ID="rblCompleto" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblCompleto_SelectedIndexChanged">
                                            <asp:ListItem Value="1">R$ 60,00 por mês<br></br></asp:ListItem>
                                            <asp:ListItem Value="2">R$ 600,00 por ano</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <p></p>
                            </div>
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                            <h5>
                                <strong>
                                    <asp:Label runat="server" CssClass="col-lg-4 control-label" ID="lblSelecionaCC" Text="Selecionar Cartão Crédito"></asp:Label></h5>
                            </strong>
                                <div class="col-lg-8">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fas fa-id-card"></i></span>
                                        <asp:DropDownList ID="ddlCCRenovAssin" runat="Server" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Escolher Catão de Crédito para renovar Assinatura">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            <hr />
                            <asp:UpdateProgress ID="UpdateProgress1" class="divProgress" runat="server" DisplayAfter="1000">
                                <ProgressTemplate>
                                    <div class="semipolar-spinner pull-center">
                                        <div class="ring"></div>
                                        <div class="ring"></div>
                                        <div class="ring"></div>
                                        <div class="ring"></div>
                                        <div class="ring"></div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <hr />
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        </div>
                        <!--Footer-->
                        <div class="modal-footer justify-content-center">
                            <asp:LinkButton runat="server" ID="lbFecharModalSelecaoPano" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbSalvarModalSelecaoPano" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvarRenovarAssinatura_Click"> <i class='far fa-save'></i> Salvar </asp:LinkButton>
                        </div>
                    </div>
                    <!--/.Content-->
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="popUpModalEscolhaPlanoRenovacao" runat="server"
                PopupControlID="modalEscolhaPlanoRenovacao" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblPopUpModalEscolhaPlanoRenovacao">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblPopUpModalEscolhaPlanoRenovacao" runat="server" Text=""></asp:Label>
            <!-- Modal Plano de Renovação -->
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
