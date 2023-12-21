using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Cluster;

public class ClusterErrors
{
    public static Error AlreadyTagged() => new ("ClusterError.AlreadyTagged", "User already tagged to this cluster");
    public static Error AlreadyExist() => new("ClusterError.AlreadyExist", "Cluster already exist");
    public static Error NotFound() => new Error("ClusterError.NotFound", "No cluster found");
    public static Error InUse() => new("ClusterError.InUse", "CLuster is in use");
}