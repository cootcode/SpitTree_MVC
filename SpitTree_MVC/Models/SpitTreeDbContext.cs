using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpitTree_MVC.Models
{
        public class SpitTreeDbContext : IdentityDbContext<User>
        {
            public DbSet<Category> Categories { get; set; }
            public DbSet<Post> Posts { get; set; }

            public SpitTreeDbContext()
                : base("SpitTreeConnection2", throwIfV1Schema: false)
            {

            Database.SetInitializer(new DatabaseInitializer());
            }

            public static SpitTreeDbContext Create()
            {
                return new SpitTreeDbContext();
            }
        }
}
