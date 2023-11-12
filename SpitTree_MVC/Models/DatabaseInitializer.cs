using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web;

namespace SpitTree_MVC.Models
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<SpitTreeDbContext>
    {
        protected override void Seed(SpitTreeDbContext context)
        {
            if (!context.Users.Any())
            {
                //*********************************************************
                //create a few roles and store them in AspNetRoles tables
                //*********************************************************

                //create a roleManager object that will allow us to create roles and store them in the database
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                //if the Admin role doesn't exist
                if (!roleManager.RoleExists("Admin"))
                {
                    //create an Admin role
                    roleManager.Create(new IdentityRole("Admin"));
                }

                //if the Member role doesn't exist
                if (!roleManager.RoleExists("Member"))
                {
                    //create a Member role
                    roleManager.Create(new IdentityRole("Member"));
                }

                //save the new roles to the database
                context.SaveChanges();
            }

            //*********************************************************
            //create some users and assign them to different roles
            //*********************************************************

            //the userManager object allows creating users and store them in the database
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

            //if user with the admin@spittree.com username doesn't exist then
            if (userManager.FindByName("admin@spittree.com") == null)
            {
                //super relaxed password validator
                userManager.PasswordValidator = new PasswordValidator()
                {
                    RequireDigit = false,
                    RequiredLength = 1,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false


                };

                //create a user admin
                var admin = new User()
                {
                    UserName = "admin@spittree.com",
                    Email = "admin@spittree.com",
                    FirstName = "Jim",
                    LastName = "Smith",
                    Street = "56 High Street",
                    City = "Glasgow",
                    PostCode = "G1 67AD",
                    EmailConfirmed = true,
                    PhoneNumber = "0044789145567"
                };

                //add the hashed password to user
                userManager.Create(admin, "admin123");

                //add the user to the role Admin
                userManager.AddToRole(admin.Id, "Admin");
            }

            var member1 = new User()
            {
                UserName = "member@gmail.com",
                Email = "member1@gmail.com",
                FirstName = "Paul",
                LastName = "Goat",
                Street = "5 Merry Street",
                City = "Coatbridge",
                PostCode = "ML1 67AD",
                EmailConfirmed = true,
                PhoneNumber = "00447979164499"
            };

            if (userManager.FindByName("member1@gmail.com") == null)
            {
                userManager.Create(member1, "password1");
                userManager.AddToRole(member1.Id, "Member");
            }


            var member2 = new User()
            {
                UserName = "member2@yahoo.com",
                Email = "member2@yahoo.com",
                FirstName = "Luigi",
                LastName = "Musolini",
                Street = "15 Confused Street",
                City = "Rutherglen",
                PostCode = "G1 7HO",
                EmailConfirmed = true,
                PhoneNumber = "00447779163399"
            };

            if (userManager.FindByName("member2@yahoo.com") == null)
            {
                userManager.Create(member2, "password2");
                userManager.AddToRole(member2.Id, "Member");
            }

            //save users to the database
            context.SaveChanges();
        }
    }
}