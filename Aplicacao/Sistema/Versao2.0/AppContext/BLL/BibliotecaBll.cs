using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web;

namespace BLL
{
    public class clBibliotecaBll
    {
        public bllRetorno Inserir(Biblioteca _biblio)
        {
            bllRetorno ret = _biblio.ValidarRegras(true);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    crud.Inserir(_biblio);

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
                var erro = err;
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a INSERÇÃO!!!");
            }
        }

        public bllRetorno Alterar(Biblioteca _biblio)
        {
            bllRetorno ret = _biblio.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_biblio);

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

        public Biblioteca Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            Biblioteca _biblio = null;

            var consulta = crud.ExecutarComando<Biblioteca>(string.Format(@"
                Select *
                From Biblioteca
                Where (IdBiblio = {0})", _id));

            foreach (var item in consulta)
            {
                _biblio = item;
            }

            return _biblio;
        }

        public bllRetorno Excluir(Biblioteca _biblio)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg = new bllRetorno();

            try
            {
                crud.Excluir(_biblio);

                msg = bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                var erro = err;
                msg = bllRetorno.GeraRetorno(true,
                    "Não foi Possível Efetuar a EXCLUSÃO!!!");
            }

            return msg;
        }

        public Funcoes.Funcoes.bllRetorno ExcluirArquivo(Biblioteca _biblio)
        {
            CrudDal crud = new CrudDal();
            Funcoes.Funcoes.bllRetorno _msg = new Funcoes.Funcoes.bllRetorno();

            HttpServerUtility _server;
            _server = HttpContext.Current.Server;

            string _path = _server.MapPath(_biblio.Caminho);

            _msg = Funcoes.Funcoes.Arquivos.DeletaArquivo(_path);

            return _msg;
        }

        public List<TOBibliotecaBll> Listar()
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT  b.IdBiblio, b.IdSecao, s.Secao, b.NomeArq, 
                        b.Descricao, b.Autor, b.Ano, b.Caminho, 
                        b.Ordenador, b.Ativo, b.IdOperador, b.IP, 
                        b.DataCadastro
                FROM    Biblioteca AS b LEFT OUTER JOIN
                            BibliotecaAuxSecoes AS s ON b.IdSecao = s.IdSecao
                ORDER BY b.IdSecao, b.Ano DESC";

            var lista = crud.Listar<TOBibliotecaBll>(_sql);

            return lista.ToList();
        }

        public List<TOBibliotecaBll> Listar(int _idSecao)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SELECT  b.IdBiblio, b.IdSecao, s.Secao, b.NomeArq, 
                        b.Descricao, b.Autor, b.Ano, b.Caminho, 
                        b.Ordenador, b.Ativo, b.IdOperador, b.IP, 
                        b.DataCadastro
                FROM    Biblioteca AS b LEFT OUTER JOIN
                            BibliotecaAuxSecoes AS s ON b.IdSecao = s.IdSecao
                WHERE   (b.IdSecao = {0})
                ORDER BY b.Ano DESC", _idSecao);

            var lista = crud.Listar<TOBibliotecaBll>(_sql);

            return lista.ToList();
        }

        public List<TOBibliotecaBll> Listar(int _secao, string _pesquisa)
        {
            CrudDal crud = new CrudDal();

            int _ano = Funcoes.Funcoes.ConvertePara.Int(_pesquisa);

            string _sql = string.Format(@"
                EXECUTE BibliotecaListar {0}, '{1}', {2}", _secao,
                (_ano <= 0 ? _pesquisa : ""), (_ano > 0 ? _ano : 0));

            var lista = crud.Listar<TOBibliotecaBll>(_sql);

            return lista.ToList();
        }

        public List<TOTela3Bll> ListarSecoes(List<TOBibliotecaBll> _pesquisa)
        {
            if (_pesquisa.Count() > 0)
            {
                List<TOTela3Bll> _listagemSecoes = new List<TOTela3Bll>();
                TOTela3Bll _itemTela3;

                foreach (TOBibliotecaBll item in _pesquisa)
                {
                    if (!_listagemSecoes.Exists(e => e.Id == item.IdSecao))
                    {
                        _itemTela3 = new TOTela3Bll();

                        _itemTela3.Id = Funcoes.Funcoes.ConvertePara.Int(item.IdSecao);
                        _itemTela3.Nome = item.Secao;
                        _itemTela3.Ativo = true;

                        _listagemSecoes.Add(_itemTela3);
                    }
                }

                return _listagemSecoes.ToList();
            }
            else
            {
                return null;
            }
        }

        public int Ordenador(int _idSecao)
        {
            CrudDal crud = new CrudDal();

            int result = crud.ExecutarComandoTipoInteiro(string.Format(@"
                Select	Coalesce(Max(b.ordenador), 0) Ordem
                From	Biblioteca b
                Where	(b.IdSecao = {0}) And (b.Ativo = 1)", _idSecao));

            return result + 1;
        }
    }
}
