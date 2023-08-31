using MySqlX.XDevAPI;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class AuthorizedRepresentative : BaseEntity
{
    public int ClientId { get; set; }
    public string AuthorizedRepresentativeName { get; set; }
    public string AuthorizedRepresentativePosition { get; set; }
    public string TitleAuthorizedSignatory { get; set; }
    public string AuthorizationLetter { get; set; }
    public string RepresentativeValidId { get; set; }

    public virtual User User { get; set; }
    public Client Client { get; set; }
}