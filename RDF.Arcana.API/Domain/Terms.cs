using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class Terms : BaseEntity
    {
        public string TermType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public int AddedBy { get; set; }
        public User AddedByUser { get; set; }
        public virtual TermOptions TermOptions { get; set; }
    }
}