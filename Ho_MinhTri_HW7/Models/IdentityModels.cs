//Libs from HW4, mainly for Scalar properties
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//Original Libs
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ho_MinhTri_HW7.Models
{
    //Put the enum outside the class to make it accessible from UserViewModels (RegisterViewModel)
    public enum MajorList { Accounting, Business_Honors, Finance, International_Business, Management, MIS, Marketing, SCM, STM }

    // You can add profile data for the user by adding more properties to your AppUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {
        //SCALAR PROPERTIES

        //First Name
        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        //Last Name
        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        //OK to text
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Is it OK to text the member?")]
        public bool OKToText { get; set; }

        //Major
        [Required(ErrorMessage = "Major is required")]
        [Display(Name = "Major")]
        public MajorList Major { get; set; }

        //NAVIGATIONAL PROPERTIES
        [Display(Name = "Events Attending")]
        public virtual List<Event> EventAttending { get; set; }
        public virtual List<Committee> CommitteePartOf { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    //NOTE: Here is your dbContext for the entire project.  There should only be ONE DbContext per project
    //Your dbContext (AppDbContext) inherits from IdentityDbContext, which inherits from the "regular" DbContext

    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //Remember - the IdentityDbContext already contains a db set for Users.  If you add another one, your code will break
        //Create the db set
        //No need for Members as it's created via Users in Identity
        //public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Event> Events { get; set; }

        //Constructor that invokes the base constructor
        public AppDbContext() : base("MyDbConnection", throwIfV1Schema: false)
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        //public System.Data.Entity.DbSet<Ho_MinhTri_HW7.Models.AppUser> AppUsers { get; set; }
    }
}