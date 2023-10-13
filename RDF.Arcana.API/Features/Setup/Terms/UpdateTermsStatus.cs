using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Terms.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Terms;

public class UpdateTermsStatus
{
    public class UpdateTermsStatusCommand : IRequest<UpdatedTermsResult>
    {
        public int TermsId { get; set; }
    }

    public class UpdatedTermsResult
    {
        public int Id { get; set; }
        public string TermType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateTermsStatusCommand, UpdatedTermsResult>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<UpdatedTermsResult> Handle(UpdateTermsStatusCommand request,
            CancellationToken cancellationToken)
        {
            var existingTerms = await _context.Terms
                .Include(x => x.AddedByUser)
                .FirstOrDefaultAsync(x => x.Id == request.TermsId, cancellationToken);

            if (existingTerms == null)
            {
                throw new TermsDoesNotExistException(request.TermsId);
            }

            existingTerms.IsActive = !existingTerms.IsActive;
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdatedTermsResult
            {
                Id = existingTerms.Id,
                TermType = existingTerms.TermType,
                IsActive = existingTerms.IsActive,
                CreatedAt = existingTerms.CreatedAt,
                UpdatedAt = DateTime.Now,
                AddedBy = existingTerms.AddedByUser.Fullname
            };
        }
    }
}