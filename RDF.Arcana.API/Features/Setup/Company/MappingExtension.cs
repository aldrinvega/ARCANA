namespace RDF.Arcana.API.Features.Setup.Company;

public static class MappingExtension
{
    
    public static GetCompaniesAsync.GetCompaniesResult
         ToGetAllCompaniesResult(this Domain.Company company)
    {
        return new GetCompaniesAsync.GetCompaniesResult
        {
            Id = company.Id,
            CompanyName = company.CompanyName,
            AddedBy = company.AddedByUser.Fullname,
            Users = company.Users.Select(x => x.Fullname),
            CreatedAt = company.CreatedAt,
            UpdatedAt = company.UpdatedAt,
            IsActive = company.IsActive
        };
    }
}