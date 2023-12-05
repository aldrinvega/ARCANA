using System.Runtime.InteropServices.JavaScript;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Users;

public class UserErrors
{
    public static Error UsernameAlreadyExist(string username) =>
        new Error("User.UsernameAlreadyExist", $"{username} is already exist");

    public static Error UserAlreadyExist() => new Error("User.AlreadyExist", "This user is already exist");
    public static Error NotFound() => new Error("User.NotFound", "User not found");
    public static Error OldPasswordIncorrect() => new Error("User.OldPassword", "Old password is incorrect");
}