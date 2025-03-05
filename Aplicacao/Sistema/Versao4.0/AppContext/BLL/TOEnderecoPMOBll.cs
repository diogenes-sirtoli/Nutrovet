using System;

namespace BLL
{
    public class TOEnderecoPMOBll
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string StreetNumber { get; set; }
        public string Complementary { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string ErrorMessage { get; set; }
    }
}
