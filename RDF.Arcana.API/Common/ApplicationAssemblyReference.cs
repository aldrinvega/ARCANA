using System.Reflection;

namespace RDF.Arcana.API.Common;

public static class ApplicationAssemblyReference
{
    public static Assembly Assembly => typeof(Program).Assembly;
}