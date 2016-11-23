using System;
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
        public DbSet<AccountMessage> AccountMessages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<AccountFriend> AccountFriends { get; set; }
        public DbSet<Stats> Statses { get; set; }
        public DbSet<ItemShield> ItemShields { get; set; }
        public DbSet<AccountItemShield> AccountItemShields { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<NorseWar.Models.ItemWeapon> ItemWeapons { get; set; }

        public System.Data.Entity.DbSet<NorseWar.Models.AccountItemWeapon> AccountItemWeapons { get; set; }
    }
}