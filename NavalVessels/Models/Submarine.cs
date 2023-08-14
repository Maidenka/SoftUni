using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Submarine : Vessel , ISubmarine
    {
        private const double SubmarineArmorThickness = 200;


        public Submarine(string name, double mainWeaponCaliber, double speed) : 
                                                        base(name, mainWeaponCaliber, speed, SubmarineArmorThickness)
        {
            SubmergeMode = false;
        }

        public bool SubmergeMode { get; private set; }

        public void ToggleSubmergeMode()
        {
            SubmergeMode = !SubmergeMode;

            if (SubmergeMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 4;
                //base.Toggle(40, -4);

            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 4;
                //base.Toggle(-40, 4);
            }
        }

        public override void RepairVessel()
        {
            if (ArmorThickness < SubmarineArmorThickness) 
            {
                ArmorThickness = SubmarineArmorThickness;
                //IsRepaired= true;
            }
           
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());

            string submergeMode = SubmergeMode ? "ON" : "OFF";

            sb.AppendLine($" *Submerge mode: {submergeMode}");

            return sb.ToString().TrimEnd();
        }
    }
}
