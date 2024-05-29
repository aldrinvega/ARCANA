
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Sales_Management.Online_Payment
{
    [Route("api/online-payment"), ApiController]
    public class GetOnlinePayment : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetOnlinePayment(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("page")]
        public async Task<IActionResult> Get([FromQuery] GetOnlinePaymentQuery query)
        {
            try
            {
                var onlinePayments = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    onlinePayments.CurrentPage,
                    onlinePayments.PageSize,
                    onlinePayments.TotalCount,
                    onlinePayments.TotalPages,
                    onlinePayments.HasPreviousPage,
                    onlinePayments.HasNextPage);

                var result = new
                {
                    onlinePayments,
                    onlinePayments.CurrentPage,
                    onlinePayments.PageSize,
                    onlinePayments.TotalCount,
                    onlinePayments.TotalPages,
                    onlinePayments.HasPreviousPage,
                    onlinePayments.HasNextPage
                };
                var successResult = Result.Success(result);
                return Ok(successResult);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        public class GetOnlinePaymentQuery : UserParams, IRequest<PagedList<GetOnlinePaymentResult>>
        {
            public string Search { get; set; }
            public bool? Status { get; set; }
            public string OnlinePaymentStatus { get; set; }
        }

        public class GetOnlinePaymentResult
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public string OnlinePaymentName { get; set; }
            public string AccountName { get; set; }
            public string AccountNumber { get; set; }
            public decimal PaymentAmount { get; set; }
            public string ReferenceNumber { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public int AddedBy { get; set; }
            public int? ModifiedBy { get; set; }
            public bool IsActive { get; set; }
            public string Status { get; set; }
            public string Remarks { get; set; }
        }
      

        public class Handler : IRequestHandler<GetOnlinePaymentQuery, PagedList<GetOnlinePaymentResult>>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public Task<PagedList<GetOnlinePaymentResult>> Handle(GetOnlinePaymentQuery request, CancellationToken cancellationToken)
            {
                IQueryable<OnlinePayment> onlinePayments = _context.OnlinePayments
                    .Include(cl => cl.Client)
                    .Include(u => u.AddedByUser);                

                if (!string.IsNullOrEmpty(request.Search)) 
                {
                    onlinePayments = onlinePayments.Where(op =>
                        op.Client.Fullname.Contains(request.Search) ||
                        op.Client.BusinessName.Contains(request.Search) || 
                        op.OnlinePaymentName.Contains(request.Search));                    
                }

                if (request.Status is not null)
                {
                    onlinePayments = onlinePayments.Where(op => op.IsActive == request.Status);
                }

                if (!string.IsNullOrEmpty(request.OnlinePaymentStatus) && request.OnlinePaymentStatus != Status.Voided) 
                {
                    onlinePayments = onlinePayments.Where(op => op.Status == request.OnlinePaymentStatus &&
                        op.Status != Status.Voided);
                }
                
                if (request.OnlinePaymentStatus == Status.Voided)
                {
                    var voided = onlinePayments.Where(ap => ap.Status == Status.Voided)
                        .Select(op => new GetOnlinePaymentResult
                        {
                            Id = op.Id,
                            ClientId = op.ClientId,
                            OnlinePaymentName = op.OnlinePaymentName,
                            AccountName = op.AccountName,
                            AccountNumber = op.AccountName,
                            PaymentAmount = op.PaymentAmount,
                            ReferenceNumber = op.ReferenceNumber,
                            CreatedAt = op.CreatedAt,
                            UpdatedAt = op.UpdatedAt,
                            AddedBy = op.AddedBy,
                            ModifiedBy = op.ModifiedBy,
                            IsActive = op.IsActive,
                            Status = op.Status,
                            Remarks = op.Remarks
                        });

                    return PagedList<GetOnlinePaymentResult>.CreateAsync(voided, request.PageNumber, request.PageSize);
                }

                var result = onlinePayments.Select(op => new GetOnlinePaymentResult 
                {
                    Id = op.Id,
                    ClientId = op.ClientId,
                    OnlinePaymentName = op.OnlinePaymentName,
                    AccountName = op.AccountName,
                    AccountNumber = op.AccountName,
                    PaymentAmount = op.PaymentAmount,
                    ReferenceNumber = op.ReferenceNumber,
                    CreatedAt = op.CreatedAt,
                    UpdatedAt = op.UpdatedAt,
                    AddedBy = op.AddedBy,
                    ModifiedBy = op.ModifiedBy,
                    IsActive = op.IsActive,
                    Status = op.Status,
                    Remarks = op.Remarks
                });

                return PagedList<GetOnlinePaymentResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
