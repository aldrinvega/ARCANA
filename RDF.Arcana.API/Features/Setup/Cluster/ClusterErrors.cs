using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Cluster;

public class ClusterErrors
{
    public static Error AlreadyExist() => new Error("ClusterError.AlreadyExist", "User already tagged to this cluster");
    public static Error NotFound() => new Error("ClusterError.NotFound", "No cluster found");
}