<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true"
    CodeBehind="EscolherAssinatura.aspx.cs" Inherits="Nutrovet.Plano.EscolherAssinatura" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div runat="server" id="alertas">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <h4>
            <asp:Label ID="lblAlerta" runat="server" Text=""></asp:Label>
        </h4>
    </div>
    <section id="pricing" style="background: #F0F0F0;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <hr />
                    <div class="section-header">
                        <h1 class="section-title wow fadeInDown"><i class="fas fa-handshake"></i>&nbsp;<strong>Planos</strong></h1>
                        <h4><small>O sistema oferece espaços diferentes para você cadastrar seus pacientes, de forma que você pode pagar de acordo com o espaço que precisará utilizar. Independentemente se você vai pagar por ano ou por mês, o espaço que você estará contratando é o mesmo, então ao atingir o seu número de pacientes limite, basta migrar para um plano com maior espaço.</small></h4>
                    </div>
                    <div class="container">
                        <hr />
                        <div class="row">
                            <section class="wrapperColor">
                                <div class="container-fostrap">
                                    <div class="content">
                                        <div class="container">
                                            <div class="row">
                                                <div class="offset-lg-2">
                                                </div>
                                                <div class="col-sm-1 col-md-1 col-lg-1 "></div>
                                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-8">
                                                    <div class="card">
                                                        <img class="img-card" src="../Imagens/aviaopapel.png" />
                                                        <div class="card-content">
                                                            <h6 class="card-title">BÁSICO</h6>
                                                            <h5 class="badge"><i class="fas fa-paper-plane"></i>&nbsp;até 10 pacientes</h5>
                                                            <div class="generic_feature_list">
                                                                <div class="radio radio-primary">
                                                                    <h4>
                                                                        <asp:RadioButtonList ID="rblBasico" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblBasico_SelectedIndexChanged">
                                                                            <asp:ListItem Value="1">R$ 20,00/mês<br></br></asp:ListItem>
                                                                            <asp:ListItem Value="2">R$ 200,00/ano</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </h4>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-8">
                                                    <div class="card">
                                                        <img class="img-card" src="../Imagens/aviao.png" />
                                                        <div class="card-content">
                                                            <h4 class="card-title pull-center">INTERMEDIÁRIO</h4>
                                                            <h5 class="badge"><i class="fas fa-plane"></i>&nbsp;até 20 pacientes</h5>
                                                            <div class="radio radio-primary">
                                                                <h4>
                                                                    <asp:RadioButtonList ID="rblIntermediario" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblIntermediario_SelectedIndexChanged">
                                                                        <asp:ListItem Value="1">R$ 40,00/mês<br></br></asp:ListItem>
                                                                        <asp:ListItem Value="2">R$ 400,00/ano</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </h4>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-8">
                                                    <div class="card">
                                                        <img class="img-card" src="../Imagens/foguete.png" />
                                                        <div class="card-content">
                                                            <h4 class="card-title pull-center">COMPLETO</h4>
                                                            <h5 class="badge"><i class="fas fa-rocket"></i>&nbsp;acima de 20 pacientes</h5>
                                                            <div class="radio radio-primary ">
                                                                <h4>
                                                                    <asp:RadioButtonList ID="rblCompleto" runat="server" CssClass="alert" AutoPostBack="True" OnSelectedIndexChanged="rblCompleto_SelectedIndexChanged">
                                                                        <asp:ListItem Value="1">R$ 60,00/mês<br></br></asp:ListItem>
                                                                        <asp:ListItem Value="2">R$ 600,00/ano</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </h4>
                                                            </div>
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
                                                                            <h6 class="card-title">RECEITUÁRIO</h6>
                                                                            <h5 class="badge"><i class="fas fa-mortar-pestle"></i></h5>
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
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-8">
                                                                    <div class="card">
                                                                        <img class="img-card" src="../Imagens/pronturario.png" />
                                                                        <div class="card-content">
                                                                            <h4 class="card-title pull-center">PRONTUÁRIO</h4>
                                                                            <h5 class="badge"><i class="fas fa-notes-medical"></i></h5>
                                                                            <div class="radio radio-primary">
                                                                                <h4>
                                                                                    <asp:RadioButtonList ID="rblProntuario" runat="server" CssClass="alert">
                                                                                        <asp:ListItem Value="1">R$ 15,00/mês<br></br></asp:ListItem>
                                                                                        <asp:ListItem Value="2">R$ 150,00/ano</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </h4>
                                                                            </div>
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
                    <div class="container">
                        <div class="section-header">
                            <h1 class="section-title wow fadeInDown"><small>Depois de escolher o plano desejado, clique no botão avançar para visualizar o resumo e completar a assinatura.</small></h1>
                        </div>
                        <div class="col-lg-12">
                            <div class="generic_feature_list pull-right">
                                <h2 class="section-title wow fadeInDown">
                                    <asp:LinkButton runat="server" ID="lbAvancar" CssClass="btn btn-md btn-primary-nutrovet m-t-n-xs" OnClick="btnAvancar_Click">AVANÇAR&nbsp;<i class="fas fa-hand-point-right"></i></asp:LinkButton>
                                </h2>
                            </div>
                            <asp:Button ID="Button1" runat="server" BackColor="#F6F6F6" BorderColor="#F6F6F6" BorderStyle="None" Height="5px" Width="10px" />
                        </div>
                    </div>
                </div>
                <hr />
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>
