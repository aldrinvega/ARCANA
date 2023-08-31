using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ClientDocuments : BaseEntity
{
    public int ClientId { get; set; }
    public Clients Clients { get; set; }
    public string DocumentType { get; set; }
    public string DocumentPath { get; set; }
}