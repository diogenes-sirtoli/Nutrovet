using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Web.UI.WebControls;
using System.Collections;

namespace BLL
{
    public class clTela4Bll
    {
        public bllRetorno Inserir(int _tela, TOTela4Bll _objeto)
        {
            clCrudDal crud = new clCrudDal();
            bllRetorno ret = new bllRetorno();

            switch (_tela)
            {
                case 5:
                    {
                        Equipe equipeDcl = new Equipe();
                        clEquipesBll equipeBll = new clEquipesBll();

                        equipeDcl.IdDpto = _objeto.IdRef;
                        equipeDcl.Equipes = _objeto.Nome;
                        equipeDcl.IdOperador = _objeto.IdOperador;
                        equipeDcl.Ativo = _objeto.Ativo;
                        equipeDcl.IP = _objeto.IP;
                        equipeDcl.DataCadastro = _objeto.DataCadastro;

                        ret = equipeBll.Inserir(equipeDcl);

                        break;
                    }
                default:
                    {
                        ret = bllRetorno.GeraRetorno(false,
                            "Não foi possível efetuar a INCLUSÃO!!!");
                        break;
                    }
            }

            return ret;
        }

        public bllRetorno Alterar(int _tela, TOTela4Bll _objeto)
        {
            clCrudDal crud = new clCrudDal();
            bllRetorno ret = new bllRetorno(); ;

            switch (_tela)
            {
                case 5:
                    {
                        Equipe equipeDcl;
                        clEquipesBll equipeBll = new clEquipesBll();

                        equipeDcl = equipeBll.Carregar(_objeto.Id);

                        equipeDcl.IdDpto = _objeto.IdRef;
                        equipeDcl.Equipes = _objeto.Nome;
                        equipeDcl.IdOperador = _objeto.IdOperador;
                        equipeDcl.Ativo = _objeto.Ativo;
                        equipeDcl.IP = _objeto.IP;
                        equipeDcl.DataCadastro = _objeto.DataCadastro;

                        ret = equipeBll.Alterar(equipeDcl);

                        break;
                    }
                default:
                    {
                        ret = bllRetorno.GeraRetorno(false,
                            "Não foi possível efetuar a ALTERAÇÃO!!!");
                        break;
                    }
            }

            return ret;
        }

        public TOTela4Bll Carregar(int _tela, int _id)
        {
            TOTela4Bll retItem = new TOTela4Bll();
            clCrudDal crud = new clCrudDal();

            switch (_tela)
            {
                case 5:
                    {
                        Equipe equipeDcl = crud.Carregar<Equipe>(
                            _id.ToString());

                        retItem.IdRef = equipeDcl.IdDpto;
                        retItem.NomeRef = equipeDcl.Departamento.Departamento1;
                        retItem.Id = equipeDcl.IdEquipe;
                        retItem.Nome = equipeDcl.Equipes;
                        retItem.Sigla = "";
                        retItem.Ativo = equipeDcl.Ativo;
                        retItem.IdOperador = equipeDcl.IdOperador;
                        retItem.IP = equipeDcl.IP;
                        retItem.DataCadastro = equipeDcl.DataCadastro;

                        break;
                    }
            }

            return retItem;
        }

        public bllRetorno Excluir(int _tela, TOTela4Bll _objeto)
        {
            clCrudDal crud = new clCrudDal();
            bllRetorno ret = new bllRetorno(); ;

            switch (_tela)
            {
                case 5:
                    {
                        Equipe equipeDcl;
                        clEquipesBll equipeBll = new clEquipesBll();

                        equipeDcl = equipeBll.Carregar(_objeto.Id);

                        equipeDcl.Ativo = false;
                        equipeDcl.IdOperador = _objeto.IdOperador;
                        equipeDcl.DataCadastro = _objeto.DataCadastro;
                        equipeDcl.IP = _objeto.IP;

                        ret = equipeBll.Excluir(equipeDcl);
                        string mudar = ret.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
                        ret.mensagem = mudar;

                        break;
                    }
                default:
                    {
                        ret = bllRetorno.GeraRetorno(false,
                            "Não foi possível efetuar a EXCLUSÃO!!!");
                        break;
                    }
            }

            return ret;
        }

        public List<TOTela4Bll> Listar(int _tela, int? _idRefer)
        {
            List<TOTela4Bll> retLista = new List<TOTela4Bll>();
            clCrudDal crud = new clCrudDal();
            string _sql = "";

            switch (_tela)
            {
                case 5:
                    {
                        _sql = @"
                            SELECT  Eqp.IdDpto AS IdRef, Dpto.Departamento AS NomeRef, Eqp.IdEquipe AS Id, 
                                    Eqp.Equipes AS Nome, Eqp.Ativo, Eqp.IdOperador, Eqp.IP, Eqp.DataCadastro
                            FROM    Departamentos AS Dpto INNER JOIN
                                        Equipes AS Eqp ON Dpto.IdDpto = Eqp.IdDpto
                            WHERE     (Eqp.Ativo = 1) ";

                        if ((_idRefer != null) && (_idRefer > 0))
                        {
                            _sql += string.Format(
                                @" AND (Eqp.IdDpto = {0})
                            ORDER BY AcessoAuxFuncoesGrupos.Grupo, 
                                AcessoAuxFuncoes.Funcao", _idRefer);
                        }
                        else
                        {
                            _sql += @"ORDER BY NomeRef, Nome";
                        }

                        retLista = crud.Listar<TOTela4Bll>(_sql).ToList();

                        break;
                    }
            }

            return retLista;
        }

        public List<TOTela3Bll> ListarReferencia(int _tela)
        {
            List<TOTela3Bll> retLista = new List<TOTela3Bll>();
            clTela3Bll tela3Bll = new clTela3Bll();

            switch (_tela)
            {
                case 5:
                    {
                        retLista = tela3Bll.Listar("1.4"); //4 - dos Departamentos
                        break;
                    }
            }

            return retLista;
        }

        /// <summary>
        /// Retorna no Índice 0 - Título da Página,
        ///                   1 - Label da Referência,
        ///                   2 - Label da Descrição,
        ///                   3 - Label da Sigla,
        ///                   4 - Módulo,
        ///                   5 - Cód. da Tela
        /// </summary>
        /// <param name="_tela"></param>
        /// <returns></returns>
        public ListItemCollection PopulaTitulos(int _tela)
        {
            ListItemCollection retLista = new ListItemCollection();

            switch (_tela)
            {
                case 5:
                    {
                        retLista.Add(new ListItem("Titulo", "Cadastro de Equipes"));
                        retLista.Add(new ListItem("Referencia", "Departamentos"));
                        retLista.Add(new ListItem("Nome", "Equipes"));
                        retLista.Add(new ListItem("Sigla", "sem sigla"));
                        //retLista.Add(new ListItem("PermModulo", "1"));
                        retLista.Add(new ListItem("PermTela", "5"));

                        break;
                    }
            }

            return retLista;
        }
    }
}
