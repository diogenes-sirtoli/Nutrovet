using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Web.UI.WebControls;

namespace BLL
{
    public class clDietasAlimentosBll
    {
        public bllRetorno Inserir(DietasAlimento _dietaAlim)
        {
            bllRetorno ret = _dietaAlim.ValidarRegras(true);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_dietaAlim);

                    return bllRetorno.GeraRetorno(true, 
                        "INSERÇÃO efetuada com sucesso!!!");

                }
                catch (Exception err)
                {
                    return bllRetorno.GeraRetorno(false, 
                        "Não foi possível efetuar a INSERÇÃO!!!");
                }
            }
            else
            {
                return ret;
            }
        }

        public bllRetorno Alterar(DietasAlimento _dietaAlim)
        {
            bllRetorno ret = _dietaAlim.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_dietaAlim);

                    return bllRetorno.GeraRetorno(true, 
                        "ALTERAÇÃO efetuada com sucesso!!!");
                }
                catch (Exception err)
                {
                    return bllRetorno.GeraRetorno(false, 
                        "Não foi possível efetuar a ALTERAÇÃO!!!");
                }
            }
            else
            {
                return ret;
            }
        }

        public bllRetorno Excluir(DietasAlimento _dietaAlim)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_dietaAlim);

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                return bllRetorno.GeraRetorno(false, 
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

        public bllRetorno Excluir(int _idDieta, int _idAlimento)
        {
            CrudDal crud = new CrudDal();
            string _sql = string.Format(@"
                Delete  From DietasAlimentos
                Where   (IdDieta = {0}) AND 
                        (IdAlimento = {1})", _idDieta, _idAlimento);

            try
            {
                crud.ExecutarComando(_sql);

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

        public DietasAlimento Carregar(int _idDietaAlim)
        {
            CrudDal crud = new CrudDal();

            DietasAlimento _dietaAlim = null;

            var consulta = crud.ExecutarComando<DietasAlimento>(string.Format(@"
                Select *
                From DietasAlimento
                Where (IdDietaAlim = {0})", _idDietaAlim));

            foreach (var item in consulta)
            {
                _dietaAlim = item;
            }

            return _dietaAlim;
        }

        public List<TODietasAlimentosBll> ListarTO(int _idDieta, int _idTpIndic)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SELECT	da.IdDieta, d.Dieta, da.IdDietaAlim, da.IdAlimento, 
		                a.Alimento, da.IdTpIndicacao, 
		                (Case da.IdTpIndicacao
			                When 1 Then 'Indicado'
			                When 2 Then 'Contraindicado'
		                 End) TipoIndicacao,
		                da.Quant, da.Ativo, 
		                da.IdOperador, da.IP, da.DataCadastro
                FROM	Alimentos AS a INNER JOIN
			                DietasAlimentos AS da ON a.IdAlimento = 
				                da.IdAlimento INNER JOIN
                            Dietas AS d ON da.IdDieta = d.IdDieta
                WHERE	(da.Ativo = 1) AND (da.IdDieta = {0})",
                _idDieta);

            if (_idTpIndic > 0)
            {
                _sql += string.Format(@" AND
                        (da.IdTpIndicacao = {0})", _idTpIndic);
            }

            _sql += @"
                ORDER BY d.Dieta, a.Alimento";

            var lista = crud.Listar<TODietasAlimentosBll>(_sql);

            return lista.ToList();
        }
    }
}
