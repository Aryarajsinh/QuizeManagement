﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuizeManagement.Models.DbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuizeManagement_0406Entities : DbContext
    {
        public QuizeManagement_0406Entities()
            : base("name=QuizeManagement_0406Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Options_Table> Options_Table { get; set; }
        public virtual DbSet<Question_Table> Question_Table { get; set; }
        public virtual DbSet<Quizzes_Table> Quizzes_Table { get; set; }
        public virtual DbSet<Result_Table> Result_Table { get; set; }
        public virtual DbSet<User_Answer_Table> User_Answer_Table { get; set; }
        public virtual DbSet<User_Table> User_Table { get; set; }
        public virtual DbSet<Admin_Table> Admin_Table { get; set; }
    }
}
