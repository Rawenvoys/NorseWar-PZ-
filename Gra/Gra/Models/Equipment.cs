//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gra.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Equipment
    {
        public int IdEquipment { get; set; }
        public string Shield { get; set; }
        public string Weapon { get; set; }
        public string Armor { get; set; }
        public string Boots { get; set; }
        public string Legs { get; set; }
        public string Helmet { get; set; }
        public string Accessory { get; set; }
    
        public virtual Account Account { get; set; }
    }
}
