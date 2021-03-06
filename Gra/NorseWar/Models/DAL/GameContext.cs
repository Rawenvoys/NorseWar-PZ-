﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NorseWar.Models.DAL
{
    public class GameContext : DbContext
    {
        public GameContext() : base("GameContext")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Message> Messages { get; set; }
       // public DbSet<Friend> Friends { get; set; }
       // public DbSet<AccountFriend> AccountFriends { get; set; }
        public DbSet<Stats> Statses { get; set; }
       // public DbSet<ItemShield> ItemShields { get; set; }
    //    public DbSet<AccountItemShield> AccountItemShields { get; set; }
        public DbSet<AccountQuests> AccountQuestes { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Guard> Quards { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<StatsBoost> StatsBoosts { get; set; }
        public DbSet<Market> Markets { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<NorseWar.Models.ItemWeapon> ItemWeapons { get; set; }

        public System.Data.Entity.DbSet<NorseWar.Models.AccountItemWeapon> AccountItemWeapons { get; set; }
    }
}