<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true"
    CodeBehind="EscolherAssinatura.aspx.cs" Inherits="Nutrovet.Plano.EscolherAssinatura" ValidateRequest="false" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div runat="server" id="alertas">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <h4>
                    <asp:Label ID="lblAlerta" runat="server"></asp:Label>
                </h4>
            </div>
            <section class="details-card">
                <div class="container">
                    <div class="ibox-title col-lg-12">
                        <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                            <div class="section-header">
                                <h1 class="section-title wow fadeInDown"><i class="fas fa-handshake"></i>&nbsp;<strong>Planos</strong></h1>
                                <h4><small>O sistema oferece espaços diferentes para você cadastrar seus pacientes, de forma que você pode pagar de acordo com o espaço que precisará utilizar. Independentemente se você vai pagar por ano ou por mês, o espaço que você estará contratando é o mesmo, então ao atingir o seu número de pacientes limite, basta migrar para um plano com maior espaço.</small></h4>
                            </div>
                        </div>
                        <div class="table-responsive col-lg-12">
                            <div class="table-responsive">
                                <table class="table shoping-cart-table">
                                    <tbody>
                                        <tr>
                                            <td class="desc">
                                                <asp:Panel ID="pnlVoucher" runat="server" DefaultButton="lbVerificarVoucher">
                                                    <h4><strong>
                                                        <asp:Label runat="server" CssClass="col-lg-1 control-label" ID="lbVoucher" Text="Voucher"></asp:Label></h4>
                                                    </strong>
                                                    <div class="col-lg-10">
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
                            <div class="col-md-2">
                                
                            </div>
                            <div class="col-md-3">
                                <div class="card-content" id="cardBasico" runat="server">
                                    <div class="card-img">
                                        <img class="img-card" src="../Imagens/aviaopapel.png" />
                                    </div>
                                    <p></p>
                                    <h4 class="card-title">BÁSICO</h4>
                                    <h6>
                                        <asp:Label ID="lblDescrB" runat="server" Text=""></asp:Label>
                                    </h6>
                                    <h5 class="badge"><i class="fas fa-paper-plane"></i>&nbsp;até 10 pacientes</h5>
                                    <div class="radio radio-primary">
                                        <asp:RadioButtonList ID="rblBasico" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblBasico_SelectedIndexChanged">
                                            <asp:ListItem Value="1">R$ 25,00 por mês<br></br></asp:ListItem>
                                            <asp:ListItem Value="2">R$ 250,00 por ano</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <p></p>
                            </div>
                            <div class="col-md-3 pull-center">
                                <div class="card-content" id="cardIntermediario" runat="server">
                                    <div class="card-img">
                                        <img class="img-card" src="../Imagens/aviao.png" />
                                    </div>
                                    <p></p>
                                    <h4 class="card-title align-center">INTERMEDIÁRIO</h4>
                                    <h6>
                                        <asp:Label ID="lblDescrI" runat="server" Text=""></asp:Label>
                                    </h6>
                                    <h5 class="badge"><i class="fas fa-plane fa-flip-horizontal"></i>&nbsp;até 20 pacientes</h5>
                                    <div class="radio radio-primary">

                                        <asp:RadioButtonList ID="rblIntermediario" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblIntermediario_SelectedIndexChanged">
                                            <asp:ListItem Value="1">R$ 47,00 por mês<br></br></asp:ListItem>
                                            <asp:ListItem Value="2">R$ 470,00 por ano</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>
                                </div>
                                <p></p>
                            </div>
                            <div class="col-md-3 pull-center">
                                <div class="card-content" id="cardCompleto" runat="server">
                                    <div class="card-img">
                                        <img class="img-card" src="../Imagens/foguete.png" />
                                    </div>
                                    <p></p>
                                    <h4 class="card-title pull-center">COMPLETO</h4>
                                    <h6>
                                        <asp:Label ID="lblDescrC" runat="server" Text=""></asp:Label>
                                    </h6>
                                    <h5 class="badge"><i class="fas fa-rocket"></i>&nbsp;acima de 20 pacientes</h5>
                                    <div class="radio radio-primary ">

                                        <asp:RadioButtonList ID="rblCompleto" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblCompleto_SelectedIndexChanged">
                                            <asp:ListItem Value="1">R$ 69,00 por mês<br></br></asp:ListItem>
                                            <asp:ListItem Value="2">R$ 690,00 por ano</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>

                                </div>
                                <p></p>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="ibox-title col-lg-12">
                        <div class="generic_feature_list pull-right">
                            <h2 class="section-title wow fadeInDown">
                                <asp:LinkButton runat="server" ID="lbAvancar" CssClass="btn btn-md btn-primary-nutrovet m-t-n-xs" OnClick="btnAvancar_Click">AVANÇAR&nbsp;<i class="fas fa-hand-point-right"></i></asp:LinkButton>
                            </h2>
                        </div>
                    </div>
                </div>
                <div runat="server" visible="false">
                    <div class="section-header">
                        <h1 class="section-title wow fadeInDown"><i class="fas fa-cogs"></i>&nbsp;<strong>Módulos Adicionais</strong></h1>
                        <h4><small>Em breve serão disponibilizados dois novos módulos adicionais, que você poderá optar por assinar ou não, de acordo com a sua necessidade. O <strong>Receituário Inteligente</strong> será uma ferramenta para auxiliar na elaboração de receitas de forma automatizada, e o <strong>Prontuário</strong> será uma forma de organizar as dietas, receitas, informações das consultas e demais detalhes, de forma prática e organizada como uma linha do tempo.</small></h4>
                    </div>
                    <div>
                        <fieldset>
                            <div class="container">
                                <div class="row">
                                    <div class="container">
                                        <hr />
                                        <div class="row">
                                            <section class="wrapperColor">
                                                <div class="container-fostrap">
                                                    <div class="content">
                                                        <div class="container">
                                                            <div class="row">
                                                                <div class="col-sm-3 col-md-3 col-lg-3"></div>

                                                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-8">
                                                                    <div class="card">
                                                                        <img class="img-card" src="../Imagens/receituario.png" />
                                                                        <div class="card-content">
                                                                            <h6 class="card-title">
                                                                                <h6 class="badge"><i class="fas fa-mortar-pestle"></i></h6>
                                                                                <strong>RECEITUÁRIO</strong><h6></h6>
                                                                                <div class="generic_feature_list">
                                                                                    <div id="recint" class="radio radio-primary">
                                                                                        <h4>
                                                                                            <asp:RadioButtonList ID="rblReceituario" runat="server" CssClass="alert">
                                                                                                <asp:ListItem Value="1">R$ 10,00/mês<br></br></asp:ListItem>
                                                                                                <asp:ListItem Value="2">R$ 100,00/ano</asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </h4>
                                                                                    </div>
                                                                                </div>
                                                                                <h6></h6>
                                                                                <h6></h6>
                                                                                <h6></h6>
                                                                                <h6></h6>
                                                                            </h6>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-8">
                                                                    <div class="card">
                                                                        <img class="img-card" src="../Imagens/pronturario.png" />
                                                                        <div class="card-content">
                                                                            <h4 class="card-title pull-center">
                                                                                <h6 class="badge"><i class="fas fa-notes-medical"></i></h6>
                                                                                <strong>PRONTUÁRIO</strong><h4></h4>
                                                                                <div class="radio radio-primary">
                                                                                    <h4>
                                                                                        <asp:RadioButtonList ID="rblProntuario" runat="server" CssClass="alert">
                                                                                            <asp:ListItem Value="1">R$ 15,00/mês<br></br></asp:ListItem>
                                                                                            <asp:ListItem Value="2">R$ 150,00/ano</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </h4>
                                                                                </div>
                                                                                <h4></h4>
                                                                                <h4></h4>
                                                                                <h4></h4>
                                                                                <h4></h4>
                                                                            </h4>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

