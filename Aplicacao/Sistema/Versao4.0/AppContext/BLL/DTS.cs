using DAL;
using DCL;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class clDTS
    {
        public bllRetorno Inserir(AlimentoNutrientes _alimComp)
        {
            CrudsDal<AlimentoNutrientes> crud = new CrudsDal<AlimentoNutrientes>();

            try
            {
                crud.Inserir(_alimComp);
                crud.Dispose();

                return bllRetorno.GeraRetorno(true,
                    "INSERÇÃO efetuada com sucesso!!!");

            }
            catch
            {
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a INSERÇÃO!!!");
            }
        }

        public List<TOAlimentoNutrientesBll> ListagemAlimentos()
        {
            CrudsDal<TOAlimentoNutrientesBll> crud = new CrudsDal<TOAlimentoNutrientesBll>();

            string _sql = @"
                SELECT	Alimento.IdAlimento, Alimento.Alimento, (78) IdComponente, 
                        'Beta-sitosterol' Componente,
		                PlanilhaExcel.[Beta-sitosterol] ValorNaoConvert
                FROM	(SELECT	f.Fonte + CAST(a.NDB_No AS Varchar) AS Codigo, a.IdAlimento, 
				                a.IdFonte, f.Fonte, a.NDB_No, a.Alimento
		                 FROM	Alimentos AS a INNER JOIN
					            AlimentosAuxFontes AS f ON a.IdFonte = f.IdFonte) AS 
		                        Alimento INNER JOIN
                                PlanilhaExcel ON Alimento.Codigo = PlanilhaExcel.Código
                Order By IdAlimento";

            var lista = crud.Listar(_sql);
            crud.Dispose();

            return lista.ToList();
        }

        public List<TOAlimentoNutrientesBll> Listar()
        {
            CrudsDal<TOAlimentoNutrientesBll> crud = new CrudsDal<TOAlimentoNutrientesBll>();

            string _sql = @"
                SELECT  AlimentoComponentes.IdAlimento, Alimentos.Alimento, 
                        AlimentoComponentes.IdAlimCompon, AlimentoComponentes.IdComponente, 
                        AlimentosAuxComponentes.Componente, AlimentoComponentes.Valor
                FROM    AlimentoComponentes INNER JOIN
                            Alimentos ON AlimentoComponentes.IdAlimento = 
                                Alimentos.IdAlimento INNER JOIN
                            AlimentosAuxComponentes ON AlimentoComponentes.IdComponente = 
                                AlimentosAuxComponentes.IdComponente";

            var lista = crud.Listar(_sql);
            crud.Dispose();

            return lista.ToList();
        }
    }
}
