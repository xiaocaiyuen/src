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
    
    public partial class Membership_MenuItem
    {
        public Membership_MenuItem()
        {
            this.Membership_MenuItem1 = new HashSet<Membership_MenuItem>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public string NgUrl { get; set; }
        public string TargetUrl { get; set; }
        public string Icon { get; set; }
        public bool IsPermission { get; set; }
        public bool Visible { get; set; }
        public int SortOrder { get; set; }
        public Nullable<System.Guid> CreatedById { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.Guid> UpdatedById { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.Guid> DeletedById { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
    
        public virtual ICollection<Membership_MenuItem> Membership_MenuItem1 { get; set; }
        public virtual Membership_MenuItem Membership_MenuItem2 { get; set; }
    }
}