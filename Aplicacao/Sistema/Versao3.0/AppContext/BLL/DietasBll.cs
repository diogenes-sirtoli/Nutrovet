using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
	public class clDietasBll
	{
		public bllRetorno Inserir(Dietas _dieta)
		{
			bllRetorno ret = _dieta.ValidarRegras(true);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Inserir(_dieta);

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

		public bllRetorno Alterar(Dietas _dieta)
		{
			bllRetorno ret = _dieta.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_dieta);

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

		public Dietas Carregar(int _id)
		{
			CrudDal crud = new CrudDal();

			Dietas _dietas = null;

			var consulta = crud.ExecutarComando<Dietas>(string.Format(@"
				SELECT	IdEspecie, IdDieta, Dieta, Carboidrato, Proteina, 
						Gordura, Ativo, IdOperador, IP, DataCadastro, 
						Versao
				From Dietas
				Where (IdDieta = {0})", _id));

			foreach (var item in consulta)
			{
				_dietas = item;
			}

			return _dietas;
		}

        public TODietasBll CarregarTO(int _id)
        {
            CrudDal crud = new CrudDal();

            TODietasBll _dietas = null;

            var consulta = crud.ExecutarComando<TODietasBll>(string.Format(@"
				SELECT	d.IdEspecie, e.Especie, d.IdDieta, d.Dieta, d.Carboidrato, 
						d.Proteina, d.Gordura, d.Ativo, d.IdOperador, d.IP, 
						d.DataCadastro, d.Versao
				FROM	Dietas AS d INNER JOIN
							AnimaisAuxEspecies AS e ON d.IdEspecie = e.IdEspecie
				WHERE	(d.IdDieta = {0})", _id));

            foreach (var item in consulta)
            {
                _dietas = item;
            }

            return _dietas;
        }

  //      public Dietas Carregar(string _dieta)
		//{
		//	CrudDal crud = new CrudDal();
		//	Dietas _retorno = new Dietas();

		//	string _sql = string.Format(
  //              @"SELECT	IdEspecie, IdDieta, Dieta, Carboidrato, Proteina, 
		//					Gordura, Ativo, IdOperador, IP, DataCadastro, 
		//					Versao
		//		  From Dietas
		//		  Where (Dieta = '{0}')", _dieta);

		//	var ret = crud.ExecutarComando<Dietas>(_sql);

		//	foreach (var item in ret)
		//	{
		//		_retorno = item;
		//	}

		//	return _retorno;
		//}

		public bllRetorno Excluir(Dietas _dieta)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();

			try
			{
				crud.Excluir(_dieta);

				msg = bllRetorno.GeraRetorno(true,
						  "EXCLUSÃO efetuada com sucesso!!!");
			}
			catch (Exception err)
			{
				var erro = err;
				msg = Alterar(_dieta);
			}

			return msg;
		}

		public List<Dietas> Listar()
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT  IdEspecie, IdDieta, Dieta, Carboidrato, Proteina, 
						Gordura, Ativo, IdOperador, IP, DataCadastro
				FROM    Dietas
				WHERE   (Ativo = 1) 
				ORDER BY Dieta";

			var lista = crud.Listar<Dietas>(_sql);

			return lista.ToList();
		}

        public List<TODietasBll> ListarTO()
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
				SELECT	d.IdEspecie, e.Especie, d.IdDieta, d.Dieta, d.Carboidrato, 
						d.Proteina, d.Gordura, d.Ativo, d.IdOperador, d.IP, 
						d.DataCadastro
				FROM	Dietas AS d INNER JOIN
							AnimaisAuxEspecies AS e ON d.IdEspecie = e.IdEspecie
				WHERE	(d.Ativo = 1)
				ORDER BY d.Dieta";

            var lista = crud.Listar<TODietasBll>(_sql);

            return lista.ToList();
        }

        public List<Dietas> Listar(int _idEspecie)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				SELECT  IdEspecie, IdDieta, Dieta, Carboidrato, Proteina, 
						Gordura, Ativo, IdOperador, IP, DataCadastro
				FROM    Dietas
				WHERE   (Ativo = 1) AND (IdEspecie = {0}) 
				ORDER BY Dieta", _idEspecie);

            var lista = crud.Listar<Dietas>(_sql);

            return lista.ToList();
        }

        public List<TODietasBll> ListarTO(int _idEspecie)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				SELECT	d.IdEspecie, e.Especie, d.IdDieta, d.Dieta, d.Carboidrato, 
						d.Proteina, d.Gordura, d.Ativo, d.IdOperador, d.IP, 
						d.DataCadastro
				FROM	Dietas AS d INNER JOIN
							AnimaisAuxEspecies AS e ON d.IdEspecie = e.IdEspecie
				WHERE	(d.Ativo = 1) AND (d.IdEspecie = {0}) 
				ORDER BY d.Dieta", _idEspecie);

            var lista = crud.Listar<TODietasBll>(_sql);

            return lista.ToList();
        }

        public List<TODietasBll> Listar(string _pesqNome, int _idEspecie, int _tamPag, int _pagAtual)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0}                 

				SELECT  IdEspecie, Especie, IdDieta, Dieta, Carboidrato, Proteina, 
						Gordura, Total, Ativo, IdOperador, IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY d.IdEspecie, d.Dieta) AS NUMBER, 
									d.IdEspecie, e.Especie, d.IdDieta, d.Dieta, d.Carboidrato, 
									d.Proteina, d.Gordura, 
									(d.Carboidrato + d.Proteina + d.Gordura) Total, 
									d.Ativo, d.IdOperador, d.IP, d.DataCadastro
							FROM	Dietas AS d INNER JOIN
										AnimaisAuxEspecies AS e ON d.IdEspecie = e.IdEspecie
							WHERE   (d.Ativo = 1) ",
							_tamPag, _pagAtual);

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
									(d.Dieta Like '%{0}%' COLLATE Latin1_General_CI_AI)",
									_pesqNome);
			}

            if (_idEspecie > 0)
            {
                _sql += string.Format(@" AND 
									(d.IdEspecie = {0})", _idEspecie);
            }

            _sql += @") AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
				ORDER BY    Dieta";

			var lista = crud.Listar<TODietasBll>(_sql);

			return lista.ToList();
		}

		public int TotalPaginas(string _pesqNome, int _idEspecie, int _tamPag)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	Count(d.IdDieta) Total
				FROM	Dietas AS d INNER JOIN
							AnimaisAuxEspecies AS e ON d.IdEspecie = e.IdEspecie
				WHERE   (d.Ativo = 1)";

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
						 (d.Dieta LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
			}

            if (_idEspecie > 0)
            {
                _sql += string.Format(@" AND 
						(d.IdEspecie = {0})", _idEspecie);
            }

            int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}
	}
}
