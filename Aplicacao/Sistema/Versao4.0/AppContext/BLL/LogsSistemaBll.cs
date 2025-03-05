using DAL;
using DCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace BLL
{
    public class clLogsSistemaBll
    {
        public bllRetorno Inserir(LogsSistema _log)
        {
            bllRetorno _ret = _log.ValidarRegras();
            CrudsDal<LogsSistema> crud = new CrudsDal<LogsSistema>();

            try
            {
                if (_ret.Retorno)
                {
                    if (_ret.Mensagem != "Registro Reativado")
                    {
                        crud.Inserir(_log);
                        crud.Dispose();
                    }

                    return bllRetorno.GeraRetorno(true,
                        "INSERÇÃO efetuada com sucesso!!!");
                }
                else
                {
                    return _ret;
                }
            }
            catch (Exception msg)
            {
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a INSERÇÃO!!!");
            }
        }

        public bllRetorno Alterar(LogsSistema _log)
        {
            bllRetorno ret = _log.ValidarRegras();

            if (ret.Retorno)
            {
                CrudsDal<LogsSistema> crud = new CrudsDal<LogsSistema>();

                try
                {
                    crud.Alterar(_log);
                    crud.Dispose();

                    return bllRetorno.GeraRetorno(true,
                        "ALTERAÇÃO efetuada com sucesso!!!");
                }
                catch (Exception msg)
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

        public LogsSistema Carregar(int _id)
        {
            CrudsDal<LogsSistema> crud = new CrudsDal<LogsSistema>();
            LogsSistema _log = crud.Carregar(_id);
            crud.Dispose();

            return _log;
        }

        public bllRetorno Excluir(int _idLog)
        {
            CrudsDal<LogsSistema> crud = new CrudsDal<LogsSistema>();
            bllRetorno msg;

            try
            {
                crud.Excluir(e => e.IdLog == _idLog);
                crud.Dispose();

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

        public List<TOLogsSistemaBll> Listar()
        {
            CrudsDal<TOLogsSistemaBll> crud = new CrudsDal<TOLogsSistemaBll>();

            string _sql = @"
                Select	l.IdPessoa, l.IdLog, l.IdTabela,
		                (Case l.IdTabela
			                When 1 Then 'Acessos'			
                            When 2 Then 'AcessosAuxFuncoes'
                            When 3 Then 'AcessosAuxTelas'
                            When 4 Then 'AcessosFuncoesTelas'
                            When 5 Then 'AcessosVigenciaCupomDesconto'
                            When 6 Then 'AcessosVigenciaPlanos'
                            When 7 Then 'AcessosVigenciaSituacao'
                            When 8 Then 'AlimentoNutrientes'
                            When 9 Then 'Alimentos'
                            When 10 Then 'AlimentosAuxCategorias'
                            When 11 Then 'AlimentosAuxFontes'
                            When 12 Then 'AlimentosAuxGrupos'
                            When 13 Then 'Animais'
                            When 14 Then 'AnimaisAuxEspecies'
                            When 15 Then 'AnimaisAuxRacas'
                            When 16 Then 'AnimaisPesoHistorico'
                            When 17 Then 'Biblioteca'
                            When 18 Then 'BibliotecaAuxSecoes'
                            When 19 Then 'Cardapio'
                            When 20 Then 'CardapiosAlimentos'
                            When 21 Then 'ConfigReceituario'
                            When 22 Then 'Dietas'
                            When 23 Then 'DietasAlimentos'
                            When 24 Then 'ExigenciasNutrAuxIndicacoes'
                            When 25 Then 'ExigenciasNutricionais'
                            When 26 Then 'Nutraceuticos'
                            When 27 Then 'Nutrientes'
                            When 28 Then 'NutrientesAuxGrupos'
                            When 29 Then 'Pessoas'
                            When 30 Then 'PessoasCartaoCredito'
                            When 31 Then 'PessoasDocumentos'
                            When 32 Then 'PlanosAssinaturas'
                            When 33 Then 'PortalContato'
                            When 34 Then 'PrescricaoAuxTipos'
                            When 35 Then 'Tutores'
		                 End) Tabela, l.IdAcao,
		                 (Case l.IdAcao
			                When 1 Then 'Inserir'			
                            When 2 Then 'Alterar'
                            When 3 Then 'Excluir'
                            When 4 Then 'Carregar'
                            When 5 Then 'Consultar'
                            When 6 Then 'Listar'
                            When 7 Then 'Gerar'
                            When 8 Then 'Gerar Relatorio'
                            When 9 Then 'Efetuar Logon'
                            When 10 Then 'Assinatura de Planos'
		                 End) Acao, l.Mensagem, l.Justificativa, l.Datahora
                FROM	LogsSistema AS l INNER JOIN
						    Pessoas AS p ON l.IdPessoa = p.IdPessoa
                ORDER BY    Tabela";

            var lista = crud.Listar(_sql);
            crud.Dispose();

            return lista.ToList();
        }

        public List<TOLogsSistemaBll> Listar(int? _pesqTabela, int? _pesqAcao,
            DateTime? _dtIni, DateTime? _dtFim, int _tamPag, int _pagAtual)
        {
            CrudsDal<TOLogsSistemaBll> crud = new CrudsDal<TOLogsSistemaBll>();

            string _sql = string.Format(@"
				EXECUTE LogsListarSelecao
						@pesquisaTabela =  {0}, 
                        @pesquisaAcao   =  {1}, 
						@dataInicial    = '{2}',
						@dataFinal      = '{3}', 
						@RowspPage      =  {4},
						@PageNumber     =  {5} ",
                _pesqTabela, _pesqAcao,
                (_dtIni != null ? _dtIni.Value.ToString("dd/MM/yyyy") : ""),
                (_dtFim != null ? _dtFim.Value.ToString("dd/MM/yyyy") : ""),
                _tamPag, _pagAtual);

            var lista = crud.Listar(_sql);
            crud.Dispose();

            return lista.ToList();
        }

        public int TotalPaginas(int? _pesqTabela, int? _pesqAcao, DateTime? _dtIni,
            DateTime? _dtFim, int _tamPag)
        {
            CrudsDal<TOLogsSistemaBll> crud = new CrudsDal<TOLogsSistemaBll>();

            string _sql = string.Format(@"
				EXECUTE LogsListarSelecaoTotalPaginas
						@pesquisaTabela =  {0}, 
                        @pesquisaAcao   =  {1}, 
						@dataInicial    = '{2}',
						@dataFinal      = '{3}' ",
                _pesqTabela, _pesqAcao,
                (_dtIni != null ? _dtIni.Value.ToString("dd/MM/yyyy") : ""),
                (_dtFim != null ? _dtFim.Value.ToString("dd/MM/yyyy") : ""));

            int _total = crud.ExecutarComandoTipoInteiro(_sql);
            crud.Dispose();

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }

        public ListItem[] ListarTabelas()
        {
            ListItem[] tabelas = Funcoes.Funcoes.GetEnumList<DominiosBll.LogTabelas>();

            return tabelas;
        }

        public ListItem[] ListarAcoes()
        {
            ListItem[] acoes = Funcoes.Funcoes.GetEnumList<DominiosBll.AcoesCrud>();

            return acoes;
        }
    }
}
