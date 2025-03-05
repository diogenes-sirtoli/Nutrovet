using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DCL;
using BLL;

namespace DataTransform
{
    public partial class Form1 : Form
    {
        protected clDTS dtsBll = new clDTS();
        protected AlimentoComponente alimCompDcl;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnExecuta_Click(object sender, EventArgs e)
        {
            ExecutaAcao();
        }

        private void ExecutaAcao()
        {
            bllRetorno _ret = new bllRetorno();
            List<TOAlimentoComponentesBll> lista = dtsBll.ListagemAlimentos();
            List<TOAlimentoComponentesBll> listaAlim = new
                List<TOAlimentoComponentesBll>();
            int _erros = 0, _acertos = 0, _progress = 0;

            foreach (TOAlimentoComponentesBll item in lista)
            {
                if (item.ValorNaoConvert != null)
                {
                    item.Valor = Funcoes.Funcoes.ConvertePara.Decimal(
                                item.ValorNaoConvert);
                }

                listaAlim.Add(item);
            }

            foreach (TOAlimentoComponentesBll item in listaAlim)
            {
                _progress += 1;

                if (item.Valor != null)
                {
                    alimCompDcl = new AlimentoComponente();

                    alimCompDcl.IdAlimento = item.IdAlimento;
                    alimCompDcl.IdComponente = item.IdComponente;
                    alimCompDcl.Valor = item.Valor.Value;
                    alimCompDcl.Ativo = true;
                    alimCompDcl.IdOperador = 1;
                    alimCompDcl.DataCadastro = DateTime.Now;

                    try
                    {
                        _ret = dtsBll.Inserir(alimCompDcl);

                        if (_ret.retorno)
                        {
                            _acertos += 1;
                        }
                        else
                        {
                            _erros += 1;
                        }
                    }
                    catch (Exception msg)
                    {
                        _erros += 1;
                        throw;
                    } 
                }

                progressDTS.Value = Funcoes.Funcoes.ConvertePara.Int(
                    (100 * _progress) / listaAlim.Count);
            }

            dgListagem.DataSource = dtsBll.Listar();

            lblIncluidos.Text = Funcoes.Funcoes.ConvertePara.String(_acertos);
            lblErros.Text = Funcoes.Funcoes.ConvertePara.String(_erros);
        }
    }
}
