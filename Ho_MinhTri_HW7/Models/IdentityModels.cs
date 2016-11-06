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

//TODO: Change the namespace here to match your project's name
namespace Ho_MinhTri_HW7.Models
{
    // You can add profile data for the user by adding more properties to your AppUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {
        //SCALAR PROPERTIES
        //ID
        [Display(Name = "Customer ID")]
        public Int32 CustomerID { get; set; }

        //First Name
        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        //Last Name
        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        //Email
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid email address.")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public String Email { get; set; }

        //Phone number
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        public String PhoneNumber { get; set; }

        //OK to text
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Is it OK to text the member?")]
        public bool OKToText { get; set; }

        //Major
        public enum MajorList { Accounting, Business_Honors, Finance, International_Business, Management, MIS, Marketing, SCM, STM }
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
        //TODO: Add your dbSets here.  As an example, I've included one for products
        //Remember - the IdentityDbContext already contains a db set for Users.  If you add another one, your code will break
        //public DbSet<Product> Products { get; set; }

        //Constructor that invokes the base constructor
        public AppDbContext() : base("MyDbConnection", throwIfV1Schema: false)
        {
        }

        //Create the db set
        public DbSet<AppUser> Members { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Event> Events { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}