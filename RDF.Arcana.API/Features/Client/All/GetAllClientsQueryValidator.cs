using FluentValidation;

namespace RDF.Arcana.API.Features.Client.All;

public class GetAllClientsQueryValidator : AbstractValidator<GetAllClients.GetAllClientsQuery>
{
    public GetAllClientsQueryValidator()
    {
        RuleFor(x => x.RegistrationStatus)
            .Must(BeAValidStatus)
            .WithMessage(
                "Invalid value for RegistrationStatus. Only 'Approved', 'Under review', and 'Rejected' are permitted.");
    }

    private static bool BeAValidStatus(string status)
    {
        return status is "Approved" or "Under review" or "Rejected";
    }
}