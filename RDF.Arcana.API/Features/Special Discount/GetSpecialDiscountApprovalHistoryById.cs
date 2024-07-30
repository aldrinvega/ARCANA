﻿using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;
using static RDF.Arcana.API.Features.Client.All.GetClientApprovalHistory;

namespace RDF.Arcana.API.Features.Special_Discount;

[Route("api/special-discount"), ApiController]
public class GetSpecialDiscountApprovalHistoryById : ControllerBase
{
    private readonly IMediator _mediator;

    public GetSpecialDiscountApprovalHistoryById(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("approval-history/{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var query = new GetSpecialDiscountApprovalHistoryByIdQuery
            {
                Id = id
            };

            var result = await _mediator.Send(query);

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

    public class GetSpecialDiscountApprovalHistoryByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
    }

    public class SpecialDiscountApprovalHistoryResult
    {
        public IEnumerable<ApprovalHistory> ApprovalHistories { get; set; }
        public IEnumerable<UpdateHistory> UpdateHistories { get; set; }
        public IEnumerable<RequestApprovers> Approvers { get; set; }

        public class ApprovalHistory
        {
            public string Module { get; set; }
            public string Approver { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Status { get; set; }
            public int? Level { get; set; }
            public string Reason { get; set; }
        }

        public class UpdateHistory
        {
            public string Module { get; set; }
            public DateTime UpdatedAt { get; set; }
        }

        public class RequestApprovers
        {
            public string Name { get; set; }
            public int Level { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetSpecialDiscountApprovalHistoryByIdQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetSpecialDiscountApprovalHistoryByIdQuery request, CancellationToken cancellationToken)
        {
            var specialDiscount = await _context
                .SpecialDiscounts
                .Include(r => r.Request)
                .ThenInclude(up => up.UpdateRequestTrails)
                .Include(r => r.Request)
                .ThenInclude(x => x.Approvals)
                .ThenInclude(x => x.Approver)
                .Include(x => x.Request)
                .ThenInclude(x => x.RequestApprovers)
                .ThenInclude(x => x.Approver)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (specialDiscount is null)
            {
                return SpecialDiscountErrors.NotFound();
            }

            var approverLevels = specialDiscount.Request.RequestApprovers.ToDictionary(x => x.ApproverId, x => x.Level);

            var result = new SpecialDiscountApprovalHistoryResult
            {
                ApprovalHistories = specialDiscount.Request.Approvals?.OrderByDescending(a => a.CreatedAt)
                    .Select(a => new SpecialDiscountApprovalHistoryResult.ApprovalHistory
                    {
                        Module = a.Request.Module,
                        Approver = a.Approver.Fullname,
                        CreatedAt = a.CreatedAt,
                        Status = a.Status,
                        Level = approverLevels.ContainsKey(a.ApproverId) ? approverLevels[a.ApproverId] : (int?)null,
                        Reason = a.Reason
                    }),
                UpdateHistories = specialDiscount.Request.UpdateRequestTrails?.Select(uh => new SpecialDiscountApprovalHistoryResult.UpdateHistory
                {
                    Module = uh.ModuleName,
                    UpdatedAt = uh.UpdatedAt
                }),
                Approvers = specialDiscount.Request.RequestApprovers.Select(x => new SpecialDiscountApprovalHistoryResult.RequestApprovers
                {
                    Name = x.Approver.Fullname,
                    Level = x.Level
                })
            };

            return Result.Success(result);
        }
    }
}
