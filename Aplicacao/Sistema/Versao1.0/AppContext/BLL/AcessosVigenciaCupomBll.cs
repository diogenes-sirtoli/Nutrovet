using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
    public class clAcessosVigenciaCupomBll
    {
        public bllRetorno Inserir(AcessosVigenciaCupomDesconto _cupom)
        {
            bllRetorno ret = _cupom.ValidarRegras(true);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_cupom);

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

        public bllRetorno Alterar(AcessosVigenciaCupomDesconto _cupom)
        {
            bllRetorno ret = _cupom.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_cupom);

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

        public bllRetorno Excluir(AcessosVigenciaCupomDesconto _cupom)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_cupom);

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

        public AcessosVigenciaCupomDesconto Carregar(int _idCupom)
        {
            CrudDal crud = new CrudDal();

            AcessosVigenciaCupomDesconto _cupom = null;

            var cupom = crud.ExecutarComando<AcessosVigenciaCupomDesconto>(
                string.Format(@"
                Select *
                From AcessosVigenciaCupomDesconto
                Where (IdCupom = {0})", _idCupom));

            foreach (var item in cupom)
            {
                _cupom = item;
            }

            return _cupom;
        }

        public AcessosVigenciaCupomDesconto Carregar(string _nrCupom)
        {
            CrudDal crud = new CrudDal();

            AcessosVigenciaCupomDesconto _cupom = null;

            var cupom = crud.ExecutarComando<AcessosVigenciaCupomDesconto>(
                string.Format(@"
                Select *
                From AcessosVigenciaCupomDesconto
                Where (NrCumpom = '{0}')", _nrCupom));

            foreach (var item in cupom)
            {
                _cupom = item;
            }

            return _cupom;
            //teste
        }

        public bool Vigencia(string _Nrcupom)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _today = DateTime.Today.ToString("dd/MM/yyyy");
            string _sql = string.Format(@"
                SET DATEFORMAT dmy

                SELECT  COUNT(IdCupom) AS Total
                FROM    AcessosVigenciaCupomDesconto
                WHERE   ('{1}' Between DtInicial And DtFinal) AND 
                        (NrCumpom = '{0}') And (fUsado = 0)", _Nrcupom, _today);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }

        public bool InvalidarVoucher(string _Nrcupom)
        {
            bool _ret = false;

            if ((_Nrcupom != null) && (_Nrcupom != ""))
            {
                AcessosVigenciaCupomDesconto cupomDcl = Carregar(_Nrcupom);

                cupomDcl.fUsado = true;
                cupomDcl.Ativo = true;
                cupomDcl.IdOperador = 1;
                cupomDcl.DataCadastro = DateTime.Now;

                bllRetorno _retVoucher = Alterar(cupomDcl);

                _ret = _retVoucher.retorno;
            }

            return _ret;
        }

        public double CalculaValorDesconto(int _idTipoDesc, double _valorBruto,
        int _valorDesc)
        {
            double _valorRetorno = 0;


            switch (_idTipoDesc)
            {
                case (int)DominiosBll.CupomDescontoTipos.Anual:
                    {
                        _valorRetorno = _valorBruto;

                        break;
                    }
                case (int)DominiosBll.CupomDescontoTipos.Mensal:
                    {
                        _valorRetorno = _valorBruto;

                        break;
                    }
                case (int)DominiosBll.CupomDescontoTipos.Percentual:
                    {
                        _valorRetorno = _valorBruto - (
                            (_valorBruto * _valorDesc) / 100);

                        break;
                    }
                case (int)DominiosBll.CupomDescontoTipos.Valor:
                    {
                        _valorRetorno = _valorBruto - _valorDesc;

                        break;
                    }
            }

            return _valorRetorno;
        }

        //Revisar
        public List<TOAcessosVigenciaCupomBll> ListarTO(int _idVigencia)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SELECT  AcessosVigenciaPlanos.IdPessoa, Pessoas.Nome, 
                        AcessosVigenciaCupomDesconto.IdVigencia, 
                        AcessosVigenciaPlanos.IdPlano, PlanosAssinaturas.Plano, 
                        AcessosVigenciaCupomDesconto.IdCupom, 
                        AcessosVigenciaCupomDesconto.NrCumpom, 
                        AcessosVigenciaCupomDesconto.PercentDesconto, 
                        AcessosVigenciaCupomDesconto.DtInicial, 
                        AcessosVigenciaCupomDesconto.DtFinal, 
                        AcessosVigenciaCupomDesconto.Ativo, 
                        AcessosVigenciaCupomDesconto.IdOperador, 
                        AcessosVigenciaCupomDesconto.IP, 
                        AcessosVigenciaCupomDesconto.DataCadastro
                FROM    AcessosVigenciaCupomDesconto INNER JOIN
                        AcessosVigenciaPlanos ON AcessosVigenciaCupomDesconto.IdVigencia = 
                            AcessosVigenciaPlanos.IdVigencia INNER JOIN
                        PlanosAssinaturas ON AcessosVigenciaPlanos.IdPlano = 
                            PlanosAssinaturas.IdPlano INNER JOIN
                        Pessoas ON AcessosVigenciaPlanos.IdPessoa = Pessoas.IdPessoa
                WHERE   (AcessosVigenciaCupomDesconto.Ativo = 1) AND 
                        (AcessosVigenciaCupomDesconto.IdVigencia = {0})
                ORDER BY PlanosAssinaturas.Plano, Pessoas.Nome",
                        _idVigencia);

            var lista = crud.Listar<TOAcessosVigenciaCupomBll>(_sql);

            return lista.ToList();
        }

        public void GerarVouchers(int _nrInicial, int _nrFinal, int _tipo,
            int _valor, int _validadeEmDias)
        {
            CrudDal crud = new CrudDal();
            int _total = 0;

            if (_nrFinal < _nrInicial)
            {
                int _troca = _nrFinal;

                _nrFinal = _nrInicial;
                _nrInicial = _troca;
            }

            _total = _nrFinal - _nrInicial;

            for (int i = 0; i < _total; i++)
            {
                string _sql = string.Format(@"
                SET DATEFORMAT dmy

                Insert Into AcessosVigenciaCupomDesconto 
	                (NrCumpom, IdTipoDesc, ValorDesc, DtInicial, DtFinal, fUsado, fAcessoLiberado,
                     Ativo, IdOperador, DataCadastro)
                Values ({0}, {1}, {2}, '{3}', '{4}', {5}, {6}, {7}, '{8}')",
                _nrInicial + i, _tipo, _valor, DateTime.Today.ToString("dd/MM/yyyy"),
                DateTime.Today.AddDays(_validadeEmDias).ToString("dd/MM/yyyy"), 0,
                1, 1, DateTime.Now);

                int executar = crud.ExecutarComando(_sql);
            }
        }

        public bllRetorno VoucherSituacao(string _Nrcupom)
        {
            CrudDal crud = new CrudDal();
            AcessosVigenciaCupomDesconto cupomDcl = Carregar(_Nrcupom);
            bllRetorno retorno = new bllRetorno();
            string _today = DateTime.Today.ToString("dd/MM/yyyy");

            if ((cupomDcl.NrCumpom != null) && (cupomDcl.NrCumpom != ""))
            {
                if (Vigencia(_Nrcupom))
                {
                    retorno.retorno = true;
                    retorno.mensagem = "Voucher Válido!!!";
                }
                else
                {
                    retorno.retorno = false;
                    retorno.mensagem = "Prazo do Voucher Expiado!!!";
                }
            }
            else
            {
                retorno.retorno = false;
                retorno.mensagem = "Voucher Não Existe!!!";
            }

            return retorno;
        }
    }
}
