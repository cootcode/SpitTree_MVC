using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpitTree_MVC.Models
{
        public class SpitTreeDbContext : IdentityDbContext<User>
        {
            public SpitTreeDbContext()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
            }

            public static SpitTreeDbContext Create()
            {
                return new SpitTreeDbContext();
            }
        }
}
