using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProjectName.Contract.Model.Data;
using ProjectName.Contract.Model.Request;
using ProjectName.Contract.Model.Response;
using ProjectName.Shared.AppSettings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ProjectName.Shared.Utils.Security
{
    public class AccessToken
    {
        private SecurityConfig _securityConfig;

        public AccessToken(IOptions<SecurityConfig> securityConfig)
        {
            _securityConfig = securityConfig.Value;

        }
        // public string GetToken (LoginResponse loginRespose, List<Permission> permissions) {

        //     return BuildToken (loginRespose, permissions);

        // }

        // private string BuildToken (LoginResponse loginRespose, List<Permission> permissions) {

        //     var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_securityConfig.JwtKey));
        //     var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);
        //     List<Claim> claimList = new List<Claim> ();
        //     claimList.Add (new Claim ("Name", loginRespose.UserInfo.UserName));
        //     claimList.Add (new Claim ("Id", (loginRespose.UserInfo.Id).ToString ()));
        //     claimList.Add (new Claim ("RoleId", (loginRespose.UserInfo.RoleId).ToString ()));
        //     claimList.Add (new Claim ("FirstName", (loginRespose.UserInfo.FirstName).ToString ()));
        //     claimList.Add (new Claim ("LastName", (loginRespose.UserInfo.LastName).ToString ()));
        //     claimList.Add (new Claim ("EmailId", (loginRespose.UserInfo.EmailAddress.ToString ())));
        //     claimList.Add (new Claim ("UserId", (loginRespose.UserInfo.UserId.ToString ())));

        //     if (permissions != null) {
        //         foreach (Permission permission in permissions) {
        //             claimList.Add (new Claim (permission.PermissionName, ""));
        //         }

        //     }



        //     if(permissions!=null && loginRespose!=null && loginRespose.UserInfo!=null && loginRespose.UserInfo.IsTorrentUser){

        //         permissions.Add(new Permission(1000,"TorrentUser"));
        //     }

        //     var token = new JwtSecurityToken (
        //         issuer: "ProjectName.com",
        //         audience: "ProjectName.com",
        //         claims : claimList,
        //         expires : DateTime.UtcNow.AddDays(1),
        //         signingCredentials : creds);

        //     return new JwtSecurityTokenHandler ().WriteToken (token);
        // }
    }
}