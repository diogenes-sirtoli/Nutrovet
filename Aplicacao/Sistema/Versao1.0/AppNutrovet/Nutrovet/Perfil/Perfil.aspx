<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master"
    AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Nutrovet.Perfil.Perfil" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

    <script>
        function BindEvents() {
            $(document).ready(function () {
                $('#FileUpload1').change(function () {
                    var path = $(this).val();
                    if (path != '' && path != null) {
                        var q = path.substring(path.lastIndexOf('\\') + 1);
                        $('#lblFileUpload').html(q);
                    }
                })
            })
        };
        function up(url) {
            document.getElementById('uprl').value = url;
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="">
                <div class="page-title">
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row wrapper border-bottom white-bg page-heading">
                                <div class="col-lg-1">
                                    <asp:HyperLink ID="HyperLink12" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
                                </div>
                                <h2>Perfil do Usuário</h2>
                                <div class="col-lg-4">
                                    <ol class="breadcrumb">
                                        <li>
                                            <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i>&nbsp;Início</asp:HyperLink>
                                        </li>
                                        <li class="active">
                                            <i class="fas fa-portrait"></i><strong>&nbsp;Perfil</strong>
                                        </li>
                                    </ol>
                                </div>
                                <div class="col-lg-12">
                                    <small>Nesta página é possível visualizar os meus dados e realizar a troca e/ou alteração de alguns deles.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="wrapper wrapper-content fadeInRight">

                <div class="row">
                    <div class="col-lg-12 m-b-lg">
                        <div id="vertical-timeline" class="vertical-container light-timeline no-margins">

                            <div class="vertical-timeline-block">
                                <div class="vertical-timeline-icon navy-bg">
                                    <i class="fa fa-camera"></i>
                                </div>
                                <div class="vertical-timeline-content">
                                    <div class="panel panel-default">
                                        <div class="panel-heading"><strong>Selecione o arquivo de imagem para o perfil a partir do seu computador</strong></div>
                                        <div class="panel-body">
                                            <asp:UpdatePanel ID="UpdatePanelUpld" runat="server">
                                                <ContentTemplate>

                                                    <!-- Standar Form -->
                                                    <div class="form-inline">

                                                        <div class="col-lg-2">
                                                            <asp:Image ID="imgFoto" ImageUrl="~/Imagens/user1.png" alt="..." class="img-thumbnail profile_img" runat="server" />
                                                        </div>

                                                        <div class="col-lg-10">
                                                            <div class="input-group">
                                                                <span class="input-group-addon" style="">
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" accept=".png,.jpg,.jpeg" />
                                                                </span>
                                                                <span class="input-group-addon"></span>
                                                            </div>

                                                        </div>

                                                        <span class="pul-right">
                                                            <asp:LinkButton ID="lbEnviaImagem" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbEnviaImagem_Click"><i class="fas fa-cloud-upload-alt"></i> Enviar</asp:LinkButton>
                                                        </span>
                                                    </div>

                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                                    <div class="ibox-content">
                                                        <div class="row">
                                                            <h3>Arquivo Enviado</h3>
                                                            <div class="list-group">

                                                                <asp:Label ID="lblFileUpload" runat="server" class="list-group-item list-group-item-success"></asp:Label>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lbEnviaImagem" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="vertical-timeline-block">
                                <div class="vertical-timeline-icon blue-bg">
                                    <i class="fas fa-portrait"></i>
                                </div>
                                <div class="vertical-timeline-content">

                                    <h2 class="no-margins">
                                        <asp:Label ID="lblUsuario" runat="server" Text=""></asp:Label>
                                    </h2>
                                    <h4>
                                        <asp:Label ID="lblTpPessoa" runat="server" Text="Label"></asp:Label></h4>

                                    <h2>Meus Dados<small> - Consultando e alterando meus dados.</small></h2>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="form-group">
                                                <label id="lblNome" class="col-lg-2 control-label">Nome Completo</label>
                                                <div class="col-lg-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style=""><i class="fas fa-user fa-fw"></i></span>
                                                        <asp:TextBox ID="tbxNomeUsuario" runat="server" CssClass="form-control" placeholder="Nome do Usuário"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label id="lblEmail" class="col-lg-2 control-label">E-mail</label>
                                                <div class="col-lg-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style=""><i class="fa fa-at fa-fw"></i></span>
                                                        <asp:TextBox ID="tbxEmailUsuario" runat="server" CssClass="form-control" placeholder="E-Mail do Usuário"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label id="lblTelefone" class="col-lg-2 control-label">Telefone</label>
                                                <div class="col-lg-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style=""><i class="fa fa-phone-square fa-fw"></i></span>
                                                        <cc1:MEdit ID="tbTelefone" runat="server" Mascara="Telefone" placeholder="(xx) xxxx-xxxx" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Telefone"></cc1:MEdit>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label id="lblCelular" class="col-lg-2 control-label">Celular</label>
                                                <div class="col-lg-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style=""><i class="fas fa-mobile-alt fa-fw"></i></span>
                                                        <cc1:MEdit ID="tbCelular" runat="server" Mascara="Telefone" placeholder="(xx) xxxx-xxxx" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Celular"></cc1:MEdit>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                        <div class="modal-footer col-lg-12">
                                            <div class="btn-group" role="group">
                                                <asp:LinkButton ID="lbSalvarDados" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvar_Click"><i class="far fa-save"></i> Salvar</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="vertical-timeline-block">
                                <div class="vertical-timeline-icon yellow-bg">
                                    <i class="fas fa-lock"></i>
                                </div>
                                <div class="vertical-timeline-content">
                                    <h2>Trocar Senha<small> - Realizando a troca da minha senha.</small></h2>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="form-group">
                                                <label id="lblSenhaAtual" class="col-lg-2 control-label">Senha Atual</label>
                                                <div class="col-lg-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style=""><i class="fas fa-unlock-alt fa-fw"></i></span>
                                                        <asp:TextBox ID="txbSenhaAtual" runat="server" placeholder="Senha Atual" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Senha Atual"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label id="lblNovaSenha" class="col-lg-2 control-label">Nova Senha</label>
                                                <div class="col-lg-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style=""><i class="fas fa-key fa-fw"></i></span>
                                                        <asp:TextBox ID="txbNovaSenha" runat="server" placeholder="Nova Senha" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Nova Senha"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label id="lblConfirmacaoSenha" class="col-lg-2 control-label">Confirmação da Senha</label>
                                                <div class="col-lg-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style=""><i class="fas fa-key fa-fw"></i></span>
                                                        <asp:TextBox ID="txbConfirmacaoSenha" runat="server" placeholder="Confirmação da Senha" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Confirmação Nova Senha"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer col-lg-12">
                                            <div class="btn-group" role="group">
                                                <asp:LinkButton ID="lbSalvaNovaSenha" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbSalvarNovaSenha_Click"><i class="far fa-save"></i> Salvar</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="vertical-timeline-block">
                                <div class="vertical-timeline-icon navy-bg">
                                    <i class="fa fa-comments"></i>
                                </div>
                                <div class="vertical-timeline-content">
                                    <h2>Enviar mensagem NutroVET</h2>
                                    <div class="contact-form">
                                        <div class="form-group">
                                            <asp:TextBox ID="tbxName" runat="server" CssClass="form-control" placeholder="Nome" required="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="tbxEmail" runat="server" CssClass="form-control" placeholder="E-mail" required="true" TextMode="Email"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="tbxAssunto" runat="server" CssClass="form-control" placeholder="Assunto" required="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="tbxMsg" runat="server" CssClass="form-control" Rows="4" placeholder="Mensagem" required="true" TextMode="MultiLine"></asp:TextBox>
                                            <br />
                                        </div>
                                        <p class="text-right to-animate">
                                            <asp:LinkButton ID="lbEmail" runat="server" CssClass="btn btn-primary-nutrovet btn-outline" OnClick="lbEmail_Click"><i class="fas fa-paper-plane"></i>&nbsp;Enviar</asp:LinkButton>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="vertical-timeline-block">
                                <div class="vertical-timeline-icon blue-bg">
                                    <i class="fas fa-handshake"></i>
                                </div>

                                <div class="vertical-timeline-content">
                                    <h2>Meu Plano - <small>Consultando dados do meu plano</small></h2>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="form-group">
                                                <label id="lblPlano" class="col-lg-1 control-label">Tipo</label>
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i class="fas fa-book-open"></i></span>
                                                        <asp:Label ID="lblDescPlano" CssClass="form-control" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <label id="lblPeriodo" class="col-lg-1 control-label">Período</label>
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i class="fas fa-calendar-alt"></i></span>
                                                        <asp:Label ID="lblPeriodoDesc" CssClass="form-control" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <label id="lblSituacao" class="col-lg-1 control-label">Situação</label>
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i class="fas fa-hourglass-end"></i></i></span>
                                                        <asp:Label ID="lblSituacaoDescr" CssClass="form-control" runat="server" Text=""></asp:Label>
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
                NutroVET by <strong>SICORP &copy;<asp:Label ID="lblAno" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </div>
</asp:Content>
