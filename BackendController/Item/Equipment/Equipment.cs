using System;

namespace OOAD_WarChess.Item.Equipment
{
    public class Equipment : IEquipment
    {
        public string Name { get; set; }
        public int LVL { get; set; }
        public int PHY_ATK { get; set; }
        public int PHY_DEF { get; set; }
        public int MAG_ATK { get; set; }
        public int MAG_DEF { get; set; }

        public static readonly IEquipment DefaultEquipment = new Equipment()
        {
            Name = "Default",
            LVL = 1,
            PHY_ATK = 0,
            PHY_DEF = 0,
            MAG_ATK = 0,
            MAG_DEF = 0
        };

        public void Upgrade()
        {
            LVL++;
            PHY_ATK = (int) (PHY_ATK * 1.2);
            PHY_DEF = (int) (PHY_DEF * 1.2);
            MAG_ATK = (int) (MAG_ATK * 1.2);
            MAG_DEF = (int) (MAG_DEF * 1.2);
        }

        public Equipment()
        {
        }

        public Equipment(int phyatk, int phydef, int magatk, int magdef)
        {
            LVL = 0;
            PHY_ATK = phyatk;
            PHY_DEF = phydef;
            MAG_ATK = magatk;
            MAG_DEF = magdef;
        }
    }
}