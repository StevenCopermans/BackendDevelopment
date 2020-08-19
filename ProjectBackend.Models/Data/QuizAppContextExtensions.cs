using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBackend.Models.Data
{
    public static class QuizAppContextExtensions
    {
        public async static Task SeedRoles(RoleManager<IdentityRole> RoleMgr)
        {
            IdentityResult roleResult;
            string[] roleNames = { "Admin", "Default"};
            foreach (var roleName in roleNames)
            {
                //verhinderen dat continue dezelfde rollen worden toegvoegd.
                var roleExist = await RoleMgr.RoleExistsAsync(roleName);
                if (!roleExist) { 
                    roleResult = await RoleMgr.CreateAsync(new IdentityRole(roleName));
                } 
            } 
        }

        public async static Task SeedUsers(UserManager<IdentityUser> userMgr)
        {
            var user = new IdentityUser("Docent@MCT");
            var userResult = await userMgr.CreateAsync(user, "Docent@1");
            var roleResult = await userMgr.AddToRoleAsync(user, "Admin");

            if (!userResult.Succeeded || !roleResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to build user and roles");
            }
        }
    }
}
