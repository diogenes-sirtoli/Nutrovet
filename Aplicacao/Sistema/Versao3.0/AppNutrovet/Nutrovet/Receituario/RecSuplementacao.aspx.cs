using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using System.Web.Security;
using MaskEdit;

namespace Nutrovet.Receituario
{
    public partial class RecSuplementacao : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll assinanteBll = new clPessoasBll();
        protected Pessoa assinanteDCL;
        protected clAnimaisBll animaisBll = new clAnimaisBll();
        protected Animai animaisDcl;
        protected clCardapioBll cardapioBll = new clCardapioBll();
        protected DCL.Cardapio cardapioDcl;
        protected TOCardapioBll cardapioTO;
        protected clConfigReceituarioBll configReceitBll = new clConfigReceituarioBll();
        protected ConfigReceituario configReceitDcl;
        protected clExigenciasNutrBll exigenciasNutrBll = new clExigenciasNutrBll();
        protected ExigenciasNutricionai exigenciasNutrDcl;
        protected clReceituarioBll receituarioBll = new clReceituarioBll();
        protected DCL.Receituario receituarioDcl;
        protected TOReceituarioBll receituarioTO;
        protected clReceituarioNutrientesBll recNutrBll = new clReceituarioNutrientesBll();
        protected ReceituarioNutriente recNutrDcl;
        protected TOReceituarioNutrientesBll recNutrTO;
        protected List<TOReceituarioNutrientesBll> listRecNutrTO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!User.Identity.IsAuthenticated) || (Session["Receituario"] == null))
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    lblAno.Text = DateTime.Today.ToString("yyyy");

                    int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(
                        User.Identity.Name);
                    int _idCardapio = Funcoes.Funcoes.ConvertePara.Int(
                        Funcoes.Funcoes.Seguranca.Descriptografar(
                            Funcoes.Funcoes.ConvertePara.String(
                                Request.QueryString["_idCardapio"])));
                    hlCardapio.NavigateUrl = 
                        "~/Cardapio/CardapioCadastro.aspx?_idCardapio=" +
                            Funcoes.Funcoes.ConvertePara.String(
                                Request.QueryString["_idCardapio"]);

                    ViewState["_idCardapio"] = _idCardapio;
                    ViewState["_idReceita"] = 0;

                    PopulaTela(_idCardapio);
                    PopulaCabecalhoReceita(_idPessoa);
                    PopularLogo(_idPessoa);
                    PopularAssinatura(_idPessoa);

                    string x = Funcoes.Funcoes.ManterPosicaoSelecionadaGridView(
                            gridRepeater.ClientID);
                    ClientScript.RegisterStartupScript(this.GetType(), "tt", x);

                    PopulaNutrientesReceita(0);
                }
            }
        }

        private void PopularLogo(int idPessoa)
        {
            if ((imgLogo.ImageUrl == "") || 
                (imgLogo.ImageUrl == "~/Perfil/Logotipos/logo_receita.png"))
            {
                imgLogo.ImageUrl = configReceitBll.CarregarImgLogo(idPessoa);
            }
        }

        private void PopularAssinatura(int idPessoa)
        {
            if ((imgAssinatura.ImageUrl == "") ||
                (imgAssinatura.ImageUrl == "~/Perfil/Assinaturas/assinatura_receita.png"))
            {
                imgAssinatura.ImageUrl = configReceitBll.CarregarImgAssinatura(idPessoa);
            }
        }

        private void PopulaTela(int idCardapio)
        {
            cardapioTO = cardapioBll.CarregarTO(idCardapio);

            lblPaciente.Text = cardapioTO.Animal;
            lblPeso.Text = Funcoes.Funcoes.ConvertePara.String(cardapioTO.PesoAtual) + " Kg(s)";
            lblEspecie.Text = cardapioTO.Especie;
            lblSexo.Text = cardapioTO.Sexo;
            lblRaca.Text = cardapioTO.Raca;
            lblIdade.Text = Funcoes.Funcoes.ConvertePara.String(cardapioTO.Idade) + " ano(s)";
            lblTutor.Text = cardapioTO.Tutor;
            lblEMailTutor.Text = cardapioTO.TutorEMail;
            lblFoneTutor.Text = cardapioTO.TutorFone;
        }

        private void PopulaCabecalhoReceita(int idPessoa)
        {
            configReceitDcl = configReceitBll.Carregar(idPessoa);
            
            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                lblNomeClin.Text = (configReceitDcl.NomeClinica != "" ? 
                    configReceitDcl.NomeClinica : "");
                lblSlogan.Text = (configReceitDcl.Slogan != "" ? configReceitDcl.Slogan : "");
                //hlkSite.Text = configReceitDcl.Site;
                //tbFacebookclinica.Text = configReceitDcl.Facebook;
                //tbTwitterClinica.Text = configReceitDcl.Twitter;
                //tbInstagramClinica.Text = configReceitDcl.Instagram;
                //tbCrvm.Text = configReceitDcl.CRMV;
                lblEndereco.Text = configReceitBll.MontaCamposEndereco(configReceitDcl);
                lblEMail.Text = (configReceitDcl.Email != "" ? configReceitDcl.Email : "");
                lblTelefone.Text = 
                    (configReceitDcl.Telefone != "" ? configReceitDcl.Telefone + 
                        (configReceitDcl.Celular != "" ? " / " + configReceitDcl.Celular : "") :
                             (configReceitDcl.Celular != "" ? configReceitDcl.Celular : ""));
                lblLocalData.Text = (configReceitDcl.Logr_Cidade != "" ? 
                    configReceitDcl.Logr_Cidade + ", " : "") + DateTime.Today.ToString("D");

                assinanteDCL = assinanteBll.Carregar(Funcoes.Funcoes.ConvertePara.Int(
                                            User.Identity.Name));
                lblNomeVeterinario.Text = assinanteDCL.Nome;
                lblTituloECRMV.Text =
                    (Funcoes.Funcoes.ConvertePara.Bool(configReceitDcl.fLivreRodape) ?
                        configReceitDcl.LivreRodape :
                            ("Médico(a) Veterinário(a) - CRMV" +
                                (configReceitDcl.CrmvUf != null &&
                                 configReceitDcl.CrmvUf != "" &&
                                 configReceitDcl.CrmvUf != "0" ?
                                    "/" + configReceitDcl.CrmvUf + " " :
                                    "") + configReceitDcl.CRMV
                             )
                     );

                if (Funcoes.Funcoes.ConvertePara.Bool(configReceitDcl.fLivreCabecalho))
                {
                    divCabecalhoGrande.Visible = false;
                    divCabecalhoSlim.Visible = true;

                    lblCabecalhoSlim.Text = configReceitDcl.LivreCabecalho;
                }
                else
                {
                    divCabecalhoGrande.Visible = true;
                    divCabecalhoSlim.Visible = false;

                    lblCabecalhoSlim.Text = "";
                }
            }
            else
            {
                lblSlogan.Text = "(Configure os Dados da Receita em Perfil > Aba Receituário)";
                lblEndereco.Text = "";
                lblEMail.Text = "";
                lblTelefone.Text = "";
                lblLocalData.Text = "";
            }
        }

        private void PopulaNutrientesReceita(int _idReceita)
        {
            if (_idReceita > 0)
            {
                listRecNutrTO = recNutrBll.ListarTO(_idReceita);
            }
            else
            {
                listRecNutrTO = ((List<TOReceituarioNutrientesBll>)Session["Receituario"]).
                    OrderBy(o => o.Nutriente).ToList();
            }

            rptReceitaSupl.DataSource = listRecNutrTO;
            rptReceitaSupl.DataBind();
        }

        protected void rptReceitaSupl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    { 
                        CheckBox cbxIncldiet = (CheckBox)e.Item.FindControl("cbxIncldiet");
                        Label lblConstaReceita = (Label)e.Item.FindControl("lblConstaReceita");
                        Label lblFaltaReceita = (Label)e.Item.FindControl("lblFaltaReceita");
                        Label lblDoseReceita = (Label)e.Item.FindControl("lblDoseReceita");
                        MEdit meQtdFalta = (MEdit)e.Item.FindControl("meQtdFalta");
                        LinkButton lbSalvar = (LinkButton)e.Item.FindControl("lbSalvar");

                        recNutrTO = (TOReceituarioNutrientesBll)e.Item.DataItem;

                        cbxIncldiet.Checked = Funcoes.Funcoes.ConvertePara.Bool(
                            recNutrTO.EmReceita);
                        lblConstaReceita.Text = string.Format("{0:#,##0.00#}", recNutrTO.Consta);
                        lblFaltaReceita.Text = string.Format("{0:#,##0.00#}", recNutrTO.Falta);
                        lblDoseReceita.Text = string.Format("{0:#,##0.00#}", recNutrTO.Dose);

                        if (cbxIncldiet.Checked)
                        {
                            lblDoseReceita.Visible = false;
                            meQtdFalta.Visible = true;

                            lbSalvar.Visible = (Funcoes.Funcoes.ConvertePara.Int(
                                ViewState["_idReceita"]) > 0 ? true : false);
                        }
                        else
                        {
                            lblFaltaReceita.Visible = true;
                            meQtdFalta.Visible = false;
                            lbSalvar.Visible = false;
                        }

                        meQtdFalta.Text = string.Format("{0:#,##0.00#}", recNutrTO.Dose);

                        break;
                    }
            }
        }

        protected void cbxIncldiet_CheckedChanged(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idReceita"]) > 0)
            {
                bllRetorno updateRet;
                listRecNutrTO = recNutrBll.ListarTO(Funcoes.Funcoes.ConvertePara.Int(
                    ViewState["_idReceita"]));

                foreach (RepeaterItem item in rptReceitaSupl.Items)
                {
                    HiddenField IdNutrRec = (HiddenField)item.FindControl("hfIdNutrRec");
                    CheckBox cbxIncldiet = (CheckBox)item.FindControl("cbxIncldiet");
                    MEdit meQtdFalta = (MEdit)item.FindControl("meQtdFalta");
                    int _idNutrRec = Funcoes.Funcoes.ConvertePara.Int(IdNutrRec.Value);

                    switch (item.ItemType)
                    {
                        case ListItemType.Item:
                        case ListItemType.AlternatingItem:
                            {
                                foreach (TOReceituarioNutrientesBll itemRec in listRecNutrTO)
                                {
                                    if (itemRec.IdNutrRec == _idNutrRec)
                                    {
                                        recNutrDcl = recNutrBll.Carregar(_idNutrRec);

                                        recNutrDcl.EmReceita = cbxIncldiet.Checked;
                                        recNutrDcl.Dose = (cbxIncldiet.Checked ?
                                            Funcoes.Funcoes.ConvertePara.Decimal(meQtdFalta.Text) :
                                            0);

                                        recNutrDcl.Ativo = true;
                                        recNutrDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                                            User.Identity.Name);
                                        recNutrDcl.DataCadastro = DateTime.Now;
                                        recNutrDcl.IP = Request.UserHostAddress;

                                        updateRet = recNutrBll.Alterar(recNutrDcl);

                                        if (updateRet.retorno)
                                        {
                                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                                Funcoes.Funcoes.Toastr.ToastType.Success,
                                                "Alteração Efetuada com Sucesso!!!",
                                                "NutroVET Informa - Alteração",
                                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                                true);
                                        }
                                        else
                                        {
                                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                                updateRet.mensagem,
                                                "NutroVET Informa - Alteração",
                                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                                true);
                                        }

                                        break;
                                    }
                                }

                                break;
                            }
                    }
                }
            }
            else
            {
                listRecNutrTO = ((List<TOReceituarioNutrientesBll>)Session["Receituario"]).
                    OrderBy(o => o.Nutriente).ToList();

                foreach (RepeaterItem item in rptReceitaSupl.Items)
                {
                    HiddenField hfIdIncluir = (HiddenField)item.FindControl("hfIdIncluir");
                    CheckBox cbxIncldiet = (CheckBox)item.FindControl("cbxIncldiet");
                    MEdit meQtdFalta = (MEdit)item.FindControl("meQtdFalta");
                    int _idNutr = Funcoes.Funcoes.ConvertePara.Int(hfIdIncluir.Value);

                    switch (item.ItemType)
                    {
                        case ListItemType.Item:
                        case ListItemType.AlternatingItem:
                            {
                                foreach (TOReceituarioNutrientesBll itemRec in listRecNutrTO)
                                {
                                    if (itemRec.IdNutr == _idNutr)
                                    {
                                        itemRec.EmReceita = cbxIncldiet.Checked;

                                        if (cbxIncldiet.Checked)
                                        {
                                            if ((itemRec.Falta > 0) && (itemRec.Dose <= 0))
                                            {
                                                itemRec.Dose = itemRec.Falta;
                                            }
                                            else if ((itemRec.Falta <= 0) && (itemRec.Dose <= 0))
                                            {
                                                itemRec.Dose =
                                                    Funcoes.Funcoes.ConvertePara.Decimal(
                                                        meQtdFalta.Text);
                                            }
                                            else if ((itemRec.Falta <= 0) && (itemRec.Dose > 0))
                                            {
                                                itemRec.Dose =
                                                    Funcoes.Funcoes.ConvertePara.Decimal(
                                                        meQtdFalta.Text);
                                            }
                                            else if ((itemRec.Falta > 0) && (itemRec.Dose > 0))
                                            {
                                                itemRec.Dose =
                                                    Funcoes.Funcoes.ConvertePara.Decimal(
                                                        meQtdFalta.Text);
                                            }
                                        }
                                        else
                                        {
                                            itemRec.Dose = 0;
                                        }

                                        break;
                                    }
                                }

                                break;
                            }
                    }
                }

                Session["Receituario"] = listRecNutrTO;
            }

            PopulaNutrientesReceita(Funcoes.Funcoes.ConvertePara.Int(ViewState["_idReceita"]));
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cardapio/CardapioCadastro.aspx?_idCardapio=" +
                Funcoes.Funcoes.Seguranca.Criptografar(Funcoes.Funcoes.ConvertePara.String(
                    ViewState["_idCardapio"])));
        }

        protected void lbSalvaReceSupl_Click(object sender, EventArgs e)
        {
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            configReceitDcl = configReceitBll.Carregar(_idPessoa);

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                if (Funcoes.Funcoes.ConvertePara.Int(ViewState["_idReceita"]) > 0)
                {
                    Alterar(Funcoes.Funcoes.ConvertePara.Int(ViewState["_idReceita"]));
                }
                else
                {
                    Inserir(configReceitDcl);
                } 
            }
            else
            {
                lblDescricao.Text = @"É necessário configurar os Dados Básicos da Receita no 
                    MENU:</br>    Minha Conta > Perfil > Aba Receituário!!!
                    </br></br>
                    Clique no link abaixo para abir a Tela de Perfil!
                    </br>
                    </br>";

                popUpModal.Show();
            }
        }

        private void Inserir(ConfigReceituario _configRec)
        {
            bllRetorno insertRet;

            //Cria Objeto da Receita
            receituarioDcl = new DCL.Receituario();

            receituarioDcl.IdCardapio = Funcoes.Funcoes.ConvertePara.Int(
                ViewState["_idCardapio"]);
            receituarioDcl.dTpRec = (int)DominiosBll.ReceitasAuxTipos.Suplementação;
            receituarioDcl.Titulo = tbxUso.Text;
            receituarioDcl.Veiculo = tbxVeicExcip.Text;
            receituarioDcl.QuantVeic = tbxQtdExcip.Text;
            receituarioDcl.InstrucoesReceita = tbxInstrRec.Text;
            receituarioDcl.Posologia = ftbPosologia.Text;
            receituarioDcl.Prescricao = "";
            receituarioDcl.DataReceita = DateTime.Today;
            receituarioDcl.LocalReceita = (_configRec != null ? _configRec.Logr_Cidade : "");
            receituarioDcl.NrReceita = receituarioBll.GerarNumeroArquivo();
            receituarioDcl.Arquivo = "RecSuplem_" + receituarioDcl.NrReceita + "_" + 
                DateTime.Today.ToString("yy") + ".pdf";

            receituarioDcl.Ativo = true;
            receituarioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            receituarioDcl.DataCadastro = DateTime.Now;
            receituarioDcl.IP = Request.UserHostAddress;

            //Cria Objetos dos Itens da Receita
            foreach (RepeaterItem item in rptReceitaSupl.Items)
            {
                HiddenField hfIdIncluir = (HiddenField)item.FindControl("hfIdIncluir");
                HiddenField hfIdUnid = (HiddenField)item.FindControl("hfIdUnid");
                CheckBox cbxIncldiet = (CheckBox)item.FindControl("cbxIncldiet");
                MEdit meQtdFalta = (MEdit)item.FindControl("meQtdFalta");
                Label lblFaltaReceita = (Label)item.FindControl("lblFaltaReceita");
                Label lblConstaReceita = (Label)item.FindControl("lblConstaReceita");

                int _idNutr = Funcoes.Funcoes.ConvertePara.Int(hfIdIncluir.Value);

                switch (item.ItemType)
                {
                    case ListItemType.Item:
                    case ListItemType.AlternatingItem:
                        {
                            recNutrDcl = new ReceituarioNutriente();

                            recNutrDcl.EmReceita = cbxIncldiet.Checked;
                            recNutrDcl.IdNutr = _idNutr;
                            recNutrDcl.Consta = (Funcoes.Funcoes.ConvertePara.Decimal(
                                lblConstaReceita.Text) > 0 ?
                                    Funcoes.Funcoes.ConvertePara.Decimal(
                                        lblConstaReceita.Text) : 0);
                            recNutrDcl.Falta = (Funcoes.Funcoes.ConvertePara.Decimal(
                                lblFaltaReceita.Text) > 0 ?
                                    Funcoes.Funcoes.ConvertePara.Decimal(
                                        lblFaltaReceita.Text) : 0);
                            recNutrDcl.Dose = (Funcoes.Funcoes.ConvertePara.Decimal(
                                meQtdFalta.Text) > 0 ? Funcoes.Funcoes.ConvertePara.Decimal(
                                    meQtdFalta.Text) : 0);
                            recNutrDcl.IdUnid = Funcoes.Funcoes.ConvertePara.Int(hfIdUnid.Value);
                            recNutrDcl.IdPrescr = null;

                            recNutrDcl.Ativo = true;
                            recNutrDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                                User.Identity.Name);
                            recNutrDcl.DataCadastro = DateTime.Now;
                            recNutrDcl.IP = Request.UserHostAddress;

                            receituarioDcl.ReceituarioNutrientes.Add(recNutrDcl);

                            break;
                        }
                }
            }

            insertRet = receituarioBll.Inserir(receituarioDcl);

            if (insertRet.retorno)
            {
                ViewState["_idReceita"] = receituarioDcl.IdReceita;

                hlImprReceSupl.Enabled = true;
                hlImprReceSupl.CssClass = "btn btn-primary-nutrovet";
                hlImprReceSupl.NavigateUrl =
                    "~/Receituario/Impressao/RptSuplement.aspx?_idRec=" +
                        receituarioDcl.IdReceita + "&_idCardapio=" +
                            Funcoes.Funcoes.ConvertePara.Int(ViewState["_idCardapio"]);

                PopulaNutrientesReceita(receituarioDcl.IdReceita);

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Inserção Efetuada com Sucesso!!!",
                    "NutroVET Informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                hlImprReceSupl.Enabled = false;
                hlImprReceSupl.CssClass = "btn btn-secondary";
                hlImprReceSupl.NavigateUrl = "";

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    insertRet.mensagem,
                    "NutroVET Informa - Inserção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Alterar(int _idReceita)
        {
            bllRetorno updateRet;
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            configReceitDcl = configReceitBll.Carregar(_idPessoa);

            //Cria Objeto da Receita
            receituarioDcl = receituarioBll.Carregar(_idReceita);

            receituarioDcl.Titulo = tbxUso.Text;
            receituarioDcl.InstrucoesReceita = tbxInstrRec.Text;
            receituarioDcl.Veiculo = tbxVeicExcip.Text;
            receituarioDcl.QuantVeic = tbxQtdExcip.Text;
            receituarioDcl.Posologia = ftbPosologia.Text;
            receituarioDcl.DataReceita = DateTime.Today;
            receituarioDcl.LocalReceita = (configReceitDcl != null ?
                configReceitDcl.Logr_Cidade : "");

            receituarioDcl.Ativo = true;
            receituarioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            receituarioDcl.DataCadastro = DateTime.Now;
            receituarioDcl.IP = Request.UserHostAddress;

            updateRet = receituarioBll.Alterar(receituarioDcl);

            if (updateRet.retorno)
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Alteração Efetuada com Sucesso!!!",
                    "NutroVET Informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error,
                    updateRet.mensagem,
                    "NutroVET Informa - Alteração",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void rptReceitaSupl_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            CheckBox cbxIncldiet = (CheckBox)e.Item.FindControl("cbxIncldiet");
            MEdit meQtdFalta = (MEdit)e.Item.FindControl("meQtdFalta");

            int _idNutrRec = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "salvarItem":
                    {
                        recNutrDcl = recNutrBll.Carregar(_idNutrRec);

                        recNutrDcl.EmReceita = cbxIncldiet.Checked;
                        recNutrDcl.Dose = (cbxIncldiet.Checked ?
                            Funcoes.Funcoes.ConvertePara.Decimal(meQtdFalta.Text) : 0);

                        recNutrDcl.Ativo = true;
                        recNutrDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name);
                        recNutrDcl.DataCadastro = DateTime.Now;
                        recNutrDcl.IP = Request.UserHostAddress;

                        bllRetorno _alterRet = recNutrBll.Alterar(recNutrDcl);

                        if (_alterRet.retorno)
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Success,
                                _alterRet.mensagem, "NutroVET informa - Alteração",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Error,
                                _alterRet.mensagem, "NutroVET informa - Alteração",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }

                        break;
                    }
            }
        }
    }
}