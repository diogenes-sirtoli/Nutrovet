using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web.UI.WebControls;
using System.Web;

namespace BLL
{
	public class clAnimaisBll
	{
		public bllRetorno Inserir(Animai _animal)
		{
			bllRetorno ret = _animal.ValidarRegras(true);
			CrudDal crud = new CrudDal();

			try
			{
				if (ret.retorno)
				{
					if (ret.mensagem != "Registro Reativado")
					{
						crud.Inserir(_animal);
					}

					return bllRetorno.GeraRetorno(true,
						"INSERÇÃO efetuada com sucesso!!!");
				}
				else
				{
					return ret;
				}
			}
			catch (Exception err)
			{
				List<object> list = new List<object>();
				list.Add(err.Message);

				return bllRetorno.GeraRetorno(false,
					"Não foi possível efetuar a INSERÇÃO!!!", list);
			}
		}

		public bllRetorno Alterar(Animai _animal)
		{
			bllRetorno ret = _animal.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_animal);

					return bllRetorno.GeraRetorno(true, 
						"ALTERAÇÃO efetuada com sucesso!!!");
				}
				catch (Exception err)
				{
                    List<object> list = new List<object>();
                    list.Add(err.Message);

                    return bllRetorno.GeraRetorno(false, 
						"Não foi possível efetuar a ALTERAÇÃO!!!", list);
				}
			}
			else
			{
				return ret;
			}
		}

		public Animai Carregar(int _id)
		{
			CrudDal crud = new CrudDal();

			Animai _animais = null;

			var consulta = crud.ExecutarComando<Animai>(string.Format(@"
				Select *
				From Animais
				Where (IdAnimal = {0})", _id));

			foreach (var item in consulta)
			{
				_animais = item;
			}

			return _animais;
		}

		public TOAnimaisBll CarregarTO(int _id)
		{
			CrudDal crud = new CrudDal();
			string _sql = string.Format(@"
				Execute AnimaisCarregarTO {0}, 0, ''", _id);

			TOAnimaisBll _animais = crud.ExecutarComando<TOAnimaisBll>(_sql).FirstOrDefault();

			if (_animais != null)
			{
				Funcoes.Funcoes.Datas.IdadeDecomposta _idadeDecomposta =
						Funcoes.Funcoes.Datas.CalculaIdadeDecomposta(
							_animais.DtNascim != null ? _animais.DtNascim.Value : DateTime.Today);

				_animais.Idade = _idadeDecomposta.Anos;
				_animais.IdadeAno = _idadeDecomposta.Anos;
				_animais.IdadeMes = _idadeDecomposta.Meses;
				_animais.IdadeDia = _idadeDecomposta.Dias; 
			}

			return _animais;
		}

		public TOAnimaisBll Carregar(int _idTutor, string _animal)
		{
			CrudDal crud = new CrudDal();
			string _sql = string.Format(@"
				Execute AnimaisCarregarTO 0, {0}, '{1}'", _idTutor, _animal);

			TOAnimaisBll _animais = crud.ExecutarComando<TOAnimaisBll>(_sql).FirstOrDefault();

			Funcoes.Funcoes.Datas.IdadeDecomposta _idadeDecomposta =
					Funcoes.Funcoes.Datas.CalculaIdadeDecomposta(
						_animais.DtNascim != null ? _animais.DtNascim.Value : DateTime.Today);

			_animais.Idade = _idadeDecomposta.Anos;
			_animais.IdadeAno = _idadeDecomposta.Anos;
			_animais.IdadeMes = _idadeDecomposta.Meses;
			_animais.IdadeDia = _idadeDecomposta.Dias;

			return _animais;
		}

		public bllRetorno Excluir(Animai _animal)
		{
			CrudDal crud = new CrudDal();
			
			try
			{
				crud.Excluir(_animal);

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

		/// <summary>
		/// Lista Por Tutor ou Assinante
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_tpLista">1 - Assinante, 2 - Tutor</param>
		/// <returns></returns>
		public List<TOAnimaisBll> Listar(int _idTutor, int _idCliente, 
			DominiosBll.ListarAnimaisPor _tpLista)
		{
			CrudDal crud = new CrudDal();
			List<TOAnimaisBll> _retLista = new List<TOAnimaisBll>();
			TOAnimaisBll _retItem;

			string _sql = string.Format(@"Execute AnimaisListar {0}, {1}, {2}",
				_idTutor, _idCliente, (int)_tpLista);
			List<TOAnimaisBll> _animaisLista = crud.Listar<TOAnimaisBll>(_sql).ToList();

			foreach (var item in _animaisLista)
			{
				Funcoes.Funcoes.Datas.IdadeDecomposta _idadeDecomposta =
					Funcoes.Funcoes.Datas.CalculaIdadeDecomposta(
						item.DtNascim != null ? item.DtNascim.Value : DateTime.Today);

				_retItem = item;
				_retItem.Idade = _idadeDecomposta.Anos;
				_retItem.IdadeAno = _idadeDecomposta.Anos;
				_retItem.IdadeMes = _idadeDecomposta.Meses;
				_retItem.IdadeDia = _idadeDecomposta.Dias;

				_retLista.Add(_retItem);
			}

			return _retLista.ToList();
		}
		
		public List<TOAnimaisBll> Listar(string _pAnimal, int _idCliente,
			int _tamPag, int _pagAtual)
		{
			CrudDal crud = new CrudDal();
			List<TOAnimaisBll> _retLista = new List<TOAnimaisBll>();
			TOAnimaisBll _retItem;

			string _sql = string.Format(@"
				Execute AnimaisListarSelecao 
					@pesquisaAnimal = '{0}', 
					@idCliente = {1}, 
					@RowspPage = {2}, 
					@PageNumber = {3}",
				_pAnimal, _idCliente, _tamPag, _pagAtual);
			List<TOAnimaisBll> _animaisLista = crud.Listar<TOAnimaisBll>(_sql).ToList();

			foreach (var item in _animaisLista)
			{
				Funcoes.Funcoes.Datas.IdadeDecomposta _idadeDecomposta =
					Funcoes.Funcoes.Datas.CalculaIdadeDecomposta(
						item.DtNascim != null ? item.DtNascim.Value : DateTime.Today);

				_retItem = item;
				_retItem.Idade = _idadeDecomposta.Anos;
				_retItem.IdadeAno = _idadeDecomposta.Anos;
				_retItem.IdadeMes = _idadeDecomposta.Meses;
				_retItem.IdadeDia = _idadeDecomposta.Dias;

				_retLista.Add(_retItem);
			}

			return _retLista.ToList();
		}

		public int TotalPaginas(string _pAnimal, int _idCliente, int _tamPag)
		{
			CrudDal crud = new CrudDal();
			string _sql = string.Format(@"
				Execute AnimaisListarSelecaoTotalPaginas 
					@pesquisaAnimal = '{0}', 
					@idCliente = {1}", _pAnimal, _idCliente);

			int _total = crud.ExecutarComandoTipoInteiro(_sql);
			
			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}

		public int TotalAnimaisCadastrados(int _idCliente)
		{
			CrudDal crud = new CrudDal();
			string _sql = string.Format(@"
				Execute AnimaisListarSelecaoTotalPaginas 
					@pesquisaAnimal = '', 
					@idCliente = {0}", _idCliente);

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			return _total;
		}

        public int TotalCardapiosDoAnimal(int _idAnimal)
        {
            CrudDal crud = new CrudDal();
            int retorno = 0;
            string _sql = string.Format(@"
				SELECT	COUNT(IdCardapio) AS Total
				FROM	Cardapio
				WHERE	(IdAnimal = {0})", _idAnimal);

            retorno = crud.ExecutarComandoTipoInteiro(_sql);

            return retorno;
        }

        public int TotalPesosDoAnimal(int _idAnimal)
        {
            CrudDal crud = new CrudDal();
            int retorno = 0;
            string _sql = string.Format(@"
				SELECT	COUNT(IdHistorico) AS Total
				FROM	AnimaisPesoHistorico
				WHERE	(IdAnimal = {0})", _idAnimal);

            retorno = crud.ExecutarComandoTipoInteiro(_sql);

            return retorno;
        }

        public ListItem[] ListarSexo()
		{
			ListItem[] sexo = Funcoes.Funcoes.GetEnumList<DominiosBll.Sexo>();

			return sexo;
		}

		public DominiosBll.ExigenciasNutrAuxIndicacoes CrescimentoAnimal(
			TOAnimaisBll _dadosPaciente, bool _gestante, bool _lactante)
		{
			DominiosBll.ExigenciasNutrAuxIndicacoes _retorno = 
				new DominiosBll.ExigenciasNutrAuxIndicacoes();

			long _aniverEmMeses = Funcoes.Funcoes.Datas.DataEmTipos(
				_dadosPaciente.DtNascim.Value.ToString("dd/MM/yyyy"),
				Funcoes.Funcoes.Datas.TipoData.EmMeses);

			if (_gestante)
			{
				_retorno = DominiosBll.ExigenciasNutrAuxIndicacoes.Gestante;
			}
			else if (_lactante)
			{
				_retorno = DominiosBll.ExigenciasNutrAuxIndicacoes.Lactante;
			}
			else if ((_aniverEmMeses >= 0) && (_aniverEmMeses <= 
					Funcoes.Funcoes.ConvertePara.Int(_dadosPaciente.CrescInicial)))
			{
				_retorno = DominiosBll.ExigenciasNutrAuxIndicacoes.CresInicial;
			}
			else if ((_aniverEmMeses > 
						Funcoes.Funcoes.ConvertePara.Int(_dadosPaciente.CrescInicial)) 
					 && 
					 (_aniverEmMeses <= Funcoes.Funcoes.ConvertePara.Int(
						_dadosPaciente.CrescFinal)))
			{
				_retorno = DominiosBll.ExigenciasNutrAuxIndicacoes.CresFinal;
			}
			else if (_aniverEmMeses >= Funcoes.Funcoes.ConvertePara.Int(
					  _dadosPaciente.IdadeAdulta))
			{
				_retorno = DominiosBll.ExigenciasNutrAuxIndicacoes.Adulto;
			}
			
			return _retorno;
		}

		public List<AnimaisPesoHistorico> GeraGrafico(int _idAnimal, DateTime? _dtIni, 
			DateTime? _dtFim)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SET DATEFORMAT dmy                
				SET QUERY_GOVERNOR_COST_LIMIT 0

				Select	h.IdHistorico, h.IdAnimal, h.Peso, h.DataHistorico
				From	AnimaisPesoHistorico h
				Where	(h.IdAnimal = {0}) AND (h.Ativo = 1)", _idAnimal);

			if (_dtIni != null)
			{
				if (_dtFim != null)
				{
					if (_dtIni > _dtFim)
					{
						DateTime? dataNova = _dtFim;
						_dtFim = _dtIni;
						_dtIni = dataNova;
					}

					_sql += string.Format(@" AND
						(h.DataHistorico Between '{0}' And '{1}')", _dtIni, _dtFim);
				}
				else
				{
					_sql += string.Format(@" AND
						(h.DataHistorico = '{0}')", _dtIni);
				}
			}

				_sql += @"
				Order By h.DataHistorico";

			var lista = crud.Listar<AnimaisPesoHistorico>(_sql);

			return lista.ToList();
		}

		public bllRetorno PodeCadastrarAnimal(int _idOperador)
		{
			CrudDal crud = new CrudDal();
			bllRetorno _retorno = new bllRetorno();
			int _totalAnimaisPlano = 0;
			bool _superUsuario = false;

			string _dataAtual = DateTime.Today.ToString("dd/MM/yyyy");
			string _superUsuarioSql = string.Format(@"
				Select SuperUser
				From Acessos
				Where (IdPessoa = {0})", _idOperador);

			foreach (var item in crud.ExecutarComandoTipoBasico<bool>(_superUsuarioSql))
			{
				_superUsuario = item;
			}

			if (!_superUsuario)
			{
				int _totalAnimaisCadastrados = TotalAnimaisCadastrados(_idOperador);

				string _planoVigenteSql = string.Format(@"
				SET DATEFORMAT dmy                
				SET QUERY_GOVERNOR_COST_LIMIT 0

				SELECT  avp.IdVigencia, 
						(Case dNomePlano
							When 1 Then 'Básico'
							When 2 Then 'Intermediário'
							When 3 Then 'Completo'
							When 4 Then 'Receituário'
							When 5 Then 'Prontuário'
							End) Plano, planos.QtdAnimais
				FROM    AcessosVigenciaPlanos AS avp INNER JOIN
						AcessosVigenciaSituacao AS avs ON avp.IdVigencia = 
							avs.IdVigencia INNER JOIN
						PlanosAssinaturas AS planos ON avp.IdPlano = planos.IdPlano    
				Where	(avs.IdSituacao = 3) AND
						(avp.IdVigencia =	(SELECT	MAX(p.IdVigencia) AS IdVigencia
											 FROM	AcessosVigenciaPlanos AS p INNER JOIN
													AcessosVigenciaSituacao AS s ON 
														p.IdVigencia = s.IdVigencia
											 WHERE	(p.IdPessoa = {0}) AND (p.Ativo = 1) AND
													('{1}' BETWEEN p.DtInicial AND p.DtFinal)))",
					_idOperador, _dataAtual);

				TOPlanosBll _planoVigente = crud.ExecutarComando<TOPlanosBll>(
					_planoVigenteSql).FirstOrDefault();
				_totalAnimaisPlano = Funcoes.Funcoes.ConvertePara.Int(_planoVigente.QtdAnimais);

				if (_planoVigente != null)
				{
					int _total = _totalAnimaisPlano - _totalAnimaisCadastrados;

					if (_total > 0)
					{
						_retorno.retorno = true;
						_retorno.mensagem = "Pode Cadastrar!";
					}
					else
					{
						_retorno.retorno = false;
						_retorno.mensagem = "O Limite de Cadastro de Pacientes do Seu Plano foi Atingido!!!";
					}
				}
				else
				{
					_retorno.retorno = true;
					_retorno.mensagem = "Plano Fora da Vigência! Faça um Upgrade em sua Página de Perfil!";
				}
			}
			else
			{
				_retorno.retorno = true;
				_retorno.mensagem = "Pode Cadastrar!";
			}

			return _retorno;
		}
    }
}
