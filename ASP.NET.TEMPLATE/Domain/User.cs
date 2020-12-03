using System;
using System.Collections.Generic;

namespace ASP.NET.TEMPLATE.Domain
{
    public partial class User : IAuditable
    {
        public User()
        {
            Error = new HashSet<Error>();
            Login = new HashSet<Login>();
            ThingA = new HashSet<ThingA>();
            ThingB = new HashSet<ThingB>();
            ThingC = new HashSet<ThingC>();
            ThingD = new HashSet<ThingD>();
            ThingE = new HashSet<ThingE>();
            Viewed = new HashSet<Viewed>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string EmployeeNumber { get; set; }
        public string Department { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int TotalThingsA { get; set; }
        public int TotalThingsB { get; set; }
        public int TotalThingsC { get; set; }
        public int TotalThingsD { get; set; }
        public int TotalThingsE { get; set; }
        public string Role { get; set; }
        public string IdentityId { get; set; }
        public string IdentityName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ActivationCode { get; set; }
        public DateTime? ActivationDate { get; set; }
        public string ResetCode { get; set; }
        public DateTime? ResetDate { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ChangedOn { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Error> Error { get; set; }
        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<ThingA> ThingA { get; set; }
        public virtual ICollection<ThingB> ThingB { get; set; }
        public virtual ICollection<ThingC> ThingC { get; set; }
        public virtual ICollection<ThingD> ThingD { get; set; }
        public virtual ICollection<ThingE> ThingE { get; set; }
        public virtual ICollection<Viewed> Viewed { get; set; }
    }
}
