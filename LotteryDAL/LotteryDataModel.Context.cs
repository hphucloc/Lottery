﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LotteryDAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LotteryEntities : DbContext
    {
        public LotteryEntities()
            : base("name=LotteryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<NumberType> NumberTypes { get; set; }
        public virtual DbSet<NumberWinLevel> NumberWinLevels { get; set; }
        public virtual DbSet<NumberTipBet> NumberTipBets { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<AmountBuyLottery> AmountBuyLotteries { get; set; }
        public virtual DbSet<Number> Numbers { get; set; }
        public virtual DbSet<NumberBought> NumberBoughts { get; set; }
    }
}