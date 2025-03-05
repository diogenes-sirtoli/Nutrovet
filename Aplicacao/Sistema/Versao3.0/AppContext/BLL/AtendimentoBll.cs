using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DAL;
using DCL;

namespace BLL
{
    public class clAtendimentoBll
    {
        public bllRetorno Inserir(Atendimento _atend)
        {
            bllRetorno ret = _atend.ValidarRegras(true);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_atend);

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

        public bllRetorno Alterar(Atendimento _atend)
        {
            bllRetorno ret = _atend.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_atend);

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

        public bllRetorno Excluir(Atendimento _atend)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_atend);

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

        public Atendimento Carregar(int _idAtend)
        {
            CrudDal crud = new CrudDal();

            Atendimento atend;

            atend = crud.ExecutarComando<Atendimento>(
                string.Format(@"
				Select  IdPessoa, IdAnimal, IdAtend, IdTpAtend, Descricao, 
                        DtHrAtend, Atendimento, Ativo, IdOperador, IP, 
                        DataCadastro, Versao
				From    Atendimento
				Where   (IdAtend = {0})", _idAtend)).FirstOrDefault();

            return atend;
        }

        public TOAtendimentoBll CarregarTO(int _idAtend)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				Set DateFormat dmy

                SELECT	atend.IdPessoa AS IdCliente, clientes.Nome AS cNome, 
		                clientes.Email AS cEMail, Tutores.IdTutor, 
		                tutor.Nome AS tNome, tutor.Email AS tEMail, atend.IdAnimal, 
		                anim.Nome AS Paciente, R.IdEspecie, E.Especie, R.IdRaca, R.Raca,
                        atend.IdAtend, atend.IdTpAtend, tp.TipoAtendimento, 
		                atend.Descricao, atend.DtHrAtend, 
		                FORMAT(CAST(atend.DtHrAtend AS date), N'dd/MM/yyyy') AS DtAtend, 
                        FORMAT(CAST(atend.DtHrAtend AS time), N'hh\:mm') AS HrAtend, 
                        atend.Atendimento, atend.Ativo, atend.IdOperador, atend.IP, 
                        atend.DataCadastro, atend.Versao 
                FROM	Tutores INNER JOIN
			                Pessoas AS tutor ON Tutores.IdTutor = 
                                tutor.IdPessoa INNER JOIN
                            Animais AS anim ON Tutores.IdTutores = 
                                anim.IdTutores INNER JOIN
                            Atendimento AS atend INNER JOIN
                            Pessoas AS clientes ON atend.IdPessoa = 
				                clientes.IdPessoa ON anim.IdAnimal = 
					                atend.IdAnimal INNER JOIN
                            AtendimentoAuxTipos AS tp ON atend.IdTpAtend = 
				                tp.IdTpAtend LEFT OUTER JOIN
                            AnimaisAuxRacas AS R ON anim.IdRaca = R.IdRaca LEFT OUTER JOIN
                            AnimaisAuxEspecies AS E ON R.IdEspecie = E.IdEspecie
                WHERE	(atend.IdAtend = {0})", _idAtend);

            TOAtendimentoBll atend =
                crud.ExecutarComando<TOAtendimentoBll>(_sql).FirstOrDefault();

            return atend;
        }

        public List<TOAtendimentoBll> ListarTO(int _idAnimal)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				Set DateFormat dmy

                SELECT	atend.IdPessoa AS IdCliente, clientes.Nome AS cNome, 
		                clientes.Email AS cEMail, Tutores.IdTutor, 
		                tutor.Nome AS tNome, tutor.Email AS tEMail, atend.IdAnimal, 
		                anim.Nome AS Paciente, R.IdEspecie, E.Especie, R.IdRaca, R.Raca,
                        atend.IdAtend, atend.IdTpAtend, tp.TipoAtendimento, 
		                atend.Descricao, atend.DtHrAtend, 
		                FORMAT(CAST(atend.DtHrAtend AS date), N'dd/MM/yyyy') AS DtAtend, 
                        FORMAT(CAST(atend.DtHrAtend AS time), N'hh\:mm') AS HrAtend, 
                        atend.Atendimento, atend.Ativo, atend.IdOperador, atend.IP, 
                        atend.DataCadastro, atend.Versao 
                FROM	Tutores INNER JOIN
			                Pessoas AS tutor ON Tutores.IdTutor = 
                                tutor.IdPessoa INNER JOIN
                            Animais AS anim ON Tutores.IdTutores = 
                                anim.IdTutores INNER JOIN
                            Atendimento AS atend INNER JOIN
                            Pessoas AS clientes ON atend.IdPessoa = 
				                clientes.IdPessoa ON anim.IdAnimal = 
					                atend.IdAnimal INNER JOIN
                            AtendimentoAuxTipos AS tp ON atend.IdTpAtend = 
				                tp.IdTpAtend LEFT OUTER JOIN
                            AnimaisAuxRacas AS R ON anim.IdRaca = R.IdRaca LEFT OUTER JOIN
                            AnimaisAuxEspecies AS E ON R.IdEspecie = E.IdEspecie
                WHERE	(atend.IdAnimal = {0})", _idAnimal);

            var lista = crud.Listar<TOAtendimentoBll>(_sql);

            return lista.ToList();
        }

        public List<TOLinhaTempoBll> ListarLinhaTempo(int _idCliente, int _idAnimal)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				Set DateFormat dmy

                Execute LinhaTempoListar {0}, {1}", _idCliente, _idAnimal);

            var lista = crud.Listar<TOLinhaTempoBll>(_sql);

            return lista.ToList();
        }

        public List<TOLinhaTempoBll> ListarLinhaTempoPorAno(
            List<TOLinhaTempoBll> _lista, int _ano)
        {
            List<TOLinhaTempoBll> _listaRet = (from l in _lista
                                               where l.Data.Value.Year == _ano
                                               select l).ToList();

            return _listaRet;
        }

        public List<int> ListarAnosLinhaTempo(List<TOLinhaTempoBll> _lista)
        {
            List<int> _anos = new List<int>();

            if ((_lista != null) && (_lista.Count() > 0))
            {
                _anos = (from l in _lista
                         select l.Data.Value.Year).
                         Distinct().ToList();
            }

            return _anos.ToList();
        }

        public bool ExisteArquivoReceita(string _fileReceita)
        {
            bool retorno = false, _severLocal = Conexao.ServidorLocal();
            string _path = (_severLocal ? "~/PrintFiles/Avaliacao/Atendimentos/" :
                "~/PrintFiles/Producao/Atendimentos/");

            if ((_fileReceita != null) && (_fileReceita != ""))
            {
                if (_fileReceita == "...")
                {
                    retorno = true;
                }
                else
                {
                    HttpServerUtility _server = HttpContext.Current.Server;
                    string _partialPath = _server.MapPath(_path + _fileReceita);
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
            }

            return retorno;
        }
    }
}
