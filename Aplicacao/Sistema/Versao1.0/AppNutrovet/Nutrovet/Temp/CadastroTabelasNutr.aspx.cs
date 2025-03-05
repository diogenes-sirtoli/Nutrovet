using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;

namespace Nutrovet.Temp
{
    public partial class CadastroTabelasNutr : System.Web.UI.Page
    {
        protected clExigenciasNutrBll exigNutrBll = new clExigenciasNutrBll();
        protected ExigenciasNutricionai exigNutrDcl;
        protected TOExigenciasNutricionaisBll exigNutrTo;
        protected clAnimaisAuxEspeciesBll especiesBll = new clAnimaisAuxEspeciesBll();
        protected clExigenciasNutrAuxIndicacoesBll indicacoesBll = new 
            clExigenciasNutrAuxIndicacoesBll();
        protected clNutrientesBll nutrientesBll = new clNutrientesBll();
        protected Nutriente nutrientesDcl;
        protected TONutrientesBll nutrientesTO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulaTabNutr();
                PopulaEspecies();
                PopulaIndicacao();
                PopulaTpValor();
                PopulaNutriente1();
                PopulaUnidade1();
                PopulaNutriente2();
                PopulaUnidade2();

                string x = Funcoes.Funcoes.ManterPosicaoSelecionadaGridView(
                            pnlListagem.ClientID);
                ClientScript.RegisterStartupScript(this.GetType(), "tt", x);

                PopulaGrid();
            }
        }

        private void PopulaUnidade1()
        {
            ddlUnd1.Items.AddRange(exigNutrBll.ListarUnidades());
            ddlUnd1.DataBind();

            ddlUnd1.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaUnidade2()
        {
            ddlUnd2.Items.AddRange(exigNutrBll.ListarUnidades());
            ddlUnd2.DataBind();

            ddlUnd2.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaNutriente1()
        {
            ddlNutr1.DataTextField = "Nutriente";
            ddlNutr1.DataValueField = "IdNutr";
            ddlNutr1.DataSource = nutrientesBll.Listar();
            ddlNutr1.DataBind();

            ddlNutr1.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaNutriente2()
        {
            ddlNutr2.DataTextField = "Nutriente";
            ddlNutr2.DataValueField = "IdNutr";
            ddlNutr2.DataSource = nutrientesBll.Listar();
            ddlNutr2.DataBind();

            ddlNutr2.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaTpValor()
        {
            ddlTpValor.Items.AddRange(exigNutrBll.ListarTpValor());
            ddlTpValor.DataBind();

            ddlTpValor.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaIndicacao()
        {
            ddlIndic.DataTextField = "Nome";
            ddlIndic.DataValueField = "Id";
            ddlIndic.DataSource = indicacoesBll.Listar();
            ddlIndic.DataBind();

            ddlIndic.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaEspecies()
        {
            ddlEspecie.DataTextField = "Nome";
            ddlEspecie.DataValueField = "Id";
            ddlEspecie.DataSource = especiesBll.Listar();
            ddlEspecie.DataBind();

            ddlEspecie.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaTabNutr()
        {
            ddlTabNutr.Items.AddRange(exigNutrBll.ListarTabNutr());
            ddlTabNutr.DataBind();

            ddlTabNutr.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaGrid()
        {
            dgExigNutri.DataSource = exigNutrBll.Listar();
            dgExigNutri.DataBind();
        }

        protected void dgExigNutri_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(dgExigNutri.SelectedDataKey.Value);
            ViewState["IdExigNutr"] = _id;

            exigNutrDcl = exigNutrBll.Carregar(_id);

            PopulaTela(exigNutrDcl);
        }

        private void PopulaTela(ExigenciasNutricionai exigNutrDcl)
        {
            if (exigNutrDcl != null)
            {
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlTabNutr, exigNutrDcl.IdTabNutr);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlEspecie, exigNutrDcl.IdEspecie);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlIndic, exigNutrDcl.IdIndic);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlTpValor, exigNutrDcl.IdTpValor);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlNutr1, exigNutrDcl.IdNutr1);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnd1, exigNutrDcl.IdUnidade1);
                meProp1.Text = Funcoes.Funcoes.ConvertePara.String(exigNutrDcl.Proporcao1);
                meValor1.Text = Funcoes.Funcoes.ConvertePara.String(exigNutrDcl.Valor1);

                if (Funcoes.Funcoes.ConvertePara.Int(ddlUnd1.SelectedValue) == 6)
                {
                    ddlNutr2.Enabled = true;
                    ddlUnd2.Enabled = true;
                    meValor2.Enabled = true;
                    meProp1.Enabled = true;
                    meProp2.Enabled = true;
                }

                Funcoes.Funcoes.ControlForm.SetComboBox(ddlNutr2, exigNutrDcl.IdNutr2);
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnd2, exigNutrDcl.IdUnidade2);
                meProp2.Text = Funcoes.Funcoes.ConvertePara.String(exigNutrDcl.Proporcao2);
                meValor2.Text = Funcoes.Funcoes.ConvertePara.String(exigNutrDcl.Valor2);
            }
        }

        protected void ddlNutr1_SelectedIndexChanged(object sender, EventArgs e)
        {
            nutrientesTO = nutrientesBll.CarregarTO(Funcoes.Funcoes.ConvertePara.Int(
                ddlNutr1.SelectedValue));

            Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnd1, nutrientesTO.IdUnidade);
        }

        protected void ddlNutr2_SelectedIndexChanged(object sender, EventArgs e)
        {
            nutrientesTO = nutrientesBll.CarregarTO(Funcoes.Funcoes.ConvertePara.Int(
                ddlNutr2.SelectedValue));

            Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnd2, nutrientesTO.IdUnidade);
        }

        protected void ddlUnd1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ConvertePara.Int(ddlUnd1.SelectedValue) == 6)
            {
                ddlNutr2.Enabled = true;
                ddlUnd2.Enabled = true;
                meValor2.Enabled = true;
                meProp1.Enabled = true;
                meProp2.Enabled = true;
            }
            else
            {
                ddlNutr2.Enabled = false;
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlNutr2, 0);

                meProp1.Enabled = false;
                meProp1.Text = "";

                ddlUnd2.Enabled = false;
                Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnd2, 0);

                meProp2.Enabled = false;
                meProp2.Text = "";

                meValor2.Enabled = false;
                meValor2.Text = "";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            exigNutrDcl = new ExigenciasNutricionai
            {
                IdTabNutr = Funcoes.Funcoes.ConvertePara.Int(
                    ddlTabNutr.SelectedValue),
                IdEspecie = Funcoes.Funcoes.ConvertePara.Int(
                    ddlEspecie.SelectedValue),
                IdIndic = Funcoes.Funcoes.ConvertePara.Int(ddlIndic.SelectedValue),
                IdTpValor = Funcoes.Funcoes.ConvertePara.Int(
                    ddlTpValor.SelectedValue),

                IdNutr1 = Funcoes.Funcoes.ConvertePara.Int(ddlNutr1.SelectedValue),
                IdUnidade1 = Funcoes.Funcoes.ConvertePara.Int(
                    ddlUnd1.SelectedValue),
                Proporcao1 = Funcoes.Funcoes.ConvertePara.Decimal(meProp1.Text), 
                Valor1 = Funcoes.Funcoes.ConvertePara.Decimal(meValor1.Text),

                IdNutr2 = Funcoes.Funcoes.ConvertePara.Int(ddlNutr2.SelectedValue),
                IdUnidade2 = Funcoes.Funcoes.ConvertePara.Int(
                    ddlUnd2.SelectedValue),
                Proporcao2 = Funcoes.Funcoes.ConvertePara.Decimal(meProp2.Text),
                Valor2 = Funcoes.Funcoes.ConvertePara.Decimal(meValor2.Text),

                Ativo = true,
                IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name),
                DataCadastro = DateTime.Now,
                IP = Request.UserHostAddress
            };

            bllRetorno inserirRet = exigNutrBll.Inserir(exigNutrDcl);

            if (inserirRet.retorno)
            {
                PopulaGrid();
                Cancelar();

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success, inserirRet.mensagem,
                    "Inserção - NutroVET informa", 
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                    "Inserção - NutroVET informa", 
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        private void Cancelar()
        {
            ViewState.Remove("IdExigNutr");
            dgExigNutri.SelectedIndex = -1;

            //Funcoes.Funcoes.ControlForm.SetComboBox(ddlTabNutr, 0);
            //Funcoes.Funcoes.ControlForm.SetComboBox(ddlEspecie, 0);
            //Funcoes.Funcoes.ControlForm.SetComboBox(ddlIndic, 0);
            //Funcoes.Funcoes.ControlForm.SetComboBox(ddlTpValor, 0);
            //Funcoes.Funcoes.ControlForm.SetComboBox(ddlNutr1, 0);
            //Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnd1, 0);
            //meValor1.Text = "";

            //ddlNutr2.Enabled = false;
            //Funcoes.Funcoes.ControlForm.SetComboBox(ddlNutr2, 0);
            //ddlUnd2.Enabled = false;
            //Funcoes.Funcoes.ControlForm.SetComboBox(ddlUnd2, 0);
            //meValor2.Enabled = false;
            //meValor2.Text = "";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(ViewState["IdExigNutr"]);

            if (_id > 0)
            {
                exigNutrDcl = exigNutrBll.Carregar(_id);

                exigNutrDcl.IdTabNutr = Funcoes.Funcoes.ConvertePara.Int(
                    ddlTabNutr.SelectedValue);
                exigNutrDcl.IdEspecie = Funcoes.Funcoes.ConvertePara.Int(
                    ddlEspecie.SelectedValue);
                exigNutrDcl.IdIndic = Funcoes.Funcoes.ConvertePara.Int(
                    ddlIndic.SelectedValue);
                exigNutrDcl.IdTpValor = Funcoes.Funcoes.ConvertePara.Int(
                    ddlTpValor.SelectedValue);

                exigNutrDcl.IdNutr1 = Funcoes.Funcoes.ConvertePara.Int(
                    ddlNutr1.SelectedValue);
                exigNutrDcl.IdUnidade1 = Funcoes.Funcoes.ConvertePara.Int(
                    ddlUnd1.SelectedValue);
                exigNutrDcl.Proporcao1 = Funcoes.Funcoes.ConvertePara.Decimal(meProp1.Text); 
                exigNutrDcl.Valor1 = Funcoes.Funcoes.ConvertePara.Decimal(meValor1.Text);

                exigNutrDcl.IdNutr2 = Funcoes.Funcoes.ConvertePara.Int(
                    ddlNutr2.SelectedValue);
                exigNutrDcl.IdUnidade2 = Funcoes.Funcoes.ConvertePara.Int(
                    ddlUnd2.SelectedValue);
                exigNutrDcl.Proporcao2 = Funcoes.Funcoes.ConvertePara.Decimal(meProp2.Text);
                exigNutrDcl.Valor2 = Funcoes.Funcoes.ConvertePara.Decimal(meValor2.Text);

                exigNutrDcl.Ativo = true;
                exigNutrDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                    User.Identity.Name);
                exigNutrDcl.DataCadastro = DateTime.Now;
                exigNutrDcl.IP = Request.UserHostAddress;

                bllRetorno inserirRet = exigNutrBll.Alterar(exigNutrDcl);

                if (inserirRet.retorno)
                {
                    PopulaGrid();
                    Cancelar();

                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Success, inserirRet.mensagem,
                        "Alteração - NutroVET informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Error, inserirRet.mensagem,
                        "Alteração - NutroVET informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                } 
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int _id = Funcoes.Funcoes.ConvertePara.Int(ViewState["IdExigNutr"]);

            if (_id > 0)
            {
                exigNutrDcl = exigNutrBll.Carregar(_id);

                bllRetorno _ret = exigNutrBll.Excluir(exigNutrDcl);

                if (_ret.retorno)
                {
                    PopulaGrid();
                    Cancelar();

                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Success, _ret.mensagem,
                        "Exclusão - NutroVET informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
                else
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Error, _ret.mensagem,
                        "Exclusão - NutroVET informa",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }
            }
        }
    }
}