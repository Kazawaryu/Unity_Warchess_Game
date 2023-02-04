using System;

namespace OOAD_WarChess.Item.Equipment
{

    public interface IEquipment
    {
        public string Name { get; set; }
        public int LVL { get; set; }
        
        public int PHY_ATK { get; set; }
        public int PHY_DEF { get; set; }
        public int MAG_ATK { get; set; }
        public int MAG_DEF { get; set; }
        
        public void Upgrade();
    }
}