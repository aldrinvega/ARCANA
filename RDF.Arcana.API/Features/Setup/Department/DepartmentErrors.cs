using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Department;

public class DepartmentErrors
{
    public static Error AlreadyExist(string department) =>
        new Error("Department.AlreadyExist", $"{department} is already exist");

    public static Error NotFound() => new Error("Department.NotFound", "No department found");
}