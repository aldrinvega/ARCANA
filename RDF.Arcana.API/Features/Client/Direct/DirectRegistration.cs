//using Microsoft.AspNetCore.Mvc;
//using RDF.Arcana.API.Common;
//using RDF.Arcana.API.Data;
//using RDF.Arcana.API.Domain.New_Doamin;
//using RDF.Arcana.API.Features.Client.Direct;
//using System.ComponentModel.DataAnnotations;

//namespace RDF.Arcana.API.Features.Client.Direct
//{
//    public class DirectRegistrationCommand : IRequest<Unit>
//    {
//        [Required]
//        public string OwnersName { get; set; }
//        [Required]
//        public string OwnersAddress { get; set; }
//        [Required]
//        public string PhoneNumber { get; set; }
//        [Required]
//        public string BusinessName { get; set; }
//        [Required]
//        public int StoreTypeId { get; set; }
//        public int AddedBy { get; set; }
//    }

//    public class DirectRegistrationResult
//    {
//        public int Id { get; set; }
//        public string OwnersName { get; set; }
//        public string OwnersAddress { get; set; }
//        public string PhoneNumber { get; set; }
//        public string BusinessName { get; set; }
//        public string StoreTypeName { get; set; }
//        public int AddedBy { get; set; }
//    }

//    public class Handler : IRequestHandler<DirectRegistrationCommand, Unit>
//    {

//        private const string APPROVED_STATUS = "Approved";
//        private const string PROSPECT_TYPE = "Prospect";
//        private const string APPROVER_APPROVAL = "Approver Approval";

//        private readonly DataContext _context;

//        public Handler(DataContext context)
//        {
//            _context = context;
//        }

//        public async Task<Unit> Handle(DirectRegistrationCommand request, CancellationToken cancellationToken)
//        {
//            var existingClient = await _context.Clients.FirstOrDefaultAsync(
//                x => x.Fullname == request.OwnersName &&
//                x.StoreType.Id == request.StoreTypeId &&
//                x.BusinessName == request.BusinessName
//                );

//            if (existingClient != null)
//            {
//                var prospectingClients = new Domain.New_Doamin.Clients
//                {
//                    Fullname = request.OwnersName,
//                    Address = request.OwnersAddress,
//                    PhoneNumber = request.PhoneNumber,
//                    BusinessName = request.BusinessName,
//                    StoreTypeId = request.StoreTypeId,
//                    RegistrationStatus = APPROVED_STATUS,
//                    CustomerType = PROSPECT_TYPE,
//                    IsActive = true,
//                    AddedBy = request.AddedBy
//                };

//                await _context.Clients.AddAsync(prospectingClients, cancellationToken);
//                await _context.SaveChangesAsync(cancellationToken);
//                var approval = new Approvals
//                {
//                    ClientId = prospectingClients.Id,
//                    ApprovalType = APPROVER_APPROVAL,
//                    IsApproved = true,
//                    IsActive = true,
//                };

//                Add the new request to the database
//               await _context.Approvals.AddAsync(approval, cancellationToken);
//                await _context.SaveChangesAsync(cancellationToken);

//            }
//            else
//            {
//                throw new InvalidOperationException($"Client already exist!");
//            }

//            return Unit.Value;

//        }
//    }
//}

//[Route("api/[Directregistration]")]
//[ApiController]
//public class DirectRegistration : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public DirectRegistration(IMediator mediator)
//    {
//        _mediator = mediator;
//    }

//    [HttpPost("DirectRegistration")]
//    public async Task<IActionResult> DirectClientRegistration(DirectRegistrationCommand command)
//    {
//        var response = new QueryOrCommandResult<object>();
//        try
//        {
//            await _mediator.Send(command);
//            response.Success = true;
//            response.Status = StatusCodes.Status200OK;
//            return Ok(response);
//        }
//        catch (Exception ex)
//        {
//            response.Status = StatusCodes.Status404NotFound;
//            response.Messages.Add(ex.Message);
//            return Conflict(response);
//        }
//    }
//}