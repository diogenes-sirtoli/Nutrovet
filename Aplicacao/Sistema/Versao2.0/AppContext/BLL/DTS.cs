using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DCL;

namespace BLL
{
    public class clDTS
    {
        public bllRetorno Inserir(AlimentoNutriente _alimComp)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Inserir(_alimComp);

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
            CrudDal crud = new CrudDal();

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

            var lista = crud.Listar<TOAlimentoNutrientesBll>(_sql);

            return lista.ToList();
        }

        public List<TOAlimentoNutrientesBll> Listar()
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT  AlimentoComponentes.IdAlimento, Alimentos.Alimento, 
                        AlimentoComponentes.IdAlimCompon, AlimentoComponentes.IdComponente, 
                        AlimentosAuxComponentes.Componente, AlimentoComponentes.Valor
                FROM    AlimentoComponentes INNER JOIN
                            Alimentos ON AlimentoComponentes.IdAlimento = 
                                Alimentos.IdAlimento INNER JOIN
                            AlimentosAuxComponentes ON AlimentoComponentes.IdComponente = 
                                AlimentosAuxComponentes.IdComponente";

            var lista = crud.Listar<TOAlimentoNutrientesBll>(_sql);

            return lista.ToList();
        }
    }
}
