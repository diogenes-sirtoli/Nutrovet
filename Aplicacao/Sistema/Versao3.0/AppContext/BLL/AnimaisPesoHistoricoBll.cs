using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
	public class clAnimaisPesoHistoricoBll
	{
		public bllRetorno Inserir(AnimaisPesoHistorico _peso)
		{
			bllRetorno ret = _peso.ValidarRegras(true);
			CrudDal crud = new CrudDal();

			try
			{
				if (ret.retorno)
				{
					if (ret.mensagem != "Registro Reativado")
					{
						crud.Inserir(_peso);
					}

					return bllRetorno.GeraRetorno(true,
						"INSERÇÃO efetuada com sucesso!!!");
				}
				else
				{
					return ret;
				}
			}
			catch
			{
				return bllRetorno.GeraRetorno(false,
					"Não foi possível efetuar a INSERÇÃO!!!");
			}
		}

		public bllRetorno Alterar(AnimaisPesoHistorico _peso)
		{
			bllRetorno ret = _peso.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_peso);

					return bllRetorno.GeraRetorno(true, 
						"ALTERAÇÃO efetuada com sucesso!!!");
				}
				catch
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

		public AnimaisPesoHistorico Carregar(int _id)
		{
			CrudDal crud = new CrudDal();

			AnimaisPesoHistorico _peso = null;

			var consulta = crud.ExecutarComando<AnimaisPesoHistorico>(string.Format(@"
				Select *
				From AnimaisPesoHistorico
				Where (IdAnimal = {0})", _id));

			foreach (var item in consulta)
			{
				_peso = item;
			}

			return _peso;
		}

		public bllRetorno Excluir(AnimaisPesoHistorico _peso)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();

			try
			{
				crud.Excluir(_peso);

				msg = bllRetorno.GeraRetorno(true,
					"EXCLUSÃO efetuada com sucesso!!!");
			}
			catch (Exception err)
			{
				var erro = err;
				msg = bllRetorno.GeraRetorno(false,
					"Não foi Possível Efetuar a EXCLUSÃO!!!");
			}

			return msg;
		}

	  //  public List<TORacasBll> Listar(string _pesqNome, int _idEsp, int _tamPag, 
	  //      int _pagAtual)
	  //  {
	  //      CrudDal crud = new CrudDal();

	  //      string _sql = string.Format(@"
	  //          DECLARE @PageNumber AS INT, @RowspPage AS INT
	  //          SET @PageNumber = {1}
	  //          SET @RowspPage = {0}                 

	  //          SELECT  IdEspecie, Especie, IdRaca, Raca, IdadeAdulta, CrescInicial, 
	  //                  CrescFinal, Ativo, IdOperador, IP, DataCadastro
	  //          FROM    (
	  //                      SELECT	ROW_NUMBER() OVER(ORDER BY r.Raca) AS NUMBER, 
	  //                              r.IdEspecie, e.Especie, r.IdRaca, r.Raca, 
	  //                              r.IdadeAdulta, r.CrescInicial, r.CrescFinal, r.Ativo, 
	  //                              r.IdOperador, r.IP, r.DataCadastro
	  //                      FROM    AnimaisAuxEspecies AS e INNER JOIN
	  //                                  AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie
	  //                      WHERE   (r.Ativo = 1) ", 
	  //                      _tamPag, _pagAtual);

	  //      if (_idEsp > 0)
	  //      {
	  //          _sql += string.Format(@" AND 
	  //                              (r.IdEspecie = {0})", _idEsp);
	  //      }

	  //      if (_pesqNome != "")
	  //      {
	  //          _sql += string.Format(@" AND 
	  //                              (r.Raca Like '%{0}%' COLLATE Latin1_General_CI_AI)", 
	  //                              _pesqNome);
	  //      }

	  //      _sql += @") AS TBL
	  //          WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						//(@PageNumber * @RowspPage)
	  //          ORDER BY    Raca";

	  //      var lista = crud.Listar<TORacasBll>(_sql);

	  //      return lista.ToList();
	  //  }

	  //  public int TotalPaginas(string _pesqNome, int _idEsp, int _tamPag)
	  //  {
	  //      CrudDal crud = new CrudDal();

	  //      string _sql = @"
	  //          SELECT	Count(IdRaca) Total
	  //          FROM	AnimaisAuxRacas
	  //          WHERE   (Ativo = 1)";

	  //      if (_idEsp > 0)
	  //      {
	  //          _sql += string.Format(@" AND 
	  //                  (IdEspecie = {0})", _idEsp);
	  //      }

	  //      if (_pesqNome != "")
	  //      {
	  //          _sql += string.Format(@" AND 
	  //                   (Raca LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
	  //      }

	  //      int _total = crud.ExecutarComandoTipoInteiro(_sql);

	  //      decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

	  //      return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
	  //  }

		public List<TORacasBll> Listar(int _idAnimal)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SET DATEFORMAT dmy 

				SELECT	IdAnimal, IdHistorico, Peso, DataHistorico,	
						Ativo, IdOperador, IP, DataCadastro
				FROM	AnimaisPesoHistorico
				WHERE	(IdAnimal = {0}) AND (Ativo = 1)
				ORDER BY DataHistorico DESC, Peso", _idAnimal);

			var lista = crud.Listar<TORacasBll>(_sql);

			return lista.ToList();
		}

		public bool PesoExiste(Animai _peso)
		{
			CrudDal crud = new CrudDal();
			bool retorno = false;
			string _pesoAtual = Funcoes.Funcoes.ConvertePara.String(_peso.PesoAtual);

			string _pesoNew = _pesoAtual.Replace(",", ".");

			string _sql = string.Format(@"
				SELECT	COUNT (h.IdHistorico) Total
				FROM	Animaispesohistorico h
				WHERE	(h.IdAnimal = {0}) And (h.Peso = {1}) AND
						(h.DataHistorico = '{2}') AND (h.Ativo = 1)", 
						_peso.IdAnimal, _pesoNew, 
						DateTime.Today);

			int reg = crud.ExecutarComandoTipoInteiro(_sql);

			retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

			return retorno;
		}
	}
}
