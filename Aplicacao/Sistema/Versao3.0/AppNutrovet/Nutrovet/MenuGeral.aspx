<%@ Page Title="Nutrologia Veterinária - NutroVET" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true"
    CodeBehind="MenuGeral.aspx.cs" Inherits="Nutrovet.MenuGeral" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script>

        $(document).ready(function () {
            var btnNovaFunc = document.getElementById("btnNovaFunc");

            // Define a ação a ser executada quando o botão for clicado
            btnNovaFunc.addEventListener("click", function (event) {
                event.preventDefault(); // Evita o comportamento padrão de recarregar a página
                $('#modalNovaFunc').iziModal('open');
            });

            $('#modalNovaFunc').iziModal({
                title: '<i class="fa-solid fa-receipt"></i> Receituário Inteligente',
                padding: '20px',
                headerColor: '#2c3f51',
                iconColor: 'red',
                width: '60%',
                overlayColor: 'rgba(0, 0, 0, 0.5)',
                fullscreen: true,
                transitionIn: 'bounceInDown',
                transitionOut: 'fadeOutUp',
                //timeout: 5000,
                pauseOnHover: true,
                timeoutProgressbar: true,
                appendTo: 'body',
                rtl: false,
                bodyOverflow: false,
                openFullscreen: false,
                zindex: 9000
            });
        });
     
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
                    <div class="ibox float-e-margins">
                        <div class="row wrapper border-bottom white-bg page-heading">
                            <div class="col-lg-1">
                                <asp:HyperLink ID="HyperLink12" NavigateUrl="#" class="navbar-minimalize minimalize-styl-2 btn btn-primary-nutrovet " runat="server"><i class="fa fa-bars"></i> </asp:HyperLink>
                            </div>
                            <h2>Página Inicial</h2>
                            <div class="clearfix"></div>
                            <div class="right_col" role="main">
                                <div class="row top_tiles">
                                    <div class="animated flipInX col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-titlePI">
                                                <span class="label label-default pull-right">Cadastrados</span>
                                                <h5><i class="fas fa-utensils"></i>&nbsp;ALIMENTOS</h5>
                                            </div>
                                            <div class="ibox-contentPI">
                                                <h1 class="no-margins">
                                                    <asp:Label ID="lblTotalAlimentos" runat="server" Text="Label"></asp:Label>
                                                </h1>
                                                <div class="stat-percent font-bold text-danger"><i class="fa fa-thumb-tack"></i></div>
                                                <small>Registros</small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="animated flipInX col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-titlePI">
                                                <span class="label label-default pull-right">Elaborados</span>
                                                <h5><i class="fas fa-balance-scale"></i>&nbsp;CARDÁPIOS </h5>
                                            </div>
                                            <div class="ibox-contentPI">
                                                <h1 class="no-margins">
                                                    <asp:Label ID="lblTotalCardapios" runat="server" Text="Label"></asp:Label>
                                                </h1>
                                                <div class="stat-percent font-bold text-danger"><i class="fa fa-thumb-tack"></i></div>
                                                <small>Registros</small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="animated flipInX col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-titlePI">
                                                <span class="label label-default pull-right">Cadastrados</span>
                                                <h5><i class="fas fa-user fa-fw"></i>&nbsp;TUTORES </h5>
                                            </div>
                                            <div class="ibox-contentPI">
                                                <h1 class="no-margins">
                                                    <asp:Label ID="lblTotalTutores" runat="server" Text="Label"></asp:Label>
                                                </h1>
                                                <div class="stat-percent font-bold text-danger"><i class="fa fa-thumb-tack"></i></div>
                                                <small>Registros</small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="animated flipInX col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-titlePI">
                                                <span class="label label-default pull-right">Em tratamento</span>
                                                <h5><i class="fas fa-paw"></i>&nbsp;PACIENTES </h5>
                                            </div>
                                            <div class="ibox-contentPI">
                                                <h1 class="no-margins">
                                                    <asp:Label ID="lblTotalPacientes" runat="server" Text="Label"></asp:Label>
                                                </h1>
                                                <div class="stat-percent font-bold text-danger"><i class="fa fa-thumb-tack"></i></div>
                                                <small>Registros</small>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <section class="col-lg-12">
                                            <div id="carouselMenuGeral" class="carousel slide box" data-ride="carousel">
                                                <ol class="carousel-indicators">
                                                    <li data-target="#carouselMenuGeral" data-slide-to="0" class="active"></li>
                                                    <li data-target="#carouselMenuGeral" data-slide-to="1"></li>
                                                    <li data-target="#carouselMenuGeral" data-slide-to="2"></li>
                                                    <li data-target="#carouselMenuGeral" data-slide-to="3"></li>
                                                    <li data-target="#carouselMenuGeral" data-slide-to="4"></li>
                                                    <li data-target="#carouselMenuGeral" data-slide-to="5"></li>
                                                </ol>
                                                <div class="carousel-inner">
                                                    <div class="item active">
                                                        <div class="row">
                                                            <img alt="image" class="img-responsive" src="Imagens/img1Carousel.png" />
                                                        </div>
                                                    </div>
                                                    <%--<div class="item">
                                                        <div class="row">
                                                            <img alt="image" class="img-responsive" src="Imagens/img2Carousel.png" />
                                                        </div>
                                                    </div>--%>
                                                   <div class="item">
                                                        <div class="row">
                                                            <img alt="image" class="img-responsive" src="Imagens/img3Carousel.png" />
                                                        </div>
                                                    </div>
                                                     <div class="item">
                                                        <div class="row">
                                                            <img alt="image" class="img-responsive" src="Imagens/img4Carousel.png" />
                                                        </div>
                                                    </div>
                                                    <div class="item">
                                                        <div class="row">
                                                            <img alt="image" class="img-responsive" src="Imagens/img5Carousel.png" />
                                                        </div>
                                                    </div>
                                                    <div class="item">
                                                        <div class="row">
                                                            <button id="btnNovaFunc" class="button-carousel">Conheça aqui a nova funcionalidade</button>
                                                            <img alt="image" class="img-responsive" src="Imagens/img6Carousel.png" />
                                                        </div>
                                                    </div>
                                                    <div class="item">
                                                        <div class="row">
                                                            <img alt="image" class="img-responsive" src="Imagens/img7Carousel.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                               <%--<a data-slide="prev" href="#carouselMenuGeral" class="left carousel-control">
                                                    <span class="icon-prev"></span>
                                                </a>
                                                <a data-slide="next" href="#carouselMenuGeral" class="right carousel-control">
                                                    <span class="icon-next"></span>
                                                </a>--%>
                                            </div>
                                        </section>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /Modal nova funcionalidade -->
    <div id="modalNovaFunc" style="display: none">
        <h4>Estamos animados em anunciar uma grande novidade para o NutroVET, o sistema acaba de ficar ainda mais completo!</h4>
        <p><asp:HyperLink ID="hlkEsclarecimento" CssClass="btn btn-primary-nutrovet" NavigateUrl="https://youtu.be/56U9aHzWvwg" runat="server" Target="_blank"><i class="fas fa-video"></i> Esclarecimentos iniciais (vídeo)</asp:HyperLink></p><br />

        <p><strong>
            O Receituário Inteligente chegou e com essa nova funcionalidade é possível criar receitas de suplementos e nutracêuticos de forma automatizada e personalizada de acordo com as necessidades de cada paciente.
        </strong></p><br>
        <p>São três opções para personalizar ainda mais os cuidados com seus pacientes:</p>

        <p><strong>1. Receita para suplementos: </strong></p>
        <p>Crie uma receita complementar para equilibrar a dieta de seu paciente, suplementando de forma automática os nutrientes que faltaram no balanceamento da dieta, basta marcar o que você deseja que faça parte da sua receita, definir a posologia e imprimir, tudo com seu logotipo, cabeçalho, contatos e até sua assinatura digitalizada;</p>
        <p><asp:HyperLink ID="hlkReceitaSuplementos" CssClass="btn btn-primary-nutrovet" NavigateUrl="https://youtu.be/QPC0N3UXxuQ" runat="server" Target="_blank"><i class="fas fa-video"></i> Prescrição de Receita de Suplementos (vídeo)</asp:HyperLink></p><br />
        
        <p><strong>2. Receita para nutracêuticos: </strong></p>
        <p>Elabore uma receita com os nutracêuticos mais utilizados em cada uma das dietas terapêuticas para cães e gatos, sugeridos automaticamente pelo sistema. Está formulando uma dieta para um paciente renal? então vamos prescrever, com poucos cliques, aqueles nutracêuticos mais usados nessa doença... tudo já com a indicação de doses usuais e intervalos de uso, e ainda por cima já calculando automaticamente de acordo com o peso do paciente;</p>
        <p><asp:HyperLink ID="hlkReceitaNutraceuticos" CssClass="btn btn-primary-nutrovet" NavigateUrl="https://youtu.be/HoVUeJjqm4c" runat="server" Target="_blank"><i class="fas fa-video"></i> Prescrição de Receita de Nutracêuticos (vídeo)</asp:HyperLink></p><br />
                
        <p><strong>3. Receita em branco: </strong></p>        
        <p>Deixe sua criatividade fluir e personalize completamente a dieta de seu paciente.</p>
        <p><asp:HyperLink ID="hlkReceitaBranco" CssClass="btn btn-primary-nutrovet" NavigateUrl="https://youtu.be/G5JoAMu8shA" runat="server" Target="_blank"><i class="fas fa-video"></i> Prescrição de Receita em Branco (vídeo)</asp:HyperLink></p><br />

        <p>Todas as receitas ficarão disponíveis para editar,  reimprimir ou reenviar quando você desejar.</p><br>
        <p>Com essas opções, o NutroVET proporciona ainda mais personalização e eficiência no processo de formulação de dietas e prescrições, acreditamos que será ainda mais fácil cuidar da saúde e do bem-estar de seus pacientes.</p><br>
        <div class="bordaTexto">
            <h4><i class="fas fa-cog"></i> &nbsp;&nbsp;<strong>Como vai funcionar</strong></h4>
            <p><strong>No plano completo você terá acesso a essa nova funcionalidade de prescrição de receitas, no entanto todos aqueles que já são assinantes dos planos básico ou intermediário, e ainda os novos usuários que venham a assinar um desses planos do NutroVET receberão acesso grátis por 30 dias para testar e conhecer a funcionalidade.</strong></p><br>
        </div>

        
    </div>
    <!-- /page content -->
    <div id="modalTermosUso" class="modal-dialog modal-lg" runat="server" style="display: none">
        <div class="modal-content rotateInDownLeft">
            <div class="modal-header modal-header-warning">
                <h4 class="modal-title text-center"><i class="fas fa-book-reader"></i>&nbsp;TERMO DE USO</h4>
            </div>
            <div class="modal-body">
                <h4>Agradecemos por usar nossos serviços.</h4>
                <p>
                    Os Serviços serão fornecidos pela <strong>IB Serviços Veterinários LTDA</strong> (CONTRATADA), empresa de direito
                           privado com sede na SQSW 104 Bloco C sala 105, na cidade de Brasília, Distrito Federal, inscrita no CNPJ sob o Nº 31.174.919/0001-25.
                </p>
                <p>Ao se cadastrar no NutroVET, você (CONTRATANTE) está concordando com estes <strong>TERMOS DE USO e condições. Leia-os com atenção.</strong></p>
                <p><strong>Do NutroVET- Objeto e Serviços</strong></p>
                <p>1. O presente contrato tem como objeto a licença de uso individual do software NutroVET, bem como a prestação de serviços de software para a CONTRATANTE.</p>
                <p>2. A prestação de serviços de software compreenderá a disponibilização de um espaço virtual para que você CONTRATANTE usufrua dos recursos lá existentes.</p>
                <p><strong>Das suas Obrigações e Responsabilidade</strong></p>
                <p>3. É preciso que você CONTRANTE siga todas as políticas de uso previstas neste TERMO DE USO, em especial as suas Obrigações.</p>
                <p>4. Cabe a você CONTRATANTE a responsabilidade exclusiva de inserir dados no espaço virtual fornecido pela CONTRATADA, zelando por seu bom uso.</p>
                <p>5. Você CONTRATANTE é a única responsável, civil e criminalmente, por todos os dados inseridos no espaço virtual, e declara, ainda, não ter qualquer impedimento legal para o exercício de suas atividades.</p>
                <p>6. Você CONTRATANTE declara ter ciência de que a CONTRATADA não tem qualquer ingerência nos dados, fatos e/ou informações lançados no espaço virtual, sendo esses de sua inteira responsabilidade.</p>
                <p>7. você CONTRATANTE é a única responsável pelos problemas decorrentes do uso incorreto do software.</p>
                <p>8. você CONTRATANTE se obriga e se compromete a manter sempre atualizados junto à CONTRATADA seu endereço eletrônico (e-mail), para fins de comunicação entre as partes.</p>
                <p>9. Você CONTRATANTE declara, ser legalmente habilitado para exercício profissional da Medicina Veterinário ou Zootecnia, ou ter colado grau em alguma Faculdade de Medicina Veterinária ou Zootecnia, ou ainda, estar regularmente matriculado em alguma Faculdade de Medicina Veterinária ou Zootecnia.</p>
                <p><strong>Das nossas Obrigações e Responsabilidades</strong></p>
                <p>9. Fornecemos nossos Serviços usando um nível comercialmente razoável de capacidade e cuidado e esperamos que você aproveite seu uso deles. Nós CONTRATADA nos comprometemos a manter o espaço virtual em funcionamento, exceto em casos de impedimentos técnicos, atos relacionados à terceiros que impeçam o funcionamento do Serviços, caso fortuito e/ou força maior.</p>
                <p><strong>Do Valor e Forma De Pagamento do Serviço</strong></p>
                <p>10. Nós CONTRATADA disponibilizaremos os serviços previstos no OBJETO do contrato de acordo com o plano de assinatura, mediante a remuneração escolhida.</p>
                <p>Parágrafo primeiro: Na modalidade básica você CONTRATANTE poderá cadastrar o limite máximo de 10 (dez) pacientes no sistema, no entanto não há limites de emissão de dietas ou receitas para cada paciente. Para cadastrar um número maior de pacientes você CONTRATANTE terá que se adequar a um dos nossos demais planos disponíveis.</p>
                <p>Parágrafo segundo: Na modalidade intermediária você CONTRATANTE poderá cadastrar o limite máximo de 20 (vinte) pacientes no sistema, no entanto não há limites de emissão de dietas ou receitas para cada paciente. Para cadastrar um número maior de pacientes você CONTRATANTE terá que se adequar a um dos nossos demais planos disponíveis.</p>
                <p>Parágrafo terceiro: Na modalidade ilimitada você CONTRATANTE poderá cadastrar um número ilimitado de pacientes no sistema, e sem limites de emissão de dietas ou receitas para cada paciente.</p>
                <p>Parágrafo quarto: Efetuada a assinatura e uma vez cancelada, esta somente poderá ser reativada mediante a escolha de novo plano de assinatura, condizente com o número de cadastros já realizados no sistema.</p>
                <p>Parágrafo único: Uma vez cancelada a assinatura, você CONTRATANTE perde o direito por quaisquer descontos ou vantagens obtidos anteriormente.</p>
                <p><strong>Do Prazo de Duração, da Renovação e do Cancelamento/Rescisão dos nossos Serviços</strong></p>
                <p>11. O presente contrato tem a duração de 30 (trinta) dias ou 12 (doze) meses a contar do seu aceite, dependendo da modalidade de pagamento escolhida (mensal ou anual), renovando-se automaticamente após o período de vencimento.</p>
                <p>12. O presente contrato pode ser desfeito/rescindido por qualquer das partes, independentemente de multa, a qualquer tempo.</p>
                <p>13. Todos os nossos planos se renovam automaticamente e você contratante declara estar ciente e de acordo que o cancelamento de cobranças deverá ser providenciados diretamente por você CONTRATANTE ao próprio site, conforme o meio de pagamento por você escolhido.</p>
                <p>14. Esperamos que você utilize os nossos serviços por muito tempo. Contudo, a ausência de pagamento acarretará, automaticamente, o bloqueio da utilização do espaço virtual. Não efetuado o pagamento no prazo de 15 (quinze) dias a contar da remessa de correspondência eletrônica para o e-mail por você CONTRATANTE informado, o presente contrato será considerado rescindido de pleno direito, autorizando a CONTRATADA a excluir a sua conta e todos os dados nela contidos.</p>
                <p>15. Nós CONTRATADA podemos modificar este Termos de Uso e os serviços prestados, tais como funcionalidades do serviço, layout, etc., independentemente de prévio aviso, condição essa que você CONTRATANTE declara aceitar, não cabendo qualquer tipo de ressarcimento decorrente dessas alterações a você CONTRATANTE. Se você CONTRANTE não concordar com os termos alterados do Serviço, deve descontinuar o seu uso, observado o previsto no que toca à cancelamento do serviço e forma de pagamento escolhida.</p>
                <p>16. Em caso de conflito entre estes termos e os termos adicionais, os termos adicionais prevalecerão com relação a esse conflito.</p>
                <p>17. Estes termos regem a relação entre você CONTRANTE e nós CONTRATADA. Eles não criam quaisquer direitos para terceiros.</p>
                <p>18. Caso você não cumpra estes termos e nós não tomemos providências imediatas, isso não significa que estamos renunciando a quaisquer direitos que possamos ter (como tomar providências futuras).</p>
                <p><strong>Do Foro</strong></p>
                <p>19. Para resolver quaisquer controvérsias decorrentes do presente contrato, as partes elegem o Foro Central da comarca de Brasília - DF, com renúncia expressa a outros, por mais privilegiados que sejam.</p>
            </div>
            <div class="modal-footer">
                <asp:LinkButton runat="server" ID="lbFecharTermoUso" CssClass="btn btn-sm btn-white pull-right  m-b" data-dismiss="modal"><i class="fas fa-door-open"></i> Fechar </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lblOcultarTermoUso" CssClass="btn btn-sm btn-white pull-right  m-b" OnClick="btnOcultarTermoUso_Click"><i class="far fa-check-square"></i> Aceito </asp:LinkButton>
            </div>
        </div>
    </div>
    <ajaxToolkit:ModalPopupExtender ID="mdlTermoUso" runat="server"
        PopupControlID="modalTermosUso" BackgroundCssClass="modalBackground"
        RepositionMode="RepositionOnWindowResize"
        TargetControlID="lblTermosUso">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="lblTermosUso" runat="server" Text=""></asp:Label>
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
