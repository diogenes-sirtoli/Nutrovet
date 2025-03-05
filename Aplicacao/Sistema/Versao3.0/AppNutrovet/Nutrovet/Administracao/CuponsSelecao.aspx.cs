using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;
using System.Collections;

namespace Nutrovet.Administracao
{
    public partial class CumponsSelecao : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll PessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected clAcessosVigenciaCupomBll cupomBll = new clAcessosVigenciaCupomBll();
        protected AcessosVigenciaCupomDesconto cupomDcl;
        protected TOAcessosVigenciaCupomBll cupomTO;
        protected clPlanosAssinaturasBll planosBll = new clPlanosAssinaturasBll();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = "~/AppMenuGeral.Master";
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
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "1.3.4",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        ViewState["pagAtual"] = 1;
                        ViewState["pagTamanho"] = 10;

                        PopulaTipos();
                        PopulaProfessores();
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

        private void PopulaProfessores()
        {
            ddlProfessores.DataTextField = "Nome";
            ddlProfessores.DataValueField = "Nome";
            ddlProfessores.DataSource = cupomBll.ListarProfessores();
            ddlProfessores.DataBind();

            ddlProfessores.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaTipos()
        {
            ddlTpPlano.Items.AddRange(planosBll.ListarTipoPlano());
            ddlTpPlano.DataBind();

            ddlTpPlano.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            DominiosBll.AcoesCrud acoesCrud = (DominiosBll.AcoesCrud)ViewState["acoesCrud"];

            Salvar(acoesCrud, Funcoes.Funcoes.ConvertePara.Int(hfID.Value));
        }

        protected void rptCupom_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "inserir":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(0);
                        LimparTela();

                        lblTituloModal.Text = "Inserir Novo Cupom";
                        tbxCupom.Text = cupomBll.GerarNumeroCupom();
                        divNrCupom.Visible = true;
                        divQuantCupons.Visible = false;
                        meDtIni.Text = DateTime.Today.ToString("dd/MM/yyyy");

                        ViewState["acoesCrud"] = DominiosBll.AcoesCrud.Inserir;
                        popUpModal.Show();

                        break;
                    }
                case "editar":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(_id);
                        cupomTO = cupomBll.CarregarTO(_id);
                        ViewState["acoesCrud"] = DominiosBll.AcoesCrud.Alterar;

                        if (Funcoes.Funcoes.ConvertePara.Bool(cupomTO.fUsado))
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Success,
                                "Não é Possível Editar um Cumpom já Utilizado!!!", 
                                "NutroVET Informa - Alteração",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }
                        else
                        {
                            divNrCupom.Visible = true;
                            divQuantCupons.Visible = false;
                            
                            tbxCupom.Text = cupomTO.NrCupom;
                            Funcoes.Funcoes.ControlForm.SetComboBox(ddlTpPlano, 
                                cupomTO.dPlanoTp);

                            meDtIni.Text = (cupomTO.DtInicial <= DateTime.Parse("01/01/1910") ?
                                "" : cupomTO.DtInicial.ToString("dd/MM/yyyy"));
                            meDtFinal.Text = (cupomTO.DtFinal <= DateTime.Parse("01/01/1910") ?
                                "" : cupomTO.DtFinal.ToString("dd/MM/yyyy"));
                            cbxAcesLib.Checked = Funcoes.Funcoes.ConvertePara.Bool(
                                cupomTO.fAcessoLiberado);
                            txtProfessor.Text = cupomTO.Professor;

                            popUpModal.Show();
                        }

                        break;
                    }
                case "gerar":
                    {
                        hfID.Value = Funcoes.Funcoes.ConvertePara.String(0);
                        LimparTela();

                        lblTituloModal.Text = "Gerador de Cupons";
                        divNrCupom.Visible = false;
                        divQuantCupons.Visible = true;
                        ViewState["acoesCrud"] = DominiosBll.AcoesCrud.Gerar;
                        meDtIni.Text = DateTime.Today.ToString("dd/MM/yyyy");

                        popUpModal.Show();

                        break;
                    }
                case "excluir":
                    {
                        ViewState["acoesCrud"] = DominiosBll.AcoesCrud.Excluir;

                        Excluir(_id);

                        break;
                    }
            }
        }

        private void LimparTela()
        {
            tbxCupom.Text = "";
            meQuantCupons.Text = "";
            Funcoes.Funcoes.ControlForm.SetComboBox(ddlTpPlano, 0);
            meDtIni.Text = "";
            meDtFinal.Text = "";
            txtProfessor.Text = "";
            cbxAcesLib.Checked = false;
        }

        private void Excluir(int _id)
        {
            cupomDcl = cupomBll.Carregar(_id);

            bllRetorno ret = cupomBll.Excluir(cupomDcl);

            if (ret.retorno)
            {
                ViewState.Remove("acoesCrud");
                Paginar(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    ret.mensagem, "NutroVET Informa - Exclusão",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    ret.mensagem, "NutroVET Informa - Exclusão",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void Salvar(DominiosBll.AcoesCrud _acoesCrud, int _id)
        {
            switch (_acoesCrud)
            {
                case DominiosBll.AcoesCrud.Inserir:
                    {
                        Inserir();

                        break;
                    }
                case DominiosBll.AcoesCrud.Alterar:
                    {
                        if (_id > 0)
                        {
                            Alterar(_id); 
                        }

                        break;
                    }
                case DominiosBll.AcoesCrud.Gerar:
                    {
                        Gerar();

                        break;
                    }
            }
        }

        private void Gerar()
        {
            int _quant = Funcoes.Funcoes.ConvertePara.Int(meQuantCupons.Text);
            int _idTipoDesc = Funcoes.Funcoes.ConvertePara.Int(ddlTpPlano.SelectedValue);
            DateTime _dtInicial = (meDtIni.Text != "" ? DateTime.Parse(meDtIni.Text) :
                DateTime.Today);
            DateTime _dtFinal = (meDtFinal.Text != "" ? DateTime.Parse(meDtFinal.Text) :
                _dtInicial.AddYears(1));
            string _professor = txtProfessor.Text;
            bool _fAcessoLiberado = cbxAcesLib.Checked;

            ArrayList _total = cupomBll.GerarVouchers(_quant, _idTipoDesc, 
                _dtInicial, _dtFinal, _professor, _fAcessoLiberado);

            if (_total.Count > 0)
            {
                Paginar(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                if (_professor != "")
                {
                    PopulaProfessores();
                }

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, 
                    "Vouchers Criados com Sucesso!!!",
                    "NutroVET Informa - Gerador",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, 
                    "Não foi Possível Criar Todos os Vouchers!!!",
                    "NutroVET Informa - Gerador",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Inserir()
        {
            cupomDcl = new AcessosVigenciaCupomDesconto();

            cupomDcl.NrCumpom = tbxCupom.Text;
            cupomDcl.dPlanoTp = Funcoes.Funcoes.ConvertePara.Int(ddlTpPlano.SelectedValue);
            cupomDcl.DtInicial = (meDtIni.Text != "" ? DateTime.Parse(meDtIni.Text) :
                DateTime.Parse("01/01/1910"));
            cupomDcl.DtFinal = (meDtFinal.Text != "" ? DateTime.Parse(meDtFinal.Text) :
                DateTime.Parse("01/01/1910"));
            cupomDcl.fUsado = false;
            cupomDcl.Professor = txtProfessor.Text;
            cupomDcl.fAcessoLiberado = cbxAcesLib.Checked;

            cupomDcl.Ativo = true;
            cupomDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            cupomDcl.DataCadastro = DateTime.Now;
            cupomDcl.IP = Request.UserHostAddress;

            bllRetorno inserirRet = cupomBll.Inserir(cupomDcl);

            if (inserirRet.retorno)
            {
                LimparTela();
                ViewState.Remove("acoesCrud");

                if (cupomDcl.Professor != "")
                {
                    PopulaProfessores();
                }

                Paginar(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, inserirRet.mensagem,
                    "NutroVET Informa - Inserção", 
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                popUpModal.Show();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                    "NutroVET Informa - Inserção", 
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Alterar(int _id)
        {
            cupomDcl = cupomBll.Carregar(_id);

            cupomDcl.dPlanoTp = Funcoes.Funcoes.ConvertePara.Int(ddlTpPlano.SelectedValue);
            cupomDcl.DtInicial = (meDtIni.Text != "" ? DateTime.Parse(meDtIni.Text) :
                DateTime.Parse("01/01/1910"));
            cupomDcl.DtFinal = (meDtFinal.Text != "" ? DateTime.Parse(meDtFinal.Text) :
                DateTime.Parse("01/01/1910"));
            cupomDcl.fUsado = false;
            cupomDcl.Professor = txtProfessor.Text;
            cupomDcl.fAcessoLiberado = cbxAcesLib.Checked;

            cupomDcl.Ativo = true;
            cupomDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            cupomDcl.DataCadastro = DateTime.Now;
            cupomDcl.IP = Request.UserHostAddress;

            bllRetorno alterarRet = cupomBll.Alterar(cupomDcl);

            if (alterarRet.retorno)
            {
                LimparTela();
                ViewState.Remove("acoesCrud");

                if (cupomDcl.Professor != "")
                {
                    PopulaProfessores();
                }

                Paginar(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]));

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, alterarRet.mensagem,
                    "NutroVET Informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                popUpModal.Show();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, alterarRet.mensagem,
                    "NutroVET Informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void lbPesq_Click(object sender, EventArgs e)
        {
            Paginar(1);
        }

        protected void Paginar(int _nrPag)
        {
            string _professor = "";

            if (_nrPag > 0)
            {
                if (ddlProfessores.SelectedIndex > 0)
                {
                    _professor = ddlProfessores.SelectedValue;
                }

                ViewState["pagTotal"] = cupomBll.TotalPaginas(tbPesq.Text, _professor,
                    Funcoes.Funcoes.ConvertePara.Int(rblUsados.SelectedValue), 
                    Funcoes.Funcoes.ConvertePara.Int(ddlPag.SelectedValue));
                ViewState["pagAtual"] = _nrPag;

                int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagAtual"]);
                int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTamanho"]);

                ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

                PopulaGrid(tbPesq.Text, _professor, _pagAtual, _pagTamanho);
            }
            else
            {
                ViewState["pagTotal"] = 0;
                ViewState["pagAtual"] = _nrPag;

                int _pagAtual = Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["pagAtual"]);
                int _pagTamanho = Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["pagTamanho"]);

                ExibeBotoes(Funcoes.Funcoes.ConvertePara.Int(ViewState["pagTotal"]));

                rptCupom.DataSource = null;
                rptCupom.DataBind();
            }
        }

        private void PopulaGrid(string _pesquisa, string _professor, int _pagAtual, 
            int _pagTamanho)
        {
            List<TOAcessosVigenciaCupomBll> cuponsListagem = cupomBll.Listar(_pesquisa,
                 _professor, Funcoes.Funcoes.ConvertePara.Int(rblUsados.SelectedValue), 
                 _pagTamanho, _pagAtual);

            rptCupom.DataSource = cuponsListagem;
            rptCupom.DataBind();
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
            else if ((_totalPag >= 2) && (_totalPag < 6))
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
            else if (_totalPag >= 6)
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
            string _professor = "";

            if (ddlProfessores.SelectedIndex > 0)
            {
                _professor = ddlProfessores.SelectedValue;
            }

            ViewState["pagTotal"] = cupomBll.TotalPaginas(tbPesq.Text, _professor,
                Funcoes.Funcoes.ConvertePara.Int(rblUsados.SelectedValue),
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

        protected void rptCupom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) ||
                (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Label lblTipoPlanoTemp = (Label)e.Item.FindControl("lblTipoPlanoTemp");
                Label lblDtInicioTemp = (Label)e.Item.FindControl("lblDtInicioTemp");
                Label lblDtFimTemp = (Label)e.Item.FindControl("lblDtFimTemp");
                Label lblUsadoTemp = (Label)e.Item.FindControl("lblUsadoTemp");
                Label lblAcessoLibTemp = (Label)e.Item.FindControl("lblAcessoLibTemp");
                LinkButton lbEditarCupom = (LinkButton)e.Item.FindControl("lbEditarCupom");
                LinkButton lbExcluir = (LinkButton)e.Item.FindControl("lbExcluir");

                cupomTO = (TOAcessosVigenciaCupomBll)e.Item.DataItem;

                lblDtInicioTemp.Text = (cupomTO.DtInicial <= DateTime.Parse("01/01/1910") ? "" : 
                    cupomTO.DtInicial.ToString("dd/MM/yyyy"));
                lblDtFimTemp.Text = (cupomTO.DtFinal <= DateTime.Parse("01/01/1910") ? "" : 
                    cupomTO.DtFinal.ToString("dd/MM/yyyy"));
                lblUsadoTemp.Text = (Funcoes.Funcoes.ConvertePara.Bool(cupomTO.fUsado) ? 
                    "Sim" : "Não");
                lblAcessoLibTemp.Text = (Funcoes.Funcoes.ConvertePara.Bool(
                    cupomTO.fAcessoLiberado) ? "Sim" : "Não");
                lblTipoPlanoTemp.Text = (cupomTO.TipoPlano != null ? cupomTO.TipoPlano : 
                    "Voucher Antigo");

                if (Funcoes.Funcoes.ConvertePara.Bool(cupomTO.fUsado))
                {
                    lbEditarCupom.Visible = false;
                    lbExcluir.Visible = false;
                }
            }
        }
    }
}