﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vieon.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebVieonEntities2 : DbContext
    {
        public WebVieonEntities2()
            : base("name=WebVieonEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<DaoDien> DaoDiens { get; set; }
        public virtual DbSet<Phim> Phims { get; set; }
        public virtual DbSet<Phim_DaoDien> Phim_DaoDien { get; set; }
        public virtual DbSet<Phim_TheLoai> Phim_TheLoai { get; set; }
        public virtual DbSet<TapPhim> TapPhims { get; set; }
        public virtual DbSet<ThanhToan> ThanhToans { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}