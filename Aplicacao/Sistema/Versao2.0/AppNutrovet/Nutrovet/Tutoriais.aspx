<%@ Page Title="Nutrologia Veterinária - NutroVET>" Language="C#" MasterPageFile="~/AppMenuGeral.Master" AutoEventWireup="true" CodeBehind="Tutoriais.aspx.cs" Inherits="Nutrovet.Tutoriais" %>

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
                        <h2>Tutoriais</h2>
                        <div class="col-lg-4">
                            <ol class="breadcrumb">
                                <li>
                                    <asp:HyperLink ID="hlInicioBread" NavigateUrl="~/MenuGeral.aspx" runat="server"><i class="fas fa-home"></i> Início</asp:HyperLink>
                                </li>
                                <li class="active">
                                    <i class="fa fa-copy"></i><strong>Tutoriais</strong>
                                </li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="ibox float-e-margins">
            <div class="ibox-title">
                <h5>Tutoriais Aplicativo NutroVET</h5>
            </div>
            <div class="ibox-content">
                <div class="panel-body">
                    <div class="panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h5 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne"><i class="fas fa-home"></i>&nbsp;Página Inicial</a>
                                </h5>
                            </div>
                            <div id="collapseOne" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Observe que o botão
                                    <img src="./Imagens/Tutorial/01.png" alt="botão minimizar" height="25" width="25" />
                                    pode ocultar ou mostrar o menu lateral esquerdo do sistema, o que permite uma visualização mais ampla da tela do sistema.
                                                <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> De qualquer parte dos sistema que você estiver, ao clicar no menu lateral esquerdo
                                    <img src="./Imagens/Tutorial/02.png" alt="botão página inicial" height="20" width="90" />
                                    você será redirecionado para a página inicial do sistema. 
                                                Lá é possível visualizar os cards que demostram dados importantes sobre a quantidade dos seus registros no sistema.
                                                <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> No menu lateral esquerdo 
                                    <img src="./Imagens/Tutorial/03.png" alt="botão alimentos" height="20" width="80" />
                                    é possível acessar a biblioteca cadastrada de alimentos.
                                                <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Nos menus
                                    <img src="./Imagens/Tutorial/04.png" alt="botão tutores" height="20" width="70" />
                                    e
                                    <img src="./Imagens/Tutorial/05.png" alt="botão pacientes" height="20" width="90" />
                                    você poderá fazer os cadastros dos seus Tutores e Pacientes. Observe que para o cadastro do Tutor apenas o nome e email são obrigatórios, mas para o cadastro de paciente todos os dados são obrigatórios.
                                                <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Em
                                    <img src="./Imagens/Tutorial/06.png" alt="botão cardápios" height="20" width="70" />
                                    você terá acesso ao coração do sistema, veja mais detalhes abaixo.
                                                <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Para sair do sistema e acessar de outro dispositivo, clique no botão
                                    <img src="./Imagens/Tutorial/07.png" alt="botão sair dos sitema" height="20" width="90" />
                                    e depois realize o login no outro dispositivo, pois a licença de uso do sistema dá direito ao uso em apenas um aparelho por vez.
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo"><i class="fas fa-utensils"></i>&nbsp;Alimentos</a>
                                </h4>
                            </div>
                            <div id="collapseTwo" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Acessando esse menu você tem acesso a lista de alimentos cadastrados no sistema, e pode realizar:
                                                <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> uma busca pelo nome do alimento
                                    <img src="./Imagens/Tutorial/08.png" alt="botão pesquisa" height="30" width="500" />
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> percorrer a lista utilizando a paginação na parte inferior da tela 
                                    <img src="./Imagens/Tutorial/09.png" alt="botões de paginação" height="30" width="250" />
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> E também estipular qual o número de alimentos que deseja ver em uma página
                                    <img src="./Imagens/Tutorial/10.png" alt="registros por página" height="30" width="250" />
                                    <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Para visualizar a composição de um alimento, clique em
                                    <img src="./Imagens/Tutorial/11.png" alt="botão editar" height="30" width="70" />
                                    <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Para visualizar os nutrientes escolha um grupo e clique para abrir os nutrientes daquele grupo, como por exemplo
                                    <img src="./Imagens/Tutorial/12.png" alt="botão nutrientes" height="30" width="80" />
                                    <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Você pode visualizar todos os alimentos e seus ingredientes, no entanto não é possível alterar nenhuma informação da biblioteca de alimentos, para a segurança do banco de dados, em caso de detectar alguma divergência por gentileza comunique por <i class="fas fa-envelope-square"></i>email em <a href="contato@nutrovet.com.br">contato@nutrovet.com.br</a>
                                    <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Você pode cadastrar alimentos normalmente e eles ficarão restritos ao seu uso, para isso selecione a fonte, PESSOAL, o grupo que o alimento melhor se encaixa e defina o nome do alimento, depois é só cadastrar os valores observando as unidades. Caso queira compartilhar algum alimento que você cadastrou com a Biblioteca Geral de Alimentos e consequentemente permitir que todos os outros usuários tenham acesso, selecione “SIM” na opção compartilhar do cadastro de alimentos, que iremos conferir os dados e providenciar o compartilhamento, veja:
                                                 <img src="./Imagens/Tutorial/13.png" alt="botão compartilhar" height="30" width="200" />

                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree"><i class="fas fa-balance-scale"></i>&nbsp;Cardápios</a>
                                </h4>
                            </div>
                            <div id="collapseThree" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Acessando esse menu você terá uma lista de todos os cardápios registrados no sistema, e caso deseje poderá utilizar o filtro superior para achar mais rapidamente a dieta de um tutor ou de um paciente específico. Para acessar o conteúdo da dieta desejada, basta clicar em 
                                    <img src="./Imagens/Tutorial/11.png" alt="botão editar" height="30" width="70" />
                                    <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> E para criar um novo cardápio, escolha a opção
                                    <img src="./Imagens/Tutorial/14.png" alt="botão inserir" height="30" width="70" />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFour"><i class="fas fa-balance-scale"></i>&nbsp;Cardápios <i class="fas fa-arrow-right"></i>&nbsp;Dados Do Paciente</a>
                                </h4>
                            </div>
                            <div id="collapseFour" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Ao clicar em inserir, uma tela será apresentada para a escolha do tutor, do paciente, do peso ideal, da sugestão de dieta e do fator a ser utilizado no cálculo do NEM da dieta. Todo o calculo é feito com base no peso ideal e não do peso atual. Outro detalhe é que depois de inseridas as informações, é necessário clicar no botão
                                    <img src="./Imagens/Tutorial/15.png" alt="botão calcular NEM" height="25" width="80" />
                                    para que o NEM seja calculado corretamente, e não se esqueça que caso altere alguma coisa posteriormente, também é preciso clicar no botão para refazer as contas. Da mesma forma essa sistemática vale caso você esteja editando uma dieta feita anteriormente, sempre ao final da edição dos dados, clique em calcular.
                                                <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Temos cerca de 40 sugestões de distribuições calóricas cadastradas, mas veja que são apenas sugestivas, você pode e deve formular da forma como preferir. Todas as sugestões de dietas são frutos de consultoria da equipe do Cachorro Verde e frutos de muitos anos de experiência em formulação de dietas, aproveito para agradecer a Sylvia e a Vanessa por todo o apoio.
                                                Depois de inseridas as informações e calculado o NEM, clique em
                                    <img src="./Imagens/Tutorial/16.png" alt="botão calcular NEM" height="30" width="70" />
                                    e você será direcionado para a aba “Composição do Cardápio”.
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive"><i class="fas fa-balance-scale"></i>&nbsp;Cardápios <i class="fas fa-arrow-right"></i>&nbsp;Composição do Cardápio</a>
                                </h4>
                            </div>
                            <div id="collapseFive" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Para iniciar a montar o cardápio, na caixa de pesquisa escreva mais de 3 letras do alimento a ser pesquisado e então clique na lupa ou simplesmente tecle Enter no seu teclado. Caso esteja procurando por sal, por exemplo, que tem apenas 3 letras, digite sal seguida de um espaço.
                                    <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Agora uma lista de alimentos será exibida, com base no seu critério de pesquisa, escolha um dos alimentos e clique nele, você notará que o alimento ficará marcado na lista, depois disso basta digitar a quantidade do alimento e clicar em inserir. Observe também que ao lado do alimento há a indicação de que biblioteca ele pertence. Atualmente temos TACO, TUCUNDUVA, USDA, alimentos de fabricantes específicos, definidos com a fonte FABRICANTE, que significa que os dados do alimento foram fornecidos pelo próprio fabricante do produto.
                                    <img src="./Imagens/Tutorial/17.png" class="img-responsive" alt="incluir alimento" height="180" width="450" />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Ao incluir um alimento, os gráficos da linha inferior automaticamente se movimentarão e irão refletir a situação atual da sua dieta, e você poderá comparar com os gráficos da linha superior, que são justamente o reflexo da sugestão de dieta que foi escolhida na aba anterior.
                                    <img src="./Imagens/Tutorial/18.png" class="img-responsive" alt="distribuição dos nutrientes" />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Veja se os valores de fibras e umidade na dieta e estão de acordo com seus objetivos na dieta.
                                    <img src="./Imagens/Tutorial/19.png" class="img-responsive" alt="valores de fibra e umidade" />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Depois, veja se o NEM está sendo atingido com os alimentos adicionados no campo ˜Energia Presente (EM)”, perceba que quando a Energia Presente está menor do que o NEM o campo de Energia Presente fica amarelo, da mesma forma, se a Energia presente estiver dentro de 1% a mais ou a menos do que o NEM, o campo ficará azul, e caso a Energia Presente fique acima do NEM, o campo ficará vermelho.
                                    <img src="./Imagens/Tutorial/20.png" class="img-responsive" alt="valor do NEM atingido" />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Ao lado do nome do alimento adicionado na dieta há um símbolo de informação, veja:
                                    <img src="./Imagens/Tutorial/21.png" class="img-responsive" alt="informação do alimento" />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Ao clicar nele é exibido quanto temos de cada ingrediente cadastrado para aquele alimento em 100g de produto e na coluna ao lado, quanto temos de cada ingrediente de acordo com a quantidade que foi adicionada na dieta:
                                    <img src="./Imagens/Tutorial/22.png" class="img-responsive" alt="valor nutricional para 100 e para quantidade digitada" />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Ao clicar em
                                    <img src="./Imagens/Tutorial/23.png" alt="botão detalhes" height="30" width="70" />
                                    no rodapé da página, você poderá conferir os alimentos e as quantidade de carboidrato, proteína, gordura e fibras que cada alimento adiciona na dieta, veja:
                                    <img src="./Imagens/Tutorial/24.png" class="img-responsive" alt="valor nutricional para 100 e para quantidade digitada" />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseSix"><i class="fas fa-balance-scale"></i>&nbsp;Cardápios <i class="fas fa-arrow-right"></i>&nbsp;Nutrientes do Cardápio</a>
                                </h4>
                            </div>
                            <div id="collapseSix" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Depois de feitas todas as checagens na aba “Composição do Cardápio”, clique na aba seguinte s você terá acesso aos 80 nutrientes cadastrados para cada alimento, e poderá verificar diversas informações:
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Ao clicar em
                                    <img src="./Imagens/Tutorial/25.png" alt="tabela de exigências" height="30" width="350" />
                                    você poderá navegar entre as exigências nutricionais definidas para AAFCO, FEDIAF e NRC.
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Ao clicar no símbolo de informação ao lado do seletor de tabelas de exigências nutricionais você poderá ver quais são as exigências para 1.000Kcal de acordo com a tabela selecionada.
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Já nas informações do relatório exibido você poderá verificar a proporção entre a recomendação e as Kcal presentes na dieta, facilitando a visualização de quais as recomendações para a dieta em questão.
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Ao lado dessas colunas, pode ser visualizada a quantidade do nutriente que está presente na dieta, considerando os alimentos inseridos na aba anterior.
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> E por fim você pode verificar se há sobra ou falta de algum nutriente.
                                    <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Para selecionar os nutrientes que deseja visualizar, selecione um dos grupos listados.
                                    <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Veja que muitos dos nutrientes listados não são definidos por nenhuma das entidades utilizadas pelo sistema com seus mínimos e máximos, no entanto os valores podem ser visualizados para que você possa utilizar essa informação na sua estratégia nutricional para o paciente.
                                    <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> No caso desses alimentos sem definição de parâmetros, logicamente não haverá nem a apresentação de valores mínimos e máximos, nem mesmo a informação de sobra ou falta, será possível ver apenas a quantidade presente na dieta. 
                                    <br />
                                    <br />
                                    <i class="fas fa-toggle-on"></i> Atenção para quando o alimento possui apenas valores recomendados ou adequados, caso do NRC, mas não possui mínimos e máximos definidos, nesse caso não será exibido sobra e falta do nutriente, já que essa informação depende da fixação de mínimo e máximo.
                                </div>
                            </div>
                        </div>



                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseSeven"><i class="fas fa-portrait"></i>&nbsp;Perfil  <i class="fas fa-arrow-right"></i>&nbsp;Dados do Assinante</a>
                                </h4>
                            </div>
                            <div id="collapseSeven" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Acessando os Dados do Assinante na página de Perfil você terá acesso aos dados cadastrais, para executar as seguintes ações:
                                                <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Realizar uma conferência dos Dados
                                    <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                        <img src="./Imagens/Tutorial/Perfil/dadosAssinante.png" alt="dados do assinante" height="288" width="540" />
                                    </div>
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Fazer uma Inclusão dos dados 
                                    <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                        <img src="./Imagens/Tutorial/Perfil/dadosAssinanteAlterar.png" alt="dados do assinante" height="288" width="540" />
                                    </div>
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Fazer uma correção dos dados
                                    <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                        <img src="./Imagens/Tutorial/Perfil/dadosAssinanteCorrigir.png" alt="dados do assinante" height="288" width="540" />
                                    </div>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseEight"><i class="fas fa-portrait"></i>&nbsp;Perfil  <i class="fas fa-arrow-right"></i>&nbsp;Dados do Plano do Assinante</a>
                                </h4>
                            </div>
                            <div id="collapseEight" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Acessando os Dados do Plano do Assinante na página de Perfil você terá acesso aos dados do plano contratado, para executar as seguintes ações:
                                                <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Realizar uma conferência dos Dados do Plano
                                    <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                        <img src="./Imagens/Tutorial/Perfil/dadosPlano.png" alt="botão pesquisa" height="314" width="526" />
                                    </div>
                                     &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Fazer a inclusão ou correção do Cartão de Crédito associado ao Plano
                                    <br />
                                    <br />
                                    Para qualquer operação a ser realizada na aba de planos, além dos dados do assinante estarem completos, é necessário que um cartão de crédito válido seja incluído.
                                    <br />
                                    <br />
                                    Para realizar a inclusão do cartão de crédito o assinante deve clicar no botão 
                                    <img src="./Imagens/Tutorial/perfil/incluirCartao.png" alt="registros por página" height="30" width="120" />
                                    e preencher os dados referentes, para que o mesmo possa estar apto a ser associado ao plano. 
                                     <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                        <img src="./Imagens/Tutorial/Perfil/dadosInclusaoCartao.png" alt="botão pesquisa" height="234" width="268" />
                                    </div>

                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Renovar o Plano contratado ou assinar um Novo Plano
                                    <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                        <img src="./Imagens/Tutorial/Perfil/escolherPlanoCartao.png" alt="botão pesquisa" height="234" width="538" />
                                    </div>
                                    <br />
                                    <br />
                                   
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseNine"><i class="fas fa-portrait"></i>&nbsp;Perfil  <i class="fas fa-arrow-right"></i>&nbsp;Dados do Acesso do Assinante</a>
                                </h4>
                            </div>
                            <div id="collapseNine" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Na aba Acesso, o Assinante poderá realizar as seguintes ações:
                                                <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Alterar o usuário de acesso ao sistema
                                    <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                        <img src="./Imagens/Tutorial/Perfil/dadosAcessoUsuario.png" alt="alterar usuário" height="126" width="528" />
                                    </div>
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Alterar a senha de acesso ao sistema 
                                    <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                        <img src="./Imagens/Tutorial/Perfil/dadosAcessoSenha.png" alt="alterar usuário" height="126" width="528" />
                                    </div>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTen"><i class="fas fa-portrait"></i>&nbsp;Perfil <i class="fas fa-arrow-right"></i>&nbsp;Imagem</a>
                                </h4>
                            </div>
                            <div id="collapseTen" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Acessando a aba Imagem na página de Perfil você terá poderá executar a seguinte ação:
                                                <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Realizar a conferência ou alteração da imagem que aparece no menu da aplicação
                                    <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                        <img src="./Imagens/Tutorial/Perfil/imagemAssinante.png" alt="imagem do assinante" height="212" width="528" />
                                    </div>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseEleven"><i class="fas fa-portrait"></i>&nbsp;Perfil  <i class="fas fa-arrow-right"></i>&nbsp;Mensagem</a>
                                </h4>
                            </div>
                            <div id="collapseEleven" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <i class="fas fa-toggle-on"></i> Acessando a aba Mensagem na página de Perfil você terá acesso para executar a seguinte ação:
                                                <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fas fa-toggle-off"></i> Entrar em contato com o Administrador do Sistema Nutrovet
                                    <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                        <img src="./Imagens/Tutorial/Perfil/mensagemAssinante.png" alt="mensagem do assinante" height="212" width="528" />
                                    </div>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>
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
