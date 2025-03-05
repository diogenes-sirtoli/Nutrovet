using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Web.UI.WebControls;

namespace BLL
{
    public class clAcessosVigenciaPlanosBll
    {
        public bllRetorno Inserir(AcessosVigenciaPlano _vigPlan)
        {
            bllRetorno ret = _vigPlan.ValidarRegras(true);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_vigPlan);

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

        public bllRetorno Alterar(AcessosVigenciaPlano _vigPlan)
        {
            bllRetorno ret = _vigPlan.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_vigPlan);

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

        public bllRetorno Excluir(AcessosVigenciaPlano _vigPlan)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_vigPlan);

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                return bllRetorno.GeraRetorno(false, 
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

        public AcessosVigenciaPlano Carregar(int _idVigencia)
        {
            CrudDal crud = new CrudDal();

            AcessosVigenciaPlano _usuerFunc = null;

            var logon = crud.ExecutarComando<AcessosVigenciaPlano>(string.Format(@"
                Select *
                From AcessosVigenciaPlanos
                Where (IdVigencia = {0})", _idVigencia));

            foreach (var item in logon)
            {
                _usuerFunc = item;
            }

            return _usuerFunc;
        }

        public TOAcessosVigenciaPlanosBll CarregarTO(int _idVigencia)
        {
            CrudDal crud = new CrudDal();

            TOAcessosVigenciaPlanosBll _usuerFunc = null;
            string _sql = string.Format(@"
                SELECT  avp.IdPessoa, p.Nome, avp.IdVigencia, avp.IdPlano, pa.Plano, 
                        avp.DtInicial, avp.DtFinal, avp.ComprovanteAnexou, avp.QtdAnim, 
                        avp.Periodo, 
                        (CASE avp.Periodo 
                            WHEN 1 THEN 'Mensal' 
                            WHEN 2 THEN 'Anual' 
                            END) AS PeriodoDescr, avp.Receituario, avp.Prontuario, 
                        avp.ComprovantePath, avp.ComprovanteHomologado, 
                        avp.ComprovanteDtHomolog, avp.ComprovanteHomologador, 
                        ph.Nome AS ComprovanteHomologadorNome, 
		                (SELECT	(Case s.IdSituacao
					                When 1 Then 'Cadastrado'
					                When 2 Then 'Pago'
					                When 3 Then 'Permitido'
					                When 4 Then 'Cancelado'
					                When 5 Then 'Alterado'
                                    When 6 Then 'Renovado'
		                        End)
                        FROM	AcessosVigenciaSituacao s
                        WHERE	(s.IdVigencia = avp.IdVigencia) And
				                (s.IdVigSit = (Select Max(sm.IdVigSit)
								                From AcessosVigenciaSituacao sm
								                Where (sm.IdVigencia = avp.IdVigencia)))) Situacao,
		                (Select	cupom.NrCumpom
		                    From	AcessosVigenciaCupomDesconto cupom
		                    Where	(cupom.IdCupom = avp.IdCupom)) NrCupom,
                        avp.Ativo, avp.IdOperador, avp.IP, avp.DataCadastro
                FROM    AcessosVigenciaPlanos AS avp INNER JOIN
                        Pessoas AS p ON avp.IdPessoa = p.IdPessoa INNER JOIN
                        PlanosAssinaturas AS pa ON avp.IdPlano = 
                            pa.IdPlano LEFT OUTER JOIN
                        Pessoas AS ph ON avp.ComprovanteHomologador = ph.IdPessoa
                WHERE   (avp.IdVigencia = {0})", _idVigencia);


            var logon = crud.ExecutarComando<TOAcessosVigenciaPlanosBll>(_sql);

            foreach (var item in logon)
            {
                _usuerFunc = item;
            }

            return _usuerFunc;
        }

        public AcessosVigenciaPlano CarregarPlanoNovo(int _idPessoa)
        {
            CrudDal crud = new CrudDal();

            AcessosVigenciaPlano _usuerFunc = null;

            string _sql = string.Format(@"
                SELECT  DISTINCT    p.IdPessoa, p.IdVigencia, p.IdPlano, p.DtInicial, p.DtFinal, 
                        p.IdCupom, p.QtdAnim, p.Periodo, p.Receituario, p.Prontuario, p.ComprovanteAnexou, 
                        p.ComprovantePath, p.ComprovanteHomologado, p.ComprovanteDtHomolog, 
                        p.ComprovanteHomologador, p.Ativo, p.IdOperador, p.IP, p.DataCadastro, p.Versao
                FROM    AcessosVigenciaPlanos AS p INNER JOIN
                            AcessosVigenciaSituacao AS s ON p.IdVigencia = s.IdVigencia
                WHERE   (p.IdPessoa = {0}) AND (s.IdSituacao IN (1, 2, 3))", _idPessoa);

            var logon = crud.ExecutarComando<AcessosVigenciaPlano>(_sql);

            foreach (var item in logon)
            {
                _usuerFunc = item;
            }

            return _usuerFunc;
        }

        public List<AcessosVigenciaPlano> Listar(bool _anexou, bool _homologou)
        {
            CrudDal crud = new CrudDal();

            return crud.Listar<AcessosVigenciaPlano>().
                        Where(l => l.ComprovanteAnexou == _anexou && 
                                   l.ComprovanteHomologado == _homologou).ToList();
        }

        public List<TOAcessosVigenciaPlanosBll> ListarTO(int _idPessoa)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SELECT  avp.IdPessoa, p.Nome, avp.IdVigencia, avp.IdPlano, pa.Plano, 
                        avp.DtInicial, avp.DtFinal, avp.ComprovanteAnexou, avp.QtdAnim, 
                        avp.Periodo, 
                        (CASE avp.Periodo 
                            WHEN 1 THEN 'Mensal' 
                            WHEN 2 THEN 'Anual' 
                            END) AS PeriodoDescr, avp.Receituario, avp.Prontuario, 
                        avp.ComprovantePath, avp.ComprovanteHomologado, 
                        avp.ComprovanteDtHomolog, avp.ComprovanteHomologador, 
                        ph.Nome AS ComprovanteHomologadorNome, 
		                (SELECT	(Case s.IdSituacao
					                When 1 Then 'Cadastrado'
					                When 2 Then 'Pago'
					                When 3 Then 'Permitido'
					                When 4 Then 'Cancelado'
					                When 5 Then 'Alterado'
                                    When 6 Then 'Renovado'
		                        End)
                        FROM	AcessosVigenciaSituacao s
                        WHERE	(s.IdVigencia = avp.IdVigencia) And
				                (s.IdVigSit = (Select Max(sm.IdVigSit)
								                From AcessosVigenciaSituacao sm
								                Where (sm.IdVigencia = avp.IdVigencia)))) Situacao,
		                (Select	cupom.NrCumpom
		                 From	AcessosVigenciaCupomDesconto cupom
		                 Where	(cupom.IdCupom = avp.IdCupom)) NrCupom,
                        avp.Ativo, avp.IdOperador, avp.IP, avp.DataCadastro
                FROM    AcessosVigenciaPlanos AS avp INNER JOIN
                        Pessoas AS p ON avp.IdPessoa = p.IdPessoa INNER JOIN
                        PlanosAssinaturas AS pa ON avp.IdPlano = 
                            pa.IdPlano LEFT OUTER JOIN
                        Pessoas AS ph ON avp.ComprovanteHomologador = ph.IdPessoa
                WHERE   (avp.IdPessoa = {0})", _idPessoa);

            var lista = crud.Listar<TOAcessosVigenciaPlanosBll>(_sql);

            return lista.ToList();
        }

        public ListItem[] ListarPeriodo()
        {
            ListItem[] periodo = Funcoes.Funcoes.GetEnumList<DominiosBll.Periodo>();

            return periodo;
        }

        public int QtdAnimais(TOPlanosBll planosTO)
        {
            int _qtdAnim = 0;

            switch (planosTO.Plano)
            {
                case "BM":
                case "BA":
                case "BMdp05":
                case "BMdp10":
                case "BMdm1":
                case "BAdp05":
                case "BAdp10":
                case "BAda12":
                    {
                        _qtdAnim = 10;

                        break;
                    }
                case "IM":
                case "IA":
                case "IMdp05":
                case "IMdp10":
                case "IMdm1":
                case "IAdp05":
                case "IAdp10":
                case "IAda12":
                    {
                        _qtdAnim = 20;

                        break;
                    }
                case "CM":
                case "CA":
                case "CMdp05":
                case "CMdp10":
                case "CMdm1":
                case "CAdp05":
                case "CAdp10":
                case "CAda12":
                    {
                        _qtdAnim = 7000000;

                        break;
                    }
            }

            return _qtdAnim;
        }

        public int IdPlano(TOPlanosBll planosTO)
        {
            int _idPlano = 0;

            switch (planosTO.Plano)
            {
                case "BM":
                case "BA":
                case "BMdp05":
                case "BMdp10":
                case "BMdm1":
                case "BAdp05":
                case "BAdp10":
                case "BAda12":
                    {
                        _idPlano = 1;

                        break;
                    }
                case "IM":
                case "IA":
                case "IMdp05":
                case "IMdp10":
                case "IMdm1":
                case "IAdp05":
                case "IAdp10":
                case "IAda12":
                    {
                        _idPlano = 2;

                        break;
                    }
                case "CM":
                case "CA":
                case "CMdp05":
                case "CMdp10":
                case "CMdm1":
                case "CAdp05":
                case "CAdp10":
                case "CAda12":
                    {
                        _idPlano = 3;

                        break;
                    }
            }

            return _idPlano;
        }

        public int IdPeriodo(TOPlanosBll planosTO)
        {
            int _idPeriodo = 0;

            switch (planosTO.AnualMensal)
            {
                case 'M':
                    {
                        _idPeriodo = (int)DominiosBll.Periodo.Mensal;

                        break;
                    }
                case 'A':
                    {
                        _idPeriodo = (int)DominiosBll.Periodo.Anual; ;

                        break;
                    }
            }

            return _idPeriodo;
        }

        public bool PlanoEstaNaVigencia(int _idPessoa)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComandoTipoInteiro(string.Format(@"
                SET DATEFORMAT dmy

                SELECT	Count(IdVigencia) Vigencia
                FROM	AcessosVigenciaPlanos
                WHERE	(IdPessoa = {0}) AND 
		                (GETDATE() BETWEEN DtInicial AND DtFinal) ", _idPessoa));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }
    }
}
