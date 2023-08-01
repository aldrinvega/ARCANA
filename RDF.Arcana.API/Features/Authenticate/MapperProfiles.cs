using AutoMapper;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Authenticate;

namespace MineralWaterMonitoring.Features.Authenticate;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<User, AuthenticateUser.AuthenticateUserResult>();
    }
}