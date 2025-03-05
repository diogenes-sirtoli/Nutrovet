using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Web.UI.WebControls;
using System.Collections;
using System.Security.Cryptography;

namespace BLL
{
    public class clTela3Bll
    {
        #region Inserir
        public bllRetorno Inserir(string _tela, TOTela3Bll _objeto)
        {
            CrudDal crud = new CrudDal();
            bllRetorno ret = new bllRetorno();

            switch (_tela)
            {
                case "1.1.1"://AcessosAuxFuncoes
                    {
                        AcessosAuxFuncoe funcaoDcl = new AcessosAuxFuncoe();
                        clAcessosAuxFuncoesBll funcaoBll = new clAcessosAuxFuncoesBll();

                        funcaoDcl.Funcao = _objeto.Nome;
                        funcaoDcl.IdOperador = _objeto.IdOperador;
                        funcaoDcl.Ativo = _objeto.Ativo;
                        funcaoDcl.IP = _objeto.IP;
                        funcaoDcl.DataCadastro = _objeto.DataCadastro;

                        ret = funcaoBll.Inserir(funcaoDcl);

                        break;
                    }
                case "1.1.2"://AcessosAuxTelas
                    {
                        AcessosAuxTela telaDcl = new AcessosAuxTela();
                        clAcessosAuxTelasBll telaBll = new clAcessosAuxTelasBll();

                        telaDcl.Telas = _objeto.Nome;
                        telaDcl.CodTela = _objeto.Sigla;
                        telaDcl.IdOperador = _objeto.IdOperador;
                        telaDcl.Ativo = _objeto.Ativo;
                        telaDcl.IP = _objeto.IP;
                        telaDcl.DataCadastro = _objeto.DataCadastro;

                        ret = telaBll.Inserir(telaDcl);

                        break;
                    }
                case "1.2.1"://PrescricaoAuxTipos
                    {
                        PrescricaoAuxTipo prescrDcl = new PrescricaoAuxTipo();
                        clPrescricaoAuxTiposBll prescrBll = new clPrescricaoAuxTiposBll();

                        prescrDcl.Prescricao = _objeto.Nome;
                        prescrDcl.IdOperador = _objeto.IdOperador;
                        prescrDcl.Ativo = _objeto.Ativo;
                        prescrDcl.IP = _objeto.IP;
                        prescrDcl.DataCadastro = _objeto.DataCadastro;

                        ret = prescrBll.Inserir(prescrDcl);

                        break;
                    }
                case "1.3.2"://AnimaisAuxEspecies
                    {
                        AnimaisAuxEspecy especieDcl = new AnimaisAuxEspecy();
                        clAnimaisAuxEspeciesBll especieBll = new clAnimaisAuxEspeciesBll();

                        especieDcl.Especie = _objeto.Nome;
                        especieDcl.IdOperador = _objeto.IdOperador;
                        especieDcl.Ativo = _objeto.Ativo;
                        especieDcl.IP = _objeto.IP;
                        especieDcl.DataCadastro = _objeto.DataCadastro;

                        ret = especieBll.Inserir(especieDcl);

                        break;
                    }
                case "1.4.2"://ExigenciasNutrAuxIndicacoes
                    {
                        ExigenciasNutrAuxIndicacoe indicDcl = new 
                            ExigenciasNutrAuxIndicacoe();
                        clExigenciasNutrAuxIndicacoesBll indicBll = new
                            clExigenciasNutrAuxIndicacoesBll();

                        indicDcl.Indicacao = _objeto.Nome;
                        indicDcl.IdOperador = _objeto.IdOperador;
                        indicDcl.Ativo = _objeto.Ativo;
                        indicDcl.IP = _objeto.IP;
                        indicDcl.DataCadastro = _objeto.DataCadastro;

                        ret = indicBll.Inserir(indicDcl);

                        break;
                    }
                case "1.4.5"://AlimentosAuxGrupos
                    {
                        AlimentosAuxGrupo alimGrupoDcl = new AlimentosAuxGrupo();
                        clAlimentosAuxGruposBll alimGrupoBll = new clAlimentosAuxGruposBll();

                        alimGrupoDcl.GrupoAlimento = _objeto.Nome;
                        alimGrupoDcl.IdOperador = _objeto.IdOperador;
                        alimGrupoDcl.Ativo = _objeto.Ativo;
                        alimGrupoDcl.IP = _objeto.IP;
                        alimGrupoDcl.DataCadastro = _objeto.DataCadastro;

                        ret = alimGrupoBll.Inserir(alimGrupoDcl);

                        break;
                    }
                case "1.4.6"://AlimentosAuxFontes
                    {
                        AlimentosAuxFonte alimFonteDcl = new AlimentosAuxFonte();
                        clAlimentosAuxFontesBll alimFontesBll = new clAlimentosAuxFontesBll();

                        alimFonteDcl.Fonte = _objeto.Nome;
                        alimFonteDcl.IdOperador = _objeto.IdOperador;
                        alimFonteDcl.Ativo = _objeto.Ativo;
                        alimFonteDcl.IP = _objeto.IP;
                        alimFonteDcl.DataCadastro = _objeto.DataCadastro;

                        ret = alimFontesBll.Inserir(alimFonteDcl);

                        break;
                    }
                case "1.4.7"://AlimentosAuxCategorias
                    {
                        AlimentosAuxCategorias alimCategoriaDcl = new AlimentosAuxCategorias();
                        clAlimentosAuxCategoriasBll alimCategoriaBll = new clAlimentosAuxCategoriasBll();

                        alimCategoriaDcl.Categoria = _objeto.Nome;
                        alimCategoriaDcl.IdOperador = _objeto.IdOperador;
                        alimCategoriaDcl.Ativo = _objeto.Ativo;
                        alimCategoriaDcl.IP = _objeto.IP;
                        alimCategoriaDcl.DataCadastro = _objeto.DataCadastro;

                        ret = alimCategoriaBll.Inserir(alimCategoriaDcl);

                        break;
                    }
                case "1.4.8"://AtendimentoAuxTipos
                    {
                        AtendimentoAuxTipo atendTpDcl = new AtendimentoAuxTipo();
                        clAtendimentoAuxTiposBll atendTpBll = new clAtendimentoAuxTiposBll();

                        atendTpDcl.TipoAtendimento = _objeto.Nome;
                        atendTpDcl.IdOperador = _objeto.IdOperador;
                        atendTpDcl.Ativo = _objeto.Ativo;
                        atendTpDcl.IP = _objeto.IP;
                        atendTpDcl.DataCadastro = _objeto.DataCadastro;

                        ret = atendTpBll.Inserir(atendTpDcl);

                        break;
                    }
                case "10.0.1"://BibliotecaAuxSecoes
                    {
                        BibliotecaAuxSecoe secoesDcl = new BibliotecaAuxSecoe();
                        clBibliotecaAuxSecoesBll secoesBll = new clBibliotecaAuxSecoesBll();

                        secoesDcl.Secao = _objeto.Nome;
                        secoesDcl.IdOperador = _objeto.IdOperador;
                        secoesDcl.Ativo = _objeto.Ativo;
                        secoesDcl.IP = _objeto.IP;
                        secoesDcl.DataCadastro = _objeto.DataCadastro;

                        ret = secoesBll.Inserir(secoesDcl);

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
        #endregion

        #region Alterar
        public bllRetorno Alterar(string _tela, TOTela3Bll _objeto)
        {
            CrudDal crud = new CrudDal();
            bllRetorno ret = new bllRetorno();

            switch (_tela)
            {
                case "1.1.1"://AcessosAuxFuncoes
                    {
                        clAcessosAuxFuncoesBll funcaoBll = new clAcessosAuxFuncoesBll();
                        AcessosAuxFuncoe funcaoDcl = funcaoBll.Carregar(_objeto.Id);

                        funcaoDcl.Funcao = _objeto.Nome;
                        funcaoDcl.IdOperador = _objeto.IdOperador;
                        funcaoDcl.Ativo = _objeto.Ativo;
                        funcaoDcl.IP = _objeto.IP;
                        funcaoDcl.DataCadastro = _objeto.DataCadastro;

                        ret = funcaoBll.Alterar(funcaoDcl);

                        break;
                    }
                case "1.1.2"://AcessosAuxTelas
                    {
                        clAcessosAuxTelasBll telaBll = new clAcessosAuxTelasBll();
                        AcessosAuxTela telaDcl = telaBll.Carregar(_objeto.Id);

                        telaDcl.Telas = _objeto.Nome;
                        telaDcl.CodTela = _objeto.Sigla;
                        telaDcl.IdOperador = _objeto.IdOperador;
                        telaDcl.Ativo = _objeto.Ativo;
                        telaDcl.IP = _objeto.IP;
                        telaDcl.DataCadastro = _objeto.DataCadastro;

                        ret = telaBll.Alterar(telaDcl);

                        break;
                    }
                case "1.2.1"://PrescricaoAuxTipos
                    {
                        clPrescricaoAuxTiposBll prescrBll = new clPrescricaoAuxTiposBll();
                        PrescricaoAuxTipo prescrDcl = prescrBll.Carregar(_objeto.Id);

                        prescrDcl.Prescricao = _objeto.Nome;
                        prescrDcl.IdOperador = _objeto.IdOperador;
                        prescrDcl.Ativo = _objeto.Ativo;
                        prescrDcl.IP = _objeto.IP;
                        prescrDcl.DataCadastro = _objeto.DataCadastro;

                        ret = prescrBll.Alterar(prescrDcl);

                        break;
                    }
                case "1.3.2"://AnimaisAuxEspecies
                    {
                        clAnimaisAuxEspeciesBll especieBll = new clAnimaisAuxEspeciesBll();
                        AnimaisAuxEspecy especieDcl = especieBll.Carregar(_objeto.Id);

                        especieDcl.Especie = _objeto.Nome;
                        especieDcl.Ativo = _objeto.Ativo;
                        especieDcl.IdOperador = _objeto.IdOperador;
                        especieDcl.DataCadastro = _objeto.DataCadastro;
                        especieDcl.IP = _objeto.IP;

                        ret = especieBll.Alterar(especieDcl);

                        break;
                    }
                case "1.4.2"://ExigenciasNutrAuxIndicacoes
                    {
                        clExigenciasNutrAuxIndicacoesBll indicBll = new
                            clExigenciasNutrAuxIndicacoesBll();
                        ExigenciasNutrAuxIndicacoe indicDcl = indicBll.Carregar(_objeto.Id);

                        indicDcl.Indicacao = _objeto.Nome;
                        indicDcl.IdOperador = _objeto.IdOperador;
                        indicDcl.Ativo = _objeto.Ativo;
                        indicDcl.IP = _objeto.IP;
                        indicDcl.DataCadastro = _objeto.DataCadastro;

                        ret = indicBll.Alterar(indicDcl);

                        break;
                    }
                case "1.4.5"://AlimentosAuxGrupos
                    {
                        clAlimentosAuxGruposBll alimGruposBll = new clAlimentosAuxGruposBll();
                        AlimentosAuxGrupo alimGruposDcl = alimGruposBll.Carregar(_objeto.Id);

                        alimGruposDcl.GrupoAlimento = _objeto.Nome;
                        alimGruposDcl.IdOperador = _objeto.IdOperador;
                        alimGruposDcl.Ativo = _objeto.Ativo;
                        alimGruposDcl.IP = _objeto.IP;
                        alimGruposDcl.DataCadastro = _objeto.DataCadastro;

                        ret = alimGruposBll.Alterar(alimGruposDcl);

                        break;
                    }
                case "1.4.6"://AlimentosAuxFontes
                    {
                        clAlimentosAuxFontesBll alimFontesBll = new clAlimentosAuxFontesBll();
                        AlimentosAuxFonte alimFontesDcl = alimFontesBll.Carregar(_objeto.Id);

                        alimFontesDcl.Fonte = _objeto.Nome;
                        alimFontesDcl.IdOperador = _objeto.IdOperador;
                        alimFontesDcl.Ativo = _objeto.Ativo;
                        alimFontesDcl.IP = _objeto.IP;
                        alimFontesDcl.DataCadastro = _objeto.DataCadastro;

                        ret = alimFontesBll.Alterar(alimFontesDcl);

                        break;
                    }
                case "1.4.7"://AlimentosAuxCategorias
                    {
                        clAlimentosAuxCategoriasBll alimCategoriasBll = new 
                            clAlimentosAuxCategoriasBll();
                        AlimentosAuxCategorias alimCategoriasDcl = alimCategoriasBll.Carregar(
                            _objeto.Id);

                        alimCategoriasDcl.Categoria = _objeto.Nome;
                        alimCategoriasDcl.IdOperador = _objeto.IdOperador;
                        alimCategoriasDcl.Ativo = _objeto.Ativo;
                        alimCategoriasDcl.IP = _objeto.IP;
                        alimCategoriasDcl.DataCadastro = _objeto.DataCadastro;

                        ret = alimCategoriasBll.Alterar(alimCategoriasDcl);

                        break;
                    }
                case "1.4.8"://AtendimentoAuxTipos
                    {
                        clAtendimentoAuxTiposBll atendTpBll = new clAtendimentoAuxTiposBll();
                        AtendimentoAuxTipo atendTpDcl = atendTpBll.Carregar(
                            _objeto.Id);

                        atendTpDcl.TipoAtendimento = _objeto.Nome;
                        atendTpDcl.IdOperador = _objeto.IdOperador;
                        atendTpDcl.Ativo = _objeto.Ativo;
                        atendTpDcl.IP = _objeto.IP;
                        atendTpDcl.DataCadastro = _objeto.DataCadastro;

                        ret = atendTpBll.Alterar(atendTpDcl);

                        break;
                    }
                case "10.0.1"://BibliotecaAuxSecoes
                    {
                        clBibliotecaAuxSecoesBll secoesBll = new clBibliotecaAuxSecoesBll();
                        BibliotecaAuxSecoe secoesDcl = secoesBll.Carregar(_objeto.Id);

                        secoesDcl.Secao = _objeto.Nome;
                        secoesDcl.IdOperador = _objeto.IdOperador;
                        secoesDcl.Ativo = _objeto.Ativo;
                        secoesDcl.IP = _objeto.IP;
                        secoesDcl.DataCadastro = _objeto.DataCadastro;

                        ret = secoesBll.Alterar(secoesDcl);

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
        #endregion

        #region Carregar
        public TOTela3Bll Carregar(string _tela, int _id)
        {
            TOTela3Bll retItem = new TOTela3Bll();
            CrudDal crud = new CrudDal();

            switch (_tela)
            {
                case "1.1.1"://AcessosAuxFuncoes
                    {
                        AcessosAuxFuncoe funcaoDcl = crud.Carregar<AcessosAuxFuncoe>(
                            _id.ToString());

                        retItem.Id = funcaoDcl.IdAcFunc;
                        retItem.Nome = funcaoDcl.Funcao;
                        retItem.IdOperador = funcaoDcl.IdOperador;
                        retItem.Ativo = funcaoDcl.Ativo;
                        retItem.IP = funcaoDcl.IP;
                        retItem.DataCadastro = funcaoDcl.DataCadastro;

                        break;
                    }
                case "1.1.2"://AcessosAuxTelas
                    {
                        AcessosAuxTela telaDcl = crud.Carregar<AcessosAuxTela>(
                            _id.ToString());

                        retItem.Id = telaDcl.IdTela;
                        retItem.Nome = telaDcl.Telas;
                        retItem.Sigla = telaDcl.CodTela;
                        retItem.IdOperador = telaDcl.IdOperador;
                        retItem.Ativo = telaDcl.Ativo;
                        retItem.IP = telaDcl.IP;
                        retItem.DataCadastro = telaDcl.DataCadastro;

                        break;
                    }
                case "1.2.1"://PrescricaoAuxTipos
                    {
                        PrescricaoAuxTipo telaDcl = crud.Carregar<PrescricaoAuxTipo>(
                            _id.ToString());

                        retItem.Id = telaDcl.IdPrescr;
                        retItem.Nome = telaDcl.Prescricao;
                        retItem.IdOperador = telaDcl.IdOperador;
                        retItem.Ativo = telaDcl.Ativo;
                        retItem.IP = telaDcl.IP;
                        retItem.DataCadastro = telaDcl.DataCadastro;

                        break;
                    }
                case "1.3.2"://AnimaisAuxEspecies
                    {
                        AnimaisAuxEspecy especieDcl = crud.Carregar<AnimaisAuxEspecy>(
                            _id.ToString());

                        retItem.Id = especieDcl.IdEspecie;
                        retItem.Nome = especieDcl.Especie;
                        retItem.Sigla = "";
                        retItem.Ativo = especieDcl.Ativo;
                        retItem.IdOperador = especieDcl.IdOperador;
                        retItem.IP = especieDcl.IP;
                        retItem.DataCadastro = especieDcl.DataCadastro;

                        break;
                    }
                case "1.4.2"://ExigenciasNutrAuxIndicacoes
                    {
                        ExigenciasNutrAuxIndicacoe indicDcl =
                            crud.Carregar<ExigenciasNutrAuxIndicacoe>(_id.ToString());

                        retItem.Id = indicDcl.IdIndic;
                        retItem.Nome = indicDcl.Indicacao;
                        retItem.Sigla = "";
                        retItem.Ativo = indicDcl.Ativo;
                        retItem.IdOperador = indicDcl.IdOperador;
                        retItem.IP = indicDcl.IP;
                        retItem.DataCadastro = indicDcl.DataCadastro;

                        break;
                    }
                case "1.4.5"://AlimentosAuxGrupos
                    {
                        AlimentosAuxGrupo alimGruposDcl = crud.Carregar<AlimentosAuxGrupo>(
                            _id.ToString());

                        retItem.Id = alimGruposDcl.IdGrupo;
                        retItem.Nome = alimGruposDcl.GrupoAlimento;
                        retItem.Sigla = "";
                        retItem.Ativo = alimGruposDcl.Ativo;
                        retItem.IdOperador = alimGruposDcl.IdOperador;
                        retItem.IP = alimGruposDcl.IP;
                        retItem.DataCadastro = alimGruposDcl.DataCadastro;

                        break;
                    }
                case "1.4.6"://AlimentosAuxFontes
                    {
                        AlimentosAuxFonte alimFontesDcl = crud.Carregar<AlimentosAuxFonte>(
                            _id.ToString());

                        retItem.Id = alimFontesDcl.IdFonte;
                        retItem.Nome = alimFontesDcl.Fonte;
                        retItem.Sigla = "";
                        retItem.Ativo = alimFontesDcl.Ativo;
                        retItem.IdOperador = alimFontesDcl.IdOperador;
                        retItem.IP = alimFontesDcl.IP;
                        retItem.DataCadastro = alimFontesDcl.DataCadastro;

                        break;
                    }
                case "1.4.7"://AlimentosAuxCategorias
                    {
                        AlimentosAuxCategorias alimCategoriasDcl = 
                            crud.Carregar<AlimentosAuxCategorias>(_id.ToString());

                        retItem.Id = alimCategoriasDcl.IdCateg;
                        retItem.Nome = alimCategoriasDcl.Categoria;
                        retItem.Sigla = "";
                        retItem.Ativo = alimCategoriasDcl.Ativo;
                        retItem.IdOperador = alimCategoriasDcl.IdOperador;
                        retItem.IP = alimCategoriasDcl.IP;
                        retItem.DataCadastro = alimCategoriasDcl.DataCadastro;

                        break;
                    }
                case "1.4.8"://AtendimentoAuxTipos
                    {
                        AtendimentoAuxTipo atendTpDcl = 
                            crud.Carregar<AtendimentoAuxTipo>(_id.ToString());

                        retItem.Id = atendTpDcl.IdTpAtend;
                        retItem.Nome = atendTpDcl.TipoAtendimento;
                        retItem.Sigla = "";
                        retItem.Ativo = atendTpDcl.Ativo;
                        retItem.IdOperador = atendTpDcl.IdOperador;
                        retItem.IP = atendTpDcl.IP;
                        retItem.DataCadastro = atendTpDcl.DataCadastro;

                        break;
                    }
                case "10.0.1"://BibliotecaAuxSecoes
                    {
                        BibliotecaAuxSecoe secoesDcl = 
                            crud.Carregar<BibliotecaAuxSecoe>(_id.ToString());

                        retItem.Id = secoesDcl.IdSecao;
                        retItem.Nome = secoesDcl.Secao;
                        retItem.IdOperador = secoesDcl.IdOperador;
                        retItem.Ativo = secoesDcl.Ativo;
                        retItem.IP = secoesDcl.IP;
                        retItem.DataCadastro = secoesDcl.DataCadastro;

                        break;
                    }
            }

            return retItem;
        }
        #endregion

        #region Excluir
        public bllRetorno Excluir(string _tela, TOTela3Bll _objeto)
        {
            CrudDal crud = new CrudDal();
            bllRetorno ret = new bllRetorno();

            switch (_tela)
            {
                case "1.1.1"://AcessosAuxFuncoes
                    {
                        AcessosAuxFuncoe funcaoDcl;
                        clAcessosAuxFuncoesBll funcaoBll = new clAcessosAuxFuncoesBll();

                        funcaoDcl = funcaoBll.Carregar(_objeto.Id);

                        funcaoDcl.IdOperador = _objeto.IdOperador;
                        funcaoDcl.Ativo = false;
                        funcaoDcl.IP = _objeto.IP;
                        funcaoDcl.DataCadastro = _objeto.DataCadastro;

                        ret = funcaoBll.Excluir(funcaoDcl);
                        string mudar = ret.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
                        ret.mensagem = mudar;

                        break;
                    }
                case "1.1.2"://AcessosAuxTelas
                    {
                        AcessosAuxTela telaDcl;
                        clAcessosAuxTelasBll telaBll = new clAcessosAuxTelasBll();

                        telaDcl = telaBll.Carregar(_objeto.Id);

                        telaDcl.IdOperador = _objeto.IdOperador;
                        telaDcl.Ativo = false;
                        telaDcl.IP = _objeto.IP;
                        telaDcl.DataCadastro = _objeto.DataCadastro;

                        ret = telaBll.Excluir(telaDcl);
                        string mudar = ret.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
                        ret.mensagem = mudar;

                        break;
                    }
                case "1.2.1"://PrescricaoAuxTipos
                    {
                        clPrescricaoAuxTiposBll prescrBll = new clPrescricaoAuxTiposBll();
                        PrescricaoAuxTipo prescrDcl;

                        prescrDcl = prescrBll.Carregar(_objeto.Id);

                        prescrDcl.IdOperador = _objeto.IdOperador;
                        prescrDcl.Ativo = _objeto.Ativo;
                        prescrDcl.IP = _objeto.IP;
                        prescrDcl.DataCadastro = _objeto.DataCadastro;

                        ret = prescrBll.Excluir(prescrDcl);
                        string mudar = ret.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
                        ret.mensagem = mudar;

                        break;
                    }
                case "1.3.2"://AnimaisAuxEspecies
                    {
                        AnimaisAuxEspecy especieDcl;
                        clAnimaisAuxEspeciesBll especieBll = new clAnimaisAuxEspeciesBll();

                        especieDcl = especieBll.Carregar(_objeto.Id);

                        especieDcl.Ativo = false;
                        especieDcl.IdOperador = _objeto.IdOperador;
                        especieDcl.DataCadastro = _objeto.DataCadastro;
                        especieDcl.IP = _objeto.IP;

                        ret = especieBll.Excluir(especieDcl);
                        string mudar = ret.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
                        ret.mensagem = mudar;

                        break;
                    }
                case "1.4.2"://ExigenciasNutrAuxIndicacoes
                    {
                        ExigenciasNutrAuxIndicacoe indicDcl;
                        clExigenciasNutrAuxIndicacoesBll indicBll = new
                            clExigenciasNutrAuxIndicacoesBll();

                        indicDcl = indicBll.Carregar(_objeto.Id);

                        indicDcl.Ativo = false;
                        indicDcl.IdOperador = _objeto.IdOperador;
                        indicDcl.DataCadastro = _objeto.DataCadastro;
                        indicDcl.IP = _objeto.IP;

                        ret = indicBll.Excluir(indicDcl);
                        string mudar = ret.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
                        ret.mensagem = mudar;

                        break;
                    }
                case "1.4.5"://AlimentosAuxGrupos
                    {
                        AlimentosAuxGrupo alimGruposDcl;
                        clAlimentosAuxGruposBll alimGruposBll = new clAlimentosAuxGruposBll();

                        alimGruposDcl = alimGruposBll.Carregar(_objeto.Id);

                        alimGruposDcl.Ativo = false;
                        alimGruposDcl.IdOperador = _objeto.IdOperador;
                        alimGruposDcl.DataCadastro = _objeto.DataCadastro;
                        alimGruposDcl.IP = _objeto.IP;

                        ret = alimGruposBll.Excluir(alimGruposDcl);
                        string mudar = ret.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
                        ret.mensagem = mudar;

                        break;
                    }
                case "1.4.6"://AlimentosAuxFontes
                    {
                        AlimentosAuxFonte alimFontesDcl;
                        clAlimentosAuxFontesBll alimFontesBll = new clAlimentosAuxFontesBll();

                        alimFontesDcl = alimFontesBll.Carregar(_objeto.Id);

                        alimFontesDcl.Ativo = false;
                        alimFontesDcl.IdOperador = _objeto.IdOperador;
                        alimFontesDcl.DataCadastro = _objeto.DataCadastro;
                        alimFontesDcl.IP = _objeto.IP;

                        ret = alimFontesBll.Excluir(alimFontesDcl);
                        string mudar = ret.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
                        ret.mensagem = mudar;

                        break;
                    }
                case "1.4.7"://AlimentosAuxCategorias
                    {
                        AlimentosAuxCategorias alimCategoriasDcl;
                        clAlimentosAuxCategoriasBll alimCategoriasBll = new 
                            clAlimentosAuxCategoriasBll();

                        alimCategoriasDcl = alimCategoriasBll.Carregar(_objeto.Id);

                        alimCategoriasDcl.Ativo = false;
                        alimCategoriasDcl.IdOperador = _objeto.IdOperador;
                        alimCategoriasDcl.DataCadastro = _objeto.DataCadastro;
                        alimCategoriasDcl.IP = _objeto.IP;

                        ret = alimCategoriasBll.Excluir(alimCategoriasDcl);
                        string mudar = ret.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
                        ret.mensagem = mudar;

                        break;
                    }
                case "1.4.8"://AtendimentoAuxTipos
                    {
                        AtendimentoAuxTipo atendTpDcl;
                        clAtendimentoAuxTiposBll atendTpBll = new
                            clAtendimentoAuxTiposBll();

                        atendTpDcl = atendTpBll.Carregar(_objeto.Id);

                        atendTpDcl.Ativo = false;
                        atendTpDcl.IdOperador = _objeto.IdOperador;
                        atendTpDcl.DataCadastro = _objeto.DataCadastro;
                        atendTpDcl.IP = _objeto.IP;

                        ret = atendTpBll.Excluir(atendTpDcl);
                        string mudar = ret.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
                        ret.mensagem = mudar;

                        break;
                    }
                case "10.0.1"://BibliotecaAuxSecoes
                    {
                        clBibliotecaAuxSecoesBll secoesBll = new clBibliotecaAuxSecoesBll();
                        BibliotecaAuxSecoe secoesDcl = secoesBll.Carregar(_objeto.Id);

                        secoesDcl.Ativo = false;
                        secoesDcl.IdOperador = _objeto.IdOperador;
                        secoesDcl.DataCadastro = _objeto.DataCadastro;
                        secoesDcl.IP = _objeto.IP;

                        ret = secoesBll.Excluir(secoesDcl);
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
        #endregion

        #region Títulos
        /// <summary>
        /// Retorna no Índice 0 - Título da Página,
        ///                   1 - Label da Descrição,
        ///                   2 - Label da Sigla,
        ///                   3 - Cód. da Tela
        /// </summary>
        /// <param name="_tela"></param>
        /// <returns></returns>
        public ListItemCollection PopulaTitulos(string _tela)
        {
            ListItemCollection retLista = new ListItemCollection();

            switch (_tela)
            {
                case "1.1.1"://AcessosAuxFuncoes
                    {
                        retLista.Add(new ListItem("Titulo", "Cadastro de Funções de Usuários"));
                        retLista.Add(new ListItem("Nome", "Função"));
                        retLista.Add(new ListItem("Sigla", "sem sigla"));
                        retLista.Add(new ListItem("PermTela", "1.1.1"));

                        break;
                    }
                case "1.1.2"://AcessosAuxTelas
                    {
                        retLista.Add(new ListItem("Titulo", "Cadastro de Telas"));
                        retLista.Add(new ListItem("Nome", "Tela"));
                        retLista.Add(new ListItem("Sigla", "Código Tela"));
                        retLista.Add(new ListItem("PermTela", "1.1.2"));

                        break;
                    }
                case "1.2.1"://PrescricaoAuxTipos
                    {
                        retLista.Add(new ListItem("Titulo", "Cadastro de Tipos de Prescrições"));
                        retLista.Add(new ListItem("Nome", "Tipo Prescrição"));
                        retLista.Add(new ListItem("Sigla", "sem sigla"));
                        retLista.Add(new ListItem("PermTela", "1.2.1"));

                        break;
                    }
                case "1.3.2"://AnimaisAuxEspecies
                    {
                        retLista.Add(new ListItem("Titulo", "Cadastro de Espécies"));
                        retLista.Add(new ListItem("Nome", "Espécie"));
                        retLista.Add(new ListItem("Sigla", "sem sigla"));
                        retLista.Add(new ListItem("PermTela", "1.3.2"));

                        break;
                    }
                case "1.4.2"://ExigenciasNutrAuxIndicacoes
                    {
                        retLista.Add(new ListItem("Titulo",
                            "Cadastro de Indicações das Exigências Nutricionais"));
                        retLista.Add(new ListItem("Nome", "Indicação"));
                        retLista.Add(new ListItem("Sigla", "sem sigla"));
                        retLista.Add(new ListItem("PermTela", "1.4.2"));

                        break;
                    }
                case "1.4.5"://AlimentosAuxGrupos
                    {
                        retLista.Add(new ListItem("Titulo", "Cadastro de Grupos dos Alimentos"));
                        retLista.Add(new ListItem("Nome", "Grupos"));
                        retLista.Add(new ListItem("Sigla", "sem sigla"));
                        retLista.Add(new ListItem("PermTela", "1.4.5"));

                        break;
                    }
                case "1.4.6"://AlimentosAuxFontes
                    {
                        retLista.Add(new ListItem("Titulo", "Cadastro de Fontes dos Alimentos"));
                        retLista.Add(new ListItem("Nome", "Fontes"));
                        retLista.Add(new ListItem("Sigla", "sem sigla"));
                        retLista.Add(new ListItem("PermTela", "1.4.6"));

                        break;
                    }
                case "1.4.7"://AlimentosAuxCategorias
                    {
                        retLista.Add(new ListItem("Titulo", "Cadastro de Categorias dos Alimentos"));
                        retLista.Add(new ListItem("Nome", "Categorias"));
                        retLista.Add(new ListItem("Sigla", "sem sigla"));
                        retLista.Add(new ListItem("PermTela", "1.4.7"));

                        break;
                    }
                case "1.4.8"://AtendimentoAuxTipos
                    {
                        retLista.Add(new ListItem("Titulo", "Cadastro de Tipos de Atendimentos"));
                        retLista.Add(new ListItem("Nome", "Tipos de Atendimento"));
                        retLista.Add(new ListItem("Sigla", "sem sigla"));
                        retLista.Add(new ListItem("PermTela", "1.4.8"));

                        break;
                    }
                case "10.0.1"://BibliotecaSeções
                    {
                        retLista.Add(new ListItem("Titulo", "Cadastro de Seções para a Biblioteca"));
                        retLista.Add(new ListItem("Nome", "Seção"));
                        retLista.Add(new ListItem("Sigla", "sem sigla"));
                        retLista.Add(new ListItem("PermTela", "10.0.1"));

                        break;
                    }
            }

            return retLista;
        }
        #endregion

        #region Listagem
        public List<TOTela3Bll> Listar(string _tela, string _pesquisa,
            int _tamPag, int _pagAtual)
        {
            List<TOTela3Bll> retLista = new List<TOTela3Bll>();

            switch (_tela)
            {
                case "1.1.1"://AcessosAuxFuncoes
                    {
                        clAcessosAuxFuncoesBll funcoesBll = new clAcessosAuxFuncoesBll();

                        retLista = funcoesBll.Listar(_pesquisa, _tamPag, _pagAtual);

                        break;
                    }
                case "1.1.2"://AcessosAuxTelas
                    {
                        clAcessosAuxTelasBll telasBll = new clAcessosAuxTelasBll();

                        retLista = telasBll.Listar(_pesquisa, _tamPag, _pagAtual);

                        break;
                    }
                case "1.2.1"://PrescricaoAuxTipos
                    {
                        clPrescricaoAuxTiposBll prescrBll = new clPrescricaoAuxTiposBll();

                        retLista = prescrBll.Listar(_pesquisa, _tamPag, _pagAtual);

                        break;
                    }
                case "1.3.2"://AnimaisAuxEspecies
                    {
                        clAnimaisAuxEspeciesBll especieBll = new clAnimaisAuxEspeciesBll();

                        retLista = especieBll.Listar(_pesquisa, _tamPag, _pagAtual);

                        break;
                    }
                case "1.4.2"://ExigenciasNutrAuxIndicacoes
                    {
                        clExigenciasNutrAuxIndicacoesBll indicBll = new
                            clExigenciasNutrAuxIndicacoesBll();

                        retLista = indicBll.Listar(_pesquisa, _tamPag, _pagAtual);

                        break;
                    }
                case "1.4.5"://AlimentosAuxGrupos
                    {
                        clAlimentosAuxGruposBll alimGruposBll = new clAlimentosAuxGruposBll();

                        retLista = alimGruposBll.Listar(_pesquisa, _tamPag, _pagAtual);

                        break;
                    }
                case "1.4.6"://AlimentosAuxFontes
                    {
                        clAlimentosAuxFontesBll alimFontesBll = new clAlimentosAuxFontesBll();

                        retLista = alimFontesBll.Listar(_pesquisa, _tamPag, _pagAtual);

                        break;
                    }
                case "1.4.7"://AlimentosAuxCategorias
                    {
                        clAlimentosAuxCategoriasBll alimCategoriasBll = new 
                            clAlimentosAuxCategoriasBll();

                        retLista = alimCategoriasBll.Listar(_pesquisa, _tamPag, _pagAtual);

                        break;
                    }

                case "1.4.8"://AtendimentosAuxTipos
                    {
                        clAtendimentoAuxTiposBll atendTpBll = new
                            clAtendimentoAuxTiposBll();

                        retLista = atendTpBll.Listar(_pesquisa, _tamPag, _pagAtual);

                        break;
                    }
                case "10.0.1"://BibliotecaAuxSecoes
                    {
                        clBibliotecaAuxSecoesBll secoesBll = new clBibliotecaAuxSecoesBll();

                        retLista = secoesBll.Listar(_pesquisa, _tamPag, _pagAtual);

                        break;
                    }
            }

            return retLista;
        }
        #endregion

        #region TotalPaginas
        public int TotalPaginas(string _tela, string _pesqNome, int _tamPag)
        {
            int _ret = 0;

            switch (_tela)
            {
                case "1.1.1"://AcessosAuxFuncoes
                    {
                        clAcessosAuxFuncoesBll funcoesBll = new clAcessosAuxFuncoesBll();

                        _ret = funcoesBll.TotalPaginas(_pesqNome, _tamPag);

                        break;
                    }
                case "1.1.2"://AcessosAuxTelas
                    {
                        clAcessosAuxTelasBll telasBll = new clAcessosAuxTelasBll();

                        _ret = telasBll.TotalPaginas(_pesqNome, _tamPag);

                        break;
                    }
                case "1.2.1"://PrescricaoAuxTipos
                    {
                        clPrescricaoAuxTiposBll prescrBll = new clPrescricaoAuxTiposBll();

                        _ret = prescrBll.TotalPaginas(_pesqNome, _tamPag);

                        break;
                    }
                case "1.3.2"://AnimaisAuxEspecies
                    {
                        clAnimaisAuxEspeciesBll especieBll = new clAnimaisAuxEspeciesBll();

                        _ret = especieBll.TotalPaginas(_pesqNome, _tamPag);

                        break;
                    }
                case "1.4.2"://ExigenciasNutrAuxIndicacoes
                    {
                        clExigenciasNutrAuxIndicacoesBll indicBll = new
                            clExigenciasNutrAuxIndicacoesBll();

                        _ret = indicBll.TotalPaginas(_pesqNome, _tamPag);

                        break;
                    }
                case "1.4.5"://AlimentosAuxGrupos
                    {
                        clAlimentosAuxGruposBll alimGruposBll = new clAlimentosAuxGruposBll();

                        _ret = alimGruposBll.TotalPaginas(_pesqNome, _tamPag);

                        break;
                    }
                case "1.4.6"://AlimentosAuxFontes
                    {
                        clAlimentosAuxFontesBll alimFontesBll = new clAlimentosAuxFontesBll();

                        _ret = alimFontesBll.TotalPaginas(_pesqNome, _tamPag);

                        break;
                    }
                case "1.4.7"://AlimentosAuxCategorias
                    {
                        clAlimentosAuxCategoriasBll alimCategoriasBll = new 
                            clAlimentosAuxCategoriasBll();

                        _ret = alimCategoriasBll.TotalPaginas(_pesqNome, _tamPag);

                        break;
                    }
                case "1.4.8"://AtendimentosAuxTipos
                    {
                        clAtendimentoAuxTiposBll atendTpBll = new
                            clAtendimentoAuxTiposBll();

                        _ret = atendTpBll.TotalPaginas(_pesqNome, _tamPag);

                        break;
                    }
                case "10.0.1"://BibliotecaAuxSecoes
                    {
                        clBibliotecaAuxSecoesBll secoesBll = new clBibliotecaAuxSecoesBll();

                        _ret = secoesBll.TotalPaginas(_pesqNome, _tamPag);

                        break;
                    }
            }

            return _ret;
        }
        #endregion
    }
}