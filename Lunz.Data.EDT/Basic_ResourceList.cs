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
    
    public partial class Basic_ResourceList
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid ResourceItemId { get; set; }
        public Nullable<System.Guid> CreatedById { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.Guid> UpdatedById { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.Guid> DeletedById { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
    
        public virtual Basic_ResourceItem Basic_ResourceItem { get; set; }
    }
}
