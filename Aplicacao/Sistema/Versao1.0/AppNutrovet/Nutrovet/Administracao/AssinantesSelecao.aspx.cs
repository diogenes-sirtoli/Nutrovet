using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;

namespace Nutrovet.Administracao
{
    public partial class AssinantesSelecao : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll PessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected TOPessoasBll pessoasTO;
        protected clAcessosVigenciaPlanosBll planosBll = new clAcessosVigenciaPlanosBll();
        protected AcessosVigenciaPlano planosDcl;
        protected TOAcessosVigenciaPlanosBll planosTO;
        protected clAcessosVigenciaSituacaoBll situacaoBll = new 
            clAcessosVigenciaSituacaoBll();
        protected AcessosVigenciaSituacao situacaoDcl;
        protected PlanosAssinatura planosAssinaturaDcl;
        protected clPlanosAssinaturasBll planosAssinaturaBll = new clPlanosAssinaturasBll();
        protected TOAcessosVigenciaSituacaoBll situacaoTO;
        protected List<TOTela3Bll> listagemPlanosAssinaturas;
        
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/AppMenuGeral.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (acessosBll.Permissao(
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "7.0",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        TOToastr _toastr = (TOToastr)Session["ToastrTutores"];

                        if (_toastr != null)
                        {
                            MostraToastr(_toastr);
                        }

                        lblAno.Text = DateTime.Today.ToString("yyyy");
                        ViewState["listagemPlanosAssinaturas"] = planosAssinaturaBll.Listar();

                        ViewState["pagAtual"] = 1;
                        ViewState["pagTamanho"] = 9;

                        Paginar(1);
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaGrid(string _pesquisa, int _pagAtual, int _pagTamanho,
            bool? _ativo)
        {
            List<TOPessoasBll> assinantesListagem = PessoaBll.ListarClientes(
                _pesquisa, _pagTamanho, _pagAtual, _ativo);
         

            rptListagemAssinantes.DataSource = assinantesListagem;
            rptListagemAssinantes.DataBind();
        }

        protected void rptListagemAssinantes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "bloquear":
                    {
                        pessoaDcl = PessoaBll.Carregar(_idPessoa);
                        pessoaDcl.Bloqueado = !pessoaDcl.Bloqueado;

                        pessoaDcl.Ativo = true;
                        pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name);
                        pessoaDcl.DataCadastro = DateTime.Now;
                        pessoaDcl.IP = Request.UserHostAddress;

                        bllRetorno alterarRet = PessoaBll.Alterar(pessoaDcl);

                        if (alterarRet.retorno)
                        {
                            Paginar(Funcoes.Funcoes.ConvertePara.Int(
                                ViewState["pagAtual"]));

                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Success,
                                alterarRet.mensagem,
                                "Informe NutroVET - Alteração",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Warning,
                                alterarRet.mensagem,
                                "Informe NutroVET - Alteração",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }

                        break;
                    }
                case "desbloquear":
                    {
                        bllRetorno retPessoa, retAcessos;
                        bllRetorno[] retSituacao = new bllRetorno[3];

                        pessoaDcl = PessoaBll.Carregar(_idPessoa);

                        //Desbloqueia Usuário Novo
                        pessoaDcl.Bloqueado = false;
                        pessoaDcl.Ativo = true;
                        pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name);
                        pessoaDcl.DataCadastro = DateTime.Now;
                        pessoaDcl.IP = Request.UserHostAddress;

                        retPessoa = PessoaBll.Alterar(pessoaDcl);

                        //Revisar, pois dá para Mandar tudo através de Inlcusão em Cascata
                        if (retPessoa.retorno)
                        {
                            //Pega Id do Plano Vigente
                            planosDcl = planosBll.CarregarPlanoNovo(_idPessoa);

                            if (planosDcl != null)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    situacaoDcl = new AcessosVigenciaSituacao();

                                    situacaoDcl.IdVigencia = planosDcl.IdVigencia;
                                    situacaoDcl.IdSituacao = i + 1;
                                    situacaoDcl.DataSituacao = DateTime.Today;

                                    situacaoDcl.Ativo = true;
                                    situacaoDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                                        User.Identity.Name);
                                    situacaoDcl.DataCadastro = DateTime.Now;
                                    situacaoDcl.IP = Request.UserHostAddress;

                                    retSituacao[i] = situacaoBll.Inserir(situacaoDcl);
                                }

                                if (!acessosBll.PossuiAcesso(_idPessoa))
                                {
                                    acessosDcl = new Acesso();

                                    acessosDcl.IdPessoa = _idPessoa;
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
                                    acessosDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                                        User.Identity.Name);
                                    acessosDcl.DataCadastro = DateTime.Now;
                                    acessosDcl.IP = Request.UserHostAddress;

                                    retAcessos = acessosBll.Inserir(acessosDcl);

                                    if (retAcessos.retorno)
                                    {
                                        Paginar(Funcoes.Funcoes.ConvertePara.Int(
                                            ViewState["pagAtual"]));

                                        Funcoes.Funcoes.Toastr.ShowToast(this,
                                            Funcoes.Funcoes.Toastr.ToastType.Success,
                                            "Desbloqueio Efetuado com Sucesso!!!",
                                            "Informe NutroVET - Inserção",
                                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                            true);
                                    }
                                    else
                                    {
                                        Funcoes.Funcoes.Toastr.ShowToast(this,
                                            Funcoes.Funcoes.Toastr.ToastType.Error,
                                            retAcessos.mensagem,
                                            "Informe NutroVET - Inserção",
                                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                            true);
                                    }
                                }
                                else
                                {
                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Error,
                                        retSituacao[0].mensagem + " // " +
                                        retSituacao[1].retorno,
                                        "Informe NutroVET - Inserção",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                } 
                            }
                            else
                            {
                                Funcoes.Funcoes.Toastr.ShowToast(this,
                                    Funcoes.Funcoes.Toastr.ToastType.Error,
                                    "Plano NÃO Encontrado!!!",
                                    "Informe NutroVET - Inserção",
                                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                    true);
                            }
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                retPessoa.mensagem,
                                "Informe NutroVET - Inserção", 
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }

                        break;
                    }

                case "salvar":
                    {
                        DropDownList ddlPeriodoPlano = (DropDownList)e.Item.FindControl(
                            "ddlPeriodoPlano");
                        DropDownList ddlTipoPlano = (DropDownList)e.Item.FindControl(
                            "ddlTipoPlano");

                        //planosDcl = planosBll.Carregar(33);
                        planosDcl = planosBll.CarregarPlanoNovo(_idPessoa);
                        
                        planosDcl.IdPlano = Funcoes.Funcoes.ConvertePara.Int(
                            ddlTipoPlano.SelectedValue);

                        switch (planosDcl.IdPlano)
                        {
                            
                            case 1://plano básico com cadastro de até 10 animais
                                {
                                    planosDcl.QtdAnim = 10;
                                    break;
                                }
                            case 2://plano intermediário com cadastro de até 20 animais
                                {
                                    planosDcl.QtdAnim = 20;
                                    break;
                                }
                            case 3://plano completo com cadastro acima de 20 animais
                                {
                                    planosDcl.QtdAnim = 700000;
                                    break;
                                }
                        }

                        planosDcl.Periodo = Funcoes.Funcoes.ConvertePara.Int(
                            ddlPeriodoPlano.SelectedValue);
                        planosDcl.DtInicial = DateTime.Today;
                        planosDcl.DtFinal = DateTime.Today.AddYears(1);

                        planosDcl.Ativo = true;
                        planosDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name);
                        planosDcl.DataCadastro = DateTime.Now;
                        planosDcl.IP = Request.UserHostAddress;

                        bllRetorno alterarRet = planosBll.Alterar(planosDcl);

                        if (alterarRet.retorno)
                        {
                            Paginar(Funcoes.Funcoes.ConvertePara.Int(
                                ViewState["pagAtual"]));

                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Success,
                                alterarRet.mensagem,
                                "Informe NutroVET - Alteração",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                alterarRet.mensagem,
                                "Informe NutroVET - Alteração",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }

                        break;
                    }
            }
        }

        private void Excluir(int _id)
        {
            if (!PessoaBll.IsClient(_id, true))
            {
                pessoaDcl = PessoaBll.Carregar(_id);
                pessoaDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                pessoaDcl.Ativo = false;

                pessoaDcl.DataCadastro = DateTime.Now;
                pessoaDcl.IP = Request.UserHostAddress;

                bllRetorno ret = PessoaBll.Excluir(pessoaDcl);

                if (ret.retorno)
                {
                    Paginar(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Success,
                        ret.mensagem, "NutroVET informa - Exclusão",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Error,
                        ret.mensagem, "NutroVET informa - Exclusão",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "Esta Conta é de Cliente !</br>Não É Possível Excluí-la!",
                        "NutroVET informa - Exclusão",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
            }
        }

        protected void lbPesq_Click(object sender, EventArgs e)
        {
            Paginar(1);
        }

        protected void lbPagPrimeira_Click(object sender, EventArgs e)
        {
            lbAnt.Visible = false;
            lbPost.Visible = true;

            lb1.CommandName = Funcoes.Funcoes.ConvertePara.String(1);
            lb1.Text = "<b><u>" + lb1.CommandName + "</u></b>";

            lb2.CommandName = Funcoes.Funcoes.ConvertePara.String(2);
            lb2.Text = lb2.CommandName;

            lb3.CommandName = Funcoes.Funcoes.ConvertePara.String(3);
            lb3.Text = lb3.CommandName;

            lb4.CommandName = Funcoes.Funcoes.ConvertePara.String(4);
            lb4.Text = lb4.CommandName;

            lb5.CommandName = Funcoes.Funcoes.ConvertePara.String(5);
            lb5.Text = lb5.CommandName;

            Paginar(1);
        }

        protected void lbPagUltima_Click(object sender, EventArgs e)
        {
            lbAnt.Visible = true;
            lbPost.Visible = false;

            int _pagTotal = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]);

            lb1.CommandName = Funcoes.Funcoes.ConvertePara.String(_pagTotal - 4);
            lb1.Text = lb1.CommandName;

            lb2.CommandName = Funcoes.Funcoes.ConvertePara.String(_pagTotal - 3);
            lb2.Text = lb2.CommandName;

            lb3.CommandName = Funcoes.Funcoes.ConvertePara.String(_pagTotal - 2);
            lb3.Text = lb3.CommandName;

            lb4.CommandName = Funcoes.Funcoes.ConvertePara.String(_pagTotal - 1);
            lb4.Text = lb4.CommandName;

            lb5.CommandName = Funcoes.Funcoes.ConvertePara.String(_pagTotal);
            lb5.Text = "<b><u>" + lb5.CommandName + "</u></b>";

            Paginar(_pagTotal);
        }

        protected void ddlPag_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["pagTotal"] = PessoaBll.TotalPaginas(tbPesq.Text,
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
            Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
            ViewState["pagAtual"] = 1;
            ViewState["pagTamanho"] = Funcoes.Funcoes.ConvertePara.Int(
                ddlPag.SelectedValue);

            lb1.CommandName = Funcoes.Funcoes.ConvertePara.String(1);
            lb1.Text = "<b><u>" + lb1.CommandName + "</u></b>";

            lb2.CommandName = Funcoes.Funcoes.ConvertePara.String(2);
            lb2.Text = lb2.CommandName;

            lb3.CommandName = Funcoes.Funcoes.ConvertePara.String(3);
            lb3.Text = lb3.CommandName;

            lb4.CommandName = Funcoes.Funcoes.ConvertePara.String(4);
            lb4.Text = lb4.CommandName;

            lb5.CommandName = Funcoes.Funcoes.ConvertePara.String(5);
            lb5.Text = lb5.CommandName;

            Paginar(1);
        }

        protected void Paginar(int _nrPag)
        {
            ViewState["pagTotal"] = PessoaBll.TotalPaginasClientes(tbPesq.Text,
                true, Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
            ViewState["pagAtual"] = _nrPag;

            int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]);
            int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTamanho"]);

            ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

            PopulaGrid(tbPesq.Text, _pagAtual, _pagTamanho, true);
        }

        private void ExibeBotoes(int _totalPag)
        {
            if (_totalPag <= 1)
            {
                lbPagPrimeira.Visible = false;
                lbAnt.Visible = false;
                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lbPost.Visible = false;
                lbPagUltima.Visible = false;
            }
            else if ((_totalPag >= 2) && (_totalPag <= 6))
            {
                lbPagPrimeira.Visible = true;
                lbAnt.Visible = true;
                lb1.Visible = true;
                lb2.Visible = (_totalPag < 2 ? false : true);
                lb3.Visible = (_totalPag < 3 ? false : true);
                lb4.Visible = (_totalPag < 4 ? false : true);
                lb5.Visible = (_totalPag < 5 ? false : true);
                lbPost.Visible = false;
                lbPagUltima.Visible = false;
            }
            else if (_totalPag > 6)
            {
                lbPagPrimeira.Visible = true;
                lbAnt.Visible = true;
                lb1.Visible = true;
                lb2.Visible = true;
                lb3.Visible = true;
                lb4.Visible = true;
                lb5.Visible = true;
                lbPost.Visible = true;
                lbPagUltima.Visible = true;
            }
        }

        protected void lb1_Click(object sender, EventArgs e)
        {
            lb1.Text = "<b><u>" + lb1.CommandName + "</u></b>";
            lb2.Text = lb2.CommandName;
            lb3.Text = lb3.CommandName;
            lb4.Text = lb4.CommandName;
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName));
        }

        protected void lb2_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = "<b><u>" + lb2.CommandName + "</u></b>";
            lb3.Text = lb3.CommandName;
            lb4.Text = lb4.CommandName;
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb2.CommandName));
        }

        protected void lb3_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = lb2.CommandName;
            lb3.Text = "<b><u>" + lb3.CommandName + "</u></b>";
            lb4.Text = lb4.CommandName;
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb3.CommandName));
        }

        protected void lb4_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = lb2.CommandName;
            lb3.Text = lb3.CommandName;
            lb4.Text = "<b><u>" + lb4.CommandName + "</u></b>";
            lb5.Text = lb5.CommandName;

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb4.CommandName));
        }

        protected void lb5_Click(object sender, EventArgs e)
        {
            lb1.Text = lb1.CommandName;
            lb2.Text = lb2.CommandName;
            lb3.Text = lb3.CommandName;
            lb4.Text = lb4.CommandName;
            lb5.Text = "<b><u>" + lb5.CommandName + "</u></b>";

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName));
        }

        protected void lbPost_Click(object sender, EventArgs e)
        {
            lbAnt.Visible = true;

            if (Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName) + 1 <=
                Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]))
            {
                lb1.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName) + 1);
                lb1.Text = lb1.CommandName;

                lb2.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb2.CommandName) + 1);
                lb2.Text = lb2.CommandName;

                lb3.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb3.CommandName) + 1);
                lb3.Text = lb3.CommandName;

                lb4.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb4.CommandName) + 1);
                lb4.Text = lb4.CommandName;

                lb5.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName) + 1);
                lb5.Text = "<b><u>" + lb5.CommandName + "</u></b>";
            }
            else
            {
                lbPost.Enabled = false;
            }

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName));
        }

        protected void lbAnt_Click(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName) > 1)
            {
                lb1.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName) - 1);
                lb1.Text = "<b><u>" + lb1.CommandName + "</u></b>";

                lb2.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb2.CommandName) - 1);
                lb2.Text = lb2.CommandName;

                lb3.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb3.CommandName) - 1);
                lb3.Text = lb3.CommandName;

                lb4.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb4.CommandName) - 1);
                lb4.Text = lb4.CommandName;

                lb5.CommandName = Funcoes.Funcoes.ConvertePara.String(
                    Funcoes.Funcoes.ConvertePara.Int(lb5.CommandName) - 1);
                lb5.Text = lb5.CommandName;
            }
            else
            {
                lbAnt.Visible = false;
            }

            Paginar(Funcoes.Funcoes.ConvertePara.Int(lb1.CommandName));
        }

        protected void rptListagemAssinantes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) ||
                (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                //Colocado
                listagemPlanosAssinaturas = (List<TOTela3Bll>)ViewState["listagemPlanosAssinaturas"];
                pessoasTO = (TOPessoasBll)e.Item.DataItem;

                Label lblAssinanteSenha = (Label)e.Item.FindControl("lblSenhaAssinanteRegistro");
                Label lblAssinanteDataInicio = (Label)e.Item.FindControl("lblAssinanteDataInicio");
                Label lbAssinanteDataFim = (Label)e.Item.FindControl("lbAssinanteDataFim");
                Label lblAssinanteAtivo = (Label)e.Item.FindControl("lblAtivoAssinanteRegistro");
                Label lblbloqueadoRegistro = (Label)e.Item.FindControl("lblbloqueadoRegistro");
                LinkButton lbBloquearAssinante = (LinkButton)e.Item.FindControl("lbBloquearAssinante");
                LinkButton lbDesbloquearAssinante = (LinkButton)e.Item.FindControl(
                    "lbDesbloquearAssinante");

                DropDownList ddlTipoPlano = (DropDownList)e.Item.FindControl("ddlTipoPlano");
                ddlTipoPlano.DataTextField = "Nome";
                ddlTipoPlano.DataValueField = "Id";
                ddlTipoPlano.DataSource = listagemPlanosAssinaturas;
                ddlTipoPlano.DataBind();

                DropDownList ddlPeriodoPlano = (DropDownList)e.Item.FindControl("ddlPeriodoPlano");
                ddlPeriodoPlano.Items.AddRange(planosAssinaturaBll.ListarPeriodo());
                ddlPeriodoPlano.DataBind();
                ddlPeriodoPlano.Items.Insert(0, new ListItem("- Escolha -", "0"));

                //Método trás o plano atual da pessoa
                planosDcl = planosBll.CarregarPlanoNovo(pessoasTO.IdPessoa);
                planosAssinaturaDcl = planosAssinaturaBll.Carregar(planosDcl.IdPlano);
                
                //Coloca os valores nos combos
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlTipoPlano, planosDcl.IdPlano);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlPeriodoPlano, planosDcl.Periodo);

                lblAssinanteSenha.Text = Funcoes.Funcoes.Seguranca.Descriptografar(
                    pessoasTO.Senha);

                lblAssinanteDataInicio.Text = planosDcl.DtInicial.ToShortDateString();
                lbAssinanteDataFim.Text = (Funcoes.Funcoes.ConvertePara.Bool(pessoasTO.PlanoVencido) ?
                    "<span class='badge-danger'>Plano Vencido</span>" :
                    planosDcl.DtFinal.ToShortDateString());

                if (Funcoes.Funcoes.ConvertePara.Bool(pessoasTO.Ativo))
                {
                    lblAssinanteAtivo.Text = @"
                        <span class='label label-primary-nutrovet' style='width: 60px'>Sim</span>";
                }
                else
                {
                    lblAssinanteAtivo.Text = @"
                        <span class='label label-danger' style='width: 60px'> Não</span>";
                }

                if (Funcoes.Funcoes.ConvertePara.Bool(pessoasTO.Bloqueado))
                {
                    lblbloqueadoRegistro.Text = @"
                        <span class='label label-danger' style='width: 60px'>Sim</span>";

                    if (Funcoes.Funcoes.ConvertePara.Bool(pessoasTO.NewUser))
                    {
                        lbDesbloquearAssinante.Text = @"
                            <i class='fas fa-check - square'></i> Liberar";
                        lbDesbloquearAssinante.CssClass = @"
                            btn btn-primary-nutrovet btn-xs";
                        lbDesbloquearAssinante.ToolTip = "Liberar Assinante";

                        lbBloquearAssinante.Visible = false;
                        lbDesbloquearAssinante.Visible = true;
                    }
                    else
                    {
                        lbBloquearAssinante.Text = @"
                            <i class='fas fa-check-circle'></i> Desbloq.";
                        lbBloquearAssinante.CssClass = @"
                            btn custom btn-primary-nutrovet btn-xs";
                        lbBloquearAssinante.ToolTip = "Desbloquear Assinante";

                        lbBloquearAssinante.Visible = true;
                        lbDesbloquearAssinante.Visible = false;
                    }
                }
                else
                {
                    lblbloqueadoRegistro.Text = @"
                        <span class='label label-primary-nutrovet' 
                            style='width: 60px'>Não</span>";

                    lbBloquearAssinante.Visible = true;
                    lbBloquearAssinante.Text = @"
                            <i class='fas fa-ban'></i> Bloquear";
                    lbBloquearAssinante.CssClass = @"btn custom btn-danger btn-xs";
                    lbBloquearAssinante.ToolTip = "Bloquear Assinante";

                    lbDesbloquearAssinante.Visible = false;
                }
            }
        }

        private void MostraToastr(TOToastr toastr)
        {
            switch (toastr.Tipo)
            {
                case 'E':
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Error, toastr.Mensagem,
                            toastr.Titulo, Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);

                        break;
                    }
                case 'I':
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Info, toastr.Mensagem,
                            toastr.Titulo, Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);

                        break;
                    }
                case 'S':
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Success, toastr.Mensagem,
                            toastr.Titulo, Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);

                        break;
                    }
                case 'W':
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Warning, toastr.Mensagem,
                            toastr.Titulo, Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);

                        break;
                    }
            }

            Session.Remove("ToastrTutores");
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {

        }

    }
}
