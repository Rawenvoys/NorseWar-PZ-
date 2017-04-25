using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required(ErrorMessage = "Podaj login")]
        [Display(Name = "Wpisz login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Podaj hasło")]
        [Display(Name = "Wpisz hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Podaj adres mail")]
        [Display(Name = "Wpisz email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Niepoprawny format!")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Wybierz postać")]
        [Display(Name = "Wybierz postać")]
        public Characters CharacterClass { get; set; }


        public int Gold { get; set; }
        public int Experience { get; set; }
        public DateTime? BanTime { get; set; }
        public int StatPoints { get; set; }
        //public double Dmg { get; set; }
        //public double Hp { get; set; }
        //public double Crit { get; set; }


        public virtual Stats Stats { get; set; }

        public virtual ICollection<AccountItemHelmet> AccountItemHelmet { get; set; }
        public virtual ICollection<AccountItemShield> AccountItemShield { get; set; }
        public virtual ICollection<AccountItemArmor> AccountItemArmor { get; set; }
        public virtual ICollection<AccountItemWeapon> AccountItemWeapon { get; set; }
        public virtual ICollection<AccountItemLegs> AccountItemLegs { get; set; }
        public virtual ICollection<AccountItemAccessory> AccountItemAccessory { get; set; }
        public virtual ICollection<AccountItemBoots> AccountItemBoots { get; set; }

        public virtual ICollection<AccountFriend> AccoundFriend { get; set; }

        public Account() { }

        public Account(Stats stats)
        {

        }
    }


}