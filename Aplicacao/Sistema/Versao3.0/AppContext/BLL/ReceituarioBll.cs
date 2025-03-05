using System;
using System.Collections.Generic;
using System.Linq;
using DCL;
using DAL;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;

namespace BLL
{
    public class clReceituarioBll
    {
        private class IntervalosTotais
        {
            public int IdPrescr { get; set; }
            public int Total { get; set; }
        }

        public bllRetorno Inserir(Receituario _rec)
        {
            bllRetorno ret = _rec.ValidarRegras();

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_rec);

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

        public bllRetorno Alterar(Receituario _rec)
        {
            bllRetorno ret = _rec.ValidarRegras();

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_rec);

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

        public Receituario Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            Receituario _rec = null;

            var consulta = crud.ExecutarComando<Receituario>(string.Format(@"
                Select *
                From Receituario
                Where (IdReceita = {0})", _id));

            foreach (var item in consulta)
            {
                _rec = item;
            }

            return _rec;
        }

        public bllRetorno Excluir(Receituario _rec)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg;

            try
            {
                crud.Excluir(_rec);

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

        public List<TOReceituarioBll> Listar(int _idPessoa, int _idTutor, int _idTpRec,
            int _idAnimal, DateTime? _dtIni, DateTime? _dtFim, int _tamPag, int _pagAtual)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				EXECUTE ReceituarioListarSelecao
					@idPessoa	= {0},
	                @idTutor	= {1},
	                @idTpRec	= {2},
	                @idAnimal	= {3},
	                @dtIni		= '{4}',
	                @dtFim		= '{5}',
	                @RowspPage	= {6},
	                @PageNumber	= {7} ", _idPessoa, _idTutor, _idTpRec,
                _idAnimal, (_dtIni != null ? _dtIni.Value.ToString("dd/MM/yyyy") : ""),
                (_dtFim != null ? _dtFim.Value.ToString("dd/MM/yyyy") : ""), _tamPag,
                _pagAtual);

            var lista = crud.Listar<TOReceituarioBll>(_sql);

            return lista.ToList();
        }

        public int TotalPaginas(int _idPessoa, int _idTutor, int _idTpRec,
            int _idAnimal, DateTime? _dtIni, DateTime? _dtFim, int _tamPag)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				EXECUTE ReceituarioListarSelecaoTotalPaginas
					@idPessoa	= {0},
	                @idTutor	= {1},
	                @idTpRec	= {2},
	                @idAnimal	= {3},
	                @dtIni		= '{4}',
	                @dtFim		= '{5}'", _idPessoa, _idTutor, _idTpRec,
                _idAnimal, (_dtIni != null ? _dtIni.Value.ToString("dd/MM/yyyy") : ""),
                (_dtFim != null ? _dtFim.Value.ToString("dd/MM/yyyy") : ""));

            int _total = crud.ExecutarComandoTipoInteiro(_sql);

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }

        public ListItem[] ListarTipoReceita()
        {
            ListItem[] tabelas = Funcoes.Funcoes.GetEnumList<DominiosBll.ReceitasAuxTipos>();

            return tabelas;
        }

        public string GerarNumeroArquivo()
        {
            CrudDal crud = new CrudDal();
            string retorno = "";
            string _sql = @"
				Select distinct	(rec.NrReceita + 1) NrReceita
                From	Receituario rec
                Where	(rec.NrReceita = (Select MAX(Cast(r.NrReceita as int))
							                From Receituario r))";

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            if (reg <= 0)
            {
                reg = 1;
            }

            retorno = Funcoes.Funcoes.ConvertePara.String(reg);

            return retorno;
        }

        public bool ExisteArquivoReceita(string _fileReceita)
        {
            bool retorno = false, _severLocal = Conexao.ServidorLocal();
            string _path = (_severLocal ? "~/PrintFiles/Avaliacao/Receitas/" : 
                "~/PrintFiles/Producao/Receitas/");

            if ((_fileReceita != null) && (_fileReceita != ""))
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

            return retorno;
        }

        public BITotalIntervalosBll VerificaIntervalos(Repeater rptItem)
        {
            BITotalIntervalosBll _retorno = new BITotalIntervalosBll();

            foreach (RepeaterItem item in rptItem.Items)
            {
                CheckBox cbxIncldiet = (CheckBox)item.FindControl("cbxIncldiet");
                DropDownList ddlIntervalo = (DropDownList)item.FindControl("ddlIntervalo");

                switch (item.ItemType)
                {
                    case ListItemType.Item:
                    case ListItemType.AlternatingItem:
                        {
                            if ((cbxIncldiet.Checked) && (ddlIntervalo.SelectedValue != "0"))
                            {
                                int _valor = Funcoes.Funcoes.ConvertePara.Int(
                                    ddlIntervalo.SelectedValue);

                                switch (_valor)
                                {
                                    case 1:
                                        {
                                            _retorno.TotalBid += 1;

                                            break;
                                        }
                                    case 2:
                                        {
                                            _retorno.TotalTid += 1;

                                            break;
                                        }
                                    case 3:
                                        {
                                            _retorno.TotalSid += 1;

                                            break;
                                        }
                                }
                            }
                        }

                        break;
                }
            }

            return _retorno;
        }

        public BITotalIntervalosBll TotalIntervalos(int _idReceita)
        {
            BITotalIntervalosBll _retorno = new BITotalIntervalosBll();
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"Execute TotalIntervalos {0}", _idReceita);

            var lista = crud.Listar<IntervalosTotais>(_sql);

            foreach (IntervalosTotais item in lista)
            {
                switch (item.IdPrescr)
                {
                    case 1:
                        {
                            _retorno.TotalBid = item.Total;

                            break;
                        }
                    case 2:
                        {
                            _retorno.TotalTid = item.Total;

                            break;
                        }
                    case 3:
                        {
                            _retorno.TotalSid = item.Total;

                            break;
                        }
                }
            }

            return _retorno;
        }
    }
}
