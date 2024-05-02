using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using Vimachem.Models.Domain;
using Vimachem.Models.Dto;

namespace Vimachem.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<AuditLog> AuditLog { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Category entity
            modelBuilder.Entity<Category>(entity =>
            {   
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                    .ValueGeneratedOnAdd();
                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(255); 
                entity.Property(c => c.Description)
                    .HasMaxLength(500); 
                entity.Property(c => c.CreatedDate)
                    .IsRequired();
                entity.Property(c => c.UpdatedDate)
                    .IsRequired(false);
            });
            modelBuilder.Entity<Category>().HasData(
                new()
                {
                    Id = 1,
                    Description =
                        "This category encompasses a broad range of books that are primarily created from the imagination of the author. Fiction books may include novels, short stories, and novellas. Genres within fiction can vary widely, encompassing everything from historical fiction and fantasy to mystery, science fiction, and contemporary literature.",
                    CreatedDate = DateTime.Now,
                    Name = "Fiction"
                },
                new() { Id = 2, Description = "Non-fiction books are based on real facts and truthful accounts of events and ideas. This category includes biographies, memoirs, essays, and self-help books. Non-fiction also covers educational subjects such as history, psychology, science, and business.", CreatedDate = DateTime.Now, Name = "Non-Fiction" },
                new() { Id = 3, Description = "Books in this category are known for suspenseful plots that involve investigations and the solving of crimes. They often revolve around a mysterious event or a crime that needs to be solved, typically leading to a climactic reveal or twist.", CreatedDate = DateTime.Now, Name = "Mystery & Thriller" },
                new() { Id = 4, Description = "This category includes books that explore imaginative and futuristic concepts, alternative worlds, and advanced technology. Science fiction often deals with themes like space exploration and time travel, while fantasy books may include elements such as magic, mythical creatures, and medieval settings.", CreatedDate = DateTime.Now, Name = "Science Fiction & Fantasy" },
                new() { Id = 5, Description = "Romance books explore the theme of love and relationships between people. These stories often focus on romantic relationships from the courtship to the culmination of a love story, providing emotional narratives that may also delve into the characters’ personal growth.", CreatedDate = DateTime.Now, Name = "Romance" },
                new() { Id = 6, Description = "Designed to appeal to children from infancy through elementary school, these books range from picture books to easy readers and early chapter books. They often teach lessons through fun stories and vibrant illustrations, helping children understand their world and sparking their imagination.", CreatedDate = DateTime.Now, Name = "Children’s Books" }
            );

            // Configure the Book entity
            modelBuilder.Entity<Book>(entity =>
            {   
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(b => b.Name)
                    .IsRequired()
                    .HasMaxLength(255); 

                
                entity.Property(b => b.Description)
                    .HasMaxLength(1000);

                
                entity.Property(b => b.CreatedDate)
                    .IsRequired();

                
                entity.Property(b => b.UpdatedDate)
                    .IsRequired(false);

               

                entity.HasOne(b => b.Category)
                    .WithMany() // Omit the navigation property here
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.Navigation("Category");

            });

            //modelBuilder.Entity("Vimachem.Models.Domain.Book", b =>
            //{
            //    b.HasOne("Vimachem.Models.Domain.Category", "Category")
            //        .WithMany()
            //        .HasForeignKey("CategoryId")
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .IsRequired();

            //    b.Navigation("Category");
            //});

            modelBuilder.Entity<Book>().HasData(
                new() { Id = 1, Name = "Book One", CategoryId = 1, Description = "Book 1 Description", CreatedDate = DateTime.Now },
                new() { Id = 2, Name = "Book Two", CategoryId = 2, Description = "Book 2 Description", CreatedDate = DateTime.Now },
                new() { Id = 3, Name = "Book Three", CategoryId = 3, Description = "Book 3 Description", CreatedDate = DateTime.Now },
                new() { Id = 4, Name = "Book Four", CategoryId = 4, Description = "Book 4 Description", CreatedDate = DateTime.Now },
                new() { Id = 5, Name = "Book Five", CategoryId = 5, Description = "Book 5 Description", CreatedDate = DateTime.Now }
            );

            // Configure the AuditLog entity
            modelBuilder.Entity<AuditLog>(entity =>
            {   
                entity.HasKey(e => e.AuditLogId);
                entity.Property(e => e.AuditLogId)
                    .HasColumnName("AuditLogId") 
                    .ValueGeneratedOnAdd();  

                // Configure nullable string properties
                entity.Property(e => e.ActionType).HasMaxLength(100); // Assuming a max length for string fields
                entity.Property(e => e.EntityType).HasMaxLength(100);
                entity.Property(e => e.EntityId).HasMaxLength(100);
                entity.Property(e => e.Changes).HasColumnType("nvarchar(max)"); // Configures this as a nvarchar(max) in SQL Server
                entity.Property(e => e.UserId).HasMaxLength(450); 
                entity.Property(e => e.ErrorMessage).HasColumnType("nvarchar(max)");

                
                entity.Property(e => e.Timestamp).IsRequired();  // Makes sure the Timestamp is required
            });

            modelBuilder.Entity<AuditLog>().HasData(
                new AuditLog
                {
                    AuditLogId = 1,
                    ActionType = "Create",
                    EntityType = "Category",
                    EntityId = "1",
                    Timestamp = DateTime.Now,
                    Changes = "Initial creation of Fiction category"
                },
                new AuditLog
                {
                    AuditLogId = 2,
                    ActionType = "Create",
                    EntityType = "Category",
                    EntityId = "2",
                    Timestamp = DateTime.Now,
                    Changes = "Initial creation of Non-Fiction category"
                }
            );

            
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);  
                entity.Property(u => u.Id)
                    .ValueGeneratedOnAdd()  
                    .IsRequired();          

                entity.Property(u => u.Name)
                    .HasMaxLength(100)
                    .IsRequired();  

                entity.Property(u => u.Surname)
                    .HasMaxLength(100)
                    .IsRequired();  
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id); 

                entity.Property(r => r.Id)
                    .ValueGeneratedOnAdd()  
                    .IsRequired();          

                entity.Property(r => r.Name)
                    .HasMaxLength(50)
                    .IsRequired();  
            });

            
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();  // UserId is required

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Alice", Surname = "Johnson" },
                new User { Id = 2, Name = "Bob", Surname = "Smith" },
                new User { Id = 3, Name = "Charlie", Surname = "Brown" }
            );

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" },
                new Role { Id = 3, Name = "Guest" }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 }, 
                new UserRole { UserId = 1, RoleId = 2 }, 
                new UserRole { UserId = 2, RoleId = 2 }, 
                new UserRole { UserId = 3, RoleId = 2 }, 
                new UserRole { UserId = 3, RoleId = 3 }  
            );

        }
    }
}
