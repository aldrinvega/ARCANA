using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Client.Direct;

public class DirectRegistrationErrors
{
    public static Error NoAttachmentFound() => new Error("Attachments.NoAttachmentsFound", "Please insert attachments");
}