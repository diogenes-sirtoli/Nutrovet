<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="RecNutraceuticosUpdt.aspx.cs" Inherits="Nutrovet.Receituario.RecNutraceuticosUpdt"
    ValidateRequest="false" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

    <script>
        function BindEvents() {
            $(document).ready(function () {

            });
        }

        function atualizaTotal(txtDose) {
            var celulaDose = txtDose.parentNode;
            var label = document.getElementById('<%= lblPeso.ClientID %>');
            var celulaPesoPet = label.textContent.replace(',', '.');
            var celulaQuantidade = celulaDose.nextSibling.nextSibling.nextSibling.nextSibling;
            var unidadeDoseMaxima = celulaDose.previousSibling.previousSibling.innerText;
            var unidadeDoseMinima = celulaDose.previousSibling.previousSibling.previousSibling.previousSibling.innerText;
            var componenteMeQuant = celulaQuantidade.querySelector('#meQuant');
            var componenteMeDose = celulaDose.querySelector('#meDose');
            var dose = parseFloat(txtDose.value.replace(',', '.')).toFixed(2);
            var quantidade = null;

            if (!isNaN(dose)) {
                //alert(dose);
                //alert(celulaPesoPet);

                if (dose > 0) {

                    quantidade = dose * 1;
                    if (!unidadeDoseMinima.includes("mg/animal") && !unidadeDoseMaxima.includes("mg/animal")) {
                        quantidade = dose * celulaPesoPet /*pesoPet*/;
                    }
                    componenteMeQuant.value = quantidade.toFixed(2).replace('.', ',');
                }
                else {
                    toastr.error("Campo Dose Deve Possuir um Valor Maior que Zero!!!", "Erro");
                    componenteMeQuant.value = "";
                    componenteMeDose.value = "";
                }
            }
            else {
                componenteMeQuant.value = "";
                componenteMeDose.value = "";
            }
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

    <div class="">
        <div class="page-title">
            <div class="clearfix"></div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="row wrapper border-bottom white-bg page-heading">
                        <div class="col-lg-1">
                            <asp:HyperLink ID="HyperLink12" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
                        </div>
                        <h2>&nbsp;Receituário</h2>
                        <div class="col-lg-4">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlMenuGeral" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <asp:HyperLink ID="hlCardapio" NavigateUrl="~/Receituario/ReceituarioSelecao.aspx" runat="server"><i class="fas fa-book-medical fa-lg"></i> Receituário</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <strong>
                                        <i class="fas fa-capsules fa-lg" aria-hidden="true"></i>
                                        <asp:Label ID="Label1" runat="server" Text=" Nutracêuticos"></asp:Label>
                                    </strong>
                                </li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(BindEvents);
            </script>

            <div class="row wrapper border-bottom white-bg page-heading">
                <div>
                    <div class="ibox-title">
                        <h5>
                            <asp:Label ID="lblSubTitulo" runat="server" Text="Receituário de Nutracêuticos"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12">
                    <div class="wrapper wrapper-content fadeInRight">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-sm-4" style="justify-content: center; flex-direction: column; align-items: flex-start;">
                                                <asp:Image ID="imgLogo" ImageUrl="~/Perfil/Logotipos/logo_receita.png" alt="logotipo" runat="server" Style="width: 40%;" />
                                            </div>
                                            <div class="col-sm-8" id="divCabecalhoGrande" name="divCabecalhoGrande" runat="server" visible="true">
                                                <h3>
                                                    <asp:Label ID="lblNomeClin" runat="server" class="col-lg-12 control-label text-right" Text="Nome do Profissional ou Clínica"></asp:Label>
                                                </h3>

                                                <h5>
                                                    <asp:Label ID="lblSlogan" runat="server" class="col-lg-12 control-label text-right" Text="Slogan, site ou rede social"></asp:Label>
                                                </h5>
                                                <h5>
                                                    <asp:Label ID="lblEndereco" runat="server" class="col-lg-12 control-label text-right" Text="Endereço"></asp:Label>
                                                </h5>
                                                <h5>
                                                    <asp:Label ID="lblEMail" runat="server" class="col-lg-12 control-label text-right" Text="E-Mail"></asp:Label>
                                                </h5>
                                                <h5>
                                                    <asp:Label ID="lblTelefone" runat="server" class="col-lg-12 control-label text-right" Text="Telefone"></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="col-sm-8" id="divCabecalhoSlim" name="divCabecalhoSlim" runat="server" visible="false">
                                                <asp:Label ID="lblCabecalhoSlim" runat="server" class="col-lg-12 control-label text-right" Text="Cabecalho"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-7">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h4>DADOS DO PACIENTE</h4>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-sm-8 b-r">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbNomePacienteReceita" runat="server" class="control-label text-left" Text="Paciente:"></asp:Label>
                                                    <strong><asp:Label ID="lblPaciente" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbPesoPacienteReceita" runat="server" class="control-label text-left" Text="Peso:"></asp:Label>
                                                    <strong><asp:Label ID="lblPeso" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                            <div class="col-sm-8 b-r">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbEspeciePacienteReceita" runat="server" class="control-label text-left" Text="Espécie:"></asp:Label>
                                                    <strong><asp:Label ID="lblEspecie" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbSexoPacienteReceita" runat="server" class="control-label text-left" Text="Sexo:"></asp:Label>
                                                    <strong><asp:Label ID="lblSexo" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                            <div class="col-sm-8 b-r">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbRacaPacienteReceita" runat="server" class="control-label text-left" Text="Raça:"></asp:Label>
                                                    <strong><asp:Label ID="lblRaca" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lbIdadePacienteReceita" runat="server" class="control-label text-left" Text="Idade:"></asp:Label>
                                                    <strong><asp:Label ID="lblIdade" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h4>DADOS DO TUTOR</h4>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbNomeTutorReceita" runat="server" class="control-label text-left" Text="Tutor:"></asp:Label>
                                                <strong><asp:Label ID="lblTutor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbEMailTutorReceita" runat="server" class="control-label text-left" Text="E-Mail:"></asp:Label>
                                                <strong><asp:Label ID="lblEMailTutor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbTelefoneTutorReceita" runat="server" class="control-label text-left" Text="Telefone:"></asp:Label>
                                                <strong><asp:Label ID="lblFoneTutor" runat="server" class="control-label text-left" Text="Label"></asp:Label></strong>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h5>Uso</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="tbxUso" ClientIDMode="Static" runat="server" placeholder="Descriçao de Uso da Receita" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Descriçao de Uso da Receita" MaxLength="300"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h5>Instruções da Receita</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="tbxInstrRec" ClientIDMode="Static" runat="server" placeholder="Instruções da Receita" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Instruções da Receita" MaxLength="500"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-title">
                                        <h5>Prescrição</h5>
                                    </div>
                                    <div id="gridRepeater" class="ibox-content" style="height: 300px; overflow: auto;" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="x_content table-responsive" style="overflow-x: auto; white-space: nowrap;">
                                                    <!-- start project list -->
                                                    <table class="table table-striped table-hover">
                                                        <asp:Repeater ID="rptReceitaNutrac" runat="server" OnItemDataBound="rptReceitaNutrac_ItemDataBound" OnItemCommand="rptReceitaNutrac_ItemCommand">
                                                            <HeaderTemplate>
                                                                <thead>
                                                                    <tr>
                                                                        <th style="width: 5%" class="text-center">
                                                                            <asp:Label ID="lblIncldiet" runat="server" Text="Incluir"></asp:Label>
                                                                        </th>
                                                                        <th style="width: 20%" class="text-center">
                                                                            <asp:Label ID="lblNutr" runat="server" Text="Nutriente"></asp:Label>
                                                                        </th>
                                                                        <th style="width: 10%" class="text-center">
                                                                            <asp:Label ID="lblDoseMin" runat="server" Text="Dose Mín."></asp:Label>
                                                                        </th>
                                                                        <th style="width: 10%" class="text-center">
                                                                            <asp:Label ID="lblDoseMax" runat="server" Text="Dose Máx."></asp:Label>
                                                                        </th>
                                                                        <th style="width: 10%" class="text-center">
                                                                            <asp:Label ID="lblDose" runat="server" Text="Dose/Kg"></asp:Label>
                                                                        </th>
                                                                        <th style="width: 10%" class="text-center">
                                                                            <asp:Label ID="lblDoseUnid" runat="server" Text="Unidade"></asp:Label>
                                                                        </th>
                                                                        <th style="width: 10%" class="text-center">
                                                                            <asp:Label ID="lblQuant" runat="server" Text="Quantidade"></asp:Label>
                                                                        </th>
                                                                        <th style="width: 10%" class="text-center">
                                                                            <asp:Label ID="lblIntervalo" runat="server" Text="Intervalo"></asp:Label>
                                                                        </th>
                                                                        <th style="width: 5%">Ação
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-center">
                                                                        <asp:HiddenField ID="hfIdIncluir" runat="server" Value='<%# Eval("IdNutr") %>' />
                                                                        <asp:HiddenField ID="hfIdNutrRec" runat="server" Value='<%# Eval("IdNutrRec") %>' />
                                                                        <asp:HiddenField ID="hfIdUnid" runat="server" Value='<%# Eval("IdUnid") %>' />
                                                                        <asp:HiddenField ID="hfIdUnidMin" runat="server" Value='<%# Eval("IdUnidMin") %>' />
                                                                        <asp:HiddenField ID="hfDoseMin" runat="server" Value='<%# Eval("DoseMin") %>' />
                                                                        <asp:HiddenField ID="hfIdUnidMax" runat="server" Value='<%# Eval("IdUnidMax") %>' />
                                                                        <asp:HiddenField ID="hfDoseMax" runat="server" Value='<%# Eval("DoseMax") %>' />
                                                                        <asp:CheckBox ID="cbxIncldiet" runat="server" CssClass="custom-control custom-checkbox" OnCheckedChanged="cbxIncldiet_CheckedChanged" AutoPostBack="True" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblNutrReceita" runat="server" Text='<%# Eval("Nutriente") %>'></asp:Label>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="lblDoseMinReceita" runat="server" Text='<%# Eval("UnidadeMin") %>'></asp:Label>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="lblDoseMaxReceita" runat="server" Text='<%# Eval("UnidadeMax") %>'></asp:Label>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="lblDoseReceita" runat="server" Text='<%# Eval("Dose", "{0:C}") %>'></asp:Label>
                                                                        <cc1:MEdit ID="meDose" ClientIDMode="Static" runat="server" Text='<%# Eval("Dose") %>' placeholder="Dose" CssClass="form-control" onblur="atualizaTotal(this);" data-toggle="tooltip" data-placement="top" title="Dose" Mascara="Float" Visible="False"></cc1:MEdit>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="lblDoseUnidReceita" runat="server" Text='<%# Eval("Unidade") %>'></asp:Label>
                                                                        <asp:DropDownList ID="ddlUnidade" CssClass="form-control" runat="server" Visible="False"></asp:DropDownList>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <cc1:MEdit ID="meQuant" runat="server" ClientIDMode="Static" Text='<%# Eval("Quantidade") %>' placeholder="Quantidade" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Quantidade" Mascara="Float" Visible="False" Enabled="False"></cc1:MEdit>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <asp:Label ID="lblIntervaloReceita" runat="server" Text='<%# Eval("Prescricao") %>'></asp:Label>
                                                                        <asp:DropDownList ID="ddlIntervalo" CssClass="form-control" runat="server" Visible="False"></asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lbSalvar" runat="server" CssClass="btn btn-primary-nutrovet" data-toggle="tooltip" data-placement="top" title="Salvar o item" CommandName="salvarItem" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdNutrRec") %>'> <i class="fas fa-save"></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                    <!-- end project list -->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content col-lg-12">
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="panel panel-default">
                                        <div id="Tabs" role="tabpanel" class="">
                                            <!-- Nav tabs -->
                                            <ul class="nav nav-tabs" role="tablist">
                                                <li id="liTidCard" runat="server" role="presentation" class="active">
                                                    <asp:LinkButton ID="aTidCard" class="text-center" runat="server" OnClick="aTidCard_Click"><i class="fas fa-hourglass"></i>&nbsp;TID</asp:LinkButton>
                                                    <%-- 8/8 horas ou três vezes ao dia --%>
                                                </li>
                                                <li id="liBidCard" runat="server" role="presentation">
                                                    <asp:LinkButton ID="aBidCard" class="text-center" runat="server" OnClick="aBidCard_Click"><i class="fas fa-clock"></i>&nbsp;BID</asp:LinkButton>
                                                    <%-- 12/12 horas ou duas vezes ao dia --%>
                                                </li>
                                                <li id="liSidCard" runat="server" role="presentation">
                                                    <asp:LinkButton ID="aSidCard" class="text-center" runat="server" OnClick="aSidCard_Click"><i class="fas fa-calendar-alt"></i>&nbsp;SID</asp:LinkButton>
                                                    <%-- 24 horas ou uma vez ao dia --%>
                                                </li>
                                            </ul>
                                            <!-- /Nav tabs -->
                                            <!-- MultiView tabs -->
                                            <div class="tab-content">
                                                <asp:MultiView ID="mvTabControl" runat="server" ActiveViewIndex="0">
                                                    <asp:View ID="tabTid" runat="server">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="ibox ">
                                                                        <div class="ibox-content">
                                                                            <div class="row">
                                                                                <div class="col-sm-8">
                                                                                    <h5>
                                                                                        <asp:Label ID="Label4" runat="server" class="col-lg-12 control-label text-left" Text="Veículo"></asp:Label>
                                                                                    </h5>
                                                                                    <asp:TextBox ID="tbVeiculoTID" ClientIDMode="Static" runat="server" placeholder="Veículo Excipiente" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Veículo Excipiente" MaxLength="100"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <h5>
                                                                                        <asp:Label ID="Label5" runat="server" class="col-lg-12 control-label text-left" Text="Quantidade"></asp:Label>
                                                                                    </h5>
                                                                                    <asp:TextBox ID="tbQuantTID" runat="server" placeholder="Quantidade do Veículo" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Quantidade"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="ibox ">
                                                                        <div class="ibox-content">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <h5>
                                                                                        <asp:Label ID="Label6" runat="server" class="col-lg-12 control-label text-left" Text="Posologia"></asp:Label>
                                                                                    </h5>
                                                                                    <asp:TextBox ID="tbPosolTID" runat="server" placeholder="Posologia" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Posologia" MaxLength="200"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:View>
                                                    <asp:View ID="tabBid" runat="server">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="ibox ">
                                                                        <div class="ibox-content">
                                                                            <div class="row">
                                                                                <div class="col-sm-8">
                                                                                    <h5>
                                                                                        <asp:Label ID="lbVeiculoReceita" runat="server" class="col-lg-12 control-label text-left" Text="Veículo"></asp:Label>
                                                                                    </h5>
                                                                                    <asp:TextBox ID="tbVeiculoBID" ClientIDMode="Static" runat="server" placeholder="Veículo Excipiente" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Veículo Excipiente" MaxLength="100"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <h5>
                                                                                        <asp:Label ID="lbQuantidadeVeiculoReceita" runat="server" class="col-lg-12 control-label text-left" Text="Quantidade"></asp:Label>
                                                                                    </h5>
                                                                                    <asp:TextBox ID="tbQuantBID" runat="server" placeholder="Quantidade do Veículo" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Quantidade"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="ibox ">
                                                                        <div class="ibox-content">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <h5>
                                                                                        <asp:Label ID="Label2" runat="server" class="col-lg-12 control-label text-left" Text="Posologia"></asp:Label>
                                                                                    </h5>
                                                                                    <asp:TextBox ID="tbPosolBID" runat="server" placeholder="Posologia" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Posologia" MaxLength="200"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:View>
                                                    <asp:View ID="tabSid" runat="server">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="ibox ">
                                                                        <div class="ibox-content">
                                                                            <div class="row">
                                                                                <div class="col-sm-8">
                                                                                    <h5>
                                                                                        <asp:Label ID="Label7" runat="server" class="col-lg-12 control-label text-left" Text="Veículo"></asp:Label>
                                                                                    </h5>
                                                                                    <asp:TextBox ID="tbVeiculoSID" ClientIDMode="Static" runat="server" placeholder="Veículo Excipiente" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Veículo Excipiente" MaxLength="100"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <h5>
                                                                                        <asp:Label ID="Label8" runat="server" class="col-lg-12 control-label text-left" Text="Quantidade"></asp:Label>
                                                                                    </h5>
                                                                                    <asp:TextBox ID="tbQuantSID" runat="server" placeholder="Quantidade do Veículo" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Quantidade"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="ibox ">
                                                                        <div class="ibox-content">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <h5>
                                                                                        <asp:Label ID="Label9" runat="server" class="col-lg-12 control-label text-left" Text="Posologia"></asp:Label>
                                                                                    </h5>
                                                                                    <asp:TextBox ID="tbPosolSID" runat="server" placeholder="Posologia" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Posologia" MaxLength="200"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
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
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox-content" style="margin-bottom: 5px">
                                    <div class="btn-group" role="group" style="display: flex; justify-content: flex-end">
                                        <div id="blocoAssinatura" style="display: flex; justify-content: center; flex-direction: column; align-items: center; width: 25%">
                                            <asp:Label ID="lblLocalData" runat="server" class="control-label text-right" Text="Local e Data automática"></asp:Label>
                                            <asp:Image ID="imgAssinatura" ImageUrl="~/Perfil/Assinaturas/assinatura_receita.png" alt="imagem assinatura digitalizada" class="img-fluid rounded mx-auto d-block imgAssinatura" Style="width: 100%;" runat="server" />
                                            <asp:Label ID="lblNomeVeterinario" runat="server" class="control-label text-center" Text="Nome Médico Veterinário"></asp:Label>
                                            <asp:Label ID="lblTituloECRMV" runat="server" class="control-label text-center" Text="Médico Veterinário - "></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox ">
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="pagina-footer col-lg-12 text-right">
                                                <div class="btn-group" role="group">
                                                    <asp:LinkButton runat="server" ID="lbFechar" CssClass="btn btn-default" OnClick="lbFechar_Click"><i class="fas fa-door-open"></i>&nbsp;Fechar </asp:LinkButton>
                                                    
                                                    <div class="btn-group dropup">
                                                        <a class="btn btn-success dropdown-toggle" data-placement="top" data-toggle="dropdown"><i class="fas fa-print"></i>&nbsp;imprimir</a>
                                                        <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                                            <li>
                                                                <asp:HyperLink ID="hlImprCardapioPdf" runat="server" title="Impressão do Cardápio em PDF" CssClass="btn btn-ligth" Target="_blank" Enabled="False"><i class="far fa-file-pdf"></i>&nbsp;PDF</asp:HyperLink>
                                                            </li>
                                                            <li>
                                                                <hr class="dropdown-divider"></hr>
                                                            </li>
                                                            <li>
                                                                <asp:HyperLink ID="hlImprCardapioWord" runat="server" title="Impressão do Cardápio em Word" CssClass="btn btn-secondary" Target="_blank" Enabled="False"><i class="far fa-file-word"></i>&nbsp;Word</asp:HyperLink>
                                                            </li>
                                                            <li>
                                                                <asp:HyperLink ID="hlImprCardapioExcel" runat="server" title="Impressão do Cardápio em Excel" CssClass="btn btn-secondary" Target="_blank" Enabled="False"><i class="far fa-file-excel"></i>&nbsp;Excel</asp:HyperLink>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    
                                                    <asp:LinkButton ID="lbSalvaReceSupl" runat="server" title="Salva as Informações da Receita de Suplementação" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvaReceSupl_Click" Visible="True"><i class='far fa-save' aria-hidden="true"></i>&nbsp;Salvar</asp:LinkButton>
                                                    <%--<asp:HyperLink ID="hlImprReceSupl" runat="server" title="Impressão da Receita de Suplementação" CssClass="btn btn-secondary" Target="_blank" Enabled="False"><i class="fas fa-print"></i>&nbsp;imprimir</asp:HyperLink>--%>
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

            <!-- Central Modal Medium Success -->
            <div id="myModal" runat="server" style="display: none">
                <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                    <!--Content-->
                    <div class="modal-content">
                        <!--Header-->
                        <div class="modal-header modal-header-success">
                            <div class="col-md-9 col-sm-9 col-xs-9">
                                <i class="fa fa-cogs fa-2x" aria-hidden="true"></i>
                                <asp:Label ID="lblTituloModal" runat="server" Text="Receituário - Nutracêuticos" class="heading lead"></asp:Label>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <asp:LinkButton runat="server" ID="LinkButton8" CssClass="close" data-dismiss="modal"> <i class='fa fa-times-circle'></i></asp:LinkButton>
                            </div>
                        </div>

                        <!--Body-->
                        <asp:Panel ID="pnlDescricao" runat="server" Height="120px">
                            <div class="row">
                                <div class="col-sm-12 col-lg-12 form-group ">
                                    <asp:Label ID="lblDescricao" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-9 col-lg-9 form-group ">
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:HyperLink ID="hlkPerfil" runat="server" NavigateUrl="~/Perfil/Perfil.aspx">Link para a Tela de Perfil</asp:HyperLink>
                                </div>
                            </div>
                        </asp:Panel>

                        <!--Footer-->
                        <div class="modal-footer justify-content-center">
                            <div class="btn-group" role="group">
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="btnFechar" CssClass="btn btn-default" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!--/.Content-->
                </div>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="popUpModal" runat="server"
                PopupControlID="myModal" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblPopUp" OkControlID="btnFechar" CancelControlID="btnFechar">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblPopUp" runat="server" Text=""></asp:Label>
            <!-- Central Modal Medium Success-->

        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="pagina-footer navbar-fixed-bottom" role="banner">
        <div class="container">
            <div class="pull-left">
                |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="https://www.youtube.com/channel/UCPk1NVPuAgVPjf6eQOI5qeg?view_as=public" target="_blank"><i class="fab fa-youtube"></i></a>&nbsp;
                |&nbsp;&nbsp;<a href="https://www.facebook.com/nutrovetonline/" target="_blank" class="facebook"><i class="fab fa-facebook"></i></a>&nbsp;
                |&nbsp;<a href="https://www.instagram.com/nutrovetonline/" target="_blank" class="instagram"><i class="fab fa-instagram"></i></a>&nbsp;|
            </div>
            <div class="pull-right">
                NutroVET by <strong>RD Sistemas &copy;<asp:Label ID="lblAno" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </div>
</asp:Content>

