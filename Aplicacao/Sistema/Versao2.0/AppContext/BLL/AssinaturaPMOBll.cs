using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PagarMeOld;
using DCL;

namespace BLL
{
    public class clAssinaturaPMOBll
    {
        public bllRetorno Inserir(TOAssinaturaPMOBll _assinatura)
        {
            bool _serverLocal = Conexao.ServidorLocal();
            AssinaturaPMO assinaturaPMO = new AssinaturaPMO();
            bllRetornoPMO _ret = new bllRetornoPMO();
            TOAssinaturaPMOBll TOAssinaturaPMOBll = null;
            List<object> _retAssinatura = new List<object>();

            try
            {
                _ret = assinaturaPMO.Inserir(ConverteToBllEmToPMO(_assinatura), _serverLocal);

                if (_ret.objeto.Count > 0)
                {
                    foreach (TOAssinaturaPMO item in _ret.objeto)
                    {
                        TOAssinaturaPMOBll = ConverteToPMOEmToBll(item);

                        _retAssinatura.Add(TOAssinaturaPMOBll);
                    }
                }

                CacheExcluir();

                return bllRetorno.GeraRetorno(true, "INSERÇÃO Efetuada com Sucesso!!!",
                    _retAssinatura);
            }
            catch (Exception err)
            {
                var error = err;
                return bllRetorno.GeraRetorno(false,
                    _ret.mensagem);
            }
        }

        public bllRetorno Alterar(string _idAssinatura, TOAssinaturaPMOBll _assinatura)
        {
            bool _serverLocal = Conexao.ServidorLocal();
            AssinaturaPMO assinaturaPMO = new AssinaturaPMO();
            bllRetornoPMO _ret = new bllRetornoPMO();
            TOAssinaturaPMOBll TOAssinaturaPMOBll = null;
            List<object> _retAssinatura = new List<object>();

            try
            {
                _ret = assinaturaPMO.Alterar(_idAssinatura,
                    ConverteToBllEmToPMO(_assinatura), _serverLocal);

                if (_ret.objeto.Count > 0)
                {
                    foreach (TOAssinaturaPMO item in _ret.objeto)
                    {
                        TOAssinaturaPMOBll = ConverteToPMOEmToBll(item);

                        _retAssinatura.Add(TOAssinaturaPMOBll);
                    }
                }

                CacheExcluir();

                return bllRetorno.GeraRetorno(true, "ALTERAÇÃO Efetuada com Sucesso!!!",
                    _retAssinatura);
            }
            catch (Exception err)
            {
                var erro = err;
                return bllRetorno.GeraRetorno(false,
                    _ret.mensagem);
            }
        }

        public TOAssinaturaPMOBll Carregar(string _idAssinatura)
        {
            bool _serverLocal = Conexao.ServidorLocal();
            AssinaturaPMO _pagarMeOld = new AssinaturaPMO();
            TOAssinaturaPMO _assinatura = _pagarMeOld.Carregar(_idAssinatura, _serverLocal);
            TOAssinaturaPMOBll _retAssinatura;

            if (!_assinatura.ErrorHash)
            {
                _retAssinatura = ConverteToPMOEmToBll(_assinatura);
            }
            else
            {
                _retAssinatura = new TOAssinaturaPMOBll();
                _retAssinatura.ErrorHash = true;
                _retAssinatura.ErrorMessage = _assinatura.ErrorMessage;
            }

            return _retAssinatura;
        }

        public bllRetorno Cancelar(string _idAssinatura)
        {
            bool _serverLocal = Conexao.ServidorLocal();
            AssinaturaPMO assinaturaPMO = new AssinaturaPMO();
            bllRetornoPMO _ret = new bllRetornoPMO();

            try
            {
                _ret = assinaturaPMO.Cancelar(_idAssinatura, _serverLocal);
                CacheExcluir();

                return bllRetorno.GeraRetorno(true,
                    "CANCELAMENTO Efetuado com Sucesso!!!", _ret.objeto);
            }
            catch (Exception err)
            {
                var error = err;
                return bllRetorno.GeraRetorno(false,
                    _ret.mensagem);
            }
        }

        private List<TOAssinaturaPMOBll> ListarCache()
        {
            bool _serverLocal = Conexao.ServidorLocal();
            AssinaturaPMO assinaturaPMO = new AssinaturaPMO();
            List<TOAssinaturaPMOBll> _retLista = new List<TOAssinaturaPMOBll>();

            List<TOAssinaturaPMO> listagem = assinaturaPMO.Listar(_serverLocal);

            foreach (TOAssinaturaPMO item in listagem)
            {
                _retLista.Add(ConverteToPMOEmToBll(item));
            }

            return _retLista;
        }

        private List<TOAssinaturaPMOBll> CacheAssinaturas()
        {
            System.Web.Caching.Cache _Cache;
            _Cache = HttpContext.Current.Cache;
            List<TOAssinaturaPMOBll> CacheAssinatura = (List<TOAssinaturaPMOBll>)_Cache["CacheAssinaturas"];

            if (CacheAssinatura == null)
            {
                _Cache.Insert("CacheAssinaturas", ListarCache(), null, 
                    DateTime.Now.AddHours(4), TimeSpan.Zero);
                CacheAssinatura = (List<TOAssinaturaPMOBll>)_Cache["CacheAssinaturas"];
            }

            return CacheAssinatura;
        }

        public void CacheExcluir()
        {
            System.Web.Caching.Cache _Cache;
            _Cache = HttpContext.Current.Cache;

            _Cache.Remove("CacheAssinaturas");
        }

        public List<TOAssinaturaPMOBll> Listar(int _tamPag, int _pagAtual)
        {
            List<TOAssinaturaPMOBll> _retLista = (from l in CacheAssinaturas()
                                                  orderby l.NameCustomer
                                                  select l).Skip((_pagAtual - 1) * _tamPag).
                                                Take(_tamPag).ToList();

            return _retLista;
        }

        public List<TOAssinaturaPMOBll> ListarAssinaturasClientes(int _idPessoa)
        {
            clAcessosVigenciaPlanosBll _planosBLL = new clAcessosVigenciaPlanosBll();
            List<AcessosVigenciaPlano> _planosBD = _planosBLL.ListarPlanos(_idPessoa);
            List<TOAssinaturaPMOBll> _retLista = (from l in CacheAssinaturas()
                                                  join plan in _planosBD on l.Id equals
                                                    plan.IdSubscriptionPagarMe
                                                  orderby l.NameCustomer
                                                  select new TOAssinaturaPMOBll
                                                  {
                                                      IdPessoa = plan.IdPessoa,
                                                      IdVigencia = plan.IdVigencia,
                                                      NamePlan = l.NamePlan,
                                                      Id = l.Id,
                                                      Amount = l.Amount,
                                                      Status = l.Status,
                                                      CurrentPeriodStart = l.CurrentPeriodStart,
                                                      DtInicial = plan.DtInicial,
                                                      CurrentPeriodEnd = l.CurrentPeriodEnd,
                                                      DtFinal = plan.DtFinal,
                                                      SubscriptionType = l.SubscriptionType
                                                  }).OrderByDescending(o => o.CurrentPeriodStart).ToList();

            return _retLista;
        }

        public List<TOAssinaturaPMOBll> ListarAssinaturasClientesRealTime(int _idPessoa)
        {
            clAcessosVigenciaPlanosBll _planosBLL = new clAcessosVigenciaPlanosBll();
            List<TOAcessosVigenciaPlanosBll> _planosBD = _planosBLL.ListarTO(_idPessoa);
            List<TOAssinaturaPMOBll> _retLista = (from l in ListarCache()
                                                  join plan in _planosBD on l.Id equals
                                                    plan.IdSubscriptionPagarMe
                                                  orderby l.NameCustomer
                                                  select new TOAssinaturaPMOBll
                                                  {
                                                      IdPessoa = plan.IdPessoa,
                                                      IdVigencia = plan.IdVigencia,
                                                      NamePlan = (l.NamePlan != null && 
                                                        l.NamePlan != "" ? l.NamePlan : 
                                                            plan.Plano + " - " + plan.Periodo),
                                                      Id = l.Id,
                                                      Amount = l.Amount,
                                                      Status = l.Status,
                                                      CurrentPeriodStart = l.CurrentPeriodStart,
                                                      DtInicial = plan.DtInicial,
                                                      CurrentPeriodEnd = l.CurrentPeriodEnd,
                                                      DtFinal = plan.DtFinal,
                                                      SubscriptionType = l.SubscriptionType
                                                  }).OrderByDescending(o => o.CurrentPeriodStart).ToList();

            return _retLista;
        }

        public bllRetorno AtualizaDatasAssinatura(int _idPessoa)
        {
            bllRetorno _ret = new bllRetorno();
            clAcessosVigenciaPlanosBll _planoBll = new clAcessosVigenciaPlanosBll();
            clAcessosVigenciaSituacaoBll _situacaoBll = new clAcessosVigenciaSituacaoBll();
            AcessosVigenciaSituacao _situacaoDcl;
            List<TOAssinaturaPMOBll> _retLista = ListarAssinaturasClientes(_idPessoa);
            //List<TOAssinaturaPMOBll> _retListaTotal = ListarCache();

            if ((_retLista != null) && (_retLista.Count > 0))
            {
                int _valorStatusPagarMe = Funcoes.Funcoes.CarregarEnumValor<
                    DominiosBll.AcessosPlanosAuxSituacaoIngles>(_retLista[0].Status);

                //(_retLista[0].Status != null && _retLista[0].Status != "" ?
                //    (int)Enum.Parse(typeof(DominiosBll.AcessosPlanosAuxSituacaoIngles),
                //        _retLista[0].Status) : 0);

                AcessosVigenciaPlano planoDcl = _planoBll.Carregar(_retLista[0].IdVigencia);

                planoDcl.StatusPagarMe = _retLista[0].Status;
                planoDcl.DtFinal = (_retLista[0].CurrentPeriodEnd != null ?
                    _retLista[0].CurrentPeriodEnd.Value : planoDcl.DtFinal);

                planoDcl.Ativo = true;
                planoDcl.IdOperador = 1;
                planoDcl.DataCadastro = DateTime.Now;
                planoDcl.IP = "192.168.0.100";

                try
                {
                    _ret = _planoBll.Alterar(planoDcl);

                    if (_ret.retorno)
                    {
                        int _situacaoUltima = _situacaoBll.UltimaSituacao(planoDcl.IdVigencia);

                        if (_situacaoUltima != _valorStatusPagarMe)
                        {
                            _situacaoDcl = new AcessosVigenciaSituacao();

                            _situacaoDcl.IdVigencia = planoDcl.IdVigencia;
                            _situacaoDcl.IdSituacao = _valorStatusPagarMe;
                            _situacaoDcl.DataSituacao = DateTime.Today;

                            _situacaoDcl.Ativo = true;
                            _situacaoDcl.IdOperador = 1;
                            _situacaoDcl.DataCadastro = DateTime.Now;
                            _situacaoDcl.IP = "192.168.0.100";

                            bllRetorno _retSituacao = _situacaoBll.Inserir(_situacaoDcl);
                        }
                    }

                    return bllRetorno.GeraRetorno(true,
                        "ALTERAÇÃO Efetuada com Sucesso!!!");
                }
                catch (Exception err)
                {
                    var erro = err;
                    return bllRetorno.GeraRetorno(false, _ret.mensagem);
                }
            }
            else
            {
                return bllRetorno.GeraRetorno(false,
                        "Não Foi Possível efetuar a ALTERAÇÃO!!!");
            }
        }

        private TOAssinaturaPMO ConverteToBllEmToPMO(TOAssinaturaPMOBll _assinaturaToBll)
        {
            TOAssinaturaPMO _assinaturaPMOTO = new TOAssinaturaPMO();

            _assinaturaPMOTO.IdPlan = _assinaturaToBll.IdPlan;
            _assinaturaPMOTO.Amount = _assinaturaToBll.Amount;
            _assinaturaPMOTO.Days = _assinaturaToBll.Days;
            _assinaturaPMOTO.NamePlan = _assinaturaToBll.NamePlan;
            _assinaturaPMOTO.DateCreatedPlan = _assinaturaToBll.DateCreatedPlan;
            _assinaturaPMOTO.Charges = _assinaturaToBll.Charges;

            _assinaturaPMOTO.Id = _assinaturaToBll.Id;
            _assinaturaPMOTO.Status = _assinaturaToBll.Status;
            _assinaturaPMOTO.DateCreated = _assinaturaToBll.DateCreated;
            _assinaturaPMOTO.DateUpdated = _assinaturaToBll.DateUpdated;
            _assinaturaPMOTO.CurrentPeriodStart = _assinaturaToBll.CurrentPeriodStart;
            _assinaturaPMOTO.CurrentPeriodEnd = _assinaturaToBll.CurrentPeriodEnd;
            _assinaturaPMOTO.SubscriptionType = _assinaturaToBll.SubscriptionType;

            _assinaturaPMOTO.CardHolderName = _assinaturaToBll.CardHolderName;
            _assinaturaPMOTO.CardNumber = _assinaturaToBll.CardNumber;
            _assinaturaPMOTO.CardFirstDigits = _assinaturaToBll.CardFirstDigits;
            _assinaturaPMOTO.CardLastDigits = _assinaturaToBll.CardLastDigits;
            _assinaturaPMOTO.CardBrand = _assinaturaToBll.CardBrand;
            _assinaturaPMOTO.CardCvv = _assinaturaToBll.CardCvv;
            _assinaturaPMOTO.CardExpirationDate = _assinaturaToBll.CardExpirationDate;

            _assinaturaPMOTO.IdCustomer = _assinaturaToBll.IdCustomer;
            _assinaturaPMOTO.TypeCustomer = _assinaturaToBll.TypeCustomer;
            _assinaturaPMOTO.TypeEntityCustomer = _assinaturaToBll.TypeEntityCustomer;
            _assinaturaPMOTO.DocumentNumberCustomer = _assinaturaToBll.DocumentNumberCustomer;
            _assinaturaPMOTO.DocumentTypeCustomer = _assinaturaToBll.DocumentTypeCustomer;
            _assinaturaPMOTO.NameCustomer = _assinaturaToBll.NameCustomer;
            _assinaturaPMOTO.EmailCustomer = _assinaturaToBll.EmailCustomer;
            _assinaturaPMOTO.DdiCustomer = _assinaturaToBll.DdiCustomer;
            _assinaturaPMOTO.DddCustomer = _assinaturaToBll.DddCustomer;
            _assinaturaPMOTO.PhoneCustomer = _assinaturaToBll.PhoneCustomer;
            _assinaturaPMOTO.BirthdayCustomer = _assinaturaToBll.BirthdayCustomer;
            _assinaturaPMOTO.GenderCustomer = _assinaturaToBll.GenderCustomer;

            _assinaturaPMOTO.NationalityCustomer = _assinaturaToBll.NationalityCustomer;
            _assinaturaPMOTO.ContryCustomer = _assinaturaToBll.ContryCustomer;
            _assinaturaPMOTO.ZipCodeCustomer = _assinaturaToBll.ZipCodeCustomer;
            _assinaturaPMOTO.StateCustomer = _assinaturaToBll.StateCustomer;
            _assinaturaPMOTO.CityCustomer = _assinaturaToBll.CityCustomer;
            _assinaturaPMOTO.NeighborhoodCustomer = _assinaturaToBll.NeighborhoodCustomer;
            _assinaturaPMOTO.StreetCustomer = _assinaturaToBll.StreetCustomer;
            _assinaturaPMOTO.StreetNumberCustomer = _assinaturaToBll.StreetNumberCustomer;
            _assinaturaPMOTO.ComplementaryCustomer = _assinaturaToBll.ComplementaryCustomer;

            _assinaturaPMOTO.Loaded = _assinaturaToBll.Loaded;
            _assinaturaPMOTO.PostBackURL = _assinaturaToBll.PostBackURL;
            _assinaturaPMOTO.ErrorMessage = _assinaturaToBll.ErrorMessage;
            _assinaturaPMOTO.ErrorHash = _assinaturaToBll.ErrorHash;

            return _assinaturaPMOTO;
        }

        private TOAssinaturaPMOBll ConverteToPMOEmToBll(TOAssinaturaPMO _assinaturaPMOTO)
        {
            TOAssinaturaPMOBll _assinaturaToBll = new TOAssinaturaPMOBll();

            _assinaturaToBll.IdPlan = _assinaturaPMOTO.IdPlan;
            _assinaturaToBll.Amount = _assinaturaPMOTO.Amount;
            _assinaturaToBll.Days = _assinaturaPMOTO.Days;
            _assinaturaToBll.NamePlan = _assinaturaPMOTO.NamePlan;
            _assinaturaToBll.DateCreatedPlan = _assinaturaPMOTO.DateCreatedPlan;
            _assinaturaToBll.Charges = _assinaturaPMOTO.Charges;
            _assinaturaToBll.Id = _assinaturaPMOTO.Id;
            _assinaturaToBll.Status = _assinaturaPMOTO.Status;
            _assinaturaToBll.DateCreated = _assinaturaPMOTO.DateCreated;
            _assinaturaToBll.DateUpdated = _assinaturaPMOTO.DateUpdated;
            _assinaturaToBll.CurrentPeriodStart = _assinaturaPMOTO.CurrentPeriodStart;
            _assinaturaToBll.CurrentPeriodEnd = _assinaturaPMOTO.CurrentPeriodEnd;
            _assinaturaToBll.SubscriptionType = _assinaturaPMOTO.SubscriptionType;

            _assinaturaToBll.CardHolderName = _assinaturaPMOTO.CardHolderName;
            _assinaturaToBll.CardNumber = _assinaturaPMOTO.CardNumber;
            _assinaturaToBll.CardFirstDigits = _assinaturaPMOTO.CardFirstDigits;
            _assinaturaToBll.CardLastDigits = _assinaturaPMOTO.CardLastDigits;
            _assinaturaToBll.CardBrand = _assinaturaPMOTO.CardBrand;
            _assinaturaToBll.CardCvv = _assinaturaPMOTO.CardCvv;
            _assinaturaToBll.CardExpirationDate = _assinaturaPMOTO.CardExpirationDate;

            _assinaturaToBll.IdCustomer = _assinaturaPMOTO.IdCustomer;
            _assinaturaToBll.TypeCustomer = _assinaturaPMOTO.TypeCustomer;
            _assinaturaToBll.TypeEntityCustomer = _assinaturaPMOTO.TypeEntityCustomer;
            _assinaturaToBll.DocumentNumberCustomer = _assinaturaPMOTO.DocumentNumberCustomer;
            _assinaturaToBll.DocumentTypeCustomer = _assinaturaPMOTO.DocumentTypeCustomer;
            _assinaturaToBll.NameCustomer = _assinaturaPMOTO.NameCustomer;
            _assinaturaToBll.EmailCustomer = _assinaturaPMOTO.EmailCustomer;
            _assinaturaToBll.DdiCustomer = _assinaturaPMOTO.DdiCustomer;
            _assinaturaToBll.DddCustomer = _assinaturaPMOTO.DddCustomer;
            _assinaturaToBll.PhoneCustomer = _assinaturaPMOTO.PhoneCustomer;
            _assinaturaToBll.BirthdayCustomer = _assinaturaPMOTO.BirthdayCustomer;
            _assinaturaToBll.GenderCustomer = _assinaturaPMOTO.GenderCustomer;

            _assinaturaToBll.NationalityCustomer = _assinaturaPMOTO.NationalityCustomer;
            _assinaturaToBll.ContryCustomer = _assinaturaPMOTO.ContryCustomer;
            _assinaturaToBll.ZipCodeCustomer = _assinaturaPMOTO.ZipCodeCustomer;
            _assinaturaToBll.StateCustomer = _assinaturaPMOTO.StateCustomer;
            _assinaturaToBll.CityCustomer = _assinaturaPMOTO.CityCustomer;
            _assinaturaToBll.NeighborhoodCustomer = _assinaturaPMOTO.NeighborhoodCustomer;
            _assinaturaToBll.StreetCustomer = _assinaturaPMOTO.StreetCustomer;
            _assinaturaToBll.StreetNumberCustomer = _assinaturaPMOTO.StreetNumberCustomer;
            _assinaturaToBll.ComplementaryCustomer = _assinaturaPMOTO.ComplementaryCustomer;

            _assinaturaToBll.Loaded = _assinaturaPMOTO.Loaded;
            _assinaturaToBll.PostBackURL = _assinaturaPMOTO.PostBackURL;
            _assinaturaToBll.ErrorMessage = _assinaturaPMOTO.ErrorMessage;
            _assinaturaToBll.ErrorHash = _assinaturaPMOTO.ErrorHash;

            return _assinaturaToBll;
        }
    }
}
