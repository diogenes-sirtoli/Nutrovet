<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true" CodeBehind="AssinantesSelecao.aspx.cs" Inherits="Nutrovet.Administracao.AssinantesSelecao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

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
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                    <div class="panel panel-success">
                                        <b title="Filtrar por assinaturas a vencer">
                                            <asp:Label ID="lblFiltroVencimentoAssinaturas" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Assinaturas a vencerem em:"></asp:Label></b>
                                        <div class="panel-heading">
                                            <asp:RadioButtonList runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" TextAlign="right" ID="rblDias">
                                                <%--<asp:RadioButtonList runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" TextAlign="right" ID="RadioButtonList1" AutoPostBack="true"  OnSelectedIndexChanged="rblDias_SelectedIndexChanged">--%>
                                                <asp:ListItem class="radio-inline" Value="0" Selected="True">&nbsp;&nbsp;Sem&nbsp;Filtro&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="1">&nbsp;&nbsp;1&nbsp;dia&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="5">&nbsp;&nbsp;5&nbsp;dias&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="15">&nbsp;&nbsp;15&nbsp;dias&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="30">&nbsp;&nbsp;30&nbsp;dias</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5">
                                    <div class="panel panel-success">
                                        <b title="Filtrar por assinaturas a vencer">
                                            <asp:Label ID="lblFiltroTipoAssinaturas" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Tipo de Assinatura:"></asp:Label></b>
                                        <div class="panel-heading">
                                            <asp:RadioButtonList runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" TextAlign="right" ID="rblTipoAssinaturas" AutoPostBack="True" OnSelectedIndexChanged="rblTipoAssinaturas_SelectedIndexChanged">
                                                <asp:ListItem class="radio-inline" Value="0" Selected="True">&nbsp;Sem&nbsp;Filtro&nbsp;</asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="1">&nbsp;Vencida&nbsp;</asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="2">&nbsp;PagarMe&nbsp;</asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="3">&nbsp;Outros</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-1">
                                    <div class="panel-heading">
                                        <asp:LinkButton ID="lbFilter" runat="server" CssClass="btn btn-md btn-primary-nutrovet" OnClick="lbFilter_Click" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Aplica o filtro selecionado à Pesquisa"><i class="fas fa-filter"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfID" runat="server" />
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                <!-- start project list -->
                                <asp:Repeater ID="rptListagemAssinantes" runat="server" OnItemCommand="rptListagemAssinantes_ItemCommand" OnItemDataBound="rptListagemAssinantes_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                            <div class="thumbnail">
                                                <div class="caption">
                                                    <div class='col-xs-12 col-sm-12 col-md-12 col-lg-12 well well-add-card'>
                                                        <img class="img-responsive  pull-right" src="../Imagens/user-color.png" />
                                                        <b>
                                                            <h3>
                                                                <asp:Label ID="lblNomeAssinanteRegistro" Style="color: #1A1A40;" runat="server" Text='<%# Eval("Nome") %>' data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Nome do Assinante"></asp:Label></h3>
                                                        </b>
                                                    </div>
                                                    <div class='col-md-12 col-sm-12 col-lg-12 col-xs-12'>
                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>

                                                        <div class="row">
                                                            <div class="form-group">
                                                                <div class="col-md-4 col-sm-12 col-lg-4 col-xs-4">
                                                                    <asp:Label ID="lblCodAssinante" runat="server"><strong>Código </strong></asp:Label>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fa-solid fa-id-badge"></i></span>
                                                                        <asp:Label class="form-control" ID="lblIdAssinante" runat="server" Text='<%# Eval("IdPessoa") %>' data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Plano do Assinante"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-8 col-sm-12 col-lg-8 col-xs-8">
                                                                    <asp:Label ID="Label3" runat="server"><strong>Plano</strong></asp:Label>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fas fa-handshake fa-fw "></i></span>
                                                                        <asp:Label class="form-control  table-responsive" ID="lblPlanoAssinante" runat="server" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Plano do Assinante"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                <div class="col-xs-12 col-md-12 col-sm-12 col-lg-5 col-xs-5 table-responsive">
                                                                    <asp:Label ID="Label8" runat="server"><strong>PagarMe</strong></asp:Label>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fa-solid fa-circle-info"></i></span>
                                                                        <asp:Label class="form-control" ID="lblClientePagarMe" runat="server" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Identifica se o assinante pertence ao PagarMe"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-xs-12 col-md-12 col-sm-12 col-lg-7 col-xs-7 table-responsive">
                                                                    <asp:Label ID="lblStatusPlano" runat="server"><strong>Status do Plano</strong></asp:Label>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fa-solid fa-circle-info"></i></span>
                                                                        <asp:Label class="form-control" ID="lblDescPlanoVencido" runat="server" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Identifica se o plano do assinante está vencido"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                <div class="row">
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                                <div class="form-group">
                                                                    <div class="col-xs-6 col-md-12 col-sm-6 col-lg-6">
                                                                        <asp:Label ID="Label9" runat="server"><strong>Data Inicial</strong></asp:Label>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon"><i class="fa-solid fa-calendar-day"></i></span>
                                                                            <asp:Label class="form-control" Style="background-color: #87CEFA; color: #1A1A40; font-weight: bolder" ID="lblAssinanteDataInicio" runat="server" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Data de início de liberação de acesso ao assinante"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xs-6 col-md-12 col-sm-6 col-lg-6 ">
                                                                        <asp:Label ID="Label11" runat="server"><strong>Data Final</strong></asp:Label>
                                                                        <div class="input-group">
                                                                            <span class="input-group-addon"><i class="fa-solid fa-calendar-week"></i></span>
                                                                            <asp:Label class="form-control" Style="background-color: #87CEFA; color: #1A1A40; font-weight: bolder" ID="lbAssinanteDataFim" runat="server" data-toggle="tooltip" data-placement="top" TextMode="SingleLine" title="Data de término de liberação de acesso ao assinante"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12 table-responsive">
                                                                    <asp:Label ID="lblUsuarioAcesso" runat="server"><strong>Usuario de Acesso</strong></asp:Label>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fas fa-user-lock pull-left fa-fw "></i></span>
                                                                        <asp:Label class="form-control" ID="Label4" runat="server" Text='<%# Eval("Usuario") %>'></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12">
                                                                    <asp:Label ID="lblSenhaAcesso" runat="server"><strong>Senha de Acesso</strong></asp:Label>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fab fa-creative-commons-sampling fa-fw"></i></span>
                                                                        <asp:Label class="form-control" ID="lblSenhaAssinanteRegistro" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                        </div>
                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12 table-responsive">
                                                                    <asp:Label ID="Label1" runat="server"><strong>Voucher</strong></asp:Label>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fas fa-ticket-alt pull-left fa-fw "></i></span>
                                                                        <asp:Label class="form-control" ID="Label6" runat="server" Text='<%# Eval("NrCupom") %>'></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                        </div>
                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <div class="col-md-12 col-sm-12 col-xs-6 col-lg-6">
                                                                    <asp:Label ID="Label2" runat="server"><strong>Senha Bloqueada</strong></asp:Label>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fas fa-user-lock pull-left fa-fw "></i></span>
                                                                        <asp:Label class="form-control" ID="lblbloqueadoRegistro" runat="server" Text='<%# Eval("Bloqueado") %>'></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Label ID="Label5" runat="server"><strong>Acesso ao Sistema</strong></asp:Label>
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon" style=""><i class="fab fa-creative-commons-sampling fa-fw"></i></span>
                                                                        <asp:Label class="form-control" ID="lblAtivoAssinanteRegistro" runat="server" Text='<%# Eval("AcessoNoSistema") %>'></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer col-sm-12 col-lg-12">
                                                        <div class="btn-group" role="group">
                                                            <asp:LinkButton ID="lbBloquearAssinante" runat="server" CssClass="btn btn-danger btn-xs pull-right form-control" CommandName="bloquear" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdPessoa") %>'></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <p class="btn  btn-xs btn-update btn-add-card"></p>
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
                NutroVET by <strong>RD Sistemas &copy;<asp:Label ID="lblAno" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </div>
</asp:Content>
