﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WelcomeToVietnam.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AdminDbContext : DbContext
    {
        public AdminDbContext()
            : base("name=AdminDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblAdmin> tblAdmin { get; set; }
        public virtual DbSet<adminManageHotel> adminManageHotel { get; set; }
        public virtual DbSet<adminManagePlace> adminManagePlace { get; set; }
    }
}
