using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Mode_Of_Payment;

public static class ModeOfPaymentErrors
{
    public static Error NotFound() =>
        new Error("ModeOfPayment.NotFound", "Mode of Payment is not exist");

    public static Error AlreadyExist(string modeOfPayment) =>
        new Error("ModeOfPayment.NotFound", $"{modeOfPayment} is already exist");
}