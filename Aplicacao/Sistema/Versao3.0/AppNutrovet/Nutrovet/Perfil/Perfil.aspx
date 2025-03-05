<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" ValidateRequest="false"
    AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Nutrovet.Perfil.Perfil" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {
                $('#txbDataNascimentoAssinantePerfil').datepicker({
                    format: "dd/mm/yyyy",
                    clearBtn: true,
                    language: "pt-BR",
                    daysOfWeekHighlighted: "0,6",
                    autoclose: true,
                    todayHighlight: true
                });

                $('#<%= tbxCnpjCpfAssinantePerfil.ClientID %>').on('input', function () {
                    this.value = this.value.replace(/[^0-9\.\-\/]/g, '');
                    let tipoPessoa = $('#ddlTipoPessoaAssinante').val();
                    let cpfCnpj = $(this).val().replace(/\D/g, '');
                    
                    if (tipoPessoa === 'Fisica' && cpfCnpj.length <= 11) {
                        $(this).val(cpfCnpj.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4"));
                    } else if (tipoPessoa === 'Juridica' && cpfCnpj.length <= 14) {
                        $(this).val(cpfCnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5"));
                    }
                });

            });

            $('#<%= tbxCnpjCpfAssinantePerfil.ClientID %>').on('change', function () {
                let cpfCnpj = $(this).val().replace(/\D/g, '');

                if (cpfCnpj.length === 11) {
                    if (!validateCPF(cpfCnpj)) {
                        alert('CPF inválido!');
                        $(this).focus();
                    } else {
                        // Aplicar máscara de CPF
                        $(this).val(cpfCnpj.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4"));
                    }
                } else if (cpfCnpj.length === 14) {
                    if (!validateCNPJ(cpfCnpj)) {
                        alert('CNPJ inválido!');
                        $(this).focus();
                    } else {
                        // Aplicar máscara de CNPJ
                        $(this).val(cpfCnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5"));
                    }
                }
            });

        }

        function validateCPF(cpf) {
            if (cpf.length !== 11 || /(\d)\1{10}/.test(cpf)) return false;
            let sum = 0, remainder;
            for (let i = 1; i <= 9; i++) sum += parseInt(cpf.substring(i - 1, i)) * (11 - i);
            remainder = (sum * 10) % 11;
            if ((remainder === 10) || (remainder === 11)) remainder = 0;
            if (remainder !== parseInt(cpf.substring(9, 10))) return false;
            sum = 0;
            for (let i = 1; i <= 10; i++) sum += parseInt(cpf.substring(i - 1, i)) * (12 - i);
            remainder = (sum * 10) % 11;
            if ((remainder === 10) || (remainder === 11)) remainder = 0;
            if (remainder !== parseInt(cpf.substring(10, 11))) return false;
            return true;
        }

        function validateCNPJ(cnpj) {
            if (cnpj.length !== 14 || /(\d)\1{13}/.test(cnpj)) return false;
            let length = cnpj.length - 2;
            let numbers = cnpj.substring(0, length);
            let digits = cnpj.substring(length);
            let sum = 0;
            let pos = length - 7;
            for (let i = length; i >= 1; i--) {
                sum += numbers.charAt(length - i) * pos--;
                if (pos < 2) pos = 9;
            }
            let result = sum % 11 < 2 ? 0 : 11 - sum % 11;
            if (result != digits.charAt(0)) return false;
            length = length + 1;
            numbers = cnpj.substring(0, length);
            sum = 0;
            pos = length - 7;
            for (let i = length; i >= 1; i--) {
                sum += numbers.charAt(length - i) * pos--;
                if (pos < 2) pos = 9;
            }
            result = sum % 11 < 2 ? 0 : 11 - sum % 11;
            if (result != digits.charAt(1)) return false;
            return true;
        }

        $('body').on('keydown', 'input, select, textarea', function (e) {
            var self = $(this),
                form = self.parents('form:eq(0)'),
                focusable,
                next;

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
        <contenttemplate>
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
                                                    <asp:LinkButton ID="aReceituario" class="text-center" runat="server" OnClick="aReceituarioPerfil_Click"><i class="fas fa-book-medical" aria-deden="true"></i>&nbsp;Receituário</asp:LinkButton>
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
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
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
                                                                            <strong>
                                                                                <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbNacionalidadeAssinantePerfil" Text="Nacionalidade"></asp:Label></h5>
                                                                        </strong>
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
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lblTipoPessoaAssinantePerfil" Text="Tipo de Pessoa"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-user-cog"></i></span>
                                                                                <asp:DropDownList ID="ddlTipoPessoaAssinante" runat="Server" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Tipo Pessoa" AutoPostBack="True" OnSelectedIndexChanged="ddlTpPessoaAssinante_SelectedIndexChanged">
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
                                                                                <%--<cc1:medit id="meCnpjCpfAssinantePerfil" runat="server" placeholder="CPF" cssclass="form-control" data-toggle="tooltip" data-placement="top" title="CPF do Assinante" mascara="CPF" maxlength="100"></cc1:medit>--%>
                                                                                <asp:TextBox ID="tbxCnpjCpfAssinantePerfil" runat="server" CssClass="form-control" placeholder="Documento" MaxLength="18"  data-toggle="tooltip" data-placement="top" title="CPF do Assinante"></asp:TextBox>
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
                                                                                    <cc1:medit id="txbDataNascimentoAssinantePerfil" name="txbDataNascimentoAssinantePerfil" clientidmode="Static" runat="server" mascara="Data" placeholder="dd/mm/aaaa" cssclass="form-control" data-toggle="tooltip" data-placement="top" title="Data de Nascimento"></cc1:medit>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group"></div>
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbTelefoneAssinantePerfil" Text="Telefone"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fa fa-phone-square fa-fw"></i></span>
                                                                                <cc1:medit id="tbxTelefoneAssinantePerfil" runat="server" mascara="Telefone" placeholder="(xx) xxxx-xxxx" cssclass="form-control" data-toggle="tooltip" data-placement="top" title="Telefone"></cc1:medit>
                                                                            </div>
                                                                        </div>
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbCelularAssinantePerfil" Text="Celular"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-mobile-alt fa-fw"></i></span>
                                                                                <cc1:medit id="tbxCelularAssinantePerfil" runat="server" mascara="Celular" placeholder="(xx) xxxxx-xxxx" cssclass="form-control" data-toggle="tooltip" data-placement="top" title="Celular"></cc1:medit>
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
                                                                                            <cc1:medit id="meCEPAssinantePerfil" runat="server" placeholder="xxxxx-xxx" cssclass="form-control" data-toggle="tooltip" data-placement="top" textmode="SingleLine" title="CEP" autopostback="true" ontextchanged="tbCEP_TextChanged" mascara="CEP"></cc1:medit>
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
                                                                        <div role="group">
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
                                                                    <asp:Label ID="lbDataFinalAssinaturaAssinantePerfil" runat="server" Text="" class="label pull-right"></asp:Label>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                            <div>
                                                                <asp:LinkButton ID="lbAlterarPlanoAssinantePerfil" runat="server" CssClass="btn btn-primary-nutrovet pull-right" OnClick="lbAlterarPlano_Click" Enabled="True"><i class="fas fa-exchange-alt" ></i> Renovar</asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
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
                                                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group ">
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
                                                                                        <ajaxtoolkit:confirmbuttonextender id="ConfirmButtonExtender1" runat="server" confirmtext="Ao realizar o Cancelamento seu acesso será imediatamente bloqueado, mesmo que ainda reste tempo útil até o próximo vencimento. DESEJA REALMENTE CANCELAR SUA ASSINATURA?" targetcontrolid="lbCancelarAssinatura" />
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
                                                            <div>
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
                                                                                    <tr class="text-center">
                                                                                        <th style="width: 15%">Ação
                                                                                        </th>
                                                                                        <th style="width: 20%">
                                                                                            <asp:Label ID="lblListaNumeroCartaoCredito" runat="server" Text="Número"></asp:Label>
                                                                                        </th>
                                                                                        <th style="width: 10%">
                                                                                            <asp:Label ID="lblListaValidadeCartaoCredito" runat="server" Text="Validade"></asp:Label>
                                                                                        </th>
                                                                                        <th style="width: 40%">
                                                                                            <asp:Label ID="lblListaNomeCartaoCredito" runat="server" Text="Nome"></asp:Label>
                                                                                        </th>
                                                                                        <th style="width: 15%">
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
                                                                                    <ajaxtoolkit:confirmbuttonextender id="ConfirmButtonExtender2" runat="server" confirmtext="Deseja Realmente Excluir este Cartão de Crédito?" targetcontrolid="lbExcluirCartaoCredito" />
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

                                                        </div>
                                                    </asp:View>
                                                    <asp:View ID="tabTrocarSenhaPerfil" runat="server">
                                                        <div class="panel-body">
                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="lblAlterarUsuarioAcesso" runat="server" Text="<i class='fa fa-thumb-tack'></i> Alterar Usuário de Acesso - <span class='label label-default'>Realizando a troca do usuário de acesso.</span> <i class='fa fa-thumb-tack pull-right'></i>"></asp:Label>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
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
                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
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
                                                                        <asp:Label ID="Label22" runat="server" Text="Realizando a troca da senha." class="label label-default pull-right"></asp:Label>
                                                                    </h3>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
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
                                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
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
                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
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
                                                                    <asp:Label ID="Label32" runat="server" Text="Selecione o arquivo a partir do seu computador." class="label label-default pull-right"></asp:Label>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
                                                            <div class="ibox-content">
                                                                <asp:UpdatePanel ID="UpdatePanelUpld" runat="server">
                                                                    <contenttemplate>
                                                                        <div class="row">
                                                                            <div class="col-lg-6">
                                                                                <div class="ibox ">
                                                                                    <div class="ibox-title">
                                                                                        <h4>VISUALIZAÇÃO</h4>
                                                                                    </div>
                                                                                    <div class="ibox-content">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4">
                                                                                                <asp:Image ID="imgFoto" ImageUrl="~/Imagens/user1.png" alt="..." class="img-thumbnail profile_img" runat="server" />
                                                                                            </div>

                                                                                            <div class="col-md-3 col-sm-3 col-lg-3 col-xs-4 form-group list-group pull-center">
                                                                                                <asp:FileUpload ID="FileUpload1" runat="server" accept=".png,.jpg,.jpeg" />
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6">
                                                                                <div class="ibox ">
                                                                                    <div class="ibox-title">
                                                                                        <h4>Arquivo para a Imagem do Perfil Enviado</h4>
                                                                                    </div>
                                                                                    <div class="ibox-content">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <div class="list-group pull-center">
                                                                                                    <asp:Label ID="lblFileUpload" runat="server" class="list-group-item list-group-item-success"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12">
                                                                                <div class="modal-footer col-lg-12">
                                                                                    <div class="btn-group" role="group">
                                                                                        <asp:LinkButton ID="lbEnviaImagem" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbEnviaImagem_Click"><i class="fas fa-cloud-upload-alt"></i> Enviar</asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </contenttemplate>
                                                                    <triggers>
                                                                        <asp:PostBackTrigger ControlID="lbEnviaImagem" />
                                                                    </triggers>
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
                                                                    <asp:Label ID="Label33" runat="server" Text="Envie mensagem para o administrador." class="label label-default pull-right"></asp:Label>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
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
                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="Label2" runat="server" Text="<i class='fa fa-thumb-tack'></i> Dados da Clínica"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                </h3>
                                                            </div>
                                                            <div class="ibox-content">
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label7" Text="Nome&nbsp;Clínica"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-user fa-fw"></i></span>
                                                                                <asp:TextBox ID="tbNomeclinica" runat="server" CssClass="form-control" placeholder="Nome da Clínica" MaxLength="300"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label9" Text="E-mail"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fa fa-at fa-fw"></i></span>
                                                                                <asp:TextBox ID="tbEMailClinica" runat="server" CssClass="form-control" placeholder="E-Mail da Clínica" MaxLength="200"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label13" Text="CRMV Responsável"></asp:Label>
                                                                        </h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-id-card"></i></span>
                                                                                <asp:TextBox ID="tbCrmv" runat="server" CssClass="form-control" placeholder="CRMV do Responsável" MaxLength="50"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div id="divUfCrmv" runat="server">
                                                                            <h5>
                                                                                <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label34" Text="UF do CRMV"></asp:Label>
                                                                            </h5>
                                                                            <div class="col-lg-5">
                                                                                <div class="input-group">
                                                                                    <span class="input-group-addon"><i class="fas fa-id-card"></i></span>
                                                                                    <asp:DropDownList ID="ddlUfCrmv" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="UF do CRMV"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label16" Text="Telefone"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fa fa-phone-square fa-fw"></i></span>
                                                                                <cc1:medit id="meFoneClinica" runat="server" mascara="Telefone" placeholder="(xx) xxxx-xxxx" cssclass="form-control" data-toggle="tooltip" data-placement="top" title="Telefone da Clínica"></cc1:medit>
                                                                            </div>
                                                                        </div>
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label17" Text="Celular"></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-mobile-alt fa-fw"></i></span>
                                                                                <cc1:medit id="meCelularClinica" runat="server" mascara="Celular" placeholder="(xx) xxxxx-xxxx" cssclass="form-control" data-toggle="tooltip" data-placement="top" title="Celular da Clínica"></cc1:medit>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label14" Text="Slogan Clínica"></asp:Label>
                                                                        </h5>
                                                                        <div class="col-lg-5" runat="server" id="div1">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-id-card"></i></span>
                                                                                <asp:TextBox ID="tbSlogan" runat="server" ClientIDMode="Static" placeholder="Escreva aqui o Slogan da Clínica" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Escreva aqui o Slogan da Clínica." Rows="2" MaxLength="300" Height="35px" BackColor="#FFFAFA" TextMode="MultiLine" Font-Bold="True" BorderStyle="Outset"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="Label12" runat="server" Text="<i class='fa fa-thumb-tack'></i> Cabeçalho"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
                                                            <div class="ibox-content">
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <asp:CheckBox ID="cbxCabecalho" runat="server" Text="&nbsp;&nbsp;Usar Texto Livre para Cabeçalho?" OnCheckedChanged="cbxCabecalho_CheckedChanged" AutoPostBack="True" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label30" Text="Texto"></asp:Label></h5>
                                                                        <div class="col-lg-10">
                                                                            <div class="input-group">
                                                                                <ftb:freetextbox id="ftbCabecalho" runat="server" allowhtmlmode="False"
                                                                                    assemblyresourcehandlerpath="" autoconfigure="" autogeneratetoolbarsfromstring="True"
                                                                                    autohidetoolbar="True" autoparsestyles="True" backcolor="158, 190, 245" baseurl=""
                                                                                    breakmode="Paragraph" buttondownimage="False" buttonfileextention="gif"
                                                                                    buttonfolder="Images" buttonheight="20" buttonimageslocation="InternalResource"
                                                                                    buttonoverimage="False" buttonpath="" buttonset="Office2003" buttonwidth="21"
                                                                                    clientsidetextchanged="" converthtmlsymbolstohtmlcodes="False"
                                                                                    designmodebodytagcssclass="" designmodecss="" disableiebackbutton="False"
                                                                                    downlevelcols="50" downlevelmessage="" downlevelmode="TextArea" downlevelrows="10"
                                                                                    editorbordercolordark="Gray" editorbordercolorlight="Gray" enablehtmlmode="False"
                                                                                    enablessl="False" enabletoolbars="True" focus="False" formathtmltagstoxhtml="True"
                                                                                    gutterbackcolor="129, 169, 226" gutterbordercolordark="Gray"
                                                                                    gutterbordercolorlight="White" height="150px" helperfilesparameters="" helperfilespath=""
                                                                                    htmlmodecss="" htmlmodedefaultstomonospacefont="True" imagegallerypath="~/imagens/"
                                                                                    imagegalleryurl="ftb.imagegallery.aspx?rif={0}&amp;cif={0}"
                                                                                    installationerrormessage="InlineMessage" javascriptlocation="InternalResource"
                                                                                    language="pt-PT" pastemode="Default" readonly="True" removescriptnamefrombookmarks="True"
                                                                                    removeservernamefromurls="True" rendermode="NotSet" scriptmode="External"
                                                                                    showtagpath="False" sslurl="/." startmode="DesignMode" stripallscripting="False"
                                                                                    supportfolder="/aspnet_client/FreeTextBox/" tabindex="-1" tabmode="InsertSpaces" text=""
                                                                                    textdirection="LeftToRight" toolbarbackcolor="Transparent" toolbarbackgroundimage="True"
                                                                                    toolbarimageslocation="InternalResource" toolbarlayout="FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList|Cut,Copy,Paste;Undo,Redo"
                                                                                    toolbarstyleconfiguration="Office2003" updatetoolbar="True" usetoolbarbackgroundimage="True"
                                                                                    width="100%">
                                                                                </ftb:freetextbox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12"></div>
                                                            </div>

                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="Label37" runat="server" Text="<i class='fa fa-thumb-tack'></i> Rodapé da Assinatura"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
                                                            <div class="ibox-content">
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <asp:CheckBox ID="cbxRodape" runat="server" Text="&nbsp;&nbsp;Usar Texto Livre para Rodapé da Assinatura?" OnCheckedChanged="cbxRodape_CheckedChanged" AutoPostBack="True" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label36" Text="Texto"></asp:Label></h5>
                                                                        <div class="col-lg-8">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fas fa-file-alt" aria-hidden="true"></i></span>
                                                                                <asp:TextBox ID="tbxRodape" runat="server" CssClass="form-control" placeholder="Texto Livre para Rodapé da Assinatura" MaxLength="100" Enabled="False"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12"></div>
                                                            </div>

                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="Label8" runat="server" Text="<i class='fa fa-thumb-tack'></i> Redes Sociais da Clínica"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
                                                            <div class="ibox-content">
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <h5>
                                                                            <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label11" Text="Site "></asp:Label></h5>
                                                                        <div class="col-lg-5">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon"><i class="fa fa-bookmark" aria-hidden="true"></i></span>
                                                                                <asp:TextBox ID="tbSiteClinica" runat="server" CssClass="form-control" placeholder="Site da Clínica" MaxLength="300"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12"></div>
                                                            </div>

                                                            <div class="ibox-titlePI">
                                                                <h3 class="no-margins">
                                                                    <asp:Label ID="Label18" runat="server" Text="<i class='fa fa-thumb-tack'></i> Endereço da Clínica"></asp:Label>
                                                                    <i class='fa fa-thumb-tack pull-right'></i>
                                                                    <asp:Label ID="Label19" runat="server" Text="" class="pull-right"></asp:Label>
                                                                </h3>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12"></div>
                                                            <div class="ibox-content">
                                                                <asp:MultiView ID="mvEndClinica" runat="server" ActiveViewIndex="0">
                                                                    <asp:View ID="View1" runat="server">
                                                                        <div class="row">
                                                                            <div class="form-group">
                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label20" Text="CEP"></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon"><i class="fas fa-map-marker-alt"></i></span>
                                                                                        <cc1:medit id="meCEPClinica" runat="server" placeholder="xxxxx-xxx" cssclass="form-control" data-toggle="tooltip" data-placement="top" textmode="SingleLine" title="CEP" autopostback="true" ontextchanged="meCEPClinica_TextChanged" mascara="CEP"></cc1:medit>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:View>
                                                                    <asp:View ID="View2" runat="server">
                                                                        <div class="row">
                                                                            <div class="form-group">
                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label21" Text=""></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <h5>
                                                                                        <i class="fas fa-map-marker-alt"></i>&nbsp;
                                                                                            <asp:Label runat="server" ID="lblCepClinInfo" Text="CEP Informado"></asp:Label>
                                                                                        <asp:LinkButton ID="lbCorrigirCEPClinicaInformado" runat="server" CssClass="text-muted small" OnClick="lbCorrigirCEPClinicaInformado_Click">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-reply"></i>&nbsp;Corrigir</asp:LinkButton>
                                                                                    </h5>
                                                                                </div>
                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label23" Text="País"></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon"><i class="fas fa-flag"></i></span>
                                                                                        <asp:DropDownList ID="ddlPaisClinica" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="top" title="País"></asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="form-group">
                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label24" Text="Logradouro"></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon"><i class="fas fa-map-signs"></i></span>
                                                                                        <asp:TextBox ID="tbLogrClinica" runat="server" placeholder="Logradouro da Clínica" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Logradouro da Clínica" MaxLength="300"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label25" Text="Número"></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon" style=""><i class="fas fa-map-marker"></i></span>
                                                                                        <asp:TextBox ID="tbNrLogrClinica" runat="server" placeholder="Número do Logradouro da Clínica" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Nr do Logradouro da Clínica" MaxLength="10"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="form-group">
                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label26" Text="Complemento"></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon" style=""><i class="fas fa-map-marker"></i></span>
                                                                                        <asp:TextBox ID="tbComplClinica" runat="server" placeholder="Complemento do Logradouro da Clínica" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Complemento do Logradouro da Clínica" MaxLength="100"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label27" Text="Bairro"></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon" style=""><i class="fas fa-map-pin"></i></span>
                                                                                        <asp:TextBox ID="tbBairroClinica" runat="server" placeholder="Bairro da Clínica" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Bairro da Clínica" MaxLength="200"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="form-group">
                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label28" Text="Município da Clínica"></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon" style=""><i class="fas fa-map-marked-alt"></i></span>
                                                                                        <asp:TextBox ID="tbMuniClinica" runat="server" placeholder="Município da Clínica" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Município da Clínica" MaxLength="200"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                                <h5>
                                                                                    <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="Label29" Text="Estado"></asp:Label></h5>
                                                                                <div class="col-lg-5">
                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon" style=""><i class="fas fa-map"></i></span>
                                                                                        <asp:TextBox ID="tbUFClinica" runat="server" placeholder="UF da Clínica" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="UF da Clínica" MaxLength="10"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:View>
                                                                </asp:MultiView>
                                                            </div>
                                                            <div class="modal-footer col-lg-12">
                                                                <div class="btn-group" role="group">
                                                                    <asp:LinkButton ID="lbSalvarReceituario" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvarReceituario_Click"><i class="far fa-save"></i> Salvar</asp:LinkButton>
                                                                </div>
                                                            </div>


                                                            <div class="ibox float-e-margins" runat="server" id="divImgReceit">
                                                                <div class="ibox-titlePI">
                                                                    <h3 class="no-margins">
                                                                        <asp:Label ID="Label31" runat="server" Text="<i class='fa fa-thumb-tack'></i> Imagens"></asp:Label>
                                                                        <i class='fa fa-thumb-tack pull-right'></i>
                                                                    </h3>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12 form-group "></div>
                                                                <div class="row">
                                                                    <div class="col-lg-6">
                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                            <contenttemplate>
                                                                                <div class="ibox ">
                                                                                    <div class="ibox-title">
                                                                                        <h4>LOGOTIPO PARA CABEÇALHO DA RECEITA</h4>
                                                                                    </div>
                                                                                    <div class="ibox-content">
                                                                                        <div class="row">
                                                                                            <div style="display: flex; justify-content: center; flex-direction: column; align-items: flex-start;">
                                                                                                <asp:Image ID="imgLogo" ImageUrl="~/Imagens/user1.png" alt="imagem Logotipo" runat="server" Style="width: 40%;" />
                                                                                            </div>
                                                                                            <div class="col-md-3 col-sm-3 col-lg-3 col-xs-4 form-group list-group pull-center">
                                                                                                <asp:FileUpload ID="fupldLogo" runat="server" accept=".png,.jpg,.jpeg" />
                                                                                            </div>

                                                                                        </div>

                                                                                        <div class="row">
                                                                                            <div class="pagina-footer col-lg-12 text-center">
                                                                                                <div class="btn-group" role="group">
                                                                                                    <asp:LinkButton ID="lbEnviaLogo" runat="server" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" OnClick="lbEnviaLogo_Click"><i class="fas fa-cloud-upload-alt"></i> Enviar Logotipo</asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <div class="list-group pull-center">
                                                                                                    <asp:Label ID="lblFileUploadLogo" runat="server" class="list-group-item list-group-item-success"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </contenttemplate>
                                                                            <triggers>
                                                                                <asp:PostBackTrigger ControlID="lbEnviaLogo" />
                                                                            </triggers>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-lg-6">
                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                            <contenttemplate>
                                                                                <div class="ibox ">
                                                                                    <div class="ibox-title">
                                                                                        <h4>ASSINATURA DIGITALIZADA</h4>
                                                                                    </div>
                                                                                    <div class="ibox-content">
                                                                                        <div class="row">
                                                                                            <div style="display: flex; justify-content: center; flex-direction: column; align-items: flex-start;">
                                                                                                <asp:Image ID="imgAssinatura" ImageUrl="~/Imagens/user1.png" alt="imagem Assinatura" runat="server" Style="width: 40%;" />
                                                                                            </div>
                                                                                            <div class="col-md-3 col-sm-3 col-lg-3 col-xs-4 form-group list-group pull-center">
                                                                                                <asp:FileUpload ID="fupldAssinatura" runat="server" accept=".png,.jpg,.jpeg" />
                                                                                            </div>

                                                                                        </div>

                                                                                        <div class="row">
                                                                                            <div class="pagina-footer col-lg-12 text-center">
                                                                                                <div class="btn-group" role="group">
                                                                                                    <asp:LinkButton ID="lbEnviaAssinatura" runat="server" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" OnClick="lbEnviaAssinatura_Click"><i class="fas fa-cloud-upload-alt"></i> Enviar Logotipo</asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <div class="list-group pull-center">
                                                                                                    <asp:Label ID="lblFileUploadAssinatura" runat="server" class="list-group-item list-group-item-success"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </contenttemplate>
                                                                            <triggers>
                                                                                <asp:PostBackTrigger ControlID="lbEnviaAssinatura" />
                                                                            </triggers>
                                                                        </asp:UpdatePanel>
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
                                        <cc1:medit id="meNumeroCartaoCredito" runat="server" placeholder="0000 0000 0000 0000" cssclass="form-control" data-toggle="tooltip" data-placement="top" textmode="SingleLine" title="Número do Cartão de Crédito" mascara="Inteiro"></cc1:medit>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                            <div class="form-group">
                                <label id="lblCodigoSegurancaCartaoCredito" class="col-sm-2 col-lg-2 control-label">CVV</label>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fas fa-key"></i></span>
                                        <cc1:medit id="meCodigoSegurancaCartaoCredito" runat="server" placeholder="0000" min="0" cssclass="form-control" data-toggle="tooltip" data-placement="top" title="Código de Segurança" maxlength="4" mascara="Inteiro"></cc1:medit>
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
            <ajaxtoolkit:modalpopupextender id="popUpModal" runat="server"
                popupcontrolid="myModal" backgroundcssclass="modalBackground"
                repositionmode="RepositionOnWindowResize"
                targetcontrolid="lblPopUp">
            </ajaxtoolkit:modalpopupextender>
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
                            <asp:UpdateProgress ID="UpdateProgress1" class="divProgress" runat="server" DisplayAfter="1000">
                                <progresstemplate>
                                    <div class="semipolar-spinner pull-center">
                                        <div class="ring"></div>
                                        <div class="ring"></div>
                                        <div class="ring"></div>
                                        <div class="ring"></div>
                                        <div class="ring"></div>
                                    </div>
                                </progresstemplate>
                            </asp:UpdateProgress>
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
                                                            <cc1:medit id="meVoucher" runat="server" cssclass="form-control mx-sm-3 mb-2" placeholder="Informe aqui para receber o benefício" min="0" tooltip="Informe o número do VOUCHER" mascara="Inteiro"></cc1:medit>
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
                            <div class="col-md-4 pull-center">
                                <div class="card-content" id="cardBasico" runat="server" style="text-align: center;">
                                    <img class="img-card" src="../Imagens/aviaopapel - menor.png" />
                                    <h6><asp:Label ID="lblDescrB" runat="server" Text=""></asp:Label></h6>
                                    <h5 class="card-title" >BÁSICO</h5>
                                    <h6 class="badge"><i class="fas fa-paper-plane"></i>&nbsp;até 10 pacientes</h6>
                                    <div class="radio radio-primary" style="display: flex; justify-content: center; align-items: center;">
                                        <asp:RadioButtonList ID="rblBasico" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblBasico_SelectedIndexChanged">
                                            <asp:ListItem Value="1">R$ 20,00 por mês</asp:ListItem>
                                            <asp:ListItem Value="2">R$ 200,00 por ano</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 pull-center">
                                <div class="card-content" id="cardIntermediario" runat="server" style="text-align: center;">
                                    <img class="img-card" src="../Imagens/aviao - menor.png" />
                                    <h6><asp:Label ID="lblDescrI" runat="server" Text=""></asp:Label></h6>
                                    <h5 class="card-title">INTERMEDIÁRIO</h5>
                                    <h6 class="badge"><i class="fas fa-plane fa-flip-horizontal"></i>&nbsp;até 20 pacientes</h6>
                                    <div class="radio radio-primary"  style="display: flex; justify-content: center; align-items: center;">
                                        <asp:RadioButtonList ID="rblIntermediario" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblIntermediario_SelectedIndexChanged">
                                            <asp:ListItem Value="1">R$ 40,00 por mês</asp:ListItem>
                                            <asp:ListItem Value="2">R$ 400,00 por ano</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 pull-center">
                                <div class="card-content" id="cardCompleto" runat="server" style="text-align: center;">
                                    <img class="img-card" src="../Imagens/foguete - menor.png" />
                                    <h6><asp:Label ID="lblDescrC" runat="server" Text=""></asp:Label></h6>
                                    <h5 class="card-title pull-center" style="text-align: center;">COMPLETO</h5>
                                    <h6 class="badge"><i class="fas fa-rocket"></i>&nbsp;acima de 20 pacientes</h6>
                                    <div class="radio radio-primary " style="display: flex; justify-content: center; align-items: center;">
                                        <asp:RadioButtonList ID="rblCompleto" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblCompleto_SelectedIndexChanged">
                                            <asp:ListItem Value="1">R$ 60,00 por mês</asp:ListItem>
                                            <asp:ListItem Value="2">R$ 600,00 por ano</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <h5><strong><asp:Label runat="server" CssClass="col-lg-4 control-label" ID="lblSelecionaCC" Text="Selecionar Cartão Crédito"></asp:Label></strong></h5>
                            <div class="col-lg-8">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fas fa-id-card"></i></span>
                                    <asp:DropDownList ID="ddlCCRenovAssin" runat="Server" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Escolher Catão de Crédito para renovar Assinatura">
                                    </asp:DropDownList>
                                </div>
                            </div>   
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
            <ajaxtoolkit:modalpopupextender id="popUpModalEscolhaPlanoRenovacao" runat="server"
                popupcontrolid="modalEscolhaPlanoRenovacao" backgroundcssclass="modalBackground"
                repositionmode="RepositionOnWindowResize"
                targetcontrolid="lblPopUpModalEscolhaPlanoRenovacao">
            </ajaxtoolkit:modalpopupextender>
            <asp:Label ID="lblPopUpModalEscolhaPlanoRenovacao" runat="server" Text=""></asp:Label>
            <!-- Modal Plano de Renovação -->

            <div id="modalCamposObrigatorios" runat="server" class="modal-dialog modalControleMenor" style="display: none">
                <div class="modal-content animated fadeIn">
                    <div class="modal-header modal-header-success">
                        <asp:LinkButton runat="server" ID="LinkButton3" CssClass="close" data-dismiss="modal" ForeColor="White"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></asp:LinkButton>
                        <h4 class="modal-title"><i class="fa fa-exclamation-triangle" aria-hidden="true"></i>
                            <asp:Label ID="lblTituloModalCamposObrigatorios" runat="server" Text=""></asp:Label>&nbsp;NutroVet Informa
                        </h4>
                    </div>
                    <div class="modal-body-menor">
                        <h4>CAMPOS OBRIGATÓRIOS</h4>
                        <p>
                            <asp:Label ID="lblTxtCamposObrig" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="btnModalCamposObrigatorios" runat="server" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                    </div>
                </div>
            </div>

            <ajaxtoolkit:modalpopupextender id="mdlCamposObrigatorios" runat="server"
                popupcontrolid="modalCamposObrigatorios" backgroundcssclass="modalBackground"
                repositionmode="RepositionOnWindowResize"
                targetcontrolid="lblCamposObrigatorios">
            </ajaxtoolkit:modalpopupextender>
            <asp:Label ID="lblCamposObrigatorios" runat="server" Text=""></asp:Label>

        </contenttemplate>
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
