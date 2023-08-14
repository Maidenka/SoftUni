using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private const double BattleshipArmorThickness = 300;

        public Battleship(string name, double mainWeaponCaliber, double speed) :
                                    base(name, mainWeaponCaliber, speed, BattleshipArmorThickness)
        {

            SonarMode = false;
        }

        public bool SonarMode {get ; private set;}

        public void ToggleSonarMode()
        {
            SonarMode = !SonarMode;

            if (SonarMode) 
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
                //base.Toggle(40, -5);
            }
            else 
            {
                 MainWeaponCaliber -= 40;
                Speed += 5;
                //base.Toggle(-40, 5);
            }

        }

        public override void RepairVessel()
        {
            if (ArmorThickness < BattleshipArmorThickness)
            {
                ArmorThickness = BattleshipArmorThickness;
                //IsRepaired= true;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());

            string sonnarMode = SonarMode ? "ON" : "OFF";

            sb.AppendLine($" *Sonar mode: {sonnarMode}");

            return sb.ToString().TrimEnd();
            
        }
    }
}
