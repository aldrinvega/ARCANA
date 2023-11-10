using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class TermOptions : BaseEntity
    {
        public int TermsId { get; set; }
        public int? CreditLimit { get; set; }
        public int? TermDaysId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AddedBy { get; set; }
        public Clients Clients { get; set; }
        public User AddedByUser { get; set; }
        public TermDays TermDays { get; set; }
        public Terms Terms { get; set; }
    }
}