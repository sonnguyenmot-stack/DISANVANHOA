using DISANVANHOA.Models.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DISANVANHOA.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
   
        public string Address { get; set; }
        public string GTinh { get; set; }
        public string lop { get; set; }
        public string MHS { get; set; }
        public string KH { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Image { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Certificate> Certificates { get; set; }
       public DbSet<ChatFile> ChatFiles { get; set; }
      
       public DbSet<Contact> Contacts { get; set; }
       public DbSet<Document> Documents { get; set; }
       public DbSet<DocumentCategory> DocumentCategories { get; set; }
       public DbSet<DocumentImage> DocumentImages { get; set; }
       public DbSet<Exam> Exams { get; set; }
       public DbSet<News> News { get; set; }
       public DbSet<DocType> DocTypes { get; set; }
        public DbSet<General> Generals {  get; set; }
        public DbSet<Historicalrelics>Historicalrelics { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Question>Questions { get; set; }
        public DbSet<QuestionDiSan>QuestionDiSans { get; set; }
        public DbSet<ThongKe>ThongKes { get; set; }
        public DbSet<PostMedia>PostMedias { get; set; }





        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}