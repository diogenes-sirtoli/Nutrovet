using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using DCL;
using BLL;
using System.Text.RegularExpressions;
using PagarMe;

namespace Nutrovet.Plano
{
    public partial class FinalizarAssinatura : System.Web.UI.Page
    {
        protected clAcessosVigenciaPlanosBll planoVigenteBll = new clAcessosVigenciaPlanosBll();
        protected clPessoasBll pessoasBll = new clPessoasBll();
        protected clTutoresBll tutoresBll = new clTutoresBll();
        protected clAcessosVigenciaCupomBll voucherBll = new clAcessosVigenciaCupomBll();
        protected AcessosVigenciaCupomDesconto voucherDcl;
        protected TOPlanosBll _planoTO;
        protected List<TOPlanosBll> _planosLista = new List<TOPlanosBll>();
        protected clPlanosAssinaturasBll planosBll = new clPlanosAssinaturasBll();
        protected PlanosAssinatura planosDcl;
        protected clAcessosVigenciaCupomBll cupomBll = new clAcessosVigenciaCupomBll();
        protected AcessosVigenciaCupomDesconto cupomDcl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if ((Session["PlanoAssinatura"] == null) &&
                    (Session["DadosComprador"] == null))
                {
                    Response.Redirect("~/Plano/EscolherAssinatura.aspx");
                }
                else
                {
                    PopulaPlano();
                }
            }
        }

        private void PopulaPlano()
        {
            TOPessoasCartaoCreditoBll _dadosComprador =
                (TOPessoasCartaoCreditoBll)Session["DadosComprador"];
            lblSelecionados.Text = "Selecionados por: " + _dadosComprador.Nome;

            List<TOPlanosBll> listagem = (List<TOPlanosBll>)Session["PlanoAssinatura"];
            TOPlanosBll _plano = listagem.Where(l => l.dPlanoTp !=
                (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar).SingleOrDefault();
            double _valorTotal = 0;
            double _somaPlanos = Funcoes.Funcoes.ConvertePara.Double(
                listagem.Where(l => l.dPlanoTp !=
                    (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar).Sum(s => s.ValorPlano));
            double _somaModulos = Funcoes.Funcoes.ConvertePara.Double(
                listagem.Where(l => l.dPlanoTp ==
                    (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar).Sum(s => s.ValorPlano));

            lblNumeroItens.Text = Funcoes.Funcoes.ConvertePara.String(listagem.Count);
            lblNumeroDescontos.Text = _plano.VoucherNr;
            _valorTotal = _somaPlanos + _somaModulos;
            lblValorAssinatura.Text = string.Format("Valor Total: {0:c}", _valorTotal);

            ViewState["_somaPlanos"] = _somaPlanos;
            ViewState["_somaModulos"] = _somaModulos;
            ViewState["_valorTotal"] = _valorTotal;

            rptPlanos.DataSource = ColocaPercentual(listagem.Where(l => l.dPlanoTp !=
                (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar));
            rptPlanos.DataBind();
            rptModulos.DataSource = ColocaPercentual(listagem.Where(l => l.dPlanoTp ==
                (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar));
            rptModulos.DataBind();
        }

        private List<TOPlanosBll> ColocaPercentual(IEnumerable<TOPlanosBll> _planos)
        {
            List<TOPlanosBll> retListagem = new List<TOPlanosBll>();
            TOPlanosBll retItem;

            foreach (TOPlanosBll item in _planos)
            {
                retItem = item;
                retItem.Plano = item.Plano.Replace("Percent", "%");

                retListagem.Add(retItem);
            }

            return retListagem.ToList();
        }

        protected void lbVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Plano/ResumoAssinatura.aspx");
        }


        protected void rptPlanos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        break;
                    }
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        Label lblValorPlanoSelecionado = (Label)e.Item.FindControl(
                            "lblValorPlanoSelecionado");
                        Label lblDescrB = (Label)e.Item.FindControl("lblDescrB");
                        double _soma = (double)ViewState["_somaPlanos"];

                        lblValorPlanoSelecionado.Text = string.Format("{0:c}", _soma);

                        HtmlGenericControl _iconeDoPlano = (HtmlGenericControl)e.Item.FindControl(
                            "iconeDoPlano");

                        Label lblTextoPlanoSelecionado = (Label)e.Item.FindControl(
                            "lblTextoPlanoSelecionado");

                        string _plano = ((TOPlanosBll)e.Item.DataItem).Plano;

                        switch (_plano)
                        {

                            case "Básico":
                                {
                                    _iconeDoPlano.Attributes["class"] = "fas fa-paper-plane";
                                    break;
                                }
                            case "Intermediário":
                                {
                                    _iconeDoPlano.Attributes["class"] = "fas fa-plane fa-flip-horizontal";
                                    break;
                                }
                            case "Completo":
                                {
                                    _iconeDoPlano.Attributes["class"] = "fas fa-rocket";
                                    break;
                                }
                        }

                        lblTextoPlanoSelecionado.Text = _plano +
                            " - " + ((TOPlanosBll)e.Item.DataItem).Periodo;
                        lblDescrB.Text = ((TOPlanosBll)e.Item.DataItem).TipoPlano;

                        break;
                    }
            }
        }

        protected void rptModulos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        Label lblValorModuloAdicional = (Label)e.Item.FindControl(
                            "lblValorModuloAdicional");
                        double _soma = (double)ViewState["_somaModulos"];

                        lblValorModuloAdicional.Text = string.Format("{0:c}", _soma);

                        break;
                    }
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        Label lblTextoModuloAdicional = (Label)e.Item.FindControl(
                            "lblTextoModuloAdicional");
                        lblTextoModuloAdicional.Text = ((TOPlanosBll)e.Item.DataItem).Plano +
                            " - " + ((TOPlanosBll)e.Item.DataItem).Periodo;
                        break;
                    }
            }
        }

        protected void lbRemoverVoucher_Click(object sender, EventArgs e)
        {
            if ((Session["PlanoAssinatura"] == null) &&
                (Session["DadosComprador"] == null))
            {
                Response.Redirect("~/Plano/EscolherAssinatura.aspx");
            }
            else
            {
                List<TOPlanosBll> _planos = (List<TOPlanosBll>)
                    Session["PlanoAssinatura"];

                foreach (TOPlanosBll item in _planos)
                {
                    _planoTO = item;

                    _planoTO.VoucherId = 0;
                    _planoTO.VoucherUsado = false;
                    _planoTO.VoucherNr = null;
                    _planoTO.VoucherTipo = 0;
                    _planoTO.VoucherValor = null;

                    _planosLista.Add(_planoTO);
                }

                Session["PlanoAssinatura"] = _planosLista;

                PopulaPlano();
            }
        }

        protected void lbAssinar_Click(object sender, EventArgs e)
        {
            bool _servidoLocal = false;

            lbAssinar.Attributes.Add("onclick", "return false;");
            lbAssinar.Attributes.Add("disabled", "true"); //or try .Add("disabled","true");
            lbAssinar.Enabled = false;

            switch (Server.MachineName.ToUpper())
            {
                case "SICORP-01":
                case "SICORP-02":
                case "PC-D1R3ARJU":
                case "SICORP-SERVER":

                    {
                        PagarMeService.DefaultEncryptionKey = "ek_test_CO56Ihyug989EVL7ObCDqLkDgvPzTo";
                        PagarMeService.DefaultApiKey = "ak_test_aCsUqdiE1HAosPWLS3qf4A4aaETTut";
                        _servidoLocal = true;

                        break;
                    }
                case "S198-12-156-200":
                case "NUTROVET-SERVER":
                    {
                        PagarMeService.DefaultEncryptionKey = "ek_live_2xSZkpu0OMnFvmavVWY2oDTQ2eyqqf";
                        PagarMeService.DefaultApiKey = "ak_live_Rf9aOSL54oyBwbryrwQwFMx8IE8mBh";
                        _servidoLocal = false;

                        break;
                    }
            }

            if ((Session["PlanoAssinatura"] == null) &&
                (Session["DadosComprador"] == null))
            {
                Response.Redirect("~/Plano/EscolherAssinatura.aspx");
            }
            else
            {
                double _valorTotal = Funcoes.Funcoes.ConvertePara.Double(
                    ViewState["_valorTotal"]);
                TOPessoasCartaoCreditoBll _dadosComprador =
                    (TOPessoasCartaoCreditoBll)Session["DadosComprador"];
                List<TOPlanosBll> _listagemPlanos = (List<TOPlanosBll>)Session["PlanoAssinatura"];
                TOPlanosBll _dadosPlano = _listagemPlanos.FirstOrDefault();

                string statusPagamento = "";

                switch (_dadosComprador.dNacionalidade)
                {
                    case 1:
                        {
                            Subscription assinatura = InserirAssinatura(_dadosComprador,
                                _dadosPlano, _servidoLocal);

                            if (assinatura != null)
                            {
                                string idTransaction = assinatura.Id;

                                statusPagamento = assinatura.Status.ToString();

                                if ((statusPagamento == "Paid") || (statusPagamento == "Trialing"))
                                {
                                    _dadosPlano.IdSubscriptionPagarMe = assinatura.Id;
                                    _dadosPlano.DataPlanoInicial = (
                                        assinatura.CurrentPeriodStart > DateTime.Today ?
                                            DateTime.Today : assinatura.CurrentPeriodStart);
                                    _dadosPlano.DataPlanoFinal = assinatura.CurrentPeriodEnd;
                                    _dadosPlano.StatusPagarMe = statusPagamento;

                                    bllRetorno retorno = GravaDadosNoBanco(_dadosPlano, _dadosComprador);

                                    if (retorno.retorno)
                                    {
                                        lblMsgModal.Text = @"Assinatura realizada com sucesso";
                                        lblMensagemAssinatura.Text =
                                            @"Você será direcionado para a tela de Logon.";
                                        btnSim.Visible = true;
                                        btnNao.Visible = false;

                                        popUpModal.Show();
                                    }
                                    else
                                    {
                                        lblMsgModal.Text = @"Problema de Gravação no Banco de Dados";
                                        lblMensagemAssinatura.Text =
                                            @"Sua Assinatura foi Efetivada, 
                                                mas seus dados não foram gravados no Banco de Dados. 
                                            </br>
                                            Por Favor, Contate o Administrador!!!";
                                        btnSim.Visible = false;
                                        btnNao.Visible = true;

                                        popUpModal.Show();
                                    }
                                }
                                else
                                {
                                    lblMsgModal.Text = @"Problema ao Gerar Assinatura";
                                    lblMensagemAssinatura.Text =
                                            @"Houve um Problema ao Gerar sua Assinatura.
                                            </br>
                                            Por Favor, Contate o Administrador!!!";
                                    btnSim.Visible = true;
                                    btnNao.Visible = false;

                                    popUpModal.Show();
                                }
                            }

                            break;
                        }
                    case 2:
                        {
                            Transaction transacao = InserirTransacao(_dadosComprador, _dadosPlano);

                            if (transacao != null)
                            {
                                string idTransaction = transacao.Id;

                                statusPagamento = transacao.Status.ToString();

                                if ((statusPagamento == "Paid") || (statusPagamento == "Trialing"))
                                {
                                    _dadosPlano.IdSubscriptionPagarMe = transacao.Id;
                                    _dadosPlano.DataPlanoInicial = DateTime.Today;
                                    _dadosPlano.DataPlanoFinal = (_dadosPlano.Periodo == "Mensal" ?
                                        DateTime.Today.AddMonths(1) : (_dadosPlano.Periodo == "Anual" ?
                                            DateTime.Today.AddYears(1) : DateTime.Today.AddMonths(1)));
                                    _dadosPlano.StatusPagarMe = transacao.Status.ToString();

                                    bllRetorno retorno = GravaDadosNoBanco(_dadosPlano, _dadosComprador);

                                    if (retorno.retorno)
                                    {
                                        lblMsgModal.Text = @"Assinatura realizada com sucesso";
                                        lblMensagemAssinatura.Text =
                                            @"Você será direcionado para a tela de Logon.";
                                        btnSim.Visible = true;
                                        btnNao.Visible = false;
                                        popUpModal.Show();
                                    }
                                    else
                                    {
                                        lblMsgModal.Text = @"Problema de Gravação no Banco de Dados";
                                        lblMensagemAssinatura.Text =
                                            @"Sua Assinatura foi Efetivada, 
                                            mas seus dados não foram gravados no Banco de Dados. 
                                            </br>
                                            Por Favor, Contate o Administrador!!!";
                                        btnSim.Visible = false;
                                        btnNao.Visible = true;
                                        popUpModal.Show();
                                    }
                                }
                                else
                                {
                                    lblMsgModal.Text = @"Transação Não Efetivada";
                                    lblMensagemAssinatura.Text =
                                        @"Sua transação não foi efetivada, 
                                          pela Administradora de Planos. 
                                          </br>
                                          Por Favor, Contate o Administrador!!!";
                                    btnSim.Visible = false;
                                    btnNao.Visible = true;
                                    popUpModal.Show();
                                }
                            }
                            else
                            {
                                lblMsgModal.Text = @"Transação Não Efetivada";
                                lblMensagemAssinatura.Text =
                                    @"A transação não pode efetivada. 
                                      Verifique as informações digitadas!!!";
                                btnSim.Visible = false;
                                btnNao.Visible = true;
                                popUpModal.Show();
                            }

                            break;
                        }
                }
            }
        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void btnNao_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Plano/EscolherAssinatura.aspx");
        }

        protected bllRetorno GravaDadosNoBanco(TOPlanosBll dadosPlano,
            TOPessoasCartaoCreditoBll dadosComprador)
        {
            bllRetorno retornoDados = new bllRetorno();
            clTutoresBll tutoresBll = new clTutoresBll();
            clPessoasBll pessoaBll = new clPessoasBll();
            Pessoa pessoaDcl;
            Tutore tutoresDcl;
            Acesso acessoDcl;
            clPessoasCartaoCreditoBll cartaoCreditoBll = new clPessoasCartaoCreditoBll();
            PessoasCartaoCredito cartaoCreditoDcl;
            AcessosVigenciaPlano vigenciaPlanoDcl;
            AcessosVigenciaSituacao vigenciaSituacaoDcl;

            if (Funcoes.Funcoes.ConvertePara.Bool(dadosComprador.Tutor))
            {
                retornoDados = tutoresBll.ModificaTutorParaCliente(dadosComprador.Nome,
                    dadosComprador.EMail);
            }
            else
            {
                #region PESSOAS
                pessoaDcl = new Pessoa();
                pessoaDcl.IdTpPessoa = (int)DominiosBll.PessoasAuxTipos.Cliente;
                pessoaDcl.Nome = dadosComprador.Nome;
                pessoaDcl.dTpEntidade = dadosComprador.dTpEntidade;
                pessoaDcl.dNacionalidade = dadosComprador.dNacionalidade;
                pessoaDcl.DataNascimento = dadosComprador.DataNascimento;
                pessoaDcl.Nome = dadosComprador.Nome;

                switch (Funcoes.Funcoes.ConvertePara.Int(dadosComprador.dTpEntidade))
                {
                    case 1:
                        {
                            switch (dadosComprador.dNacionalidade)
                            {
                                case 1:
                                    {
                                        pessoaDcl.CPF = dadosComprador.CPF;
                                        pessoaDcl.Passaporte = "";

                                        break;
                                    }
                                case 2:
                                    {
                                        pessoaDcl.CPF = "";
                                        pessoaDcl.Passaporte = dadosComprador.Passaporte;

                                        break;
                                    }
                                default:
                                    {
                                        pessoaDcl.CPF = "";
                                        pessoaDcl.Passaporte = "";

                                        break;
                                    }
                            }

                            pessoaDcl.CNPJ = "";

                            break;
                        }
                    case 2:
                        {
                            pessoaDcl.Passaporte = "";
                            pessoaDcl.CPF = "";
                            pessoaDcl.CNPJ = dadosComprador.CNPJ;

                            break;
                        }
                    default:
                        {
                            pessoaDcl.Passaporte = "";
                            pessoaDcl.CPF = "";
                            pessoaDcl.CNPJ = "";

                            break;
                        }
                }

                pessoaDcl.Email = dadosComprador.EMail;

                switch (Funcoes.Funcoes.ConvertePara.Int(dadosComprador.dTpFone))
                {
                    case 1:
                        {
                            pessoaDcl.Telefone = dadosComprador.TelefoneComMascara;
                            pessoaDcl.Celular = "";

                            break;
                        }
                    case 2:
                        {
                            pessoaDcl.Telefone = "";
                            pessoaDcl.Celular = dadosComprador.TelefoneComMascara;

                            break;
                        }
                    default:
                        {
                            pessoaDcl.Telefone = "";
                            pessoaDcl.Celular = "";

                            break;
                        }
                }

                pessoaDcl.Logradouro = dadosComprador.Logradouro;
                pessoaDcl.Logr_Nr = dadosComprador.NumeroLogradouro;
                pessoaDcl.Logr_Compl = dadosComprador.Complemento;
                pessoaDcl.Logr_Bairro = dadosComprador.Bairro;
                pessoaDcl.Logr_CEP = dadosComprador.CEP;
                pessoaDcl.Logr_Cidade = dadosComprador.Cidade;
                pessoaDcl.Logr_UF = dadosComprador.Estado;
                pessoaDcl.Logr_Pais = dadosComprador.Pais;

                pessoaDcl.Usuario = dadosComprador.EMail;
                pessoaDcl.Senha = Funcoes.Funcoes.Seguranca.Criptografar(
                    dadosComprador.Senha);
                pessoaDcl.Bloqueado = false;

                pessoaDcl.Ativo = true;
                pessoaDcl.IdOperador = 1;
                pessoaDcl.DataCadastro = DateTime.Now;
                pessoaDcl.IP = Request.UserHostAddress;
                #endregion                

                #region ACESSOS
                acessoDcl = new Acesso();
                acessoDcl.IdAcFunc = 3;
                acessoDcl.Inserir = true;
                acessoDcl.Alterar = true;
                acessoDcl.Excluir = true;
                acessoDcl.Consultar = true;
                acessoDcl.AcoesEspeciais = false;
                acessoDcl.Relatorios = true;
                acessoDcl.SuperUser = false;
                acessoDcl.TermoUso = false;

                acessoDcl.Ativo = true;
                acessoDcl.IdOperador = 1;
                acessoDcl.DataCadastro = DateTime.Now;
                acessoDcl.IP = Request.UserHostAddress;
                #endregion

                #region CARTÃO DE CRÉDITO
                cartaoCreditoDcl = new PessoasCartaoCredito();
                cartaoCreditoDcl.NrCartao = Funcoes.Funcoes.Seguranca.Criptografar(
                    dadosComprador.NrCartao);
                cartaoCreditoDcl.CodSeg = Funcoes.Funcoes.Seguranca.Criptografar(
                    dadosComprador.CodSeg);
                cartaoCreditoDcl.VencimCartao = Funcoes.Funcoes.Seguranca.Criptografar(
                    dadosComprador.VencimCartao);
                cartaoCreditoDcl.NomeCartao = Funcoes.Funcoes.Seguranca.Criptografar(
                    dadosComprador.NomeCartao);

                cartaoCreditoDcl.Ativo = true;
                cartaoCreditoDcl.IdOperador = 1;
                cartaoCreditoDcl.DataCadastro = DateTime.Now;
                cartaoCreditoDcl.IP = Request.UserHostAddress;
                #endregion

                #region VIGÊNCIA PLANOS
                vigenciaPlanoDcl = new AcessosVigenciaPlano();
                vigenciaPlanoDcl.IdPlano = Funcoes.Funcoes.ConvertePara.Int(
                    dadosPlano.IdPlano);
                vigenciaPlanoDcl.IdSubscriptionPagarMe = dadosPlano.IdSubscriptionPagarMe;
                vigenciaPlanoDcl.StatusPagarMe = dadosPlano.StatusPagarMe;
                vigenciaPlanoDcl.DtInicial = (dadosPlano.DataPlanoInicial != null ?
                    dadosPlano.DataPlanoInicial.Value : DateTime.Today);
                vigenciaPlanoDcl.DtFinal = (dadosPlano.DataPlanoFinal != null ?
                    dadosPlano.DataPlanoFinal.Value : DateTime.Today.AddYears(1));

                if (dadosPlano.VoucherId > 0)
                {
                    vigenciaPlanoDcl.IdCupom = dadosPlano.VoucherId;
                }

                vigenciaPlanoDcl.Ativo = true;
                vigenciaPlanoDcl.IdOperador = 1;
                vigenciaPlanoDcl.DataCadastro = DateTime.Now;
                vigenciaPlanoDcl.IP = Request.UserHostAddress;
                #endregion

                #region VIGÊNCIA SITUAÇÃO

                for (int i = 1; i < 4; i++)
                {
                    vigenciaSituacaoDcl = new AcessosVigenciaSituacao();
                    vigenciaSituacaoDcl.IdSituacao = i;
                    vigenciaSituacaoDcl.DataSituacao = DateTime.Today;

                    vigenciaSituacaoDcl.Ativo = true;
                    vigenciaSituacaoDcl.IdOperador = 1;
                    vigenciaSituacaoDcl.DataCadastro = DateTime.Now;
                    vigenciaSituacaoDcl.IP = Request.UserHostAddress;

                    vigenciaPlanoDcl.AcessosVigenciaSituacaos.Add(vigenciaSituacaoDcl);
                }

                #endregion

                # region Coloca tudo no Banco de Dados

                pessoaDcl.PessoasCartaoCreditos.Add(cartaoCreditoDcl);
                pessoaDcl.AcessosVigenciaPlanos.Add(vigenciaPlanoDcl);
                pessoaDcl.Acesso.Add(acessoDcl);
                #endregion

                //Insere tudo no Banco de Dados
                retornoDados = pessoaBll.Inserir(pessoaDcl);
                retornoDados.mensagem = (retornoDados.retorno ?
                    "Cliente criado com Sucesso!</br>Acesso criado com Sucesso!</br>" +
                    "Cartão de Crédito gravado com Sucesso!</br>Plano criado com Suc" +
                    "esso!</br>" : "Erro: " + retornoDados.mensagem + "</br>");


                #region TUTORES e Voucher
                if (retornoDados.retorno)
                {
                    if (dadosPlano.VoucherId > 0)
                    {
                        cupomDcl = cupomBll.Carregar(dadosPlano.VoucherId);

                        cupomDcl.fUsado = true;

                        cupomDcl.Ativo = true;
                        cupomDcl.IdOperador = 1;
                        cupomDcl.DataCadastro = DateTime.Now;
                        cupomDcl.IP = Request.UserHostAddress;

                        bllRetorno retAlteraCupom = cupomBll.Alterar(cupomDcl);
                        retornoDados.mensagem += (retAlteraCupom.retorno ?
                            "Cupom validado com Sucesso!</br>" :
                            "Erro Cupom: " + retAlteraCupom.mensagem + "</br>");
                    }

                    if (cartaoCreditoDcl.IdCartao > 0)
                    {
                        AcessosVigenciaPlano planoDcl =
                            planoVigenteBll.Carregar(vigenciaPlanoDcl.IdVigencia);

                        planoDcl.IdCartao = cartaoCreditoDcl.IdCartao;

                        planoDcl.Ativo = true;
                        planoDcl.IdOperador = 1;
                        planoDcl.DataCadastro = DateTime.Now;
                        planoDcl.IP = Request.UserHostAddress;

                        bllRetorno retAlteraPlanoVigente = planoVigenteBll.Alterar(planoDcl);
                    }

                    tutoresDcl = new Tutore();

                    tutoresDcl.IdCliente = pessoaDcl.IdPessoa;
                    tutoresDcl.IdTutor = pessoaDcl.IdPessoa;

                    tutoresDcl.Ativo = true;
                    tutoresDcl.IdOperador = 1;
                    tutoresDcl.DataCadastro = DateTime.Now;
                    tutoresDcl.IP = Request.UserHostAddress;

                    bllRetorno retornoTutores = tutoresBll.Inserir(tutoresDcl);
                }
                #endregion
            }

            return retornoDados;
        }

        protected Subscription InserirAssinatura(TOPessoasCartaoCreditoBll _dadosComprador,
            TOPlanosBll _dadosPlano, bool _servidorLocal)
        {
            try
            {
                CardHash card = new CardHash
                {
                    CardNumber = _dadosComprador.NrCartao,
                    CardHolderName = _dadosComprador.NomeCartao,
                    CardExpirationDate = _dadosComprador.VencimCartao,
                    CardCvv = _dadosComprador.CodSeg
                };

                string cardhash = card.Generate();

                Subscription subscription = new Subscription
                {
                    PaymentMethod = PaymentMethod.CreditCard,
                    CardNumber = card.CardNumber,
                    CardHolderName = card.CardHolderName,
                    CardExpirationDate = card.CardExpirationDate,
                    CardCvv = card.CardCvv,
                    CardHash = cardhash
                };

                if (_servidorLocal)
                {
                    subscription.Plan = PagarMeService.GetDefaultService().Plans.Find(
                        Funcoes.Funcoes.ConvertePara.String(_dadosPlano.IdPlanoPagarMeTestes));
                }
                else
                {
                    subscription.Plan = PagarMeService.GetDefaultService().Plans.Find(
                        Funcoes.Funcoes.ConvertePara.String(_dadosPlano.IdPlanoPagarMe));
                }

                //subscription.PostbackUrl = "https://nutrovet.com.br/Login.aspx";

                Customer customer = new Customer
                {
                    Name = _dadosComprador.Nome,
                    ExternalId = Funcoes.Funcoes.ConvertePara.String(_dadosComprador.IdPessoa),
                    Type = _dadosComprador.dTpEntidade == 1 ? CustomerType.Individual : CustomerType.Corporation,
                    DocumentNumber = _dadosComprador.dTpEntidade == 1 ? _dadosComprador.CPF : _dadosComprador.CNPJ,
                    Email = _dadosComprador.EMail,
                    Country = (_dadosComprador.Pais).ToLower(),
                    Address = new Address
                    {
                        Country = _dadosComprador.Pais.ToLower(),
                        Zipcode = _dadosComprador.CEP,
                        Street = _dadosComprador.Logradouro,
                        StreetNumber = _dadosComprador.NumeroLogradouro,
                        Complementary = _dadosComprador.Complemento,
                        Neighborhood = _dadosComprador.Bairro,
                        City = _dadosComprador.Cidade,
                        State = _dadosComprador.Estado
                    },
                    DocumentType = (_dadosComprador.dNacionalidade == 2 ? DocumentType.Passport :
                        (_dadosComprador.dTpEntidade == 1 ? DocumentType.Cpf : DocumentType.Cnpj)),
                    Documents = new[]
                    {
                         new Document
                         {

                             Type = (_dadosComprador.dNacionalidade == 2 ? DocumentType.Passport :
                                        (_dadosComprador.dTpEntidade == 1 ? DocumentType.Cpf : DocumentType.Cnpj)),
                             Number = (_dadosComprador.dNacionalidade == 2 ? _dadosComprador.DocumentosOutros :
                                        (_dadosComprador.dTpEntidade == 1 ? _dadosComprador.CPF : _dadosComprador.CNPJ))
                         }
                     },
                    Phone = new Phone
                    {
                        Ddd = _dadosComprador.DDDTelefone,
                        Number = _dadosComprador.Telefone
                    }
                };
                subscription.Customer = customer;

                subscription.Save();

                return subscription;
            }
            catch (PagarMeException errPagarMe)
            {
                lblMsgModal.Text = "<ul>";

                foreach (var item in errPagarMe.Error.Errors)
                {
                    lblMsgModal.Text += "<li>" + item.Message + "</li>";
                }
                lblMsgModal.Text += "</ul>";

                lblMensagemAssinatura.Text =
                    @"Problema com os dados do Assinante. <br /> Você será direcionado para a tela de Escolha do Plano.";
                btnSim.Visible = false;
                btnNao.Visible = true;

                popUpModal.Show();

                return null;
            }
            catch (Exception err)
            {
                var erro = err;
                lblMsgModal.Text = @"Assinatura não efetivada";
                lblMensagemAssinatura.Text =
                    @"Você será direcionado para a tela de Planos.";
                btnSim.Visible = false;
                btnNao.Visible = true;

                popUpModal.Show();

                return null;
            }
        }

        protected Transaction InserirTransacao(TOPessoasCartaoCreditoBll _dadosComprador,
            TOPlanosBll _dadosPlano)
        {
            double _valorTotal = Funcoes.Funcoes.ConvertePara.Double(ViewState["_valorTotal"]);

            try
            {
                CardHash card = new CardHash
                {
                    CardNumber = _dadosComprador.NrCartao,
                    CardHolderName = _dadosComprador.NomeCartao,
                    CardExpirationDate = _dadosComprador.VencimCartao,
                    CardCvv = _dadosComprador.CodSeg
                };

                string cardhash = card.Generate();

                Transaction transacao = new Transaction();

                decimal valorPlanoDecimal = Funcoes.Funcoes.ConvertePara.Decimal(_dadosPlano.ValorPlano);
                int valorPlanoInt = (Funcoes.Funcoes.ConvertePara.Int(valorPlanoDecimal) * 100);

                transacao.Amount = valorPlanoInt;

                transacao.PaymentMethod = PaymentMethod.CreditCard;

                transacao.CardNumber = _dadosComprador.NrCartao;
                transacao.CardHolderName = _dadosComprador.NomeCartao;
                transacao.CardExpirationDate = _dadosComprador.VencimCartao;
                transacao.CardCvv = _dadosComprador.CodSeg;
                transacao.CardHash = cardhash;

                transacao.Customer = new Customer
                {
                    ExternalId = Funcoes.Funcoes.ConvertePara.String(_dadosComprador.IdPessoa),
                    Name = _dadosComprador.Nome,
                    Type = _dadosComprador.dTpEntidade == 1 ? CustomerType.Individual :
                        CustomerType.Corporation,
                    Email = _dadosComprador.EMail,
                    Country = _dadosComprador.Pais.ToLower(),
                    Documents = new[]
                    {
                        new Document
                        {
                            Type = DocumentType.Passport,
                            Number =  _dadosComprador.Passaporte
                        }
                    },
                    PhoneNumbers = new string[]
                    {
                            _dadosComprador.DDDTelefone + _dadosComprador.Telefone
                    },
                    Birthday = new DateTime(1991, 12, 12).ToString("yyyy-MM-dd")
                };

                transacao.Billing = new Billing
                {
                    Name = _dadosComprador.Nome,
                    Address = new Address
                    {
                        Zipcode = _dadosComprador.CEP,
                        Street = _dadosComprador.Logradouro,
                        StreetNumber = _dadosComprador.NumeroLogradouro,
                        Complementary = _dadosComprador.Complemento,
                        Neighborhood = _dadosComprador.Bairro,
                        City = _dadosComprador.Cidade,
                        State = _dadosComprador.Estado,
                        Country = (_dadosComprador.Pais).ToLower()
                    }
                };

                transacao.Item = new[]
                {
                    new Item()
                    {
                        Id = Funcoes.Funcoes.ConvertePara.String(_dadosPlano.IdPlanoPagarMe),
                        Title = Funcoes.Funcoes.ConvertePara.String(_dadosPlano.dNomePlano),
                        Quantity = 1,
                        Tangible = true,
                        UnitPrice = Funcoes.Funcoes.ConvertePara.Int(_dadosPlano.ValorPlano)
                    }
                };

                transacao.Save();

                return transacao;
            }
            catch (PagarMeException errPagarMe)
            {
                lblMsgModal.Text = "<ul>";

                foreach (var item in errPagarMe.Error.Errors)
                {
                    lblMsgModal.Text += "<li>" + item.Message + "</li>";
                }
                lblMsgModal.Text += "</ul>";

                lblMensagemAssinatura.Text =
                    @"Problema com os dados do Assinante. <br /> Você será direcionado para a tela de Escolha do Plano.";
                btnSim.Visible = false;
                btnNao.Visible = true;

                popUpModal.Show();

                return null;
            }
            catch (Exception err)
            {
                var erro = err;
                lblMsgModal.Text = @"Assinatura não efetivada";
                lblMensagemAssinatura.Text =
                    @"Você será direcionado para a tela de Planos.";
                btnSim.Visible = false;
                btnNao.Visible = true;

                popUpModal.Show();

                return null;
            }
        }
    }
}