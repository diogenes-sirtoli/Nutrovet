using System;
using DCL;
using BLL;

namespace Nutrovet.Plano
{
	public partial class EscolherPlano : System.Web.UI.Page
	{
        protected clAcessosVigenciaCupomBll cupomBll = new clAcessosVigenciaCupomBll();
        protected AcessosVigenciaCupomDesconto cupomDcl = null;
        protected TOPessoasBll pessoaTO;
        protected TOPlanosBll planosTO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblAno.Text = DateTime.Today.ToString("yyyy");
                
                alertas.Visible = false;
                Session.Remove("escolheuPlano");

                if (Request.QueryString["_escolheu"] != null)
                {
                    bool _escolheu = Funcoes.Funcoes.ConvertePara.Bool(
                        Request.QueryString["_escolheu"]);

                    if (!_escolheu)
                    {
                        alertas.Attributes["class"] = "alert alert-info alert-dismissible";
                        lblAlerta.Text =
                            "É necessário ESCOLHER um Plano antes de se Registrar !";
                        alertas.Visible = true;
                    }
                }
            }
        }

        private void CalculaValores()
        {
            string _montaTxtPlano = "", _planB = "Plano Basico (ate 10 Pacientes)",
                _planI = "Plano Intermediario (ate 20 Pacientes)",
                _planC = "Plano Completo (+ de 20 Pacientes)", _an = "Anual",
                _men = "Mensal", _montaTxtMod = "", _ri = "Receituario Inteligente",
                _p = "Prontuario";

            double _bm = (rbBasicoMensal.Checked ? 20 : 0);
            double _ba = (rbBasicoAnual.Checked ? 200 : 0);
            double _im = (rbIntermediarioMensal.Checked ? 40 : 0);
            double _ia = (rbIntermediarioAnual.Checked ? 400 : 0);
            double _cm = (rbCompletoMensal.Checked ? 60 : 0);
            double _ca = (rbCompletoAnual.Checked ? 600 : 0);

            double _rim = (rbReceituarioInteligenteMensal.Checked ? 10 : 0);
            double _ria = (rbReceituarioInteligenteAnual.Checked ? 100 : 0);
            double _pm = (rbProntuarioMensal.Checked ? 15 : 0);
            double _pa = (rbProntuarioAnual.Checked ? 150 : 0);
           int _idTpDesc = 0;

            _montaTxtPlano = (_bm > 0 ? _planB + " - " + _men :
                (_ba > 0 ? _planB + " - " + _an : (
                _im > 0 ? _planI + " - " + _men : (
                _ia > 0 ? _planI + " - " + _an : (
                _cm > 0 ? _planC + " - " + _men : (
                _ca > 0 ? _planC + " - " + _an : ""))))));

            _montaTxtMod =
                (_rim > 0 ? ", " + _ri + " - " + _men :
                (_ria > 0 ? ", " + _ri + " - " + _an : "")) +
                (_pm > 0 ? ", " + _p + " - " + _men :
                (_pa > 0 ? ", " + _p + " - " + _an : ""));

            double _subTotal = _bm + _ba + _im + _ia + _ca + _cm + _rim + _ria + 
                _pm + _pa;

            double _total = _subTotal;

            if (txbVoucher.Text != "")
            {
                cupomDcl = cupomBll.Carregar(txbVoucher.Text);

                if ((cupomDcl != null) && (cupomDcl.IdCupom > 0))
                {
                    if (cupomBll.Vigencia(cupomDcl.NrCumpom))
                    {
                        if (Funcoes.Funcoes.ConvertePara.Bool(cupomDcl.fAcessoLiberado))
                        {
                            _montaTxtPlano = _planC + " - " + _men;
                            _montaTxtMod = "";
                            _total = 0;

                            rbBasicoMensal.Checked = false;
                            rbBasicoAnual.Checked = false;
                            rbIntermediarioMensal.Checked = false;
                            rbIntermediarioAnual.Checked = false;
                            rbCompletoMensal.Checked = false;
                            rbCompletoAnual.Checked = false;
                        }
                        else
                        {
                            _total = cupomBll.CalculaValorDesconto(Funcoes.Funcoes.ConvertePara.Int(
                                cupomDcl.IdTipoDesc), _subTotal, cupomDcl.ValorDesc);
                        } 
                    }
                    else
                    {
                        lblTexto.Text = string.Format("Cupom Fora da Vigência! Vigência Entre {0:d} e {1:d}", 
                            cupomDcl.DtInicial, cupomDcl.DtFinal);
                        modal.Show();

                        cupomDcl = null;
                    }
                }
                else
                {
                    cupomDcl = null;
                }
            }

            if (cupomDcl != null)
            {
                _idTpDesc = Funcoes.Funcoes.ConvertePara.Int(cupomDcl.IdTipoDesc);

                if (Funcoes.Funcoes.ConvertePara.Bool(cupomDcl.fAcessoLiberado))
                {
                    lblValor.Text = "Acesso Liberado";
                }
                else
                {
                    lblValor.Text = string.Format("{0:c}", _total);
                }
            }
            else
            {
                _idTpDesc = 0;
                lblValor.Text = string.Format("{0:c}", _total);
            }

            Session["escolheuPlano"] = EscolheuPlano(cupomDcl, _total,
                _idTpDesc, _montaTxtPlano + _montaTxtMod);
        }

        protected void rbBasicoMensal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBasicoMensal.Checked)
            {
                AtivaModAdic(true);
                CalculaValores();
            }
        }

        protected void rbBasicoAnual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBasicoAnual.Checked)
            {
                AtivaModAdic(false);
                CalculaValores();
            }
        }

        protected void rbIntermediarioMensal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIntermediarioMensal.Checked)
            {
                AtivaModAdic(true);
                CalculaValores();
            }
        }

        protected void rbIntermediarioAnual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIntermediarioAnual.Checked)
            {
                AtivaModAdic(false);
                CalculaValores();
            }
        }

        protected void rbCompletoMensal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCompletoMensal.Checked)
            {
                AtivaModAdic(true);
                CalculaValores();
            }
        }

        protected void rbCompletoAnual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCompletoAnual.Checked)
            {
                AtivaModAdic(false);
                CalculaValores();
            }
        }

        protected void rbReceituarioInteligenteMensal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReceituarioInteligenteMensal.Checked)
            {
                CalculaValores();
            }
        }

        protected void rbReceituarioInteligenteAnual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReceituarioInteligenteAnual.Checked)
            {
                CalculaValores();
            }
        }

        protected void rbProntuarioMensal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbProntuarioMensal.Checked)
            {
                CalculaValores();
            }
        }

        protected void rbProntuarioAnual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbProntuarioAnual.Checked)
            {
                CalculaValores();
            }
        }

        private void AtivaModAdic(bool _ativa)
        {
            rbReceituarioInteligenteMensal.Checked = false;
            rbReceituarioInteligenteMensal.Enabled = _ativa;
            rbReceituarioInteligenteAnual.Checked = false;
            rbReceituarioInteligenteAnual.Enabled = !_ativa;

            rbProntuarioMensal.Checked = false;
            rbProntuarioMensal.Enabled = _ativa;
            rbProntuarioAnual.Checked = false;
            rbProntuarioAnual.Enabled = !_ativa;
        }

        protected void txbVoucher_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void btnPgto_Click(object sender, EventArgs e)
        {
            planosTO = (TOPlanosBll)Session["escolheuPlano"];

            if ((planosTO != null) && (planosTO.Escolheu))
            {
                CalculaValores();

                Response.Redirect("~/Plano/Registrar.aspx");
            }
            else
            {
                lblTexto.Text = "É necessário ESCOLHER um Plano !";
                modal.Show();
            }
        }

        protected TOPlanosBll EscolheuPlano(AcessosVigenciaCupomDesconto _cupom, 
            double _valor, int _idTpDesc, string _TextoProd)
        {
            planosTO = new TOPlanosBll();
            cupomDcl = (_cupom != null ? _cupom : new AcessosVigenciaCupomDesconto());

            if (rbBasicoMensal.Checked)
            {
                if ((cupomDcl.NrCumpom != null) && (cupomDcl.NrCumpom != ""))
                {
                    if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 5))
                    {
                        planosTO.Plano = "BMdp05";
                    }
                    else if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 10))
                    {
                        planosTO.Plano = "BMdp10";
                    }
                    else if (cupomDcl.IdTipoDesc ==
                                (int)DominiosBll.CupomDescontoTipos.Mensal)
                    {
                        planosTO.Plano = "BMdm" + cupomDcl.ValorDesc;
                    }

                    planosTO.VoucherNr = cupomDcl.NrCumpom;
                }
                else
                {
                    planosTO.Plano = "BM";
                }

                planosTO.fAcessoLiberado = false;
                planosTO.Escolheu = true;
                planosTO.AnualMensal = 'M';
                planosTO.Receituario = (rbReceituarioInteligenteAnual.Checked ||
                    rbReceituarioInteligenteMensal.Checked ? true : false);
                planosTO.Prontuario = (rbProntuarioAnual.Checked ||
                    rbProntuarioMensal.Checked ? true : false);
            }
            else if (rbBasicoAnual.Checked)
            {
                if ((cupomDcl.NrCumpom != null) && (cupomDcl.NrCumpom != ""))
                {
                    if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 5))
                    {
                        planosTO.Plano = "BAdp05";
                    }
                    else if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 10))
                    {
                        planosTO.Plano = "BAdp10";
                    }
                    else if (cupomDcl.IdTipoDesc ==
                                (int)DominiosBll.CupomDescontoTipos.Anual)
                    {
                        planosTO.Plano = "BAda" + cupomDcl.ValorDesc;
                    }

                    planosTO.VoucherNr = cupomDcl.NrCumpom;
                }
                else
                {
                    planosTO.Plano = "BA";
                }

                planosTO.fAcessoLiberado = false;
                planosTO.Escolheu = true;
                planosTO.AnualMensal = 'A';
                planosTO.Receituario = (rbReceituarioInteligenteAnual.Checked ||
                    rbReceituarioInteligenteMensal.Checked ? true : false);
                planosTO.Prontuario = (rbProntuarioAnual.Checked ||
                    rbProntuarioMensal.Checked ? true : false);
            }
            else if (rbIntermediarioMensal.Checked)
            {
                if ((cupomDcl.NrCumpom != null) && (cupomDcl.NrCumpom != ""))
                {
                    if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 5))
                    {
                        planosTO.Plano = "IMdp05";
                    }
                    else if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 10))
                    {
                        planosTO.Plano = "IMdp10";
                    }
                    else if (cupomDcl.IdTipoDesc ==
                                (int)DominiosBll.CupomDescontoTipos.Mensal)
                    {
                        planosTO.Plano = "IMdm" + cupomDcl.ValorDesc;
                    }

                    planosTO.VoucherNr = cupomDcl.NrCumpom;
                }
                else
                {
                    planosTO.Plano = "IM";
                }

                planosTO.fAcessoLiberado = false;
                planosTO.Escolheu = true;
                planosTO.AnualMensal = 'M';
                planosTO.Receituario = (rbReceituarioInteligenteAnual.Checked ||
                    rbReceituarioInteligenteMensal.Checked ? true : false);
                planosTO.Prontuario = (rbProntuarioAnual.Checked ||
                    rbProntuarioMensal.Checked ? true : false);
            }
            else if (rbIntermediarioAnual.Checked)
            {
                if ((cupomDcl.NrCumpom != null) && (cupomDcl.NrCumpom != ""))
                {
                    if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 5))
                    {
                        planosTO.Plano = "IAdp05";
                    }
                    else if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 10))
                    {
                        planosTO.Plano = "IAdp10";
                    }
                    else if (cupomDcl.IdTipoDesc ==
                                (int)DominiosBll.CupomDescontoTipos.Anual)
                    {
                        planosTO.Plano = "IAda" + cupomDcl.ValorDesc;
                    }

                    planosTO.VoucherNr = cupomDcl.NrCumpom;
                }
                else
                {
                    planosTO.Plano = "IA";
                }

                planosTO.fAcessoLiberado = false;
                planosTO.Escolheu = true;
                planosTO.AnualMensal = 'A';
                planosTO.Receituario = (rbReceituarioInteligenteAnual.Checked ||
                    rbReceituarioInteligenteMensal.Checked ? true : false);
                planosTO.Prontuario = (rbProntuarioAnual.Checked ||
                    rbProntuarioMensal.Checked ? true : false);
            }
            else if (rbCompletoMensal.Checked)
            {
                if ((cupomDcl.NrCumpom != null) && (cupomDcl.NrCumpom != ""))
                {
                    if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 5))
                    {
                        planosTO.Plano = "CMdp05";
                    }
                    else if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 10))
                    {
                        planosTO.Plano = "CMdp10";
                    }
                    else if (cupomDcl.IdTipoDesc ==
                                (int)DominiosBll.CupomDescontoTipos.Mensal)
                    {
                        planosTO.Plano = "CMdm" + cupomDcl.ValorDesc;
                    }

                    planosTO.VoucherNr = cupomDcl.NrCumpom;
                }
                else
                {
                    planosTO.Plano = "CM";
                }

                planosTO.fAcessoLiberado = false;
                planosTO.Escolheu = true;
                planosTO.AnualMensal = 'M';
                planosTO.Receituario = (rbReceituarioInteligenteAnual.Checked ||
                    rbReceituarioInteligenteMensal.Checked ? true : false);
                planosTO.Prontuario = (rbProntuarioAnual.Checked ||
                    rbProntuarioMensal.Checked ? true : false);
            }
            else if (rbCompletoAnual.Checked)
            {
                if ((cupomDcl.NrCumpom != null) && (cupomDcl.NrCumpom != ""))
                {
                    if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 5))
                    {
                        planosTO.Plano = "CAdp05";
                    }
                    else if ((cupomDcl.IdTipoDesc ==
                            (int)DominiosBll.CupomDescontoTipos.Percentual) &&
                        (cupomDcl.ValorDesc == 10))
                    {
                        planosTO.Plano = "CAdp10";
                    }
                    else if (cupomDcl.IdTipoDesc ==
                                (int)DominiosBll.CupomDescontoTipos.Anual)
                    {
                        planosTO.Plano = "CAda" + cupomDcl.ValorDesc;
                    }

                    planosTO.VoucherNr = cupomDcl.NrCumpom;
                }
                else
                {
                    planosTO.Plano = "CA";
                }

                planosTO.fAcessoLiberado = false;
                planosTO.Escolheu = true;
                planosTO.AnualMensal = 'A';
                planosTO.Receituario = (rbReceituarioInteligenteAnual.Checked ||
                    rbReceituarioInteligenteMensal.Checked ? true : false);
                planosTO.Prontuario = (rbProntuarioAnual.Checked ||
                    rbProntuarioMensal.Checked ? true : false);
            }
            else if (cupomDcl.NrCumpom != null)
            {
                if ((cupomDcl.NrCumpom != "") && Funcoes.Funcoes.ConvertePara.Bool(
                    cupomDcl.fAcessoLiberado))
                {
                    planosTO.fAcessoLiberado = Funcoes.Funcoes.ConvertePara.Bool(
                        cupomDcl.fAcessoLiberado);
                    planosTO.VoucherNr = cupomDcl.NrCumpom;
                    planosTO.Plano = "CM";
                    planosTO.Escolheu = true;
                    planosTO.AnualMensal = 'M';
                    planosTO.Receituario = false;
                    planosTO.Prontuario = false;
                }
            }

            planosTO.TextoProduto = _TextoProd;
            planosTO.ValorTotal = _valor;

            return planosTO;
        }

        protected void btnCalcula_Click(object sender, EventArgs e)
        {
            CalculaValores();
        }
    }
}