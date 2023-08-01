namespace RDF.Arcana.API.Features.Setup.Department.Exception;

public class DepartmentAlreadyExistException : System.Exception
{
    public DepartmentAlreadyExistException(string departmentName) : base($"{departmentName} is already exist, try something else"){}
}