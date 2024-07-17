using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Requests_Approval
{
    [Route("api/UpdateApproverByRange"), ApiController]
    public class UpdateApproversPerModuleByRange : ControllerBase
    {
        private readonly IMediator _mediator;
        public UpdateApproversPerModuleByRange(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("UpdateApproversPerModuleByRange")]
        public async Task<IActionResult> UpdateApprover([FromBody] UpdateApproversPerModuleByRangeCommand command, [FromQuery] string moduleName)
        {
            try
            {
                command.ModuleName = moduleName;
                var result = await _mediator.Send(command);
                if (result.IsFailure)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class UpdateApproversPerModuleByRangeCommand : IRequest<Result>
        {
            public string ModuleName { get; set; }
            public ICollection<ApproverToUpdatePerModuleByRange> Approvers { get; set; }

            public class ApproverToUpdatePerModuleByRange
            {
                public int UserId { get; set; }
                public decimal? MinValue { get; set; }
                public decimal? MaxValue { get; set; }
                public bool IsActive { get; set; }
                public int Level { get; set; }
            }
        }


        public class Handler : IRequestHandler<UpdateApproversPerModuleByRangeCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdateApproversPerModuleByRangeCommand request, CancellationToken cancellationToken)
            {
                var existingApprovers = await _context.ApproverByRange
                    .Where(a => a.ModuleName == request.ModuleName)
                    .ToListAsync(cancellationToken);

                var sentUserIds = request.Approvers.Select(a => a.UserId).ToList();

                // Remove approvers in the database that do not exist in the request
                var approversToRemove = existingApprovers.Where(a => !sentUserIds.Contains(a.UserId)).ToList();
                _context.ApproverByRange.RemoveRange(approversToRemove);

                foreach (var sentApprover in request.Approvers)
                {
                    var existingApprover = existingApprovers.FirstOrDefault(a => a.UserId == sentApprover.UserId);

                    if (existingApprover != null) // If the approver exists in the database, update the approver
                    {
                        if (sentApprover.IsActive != existingApprover.IsActive)
                        {
                            existingApprover.IsActive = sentApprover.IsActive;
                        }

                        if (sentApprover.MinValue >= sentApprover.MaxValue)
                        {
                            return ApprovalErrors.MinMaxError();
                        }

                        if (sentApprover.Level < 1 || sentApprover.Level > 5)
                        {
                            return ApprovalErrors.InvalidLevel();
                        }                        

                        existingApprover.MinValue = sentApprover.MinValue;
                        existingApprover.MaxValue = sentApprover.MaxValue;
                        existingApprover.Level = sentApprover.Level;
                    }
                    else // If the approver does not exist in the database, add the approver
                    {
                        if (sentApprover.MinValue >= sentApprover.MaxValue)
                        {
                            return ApprovalErrors.MinMaxError();
                        }

                        if (sentApprover.Level < 1 || sentApprover.Level > 5)
                        {
                            return ApprovalErrors.InvalidLevel();
                        }                       

                        _context.ApproverByRange.Add(new ApproverByRange
                        {
                            UserId = sentApprover.UserId,
                            ModuleName = request.ModuleName,
                            MinValue = sentApprover.MinValue,
                            MaxValue = sentApprover.MaxValue,
                            Level = sentApprover.Level,
                            IsActive = sentApprover.IsActive
                        });
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
