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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApprover([FromRoute] int id, [FromBody] UpdateApproversPerModuleByRangeCommand command)
        {
            try
            {
                command.Id = id;
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
            public int Id { get; set; }
            public int UserId { get; set; }
            public string ModuleName { get; set; }
            public decimal MinValue { get; set; }
            public decimal MaxValue { get; set; }
            public bool IsActive { get; set; }
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
                var approverToUpdate = await _context.ApproverByRange.FindAsync(request.Id);
                if (approverToUpdate == null)
                {
                    return ApprovalErrors.ApproverNotFound();
                }

                if (request.IsActive != approverToUpdate.IsActive)
                {
                    approverToUpdate.IsActive = request.IsActive;
                    await _context.SaveChangesAsync(cancellationToken);
                    return Result.Success();
                }

                var existingApprover = await _context.ApproverByRange
                    .AnyAsync(a => a.UserId == request.UserId
                    && a.ModuleName == request.ModuleName
                    && a.MinValue == request.MinValue
                    && a.MaxValue == request.MaxValue,
                    cancellationToken);

                if (existingApprover)
                {
                    return ApprovalErrors.ApproverExist();
                }

                if (request.MinValue >= request.MaxValue)
                {
                    return ApprovalErrors.MinMaxError();
                }

                approverToUpdate.UserId = request.UserId;
                approverToUpdate.ModuleName = request.ModuleName;
                approverToUpdate.MinValue = request.MinValue;
                approverToUpdate.MaxValue = request.MaxValue;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
