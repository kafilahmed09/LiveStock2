
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Areas.website.Models;



namespace LIVESTOCK.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet< LIVESTOCK.Models.Data.District> Districts {get;set;}
        public DbSet< LIVESTOCK.Models.Data.Region> Regions {get;set; }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);                     
        }                        
        public DbSet<LIVESTOCK.Areas.website.Models.Complaint> Complaint { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.Notification> Notification { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.Tender> Tender { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.ContactInfo> ContactInfo { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.SocialMedia> SocialMedia { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.ImportantLink> ImportantLink { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.WhatNew> WhatNew { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.NewsEvent> NewsEvent { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.NewsEventPicture> NewsEventPicture { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.Gallery> Gallery { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.MainSlider> MainSlider { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.DGPR> DGPR { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.Video> Video { get; set; }        
        public DbSet<LIVESTOCK.Areas.website.Models.Card> Card { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.Ongoing> Ongoing { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.ProjectService> ProjectServices { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.ServiceType> ServiceType { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.ServiceCenter> ServiceCenter { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.Director> Director { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.Budget> Budget { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.Initiative> Initiative { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.Publication> Publication { get; set; }
        public DbSet<LIVESTOCK.Areas.website.Models.GalleryFolder> GalleryFolder { get; set; }
    }
}
