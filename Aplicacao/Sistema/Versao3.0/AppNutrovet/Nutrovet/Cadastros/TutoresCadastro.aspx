<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="TutoresCadastro.aspx.cs" Inherits="Nutrovet.Cadastros.TutoresCadastro"
    ValidateRequest="false" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .form-control {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        function BindEvents() {
            $('#<%= txbCpfCnpj.ClientID %>').on('input', function () {
                this.value = this.value.replace(/[^0-9\.\-\/]/g, '');
                let tipoPessoa = $('#ddlTipoPessoaAssinante').val();
                let cpfCnpj = $(this).val().replace(/\D/g, '');

                if (tipoPessoa === 'Fisica' && cpfCnpj.length <= 11) {
                    $(this).val(cpfCnpj.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4"));
                } else if (tipoPessoa === 'Juridica' && cpfCnpj.length <= 14) {
                    $(this).val(cpfCnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5"));
                }
            });

            $('#<%= txbCpfCnpj.ClientID %>').on('change', function () {
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
                        <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                    </li>
                    <li class="active">
                        <asp:HyperLink ID="hlTutoresSelecao" NavigateUrl="~/Cadastros/TutoresSelecao.aspx" runat="server"><i class="fas fa-user"></i> Tutores</asp:HyperLink>
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblTitulo" runat="server" Text=""><i class="fas fa-plus-square"></i> </asp:Label>
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
                            <asp:Label ID="lblSubTitulo" runat="server" Text="Insira ou altere os dados do Tutor"></asp:Label></strong>
                        </h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="form-group">
                                <label id="lblPfPj" class="col-lg-2 control-label">Tipo de Entidade</label>
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <span class="input-group-addon" style=""><i class="fas fa-user-cog"></i></span>
                                        <asp:RadioButtonList ID="rblPfPj" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblPfPj_SelectedIndexChanged" RepeatColumns="2" Width="100%">
                                            <asp:ListItem Selected="True" Value="1">&nbsp;&nbsp;Pessoa Física&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;&nbsp;Pessoa Jurídica</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label ID="lblNomeTutor" runat="server" CssClass="col-lg-2 control-label" Text="Nome Completo"></asp:Label>
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <span class="input-group-addon" style=""><i class="fas fa-user fa-fw"></i></span>
                                        <asp:TextBox ID="tbNomeTutor" runat="server" placeholder="Nome Completo" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Nome Completo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        <div class="row">
                            <div class="form-group">
                                <label id="lblEmail" class="col-lg-2 control-label">E-mail</label>
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <span class="input-group-addon" style=""><i class="fa fa-at fa-fw"></i></span>
                                        <asp:TextBox ID="tbEmailTutor" runat="server" placeholder="E-mail" CssClass="form-control" data-toggle="tooltip" data-placement="top" TextMode="Email" title="E-mail"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label ID="lblCpfCnpj" runat="server" Text="CPF" CssClass="col-lg-2 control-label"></asp:Label>
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <span class="input-group-addon" style=""><i class="fas fa-id-card"></i></span>
                                        <%--<cc1:MEdit ID="meCpfCnpj" runat="server" placeholder="CPF" CssClass="form-control" title="E-mail" Mascara="CPF"></cc1:MEdit>--%>
                                        <asp:TextBox ID="txbCpfCnpj" runat="server" placeholder="E-mail" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="CPF"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        <div class="row">
                            <div class="form-group">
                                <label id="lblTelefone" class="col-lg-2 control-label">Telefone</label>
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <span class="input-group-addon" style=""><i class="fa fa-phone-square fa-fw"></i></span>
                                        <cc1:MEdit ID="tbTelefoneTutor" runat="server" Mascara="Telefone" placeholder="(xx) xxxx-xxxx" CssClass="form-control" title="Telefone"></cc1:MEdit>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        <div class="row">
                            <div class="form-group">
                                <label id="lblCelular" class="col-lg-2 control-label">Celular</label>
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <span class="input-group-addon" style=""><i class="fas fa-mobile-alt fa-fw"></i></span>
                                        <cc1:MEdit ID="tbCelularTutor" runat="server" Mascara="Celular" placeholder="(xx) xxxx-xxxx" CssClass="form-control" title="Celular"></cc1:MEdit>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfID" runat="server" />
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                    </div>

                    <div class="modal-footer col-sm-8 col-lg-8">
                        <div class="btn-group" role="group">
                            <asp:LinkButton runat="server" ID="lbFEchar" CssClass="btn btn-default" OnClick="lbFechar_Click"><i class='fas fa-door-open' aria-hidden="true"></i>&nbsp;Fechar </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbSalvar" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvar_Click"><i class='far fa-save' aria-hidden="true"></i>&nbsp;Salvar</asp:LinkButton>
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
