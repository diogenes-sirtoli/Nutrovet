using System;

namespace BLL
{
    public class TOAssinaturaPMOBll
    {
        public int IdPessoa { get; set; }
        public int IdVigencia { get; set; }
        public DateTime? DtInicial { get; set; }
        public DateTime? DtFinal { get; set; }

        public string IdPlan { get; set; }
        public int? Amount { get; set; }
        public int Days { get; set; }
        public string NamePlan { get; set; }
        public DateTime? DateCreatedPlan { get; set; }
        public int Charges { get; set; }

        public string Id { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? CurrentPeriodStart { get; set; }
        public DateTime? CurrentPeriodEnd { get; set; }
        public string SubscriptionType { get; set; }//Transaction ou Subscription

        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CardFirstDigits { get; set; }
        public string CardLastDigits { get; set; }
        public string CardBrand { get; set; }
        public string CardCvv { get; set; }
        public string CardExpirationDate { get; set; }

        public string IdCustomer { get; set; }
        public int TypeCustomer { get; set; }
        public int TypeEntityCustomer { get; set; }
        public string DocumentNumberCustomer { get; set; }
        public string DocumentTypeCustomer { get; set; }
        public string NameCustomer { get; set; }
        public string EmailCustomer { get; set; }

        public string DdiCustomer { get; set; }
        public string DddCustomer { get; set; }
        public string PhoneCustomer { get; set; }

        public string BirthdayCustomer { get; set; }
        public string GenderCustomer { get; set; }

        public int NationalityCustomer { get; set; }
        public string ContryCustomer { get; set; }
        public string ZipCodeCustomer { get; set; }
        public string StateCustomer { get; set; }
        public string CityCustomer { get; set; }
        public string NeighborhoodCustomer { get; set; }
        public string StreetCustomer { get; set; }
        public string StreetNumberCustomer { get; set; }
        public string ComplementaryCustomer { get; set; }

        public bool Loaded { get; set; }
        public string PostBackURL { get; set; }
        public string ErrorMessage { get; set; }
        public bool ErrorHash { get; set; }
    }
}
