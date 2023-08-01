using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Term_Days;

public static class TermDaysMappingExtension
{
    public static GetTermDaysAsync.GetTermDaysAsyncResult
        ToGetTermDaysAsyncResult(this TermDays termDays)
    {
        return new GetTermDaysAsync.GetTermDaysAsyncResult
        {
            Id = termDays.Id,
            Days = termDays.Days,
            AddedBy = termDays.AddedByUser.Fullname,
            CreateAt = termDays.CreateAt,
            UpdatedAt = termDays.UpdatedAt,
            IsActive = termDays.IsActive
        };
    }
}