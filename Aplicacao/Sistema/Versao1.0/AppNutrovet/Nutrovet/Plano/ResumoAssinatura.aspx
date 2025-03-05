<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="ResumoAssinatura.aspx.cs" Inherits="Nutrovet.Plano.ResumoAssinatura" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {
                $("#datepicker").datepicker({
                    format: "mm/yyyy",
                    clearBtn: true,
                    language: "pt-BR",
                    daysOfWeekHighlighted: "0,6",
                    autoclose: true,
                    todayHighlight: true,
                    viewMode: "months",
                    minViewMode: "months",
                    startDate: new Date(),
                    autoclose: true
                }).datepicker("setDate", new Date());

                $('#tbDataNascimentoAssinante').datepicker({
                    format: "dd/mm/yyyy",
                    clearBtn: true,
                    language: "pt-BR",
                    daysOfWeekHighlighted: "0,6",
                    autoclose: true,
                    todayHighlight: true
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

    <section id="pricing" style="background: #F0F0F0;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(BindEvents);
                </script>
                
                <hr />
                <div class="container">
                    <div id="page-wrapper" class="gray-bg">
                        <div class="wrapper wrapper-content fadeInRight">
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
                                            <div class="table-responsive">
                                                <table class="table shoping-cart-table">
                                                    <tbody>
                                                        <tr>
                                                            <td class="desc">
                                                                <div class="col-sm-12">
                                                                    <div class="radio radio-info radio-inline">
                                                                        <h5>
                                                                            <asp:RadioButton ID="rbPessoaFisica" runat="server" CssClass="alert" Text="Pessoa Física" AutoPostBack="True" GroupName="grRB" OnCheckedChanged="rbPessoaFisica_CheckedChanged" Checked="true" /></h5>
                                                                        <h5>
                                                                            <asp:RadioButton ID="rbPessoaJuridica" runat="server" CssClass="alert" Text="Pessoa Jurídica" AutoPostBack="True" GroupName="grRB" OnCheckedChanged="rbPessoaJuridica_CheckedChanged" /></h5>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-lg-12">
                                                                        <h5>E-mail</h5>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon" style=""><i class="fa fa-at fa-fw"></i></span>
                                                                            <asp:TextBox ID="tbEmailAssinante" runat="server" placeholder="E-mail" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="Email" title="E-mail"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-lg-12">
                                                                        <h5>Senha</h5>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon" style=""><i class="fas fa-key fa-fw"></i></span>
                                                                            <asp:TextBox ID="tbSenhaAssinante" runat="server" placeholder="Senha" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="Password" title="Senha"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-lg-12">
                                                                        <h5>Confirmar</h5>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon" style=""><i class="fas fa-key fa-fw"></i></span>
                                                                            <asp:TextBox ID="tbConfSenhaAssinante" runat="server" placeholder="Confirmação da Senha" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="Password" title="Confirmação da Senha"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-lg-12">
                                                                        <h5>
                                                                            <asp:Label runat="server" ID="lbTituloTipoPessoa" Text="CPF"></asp:Label></h5>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon" style=""><i class="fas fa-id-card"></i></span>
                                                                            <asp:TextBox ID="tbCPFAssinante" runat="server" placeholder="CPF" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="CPF do Assinante"></asp:TextBox>
                                                                            <asp:TextBox ID="tbCNPJAssinante" runat="server" placeholder="CNPJ" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="CNPJ do Assinante" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-lg-12">
                                                                        <h5>
                                                                            <asp:Label runat="server" ID="lbNomeAssinante" Text="Nome"></asp:Label></h5>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon" style=""><i class="fas fa-user fa-fw"></i></span>
                                                                            <asp:TextBox ID="tbNomeAssinante" runat="server" placeholder="Nome Assinante" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Nome Assinante"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divSobrenomeAssinante" runat="server">
                                                                    <div class="col-lg-12">
                                                                        <h5>Sobrenome</h5>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon" style=""><i class="fas fa-user fa-fw"></i></span>
                                                                            <asp:TextBox ID="tbSobrenomeAssinante" runat="server" placeholder="Sobrenome Assinante" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Sobrenome Assinante"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divDataNascimento" runat="server">
                                                                    <div class="col-lg-12">
                                                                        <h5>Data de Nascimento</h5>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon" style=""><i class="fas fa-birthday-cake"></i></span>
                                                                            <cc1:MEdit ID="tbDataNascimentoAssinante" name="tbDataNascimentoAssinante" ClientIDMode="Static" runat="server" Mascara="Data" placeholder="dd/mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Data de Nascimento"></cc1:MEdit>
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
                                </div>
                                <div class="col-md-4 col-xs-12 col-sm-4">
                                    <div class="ibox">
                                        <div class="ibox-title">
                                            <h5><strong>Forma de Pagamento</strong></h5>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="table-responsive">
                                                <table class="table shoping-cart-table">
                                                    <tbody>
                                                        <tr>
                                                            <td class="desc">
                                                                <div class="form-group">
                                                                    <div class="col-lg-12">
                                                                        <h5>Número do Cartão</h5>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon" style=""><i class="fas fa-credit-card"></i></span>
                                                                            <asp:TextBox ID="tbNumeroCartaoCredito" runat="server" placeholder="0000 0000 0000 0000" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Número do Cartão"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <h5>Códdigo Segurança</h5>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon" style=""><i class="far fa-credit-card"></i></span>
                                                                            <cc1:MEdit ID="tbCodigoSeguranca" runat="server" placeholder="0000" min="0" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Código de Segurança" MaxLength="4" Mascara="Inteiro"></cc1:MEdit>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12">
                                                                        <h5>Vencimento do Cartão <small>(Mês/Ano)</small></h5>
                                                                        <div class="input-append date input-group" id="datepicker" data-date="02-2012" data-date-format="mm-yyyy">
                                                                            <span class="input-group-addon" style=""><i class="fas fa-calendar-week"></i></span>
                                                                            <asp:TextBox ID="tbMesAnoValidadeCartao" runat="server" name="date" placeholder="mm/aaaa" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" TextMode="SingleLine" title="Mês e Ano de Validade do Cartão" MaxLength="7"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <h5>Nome que consta no Cartão</h5>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon" style=""><i class="fas fa-user fa-fw"></i></span>
                                                                            <asp:TextBox ID="tbNomeDoCartao" runat="server" placeholder="Nome que consta no Cartão" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Nome que consta no Cartão"></asp:TextBox>
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
                                </div>
                                <div class="col-md-4 col-xs-12 col-sm-4">
                                    <div class="ibox">
                                        <div class="ibox-title">
                                            <h5><strong>Sua Assinatura</strong></h5>
                                        </div>
                                        <div class="">
                                            <div class="ibox">
                                                <div class="ibox-content">
                                                    <div class="table-responsive">
                                                        <asp:HiddenField ID="hfID" runat="server" />
                                                        <asp:Repeater ID="rptAssinatura" runat="server">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                        <table class="table shoping-cart-table">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="desc">
                                                                        <h4 class="text-info">
                                                                            <i class="fas fa-paper-plane"></i>&nbsp;<asp:Label ID="lblPlanoSelecionado" runat="server" Text="Plano Selecionado"></asp:Label>
                                                                        </h4>
                                                                        <asp:Label ID="lblTextoPlanoSelecionado" class="small" runat="server" Text="Texto para o plano selecionado"></asp:Label>
                                                                    </td>
                                                                    <td class="pull-right" style="background: #fff">
                                                                        <h4>
                                                                            <asp:Label ID="lblValorPlanoSelecionado" runat="server" Text="R$ Valor"></asp:Label></h4>
                                                                        <asp:HyperLink ID="hlkRemoverPlanoSelecionado" NavigateUrl="#" class="text-muted small" runat="server"><i class="fa fa-trash"></i>&nbsp;Remover</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="desc">
                                                                        <h4 class="text-warning">
                                                                            <i class="fas fa-mortar-pestle"></i>&nbsp;
                                                                        <asp:Label ID="lblModuloAdicional" runat="server" Text="Módulo Adicional"></asp:Label>
                                                                        </h4>
                                                                        <asp:Label ID="lblTextoModuloAdicional" class="small" runat="server" Text="Texto para o módulo adicional"></asp:Label>
                                                                    </td>
                                                                    <td class="pull-right" style="background: #fff">
                                                                        <h4>
                                                                            <asp:Label ID="lblValorModuloAdicional" runat="server" Text="R$ Valor"></asp:Label></h4>
                                                                        <asp:HyperLink ID="hlkRemoverModuloAdicional" NavigateUrl="#" class="text-muted small" runat="server"><i class="fa fa-trash"></i>&nbsp;Remover</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                        </div>
                                        <div class="ibox-content">
                                            <div class="ibox">
                                                <div class="ibox-content">
                                                    <h3 class="font-bold">
                                                        <asp:Label ID="lblValorAssinatura" runat="server" Text="R$ Valor Total"></asp:Label>
                                                    </h3>
                                                    <hr />
                                                    <div class="middle-box text-center loginscreen animated fadeInDown">
                                                        <div>
                                                            <div class="form-group">
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
                            </div>
                        </div>
                    </div>
                    <hr />
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>
