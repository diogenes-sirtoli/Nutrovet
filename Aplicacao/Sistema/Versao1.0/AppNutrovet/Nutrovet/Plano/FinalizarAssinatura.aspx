<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="FinalizarAssinatura.aspx.cs" Inherits="Nutrovet.Plano.FinalizarAssinatura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
        <section id="pricing" style="background: #F0F0F0;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <hr />
                    <div class="container">
                        <div id="page-wrapper" class="gray-bg">
                            <div class="row wrapper border-bottom white-bg page-heading">
                                <div class="col-lg-12">
                                    <h2>Resumo e Assinatura</h2>
                                </div>
                            </div>
                            <div class="wrapper wrapper-content fadeInRight">
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="ibox">
                                            <div class="ibox-title">
                                                <span class="pull-right">(<strong><asp:Label ID="lblNumeroItens" runat="server" Text="Label"></asp:Label></strong>) itens</span>
                                                <h5>Selecionados</h5>
                                            </div>
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
                                                                    <h3 class="text-info">
                                                                        <i class="fas fa-paper-plane"></i>&nbsp;<asp:Label ID="lblPlanoSelecionado" runat="server" Text="Plano Selecionado"></asp:Label>
                                                                    </h3>
                                                                    <asp:Label ID="lblTextoPlanoSelecionado" class="small" runat="server" Text="Texto para o plano selecionado"></asp:Label>
                                                                </td>
                                                                <td class="pull-right" style="background: #fff">
                                                                    <h4>
                                                                        <asp:Label ID="lblValorPlanoSelecionado" runat="server" Text="R$ Valor"></asp:Label></h4>
                                                                    <asp:HyperLink ID="hlkRemoverPlanoSelecionado" NavigateUrl="#" class="text-muted small" runat="server"><i class="fa fa-trash"></i>&nbsp;Remover ítem</asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="ibox-content">
                                                <div class="table-responsive">
                                                    <table class="table shoping-cart-table">
                                                        <tbody>
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
                                                                    <asp:HyperLink ID="hlkRemoverModuloAdicional" NavigateUrl="#" class="text-muted small" runat="server"><i class="fa fa-trash"></i>&nbsp;Remover ítem</asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="row wrapper border-bottom white-bg page-heading">
                                                <div class="col-lg-12">
                                                    <h2>Área Promocional</h2>
                                                </div>
                                            </div>
                                            <div class="ibox-title">
                                                <span class="pull-right">(<strong><asp:Label ID="lblNumeroDescontos" runat="server" Text="Label"></asp:Label></strong>) descontos</span>
                                                <h5>Itens concedidos</h5>
                                            </div>
                                            <div class="ibox-content">
                                                <div class="table-responsive">
                                                    <table class="table shoping-cart-table">
                                                        <tbody>
                                                            <tr>
                                                                <td class="desc">
                                                                    <h4 class="text-success"><i class="fas fa-gifts"></i>&nbsp;Informe aqui seu Voucher
                                                                    </h4>
                                                                    
                                                                    <asp:Panel ID="pnlVoucher" runat="server" DefaultButton="lbVerificarVoucher">
                                                                        <div class="col-lg-10 ">
                                                                                <asp:TextBox ID="txbVoucher" runat="server" class="form-control  mx-sm-3 mb-2" placeholder="Informe aqui" min="0" TextMode="Number" ToolTip="Informe o número do VOUCHER"></asp:TextBox>
                                                                                <asp:Label ID="lblTextoVoucher" class="small" runat="server" Text="Aqui você recebe benefícios ao contratar o plano utilizando um voucher"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-2 ">
                                                                                <asp:LinkButton runat="server" ID="lbVerificarVoucher" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" OnClick="lbVerificarVoucher_Click"><i class="fas fa-calculator"></i>&nbsp;Validar</asp:LinkButton>
                                                                            
                                                                        </div>
                                                                        
                                                                    </asp:Panel>
                                                                </td>
                                                                <td class="pull-right">
                                                                    <h4>
                                                                        <asp:Label ID="lblValorVoucher" runat="server" Text="R$ Valor"></asp:Label></h4>
                                                                    <asp:HyperLink ID="hlkRemoverVoucher" NavigateUrl="#" class="text-muted small" runat="server"><i class="fa fa-trash"></i>&nbsp;Remover ítem</asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 ">
                                        <div class="ibox">
                                            <div class="ibox-content">
                                                <h5>AQUI ESTÁ O TOTAL DA ASSINATURA </h5>
                                                <h2 class="font-bold">
                                                    <asp:Label ID="lblValorAssinatura" runat="server" Text="R$ Valor"></asp:Label>
                                                </h2>
                                                <hr />
                                                <h5 class="section-title wow fadeInDown">PAGAMENTO VIA
                                                        <img src="../Imagens/pagseguro_sm.png" alt="logo" />
                                                </h5>

                                                <hr />
                                                <div class="middle-box text-center loginscreen   animated fadeInDown">
                                                    <div>
                                                        <div class="form-group">
                                                            <asp:LinkButton runat="server" ID="lbVoltar" CssClass="btn btn-sm BLOCK btn-default m-t-n-xs" OnClick="lbVoltar_Click"><i class="fas fa-backspace"></i>&nbsp;Voltar</asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lbAssinar" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" OnClick="lbAssinar_Click"><i class="fa fa-shopping-cart"></i>&nbsp;Assinar</asp:LinkButton>
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