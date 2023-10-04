namespace RDF.Arcana.API.Features.Client.Direct.Exceptions;

public class StoreTypeNotFoundException : Exception
{
    public StoreTypeNotFoundException(int storeTypeId)
        : base($"StoreType with ID \"{storeTypeId}\" was not found.")
    {
    }
}

public class BookingCoverageNotFoundException : Exception
{
    public BookingCoverageNotFoundException(int bookingCoverageId)
        : base($"BookingCoverage with ID \"{bookingCoverageId}\" was not found.")
    {
    }
}

public class ModeOfPaymentNotFoundException : Exception
{
    public ModeOfPaymentNotFoundException(int modeOfPaymentId)
        : base($"ModeOfPayment with ID \"{modeOfPaymentId}\" was not found.")
    {
    }
}

public class TermsNotFoundException : Exception
{
    public TermsNotFoundException(int termsId)
        : base($"Terms with ID \"{termsId}\" was not found.")
    {
    }
}

