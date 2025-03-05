using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web.UI.WebControls;

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
            catch
            {
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a INSERÇÃO!!!");
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

            TOAnimaisBll _animais = null;

            var consulta = crud.ExecutarComando<TOAnimaisBll>(string.Format(@"
                SET DATEFORMAT dmy                
                SET QUERY_GOVERNOR_COST_LIMIT 0
                
                Declare @now date, @dob date, @pesoAtual decimal(18, 3), 
		                @pesoAnimal decimal(18, 3),	@pesoHistorico decimal(18, 3)
                Declare @now_i int, @dob_i int, @days_in_birth_month int
                Declare @years int, @months int, @days int
                Set @now = GetDate() 
                Set @dob = (Select	Animais.DtNascim
			                From	Animais
			                Where	IdAnimal = {0})

                Set @now_i = convert(varchar(8), @now, 112)
                Set @dob_i = convert(varchar(8), @dob, 112)

                Set @years = ( @now_i - @dob_i)/10000
                set @months = (1200 + (month(@now) - month(@dob)) * 100 + 
	                day(@now) - day(@dob)) / 100 % 12

                set @days_in_birth_month = day(dateadd(d, -1, left(
	                convert(varchar(8), dateadd(m, 1, @dob), 112), 6) + '01'))
                set @days = (sign(day(@now) - day(@dob)) + 1) / 2 * (day(@now) - day(@dob))
                            + (sign(day(@dob) - day(@now)) + 1) / 2 * (@days_in_birth_month 
		                    - day(@dob) + day(@now))

                set @pesoHistorico = (Select Top 1 h.Peso
					                  From	AnimaisPesoHistorico h
					                  Where	(h.IdAnimal = {0})
					                  Order By h.IdHistorico desc)

                set @pesoAnimal = (Select a.PesoAtual
				                   From		Animais a
				                   Where	(a.IdAnimal = {0}))

                If (@pesoHistorico > 0) 
	                Set @pesoAtual = @pesoHistorico
                Else
	                Set @pesoAtual = @pesoAnimal

                SELECT	Animais.IdPessoa, Pessoas.Nome, Animais.IdAnimal, 
		                Animais.Nome AS Animal, AnimaisAuxRacas.IdEspecie, 
		                AnimaisAuxEspecies.Especie, Animais.IdRaca, 
		                AnimaisAuxRacas.Raca, Animais.Sexo As IdSexo,
		                (Case Animais.Sexo 
			                When 1 Then 'Macho'
			                When 2 Then 'Fêmea'
		                    End) Sexo, Animais.DtNascim, 
		                @years AS Idade, @years As IdadeAno, @months As IdadeMes, 
                        @days As IdadeDia, @pesoAtual PesoAtual, Animais.PesoIdeal, 
                        AnimaisAuxRacas.IdadeAdulta, AnimaisAuxRacas.CrescInicial, 
                        AnimaisAuxRacas.CrescFinal, Animais.Observacoes, Animais.RecalcularNEM,
                        Animais.Observacoes, Animais.Ativo, 
                        Animais.IdOperador, Animais.IP, Animais.DataCadastro
                FROM	AnimaisAuxEspecies INNER JOIN
			                AnimaisAuxRacas ON AnimaisAuxEspecies.IdEspecie = 
				                AnimaisAuxRacas.IdEspecie RIGHT OUTER JOIN
                        Animais INNER JOIN Pessoas ON Animais.IdPessoa = 
			                Pessoas.IdPessoa ON AnimaisAuxRacas.IdRaca = 
				                Animais.IdRaca
                WHERE   (Animais.IdAnimal = {0}) AND (Animais.Ativo = 1)", _id));

            foreach (var item in consulta)
            {
                _animais = item;
            }

            return _animais;
        }

        public Animai Carregar(int _idPessoa, string _animal)
        {
            CrudDal crud = new CrudDal();
            Animai _retorno = new Animai();

            string _sql = string.Format(
                @"Select *
                  From Animais
                  Where (Nome = '{0}') And (IdPessoa = {1})", 
                _animal, _idPessoa);

            var ret = crud.ExecutarComando<Animai>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
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
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

        public List<TOAnimaisBll> Listar(int _idPessoa)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SET DATEFORMAT dmy                
                SET QUERY_GOVERNOR_COST_LIMIT 0

                SELECT	Animais.IdPessoa, Pessoas.Nome, Animais.IdAnimal, 
		                Animais.Nome AS Animal, AnimaisAuxRacas.IdEspecie, 
		                AnimaisAuxEspecies.Especie, Animais.IdRaca, 
		                AnimaisAuxRacas.Raca, Animais.Sexo As IdSexo,
		                (Case Animais.Sexo 
			                When 1 Then 'Macho'
			                When 2 Then 'Fêmea'
		                 End) Sexo, Animais.DtNascim, 
		                (DATEDIFF(YY, Animais.DtNascim, GETDATE()) - 
		                 CASE 
			                WHEN RIGHT(CONVERT(VARCHAR(6), GETDATE(), 12), 4) >= 
				                 RIGHT(CONVERT(VARCHAR(6), Animais.DtNascim, 12), 4) 
			                THEN 0 
			                ELSE 1 
		                END) AS Idade,
		                Animais.PesoAtual, Animais.PesoIdeal, AnimaisAuxRacas.IdadeAdulta, 
                        AnimaisAuxRacas.CrescInicial, AnimaisAuxRacas.CrescFinal,
		                Animais.Observacoes, Animais.Ativo, Animais.RecalcularNEM,
                        Animais.IdOperador, Animais.IP, Animais.DataCadastro
                FROM	AnimaisAuxEspecies INNER JOIN
			                AnimaisAuxRacas ON AnimaisAuxEspecies.IdEspecie = 
				                AnimaisAuxRacas.IdEspecie RIGHT OUTER JOIN
                        Animais INNER JOIN Pessoas ON Animais.IdPessoa = 
			                Pessoas.IdPessoa ON AnimaisAuxRacas.IdRaca = 
				                Animais.IdRaca
                WHERE   (Animais.IdPessoa = {0}) AND (Animais.Ativo = 1)
                ORDER BY    Animais.IdPessoa, Animal", _idPessoa);

            var lista = crud.Listar<TOAnimaisBll>(_sql);

            return lista.ToList();
        }

        public List<TOAnimaisBll> Listar(string _pesqAnimal, int _idTutor,
            int _tamPag, int _pagAtual)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SET DATEFORMAT dmy                
                SET QUERY_GOVERNOR_COST_LIMIT 0

                DECLARE @PageNumber AS INT, @RowspPage AS INT
                SET @PageNumber = {1}
                SET @RowspPage = {0}                 

                SELECT  IdPessoa, Nome, IdAnimal, Animal, IdEspecie, Especie, IdRaca, 
                        Raca, IdSexo, Sexo, DtNascim, Idade, PesoAtual, PesoIdeal, 
		                IdadeAdulta, CrescInicial, CrescFinal, Observacoes, RecalcularNEM,
                        Ativo, IdOperador, IP, DataCadastro
                FROM    (
                            SELECT	ROW_NUMBER() OVER(ORDER BY Pessoas.Nome, Animais.Nome) AS NUMBER,
                                    Animais.IdPessoa, Pessoas.Nome, Animais.IdAnimal, 
		                            Animais.Nome AS Animal, AnimaisAuxRacas.IdEspecie, 
		                            AnimaisAuxEspecies.Especie, Animais.IdRaca, 
		                            AnimaisAuxRacas.Raca, Animais.Sexo As IdSexo,
		                            (Case Animais.Sexo 
			                            When 1 Then 'Macho'
			                            When 2 Then 'Fêmea'
		                             End) Sexo, Animais.DtNascim, 
		                            (DATEDIFF(YY, Animais.DtNascim, GETDATE()) - 
		                             CASE 
			                            WHEN RIGHT(CONVERT(VARCHAR(6), GETDATE(), 12), 4) >= 
				                             RIGHT(CONVERT(VARCHAR(6), Animais.DtNascim, 12), 4) 
			                            THEN 0 
			                            ELSE 1 
		                            END) AS Idade,
		                            Animais.PesoAtual, Animais.PesoIdeal, AnimaisAuxRacas.IdadeAdulta, 
                                    AnimaisAuxRacas.CrescInicial, AnimaisAuxRacas.CrescFinal,
		                            Animais.Observacoes, Animais.Ativo, Animais.RecalcularNEM,
                                    Animais.IdOperador, Animais.IP, 
		                            Animais.DataCadastro
                            FROM	AnimaisAuxEspecies INNER JOIN
			                            AnimaisAuxRacas ON AnimaisAuxEspecies.IdEspecie = 
				                            AnimaisAuxRacas.IdEspecie RIGHT OUTER JOIN
                                    Animais INNER JOIN Pessoas ON Animais.IdPessoa = 
			                            Pessoas.IdPessoa ON AnimaisAuxRacas.IdRaca = 
				                            Animais.IdRaca
                            WHERE   (Animais.Ativo = 1) ", _tamPag, _pagAtual);

            if (_idTutor > 0)
            {
                _sql += string.Format(@" AND 
                                    (Animais.IdPessoa In (
                                          Select	p.IdPessoa
							              From		Pessoas p
							              Where		(p.IdCliente = {0}))) ", _idTutor);
            }

            if (_pesqAnimal != "")
            {
                _sql += string.Format(@" AND 
                                    (Animais.Nome Like '%{0}%' COLLATE Latin1_General_CI_AI)",
                                    _pesqAnimal);
            }

            _sql += @") AS TBL
                WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
                ORDER BY    Nome, Animal";

            var lista = crud.Listar<TOAnimaisBll>(_sql);

            return lista.ToList();
        }

        public int TotalPaginas(string _pesqAnimal, int _idTutor, int _tamPag)
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT	Count(a.IdAnimal) Total
                FROM	Animais a
                WHERE   (a.Ativo = 1)";

            if (_idTutor > 0)
            {
                _sql += string.Format(@" AND 
                         (a.IdPessoa In (
                                Select	p.IdPessoa
							    From	Pessoas p
							    Where	(p.IdCliente = {0}))) ", _idTutor);
            }

            if (_pesqAnimal != "")
            {
                _sql += string.Format(@" AND 
                         (a.Nome Like '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqAnimal);
            }


            int _total = crud.ExecutarComandoTipoInteiro(_sql);

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
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
            else if (((_aniverEmMeses > 
                            Funcoes.Funcoes.ConvertePara.Int(_dadosPaciente.CrescInicial))) 
                     && 
                     ((_aniverEmMeses <= Funcoes.Funcoes.ConvertePara.Int(
                            _dadosPaciente.CrescFinal))))
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
            int _totalAnimaisPlano = 0, _totalAnimaisCadastrados = 0;
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
                string _totalAnimaisCadastradosSql = string.Format(@"
                SELECT	Count(Animais.IdAnimal) Total
				FROM	Animais INNER JOIN
				            Pessoas ON Animais.IdPessoa = Pessoas.IdPessoa
				Where Pessoas.IdCliente = {0}", _idOperador);
                _totalAnimaisCadastrados = crud.ExecutarComandoTipoInteiro(
                    _totalAnimaisCadastradosSql);

                string _planoVigenteSql = string.Format(@"
                SET DATEFORMAT dmy                
                SET QUERY_GOVERNOR_COST_LIMIT 0

                SELECT  avp.IdVigencia, planos.Plano, planos.QtdAnimais
                FROM    AcessosVigenciaPlanos AS avp INNER JOIN
                        AcessosVigenciaSituacao AS avs ON avp.IdVigencia = 
                            avs.IdVigencia INNER JOIN
                        PlanosAssinaturas AS planos ON avp.IdPlano = planos.IdPlano    
				Where	(avp.IdPessoa = {0}) AND 
						(avs.IdSituacao = 3) And 
						('{1}' Between avp.DtInicial And avp.DtFinal)",
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
                        _retorno.mensagem = "O Limite de Cadastro de Animais do Seu Plano foi Atingido!!!";
                    }
                }
                else
                {
                    _retorno.retorno = true;
                    _retorno.mensagem = "Plano Fora da Vigência! Contate o Administrador!";
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
