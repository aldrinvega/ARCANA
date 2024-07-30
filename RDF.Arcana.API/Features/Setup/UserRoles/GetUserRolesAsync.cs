﻿using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

[Route("api/UserRole")]
[ApiController]
public class GetUserRolesAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetUserRolesAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetUserRoles")]
    public async Task<IActionResult> GetUserRoles([FromQuery] GetUserRolesAsync.GetUserRoleAsyncQuery query)
    {
        try
        {
            var userRoles = await _mediator.Send(query);

            Response.AddPaginationHeader(
                userRoles.CurrentPage,
                userRoles.PageSize,
                userRoles.TotalCount,
                userRoles.TotalPages,
                userRoles.HasPreviousPage,
                userRoles.HasNextPage
            );

            var result = new
            {
                userRoles,
                userRoles.CurrentPage,
                userRoles.PageSize,
                userRoles.TotalCount,
                userRoles.TotalPages,
                userRoles.HasPreviousPage,
                userRoles.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class GetUserRoleAsyncQuery : UserParams, IRequest<PagedList<GetUserRoleAsyncResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public bool? IsTagged { get; set; }

    }

    public class GetUserRoleAsyncResult
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<string> Permissions { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsTagged { get; set; }
        public string User { get; set; }
    }

    public class Handler : IRequestHandler<GetUserRoleAsyncQuery, PagedList<GetUserRoleAsyncResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetUserRoleAsyncResult>> Handle(GetUserRoleAsyncQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.UserRoles> userRoles = _context.UserRoles
                .Include(x => x.AddedByUser)
                .Include(x => x.Users);

            if (!string.IsNullOrEmpty(request.Search))
            {
                userRoles = userRoles.Where(x => x.UserRoleName.Contains(request.Search));
            }

            if (request.IsTagged != null)
            {
                userRoles = request.IsTagged.Value
                    ? userRoles.Where(x => x.Users != null)
                    : userRoles.Where(x => x.Users == null);
            }

            if (request.Status is not null)
            {
                userRoles = userRoles.Where(x => x.IsActive == request.Status);
            }

            var result = userRoles.Select(x => x.ToGetUserRoleAsyncQueryResult());

            return await PagedList<GetUserRoleAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}