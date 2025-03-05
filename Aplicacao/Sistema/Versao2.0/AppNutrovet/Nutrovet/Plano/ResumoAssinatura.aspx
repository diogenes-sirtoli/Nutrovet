<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="ResumoAssinatura.aspx.cs" Inherits="Nutrovet.Plano.ResumoAssinatura" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {
                //$("#datepicker").datepicker({
                //    format: "mmyy",
                //    clearBtn: true,
                //    language: "pt-BR",
                //    daysOfWeekHighlighted: "0,6",
                //    keyboardNavigation: true,
                //    todayHighlight: true,
                //    minViewMode: 0,
                //    orientation: "bottom auto",
                //    rtl: false,
                //    viewMode: "months",
                //    minViewMode: "months",
                //    startDate: new Date(),
                //    autoclose: true,
                //    immediateUpdates: true,
                //    title: "Vencimento Cartão",
                //    zIndexOffset: 9999
                //});

                $('#tbDataNascimentoAssinante').datepicker({
                    format: "dd/mm/yyyy",
                    clearBtn: true,
                    language: "pt-BR",
                    daysOfWeekHighlighted: "0,6",
                    autoclose: true,
                    todayHighlight: true,
                    todayBtn: true
                });

                $('input[type=text]').bind('cut copy paste', function (e) {
                    e.preventDefault();
                    toastr["error"]("Você não pode colar texto neste campo!");
                });

                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-center",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }

                //$('#meCnpjCpfAssinante').mask('000.000.000-00', {
                //    onKeyPress: function (cpfcnpj, e, field, options) {
                //        const masks = ['000.000.000-000', '00.000.000/0000-00'];
                //        const mask = (cpfcnpj.length > 14) ? masks[1] : masks[0];
                //        $('#meCnpjCpfAssinante').mask(mask, options);
                //    }
                //});
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
    <section id="pricing" style="background: #F0F0F0;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(BindEvents);
                </script>
                <div class="container">
                    <div class="wrapper wrapper-content ">
                        <div class="row wrapper border-bottom white-bg page-heading">
                            <div class="col-lg-12">
                                <h2>Informe os dados</h2>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-xs-12 col-sm-5">
                                <div class="ibox">
                                    <div class="ibox-title">
                                        <h5><strong>Dados do Assinante</strong></h5>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="table shoping-cart-table">
                                            <tbody>
                                                <tr>
                                                    <td class="desc">
                                                        <div class="radio radio-muted radio-inline">
                                                            <asp:RadioButtonList ID="rblTpPessoa" runat="server" RepeatColumns="2" AutoPostBack="True" OnSelectedIndexChanged="rblTpPessoa_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Value="1">&nbsp;Pessoa Física&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Value="2">&nbsp;Pessoa Jurídica</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </td>
                                                    <tr>
                                                        <td class="desc">
                                                            <div class="form-group">
                                                                <div class="col-lg-12">
                                                                    <h5>
                                                                        <asp:Label runat="server" ID="lbNacionalidadeAssinante" Text="Nacionalidade"></asp:Label></h5>
                                                                    <div class="input-group" runat="server" id="divNacionalidadeAssinante" visible="true">
                                                                        <span class="input-group-addon" style=""><i class="fas fa-globe-americas"></i></span>
                                                                        <asp:DropDownList ID="ddlNacionalidadeAssinante" runat="Server" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Nacionalidade do Assinante" AutoPostBack="True" OnSelectedIndexChanged="ddlNacionalidadeAssinante_SelectedIndexChanged">
                                                                            <asp:ListItem Text="Brasileira" Value="1" />
                                                                            <asp:ListItem Text="Estrangeira" Value="2" />
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-12">
                                                                    <h5>
                                                                        <asp:Label runat="server" ID="lbNomeAssinante" Text="Nome Completo"></asp:Label></h5>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fas fa-user fa-fw"></i></span>
                                                                        <asp:TextBox ID="tbNomeAssinante" runat="server" placeholder="Nome Assinante" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Nome Assinante" onpaste="return false" ondrop="return false" MaxLength="300"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-lg-12">
                                                                    <h5>
                                                                        <asp:Label runat="server" ID="lbTituloTipoPessoaAssinante" Text="CPF"></asp:Label></h5>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fas fa-id-card"></i></span>
                                                                        <cc1:MEdit ID="meCnpjCpfAssinante" runat="server" placeholder="CPF" ClientIDMode="Static" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="CPF do Assinante" Mascara="CPF" name="meCnpjCpfAssinante" MaxLength="100"></cc1:MEdit>
                                                                        <asp:TextBox ID="tbPassaporteAssinante" runat="server" placeholder="Documento" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Documento" Visible="false" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12" id="divDataNascimento" runat="server">
                                                                <h5>Data de Nascimento</h5>
                                                                <div class="input-group">
                                                                    <span class="input-group-addon" style=""><i class="fas fa-birthday-cake"></i></span>
                                                                    <cc1:MEdit ID="tbDataNascimentoAssinante" name="tbDataNascimentoAssinante" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Data de Nascimento"></cc1:MEdit>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-lg-12">
                                                                    <h5>E-mail</h5>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fa fa-at fa-fw"></i></span>
                                                                        <asp:TextBox ID="tbEmailAssinante" runat="server" placeholder="E-mail" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="Email" title="E-mail" onpaste="return false" ondrop="return false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-lg-6">
                                                                    <h5>Senha</h5>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fas fa-key fa-fw"></i></span>
                                                                        <asp:TextBox ID="tbSenhaAssinante" runat="server" placeholder="" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="Password" title="Senha" OnTextChanged="tbSenhaAssinante_TextChanged" onpaste="return false" ondrop="return false" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-lg-6">
                                                                    <h5>Confirmar</h5>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fas fa-key fa-fw"></i></span>
                                                                        <asp:TextBox ID="tbConfSenhaAssinante" runat="server" placeholder="" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="Password" title="Confirmação da Senha" OnTextChanged="tbConfSenhaAssinante_TextChanged" onpaste="return false" ondrop="return false" MaxLength="50"></asp:TextBox>
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
                            <div class="col-md-4 col-xs-12 col-sm-4">
                                <div class="ibox">
                                    <div class="ibox-title">
                                        <h5><strong>Endereço do Assinante</strong></h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="table-responsive">
                                            <asp:MultiView ID="mvTabControlEndereco" runat="server" ActiveViewIndex="0">
                                                <asp:View ID="tabCepEnderecoAssinante" runat="server">
                                                    <table class="table shoping-cart-table">
                                                        <tbody>
                                                            <tr>
                                                                <td class="desc">
                                                                    <div class="form-group">
                                                                        <div class="col-lg-12">
                                                                            <h5>
                                                                                <asp:Label runat="server" ID="lbCEP" Text="CEP"></asp:Label></h5>
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon" style=""><i class="fas fa-map-marker-alt"></i></span>
                                                                                <cc1:MEdit ID="meCEP" runat="server" placeholder="xxxxx-xxx" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="CEP" AutoPostBack="true" OnTextChanged="tbCEP_TextChanged" Mascara="CEP"></cc1:MEdit>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="tabEnderecoAssinante" runat="server">
                                                    <table class="table shoping-cart-table">
                                                        <tbody>
                                                            <tr>
                                                                <td class="desc">
                                                                    <div class="form-group">
                                                                        <div class="col-lg-12">
                                                                            <i class="fas fa-map-marker-alt"></i>&nbsp;<strong><asp:Label runat="server" ID="lbCEPInformado" Text="CEP Informado"></asp:Label></strong>
                                                                        </div>
                                                                        <div class=" pull-right">
                                                                            <asp:LinkButton ID="lbCorrigirCEPInformado" runat="server" CssClass="text-muted small" OnClick="lbCorrigirCEPInformado_Click"><i class="fas fa-reply"></i>&nbsp;Corrigir</asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-lg-12">
                                                                            <h5>
                                                                                <asp:Label runat="server" ID="lbLogradouro" Text="Logradouro"></asp:Label></h5>
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon" style=""><i class="fas fa-map-signs"></i></span>
                                                                                <asp:TextBox ID="tbLogradouro" runat="server" placeholder="Logradouro" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Logradouro" Enabled="False" MaxLength="300"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <h5>
                                                                                <asp:Label runat="server" ID="lbNumeroLogradouro" Text="Número"></asp:Label></h5>
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon" style=""><i class="fas fa-map-marker"></i></span>
                                                                                <asp:TextBox ID="tbNumeroLogradouro" runat="server" placeholder="Número" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Número do Logradouro" MaxLength="10"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <h5>
                                                                                <asp:Label runat="server" ID="lbComplementoLogradouro" Text="Complemento"></asp:Label></h5>
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon" style=""><i class="fas fa-map-marker"></i></span>
                                                                                <asp:TextBox ID="tbComplementoLogradouro" runat="server" placeholder="Complemento" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Complemento do Logradouro" MaxLength="100"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-12">
                                                                            <h5>
                                                                                <asp:Label runat="server" ID="lbBairro" Text="Bairro"></asp:Label></h5>
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon" style=""><i class="fas fa-map-pin"></i></span>
                                                                                <asp:TextBox ID="tbBairro" runat="server" placeholder="Bairro" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Bairro" Enabled="False" MaxLength="200"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-lg-12">
                                                                            <h5>
                                                                                <asp:Label runat="server" ID="lbCidade" Text="Cidade"></asp:Label></h5>
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon" style=""><i class="fas fa-map-marked-alt"></i></span>
                                                                                <asp:TextBox ID="tbCidade" runat="server" placeholder="Cidade" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Cidade" Enabled="False" MaxLength="200"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-12">
                                                                            <h5>
                                                                                <asp:Label runat="server" ID="lbEstado" Text="Estado/Província"></asp:Label></h5>
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon" style=""><i class="fas fa-map"></i></span>
                                                                                <asp:TextBox ID="tbEstado" runat="server" placeholder="Estado" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Estado" Enabled="False" MaxLength="10"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group" id="divPaisAssinante" runat="server" visible="true">
                                                                            <div class="col-lg-12">
                                                                                <h5>
                                                                                    <asp:Label runat="server" ID="lpPais" Text="País"></asp:Label></h5>
                                                                                <div class="input-group">
                                                                                    <span class="input-group-addon" style=""><i class="fas fa-flag"></i></span>
                                                                                    <asp:DropDownList ID="ddlPaisAssinante" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="top" title="País"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </asp:View>
                                            </asp:MultiView>
                                            <!-- /MultiView tabs -->
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox">
                                    <div class="ibox-title">
                                        <h5><strong>Telefone do Assinante</strong></h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="table-responsive">
                                            <table class="table shoping-cart-table">
                                                <tbody>
                                                    <tr>
                                                        <td class="desc">

                                                            <div class="radio radio-muted radio-inline">
                                                                <asp:RadioButtonList ID="rblTpFone" runat="server" RepeatColumns="2" AutoPostBack="True" OnSelectedIndexChanged="rblTpFone_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1">&nbsp;Fixo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                    <asp:ListItem Value="2">&nbsp;Celular</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="desc">
                                                            <div class="form-group">

                                                                <div class="input-group" runat="server" id="div1">
                                                                    <span class="input-group-addon" style=""><i class="fa fa-phone-square fa-fw"></i></span>
                                                                    <cc1:MEdit ID="tbTelefoneAssinante" runat="server" Mascara="Celular" placeholder="(xx) xxxxx-xxxx" CssClass="form-control" xata-toggle="tooltip" data-placement="top" title="Telefone do Assinante"></cc1:MEdit>
                                                                </div>

                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-xs-12 col-sm-4">
                                <div class="ibox" id="cardCartCred" runat="server">
                                    <div class="ibox-title">
                                        <h5><strong>Dados do Cartão de Crédito</strong></h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="table-responsive">
                                            <table class="table shoping-cart-table">
                                                <tbody>
                                                    <tr>
                                                        <td class="desc">
                                                            <div class="form-group">
                                                                <div class="col-lg-12">
                                                                    <h5>Número</h5>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><i class="far fa-credit-card"></i></span>
                                                                        <cc1:MEdit ID="meNumeroCartaoCredito" runat="server" placeholder="0000 0000 0000 0000" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Número do Cartão" Mascara="Inteiro"></cc1:MEdit>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <h5>Código Segurança</h5>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><i class="fas fa-credit-card"></i></span>
                                                                        <cc1:MEdit ID="meCodigoSeguranca" runat="server" placeholder="0000" min="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Código de Segurança" MaxLength="4" Mascara="Inteiro"></cc1:MEdit>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <h5>Vencto. <small>(Mês/Ano)</small></h5>
                                                                    <div class="input-append date input-group" id="datepicker" data-date-format="mmyy">
                                                                        <span class="input-group-addon"><i class="fas fa-calendar"></i></span>
                                                                        <asp:TextBox ID="tbMesAnoValidadeCartao" runat="server" name="date" placeholder="mmaa" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" TextMode="SingleLine" title="Mês e Ano de Validade do Cartão" MaxLength="4"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-12">
                                                                    <h5>Nome que consta no Cartão</h5>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><i class="fas fa-user fa-fw"></i></span>
                                                                        <asp:TextBox ID="tbNomeDoCartao" runat="server" placeholder="Nome que consta no Cartão" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Nome que consta no Cartão" MaxLength="300"></asp:TextBox>
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
                                <div class="ibox">
                                    <div class="ibox-title">
                                        <h4><strong>Sua Assinatura   
                                                <asp:Label ID="lblValorAssinatura" runat="server" class="pull-right" Text="Valor Total"></asp:Label></strong></h4>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="table-responsive">
                                            <asp:HiddenField ID="hfID" runat="server" />
                                            <table class="table shoping-cart-table">
                                                <tbody>
                                                    <asp:Repeater ID="rptPlanos" runat="server" OnItemDataBound="rptPlanos_ItemDataBound" OnItemCommand="rptPlanos_ItemCommand">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="desc">
                                                                    <h4 class="text-info" id="hPlanoSelecionado" runat="server">
                                                                        <i class="fas fa-paper-plane" runat="server" id="iconeDoPlano"></i>&nbsp;
                                                                            <asp:Label ID="lblTextoPlanoSelecionado" runat="server" Text=""></asp:Label>
                                                                    </h4>
                                                                    <h6>
                                                                        <asp:Label ID="lblDescrB" runat="server" Text="baloo"></asp:Label>
                                                                    </h6>
                                                                </td>
                                                                <td class="pull-right" style="background: #fff">
                                                                    <h4>
                                                                        <asp:Label ID="lblValorPlanoSelecionado" runat="server" Text="R$ Valor"></asp:Label>
                                                                    </h4>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <div runat="server" visible="false">
                                                        <asp:Repeater ID="rptModulos" runat="server" OnItemDataBound="rptModulos_ItemDataBound" OnItemCommand="rptModulos_ItemCommand">
                                                            <HeaderTemplate>
                                                                <tr>
                                                                    <td class="desc">
                                                                        <h4 class="text-warning">
                                                                            <i class="fas fa-mortar-pestle"></i>&nbsp;
                                                                                    <asp:Label ID="lblModuloAdicional" runat="server" Text="Módulo&nbsp;Adicional"></asp:Label>
                                                                        </h4>
                                                                    </td>
                                                                    <td class="pull-right" style="background: #fff">
                                                                        <h4>
                                                                            <asp:Label ID="lblValorModuloAdicional" runat="server" Text="R$ Valor"></asp:Label>
                                                                        </h4>
                                                                    </td>
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="desc">
                                                                        <asp:Label ID="lblTextoModuloAdicional" class="small" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="pull-right" style="background: #fff">
                                                                        <asp:LinkButton ID="lbRemoverModuloAdicional" runat="server" CssClass="text-muted small" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdPlano") %>'><i class="fa fa-trash"></i>&nbsp;Remover</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </tbody>
                                            </table>
                                            <div class="middle-box text-center loginscreen fadeInDown">
                                                <asp:LinkButton runat="server" ID="lbVoltar" CssClass="btn btn-sm BLOCK btn-default m-t-n-xs" OnClick="lbVoltar_Click"><i class="fas fa-backspace"></i>&nbsp;Voltar</asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbConcluirAssinatura" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" OnClick="lbAvancarFinalizarAssinatura_Click">Avançar&nbsp;<i class="fas fa-hand-point-right"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <hr />
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

                                <div class="form-group">
                                    <asp:Label ID="lblMsgModal" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div id="rptClientes">
                                    <asp:Repeater ID="rptAssinantes" runat="server">
                                        <HeaderTemplate>
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <span>Assinante(s)</span>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <span class="badge badge-success">
                                                        <asp:Label ID="lblAssiantes" runat="server" Text='<%# Eval("Cliente") %>'></asp:Label>
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
                                <div class="form-group">
                                    <asp:Label ID="lblFinal" runat="server" Text="Label"></asp:Label>
                                </div>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>
