<%@ Page Title="" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="BibliotecaCadastro.aspx.cs" Inherits="Nutrovet.Biblioteca.BibliotecaCadastro" %>

<%@ Register Assembly="MaskEdit" Namespace="MaskEdit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>

        $('body').on('keydown', 'input, select, textarea', function (e) {
            var self = $(this)
                , form = self.parents('form:eq(0)')
                , focusable
                , next
                ;
            if (e.keyCode == 13) {
                focusable = form.find('input,a,select,button,textarea').filter(':visible');
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

    <div class="row">
        <div class="col-lg-12">
            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="col-lg-1">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet m-md" runat="server"><i class="fa fa-bars"></i></asp:HyperLink>
                </div>
                <h2>Biblioteca - Arquivos</h2>
                <div class="col-lg-4">
                    <ol class="breadcrumb">
                        <li>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                        </li>
                        <li class="active">
                            <i class="fas fa-archive"></i><strong>&nbsp;Arquivos - Seções</strong>
                        </li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(BindEvents);
            </script>
            <div class="page-title">
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row wrapper border-bottom white-bg page-heading">
                            <div class="ibox-content">
                                <div class="search-form">
                                    <div class="input-group col-lg-12">
                                        <asp:TextBox ID="tbPesq" runat="server" placeholder="Pesquisar por Seção ou Arquivo" CssClass="form-control input-md bg-muted"></asp:TextBox>
                                        <div class="input-group-btn">
                                            <asp:LinkButton ID="lbPesq" runat="server" CssClass="btn btn-primary-nutrovet"> <i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox col">
                                        <div class="ibox-title">
                                            <span class="pull-left">
                                                <h4><strong>Seções </strong></h4>
                                            </span>
                                        </div>
                                        <div class="panel-group payments-method" id="accordion">
                                            <span class="pull-right">
                                                <asp:Button ID="btnInserir" runat="server" CssClass="btn input-buttontotext fa" Style="width: 30px; height: 30px;" Text="&#xf0fe;" CommandName="Inserir" OnClick="btnInserir_Click"  data-toggle="tooltip" data-placement="top" title="Incluir Arquivo"/>
                                            </span>
                                            <%-- Início Acordeon --%>
                                            <div class="panel panel-default">
                                                <ajaxToolkit:Accordion ID="acSecoes" runat="server" FadeTransitions="false"
                                                    FramesPerSecond="40" RequireOpenedPane="False" SuppressHeaderPostbacks="True"
                                                    TransitionDuration="200" OnItemDataBound="acSecoes_ItemDataBound" SelectedIndex="-1">
                                                    <HeaderTemplate>
                                                        <div class="panel-heading">
                                                            <asp:HyperLink ID="hlcollapse" NavigateUrl="#" runat="server">
                                                                <div class="pull-left">
                                                                    <small><i class="fas fa-folder-open fa-lg" style="color: #00ba97; font-size: 18px"></i></small>&nbsp;&nbsp;
                                                                </div>
                                                                <h4 class="panel-title pull-left">
                                                                    <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("Id") %>' />
                                                                    <asp:Label ID="lblSecao" Style="color: #2c3f51" runat="server" Text='<%# Eval("Nome") %>'></asp:Label>
                                                                </h4>
                                                            </asp:HyperLink>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class=" col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                    <div class="ibox table-responsive">
                                                                        <!-- Repeater Filho -->
                                                                        <asp:Repeater ID="rptArquivos" runat="server" OnItemDataBound="rptArquivos_ItemDataBound" OnItemCommand="rptArquivos_ItemCommand">

                                                                            <HeaderTemplate>
                                                                                <table class="table table-striped projects dataTables-example">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th style="width: 5%">
                                                                                                <asp:Label ID="lblCampo" Style="color: #2c3f51" runat="server" Text="Título"></asp:Label>
                                                                                            </th>
                                                                                            <th style="width: 50%">
                                                                                                <asp:Label ID="Label1" Style="color: #2c3f51" runat="server" Text="Descrição"></asp:Label>
                                                                                            </th>
                                                                                            <th style="width: 20%">
                                                                                                <asp:Label ID="Label2" Style="color: #2c3f51" runat="server" Text="Autor"></asp:Label>
                                                                                            </th>
                                                                                            <th style="width: 5%">
                                                                                                <asp:Label ID="Label3" Style="color: #2c3f51" runat="server" Text="Ano"></asp:Label>
                                                                                            </th>
                                                                                            <th style="width: 10%" colspan="2">
                                                                                                <asp:Label ID="Label4" Style="color: #2c3f51" runat="server" Text="Ano"></asp:Label>
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:HiddenField ID="hfIdArquivo" runat="server" Value='<%# Eval("IdBiblio") %>' />
                                                                                        <asp:HyperLink ID="hlNomeArq" runat="server" Target="_blank" Text='<%# Eval("NomeArq") %>' CssClass="btn btn-link"></asp:HyperLink>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblDescricao" runat="server" Text='<%# Eval("Descricao") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblAutor" runat="server" Text='<%# Eval("Autor") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblAno" runat="server" Text='<%# Eval("Ano") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="lbEditar" runat="server" CssClass="btn btn-primary-nutrovet btn-xs" CommandName="editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdBiblio") %>'> Editar <i class="fas fa-edit"></i></asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="lbExcluir" runat="server" CssClass="btn btn-danger btn-xs" CommandName="excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdBiblio") %>'> <i class="fas fa-trash-alt"></i> Excluir </asp:LinkButton>
                                                                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Deseja Realmente Excluir Este Arquivo?" TargetControlID="lbExcluir" />
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </tbody>
                                                                        </table>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                        <!-- end Repeater Filho -->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolkit:Accordion>
                                            </div>
                                            <%-- Fim Acordeon --%>
                                        </div>
                                    </div>
                                    <div class="modal-footer col-sm-12 col-lg-12">
                                        <div class="btn-group" role="group">
                                            <asp:LinkButton runat="server" ID="lbCancelar" CssClass="btn btn-sm btn-default m-t-n-xs" OnClick="lbCancelar_Click"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--modal edição/alteração-->
            <div id="modalInserirArquivoSecao" runat="server" class="modal-dialog modalControle" style="display: none">
                <!--Content-->
                <div class="modal-content animated fadeIn">
                    <!--Header-->
                    <div class="modal-header modal-header-success">
                        <div class="col-md-9 col-sm-9 col-xs-9">
                            <asp:Label ID="lblTituloModal" class="heading lead" runat="server" Text="Registro"></asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-3">
                            <asp:LinkButton runat="server" ID="lbFecharModal" CssClass="close" data-dismiss="modal"> <i class='fa fa-times-circle'></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body" id="bodymodalInserirArquivoSecao">
                        <div class="row">
                            <label id="lblSecao" class="col-sm-3 control-label">Seção</label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <span class="input-group-addon" style=""><i class="fas fa-folder-open"></i></span>
                                    <asp:DropDownList ID="ddlSecao" runat="server" placeholder="Seção" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Seção"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        <div class="row">
                            <label id="lblTituloArquivo" class="col-sm-3 control-label">Título do Arquivo</label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <span class="input-group-addon" style=""><i class="fas fa-file-signature"></i></span>
                                    <asp:TextBox ID="tbxTituloArquivo" runat="server" placeholder="Título do Arquivo" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Título do Arquivo"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        <div class="row">
                            <label id="lblDescricao" class="col-sm-3 control-label">Descrição</label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <span class="input-group-addon" style=""><i class="fas fa-file-invoice"></i></span>
                                    <asp:TextBox ID="tbxDescricao" runat="server" placeholder="Descrição" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Descrição"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        <div class="row">
                            <label id="lblAutor" class="col-sm-3 control-label">Autor</label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <span class="input-group-addon" style=""><i class="fas fa-id-card"></i></span>
                                    <asp:TextBox ID="tbxAutor" runat="server" placeholder="Autor" CssClass="form-control" data-toggle="tooltip" data-placement="top" title="Autor"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        <div class="row">
                            <label id="lblAnoPublic" class="col-sm-3 control-label">Ano</label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <span class="input-group-addon" style=""><i class="fas fa-calendar-alt"></i></span>
                                    <cc1:MEdit ID="meAnoPublic" class="form-control" runat="server" Mascara="Inteiro" data-toggle="tooltip" data-placement="top" title="Ano"></cc1:MEdit>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group "></div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:UpdatePanel ID="UpdatePanelUpld" runat="server">
                                    <ContentTemplate>
                                        <!-- Standar Form -->
                                        <div class="form-inline">
                                            <div class="col-lg-10">
                                                <div class="input-group">
                                                    <div>
                                                        <asp:FileUpload ID="fuInserirArquivoSecao" runat="server" accept=".pdf" />
                                                    </div>
                                                    <span class="input-group-addon"></span>
                                                    <span class="pul-right">
                                                        <asp:LinkButton ID="lbEnviarArquivo" runat="server" CssClass="btn btn-primary-nutrovet" OnClick="lbEnviarArquivo_Click"><i class="fas fa-cloud-upload-alt"></i> Enviar</asp:LinkButton>
                                                    </span>
                                                </div>

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lbEnviarArquivo" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <asp:HiddenField ID="hfID" runat="server" />
                    </div>

                    <!--Footer-->
                    <div class="modal-footer ">
                        <asp:LinkButton runat="server" ID="btnSalvarInserirArquivoSecao" CssClass="btn btn-sm btn-primary-nutrovet m-t-n-xs" OnClick="btnSalvarInserirArquivoSecao_Click"> <i class='far fa-save'></i> Salvar </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btnFecharInserirArquivoSecao" CssClass="btn btn-sm btn-default m-t-n-xs" data-dismiss="modal"> <i class='fas fa-door-open'></i> Fechar </asp:LinkButton>
                    </div>
                </div>
                <!--/.Content-->
            </div>
            <ajaxToolkit:ModalPopupExtender ID="mdInserirArquivoSecao" runat="server"
                PopupControlID="modalInserirArquivoSecao" BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResize"
                TargetControlID="lblModalInserirArquivoSecao" CancelControlID="btnFecharInserirArquivoSecao">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblModalInserirArquivoSecao" runat="server" Text=""></asp:Label>
            <!-- Modal Inserir Arquivo Seção -->
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
