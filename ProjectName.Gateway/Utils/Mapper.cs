using System.Security.Claims;
using ProjectName.Contract.Model.Data;
using Microsoft.AspNetCore.Http;

namespace ProjectName.Gateway.Utils
{
    public static class Mapper
    {
        // public static PrincipalUser PrincipalUser(ClaimsPrincipal claimsPrincipal)
        // {
        //     PrincipalUser principalUser = new PrincipalUser();

        //     principalUser.Name = claimsPrincipal.FindFirst("Name")?.Value;
        //     principalUser.RoleId = int.Parse(claimsPrincipal.FindFirst("RoleId")?.Value);
        //     principalUser.Id = int.Parse(claimsPrincipal.FindFirst("Id")?.Value);
        //     principalUser.EmailId = claimsPrincipal.FindFirst("EmailId")?.Value;
        //     principalUser.FirstName = claimsPrincipal.FindFirst("FirstName")?.Value;
        //     principalUser.LastName = claimsPrincipal.FindFirst("LastName")?.Value;
        //     principalUser.UserId = claimsPrincipal.FindFirst("UserId")?.Value;

        //     return principalUser;
        // }

    }
}