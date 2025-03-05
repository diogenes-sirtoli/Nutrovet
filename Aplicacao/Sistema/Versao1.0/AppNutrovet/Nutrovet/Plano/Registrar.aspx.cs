using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DCL;

namespace Nutrovet.Plano
{
    public partial class Registrar : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll userBll = new clPessoasBll();
        protected Pessoa userDcl;
        protected TOPessoasBll userTO;
        protected TOPlanosBll planosTO;
        protected clAcessosVigenciaPlanosBll vigenciaPlanosBll = new 
            clAcessosVigenciaPlanosBll();
        protected AcessosVigenciaPlano vigenciaPlanosDcl;
        protected clAcessosVigenciaSituacaoBll vigenciaSituacaoBll = new
            clAcessosVigenciaSituacaoBll();
        protected AcessosVigenciaSituacao vigenciaSituacaoDcl;
        protected clAcessosVigenciaCupomBll cupomBll = new clAcessosVigenciaCupomBll();
        protected AcessosVigenciaCupomDesconto cupomDcl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                alertas.Visible = false;

                if (Session["escolheuPlano"] != null)
                {
                    planosTO = (TOPlanosBll)Session["escolheuPlano"];

                    if (!planosTO.fAcessoLiberado)
                    {
                        frmBtnPagSeguro.Src = "~/BtnPagSeguro/FormBtnPagSeguro.aspx?_plano=" +
                            planosTO.Plano;
                    }
                    else
                    {
                        frmBtnPagSeguro.Src = "";
                    }

                    divFrame.Visible = false;
                    divRegistrar.Visible = true;

                    Session.Remove("escolheuPlano");
                    ViewState["escolheuPlano"] = planosTO;
                }
                else
                {
                    Response.Redirect("~/Plano/EscolherPlano.aspx");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            planosTO = (TOPlanosBll)ViewState["escolheuPlano"];
            cupomDcl = cupomBll.Carregar(planosTO.VoucherNr);

            if (tbSenha.Text == tbConfirmarSenha.Text)
            {
                bllRetorno ret = new bllRetorno();

                userTO = userBll.CarregarLogon(tbNome.Text, tbEmail.Text);

                if ((userTO != null) && (userTO.IdPessoa > 0))
                {
                    userDcl = userBll.Carregar(userTO.IdPessoa);

                    userDcl.Email = tbEmail.Text;
                    userDcl.Senha = Funcoes.Funcoes.Seguranca.Criptografar(tbSenha.Text);
                    userDcl.Bloqueado = true;

                    userDcl.Ativo = true;
                    userDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                    userDcl.DataCadastro = DateTime.Now;
                    userDcl.IP = Request.UserHostAddress;

                    ret = userBll.Alterar(userDcl);
                }
                else
                {
                    userDcl = new Pessoa();
                    userTO = new TOPessoasBll();

                    //Grava o novo cliente no Banco de Dados
                    userDcl.Nome = tbNome.Text;
                    userDcl.IdTpPessoa = 2;
                    userDcl.DataNascimento = DateTime.Parse("01/01/1910");
                    userDcl.Email = tbEmail.Text;
                    userDcl.Senha = Funcoes.Funcoes.Seguranca.Criptografar(tbSenha.Text);
                    userDcl.Bloqueado = ((cupomDcl != null) && (Funcoes.Funcoes.ConvertePara.Bool(
                        cupomDcl.fAcessoLiberado)) ? false : true);

                    userDcl.Ativo = true;
                    userDcl.IdOperador = 1;
                    userDcl.DataCadastro = DateTime.Now;
                    userDcl.IP = Request.UserHostAddress;

                    //Cria o Plano
                    vigenciaPlanosDcl = new AcessosVigenciaPlano();

                    vigenciaPlanosDcl.IdPlano = vigenciaPlanosBll.IdPlano(planosTO);
                    vigenciaPlanosDcl.DtInicial = DateTime.Today;
                    vigenciaPlanosDcl.DtFinal = DateTime.Today.AddYears(1);
                    vigenciaPlanosDcl.QtdAnim = vigenciaPlanosBll.QtdAnimais(planosTO);
                    vigenciaPlanosDcl.Periodo = vigenciaPlanosBll.IdPeriodo(planosTO);
                    vigenciaPlanosDcl.Receituario = planosTO.Receituario;
                    vigenciaPlanosDcl.Prontuario = planosTO.Prontuario;
                    vigenciaPlanosDcl.ComprovanteAnexou = false;
                    vigenciaPlanosDcl.ComprovanteHomologado = false;
                    vigenciaPlanosDcl.IdCupom = cupomDcl?.IdCupom;

                    vigenciaPlanosDcl.Ativo = true;
                    vigenciaPlanosDcl.IdOperador = 1;
                    vigenciaPlanosDcl.DataCadastro = DateTime.Now;
                    vigenciaPlanosDcl.IP = Request.UserHostAddress;

                    //Cria o Andamento/Situação
                    vigenciaSituacaoDcl = new AcessosVigenciaSituacao();

                    vigenciaSituacaoDcl.IdSituacao =
                        (int)DominiosBll.AcessosPlanosAuxSituacao.Permitido;
                    vigenciaSituacaoDcl.DataSituacao = DateTime.Today;

                    vigenciaSituacaoDcl.Ativo = true;
                    vigenciaSituacaoDcl.IdOperador = 1;
                    vigenciaSituacaoDcl.DataCadastro = DateTime.Now;
                    vigenciaSituacaoDcl.IP = Request.UserHostAddress;

                    //Cria o Acesso se for Voucher Tudo Liberado
                    if ((cupomDcl != null) && (Funcoes.Funcoes.ConvertePara.Bool(
                         cupomDcl.fAcessoLiberado)))
                    {
                        acessosDcl = new Acesso();
                        acessosDcl.IdAcFunc = 3;
                        acessosDcl.Inserir = true;
                        acessosDcl.Alterar = true;
                        acessosDcl.Excluir = true;
                        acessosDcl.Consultar = true;
                        acessosDcl.AcoesEspeciais = false;
                        acessosDcl.Relatorios = true;
                        acessosDcl.SuperUser = false;
                        acessosDcl.TermoUso = false;

                        acessosDcl.Ativo = true;
                        acessosDcl.IdOperador = 1;
                        acessosDcl.DataCadastro = DateTime.Now;
                        acessosDcl.IP = Request.UserHostAddress;

                        userDcl.Acesso.Add(acessosDcl);
                    }

                    //Grava Tudo no Banco de Dados
                    vigenciaPlanosDcl.AcessosVigenciaSituacaos.Add(vigenciaSituacaoDcl);
                    userDcl.AcessosVigenciaPlanos.Add(vigenciaPlanosDcl);

                    ret = userBll.Inserir(userDcl);
                }

                if (ret.retorno)
                {
                    Pessoa tutorDcl = userDcl;
                    tutorDcl.IdCliente = userDcl.IdPessoa;

                    ret = userBll.Alterar(tutorDcl);
                    bool _invalidaVoucher = cupomBll.InvalidarVoucher(
                        planosTO.VoucherNr);

                    userTO.IdPessoa = userDcl.IdPessoa;
                    userTO.Nome = userDcl.Nome;
                    userTO.DataNascimento = userDcl.DataNascimento;
                    userTO.Email = userDcl.Email;
                    userTO.Senha = userDcl.Senha;
                    userTO.Bloqueado = userDcl.Bloqueado;
                    userTO.NrCupom = Funcoes.Funcoes.ConvertePara.Int(
                        planosTO.VoucherNr);
                    userTO.Ativo = userDcl.Ativo;
                    userTO.IdOperador = userDcl.IdOperador;
                    userTO.DataCadastro = userDcl.DataCadastro;
                    userTO.IP = userDcl.IP;

                    Session["pessoa"] = userTO;

                    //RotinaPgto(userTO);
                    if ((cupomDcl != null) &&
                        (Funcoes.Funcoes.ConvertePara.Bool(cupomDcl.fAcessoLiberado)))
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    else
                    {
                        divRegistrar.Visible = false;
                        divFrame.Visible = true;
                    }

                    alertas.Attributes["class"] = "alert alert-success alert-dismissible";
                    lblAlerta.Text = 
                        "Cadastro Realizado com Sucesso !</br>" + 
                        "Clique no Botão ASSINAR para Concluir a Assinatura !";
                    alertas.Visible = true;
                }
                else
                {
                    alertas.Attributes["class"] = "alert alert-danger alert-dismissible";
                    lblAlerta.Text = ret.mensagem;
                    alertas.Visible = true;
                }
            }
            else
            {
                alertas.Attributes["class"] = "alert alert-info alert-dismissible";
                lblAlerta.Text = @"Senha não Conferem!";
                alertas.Visible = true;
            }
        }
    }
}