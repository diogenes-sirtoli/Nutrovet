using System;
using System.Collections.Generic;
using System.Linq;
using DCL;
using DAL;
using System.IO;
using System.Web;
using System.Runtime.InteropServices;

namespace BLL
{
	[Serializable]
	public class clCardapioBll
	{
		public bllRetorno Inserir(Cardapio _cardapio, bool _copia)
		{
			bllRetorno ret = (_copia ? _cardapio.ValidarRegrasCopia(true) :
				_cardapio.ValidarRegras(true));

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Inserir(_cardapio);

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

		public bllRetorno Alterar(Cardapio _cardapio)
		{
			bllRetorno ret = _cardapio.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_cardapio);

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

		public Cardapio Carregar(int _id)
		{
			CrudDal crud = new CrudDal();

			Cardapio _cardapio = null;

			var consulta = crud.ExecutarComando<Cardapio>(string.Format(@"
				Select *
				From Cardapio
				Where (IdCardapio = {0})", _id));

			foreach (var item in consulta)
			{
				_cardapio = item;
			}

			return _cardapio;
		}

		public Cardapio Carregar(int _idAnimal, DateTime _dtCardapio)
		{
			CrudDal crud = new CrudDal();
			Cardapio _retorno = new Cardapio();

			string _sql = string.Format(
				@"SET DATEFORMAT dmy

				  Select    Count(IdCardapio) Total
				  From      Cardapio
				  Where     (IdAnimal = {0}) And (DtCardapio = '{1}') And
							(Ativo = 1)", _idAnimal, _dtCardapio.ToString("dd/MM/yyyy"));

			var ret = crud.ExecutarComando<Cardapio>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}
		public TOCardapioBll CarregarTO(int _idCardapio)
		{
			CrudDal crud = new CrudDal();
			TOCardapioBll _cardapioTO = null;

			string _sql = string.Format(@"
				SET DATEFORMAT dmy                   

				SELECT  c.IdPessoa AS IdTutor, p.Nome AS Tutor, c.IdAnimal, 
						a.Nome AS Animal, r.IdEspecie, e.Especie, r.Raca, c.IdCardapio, 
						c.Descricao, c.DtCardapio, a.PesoAtual, a.PesoIdeal, 
						p.EMAil AS TutorEMail,
						(CASE a.Sexo 
							WHEN 1 THEN 'Macho' 
							WHEN 2 THEN 'Fêmea' END) AS Sexo, 
						DATEDIFF(YY, a.DtNascim, GETDATE()) - 
							CASE WHEN RIGHT(CONVERT(VARCHAR(6), GETDATE(), 12), 4) >= 
									RIGHT(CONVERT(VARCHAR(6), a.DtNascim, 12), 4)
								THEN 0 
								ELSE 1 END AS Idade, c.IdDieta,
						d.Dieta, c.FatorEnergia, p.Celular AS TutorFone,
						c.NEM, c.Lactante, c.Gestante, c.LactacaoSemanas, c.NrFilhotes, 
						c.IdDieta, c.EmCardapio, c.Arquivo, c.NrCardapio, c.Observacao, 
						c.Ativo, c.IdOperador, c.IP, c.DataCadastro
				FROM	Tutores INNER JOIN
						Animais AS a ON Tutores.IdTutores = a.IdTutores INNER JOIN
						Pessoas AS p ON Tutores.IdTutor = p.IdPessoa RIGHT OUTER JOIN
						Cardapio AS c ON a.IdAnimal = c.IdAnimal LEFT OUTER JOIN
						Dietas AS d ON c.IdDieta = d.IdDieta LEFT OUTER JOIN
						AnimaisAuxEspecies AS e INNER JOIN
						AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie ON 
							a.IdRaca = r.IdRaca
				Where	(c.Ativo = 1) AND (c.IdCardapio = {0})
				", _idCardapio);

			var consulta = crud.ExecutarComando<TOCardapioBll>(_sql);

			foreach (var item in consulta)
			{
				_cardapioTO = item;
			}

			return _cardapioTO;
		}

		public bllRetorno Excluir(Cardapio _cardapio)
		{
			CrudDal crud = new CrudDal();

			try
			{
				crud.Excluir(_cardapio);

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

        public int TotalAlimentosDoCardapio(int _idCardapio)
        {
            CrudDal crud = new CrudDal();
            int retorno = 0;
            string _sql = string.Format(@"
				Select	Count(ca.IdCardapAlim) Total
				From	CardapiosAlimentos ca
				Where	(ca.IdCardapio = {0})", _idCardapio);

            retorno = crud.ExecutarComandoTipoInteiro(_sql);

            return retorno;
        }

        public List<TOCardapioBll> Listar(int _idAnimal)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SELECT	Tutores.IdTutor, Pessoas.Nome AS Tutor, Cardapio.IdAnimal, 
						Animais.Nome AS Animal, Cardapio.IdCardapio, Cardapio.Descricao, 
						Cardapio.DtCardapio, Animais.PesoIdeal, Cardapio.FatorEnergia, 
						Cardapio.NEM, Cardapio.Gestante, Cardapio.Lactante, 
						Cardapio.LactacaoSemanas, Cardapio.NrFilhotes, Cardapio.IdDieta, 
						Dietas.Dieta, Cardapio.EmCardapio, Cardapio.Arquivo, 
						Cardapio.NrCardapio, Cardapio.Observacao, Cardapio.Ativo, 
						Cardapio.IdOperador, Cardapio.IP, Cardapio.DataCadastro
				FROM	Animais INNER JOIN
							Cardapio ON Animais.IdAnimal = Cardapio.IdAnimal INNER JOIN
							Tutores ON Animais.IdTutores = Tutores.IdTutores INNER JOIN
							Pessoas ON Tutores.IdTutor = Pessoas.IdPessoa LEFT OUTER JOIN
							Dietas ON Cardapio.IdDieta = Dietas.IdDieta
				WHERE	(Cardapio.IdAnimal = {0}) AND (Cardapio.Ativo = 1)
				ORDER BY Cardapio.IdAnimal, Cardapio.DtCardapio DESC", _idAnimal);

			var lista = crud.Listar<TOCardapioBll>(_sql);

			return lista.ToList();
		}

		public List<TOCardapioBll> FiltroFatorEnergia(int _idAnimal)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SELECT  FatorEnergia
				FROM    Cardapio
				WHERE   (IdAnimal = {0}) AND (Ativo = 1) AND
						(FatorEnergia is not null)
				GROUP BY FatorEnergia
				Having  Count(FatorEnergia) > 1
				ORDER BY FatorEnergia", _idAnimal);

			var lista = from l in crud.Listar<TOCardapioBll>(_sql)
						select new TOCardapioBll
						{
							FatorEnergia = Math.Round(Funcoes.Funcoes.ConvertePara.Decimal(
								l.FatorEnergia), 0)
						};

			return lista.ToList();
		}

		public List<TOCardapioBll> Listar(int _usuario, int _idTutor, int _idAnimal, 
			decimal _fator, int _tamPag, int _pagAtual)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0}                 

				SELECT  IdTutor, Tutor, IdAnimal, Animal, IdEspecie, Especie, IdRaca, Raca,
						IdCardapio, Descricao, DtCardapio, PesoIdeal, FatorEnergia, NEM, 
						Gestante, Lactante, LactacaoSemanas, NrFilhotes, IdDieta, Dieta, 
						EmCardapio, Arquivo, NrCardapio, Observacao, Ativo, IdOperador, 
						IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY Cardapio.DtCardapio DESC) AS NUMBER,
									Tutores.IdTutor, Pessoas.Nome AS Tutor, Cardapio.IdAnimal, 
									Animais.Nome AS Animal, AnimaisAuxRacas.IdEspecie, 
									AnimaisAuxEspecies.Especie, Animais.IdRaca, AnimaisAuxRacas.Raca, 
									Cardapio.IdCardapio, Cardapio.Descricao, Cardapio.DtCardapio, 
									Animais.PesoIdeal, Cardapio.FatorEnergia, Cardapio.NEM, 
									Cardapio.Gestante, Cardapio.Lactante, Cardapio.LactacaoSemanas, 
									Cardapio.NrFilhotes, Cardapio.IdDieta, Dietas.Dieta, 
									Cardapio.EmCardapio, Cardapio.NrCardapio, Cardapio.Arquivo, 
									Cardapio.Observacao, Cardapio.Ativo, Cardapio.IdOperador, 
									Cardapio.IP, Cardapio.DataCadastro
							FROM	Cardapio LEFT OUTER JOIN
										Animais INNER JOIN
										Tutores ON Animais.IdTutores = Tutores.IdTutores INNER JOIN
										Pessoas ON Tutores.IdTutor = Pessoas.IdPessoa ON 
											Cardapio.IdAnimal = Animais.IdAnimal LEFT OUTER JOIN
										AnimaisAuxEspecies INNER JOIN
										AnimaisAuxRacas ON AnimaisAuxEspecies.IdEspecie = 
											AnimaisAuxRacas.IdEspecie ON Animais.IdRaca = 
												AnimaisAuxRacas.IdRaca LEFT OUTER JOIN
										Dietas ON Cardapio.IdDieta = Dietas.IdDieta
							WHERE	(Cardapio.Ativo = 1) ",
							_tamPag, _pagAtual);

			if ((_idTutor > 0) && (_idAnimal <= 0))
			{
				_sql += string.Format(@" AND 
									(Tutores.IdTutor = {0}) AND
									(Tutores.IdCliente = {1})", _idTutor, _usuario);
			}
			else if (_idAnimal > 0)
			{
				_sql += string.Format(@" AND 
									(Cardapio.IdAnimal = {0})", _idAnimal);
			}
			else if ((_idTutor <= 0) && (_idAnimal <= 0))
			{
				_sql += string.Format(@" AND 
									(Cardapio.IdPessoa = {0})", _usuario);
			}

			if (_fator > 0)
			{
				_sql += string.Format(@" AND 
									(Cardapio.FatorEnergia = {0})", _fator);
			}

			_sql += @") AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
				ORDER BY    DtCardapio DESC";

			var lista = crud.Listar<TOCardapioBll>(_sql);

			return lista.ToList();
		}

		public int TotalPaginas(int _usuario, int _idTutor, int _idAnimal, 
			decimal _fator, int _tamPag)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	Count(Cardapio.IdCardapio) Total 
				FROM	Cardapio LEFT OUTER JOIN
							Animais INNER JOIN
							Tutores ON Animais.IdTutores = Tutores.IdTutores INNER JOIN
							Pessoas ON Tutores.IdTutor = Pessoas.IdPessoa ON 
								Cardapio.IdAnimal = Animais.IdAnimal LEFT OUTER JOIN
							AnimaisAuxEspecies INNER JOIN
							AnimaisAuxRacas ON AnimaisAuxEspecies.IdEspecie = 
								AnimaisAuxRacas.IdEspecie ON Animais.IdRaca = 
									AnimaisAuxRacas.IdRaca LEFT OUTER JOIN
							Dietas ON Cardapio.IdDieta = Dietas.IdDieta
				WHERE   (Cardapio.Ativo = 1) ";

			if ((_idTutor > 0) && (_idAnimal <= 0))
			{
				_sql += string.Format(@" AND 
						(Tutores.IdTutor = {0}) AND
						(Tutores.IdCliente = {1})", _idTutor, _usuario);
			}
			else if (_idAnimal > 0)
			{
				_sql += string.Format(@" AND 
						 (Cardapio.IdAnimal = {0})", _idAnimal);
			}
			else if ((_idTutor <= 0) && (_idAnimal <= 0))
			{
				_sql += string.Format(@" AND 
						 (Cardapio.IdPessoa = {0})", _usuario);
			}

			if (_fator > 0)
			{
				_sql += string.Format(@" AND 
						 (Cardapio.FatorEnergia = {0})", _fator);
			}

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}

		public double CalculoNEM(TOAnimaisBll _dadosPaciente,
			bool _lactante, bool _gestante, int _nrFilhotes, int _semanasLactacao,
			double _pesoaIdeal, double _fator)
		{
			double _ret = 0;
			clAnimaisBll animaisBll = new clAnimaisBll();

			DominiosBll.ExigenciasNutrAuxIndicacoes situacaoDoAnimal =
				animaisBll.CrescimentoAnimal(_dadosPaciente, _gestante, _lactante);

			switch (situacaoDoAnimal)
			{
				case DominiosBll.ExigenciasNutrAuxIndicacoes.CresInicial:
					{
						switch (_dadosPaciente.IdEspecie)
						{
							case 1:
								{
									_ret = 2 * (_fator * Math.Pow(_pesoaIdeal, 0.75));

									break;
								}
							case 2:
								{
									_ret = 2 * (_fator * Math.Pow(_pesoaIdeal, 0.67));

									break;
								}
						}

						break;
					}
				case DominiosBll.ExigenciasNutrAuxIndicacoes.CresFinal:
					{
						switch (_dadosPaciente.IdEspecie)
						{
							case 1:
								{
									_ret = 1.4 * (_fator * Math.Pow(_pesoaIdeal, 0.75));

									break;
								}
							case 2:
								{
									_ret = 1.6 * (_fator * Math.Pow(_pesoaIdeal, 0.67));

									break;
								}
						}

						break;
					}
				case DominiosBll.ExigenciasNutrAuxIndicacoes.Adulto:
					{
						switch (_dadosPaciente.IdEspecie)
						{
							case 1:
								{
									_ret = _fator * Math.Pow(_pesoaIdeal, 0.75);

									break;
								}
							case 2:
								{
									_ret = _fator * Math.Pow(_pesoaIdeal, 0.67);

									break;
								}
						}

						break;
					}
				case DominiosBll.ExigenciasNutrAuxIndicacoes.Gestante:
					{
						switch (_dadosPaciente.IdEspecie)
						{
							case 1:
								{
									_ret = (130 * Math.Pow(_pesoaIdeal, 0.75)) +
										(26 * _pesoaIdeal);

									break;
								}
							case 2:
								{
									_ret = 140 * Math.Pow(_pesoaIdeal, 0.67);

									break;
								}
						}

						break;
					}
				case DominiosBll.ExigenciasNutrAuxIndicacoes.Lactante:
					{
						switch (_dadosPaciente.IdEspecie)
						{
							case 1://Cães
								{
									double n = ((_nrFilhotes >= 1 && _nrFilhotes <= 4) ?
										_nrFilhotes : 1);
									double m = ((_nrFilhotes >= 5 && _nrFilhotes <= 8) ?
										_nrFilhotes : 0);
									double L;

									switch (_semanasLactacao)
									{
										case 1:
											{
												L = 0.75;

												break;
											}
										case 2:
											{
												L = 0.95;

												break;
											}
										case 3:
											{
												L = 1.10;

												break;
											}
										case 4:
											{
												L = 1.20;

												break;
											}
										case 5:
										case 6:
										case 7:
										default:
											{
												L = 1;

												break;
											}
									}

									_ret = (145 * Math.Pow(_pesoaIdeal, 0.75)) +
										   (_pesoaIdeal * ((24 * n) + (12 * m)) * L);

									break;
								}
							case 2://Gatos
								{
									double L;

									switch (_semanasLactacao)
									{
										case 1:
										case 2:
											{
												L = 0.90;

												break;
											}
										case 3:
										case 4:
											{
												L = 1.20;

												break;
											}
										case 5:
											{
												L = 1.10;

												break;
											}
										case 6:
											{
												L = 1.00;

												break;
											}
										case 7:
											{
												L = 0.80;

												break;
											}
										default:
											{
												L = 1;

												break;
											}
									}

									if (_nrFilhotes < 3)
									{
										_ret = (100 * Math.Pow(_pesoaIdeal, 0.67)) +
											(18 * _pesoaIdeal * L);
									}
									else if ((_nrFilhotes >= 3) && (_nrFilhotes <= 4))
									{
										_ret = (100 * Math.Pow(_pesoaIdeal, 0.67)) +
											(60 * _pesoaIdeal * L);
									}
									else if (_nrFilhotes > 4)
									{
										_ret = (100 * Math.Pow(_pesoaIdeal, 0.67)) +
											(70 * _pesoaIdeal * L);
									}

									break;
								}
						}

						break;
					}
			}

			return _ret;
		}

		public TOCardapioResumoBll CardapioResumo(int _idCardapio, int _idPessoa)
		{
			clCardapioAlimentosBll cardapioAlimentosBll = new clCardapioAlimentosBll();
			TOCardapioResumoBll resumo = new TOCardapioResumoBll();
			Cardapio cardapioDcl = Carregar(_idCardapio);

			List<TOCardapioResumoBll> resumoLista =
				cardapioAlimentosBll.ListarResumoCardapio(_idCardapio, _idPessoa);

			decimal _quant = 0, _carbo = 0, _prot = 0, _gord = 0, _fibra = 0,
				_umid = 0, _energ = 0, _total = 0, _carboPerc = 0, _protPerc = 0,
				_gordPerc = 0, _somaPercent = 0, _quantTotal = 0, _fibraPerc = 0;

			foreach (TOCardapioResumoBll item in resumoLista)
			{
				_quant = (Funcoes.Funcoes.ConvertePara.Decimal(item.Quant) / 100);
				_quantTotal += Funcoes.Funcoes.ConvertePara.Decimal(item.Quant);

				resumo.Carboidrato += Math.Round(
					(item.Carboidrato != null ? item.Carboidrato.Value : 0) * _quant, 0);
				resumo.Proteina += Math.Round(
					(item.Proteina != null ? item.Proteina.Value : 0) * _quant, 0);
				resumo.Gordura += Math.Round(
					(item.Gordura != null ? item.Gordura.Value : 0) * _quant, 0);
				resumo.Fibras += Math.Round(
					(item.Fibras != null ? item.Fibras.Value : 0) * _quant, 1);
				resumo.Umidade += Math.Round(
					(item.Umidade != null ? item.Umidade.Value : 0) * _quant, 0);
				resumo.Energia += Math.Round(
					(item.Energia != null ? item.Energia.Value : 0) * _quant, 0);
			}

			_carbo = 4 * Funcoes.Funcoes.ConvertePara.Decimal(resumo.Carboidrato);
			_prot = 4 * Funcoes.Funcoes.ConvertePara.Decimal(resumo.Proteina);
			_gord = 9 * Funcoes.Funcoes.ConvertePara.Decimal(resumo.Gordura);
			_fibra = Funcoes.Funcoes.ConvertePara.Decimal(resumo.Fibras);
			_umid = Funcoes.Funcoes.ConvertePara.Decimal(resumo.Umidade);
			_energ = Funcoes.Funcoes.ConvertePara.Decimal(resumo.Energia);

			_total = _carbo + _prot + _gord;

			_carboPerc = Math.Round((_total > 0 ? (_carbo * 100) / _total : 0), 0);
			_protPerc = Math.Round((_total > 0 ? (_prot * 100) / _total : 0), 0);
			_gordPerc = Math.Round((_total > 0 ? (_gord * 100) / _total : 0), 0);
			_fibraPerc = Math.Round((_quantTotal > 0 ? (_fibra * 100) /
				_quantTotal : 0), 1);

			_somaPercent = _carboPerc + _protPerc + _gordPerc;

			if ((_somaPercent > 100) && (_carboPerc > 0))
			{
				_carboPerc -= (_somaPercent - 100);
			}
			else if ((_somaPercent < 100) && (_carboPerc > 0))
			{
				_carboPerc += (100 - _somaPercent);
			}

			resumo.CarboG = (resumo.Carboidrato > 0 ?
				string.Format("{0}", Math.Round(resumo.Carboidrato.Value), 0) + " g" : "0 g");
			resumo.ProtG = (resumo.Proteina > 0 ?
				string.Format("{0}", Math.Round(resumo.Proteina.Value), 0) + " g" : "0 g");
			resumo.GordG = (resumo.Gordura > 0 ?
				string.Format("{0}", Math.Round(resumo.Gordura.Value), 0) + " g" : "0 g");

			resumo.CarboP = (_carboPerc > 0 ? string.Format("{0}", _carboPerc) +
				" %" : "0 %");
			resumo.ProtP = (_protPerc > 0 ? string.Format("{0}", _protPerc) +
				" %" : "0 %");
			resumo.GordP = (_gordPerc > 0 ?
				string.Format("{0}", _gordPerc) + " %" : "0 %");

			resumo.FibrasG = (_fibra > 0 ?
				string.Format("{0}", _fibra) + " g" +
				(_fibraPerc > 0 ? string.Format(" - {0}%", _fibraPerc) : "")
				: "0 g");
			resumo.UmidageG = (_umid > 0 ? string.Format("{0}",
				Math.Round(_umid, 0)) + " g" : "0 g");
			resumo.EnergiaKcal = (_energ > 0 ? string.Format("{0}",
				Math.Round(_energ, 0)) + " Kcal" : "0 Kcal");
			resumo.NEM = cardapioDcl.NEM;

			resumo.CarboPerc = _carboPerc;
			resumo.ProtPerc = _protPerc;
			resumo.GordPerc = _gordPerc;

			return resumo;
		}

        public bool ExisteArquivoReceita(string _fileCardapio)
        {
            bool retorno = false, _severLocal = Conexao.ServidorLocal();
            string _path = (_severLocal ? "~/PrintFiles/Avaliacao/Cardapios/" :
                "~/PrintFiles/Producao/Cardapios/");

            if ((_fileCardapio != null) && (_fileCardapio != ""))
            {
                HttpServerUtility _server = HttpContext.Current.Server;
                string _partialPath = _server.MapPath(_path + _fileCardapio);
                FileInfo file = new FileInfo(_partialPath);

                if (file.Exists)
                {
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }
            }

            return retorno;
        }

        public int GerarNumeroArquivo()
        {
            CrudDal crud = new CrudDal();
            string _sql = @"
				Select	(c.NrCardapio + 1) NrCardapio
				From	Cardapio c
				Where	(c.NrCardapio = (Select MAX(car.NrCardapio)
										 From Cardapio car))";

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            if (reg <= 0)
            {
                reg = 1;
            }

            return reg;
        }
    }
}
