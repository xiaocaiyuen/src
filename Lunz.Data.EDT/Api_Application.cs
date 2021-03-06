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
    
    public partial class Api_Application
    {
        public Api_Application()
        {
            this.Api_ApplicationKey = new HashSet<Api_ApplicationKey>();
            this.Api_ApplicationLog = new HashSet<Api_ApplicationLog>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Lunz.Data.EDT.AppTypes AppType { get; set; }
        public string Domain { get; set; }
        public string IPAddress { get; set; }
        public bool Enabled { get; set; }
        public Nullable<System.Guid> CreatedById { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.Guid> UpdatedById { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.Guid> DeletedById { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
    
        public virtual ICollection<Api_ApplicationKey> Api_ApplicationKey { get; set; }
        public virtual ICollection<Api_ApplicationLog> Api_ApplicationLog { get; set; }
    }
}
