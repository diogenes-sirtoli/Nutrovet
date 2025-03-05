using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Web.UI.WebControls;

namespace BLL
{
	public class clAlimentosNutrientesBll
	{
		public bllRetorno Inserir(AlimentoNutriente _alimNutr)
		{
			bllRetorno ret = _alimNutr.ValidarRegras(true);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Inserir(_alimNutr);

					return bllRetorno.GeraRetorno(true, 
						"INSERÇÃO efetuada com sucesso!!!");

				}
				catch (Exception err)
				{
					var erro = err;
					return bllRetorno.GeraRetorno(false, 
						"Não foi possível efetuar a INSERÇÃO!!!");
				}
			}
			else
			{
				return ret;
			}
		}

		public bllRetorno Alterar(AlimentoNutriente _alimNutr)
		{
			bllRetorno ret = _alimNutr.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_alimNutr);

					return bllRetorno.GeraRetorno(true, 
						"ALTERAÇÃO efetuada com sucesso!!!");

				}
				catch (Exception err)
				{
					var erro = err;
					return bllRetorno.GeraRetorno(false, 
						"Não foi possível efetuar a ALTERAÇÃO!!!");
				}
			}
			else
			{
				return ret;
			}
		}

		public bllRetorno Excluir(AlimentoNutriente _alimNutr)
		{
			CrudDal crud = new CrudDal();

			try
			{
				crud.Excluir(_alimNutr);

				return bllRetorno.GeraRetorno(true,
					"EXCLUSÃO efetuada com sucesso!!!");
			}
			catch (Exception err)
			{
				var erro = err;
				return bllRetorno.GeraRetorno(false, 
					"Não foi possível efetuar a EXCLUSÃO!!!");
			}
		}

		public AlimentoNutriente Carregar(int _alimNutr)
		{
			CrudDal crud = new CrudDal();

			AlimentoNutriente _nutri = null;

			var logon = crud.ExecutarComando<AlimentoNutriente>(string.Format(@"
				Select *
				From AlimentoNutrientes
				Where (IdAlimNutr = {0})", _alimNutr));

			foreach (var item in logon)
			{
				_nutri = item;
			}

			return _nutri;
		}

		public AlimentoNutriente Carregar(int _idAlim, int _idNutr)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(
				@"Select *
				  From AlimentoNutrientes
				  Where (IdAlimento = {0}) AND (IdNutr = {1})",
				_idAlim, _idNutr);

			var ret = crud.ExecutarComando<AlimentoNutriente>(_sql).SingleOrDefault();

			return ret;
		}

		public bool ExisteNutriente(int _idAlim, int _idNutr)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(
				@"Select Count(IdAlimNutr) Total
				  From AlimentoNutrientes
				  Where (IdAlimento = {0}) AND (IdNutr = {1})",
				_idAlim, _idNutr);

			int ret = crud.ExecutarComandoTipoInteiro(_sql);

			return Funcoes.Funcoes.ConvertePara.Bool(ret);
		}

		public List<TOAlimentoNutrientesBll> ListarTO(int _idAlim)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SELECT  AlimentoNutrientes.IdAlimento, Alimentos.Alimento, 
						AlimentoNutrientes.IdAlimNutr, AlimentoNutrientes.IdNutr, 
						Nutrientes.Nutriente, AlimentoNutrientes.Valor, 
						AlimentoNutrientes.IdUnidade,
						(Case AlimentoNutrientes.IdUnidade
							WHEN 1 THEN 'µg' 
							WHEN 2 THEN 'g' 
							WHEN 3 THEN 'mg' 
							WHEN 4 THEN 'kcal' 
							WHEN 5 THEN 'UI'
							WHEN 6 THEN 'Proporção'
							WHEN 7 THEN 'mcg/kg' 
							WHEN 8 THEN 'mg/animal'
							WHEN 9 THEN 'mg/kg'
							WHEN 10 THEN 'UI/kg'
						 End) Unidade,
						AlimentoNutrientes.Ativo, AlimentoNutrientes.IdOperador, 
						AlimentoNutrientes.IP, AlimentoNutrientes.DataCadastro
				FROM    Alimentos INNER JOIN
						AlimentoNutrientes ON Alimentos.IdAlimento = 
							AlimentoNutrientes.IdAlimento INNER JOIN
						Nutrientes ON AlimentoNutrientes.IdNutr = 
							Nutrientes.IdNutr
				WHERE   (AlimentoNutrientes.IdAlimento = {0}) AND 
						(AlimentoNutrientes.Ativo = 1)
				ORDER BY Alimentos.Alimento, Nutrientes.Nutriente",
				_idAlim);

			var lista = crud.Listar<TOAlimentoNutrientesBll>(_sql);

			return lista.ToList();
		}

		public List<TOAlimentoNutrientesBll> ListarTO(int _idCardapio, int _idAlim)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SELECT	ca.IdAlimento, a.Alimento, an.IdAlimNutr, an.IdNutr, n.Nutriente, 
						an.Valor, ca.Quant, ((an.Valor * ca.Quant) / 100) ValCalc, 
						an.IdUnidade, 
						(CASE an.IdUnidade 
							WHEN 1 THEN 'µg' 
							WHEN 2 THEN 'g' 
							WHEN 3 THEN 'mg' 
							WHEN 4 THEN 'kcal' 
							WHEN 5 THEN 'UI'
							WHEN 6 THEN 'Proporção'
							WHEN 7 THEN 'mcg/kg' 
							WHEN 8 THEN 'mg/animal'
							WHEN 9 THEN 'mg/kg'
							WHEN 10 THEN 'UI/kg' 
						END) AS Unidade, an.Ativo
				FROM	Alimentos AS a INNER JOIN
							AlimentoNutrientes AS an ON a.IdAlimento = 
								an.IdAlimento INNER JOIN
							Nutrientes AS n ON an.IdNutr = n.IdNutr INNER JOIN
							CardapiosAlimentos AS ca ON a.IdAlimento = ca.IdAlimento
				WHERE	(an.Ativo = 1) AND (ca.IdAlimento = {0}) AND (ca.IdCardapio = {1})
				ORDER BY a.Alimento, n.Nutriente",
				_idAlim, _idCardapio);

			var lista = crud.Listar<TOAlimentoNutrientesBll>(_sql);

			return lista.ToList();
		}
	}
}
