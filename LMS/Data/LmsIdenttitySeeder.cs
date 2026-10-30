using LMS.Models;
using Microsoft.AspNetCore.Identity;

namespace LMS.Data
{
    public static class LmsIdentitySeeder
    {
        public static async Task SeedAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<LmsUser> userManager)
        {
            string[] roles =
            {
                LmsRoles.SuperAdmin,
                LmsRoles.Admin,
                LmsRoles.User
            };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }

            const string password = "Abcd1234@";

            await CreateUserAsync(
                userManager,
                "superadmin",
                "superadmin@lms.local",
                password,
                LmsRoles.SuperAdmin);

            await CreateUserAsync(
                userManager,
                "admin",
                "admin@lms.local",
                password,
                LmsRoles.Admin);

            await CreateUserAsync(
                userManager,
                "user",
                "user@lms.local",
                password,
                LmsRoles.User);
        }

        private static async Task CreateUserAsync(
            UserManager<LmsUser> userManager,
            string userName,
            string email,
            string password,
            string role)
        {
            var lmsUser =
                await userManager.FindByNameAsync(userName);

            if (lmsUser is null)
            {
                lmsUser = new LmsUser
                {
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true
                };

                var createResult =
                    await userManager.CreateAsync(lmsUser, password);

                if (!createResult.Succeeded)
                {
                    string errors = string.Join(
                        ", ",
                        createResult.Errors.Select(error =>
                            error.Description));

                    throw new InvalidOperationException(
                        $"Không thể tạo tài khoản {userName}: {errors}");
                }
            }

            if (!await userManager.IsInRoleAsync(lmsUser, role))
            {
                await userManager.AddToRoleAsync(lmsUser, role);
            }
        }
    }
}