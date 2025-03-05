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
    public partial class RecBrancoUpdt : System.Web.UI.Page
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
                    }
                    else
                    {
                        Response.Redirect("~/MenuGeral.aspx?perm=" +
                            Funcoes.Funcoes.Seguranca.Criptografar("False"));
                    }
                }
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

            ftbRecBranco.Text = receituarioDcl.Prescricao;

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                PopulaLinksImpressao(receituarioDcl, cardapioTO);
            }

            //hlImprReceita.Enabled = true;
            //hlImprReceita.CssClass = "btn btn-primary-nutrovet";
            //hlImprReceita.NavigateUrl =
            //    "~/Receituario/Impressao/RptBranco.aspx?_idRec=" +
            //        receituarioDcl.IdReceita + "&_idCardapio=" +
            //            cardapioTO.IdCardapio;
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

        protected void lbSalvaReceita_Click(object sender, EventArgs e)
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
                        "É Necessário Selecionar uma Receita em Branco!!!", 
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

        protected void lbFechar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Receituario/ReceituarioSelecao.aspx");
        }

        private void Alterar(int _idReceita)
        {
            bllRetorno updateRet;
            int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            configReceitDcl = configReceitBll.Carregar(_idPessoa);

            //Cria Objeto da Receita
            receituarioDcl = receituarioBll.Carregar(_idReceita);

            receituarioDcl.Prescricao = ftbRecBranco.Text;
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

        private void PopulaLinksImpressao(DCL.Receituario _receita, TOCardapioBll _cardapioTO)
        {
            bool _existeFile = receituarioBll.ExisteArquivoReceita(_receita.Arquivo);
            string url = string.Format($@"~/Receituario/Impressao/RptBranco.aspx?_idRec={
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