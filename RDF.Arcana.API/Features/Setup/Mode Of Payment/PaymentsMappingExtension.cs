using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Mode_Of_Payment;

public static class PaymentsMappingExtension
{
    public static GetAllModeOfPaymentsAsync.GetAllModeOfPaymentsAsyncResult
        ToGetModeOfPaymentAsyncResult(this ModeOfPayment modeOfPayment)
    {
        return new GetAllModeOfPaymentsAsync.GetAllModeOfPaymentsAsyncResult
        {
            Id = modeOfPayment.Id,
            ModeOfPayments = modeOfPayment.Payment,
            AddedBy = modeOfPayment.AddedByUser.Fullname
        };
    }
}