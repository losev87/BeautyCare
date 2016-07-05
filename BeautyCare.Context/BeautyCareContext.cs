using System.Data.Entity;
using BeautyCare.Model.Entity;
using BeautyCare.Model.Management;
using IntraVision.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeautyCare.Context
{
    public class BeautyCareContext : IdentityDbContext<User, Role, int, IdentityUserLoginBase, UserRole, IdentityUserClaimBase>
    {
        public BeautyCareContext()
            : base("BeautyCareContext")
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<UserPhotoData> UserPhotoDatas { get; set; }
        public DbSet<UserService> UserServices { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<AttachmentCategory> AttachmentCategories { get; set; }
        public DbSet<ColorCategory> ColorCategories { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderAttachment> OrderAttachments { get; set; }
        public DbSet<OrderAttachmentData> OrderAttachmentDatas { get; set; }
        public DbSet<OrderMessage> OrderMessages { get; set; }
        public DbSet<OrderMessageAttachment> OrderMessageAttachments { get; set; }
        public DbSet<OrderMessageAttachmentData> OrderMessageAttachmentDatas { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderUserService> OrderUserServices { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<PrivateMessageAttachment> PrivateMessageAttachments { get; set; }
        public DbSet<PrivateMessageAttachmentData> PrivateMessageAttachmentDatas { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentAttachment> CommentAttachments { get; set; }
        public DbSet<CommentAttachmentData> CommentAttachmentDatas { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<PublicationAttachment> PublicationAttachments { get; set; }
        public DbSet<PublicationAttachmentData> PublicationAttachmentDatas { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAttachment> QuestionAttachments { get; set; }
        public DbSet<QuestionAttachmentData> QuestionAttachmentDatas { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerAttachment> AnswerAttachments { get; set; }
        public DbSet<AnswerAttachmentData> AnswerAttachmentDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderAttachment>().HasOptional(x => x.Data).WithRequired().WillCascadeOnDelete(true);
            modelBuilder.Entity<OrderMessageAttachment>().HasOptional(x => x.Data).WithRequired().WillCascadeOnDelete(true);
            modelBuilder.Entity<PrivateMessageAttachment>().HasOptional(x => x.Data).WithRequired().WillCascadeOnDelete(true);
            modelBuilder.Entity<CommentAttachment>().HasOptional(x => x.Data).WithRequired().WillCascadeOnDelete(true);
            modelBuilder.Entity<PublicationAttachment>().HasOptional(x => x.Data).WithRequired().WillCascadeOnDelete(true);
            modelBuilder.Entity<QuestionAttachment>().HasOptional(x => x.Data).WithRequired().WillCascadeOnDelete(true);
            modelBuilder.Entity<AnswerAttachment>().HasOptional(x => x.Data).WithRequired().WillCascadeOnDelete(true);

            modelBuilder.Entity<Order>()
                    .HasRequired(m => m.Executor)
                    .WithMany(t => t.ExecutorOrders)
                    .HasForeignKey(m => m.ExecutorId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                        .HasRequired(m => m.Customer)
                        .WithMany(t => t.CustomerOrders)
                        .HasForeignKey(m => m.CustomerId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrivateMessage>()
                    .HasRequired(m => m.Recipient)
                    .WithMany(t => t.IncomingMessages)
                    .HasForeignKey(m => m.RecipientId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrivateMessage>()
                        .HasRequired(m => m.Sender)
                        .WithMany(t => t.OutcomingMessages)
                        .HasForeignKey(m => m.SenderId)
                        .WillCascadeOnDelete(false);
        }
    }
}
