using RDF.Arcana.API.Common.Pagination;

namespace RDF.Arcana.API.Features.Sales_Management.Online_Payment;

public class GetOnlinePayment
{
    public class GetOnlinePaymentQuery : UserParams, IRequest<PagedList<GetOnlinePaymentResult>>
    {
        public string Search { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetOnlinePaymentResult 
    {
        public int Id { get; set; }
        public string OnlinePlatform { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public int AddedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
