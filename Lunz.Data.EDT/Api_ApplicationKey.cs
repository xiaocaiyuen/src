//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lunz.Data.EDT
{
    using System;
    using System.Collections.Generic;
    
    public partial class Api_ApplicationKey
    {
        public Api_ApplicationKey()
        {
            this.Api_ApplicationLog = new HashSet<Api_ApplicationLog>();
            this.Membership_AuthToken = new HashSet<Membership_AuthToken>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid ApplicationId { get; set; }
        public Nullable<System.Guid> CreatedById { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.Guid> UpdatedById { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.Guid> DeletedById { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
    
        public virtual Api_Application Api_Application { get; set; }
        public virtual ICollection<Api_ApplicationLog> Api_ApplicationLog { get; set; }
        public virtual ICollection<Membership_AuthToken> Membership_AuthToken { get; set; }
    }
}
