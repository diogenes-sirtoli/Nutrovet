<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="FinalizarAssinatura.aspx.cs" Inherits="Nutrovet.Plano.FinalizarAssinatura" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <section id="pricing" style="background: #F0F0F0;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <div id="page-wrapper" class="gray-bg">
                        <div class="row wrapper border-bottom white-bg page-heading">
                            <div class="col-lg-12">
                                <h2>Resumo</h2>
                            </div>
                        </div>
                        <div class="wrapper wrapper-content fadeInRight">
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="ibox">
                                        <div class="ibox-title">
                                            <span class="pull-right">(<strong><asp:Label ID="lblNumeroItens" runat="server" Text="Label"></asp:Label></strong>) itens</span>
                                            <h5><strong>
                                                <asp:Label ID="lblSelecionados" runat="server" Text=""></asp:Label></strong></h5>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="table-responsive">
                                                <asp:HiddenField ID="hfID" runat="server" />

                                                <table class="table shoping-cart-table">
                                                    <tbody>
                                                        <asp:Repeater ID="rptPlanos" runat="server" OnItemDataBound="rptPlanos_ItemDataBound">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="desc">
                                                                        <h4 class="text-info" id="hPlanoSelecionado" runat="server">
                                                                            <i class="fas fa-paper-plane" runat="server" id="iconeDoPlano"></i>&nbsp;
                                                                            <asp:Label ID="lblPlanoSelecionado" runat="server" Text=""></asp:Label>
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
                                                            <asp:Repeater ID="rptModulos" runat="server" OnItemDataBound="rptModulos_ItemDataBound">
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
                                                                        <td class="pull-right" style="background: #fff"></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
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
                                            <span class="pull-right"><strong>
                                                <asp:Label ID="lblNumeroDescontos" runat="server" Text="Label"></asp:Label></strong></span>
                                            <h5>Voucher Concedido</h5>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4 ">
                                    <div class="row wrapper border-bottom white-bg page-heading">
                                        <div class="col-lg-12">
                                            <h2>Sua Assinatura</h2>
                                        </div>
                                    </div>
                                    <div class="ibox">
                                        <div class="ibox-content">

                                            <h2 class="font-bold">
                                                <asp:Label ID="lblValorAssinatura" runat="server" Text="R$ Valor"></asp:Label>
                                            </h2>
                                            <hr />
                                            <h5 class="section-title wow fadeInDown text-center">
                                                <img class="img-fluid " src="../Imagens/pagarme.png" alt="logo" />
                                            </h5>


                                            <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                                                <hr />
                                                <asp:UpdateProgress ID="UpdateProgress" class="divProgress" runat="server" DisplayAfter="1000">
                                                    <ProgressTemplate>
                                                        <div class="semipolar-spinner">
                                                            <div class="ring"></div>
                                                            <div class="ring"></div>
                                                            <div class="ring"></div>
                                                            <div class="ring"></div>
                                                            <div class="ring"></div>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <hr />
                                            </div>


                                            <div class="middle-box text-center loginscreen fadeInDown">
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

                <!-- Central Modal -->
                <div id="myModal" runat="server" class="modal-dialog modal-lg" style="display: none">
                    <div class="modal-dialog modal-notify modal-success modal-dialog-centered" role="document">
                        <!--Content-->
                        <div class="modal-content animated flipInY">
                            <!--Header-->
                            <div class="modal-header">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <h4 class="modal-title"><i class="fas fa-info-circle fa-fw"></i>
                                        <asp:Label ID="lblTituloModal" class="heading lead" runat="server" Text="Nutrovet Informa"></asp:Label></h4>
                                </div>

                            </div>
                            <!--/Header-->
                            <!--Body-->
                            <div class="modal-body">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label ID="lblMensagemAssinatura" runat="server" Text="Label"></asp:Label></strong>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblMsgModal" runat="server" Text="Label"></asp:Label>
                                </div>

                            </div>
                            <!--/Body-->
                            <!--Footer-->
                            <div class="modal-footer justify-content-center">
                                <asp:LinkButton runat="server" ID="btnSim" CssClass="btn btn-sm btn-primary-nutrovet pull-right m-t-n-xs" data-dismiss="modal" OnClick="btnSim_Click" Visible="false"><i class="far fa-check-square"></i>&nbsp;Ok</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnNao" CssClass="btn btn-sm btn-primary-nutrovet pull-right m-t-n-xs" data-dismiss="modal" OnClick="btnNao_Click" Visible="false"><i class='far fa-check-square'></i>&nbsp;Ok </asp:LinkButton>
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
