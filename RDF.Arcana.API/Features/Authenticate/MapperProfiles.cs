using AutoMapper;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Authenticate;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<User, AuthenticateUser.AuthenticateUserResult>();
    }
}