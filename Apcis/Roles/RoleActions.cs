using Apcis.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Apcis.Roles
{

    internal class RoleActions
    {
        private Task<bool> AddRole(ApplicationUser appUser, String role)
        {
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            using (var db = new ApplicationDbContext())
            using (var roleStore = new RoleStore<IdentityRole>())
            using (var roleManager = new RoleManager<IdentityRole>(roleStore))
            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)))
            {
                if (null == userManager.FindByEmail(appUser.Email))
                {
                    return (Task.FromResult(false));
                }
                if (!roleManager.RoleExists(role))
                {
                    IdRoleResult = roleManager.Create(new IdentityRole { Name = role });
                }
             
                if (!userManager.IsInRole(userManager.FindByEmail(appUser.Email).Id, role))
                {
                    IdUserResult = userManager.AddToRole(userManager.FindByEmail(appUser.Email).Id, role);
                }
                return (Task.FromResult(true));
            }
        }

        internal Task<bool> AddAdmin(ApplicationUser appUser)
        {
            return (AddRole(appUser, "admin"));
        }

        internal Task<bool> AddPublic(ApplicationUser appUser)
        {
            return (AddRole(appUser, "public"));
        }
    }
}