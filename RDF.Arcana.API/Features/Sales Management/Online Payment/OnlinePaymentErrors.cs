using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Sales_Management.Online_Payment;

public class OnlinePaymentErrors
{
    public static Error ExistingOnlinePlatform() => new("Existing", "This Online Platform already existed");
    public static Error NotFound() => new("NotFound", "Online Payment Not Found");
}
