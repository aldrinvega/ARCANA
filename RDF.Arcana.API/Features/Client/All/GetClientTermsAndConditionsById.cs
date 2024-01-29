using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

namespace RDF.Arcana.API.Features.Client.All
{
    [Route("api/terms"), ApiController]
    public class GetClientTermsAndConditionsById : ControllerBase
    {

        private readonly IMediator _mediator;

        public GetClientTermsAndConditionsById(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var query = new GetClientTermsAndConditionQuery
                {
                    ClientId = id
                };

                var result = await _mediator.Send(query);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetClientTermsAndConditionQuery : IRequest<Result>
        {
            public int ClientId { get; set; }
        }
        
        public class ClientTermsAndCondition
        {
            public int TermId { get; set; }
            public string Term { get; set; }
            public int? CreditLimit { get; set; }
            public int? TermDays { get; set; }
            public int? TermDaysId { get; set; }
        }

        public class Handler : IRequestHandler<GetClientTermsAndConditionQuery, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetClientTermsAndConditionQuery request, CancellationToken cancellationToken)
            {
                var termsAndConditions = await _context.Clients
                    .Include(x => x.Term)
                    .ThenInclude(x => x.Terms)
                    .Include(x => x.Term)
                .ThenInclude(tt => tt.TermDays)
                .FirstOrDefaultAsync(x => x.Id == request.ClientId && x.RegistrationStatus != Status.Requested);

                if(termsAndConditions is null)
                {
                    return ClientErrors.NotFound();
                }

                var termsAndCondition = new ClientTermsAndCondition
                {
                    TermId = termsAndConditions.Term.TermsId,
                    Term = termsAndConditions.Term.Terms.TermType,
                    CreditLimit = termsAndConditions.Term?.CreditLimit,
                    TermDays = termsAndConditions.Term.TermDays?.Days,
                    TermDaysId = termsAndConditions.Term?.TermDaysId
                };

                return Result.Success(termsAndCondition);
            }
        }
    }
}
