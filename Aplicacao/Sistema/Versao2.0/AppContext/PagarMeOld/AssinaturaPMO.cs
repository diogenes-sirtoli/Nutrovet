using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagarMe;

namespace PagarMeOld
{
    public class AssinaturaPMO
    {
        public bllRetornoPMO Inserir(TOAssinaturaPMO _assinatura, bool _serverLocal)
        {
            PagarMeServiceKey.PagarMeServiceKeyInicializator(_serverLocal);
            List<object> _subscriptionList = new List<object>();
            TOAssinaturaPMO _subscription;

            try
            {
                CardHash card = new CardHash
                {
                    CardNumber = _assinatura.CardNumber,
                    CardHolderName = _assinatura.CardHolderName,
                    CardExpirationDate = _assinatura.CardExpirationDate,
                    CardCvv = _assinatura.CardCvv
                };

                string cardhash = card.Generate();

                Subscription subscription = new Subscription
                {
                    Amount = _assinatura.Amount,
                    Plan = PagarMeService.GetDefaultService().Plans.Find(
                         Funcoes.Funcoes.ConvertePara.String(_assinatura.IdPlan)),

                    PaymentMethod = PaymentMethod.CreditCard,
                    CardNumber = card.CardNumber,
                    CardHolderName = card.CardHolderName,
                    CardExpirationDate = card.CardExpirationDate,
                    CardCvv = card.CardCvv,
                    CardHash = cardhash
                };

                Customer customer = new Customer
                {
                    Name = _assinatura.NameCustomer,
                    ExternalId = (_assinatura.IdCustomer).ToString(),
                    Type = _assinatura.TypeCustomer == 1 ? CustomerType.Individual : CustomerType.Corporation,
                    DocumentNumber = _assinatura.DocumentNumberCustomer,
                    Email = _assinatura.EmailCustomer,
                    Country = _assinatura.ContryCustomer.ToLower(),

                    Address = new Address
                    {
                        Country = _assinatura.ContryCustomer,
                        Zipcode = _assinatura.ZipCodeCustomer,
                        State = _assinatura.StateCustomer,
                        City = _assinatura.CityCustomer,
                        Neighborhood = _assinatura.NeighborhoodCustomer,
                        Street = _assinatura.StreetCustomer,
                        StreetNumber = _assinatura.StreetNumberCustomer,
                        Complementary = _assinatura.ComplementaryCustomer
                    },

                    Documents = new[]
                    {
                         new Document
                         {

                             Type = _assinatura.NationalityCustomer == 2 ? DocumentType.Passport :
                                 _assinatura.TypeCustomer == 1 ? DocumentType.Cpf : DocumentType.Cnpj,
                             Number =  _assinatura.DocumentNumberCustomer
                         }
                     },

                    Phone = new Phone
                    {
                        Ddd = _assinatura.DddCustomer,
                        Number = _assinatura.PhoneCustomer
                    },
                };

                subscription.Customer = customer;

                subscription.Save();

                _subscription = PopularAsinaturaTO(subscription);
                _subscriptionList.Add(_subscription);

                return bllRetornoPMO.GeraRetorno(true, "Assinatura Criada com Sucesso!!!", 
                    _subscriptionList);
            }
            catch (PagarMeException errPagarMe)
            {
                string _msg = "";

                foreach (var item in errPagarMe.Error.Errors)
                {
                    _msg += item.Message;
                }

                return bllRetornoPMO.GeraRetorno(false, _msg);
            }
        }

        public bllRetornoPMO Alterar(string _idAssinatura, TOAssinaturaPMO _assinatura, 
            bool _serverLocal)
        {
            PagarMeServiceKey.PagarMeServiceKeyInicializator(_serverLocal);
            List<object> _subscriptionList = new List<object>();
            TOAssinaturaPMO _subscription;

            try
            {
                var assinaturaAlterar = PagarMeService.GetDefaultService().Subscriptions.Find(
                    _idAssinatura);

                assinaturaAlterar.PaymentMethod = PaymentMethod.CreditCard;

                assinaturaAlterar.CardNumber = _assinatura.CardNumber;
                assinaturaAlterar.CardHolderName = _assinatura.CardHolderName;
                assinaturaAlterar.CardExpirationDate = _assinatura.CardExpirationDate;
                assinaturaAlterar.CardCvv = _assinatura.CardCvv;

                assinaturaAlterar.Plan = PagarMeService.GetDefaultService().Plans.Find(
                    _assinatura.IdPlan);

                assinaturaAlterar.Save();

                _subscription = PopularAsinaturaTO(assinaturaAlterar);
                _subscriptionList.Add(_subscription);

                return bllRetornoPMO.GeraRetorno(true, "Assinatura Alterada com Sucesso!!!",
                    _subscriptionList);
            }
            catch (PagarMeException errPagarMe)
            {
                string _msg = "";

                foreach (var item in errPagarMe.Error.Errors)
                {
                    _msg += item.Message;
                }

                return bllRetornoPMO.GeraRetorno(false, _msg);
            }
        }

        public bllRetornoPMO Cancelar(string _idAssinatura, bool _serverLocal)
        {
            PagarMeServiceKey.PagarMeServiceKeyInicializator(_serverLocal);
            List<object> _lista = new List<object>();

            try
            {
                Subscription planoCancelar = PagarMeService.GetDefaultService().Subscriptions.Find(
                    _idAssinatura);

                planoCancelar.Cancel();

                _lista.Add(planoCancelar.Status);
                
                return bllRetornoPMO.GeraRetorno(true, "Assinatura Cancelada com Sucesso!!!", _lista);
            }
            catch (PagarMeException errPagarMe)
            {
                string _msg = "";

                foreach (var item in errPagarMe.Error.Errors)
                {
                    _msg += item.Message;
                }

                return bllRetornoPMO.GeraRetorno(false, _msg);
            }
        }

        public TOAssinaturaPMO Carregar(string _idAssinatura, bool _serverLocal)
        {
            TOAssinaturaPMO _retAssinatura = new TOAssinaturaPMO();
            PagarMeServiceKey.PagarMeServiceKeyInicializator(_serverLocal);

            if ((_idAssinatura != null) && (_idAssinatura != ""))
            {
                try
                {
                    Subscription assinaturaCarregar = PagarMeService.GetDefaultService().
                        Subscriptions.Find(_idAssinatura);

                    _retAssinatura = PopularAsinaturaTO(assinaturaCarregar);

                    _retAssinatura.ErrorHash = false;
                    _retAssinatura.ErrorMessage = "";
                }
                catch (PagarMeException errPagarMeA)
                {
                    try
                    {
                        Transaction transacaoCarregar = PagarMeService.GetDefaultService().
                            Transactions.Find(_idAssinatura);

                        _retAssinatura = PopularTransacaoTO(transacaoCarregar);

                        _retAssinatura.ErrorHash = false;
                        _retAssinatura.ErrorMessage = "";
                    }
                    catch (PagarMeException errPagarMeT)
                    {

                        _retAssinatura.ErrorHash = true;
                        _retAssinatura.ErrorMessage = "Não foi possível encontrar a Assinatura!!!";
                    }
                }
            }
            else
            {
                _retAssinatura.ErrorHash = true;
                _retAssinatura.ErrorMessage = "Código da Assinatura não pode ser Vazio!!!";
            }

            return _retAssinatura;
        }

        public List<TOAssinaturaPMO> Listar(bool _serverLocal)
        {
            PagarMeServiceKey.PagarMeServiceKeyInicializator(_serverLocal);
            List<TOAssinaturaPMO> _listaRet = new List<TOAssinaturaPMO>();

            PagarMeRequest pmrSubscription = PagarMeService.GetDefaultService().
                Subscriptions.BuildFindQuery(new Subscription());
            PagarMeRequest pmrTransaction = PagarMeService.GetDefaultService().
                Transactions.BuildFindQuery(new Transaction());

            var count = Tuple.Create("count", "1000");
            var page = Tuple.Create("page", "1");
            var status = Tuple.Create("status", "paid");

            pmrSubscription.Query.Add(count);
            pmrSubscription.Query.Add(page);

            pmrTransaction.Query.Add(count);
            pmrTransaction.Query.Add(page);
            pmrTransaction.Query.Add(status);

            List<Subscription> _listaSubscription = PagarMeService.GetDefaultService().
               Subscriptions.FinishFindQuery(pmrSubscription.Execute()).ToList();
            List<Transaction> _listaTransactions = PagarMeService.GetDefaultService().
                Transactions.FinishFindQuery(pmrTransaction.Execute()).ToList();

            if ((_listaSubscription != null) && (_listaSubscription.Count > 0))
            {
                foreach (Subscription item in _listaSubscription)
                {
                    _listaRet.Add(PopularAsinaturaTO(item));
                }

                if ((_listaTransactions != null) && (_listaTransactions.Count > 0))
                {
                    foreach (Transaction item in _listaTransactions)
                    {
                        if (Funcoes.Funcoes.ConvertePara.Int(item.SubscriptionId) <= 0)
                        {
                            _listaRet.Add(PopularTransacaoTO(item)); 
                        }
                    }
                }
            }
            else
            {
                _listaRet = null;
            }

            return _listaRet.OrderBy(o => o.Id).ToList();
        }

        private TOAssinaturaPMO PopularAsinaturaTO(Subscription assinatura)
        {
            TOAssinaturaPMO _ret = new TOAssinaturaPMO();

            if ((assinatura != null) && (assinatura.Id != ""))
            {
                _ret.IdPlan = assinatura.Plan.Id;
                _ret.Amount = assinatura.Plan.Amount;
                _ret.Days = assinatura.Plan.Days;
                _ret.NamePlan = assinatura.Plan.Name;
                _ret.DateCreatedPlan = assinatura.Plan.DateCreated;
                _ret.Charges = assinatura.Charges;

                _ret.Id = assinatura.Id;
                _ret.Status = Funcoes.Funcoes.ConvertePara.String(assinatura.Status);
                _ret.DateCreated = assinatura.DateCreated;
                _ret.DateUpdated = assinatura.DateUpdated;
                _ret.CurrentPeriodStart = assinatura.CurrentPeriodStart;
                _ret.CurrentPeriodEnd = assinatura.CurrentPeriodEnd;
                _ret.SubscriptionType = "Subscription";

                _ret.CardHolderName = assinatura.Card.HolderName;
                _ret.CardNumber = assinatura.CardNumber;
                _ret.CardFirstDigits = assinatura.Card.FirstDigits;
                _ret.CardLastDigits = assinatura.CardLastDigits;
                _ret.CardBrand = Funcoes.Funcoes.ConvertePara.String(
                    assinatura.Card.Brand);
                _ret.CardCvv = assinatura.CardCvv;
                _ret.CardExpirationDate = assinatura.CardExpirationDate;

                _ret.IdCustomer = assinatura.Customer.Id;
                _ret.TypeCustomer = Funcoes.Funcoes.ConvertePara.Int(
                    assinatura.Customer.Type);
                _ret.TypeEntityCustomer = 0;
                _ret.DocumentNumberCustomer = assinatura.Customer.DocumentNumber;
                _ret.DocumentTypeCustomer = Funcoes.Funcoes.ConvertePara.String(
                    assinatura.Customer.DocumentType);
                _ret.NameCustomer = assinatura.Customer.Name;
                _ret.EmailCustomer = assinatura.Customer.Email;

                if ((assinatura.Customer.PhoneNumbers != null) && 
                    (assinatura.Customer.PhoneNumbers[0] != "") &&
                    (assinatura.Customer.PhoneNumbers[0].Length >= 13))
                {
                    _ret.DdiCustomer = assinatura.Customer.PhoneNumbers[0].Substring(0, 3);
                    _ret.DddCustomer = "0" + assinatura.Customer.PhoneNumbers[0].Substring(2, 2);
                    _ret.PhoneCustomer = assinatura.Customer.PhoneNumbers[0].Substring(5,
                        assinatura.Customer.PhoneNumbers[0].Length - 5);
                }
                else
                {
                    _ret.DdiCustomer = "";
                    _ret.DddCustomer = "";
                    _ret.PhoneCustomer = "";
                }

                _ret.BirthdayCustomer = assinatura.Customer.Birthday;
                _ret.GenderCustomer = Funcoes.Funcoes.ConvertePara.String(
                    assinatura.Customer.Gender);
            }
            else
            {
                _ret = null;
            }

            return _ret;
        }

        private TOAssinaturaPMO PopularTransacaoTO(Transaction transacao)
        {
            TOAssinaturaPMO _ret = new TOAssinaturaPMO();

            if ((transacao != null) && (transacao.Id != ""))
            {
                _ret.Id = transacao.Id;
                _ret.Status = Funcoes.Funcoes.ConvertePara.String(transacao.Status);
                _ret.DateCreated = transacao.DateCreated;
                _ret.DateUpdated = transacao.DateUpdated;
                _ret.SubscriptionType = "Transaction";

                _ret.IdPlan = "";
                _ret.Amount = transacao.Amount;
                _ret.Days = 0;
                _ret.NamePlan = "";
                _ret.DateCreatedPlan = null;
                _ret.Charges = 0;

                _ret.CardHolderName = transacao.Card.HolderName;
                _ret.CardNumber = transacao.CardNumber;
                _ret.CardFirstDigits = transacao.Card.FirstDigits;
                _ret.CardLastDigits = transacao.CardLastDigits;
                _ret.CardBrand = Funcoes.Funcoes.ConvertePara.String(
                    transacao.Card.Brand);
                _ret.CardCvv = transacao.CardCvv;
                _ret.CardExpirationDate = transacao.CardExpirationDate;

                _ret.IdCustomer = transacao.Customer.Id;
                _ret.TypeCustomer = Funcoes.Funcoes.ConvertePara.Int(
                    transacao.Customer.Type);
                _ret.TypeEntityCustomer = 0;
                _ret.DocumentNumberCustomer = transacao.Customer.DocumentNumber;
                _ret.DocumentTypeCustomer = Funcoes.Funcoes.ConvertePara.String(
                    transacao.Customer.DocumentType);
                _ret.NameCustomer = transacao.Customer.Name;
                _ret.EmailCustomer = transacao.Customer.Email;

                if ((transacao.Customer.PhoneNumbers != null) && 
                    (transacao.Customer.PhoneNumbers[0] != "") &&
                    (transacao.Customer.PhoneNumbers[0].Length >= 13))
                {
                    _ret.DdiCustomer = transacao.Customer.PhoneNumbers[0].Substring(0, 3);
                    _ret.DddCustomer = "0" + transacao.Customer.PhoneNumbers[0].Substring(2, 2);
                    _ret.PhoneCustomer = transacao.Customer.PhoneNumbers[0].Substring(5,
                        transacao.Customer.PhoneNumbers[0].Length - 5);
                }
                else
                {
                    _ret.DdiCustomer = "";
                    _ret.DddCustomer = "";
                    _ret.PhoneCustomer = "";
                }

                _ret.BirthdayCustomer = transacao.Customer.Birthday;
                _ret.GenderCustomer = Funcoes.Funcoes.ConvertePara.String(
                    transacao.Customer.Gender);
            }
            else
            {
                _ret = null;
            }

            return _ret;
        }
    }
}
