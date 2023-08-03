using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using NNPEFWEB.Models;
using System.Collections.Generic;
using System.Linq;
using NNPEFWEB.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data.SqlClient;
using NNPEFWEB.Enum;

namespace NNPEFWEB.Data
{
    public static class SeedCustomData
    {
        public static void Initialize(IConfiguration configuration)
        {
            var hasher = new PasswordHasher<User>();
            string ConnectionString = configuration.GetConnectionString("DefaultConnection");
            string email = "hicad123@hicad.com";
            string password = "Password@1234";
            string passwordHash = hasher.HashPassword(null, password);
            string FirstName = "System";
            string LastName = "Admin";
            string username = "hicad123@hicad.com";


            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand(ApplicationConstant.sp_defaultUsers, sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@email",email));
                    cmd.Parameters.Add(new SqlParameter("@passwordHash",passwordHash));
                    cmd.Parameters.Add(new SqlParameter("@firstname",FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastname",LastName));
                    cmd.Parameters.Add(new SqlParameter("@username", username));

                    sqlcon.Open();
                    cmd.ExecuteNonQuery();
                }
         }




            //if (!context.Users.Any(x => x.Email == "hicad123@hicad.com"))
            //{
            //    var hasher = new PasswordHasher<User>();
            //    var user = new User()
            //    {
            //        Email = "hicad123@hicad.com",
            //        NormalizedEmail = "hicad123@hicad.com",
            //        FirstName = "System",
            //        LastName = "Admin",
            //        UserName = "hicad123@hicad.com",
            //        NormalizedUserName = "hicad123@hicad.com",
            //        PasswordHash = hasher.HashPassword(null, "Password@1234"),
            //        EmailConfirmed = true,
            //        ResetPasswordCode = "yes",
            //        IsActive = true,
            //        SecurityStamp = Guid.NewGuid().ToString()
            //    };

            //    context.Users.Add(user);
            //    context.SaveChangesAsync();
            //}
        }

      
    }
}
