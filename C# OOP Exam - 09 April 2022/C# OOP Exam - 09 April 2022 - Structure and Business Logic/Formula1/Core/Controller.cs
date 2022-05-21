using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Utilities;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository formulaOneCarRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            formulaOneCarRepository = new FormulaOneCarRepository();
        }

        public string CreatePilot(string fullName)
        {
            IPilot specialPilot = pilotRepository.FindByName(fullName);
            
            if (specialPilot == null)
            {
                pilotRepository.Add(new Pilot(fullName));
                return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
            }

            throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (type != "Ferrari" && type != "Williams")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            IFormulaOneCar specialCar = formulaOneCarRepository.FindByName(model);

            if (specialCar != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            if (type == "Ferrari")
            {
                formulaOneCarRepository.Add(new Ferrari(model,horsepower,engineDisplacement));
                return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
            }

            formulaOneCarRepository.Add(new Williams(model, horsepower, engineDisplacement));
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            IRace specialRace = raceRepository.FindByName(raceName);

            if (specialRace != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            raceRepository.Add(new Race(raceName, numberOfLaps));
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            IPilot specialPilot = pilotRepository.FindByName(pilotName);

            if (specialPilot == null || specialPilot.CanRace == true)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            IFormulaOneCar specialCar = formulaOneCarRepository.FindByName(carModel);
            
            if (specialCar == null)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            specialPilot.AddCar(specialCar);
            formulaOneCarRepository.Remove(specialCar);
            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, specialCar.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IRace specialRace = raceRepository.FindByName(raceName);

            if (specialRace == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            IPilot specialPilot = pilotRepository.FindByName(pilotFullName);

            if (specialPilot == null || specialPilot.CanRace == false || specialRace.Pilots.Contains(specialPilot))
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            specialRace.AddPilot(specialPilot);

            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string StartRace(string raceName)
        {
            IRace specialRace = raceRepository.FindByName(raceName);

            if (specialRace == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            } 
            
            if (specialRace.Pilots.Count < 3)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }

            if (specialRace.TookPlace == true)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            var pilotsToRace = specialRace.Pilots.Where(x => x.CanRace == true);
            
            List<IPilot> results =
                pilotsToRace.OrderByDescending(x => x.Car.RaceScoreCalculator(specialRace.NumberOfLaps)).ToList();

            specialRace.TookPlace = true;
            results[0].WinRace();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format(OutputMessages.PilotFirstPlace, results[0].FullName, specialRace.RaceName));
            sb.AppendLine(string.Format(OutputMessages.PilotSecondPlace, results[1].FullName, specialRace.RaceName));
            sb.AppendLine(string.Format(OutputMessages.PilotThirdPlace, results[2].FullName, specialRace.RaceName));

            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();

            var races = raceRepository.Models.Where(x => x.TookPlace == true);

            foreach (var race in races)
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();

            var pilotsOrdered = pilotRepository.Models.OrderByDescending(x => x.NumberOfWins);

            foreach (var pilot in pilotsOrdered)
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
