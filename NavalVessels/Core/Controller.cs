using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private VesselRepository vessels;
        private List<ICaptain> captains;

        public Controller()
        {
            vessels = new VesselRepository();
            captains = new List<ICaptain>();
        }
        public string HireCaptain(string fullName)
        {
            ICaptain captain = new Captain(fullName);

            if (captains.Any(c=>c.FullName == fullName))
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }

            captains.Add(captain);
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            IVessel vessel;

            if (vesselType != nameof(Battleship) && vesselType != nameof(Submarine))
            {
                return OutputMessages.InvalidVesselType;
            }

            if (vessels.Models.Any(v=>v.Name == name))
            {
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name);
            }

            if (vesselType == nameof(Battleship))
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else 
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }

            vessels.Add(vessel);
            return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name ,mainWeaponCaliber,speed);
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            ICaptain captain = captains.FirstOrDefault(x=>x.FullName == selectedCaptainName);
            IVessel vessel = vessels.FindByName(selectedVesselName);

            if (captain == null)
            {
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }

            if (vessel == null) 
            {
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }

            if (vessel.Captain != null)
            {
                return string.Format(OutputMessages.VesselOccupied, selectedCaptainName);
            }

            vessel.Captain = captain;
            captain.Vessels.Add(vessel);

            return string.Format(OutputMessages.SuccessfullyAssignCaptain,selectedCaptainName,selectedVesselName);

        }
        public string CaptainReport(string captainFullName)
        {
            ICaptain captain = captains.FirstOrDefault(x=>x.FullName == captainFullName);

            return captain.Report();

        }
        public string VesselReport(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);

            if (vessel != null)
            {
                return vessel.ToString();
            }

            return null;

        }
        public string ToggleSpecialMode(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);

            if (vessel != null)
            {

                if (vessel.GetType().Name == nameof(Battleship))
                {
                    Battleship battleShip = vessel as Battleship;
                    battleShip.ToggleSonarMode();

                    return string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
                }
                else 
                {
                    ISubmarine submarine = vessel as Submarine;
                    submarine.ToggleSubmergeMode();
                    return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
                }

            }
            else 
            {
                return string.Format(OutputMessages.VesselNotFound,vesselName);
            }

        }
        public string ServiceVessel(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);

            if (vessel != null) 
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }
            else
            {
               vessel.RepairVessel();
               return string.Format(OutputMessages.SuccessfullyRepairVessel,vesselName);
            }


        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            IVessel atacker = vessels.FindByName(attackingVesselName);
            IVessel defender = vessels.FindByName(defendingVesselName);

            if (atacker == null || defender == null)
            {
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }

            if (atacker.ArmorThickness == 0  || defender.ArmorThickness == 0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }

            atacker.Attack(defender);
            atacker.Captain.IncreaseCombatExperience();
            defender.Captain.IncreaseCombatExperience();

            return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defender.ArmorThickness);

        }






    }
}
