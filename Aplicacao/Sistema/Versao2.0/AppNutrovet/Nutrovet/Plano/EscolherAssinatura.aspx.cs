using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;

namespace Nutrovet.Plano
{
    public partial class EscolherAssinatura : System.Web.UI.Page
    {
        protected clAcessosVigenciaPlanosBll vigenciaPlanosBll = new clAcessosVigenciaPlanosBll();

        protected clAcessosVigenciaCupomBll cupomBll = new clAcessosVigenciaCupomBll();
        protected AcessosVigenciaCupomDesconto cupomDcl = null;

        protected TOPlanosBll planosTO;
        protected PlanosAssinatura planosDcl;
        protected clPlanosAssinaturasBll planosBll = new clPlanosAssinaturasBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.Remove("PlanoAssinatura");
                Session.Remove("DadosComprador");

                PopulaPlanoBasico("");
                PopulaPlanoIntermediario("");
                PopulaPlanoCompleto("");
                //PopulaReceituario();
                //PopulaProntuario();
            }
        }

        private void PopulaPlanoBasico(string _voucher)
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Básico;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Plano_Principal;

            List<TOPlanosBll> _listagem = planosBll.ListarPlanos(_codPlano, _tpPlano, _voucher);

            rblBasico.SelectedIndex = -1;
            rblBasico.Items.Clear();
            rblBasico.DataValueField = "IdPlano";
            rblBasico.DataTextField = "ValorDescricao";
            rblBasico.DataSource = _listagem;
            rblBasico.DataBind();

            lblDescrB.Text = (_listagem.FirstOrDefault().TipoPlano != null ?
                _listagem.FirstOrDefault().TipoPlano : "");
        }

        private void PopulaPlanoIntermediario(string _voucher)
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Intermediário;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Plano_Principal;

            List<TOPlanosBll> _listagem = planosBll.ListarPlanos(_codPlano, _tpPlano, _voucher);

            rblIntermediario.SelectedIndex = -1;
            rblIntermediario.Items.Clear();
            rblIntermediario.DataValueField = "IdPlano";
            rblIntermediario.DataTextField = "ValorDescricao";
            rblIntermediario.DataSource = _listagem;
            rblIntermediario.DataBind();

            lblDescrI.Text = (_listagem.FirstOrDefault().TipoPlano != null ?
                _listagem.FirstOrDefault().TipoPlano : "");
        }

        private void PopulaPlanoCompleto(string _voucher)
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Completo;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Plano_Principal;

            List<TOPlanosBll> _listagem = planosBll.ListarPlanos(_codPlano, _tpPlano, _voucher);

            rblCompleto.SelectedIndex = -1;
            rblCompleto.Items.Clear();
            rblCompleto.DataValueField = "IdPlano";
            rblCompleto.DataTextField = "ValorDescricao";
            rblCompleto.DataSource = _listagem;
            rblCompleto.DataBind();

            lblDescrC.Text = (_listagem.FirstOrDefault().TipoPlano != null ?
                _listagem.FirstOrDefault().TipoPlano : "");
        }

        private void PopulaReceituario(string _voucher)
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Receituário;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar;

            rblReceituario.SelectedIndex = -1;
            rblReceituario.Items.Clear();
            rblReceituario.DataValueField = "IdPlano";
            rblReceituario.DataTextField = "ValorDescricao";
            rblReceituario.DataSource = planosBll.ListarPlanos(_codPlano, _tpPlano, _voucher);
            rblReceituario.DataBind();
        }

        private void PopulaProntuario(string _voucher)
        {
            int _codPlano = (int)DominiosBll.PlanosAuxNomes.Prontuário;
            int _tpPlano = (int)DominiosBll.PlanosAuxTipos.Módulo_Complementar;

            rblProntuario.SelectedIndex = -1;
            rblProntuario.Items.Clear();
            rblProntuario.DataValueField = "IdPlano";
            rblProntuario.DataTextField = "ValorDescricao";
            rblProntuario.DataSource = planosBll.ListarPlanos(_codPlano, _tpPlano, _voucher);
            rblProntuario.DataBind();
        }

        protected void btnAvancar_Click(object sender, EventArgs e)
        {
            PreencheSessao();

            List<TOPlanosBll> _listagem = (List<TOPlanosBll>)Session["PlanoAssinatura"];
            //planosTO = _listagem.Where(s => s.dPlanoTp == 1).SingleOrDefault();

            if /*(*/(_listagem.Count > 0)/* && (planosTO != null) && (planosTO.dPlanoTp == 1))*/
            {
                Response.Redirect("~/Plano/ResumoAssinatura.aspx");
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Selecione um Plano de Assinatura!!!", "Atenção",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void PreencheSessao()
        {
            List<TOPlanosBll> _listaPlanos = new List<TOPlanosBll>();

            foreach (ListItem item in rblBasico.Items)
            {
                if (item.Selected)
                {

                    planosTO = new TOPlanosBll();
                    planosTO = planosBll.CarregarTO(Funcoes.Funcoes.ConvertePara.Int(
                        item.Value));

                    if (Funcoes.Funcoes.ConvertePara.Int(ViewState["VoucherId"]) > 0)
                    {
                        int _idCupom = Funcoes.Funcoes.ConvertePara.Int(ViewState["VoucherId"]);
                        cupomDcl = cupomBll.Carregar(_idCupom);

                        planosTO.VoucherId = cupomDcl.IdCupom;
                        planosTO.VoucherNr = cupomDcl.NrCumpom;
                        planosTO.VoucherUsado = Funcoes.Funcoes.ConvertePara.Bool(cupomDcl.fUsado);
                    }

                    _listaPlanos.Add(planosTO);
                }
            }

            foreach (ListItem item in rblIntermediario.Items)
            {
                if (item.Selected)
                {
                    planosTO = new TOPlanosBll();
                    planosTO = planosBll.CarregarTO(Funcoes.Funcoes.ConvertePara.Int(
                        item.Value));

                    if (Funcoes.Funcoes.ConvertePara.Int(ViewState["VoucherId"]) > 0)
                    {
                        int _idCupom = Funcoes.Funcoes.ConvertePara.Int(ViewState["VoucherId"]);
                        cupomDcl = cupomBll.Carregar(_idCupom);

                        planosTO.VoucherId = cupomDcl.IdCupom;
                        planosTO.VoucherNr = cupomDcl.NrCumpom;
                        planosTO.VoucherUsado = Funcoes.Funcoes.ConvertePara.Bool(cupomDcl.fUsado);
                    }

                    _listaPlanos.Add(planosTO);
                }
            }

            foreach (ListItem item in rblCompleto.Items)
            {
                if (item.Selected)
                {
                    planosTO = new TOPlanosBll();
                    planosTO = planosBll.CarregarTO(Funcoes.Funcoes.ConvertePara.Int(
                        item.Value));

                    if (Funcoes.Funcoes.ConvertePara.Int(ViewState["VoucherId"]) > 0)
                    {
                        int _idCupom = Funcoes.Funcoes.ConvertePara.Int(ViewState["VoucherId"]);
                        cupomDcl = cupomBll.Carregar(_idCupom);

                        planosTO.VoucherId = cupomDcl.IdCupom;
                        planosTO.VoucherNr = cupomDcl.NrCumpom;
                        planosTO.VoucherUsado = Funcoes.Funcoes.ConvertePara.Bool(cupomDcl.fUsado);
                    }

                    _listaPlanos.Add(planosTO);
                }
            }

            //foreach (ListItem item in rblProntuario.Items)
            //{
            //    if (item.Selected)
            //    {
            //        planosTO = new TOPlanosBll();
            //        planosTO = planosBll.CarregarTO(Funcoes.Funcoes.ConvertePara.Int(
            //            item.Value));

            //        _listaPlanos.Add(planosTO);
            //    }
            //}

            //foreach (ListItem item in rblReceituario.Items)
            //{
            //    if (item.Selected)
            //    {
            //        planosTO = new TOPlanosBll();
            //        planosTO = planosBll.CarregarTO(Funcoes.Funcoes.ConvertePara.Int(
            //            item.Value));

            //        _listaPlanos.Add(planosTO);
            //    }
            //}

            Session["PlanoAssinatura"] = _listaPlanos;
        }

        protected void rblBasico_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblIntermediario.SelectedIndex = -1;
            rblCompleto.SelectedIndex = -1;
        }

        protected void rblIntermediario_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblBasico.SelectedIndex = -1;
            rblCompleto.SelectedIndex = -1;
        }

        protected void rblCompleto_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblIntermediario.SelectedIndex = -1;
            rblBasico.SelectedIndex = -1;
        }

        protected void lbVerificarVoucher_Click(object sender, EventArgs e)
        {
            if (meVoucher.Text != "")
            {
                bllRetorno _situacaoVoucher = cupomBll.VoucherSituacao(meVoucher.Text);

                if (_situacaoVoucher.retorno)
                {
                    TOAcessosVigenciaCupomBll cupomTO =
                        (TOAcessosVigenciaCupomBll)_situacaoVoucher.objeto.FirstOrDefault();

                    if ((cupomTO.dPlanoTp != null) && (cupomTO.dPlanoTp > 0))
                    {
                        PopulaPlanoBasico(cupomTO.NrCupom);
                        PopulaPlanoIntermediario(cupomTO.NrCupom);
                        PopulaPlanoCompleto(cupomTO.NrCupom);

                        ViewState["VoucherId"] = cupomTO.IdCupom;
                    }
                    else
                    {
                        Funcoes.Funcoes.Toastr.ShowToast(this,
                            Funcoes.Funcoes.Toastr.ToastType.Warning,
                            "Plano Inexistente! Contate o Administrador", "Atenção",
                            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                            true);
                    }
                }
                else
                {
                    ViewState.Remove("VoucherId");
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Warning,
                        _situacaoVoucher.mensagem, "Atenção",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
            else
            {
                ViewState.Remove("VoucherId");

                PopulaPlanoBasico("");
                PopulaPlanoIntermediario("");
                PopulaPlanoCompleto("");
            }
        }
    }
}