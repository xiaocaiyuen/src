﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EDT_DBEntities : DbContext
    {
        public EDT_DBEntities()
            : base("name=EDT_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Api_Application> Api_Application { get; set; }
        public virtual DbSet<Api_ApplicationKey> Api_ApplicationKey { get; set; }
        public virtual DbSet<Api_ApplicationLog> Api_ApplicationLog { get; set; }
        public virtual DbSet<Basic_ResourceItem> Basic_ResourceItem { get; set; }
        public virtual DbSet<Membership_AuthToken> Membership_AuthToken { get; set; }
        public virtual DbSet<Membership_MenuItem> Membership_MenuItem { get; set; }
        public virtual DbSet<Membership_User> Membership_User { get; set; }
        public virtual DbSet<Membership_UserResetPassword> Membership_UserResetPassword { get; set; }
        public virtual DbSet<Membership_VIPLevel> Membership_VIPLevel { get; set; }
        public virtual DbSet<Basic_Logs> Basic_Logs { get; set; }
        public virtual DbSet<Object_AttributeFiled> Object_AttributeFiled { get; set; }
        public virtual DbSet<Object_AttributeValue> Object_AttributeValue { get; set; }
        public virtual DbSet<Object_AttributeArea> Object_AttributeArea { get; set; }
        public virtual DbSet<Product_ProductContract> Product_ProductContract { get; set; }
        public virtual DbSet<Object_AttributeCategory> Object_AttributeCategory { get; set; }
        public virtual DbSet<Basic_Announcement> Basic_Announcement { get; set; }
        public virtual DbSet<Basic_BankInfo> Basic_BankInfo { get; set; }
        public virtual DbSet<Basic_CompanyInfo> Basic_CompanyInfo { get; set; }
        public virtual DbSet<Product_ProductDefinition> Product_ProductDefinition { get; set; }
        public virtual DbSet<Product_ProductForm> Product_ProductForm { get; set; }
        public virtual DbSet<Product_ProductGpsFee> Product_ProductGpsFee { get; set; }
        public virtual DbSet<Basic_DataDictionary> Basic_DataDictionary { get; set; }
        public virtual DbSet<Basic_ResourceList> Basic_ResourceList { get; set; }
    }
}
