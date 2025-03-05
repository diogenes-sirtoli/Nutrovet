<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true" CodeBehind="AssinantesSelecao.aspx.cs" Inherits="Nutrovet.Administracao.AssinantesSelecao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>
        $(function () {
            $('.material-card > .mc-btn-action').click(function () {
                var card = $(this).parent('.material-card');
                var icon = $(this).children('i');
                //card.preventDefault()
                icon.addClass('fa-spin-fast');

                if (card.hasClass('mc-active')) {
                    card.removeClass('mc-active');

                    window.setTimeout(function () {
                        icon
                            .removeClass('fa-arrow-left')
                            .removeClass('fa-spin-fast')
                            .addClass('fa-bars');

                    }, 500);
                } else {
                    card.addClass('mc-active');

                    window.setTimeout(function () {
                        icon
                            .removeClass('fa-bars')
                            .removeClass('fa-spin-fast')
                            .addClass('fa-arrow-left');

                    }, 500);
                }
            });
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
                        <h2>Assinantes</h2>
                        <div class="col-lg-4">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <i class="fa fa-id-badge"></i><strong>&nbsp;Assinantes</strong>
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
            <div>
                <div class="page-title">
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row wrapper border-bottom white-bg page-heading">
                                <div class="ibox-content">
                                    <div class="search-form">
                                        <div class="input-group col-lg-12">
                                            <asp:TextBox ID="tbPesq" runat="server" placeholder="Pesquisar pelo nome ou e-mail do Assinante" CssClass="form-control input-md bg-muted"></asp:TextBox>
                                            <div class="input-group-btn">
                                                <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-md btn-primary-nutrovet" OnClick="lbPesq_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfID" runat="server" />
                                <!-- start project list -->
                                <asp:Repeater ID="rptListagemAssinantes" runat="server" OnItemCommand="rptListagemAssinantes_ItemCommand" OnItemDataBound="rptListagemAssinantes_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4">
                                            <div class="panel panel-success">
                                                <div class="panel-heading">
                                                    <b title="Nome do Assinante">
                                                        <asp:Label ID="lblNomeAssinanteRegistro" runat="server" Text='<%# Eval("Nome") %>'></asp:Label></b>
                                                    <p title="E-mail">
                                                        <asp:Label class="label label-primary" ID="lblEmailAssinanteRegistro" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                        <img class="img-responsive  pull-right" src="../Imagens/user-color.png" />
                                                    </p>
                                                    <p></p>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="alert alert-info col-xs-12 col-sm-12 col-md-12 col-lg-12 pull-left">
                                                        <asp:Label ID="lblDadosDoPlano" runat="server"><strong>Dados do Plano</strong></asp:Label>
                                                        <hr />
                                                        Data de Início:
                                                        <span class="badge badge-info ">
                                                            <asp:Label ID="lblAssinanteDataInicio" runat="server"></asp:Label>
                                                        </span><br />
                                                        Data de Fim:
                                                        <span class="badge badge-info ">
                                                            <asp:Label ID="lbAssinanteDataFim" runat="server"></asp:Label>
                                                        </span>
                                                        <%--Data de Renovação:
                                                        <span class="badge badge-info"><asp:Label ID="lblAssinanteDataFim" runat="server"></asp:Label></span>--%>
                                                        <hr />
                                                        <div class="col-xs-5 col-sm-6 col-md-5 col-lg-5 " title="Tipo e Período do Plano">
                                                            <asp:DropDownList ID="ddlTipoPlano" runat="server" AutoPostBack="False" class="" data-toggle="tooltip" data-placement="top" title="Selecione o Plano">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-xs-5 col-sm-6 col-md-5 col-lg-5" title="Tipo e Período do Plano">
                                                            <asp:DropDownList ID="ddlPeriodoPlano" runat="server" class="" AutoPostBack="False" data-toggle="tooltip" data-placement="top" title="Selecione Período do Plano">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1" title="Tipo e Período do Plano">
                                                            <asp:LinkButton runat="server" ID="lbSalvar" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="salvar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdPessoa") %>' data-toggle="tooltip" data-placement="top" title="Salvar alteração do Plano"><i class='far fa-save'></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row"></div>
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7">
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <i class="fas fa-lock-open pull-left"></i>&nbsp;Senha:
                                                            </div>
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <span class="label label-primary-nutrovet">
                                                                    <asp:Label class='' ID="lblSenhaAssinanteRegistro" runat="server"></asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5">
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12">
                                                                <i class="fas fa-user-lock pull-left"></i>&nbsp;Bloq.:
                                                            </div>
                                                            <div class="col-xs-12 col-md-12">
                                                                <span class="">
                                                                    <asp:Label ID="lblbloqueadoRegistro" runat="server" Text='<%# Eval("Bloqueado") %>'></asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"></div>
                                                        <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7">
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12">
                                                                <i class="fab fa-creative-commons-sampling pull-left"></i>&nbsp;Ativo:
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12">
                                                                <span class="">
                                                                    <asp:Label ID="lblAtivoAssinanteRegistro" runat="server" Text='<%# Eval("Ativo") %>'></asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5">
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12">
                                                                <i class="far fa-play-circle pull-left"></i>&nbsp;Plano
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-lg-12 col-xs-12">
                                                                <span class="">
                                                                    <asp:LinkButton ID="lbBloquearAssinante" runat="server" CssClass="btn btn-danger btn-xs" CommandName="bloquear" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdPessoa") %>'></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbDesbloquearAssinante" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="desbloquear" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdPessoa") %>'> <i class="fas fa-check-square"></i> Desbloq.</asp:LinkButton>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>

                                    </FooterTemplate>
                                </asp:Repeater>
                                <!-- end project list -->
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="">
                <div class="page-title">
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="row wrapper border-bottom white-bg page-heading">
                                    <div class="col-lg-2 form-group ">
                                        <h5>Registros por página </h5>
                                    </div>
                                    <div class="col-lg-1 form-group ">
                                        <asp:DropDownList ID="ddlPag" runat="server" CssClass="btn btn-default form-control" AutoPostBack="True" Style="width: 70px" OnSelectedIndexChanged="ddlPag_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">9</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-9 text-right">
                                        <asp:LinkButton runat="server" ID="lbPagPrimeira" CssClass="btn btn-default" ToolTip="Primeira" OnClick="lbPagPrimeira_Click"> <i class='fa fa-backward'></i> </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbAnt" CssClass="btn btn-default" Visible="False" OnClick="lbAnt_Click" ToolTip="Anterior"><i class='fa fa-step-backward'></i></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lb1" CssClass="btn btn-default" CommandName="1" OnClick="lb1_Click"> <b><u>1</u></b> </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lb2" CssClass="btn btn-default" CommandName="2" OnClick="lb2_Click"> 2 </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lb3" CssClass="btn btn-default" CommandName="3" OnClick="lb3_Click"> 3 </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lb4" CssClass="btn btn-default" CommandName="4" OnClick="lb4_Click"> 4 </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lb5" CssClass="btn btn-default" CommandName="5" OnClick="lb5_Click"> 5 </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbPost" CssClass="btn btn-default" ToolTip="Próxima" OnClick="lbPost_Click"><i class='fa fa-step-forward'></i></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbPagUltima" CssClass="btn btn-default" ToolTip="Última" OnClick="lbPagUltima_Click"> <i class='fa fa-forward'></i> </asp:LinkButton>
                                    </div>
                                </div>
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
                <a href="https://www.youtube.com/channel/UCPk1NVPuAgVPjf6eQOI5qeg?view_as=public" target="_blank"><i class="fab fa-youtube"></i></a>
                <a href="https://www.facebook.com/nutrovetonline/" target="_blank" class="facebook"><i class="fab fa-facebook"></i></a>
                <a href="https://www.instagram.com/nutrovetonline/" target="_blank" class="instagram"><i class="fab fa-instagram"></i></a>
            </div>
            <div class="pull-right">
                NutroVET by <strong>SICORP &copy;<asp:Label ID="lblAno" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </div>
</asp:Content>
