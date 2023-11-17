using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Freebies;

public class FreebieErrors
{
    public static Error CannotBeRepeated() => new Error("Freebie.CannotBeRepeated", "Items cannot be repeated.");

    public static Error Exceed5Items() =>
        new Error("Freebie.Exceed5Items", "Freebie request is not exceeding to 5 items");

    public static Error AlreadyRequested(string itemDescription) => new Error("Freebie.AlreadyRequested",
        $"{itemDescription} has already been requested.");

    public static Error NoFreebieFound() => new Error("Freebie.NoFreebieFound", "No freebie found");
}