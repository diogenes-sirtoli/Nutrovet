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
    public partial class RecNutraceuticos : System.Web.UI.Page
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
        protected clReceituarioBll receituarioBll = new clReceituarioBll();
        protected DCL.Receituario receituarioDcl;
        protected TOReceituarioBll receituarioTO;
        protected clReceituarioNutrientesBll recNutrBll = new clReceituarioNutrientesBll();
        protected ReceituarioNutriente recNutrDcl;
        protected clNutraceuticosDietasBll nutracDietasBll = new clNutraceuticosDietasBll();
        protected NutraceuticosDieta nutracDietasDcl;
        protected TOReceituarioNutrientesBll recNutrTO, recNewNutrTO;
        protected List<TOReceituarioNutrientesBll> listRecNutrTO, listNewRecNutrTO;
        protected clNutrientesBll nutrientesBll = new clNutrientesBll();
        protected TONutrientesBll nutrientesTO;
        protected clPrescricaoAuxTiposBll prescrBll = new clPrescricaoAuxTiposBll();
        protected PrescricaoAuxTipo prescrDcl;

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

                    Session.Remove("Receituario");
                    ViewState["_idCardapio"] = _idCardapio;
                    ViewState["_idReceita"] = 0;

                    PopulaTela(_idCardapio);
                    PopulaCabecalhoReceita(_idPessoa);
                    PopularLogo(_idPessoa);
                    PopularAssinatura(_idPessoa);

                    string x = Funcoes.Funcoes.ManterPosicaoSelecionadaGridView(
                            gridRepeater.ClientID);
                    ClientScript.RegisterStartupScript(this.GetType(), "tt", x);

                    PopulaNutraceuticosReceita(0);
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

            lblSubTitulo.Text = "Receituário de Nutracêuticos - " + cardapioTO.Dieta;
            lblPaciente.Text = cardapioTO.Animal;
            lblPeso.Text = Funcoes.Funcoes.ConvertePara.String(cardapioTO.PesoAtual);
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

        private void PopulaNutraceuticosReceita(int _idReceita)
        {
            if (_idReceita > 0)
            {
                listRecNutrTO = recNutrBll.ListarTO(_idReceita);
            }
            else
            {
                if (Session["Receituario"] != null)
                {
                    listRecNutrTO = ((List<TOReceituarioNutrientesBll>)Session["Receituario"]).
                        OrderBy(o => o.Nutriente).ToList();
                }
                else
                {
                    int _idCardapo = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idCardapio"]);
                    cardapioTO = cardapioBll.CarregarTO(_idCardapo);

                    listRecNutrTO = nutracDietasBll.Listar(
                        Funcoes.Funcoes.ConvertePara.Int(cardapioTO.IdEspecie),
                        Funcoes.Funcoes.ConvertePara.Int(cardapioTO.IdDieta),
                        Funcoes.Funcoes.ConvertePara.String(cardapioTO.PesoAtual));

                    Session["Receituario"] = listRecNutrTO;
                }
            }

            rptReceitaNutrac.DataSource = listRecNutrTO;
            rptReceitaNutrac.DataBind();
        }

        protected void rptReceitaNutrac_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        Label lblDoseMinReceita = (Label)e.Item.FindControl("lblDoseMinReceita");
                        Label lblDoseMaxReceita = (Label)e.Item.FindControl("lblDoseMaxReceita");
                        Label lblDoseReceita = (Label)e.Item.FindControl("lblDoseReceita");
                        Label lblDoseUnidReceita = (Label)e.Item.FindControl("lblDoseUnidReceita");
                        //Label lblPesoAtualReceita = (Label)e.Item.FindControl("lblPesoAtualReceita");
                        Label lblIntervaloReceita = (Label)e.Item.FindControl("lblIntervaloReceita");

                        DropDownList ddlUnidade = (DropDownList)e.Item.FindControl("ddlUnidade");
                        DropDownList ddlIntervalo = (DropDownList)e.Item.FindControl("ddlIntervalo");
                        CheckBox cbxIncldiet = (CheckBox)e.Item.FindControl("cbxIncldiet");
                        MEdit meDose = (MEdit)e.Item.FindControl("meDose");
                        MEdit meQuant = (MEdit)e.Item.FindControl("meQuant");
                        LinkButton lbSalvar = (LinkButton)e.Item.FindControl("lbSalvar");

                        recNutrTO = (TOReceituarioNutrientesBll)e.Item.DataItem;

                        ListItem[] _unidades = (from l in nutrientesBll.ListarUnidades()
                                                where (l.Value == "1") || (l.Value == "2") ||
                                                      (l.Value == "3") || (l.Value == "5")
                                                select l).ToArray();

                        ddlUnidade.Items.AddRange(_unidades);
                        ddlUnidade.DataBind();
                        ddlUnidade.Items.Insert(0, new ListItem("Selecione", "0"));

                        ddlIntervalo.DataTextField = "Nome";
                        ddlIntervalo.DataValueField = "Id";
                        ddlIntervalo.DataSource = prescrBll.Listar();
                        ddlIntervalo.DataBind();
                        ddlIntervalo.Items.Insert(0, new ListItem("Selecione", "0"));

                        cbxIncldiet.Checked = Funcoes.Funcoes.ConvertePara.Bool(
                            recNutrTO.EmReceita);
                        lblDoseMinReceita.Text = string.Format("{0:#,##0.00#}",
                            recNutrTO.DoseMin) + " " + recNutrTO.UnidadeMin;
                        lblDoseMaxReceita.Text = string.Format("{0:#,##0.00#}",
                            recNutrTO.DoseMax) + " " + recNutrTO.UnidadeMax;
                        lblDoseReceita.Text = string.Format("{0:#,##0.00#}", recNutrTO.Dose);
                        lblDoseUnidReceita.Text = recNutrTO.Unidade;
                        //lblPesoAtualReceita.Text = string.Format("{0:#,##0.00#}", 
                        //    recNutrTO.PesoAtual);
                        Funcoes.Funcoes.ControlForm.SetComboBox(ddlIntervalo,
                            recNutrTO.IdPrescr);
                        Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnidade,
                            recNutrTO.IdUnid);

                        if (cbxIncldiet.Checked)
                        {
                            lblDoseReceita.Visible = false;
                            lblDoseUnidReceita.Visible = false;
                            lblIntervaloReceita.Visible = false;

                            meDose.Visible = true;
                            meDose.Text = string.Format("{0:#,##0.00#}", recNutrTO.Dose);
                            meQuant.Visible = true;
                            meQuant.Text = string.Format("{0:#,##0.00#}", recNutrTO.Quantidade);

                            ddlUnidade.Visible = true;
                            ddlIntervalo.Visible = true;

                            lbSalvar.Visible = Funcoes.Funcoes.ConvertePara.Int(
                                ViewState["_idReceita"]) > 0 ? true : false;
                        }
                        else
                        {
                            lblDoseReceita.Visible = true;
                            lblDoseUnidReceita.Visible = true;
                            lblIntervaloReceita.Visible = true;

                            meDose.Visible = false;
                            meDose.Text = "";
                            meQuant.Visible = false;
                            meQuant.Text = "";

                            ddlUnidade.Visible = false;
                            ddlIntervalo.Visible = false;

                            lbSalvar.Visible = false;
                        }

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

                foreach (RepeaterItem item in rptReceitaNutrac.Items)
                {
                    HiddenField IdNutrRec = (HiddenField)item.FindControl("hfIdNutrRec");
                    HiddenField hfIdUnidMin = (HiddenField)item.FindControl("hfIdUnidMin");
                    int _idUnidMin = Funcoes.Funcoes.ConvertePara.Int(hfIdUnidMin.Value);
                    HiddenField hfIdUnidMax = (HiddenField)item.FindControl("hfIdUnidMax");
                    int _idUnidMax = Funcoes.Funcoes.ConvertePara.Int(hfIdUnidMax.Value);
                    HiddenField hfDoseMin = (HiddenField)item.FindControl("hfDoseMin");
                    decimal _doseMin = Funcoes.Funcoes.ConvertePara.Decimal(hfDoseMin.Value);
                    HiddenField hfDoseMax = (HiddenField)item.FindControl("hfDoseMax");
                    decimal _doseMax = Funcoes.Funcoes.ConvertePara.Decimal(hfDoseMax.Value);
                    decimal _pesoAtual = Funcoes.Funcoes.ConvertePara.Decimal(lblPeso.Text);

                    CheckBox cbxIncldiet = (CheckBox)item.FindControl("cbxIncldiet");

                    DropDownList ddlUnidade = (DropDownList)item.FindControl("ddlUnidade");
                    DropDownList ddlIntervalo = (DropDownList)item.FindControl("ddlIntervalo");

                    MEdit meDose = (MEdit)item.FindControl("meDose");
                    decimal _dose = Funcoes.Funcoes.ConvertePara.Decimal(meDose.Text);
                    MEdit meQuant = (MEdit)item.FindControl("meQuant");
                    decimal _quant = (_dose * _pesoAtual);

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
                                        recNutrDcl.DoseMin = (cbxIncldiet.Checked ? _doseMin : 0);
                                        recNutrDcl.IdUnidMin = (cbxIncldiet.Checked ? _idUnidMin : 0);
                                        recNutrDcl.DoseMax = (cbxIncldiet.Checked ? _doseMax : 0);
                                        recNutrDcl.IdUnidMax = (cbxIncldiet.Checked ? _idUnidMax : 0);
                                        recNutrDcl.Dose = (cbxIncldiet.Checked ? _dose : 0);
                                        recNutrDcl.IdUnid = Funcoes.Funcoes.ConvertePara.Int(
                                            ddlUnidade.SelectedValue);
                                        recNutrDcl.IdPrescr = Funcoes.Funcoes.ConvertePara.Int(
                                            ddlIntervalo.SelectedValue);
                                        recNutrDcl.PesoAtual = (cbxIncldiet.Checked ? _pesoAtual : 0);
                                        recNutrDcl.Quantidade = (cbxIncldiet.Checked ? _quant : 0);

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

                foreach (RepeaterItem item in rptReceitaNutrac.Items)
                {
                    HiddenField hfIdIncluir = (HiddenField)item.FindControl("hfIdIncluir");
                    CheckBox cbxIncldiet = (CheckBox)item.FindControl("cbxIncldiet");
                    DropDownList ddlUnidade = (DropDownList)item.FindControl("ddlUnidade");
                    DropDownList ddlIntervalo = (DropDownList)item.FindControl("ddlIntervalo");
                    MEdit meDose = (MEdit)item.FindControl("meDose");
                    MEdit meQuant = (MEdit)item.FindControl("meQuant");

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
                                            if (meDose.Text != "")
                                            {
                                                itemRec.Dose =
                                                    Funcoes.Funcoes.ConvertePara.Decimal(
                                                        meDose.Text);
                                            }

                                            if (meQuant.Text != "")
                                            {
                                                itemRec.Quantidade =
                                                    Funcoes.Funcoes.ConvertePara.Decimal(
                                                        meQuant.Text);
                                            }

                                            if (Funcoes.Funcoes.ConvertePara.Int(
                                                ddlUnidade.SelectedValue) > 0)
                                            {
                                                itemRec.IdUnid =
                                                    Funcoes.Funcoes.ConvertePara.Int(
                                                        ddlUnidade.SelectedValue);
                                            }

                                            if (Funcoes.Funcoes.ConvertePara.Int(
                                                    ddlIntervalo.SelectedValue) > 0)
                                            {
                                                itemRec.IdPrescr =
                                                    Funcoes.Funcoes.ConvertePara.Int(
                                                        ddlIntervalo.SelectedValue);
                                            }
                                        }
                                        else
                                        {
                                            meDose.Text = "";
                                            itemRec.Dose = null;

                                            meQuant.Text = "";
                                            itemRec.Quantidade = null;

                                            itemRec.IdUnid = null;
                                            Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnidade, 0);
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

            PopulaNutraceuticosReceita(Funcoes.Funcoes.ConvertePara.Int(ViewState["_idReceita"]));
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
                bllRetorno _validaIntervalos = ValidaIntervalos(rptReceitaNutrac);

                if (_validaIntervalos.retorno)
                {
                    if (ValidaItensRepeater(rptReceitaNutrac))
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
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        _validaIntervalos.mensagem,
                        "NutroVET Informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
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
            receituarioDcl.dTpRec = (int)DominiosBll.ReceitasAuxTipos.Nutracêuticos;
            receituarioDcl.Titulo = tbxUso.Text;            
            receituarioDcl.Veiculo = tbVeiculoBID.Text;
            receituarioDcl.QuantVeic = tbQuantBID.Text;
            receituarioDcl.Posologia = tbPosolBID.Text;
            receituarioDcl.VeiculoTid = tbVeiculoTID.Text;
            receituarioDcl.QuantVeicTid = tbQuantTID.Text;
            receituarioDcl.PosologiaTid = tbPosolTID.Text;
            receituarioDcl.VeiculoSid = tbVeiculoSID.Text;
            receituarioDcl.QuantVeicSid = tbQuantSID.Text;
            receituarioDcl.PosologiaSid = tbPosolSID.Text;
            receituarioDcl.InstrucoesReceita = tbxInstrRec.Text;
            receituarioDcl.Prescricao = "";
            receituarioDcl.DataReceita = DateTime.Today;
            receituarioDcl.LocalReceita = (_configRec != null ? _configRec.Logr_Cidade : "");
            receituarioDcl.NrReceita = receituarioBll.GerarNumeroArquivo();
            receituarioDcl.Arquivo = "RecNutrac_" + receituarioDcl.NrReceita + "_" +
                DateTime.Today.ToString("yy") + ".pdf";

            receituarioDcl.Ativo = true;
            receituarioDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            receituarioDcl.DataCadastro = DateTime.Now;
            receituarioDcl.IP = Request.UserHostAddress;

            //Cria Objetos dos Itens da Receita
            foreach (RepeaterItem item in rptReceitaNutrac.Items)
            {
                HiddenField hfIdIncluir = (HiddenField)item.FindControl("hfIdIncluir");
                HiddenField hfIdUnidMin = (HiddenField)item.FindControl("hfIdUnidMin");
                int _idUnidMin = Funcoes.Funcoes.ConvertePara.Int(hfIdUnidMin.Value);
                HiddenField hfIdUnidMax = (HiddenField)item.FindControl("hfIdUnidMax");
                int _idUnidMax = Funcoes.Funcoes.ConvertePara.Int(hfIdUnidMax.Value);
                HiddenField hfDoseMin = (HiddenField)item.FindControl("hfDoseMin");
                decimal _doseMin = Funcoes.Funcoes.ConvertePara.Decimal(hfDoseMin.Value);
                HiddenField hfDoseMax = (HiddenField)item.FindControl("hfDoseMax");
                decimal _doseMax = Funcoes.Funcoes.ConvertePara.Decimal(hfDoseMax.Value);
                decimal _pesoAtual = Funcoes.Funcoes.ConvertePara.Decimal(lblPeso.Text);

                CheckBox cbxIncldiet = (CheckBox)item.FindControl("cbxIncldiet");

                DropDownList ddlUnidade = (DropDownList)item.FindControl("ddlUnidade");
                DropDownList ddlIntervalo = (DropDownList)item.FindControl("ddlIntervalo");

                MEdit meDose = (MEdit)item.FindControl("meDose");
                decimal _dose = Funcoes.Funcoes.ConvertePara.Decimal(meDose.Text);
                MEdit meQuant = (MEdit)item.FindControl("meQuant");

                decimal _quant = _dose;
                
                if(_idUnidMin != 8 && _idUnidMax != 8)
                {
                    _quant = (_dose * _pesoAtual);
                }

                int _idNutr = Funcoes.Funcoes.ConvertePara.Int(hfIdIncluir.Value);

                switch (item.ItemType)
                {
                    case ListItemType.Item:
                    case ListItemType.AlternatingItem:
                        {
                            recNutrDcl = new ReceituarioNutriente();

                            recNutrDcl.EmReceita = cbxIncldiet.Checked;
                            recNutrDcl.IdNutr = _idNutr;
                            recNutrDcl.DoseMin = _doseMin;
                            recNutrDcl.IdUnidMin = _idUnidMin;
                            recNutrDcl.DoseMax = _doseMax;
                            recNutrDcl.IdUnidMax = _idUnidMax;
                            recNutrDcl.Dose = _dose;
                            recNutrDcl.IdUnid = Funcoes.Funcoes.ConvertePara.Int(
                                ddlUnidade.SelectedValue);
                            recNutrDcl.IdPrescr = Funcoes.Funcoes.ConvertePara.Int(
                                ddlIntervalo.SelectedValue);
                            recNutrDcl.PesoAtual = _pesoAtual;
                            recNutrDcl.Quantidade = _quant;

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
                    "~/Receituario/Impressao/RptNutraceut.aspx?_idRec=" +
                        receituarioDcl.IdReceita + "&_idCardapio=" +
                            Funcoes.Funcoes.ConvertePara.Int(ViewState["_idCardapio"]);

                PopulaNutraceuticosReceita(receituarioDcl.IdReceita);

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
            receituarioDcl.Veiculo = tbVeiculoBID.Text;
            receituarioDcl.QuantVeic = tbQuantBID.Text;
            receituarioDcl.Posologia = tbPosolBID.Text;
            receituarioDcl.VeiculoTid = tbVeiculoTID.Text;
            receituarioDcl.QuantVeicTid = tbQuantTID.Text;
            receituarioDcl.PosologiaTid = tbPosolTID.Text;
            receituarioDcl.VeiculoSid = tbVeiculoSID.Text;
            receituarioDcl.QuantVeicSid = tbQuantSID.Text;
            receituarioDcl.PosologiaSid = tbPosolSID.Text;
            receituarioDcl.InstrucoesReceita = tbxInstrRec.Text;
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

        protected void aTidCard_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 0;
            liTidCard.Attributes["class"] = "active";
            liBidCard.Attributes["class"] = "";
            liSidCard.Attributes["class"] = "";
        }

        protected void aBidCard_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 1;
            liTidCard.Attributes["class"] = "";
            liBidCard.Attributes["class"] = "active";
            liSidCard.Attributes["class"] = "";
        }

        protected void aSidCard_Click(object sender, EventArgs e)
        {
            mvTabControl.ActiveViewIndex = 2;
            liBidCard.Attributes["class"] = "";
            liTidCard.Attributes["class"] = "";
            liSidCard.Attributes["class"] = "active";
        }

        protected void rptReceitaNutrac_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField hfIdUnidMin = (HiddenField)e.Item.FindControl("hfIdUnidMin");
            int _idUnidMin = Funcoes.Funcoes.ConvertePara.Int(hfIdUnidMin.Value);
            HiddenField hfIdUnidMax = (HiddenField)e.Item.FindControl("hfIdUnidMax");
            int _idUnidMax = Funcoes.Funcoes.ConvertePara.Int(hfIdUnidMax.Value);
            HiddenField hfDoseMin = (HiddenField)e.Item.FindControl("hfDoseMin");
            decimal _doseMin = Funcoes.Funcoes.ConvertePara.Decimal(hfDoseMin.Value);
            HiddenField hfDoseMax = (HiddenField)e.Item.FindControl("hfDoseMax");
            decimal _doseMax = Funcoes.Funcoes.ConvertePara.Decimal(hfDoseMax.Value);
            decimal _pesoAtual = Funcoes.Funcoes.ConvertePara.Decimal(lblPeso.Text);

            CheckBox cbxIncldiet = (CheckBox)e.Item.FindControl("cbxIncldiet");

            DropDownList ddlUnidade = (DropDownList)e.Item.FindControl("ddlUnidade");
            DropDownList ddlIntervalo = (DropDownList)e.Item.FindControl("ddlIntervalo");

            MEdit meDose = (MEdit)e.Item.FindControl("meDose");
            decimal _dose = Funcoes.Funcoes.ConvertePara.Decimal(meDose.Text);
            MEdit meQuant = (MEdit)e.Item.FindControl("meQuant");
            decimal _quant = (_dose * _pesoAtual);

            int _idNutrRec = Funcoes.Funcoes.ConvertePara.Int(e.CommandArgument);

            switch (e.CommandName)
            {
                case "salvarItem":
                    {
                        recNutrDcl = recNutrBll.Carregar(_idNutrRec);

                        recNutrDcl.EmReceita = cbxIncldiet.Checked;
                        recNutrDcl.DoseMin = (cbxIncldiet.Checked ? _doseMin : 0);
                        recNutrDcl.IdUnidMin = (cbxIncldiet.Checked ? _idUnidMin : 0);
                        recNutrDcl.DoseMax = (cbxIncldiet.Checked ? _doseMax : 0);
                        recNutrDcl.IdUnidMax = (cbxIncldiet.Checked ? _idUnidMax : 0);
                        recNutrDcl.Dose = (cbxIncldiet.Checked ? _dose : 0);
                        recNutrDcl.IdUnid = Funcoes.Funcoes.ConvertePara.Int(
                            ddlUnidade.SelectedValue);
                        recNutrDcl.IdPrescr = Funcoes.Funcoes.ConvertePara.Int(
                            ddlIntervalo.SelectedValue);
                        recNutrDcl.PesoAtual = (cbxIncldiet.Checked ? _pesoAtual : 0);
                        recNutrDcl.Quantidade = (cbxIncldiet.Checked ? _quant : 0);

                        recNutrDcl.Ativo = true;
                        recNutrDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                            User.Identity.Name);
                        recNutrDcl.DataCadastro = DateTime.Now;
                        recNutrDcl.IP = Request.UserHostAddress;

                        bllRetorno _verificaRetorno = ValidaIntervalos(recNutrDcl);

                        if (_verificaRetorno.retorno)
                        {
                            bllRetorno _alterRet = recNutrBll.Alterar(recNutrDcl);

                            if (_alterRet.retorno)
                            {
                                PopulaNutraceuticosReceita(Funcoes.Funcoes.ConvertePara.Int(
                                    ViewState["_idReceita"]));

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
                        }
                        else
                        {
                            Funcoes.Funcoes.Toastr.ShowToast(this,
                                Funcoes.Funcoes.Toastr.ToastType.Warning,
                                _verificaRetorno.mensagem, "NutroVET informa - Alteração",
                                Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                true);
                        }

                        break;
                    }
            }
        }

        private bool ValidaItensRepeater(Repeater rptItem)
        {
            bool _retorno = true;

            foreach (RepeaterItem item in rptItem.Items)
            {
                CheckBox cbxIncldiet = (CheckBox)item.FindControl("cbxIncldiet");
                DropDownList ddlUnidade = (DropDownList)item.FindControl("ddlUnidade");
                DropDownList ddlIntervalo = (DropDownList)item.FindControl("ddlIntervalo");
                MEdit meDose = (MEdit)item.FindControl("meDose");
                Label lblNutrReceita = (Label)item.FindControl("lblNutrReceita");

                switch (item.ItemType)
                {
                    case ListItemType.Item:
                    case ListItemType.AlternatingItem:
                        {
                            if (cbxIncldiet.Checked)
                            {
                                if ((meDose.Text == "") ||
                                    (Funcoes.Funcoes.ConvertePara.Decimal(meDose.Text) <= 0))
                                {
                                    _retorno = false;

                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                                        "Digite a DOSE para o Nutracêutico " +
                                        lblNutrReceita.Text, "NutroVET Informa",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                                else if (ddlUnidade.SelectedValue == "0")
                                {
                                    _retorno = false;

                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                                        "Selecione uma Unidade para o Nutracêutico " +
                                        lblNutrReceita.Text, "NutroVET Informa",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                                else if (ddlIntervalo.SelectedValue == "0")
                                {
                                    _retorno = false;

                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                                        "Selecione um Intervalo para o Nutracêutico " +
                                        lblNutrReceita.Text, "NutroVET Informa",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                            }

                            break;
                        }
                }
            }

            return _retorno;
        }

        private bllRetorno ValidaIntervalos(Repeater rptItem)
        {
            BITotalIntervalosBll _valida = receituarioBll.VerificaIntervalos(rptItem);
            bllRetorno _retorno = new bllRetorno();

            if ((_valida.TotalTid > 0) && ((tbPosolTID.Text == "") || (tbQuantTID.Text == "") ||
                (tbVeiculoTID.Text == "")))
            {
                _retorno.retorno = false;
                _retorno.mensagem = "Campos Referentes ao Intervalo TID devem ser Preenchidos!!!";
            }
            else if ((_valida.TotalBid > 0) && ((tbPosolBID.Text == "") || (tbQuantBID.Text == "") ||
                (tbVeiculoBID.Text == "")))
            {
                _retorno.retorno = false;
                _retorno.mensagem = "Campos Referentes ao Intervalo BID devem ser Preenchidos!!!";
            }
            else if ((_valida.TotalSid > 0) && ((tbPosolSID.Text == "") || (tbQuantSID.Text == "") ||
                (tbVeiculoSID.Text == "")))
            {
                _retorno.retorno = false;
                _retorno.mensagem = "Campos Referentes ao Intervalo SID devem ser Preenchidos!!!";
            }
            else
            {
                _retorno.retorno = true;
                _retorno.mensagem = "";
            }

            return _retorno;
        }

        private bllRetorno ValidaIntervalos(ReceituarioNutriente _item)
        {
            bllRetorno _retorno = new bllRetorno();

            if ((_item.IdPrescr == 1) && ((tbPosolBID.Text == "") || (tbQuantBID.Text == "") ||
                (tbVeiculoBID.Text == "")))
            {
                _retorno.retorno = false;
                _retorno.mensagem = "Campos Referentes ao Intervalo BID devem ser Preenchidos!!!";
            }
            else if ((_item.IdPrescr == 2) && ((tbPosolTID.Text == "") || (tbQuantTID.Text == "") ||
                (tbVeiculoTID.Text == "")))
            {
                _retorno.retorno = false;
                _retorno.mensagem = "Campos Referentes ao Intervalo TID devem ser Preenchidos!!!";
            }
            else if ((_item.IdPrescr == 3) && ((tbPosolSID.Text == "") || (tbQuantSID.Text == "") ||
                (tbVeiculoSID.Text == "")))
            {
                _retorno.retorno = false;
                _retorno.mensagem = "Campos Referentes ao Intervalo SID devem ser Preenchidos!!!";
            }
            else
            {
                _retorno.retorno = true;
                _retorno.mensagem = "";
            }

            return _retorno;
        }
    }
}