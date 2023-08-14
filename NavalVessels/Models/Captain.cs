using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Captain : ICaptain
    {   
        private List <IVessel> vessels;

        public Captain(string fullName)
        {   
            FullName = fullName;
            CombatExperience = 0;
            vessels = new List<IVessel>();
        }

        private string fullName;
        private int combatExperience;

        public string FullName
        {
            get => fullName;

           private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);
                }
                fullName = value;
            }
        }
        public int CombatExperience
        {
            get => combatExperience;

           private set
            {
                // To do "Could be increased by 10"
                combatExperience = value;
            }
        }


        public ICollection<IVessel> Vessels { get => vessels; }

        

        public void AddVessel(IVessel vessel)
        {
            if(vessel == null) 
            {
                throw new NullReferenceException(ExceptionMessages.InvalidVesselForCaptain);
            }

            Vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            CombatExperience += 10;
        }

        public string Report()
        {
            if (!Vessels.Any()) 
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {Vessels.Count} vessels.");

            foreach (var vessel in Vessels)
            {
                sb.AppendLine(vessel.ToString());
            }
            return sb.ToString().TrimEnd();

        }
    }
}
