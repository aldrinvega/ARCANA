using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.UOM;

public static class UomMappingExtension
{
    public static GetUomAsync.GetUomQueryResult
        ToGetUomQueryResult(this Uom uom)
    {
        return new GetUomAsync.GetUomQueryResult
        {
            Id = uom.Id,
            UomCode = uom.UomCode,
            UomDescription = uom.UomDescription,
            CreatedAt = uom.CreatedAt,
            UpdatedAt = uom.UpdatedAt,
            AddedBy = uom.AddedByUser.Fullname,
            ModifiedBy = uom.ModifiedBy,
            IsActive = uom.IsActive
        };
    }
}