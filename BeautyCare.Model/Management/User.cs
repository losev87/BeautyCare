using System;
using System.Collections.Generic;
using BeautyCare.Model.Entity;
using IntraVision.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeautyCare.Model.Management
{
    public class User : IdentityUser<int, IdentityUserLoginBase, UserRole, IdentityUserClaimBase>, IEntityBase
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PatronimicName { get; set; }
        public virtual Gender Gender { get; set; }
        public int? GenderId { get; set; }
        public DateTime BirthDate { get; set; }
        //public virtual UserType UserType { get; set; }
        //public int UserTypeId { get; set; }
        public decimal Rating { get; set; }
        public virtual ICollection<UserService> UserServices { get; set; }
        public virtual ICollection<UserPhoto> UserPhotos { get; set; }
        public virtual ICollection<PrivateMessage> IncomingMessages { get; set; }
        public virtual ICollection<PrivateMessage> OutcomingMessages { get; set; }
        public virtual ICollection<Order> ExecutorOrders { get; set; }
        public virtual ICollection<Order> CustomerOrders { get; set; }
        public virtual ICollection<OrderMessage> OrderMessages { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
        //todo public virtual ICollection<InsragramPublication> InsragramPublications { get; set; }
        public virtual ICollection<ServiceType> ServiceTypes { get; set; }
        public virtual ICollection<User> Friends { get; set; }
        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
    }
}
