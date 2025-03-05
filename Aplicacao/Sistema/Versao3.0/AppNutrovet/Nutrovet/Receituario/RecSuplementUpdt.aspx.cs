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
    public partial class RecSuplementUpdt : System.Web.UI.Page
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
        protected TOReceituarioNutrientesBll recNutrTO, recNewNutrTO;
        protected List<TOReceituarioNutrientesBll> listRecNutrTO, listNewRecNutrTO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
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
                    int _idReceita = Funcoes.Funcoes.ConvertePara.Int(
                        Funcoes.Funcoes.Seguranca.Descriptografar(
                            Funcoes.Funcoes.ConvertePara.String(
                                Request.QueryString["_idReceita"])));

                    if (_idReceita > 0)
                    {
                        ViewState["_idReceita"] = _idReceita;

                        PopulaTela(_idReceita, _idPessoa);
                        PopulaCabecalhoReceita(_idPessoa);
                        PopularLogo(_idPessoa);
                        PopularAssinatura(_idPessoa);

                        string x = Funcoes.Funcoes.ManterPosicaoSelecionadaGridView(
                                gridRepeater.ClientID);
                        ClientScript.RegisterStartupScript(this.GetType(), "tt", x);

                        PopulaNutrientesReceita(_idReceita);
                    }
                    else
                    {
                        Response.Redirect("~/MenuGeral.aspx?perm=" +
                            Funcoes.Funcoes.Seguranca.Criptografar("False"));
                    }
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

        private void PopulaTela(int _idReceita, int _idPessoa)
        {
            receituarioDcl = receituarioBll.Carregar(_idReceita);
            cardapioTO = cardapioBll.CarregarTO(receituarioDcl.IdCardapio);
            configReceitDcl = configReceitBll.Carregar(_idPessoa);

            lblPaciente.Text = cardapioTO.Animal;
            lblPeso.Text = Funcoes.Funcoes.ConvertePara.String(cardapioTO.PesoAtual) + 
                " Kg(s)";
            lblEspecie.Text = cardapioTO.Especie;
            lblSexo.Text = cardapioTO.Sexo;
            lblRaca.Text = cardapioTO.Raca;
            lblIdade.Text = Funcoes.Funcoes.ConvertePara.String(cardapioTO.Idade) + 
                " ano(s)";
            lblTutor.Text = cardapioTO.Tutor;
            lblEMailTutor.Text = cardapioTO.TutorEMail;
            lblFoneTutor.Text = cardapioTO.TutorFone;

            tbxUso.Text = receituarioDcl.Titulo;
            tbxInstrRec.Text = receituarioDcl.InstrucoesReceita;
            tbxVeicExcip.Text = receituarioDcl.Veiculo;
            tbxQtdExcip.Text = receituarioDcl.QuantVeic;
            ftbPosologia.Text = receituarioDcl.Posologia;

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                PopulaLinksImpressao(receituarioDcl, cardapioTO);
            }

            //hlImprReceSupl.Enabled = true;
            //hlImprReceSupl.CssClass = "btn btn-primary-nutrovet";
            //hlImprReceSupl.NavigateUrl =
            //    "~/Receituario/Impressao/RptSuplement.aspx?_idRec=" +
            //        receituarioDcl.IdReceita + "&_idCardapio=" +
            //            cardapioTO.IdCardapio;
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

        private void PopulaNutrientesReceita(int _idReceita)
        {
            listRecNutrTO = recNutrBll.ListarTO(_idReceita);

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

            PopulaNutrientesReceita(Funcoes.Funcoes.ConvertePara.Int(ViewState["_idReceita"]));
        }

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Receituario/ReceituarioSelecao.aspx");
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
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        "É Necessário selecionar uma Receita de Suplementação!!!",
                        "NutroVET Informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter, true);
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
            int _idReceita = Funcoes.Funcoes.ConvertePara.Int(ViewState["_idReceita"]);

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

        private void PopulaLinksImpressao(DCL.Receituario _receita, TOCardapioBll _cardapioTO)
        {
            bool _existeFile = receituarioBll.ExisteArquivoReceita(_receita.Arquivo);
            string url = string.Format($@"~/Receituario/Impressao/RptSuplement.aspx?_idRec={
                receituarioDcl.IdReceita}&_idCardapio={_cardapioTO.IdCardapio}&_tpImpr=");

            hlImprCardapioPdf.NavigateUrl = url + "pdf";
            hlImprCardapioPdf.Enabled = true;
            hlImprCardapioPdf.CssClass = "btn btn-warning";

            if (_existeFile)
            {
                hlImprCardapioWord.NavigateUrl = url + "word";
                hlImprCardapioWord.Enabled = true;
                hlImprCardapioWord.Visible = true;
                hlImprCardapioWord.CssClass = "btn btn-warning";

                hlImprCardapioExcel.NavigateUrl = url + "excel";
                hlImprCardapioExcel.Enabled = true;
                hlImprCardapioExcel.Visible = true;
                hlImprCardapioExcel.CssClass = "btn btn-warning";
            }
            else
            {
                hlImprCardapioWord.NavigateUrl = "";
                hlImprCardapioWord.Enabled = false;
                hlImprCardapioWord.Visible = false;
                hlImprCardapioWord.CssClass = "btn btn-basic";

                hlImprCardapioExcel.NavigateUrl = "";
                hlImprCardapioExcel.Enabled = false;
                hlImprCardapioExcel.Visible = false;
                hlImprCardapioExcel.CssClass = "btn btn-basic";
            }
        }
    }
}