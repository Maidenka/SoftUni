using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {

        private string name;
        private ICaptain captain;
        private double armorThickness;
        //private bool isRepaired;
        private List<String> targets;



        public Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;
            targets = new List<String>();

        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidVesselName);
                }
                name = value;
            }
        }

        public ICaptain Captain
        {
            get => captain;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidCaptainToVessel);
                }

                captain = value;
            }
        }

        public double ArmorThickness
        {
            get => armorThickness;

            set
            {
                armorThickness = value;

                if (armorThickness <0)
                {
                    armorThickness = 0;
                }

            }
        }


        public double MainWeaponCaliber { get; protected set; }

        public double Speed { get; protected set; }

        public ICollection<string> Targets { get => targets; }

        //public bool IsRepaired {get; protected set;}


        public void Attack(IVessel target)
        {
            IVessel defender = target;

            if (target == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidTarget);
            }

            //defender.Captain.IncreaseCombatExperience();
            //this.Captain.IncreaseCombatExperience();

            defender.ArmorThickness -= this.MainWeaponCaliber;



           this.Targets.Add(defender.Name);
  

        }

        public abstract void RepairVessel();

        public virtual string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //List<string> targets = new List<string>();

            string targetsString = Targets.Any() ? string.Join(", ", Targets) : "None";

            sb.AppendLine($"- {Name}");
            sb.AppendLine($" *Type: {GetType().Name}");
            sb.AppendLine($" *Armor thickness: {ArmorThickness}");
            sb.AppendLine($" *Main weapon caliber: {MainWeaponCaliber}");            
            sb.AppendLine($" *Speed: {Speed} knots");
            sb.AppendLine($" *Targets: {targetsString}");

            return sb.ToString().TrimEnd();

        }


        //protected void Toggle(double MainWeaponCaliber , double speed) 
        //{
        //    this.MainWeaponCaliber += MainWeaponCaliber;
        //    this.Speed += speed;
        //}
    }
}
