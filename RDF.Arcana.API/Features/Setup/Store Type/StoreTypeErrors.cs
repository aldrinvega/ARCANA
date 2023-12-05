using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Store_Type;

public static class StoreTypeErrors
{
    public static Error NotFound() =>
        new Error("StoreType.NotFound", "Store Type is not exist");

    public static Error AlreadyExist(string storeType) =>
        new Error("StoreType.AlreadyExist", $"{storeType} is already exist");
}