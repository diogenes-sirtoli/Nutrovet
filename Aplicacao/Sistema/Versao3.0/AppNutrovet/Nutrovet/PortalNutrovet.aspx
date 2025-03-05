<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PortalNutrovet.aspx.cs" Inherits="Nutrovet.PortalNutrovet" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Nutrovet - Sistema Veterinário de Nutrologia Animal">
    <title><%: Page.Title %>Nutrologia Veterinária - NutroVET</title>
    <asp:PlaceHolder runat="server">
        <link href="<%=ResolveClientUrl("~/CSS/portal/bootstrap.min.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/font-awesome/css/fontawesome.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/font-awesome/css/all.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/font-awesome/css/brands.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/font-awesome/css/regular.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/font-awesome/css/solid.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/portal/animate.min.css")%>" rel="stylesheet" />

        <link href="<%=ResolveClientUrl("~/CSS/portal/style_p.css")%>" rel="stylesheet" />
        <link href="<%=ResolveClientUrl("~/CSS/plugins/iziModal/iziModal.min.css")%>" rel="stylesheet" />
        <script src="<%=ResolveClientUrl("~/Scripts/portal/jquery.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/bootstrap.min.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/mousescroll.js")%>" type="text/javascript"></script>


        <script src="<%=ResolveClientUrl("~/Scripts/portal/jquery.isotope.min.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/jquery.inview.min.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveClientUrl("~/Scripts/portal/wow.min.js")%>" type="text/javascript"></script>

        <script src="<%=ResolveClientUrl("~/Scripts/plugins/iziModal/iziModal.min.js")%>" type="text/javascript"></script>

        <link href="<%=ResolveClientUrl("~/Imagens/favicon.ico")%>" rel="shortcut icon" type="image/x-icon" />
    </asp:PlaceHolder>
</head>
<body id="home" style="max-width: auto">
    <header id="header">
        <nav id="main-nav" class="navbar navbar-default navbar-fixed-top" role="navigation">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span class="sr-only">O <strong>NutroVET</strong> é um sistema on-line desenvolvido para o suporte dos profissionais que trabalham com alimentação natural de cães e gatos. É uma ferramenta que vai lhe auxiliar na elaboração de dietas e na prescrição de nutracêuticos, se tornando uma peça fundamental no seu trabalho.</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <img src="Imagens/logo.png" alt="logo" onclick="location.href='PortalNutrovet.aspx'">
                </div>
                <div class="collapse navbar-collapse navbar-right" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav">
                        <li><a data-toggle="collapse" data-target=".in" href="https://www.nutrovet.com.br"><i class="fa fa-home"></i>&nbsp;Início</a></li>
                        <li><a data-toggle="collapse" data-target=".in" href="https://www.nutrovet.com.br"><i class="fas fa-clipboard-list"></i>&nbsp;Funcionalidades</a></li>
                        <li><a data-toggle="collapse" data-target=".in" href="https://www.nutrovet.com.br"><i class="fas fa-desktop"></i>&nbsp;O Sistema</a></li>
                        <li><a data-toggle="collapse" data-target=".in" href="https://www.nutrovet.com.br"><i class="fa fa-envelope"></i>&nbsp;Contato</a></li>
                        <li><a data-toggle="collapse" data-target=".in" href="https://app.nutrovet.com.br/Plano/EscolherAssinatura.aspx"><i class="fas fa-handshake"></i>&nbsp;Planos</a></li>
                        <li><a data-toggle="collapse" data-target=".in" href="https://app.nutrovet.com.br/Login.aspx"><i class="fab fa-expeditedssl"></i>&nbsp;Login</a></li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    <section id="hero-banner">
        <img src="Imagens/banner/nut970.png" id="centralizar" alt="NutroVET" style="width: 99%" />
    </section>
    <!--/#main-slider-->
    <div id="modal-demo" class="iziModal">
        Entre os dias 19 e 21 de abril de 2021 o<br />
        Sistema NUTROVET irá passar por momentos<br />
        de instabilidade e indisponibilidade,<br />
        devido a manutenção que será realizada<br />
        nos servidores.
    </div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <section id="features">
                    <div class="container">
                        <div class="section-header">
                            <h3 class="section-title wow fadeInDown">Funcionalidades</h3>
                        </div>
                        <div class="features">
                            <div class="row">
                                <div class="col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                                    <div class="media service-box">
                                        <div class="pull-left">
                                            <i class="fas fa-door-closed"></i>
                                        </div>
                                        <div class="media-body">
                                            <h4 class="media-heading">Uso Exclusivo</h4>
                                            <p>O NutroVET é de uso exclusivo para profissionais de Medicina Veterinária e Zootecnia, ou ainda para estudantes dessas duas áreas, conforme disposições da Lei 5.517, de 23 de outubro de 1968.</p>
                                            <button type="button" id="btnUsoExclusivo" class="btn btn-primaryS pull-right m-t-n-xs">
                                                Mais...
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                                    <div class="media service-box">
                                        <div class="pull-left">
                                            <i class="fas fa-utensils fa-fw"></i>
                                        </div>
                                        <div class="media-body">
                                            <h4 class="media-heading">Biblioteca de Alimentos</h4>
                                            <p>O sistema conta com mais de 3.000 alimentos cadastrados, sendo que cada um deles conta com mais de 80 nutrientes.</p>
                                            <button type="button" id="btnBibliotecaAlimentos" class="btn btn-primaryS pull-right m-t-n-xs">
                                                Mais...
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                                    <div class="media service-box">
                                        <div class="pull-left">
                                            <i class="fas fa-balance-scale fa-fw"></i>
                                        </div>
                                        <div class="media-body">
                                            <h4 class="media-heading">Sugestão de Dietas</h4>
                                            <p>Ao formular, conte com sugestões de distribuição calórica para dietas de manutenção ou dietas terapêuticas, tudo de forma intuitiva e dinâmica.</p>
                                            <button type="button" id="btnSugestaoDietas" class="btn btn-primaryS pull-right m-t-n-xs">
                                                Mais...
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                                    <div class="media service-box">
                                        <div class="pull-left">
                                            <i class="fas fa-flask fa-fw"></i>
                                        </div>
                                        <div class="media-body">
                                            <h4 class="media-heading">Formulação de Dietas</h4>
                                            <p>Com o NutroVET a tarefa de formular uma dieta natural ficou muito mais fácil, chega de milhares de cálculos a todo o momento.</p>
                                            <button type="button" id="btnFormulacaoDietas" class="btn btn-primaryS pull-right m-t-n-xs">
                                                Mais...
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                                    <div class="media service-box">
                                        <div class="pull-left">
                                            <i class="fas fa-filter fa-fw"></i>
                                        </div>
                                        <div class="media-body">
                                            <h4 class="media-heading">Biblioteca de Nutracêuticos</h4>
                                            <p>Uma lista extensa de nutracêuticos divididos para uso em cães ou gatos, com doses mínimas e máximas , o intervalo e a unidade.</p>
                                            <button type="button" id="btnBibliotecaNutraceuticos" class="btn btn-primaryS pull-right m-t-n-xs">
                                                Mais...
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                                    <div class="media service-box">
                                        <div class="pull-left">
                                            <i class="fas fa-layer-group fa-fw"></i>
                                        </div>
                                        <div class="media-body">
                                            <h4 class="media-heading">Diferentes Categorias</h4>
                                            <p>Formule de acordo com as exigências da categoria que precisar, cães ou gatos, adultos ou filhotes, gestação ou lactação.</p>
                                            <button type="button" id="btnDiferentesCategorias" class="btn btn-primaryS pull-right m-t-n-xs">
                                                Mais...
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                                    <div class="media service-box">
                                        <div class="pull-left">
                                            <i class="fab fa-leanpub fa-fw"></i>
                                        </div>
                                        <div class="media-body">
                                            <h4 class="media-heading">Requerimentos Nutricionais</h4>
                                            <p>O NutroVET disponibiliza as exigências do NRC, FEDIAF e AAFCO, fique a vontade para escolher qual você prefere.</p>
                                            <button type="button" id="btnRequerimentosNutricionais" class="btn btn-primaryS pull-right m-t-n-xs">
                                                Mais...
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                                    <div class="media service-box">
                                        <div class="pull-left">
                                            <i class="fas fa-file-medical fa-fw"></i>
                                        </div>
                                        <div class="media-body">
                                            <h4 class="media-heading">Biblioteca de Artigos</h4>
                                            <p>Diversos artigos e trabalhos científicos organizados por assunto para você utilizar ou enviar aos tutores para solidificar sua conduta.</p>
                                            <button type="button" id="btnReceitasNutraceuticos" class="btn btn-primaryS pull-right m-t-n-xs">
                                                Mais...
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                                    <div class="media service-box">
                                        <div class="pull-left">
                                            <i class="fas fa-dna"></i>
                                        </div>
                                        <div class="media-body">
                                            <h4 class="media-heading">Raças e Idades</h4>
                                            <p>O NutroVET considera a raça do paciente para decidir se ele está na fase de crescimento inicial, crescimento final ou se já é adulto.</p>
                                            <button type="button" id="btnRacasIdades" class="btn btn-primaryS pull-right m-t-n-xs">
                                                Mais...
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!--Modais das Funcionalidades -->
                <div class="modal inmodal" id="usoExclusivo" tabindex="-1" role="dialog" aria-hidden="true">
                    <p>Ao selecionar o tipo de dieta que você deseja formular, o sistema consulta um banco de sugestões de distribuição calórica e lhe apresenta um gráfico com o alvo a ser perseguido, considerando carboidratos, proteínas e gorduras. O gráfico é apenas sugestivo e você pode formular como achar melhor, a ideia é meramente auxiliar e dar agilidade ao processo de formulação. Todas as sugestões são frutos de consultoria do site Cachorro Verde.</p>
                </div>
                <div class="modal inmodal" id="bibliotecaAlimentos" tabindex="-1" role="dialog" aria-hidden="true">
                    <p>Atualmente estão disponibilizadas as bibliotecas TACO, Tucunduva, USDA e diversos produtos industrializados como rações e suplementos. Além disso, nossa biblioteca está sempre evoluindo e você pode compartilhar alimentos da sua biblioteca particular com todos os usuários do NutroVET, tudo mediante moderação, para garantir a segurança no cadastro. E para deixar essa biblioteca mais completa ainda, cada alimento lista mais de 80 nutrientes.</p>
                </div>
                <div class="modal inmodal" id="sugestaoDietas" tabindex="-1" role="dialog" aria-hidden="true">
                    <p>Ao selecionar o tipo de dieta que você deseja formular, o sistema consulta um banco de sugestões de dietas e lhe apresenta um gráfico com o alvo a ser perseguido, considerando carboidratos, proteínas e gorduras. O gráfico é meramente sugestivo e você pode formular como achar melhor, a ideia é meramente auxiliar e dar agilidade ao processo de formulação. Todas as sugestões de dietas são frutos de consultoria do site Cachorro Verde.</p>
                </div>
                <div class="modal inmodal" id="formulacaoDietas" tabindex="-1" role="dialog" aria-hidden="true">
                    <p>Basta selecionar o tipo de dieta que pretende elaborar, incluir os alimentos desejados e definir as quantidades, depois basta observar os gráficos de sugestão da dieta e checar se estão de acordo com o gráfico da dieta que está sendo formulada. Depois, conferir energia metabolizável, fibras e umidade, e por fim conferir o detalhamento dos nutrientes, para verificar se sobra ou falta algum nutriente, lembrando que estamos trabalhando com mais de 80 nutrientes para atender as exigências nutricionais dos PETs de forma ampla.</p>
                </div>
                <div class="modal inmodal" id="bibliotecaNutraceuticos" tabindex="-1" role="dialog" aria-hidden="true">
                    <p>Na correria da formulação, é comum que o cérebro acaba se confundindo ou mesmo esquecendo alguns detalhes, principalmente as coisas mais minuciosas. Pensando nisso criamos uma tabela de nutracêuticos que vão servir como apoio para sua formulação, e em breve essa funcionalidade vai ser evoluída para um verdadeiro sistema de emissão de receitas de forma inteligente, levando em consideração o peso do seu paciente e automatizando essa tarefa também.</p>
                </div>
                <div class="modal inmodal" id="diferentesCategorias" tabindex="-1" role="dialog" aria-hidden="true">
                    <p>Formular dietas é uma atividade muito séria e deve ser encarada com muito profissionalismo. Dessa forma, o sistema automaticamente busca nas principais fontes internacionais de nutrição animal a exigência nutricional recomendada para a categoria do paciente que foi selecionado. Isso deixa sua dieta muito mais precisa e adequada ao mundo real.</p>
                </div>
                <div class="modal inmodal" id="requerimentosNutricionais" tabindex="-1" role="dialog" aria-hidden="true">
                    <p>Está no meio da dieta e ficou em dúvida se alguma das exigências nutricionais seria diferente se o organismo internacional fosse outro, diferente do que você está utilizando? Ok, basta selecionar outro e as exigências automaticamente simplesmente serão ajustadas, isso lhe dará mais segurança para formular, e mais do que isso, irá lhe conferir liberdade de escolha.</p>
                </div>
                <div class="modal inmodal" id="bibliotecaArtigos" tabindex="-1" role="dialog" aria-hidden="true">
                    <p>Muitas vezes enfrentamos situações em que o tutor questiona uma determinada conduta e vem aquela ideia de “Vou lhe enviar um artigo sobre isso…” e depois vem aquela perda de tempo para encontrar o trabalho… Bom, agora de forma rápida e prática, você pode acessar o NutroVET do seu celular mesmo e já enviar o trabalho para o WhatsApp ou mesmo e-mail do seu cliente imediatamente.</p>
                </div>
                <div class="modal inmodal" id="racasIdades" tabindex="-1" role="dialog" aria-hidden="true">
                    <p>Sabemos que um Yorkshire fica adulto muito antes do que um Dog Alemão, certo? Então o sistema estabelece a idade em que cada raça passa para a próxima fase de crescimento, e isso é fundamental para que a exigência nutricional correta seja trazida da tabela de requerimentos nutricionais. Isso te deixa mais seguro para fazer seu trabalho.</p>
                </div>
                <!--/#modais funcionalidades-->
                <section id="about" style="width: 99%">
                    <div class="container">
                        <div class="section-header">
                            <h2 class="section-title wow fadeInDown">O Sistema</h2>
                        </div>
                        <div class="col-lg-6 wow fadeInLeft">
                            <img src="Imagens/about.png" alt="" class="img-responsive">
                        </div>
                        <div class="col-lg-6 wow fadeInRight">
                            <br />
                            <br />
                            <p style="font-size: 16px; text-align: justify;"><strong>O NutroVET </strong>é um sistema on-line desenvolvido para o suporte dos profissionais que trabalham com alimentação natural de cães e gatos. É uma ferramenta que vai lhe auxiliar na elaboração de dietas e na prescrição de nutracêuticos, se tornando uma peça fundamental no seu trabalho.</p>
                            <p style="font-size: 16px; text-align: justify;">Trata-se de uma plataforma que reúne as principais diretrizes alimentares preconizadas pelos órgãos internacionalmente reconhecidos quando se fala em nutrição animal. Com o NutroVET você poderá formular dietas para cães ou gatos, animais saudáveis ou enfermos, adultos, filhotes, fêmeas gestantes ou ainda fêmeas lactantes.</p>
                            <p style="font-size: 16px; text-align: justify;"></p>
                            <br />
                        </div>
                        <div class="container">
                            <h5>Conheça as enfermidades para cães e gatos que você receberá sugetão de distribuição calórica: </h5>
                            <table class="table table-striped table-hover">
                                <thead>
                                    <tr>
                                        <th style="width: 33%"></th>
                                        <th style="width: 33%"></th>
                                        <th style="width: 33%"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td><span class="line">Manutenção</span></td>
                                        <td><span class="line">Obesidade</span></td>
                                        <td><span class="line">Diabetes</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">Cardiopatia</span></td>
                                        <td><span class="line">Câncer</span></td>
                                        <td><span class="line">Pancreatite</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">Linfangiectasia</span></td>
                                        <td><span class="line">Hipotireoidismo</span></td>
                                        <td><span class="line">Hiperadreno</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">Hepatopatia crônica</span></td>
                                        <td><span class="line">Shunt</span></td>
                                        <td><span class="line">IPE</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">DRC I</span></td>
                                        <td><span class="line">DRC II</span></td>
                                        <td><span class="line">DRC III</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">DRC IV</span></td>
                                        <td><span class="line">Gastrite crônica</span></td>
                                        <td><span class="line">Colite</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">Estruvita (dissolução)</span></td>
                                        <td><span class="line">Estruvita (prevenção)</span></td>
                                        <td><span class="line">Sílica</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">Cistina</span></td>
                                        <td><span class="line">Urato e Xantina</span></td>
                                        <td><span class="line">Oxalato</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">Estruvita + Oxalato</span></td>
                                        <td><span class="line">Cetogênica 4:1</span></td>
                                        <td><span class="line">Cetogênica 2:1</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">Cetogênica 1:1</span></td>
                                        <td><span class="line">Cetogênica 0,5:1</span></td>
                                        <td><span class="line">Câncer (avançado) + DRC (inicial)</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">Câncer + DRC (moderado)</span></td>
                                        <td><span class="line">Câncer + DRC (adiantado)</span></td>
                                        <td><span class="line">Dermatopatia (Dieta de Eliminação)</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">Disfunção Cognitiva</span></td>
                                        <td><span class="line">Dislipidemia</span></td>
                                        <td><span class="line">Artropatia</span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="line">Dieta de Baixo Resíduo</span></td>
                                        <td><span class="line">Dieta de Estabilização para Disbiose</span></td>
                                        <td><span class="line">Sugestões para caninos e felinos</span></td>
                                    </tr>
                                </tbody>
                            </table>
                            <p class="text-right to-animate"><a href="./Plano/EscolherAssinatura.aspx" class="btn btn-primaryS btn-outline"><i class="fas fa-handshake"></i>&nbsp;Assine Agora</a></p>
                            <!--<p class="text-right to-animate"><a href="#disponivel" class="btn btn-primaryS btn-outline" data-toggle="modal" data-target="#disponivel"><i class="fas fa-handshake"></i>&nbsp;Assine Agora</a></p>-->
                            <div class="modal inmodal" id="disponivel" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content animated flipInY">
                                        <div class="modal-header modal-header-warning">
                                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                            <h4 class="modal-title"><i class="fas fa-info-circle"></i>&nbsp;Aviso</h4>
                                        </div>
                                        <div class="modal-body text-center">
                                            <p><i class="fas fa-calendar-alt"></i>Assinatura disponível no dia 29/04/2019 <i class="fas fa-calendar-alt"></i></p>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-sm btn-white pull-right m-t-n-xs" data-dismiss="modal"><i class='fas fa-door-open'></i>&nbsp;Fechar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <section id="business-stats" style="background-color: #ffffff; width: 99%">
                    <div class="container">
                        <div class="section-header">
                            <h2 class="section-title wow fadeInDown">Conheça um pouco mais</h2>
                        </div>
                        <div class="row text-center">
                            <div class="ibox float-e-margins" id="carrosselPortal">
                                <div class=" item embed-responsive embed-responsive-16by9">
                                    <iframe width="950" height="534" src="https://www.youtube.com/embed/dgPjUhjXOpI" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <%--<section id="contact-us1" style="background: #9f9c9c;">
                    <div class="container">
                        <div class="section-header">
                            <h2 class="section-title wow fadeInDown">Entre em Contato</h2>
                        </div>
                    </div>
                    <div class="container contact-info">
                        <div class="col-sm-offset-2 col-sm-8">
                            <!-- Start Error box -->
                            <div runat="server" id="alertas">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                <h4>
                                    <asp:Label ID="lblAlerta" runat="server" Text="Teste de Alerta"></asp:Label>
                                </h4>
                            </div>
                            <!-- End Error box -->
                            <br />
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
                                    <asp:TextBox ID="tbxMsg" runat="server" CssClass="form-control" Rows="8" placeholder="Mensagem" required="true" TextMode="MultiLine"></asp:TextBox>
                                    <br />
                                </div>
                                <p class="text-right to-animate">
                                    <asp:LinkButton ID="lbEmail" runat="server" CssClass="btn btn-primaryS btn-outline" OnClick="lbEmail_Click"><i class="fas fa-paper-plane"></i>&nbsp;Enviar</asp:LinkButton>
                                </p>
                            </div>
                        </div>
                    </div>
                </section>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <footer id="footer" style="width: 99%">
        <div id="contato" class="section-header">
            <h2 class="section-title wow fadeInDown" style="color: white">Entre em Contato</h2>
            <p>
                Se quiser falar com a NutroVET, mande um e-mail para 
                                    <a href="mailto:contato@nutrovet.com.br?subject=Contato%20atrav%C3%A9s%20portal&body=Informe%20sua%20sugest%C3%A3o,%20d%C3%BAvida%20ou%20solicita%C3%A7%C3%A3o">contato@nutrovet.com.br</a>
                com sua sugestão, dúvida ou solicitação
            </p>
        </div>
        <div class="contact-form text-center">
            <br>
            <i class="fas fa-address-card"></i>&nbsp;CNPJ 31.174.919/0001-25<br>
            <i class="fas fa-map-marked-alt"></i>&nbsp;SQSW 104 Bloco C Sala 105<br>
            <i class="fas fa-at"></i>&nbsp;contato@nutrovet.com.br<br>
            <i class="fab fa-whatsapp"></i>&nbsp;+55 61 98136-6230
                    <h4><strong><i class="fas fa-copyright"></i>&nbsp;2018 NutroVET</strong></h4>
        </div>
        <div class="section-header">
            <h2 class="section-title wow fadeInDown"></h2>
        </div>
        <div class="col-12">
            <div class="col-lg-offset-2 col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                <div class="media-body">
                    <div class="footer-widget">
                        <div class="footer-social-info">
                            |&nbsp;&nbsp;<a href="https://www.youtube.com/channel/UCPk1NVPuAgVPjf6eQOI5qeg?view_as=public" target="_blank"><i class="fab fa-youtube"></i></a>&nbsp;
                                    |&nbsp;&nbsp;<a href="https://www.facebook.com/nutrovetonline/" target="_blank" class="facebook"><i class="fab fa-facebook"></i></a>&nbsp;
                                    |&nbsp;<a href="https://www.instagram.com/nutrovetonline/" target="_blank" class="instagram"><i class="fab fa-instagram"></i></a>&nbsp;|
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-offset-2 col-lg-4 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                <div class="media-body">
                    NutroVET by 
                            <strong>RD Sistemas &copy;
                            <asp:Label ID="lblAno" runat="server" Text=""></asp:Label>
                            </strong>
                </div>
            </div>
        </div>


    </footer>
    <script>
        $(document).ready(function () {
            $('#btnUsoExclusivo').on('click', function () { $('#usoExclusivo').iziModal('open'); });
            $('#btnBibliotecaAlimentos').on('click', function () { $('#bibliotecaAlimentos').iziModal('open'); });
            $('#btnSugestaoDietas').on('click', function () { $('#sugestaoDietas').iziModal('open'); });
            $('#btnFormulacaoDietas').on('click', function () { $('#formulacaoDietas').iziModal('open'); });
            $('#btnBibliotecaNutraceuticos').on('click', function () { $('#bibliotecaNutraceuticos').iziModal('open'); });
            $('#btnDiferentesCategorias').on('click', function () { $('#diferentesCategorias').iziModal('open'); });
            $('#btnRequerimentosNutricionais').on('click', function () { $('#requerimentosNutricionais').iziModal('open'); });
            $('#btnReceitasNutraceuticos').on('click', function () { $('#bibliotecaArtigos').iziModal('open'); });
            $('#btnRacasIdades').on('click', function () { $('#racasIdades').iziModal('open'); });
            $('#usoExclusivo').iziModal({ title: '<i class="fas fa-door-closed fa-fw"></i>&nbsp;Uso Exclusivo', padding: '20px', headerColor: '#2c3f51', iconColor: 'red', width: '60%', overlayColor: 'rgba(0, 0, 0, 0.5)', fullscreen: true, transitionIn: 'bounceInDown', transitionOut: 'fadeOutUp', pauseOnHover: true, timeoutProgressbar: true, appendTo: 'body', rtl: false, bodyOverflow: false, openFullscreen: false, zindex: 9000 });
            $('#bibliotecaAlimentos').iziModal({ title: '<i class="fas fa-utensils fa-fw"></i>&nbsp;Biblioteca de Alimentos', padding: '20px', headerColor: '#2c3f51', iconColor: 'red', width: '60%', overlayColor: 'rgba(0, 0, 0, 0.5)', fullscreen: true, transitionIn: 'bounceInDown', transitionOut: 'fadeOutUp', pauseOnHover: true, timeoutProgressbar: true, appendTo: 'body', rtl: false, bodyOverflow: false, openFullscreen: false, zindex: 9000 });
            $('#sugestaoDietas').iziModal({ title: '<i class="fas fa-balance-scale fa-fw"></i>&nbsp;Sugestão de Dietas', padding: '20px', headerColor: '#2c3f51', iconColor: 'red', width: '60%', overlayColor: 'rgba(0, 0, 0, 0.5)', fullscreen: true, transitionIn: 'bounceInDown', transitionOut: 'fadeOutUp', pauseOnHover: true, timeoutProgressbar: true, appendTo: 'body', rtl: false, bodyOverflow: false, openFullscreen: false, zindex: 9000 });
            $('#formulacaoDietas').iziModal({ title: '<i class="fas fa-flask fa-fw"></i>&nbsp;Formulação de Dietas', padding: '20px', headerColor: '#2c3f51', iconColor: 'red', width: '60%', overlayColor: 'rgba(0, 0, 0, 0.5)', fullscreen: true, transitionIn: 'bounceInDown', transitionOut: 'fadeOutUp', pauseOnHover: true, timeoutProgressbar: true, appendTo: 'body', rtl: false, bodyOverflow: false, openFullscreen: false, zindex: 9000 });
            $('#bibliotecaNutraceuticos').iziModal({ title: '<i class="fas fa-filter fa-fw"></i>&nbsp;Indicação de Alimentos', padding: '20px', headerColor: '#2c3f51', iconColor: 'red', width: '60%', overlayColor: 'rgba(0, 0, 0, 0.5)', fullscreen: true, transitionIn: 'bounceInDown', transitionOut: 'fadeOutUp', pauseOnHover: true, timeoutProgressbar: true, appendTo: 'body', rtl: false, bodyOverflow: false, openFullscreen: false, zindex: 9000 });
            $('#diferentesCategorias').iziModal({ title: '<i class="fas fa-layer-group fa-fw"></i>&nbsp;Diferentes Categorias', padding: '20px', headerColor: '#2c3f51', iconColor: 'red', width: '60%', overlayColor: 'rgba(0, 0, 0, 0.5)', fullscreen: true, transitionIn: 'bounceInDown', transitionOut: 'fadeOutUp', pauseOnHover: true, timeoutProgressbar: true, appendTo: 'body', rtl: false, bodyOverflow: false, openFullscreen: false, zindex: 9000 });
            $('#requerimentosNutricionais').iziModal({ title: '<i class="fab fa-leanpub fa-fw"></i>&nbsp;Requerimentos Nutricionais', padding: '20px', headerColor: '#2c3f51', iconColor: 'red', width: '60%', overlayColor: 'rgba(0, 0, 0, 0.5)', fullscreen: true, transitionIn: 'bounceInDown', transitionOut: 'fadeOutUp', pauseOnHover: true, timeoutProgressbar: true, appendTo: 'body', rtl: false, bodyOverflow: false, openFullscreen: false, zindex: 9000 });
            $('#bibliotecaArtigos').iziModal({ title: '<i class="fas fa-file-medical fa-fw"></i>&nbsp;Receitas e Nutracêuticos', padding: '20px', headerColor: '#2c3f51', iconColor: 'red', width: '60%', overlayColor: 'rgba(0, 0, 0, 0.5)', fullscreen: true, transitionIn: 'bounceInDown', transitionOut: 'fadeOutUp', pauseOnHover: true, timeoutProgressbar: true, appendTo: 'body', rtl: false, bodyOverflow: false, openFullscreen: false, zindex: 9000 });
            $('#racasIdades').iziModal({ title: '<i class="fas fa-dna fa-fw"></i>&nbsp;Raças e Idades', padding: '20px', headerColor: '#2c3f51', iconColor: 'red', width: '60%', overlayColor: 'rgba(0, 0, 0, 0.5)', fullscreen: true, transitionIn: 'bounceInDown', transitionOut: 'fadeOutUp', timeout: 1000000, pauseOnHover: true, timeoutProgressbar: true, appendTo: 'body', rtl: false, bodyOverflow: false, openFullscreen: false, zindex: 9000 });
            $('#modal-demo').iziModal('close');
        });


        window.onload = function () {
            // Obtém a query string da URL
            const params = new URLSearchParams(window.location.search);

            // Verifica se o parâmetro scrollTo está presente
            if (params.has('scrollTo')) {
                const anchorId = params.get('scrollTo');

                // Verifica se o elemento com o ID especificado existe na página
                const element = document.getElementById(anchorId);
                if (element) {
                    // Rola para o elemento com o ID especificado
                    element.scrollIntoView({ behavior: 'smooth' });
                }
            }
        };

        $("#modal-demo").iziModal({
            title: '<i class="fas fa-exclamation-circle"></i>  NUTROVET AVISA',
            subtitle: 'Manutenção do Servidor',
            headerColor: '#FF8C00',
            background: null,
            theme: '',  // light
            icon: null,
            iconText: null,
            iconColor: 'black',
            rtl: false,
            width: 400,
            top: null,
            bottom: null,
            borderBottom: true,
            padding: 15,
            radius: 4,
            zindex: 999,
            iframe: false,
            iframeHeight: 400,
            iframeURL: null,
            focusInput: true,
            group: '',
            loop: false,
            arrowKeys: true,
            navigateCaption: true,
            navigateArrows: true, // Boolean, 'closeToModal', 'closeScreenEdge'
            history: false,
            restoreDefaultContent: false,
            autoOpen: 0, // Boolean, Number
            bodyOverflow: false,
            fullscreen: false,
            openFullscreen: false,
            closeOnEscape: true,
            closeButton: true,
            appendTo: 'body', // or false
            appendToOverlay: 'body', // or false
            overlay: true,
            overlayClose: true,
            overlayColor: 'rgba(0, 0, 0, 0.8)',
            timeout: 10000,
            timeoutProgressbar: true,
            pauseOnHover: true,
            timeoutProgressbarColor: 'rgba(0,0,0,0.5)',
            transitionIn: 'comingIn',   // comingIn, bounceInDown, bounceInUp, fadeInDown, fadeInUp, fadeInLeft, fadeInRight, flipInX
            transitionOut: 'comingOut', // comingOut, bounceOutDown, bounceOutUp, fadeOutDown, fadeOutUp, , fadeOutLeft, fadeOutRight, flipOutX
            transitionInOverlay: 'fadeIn',
            transitionOutOverlay: 'fadeOut',
        });
    </script>
</body>
</html>
