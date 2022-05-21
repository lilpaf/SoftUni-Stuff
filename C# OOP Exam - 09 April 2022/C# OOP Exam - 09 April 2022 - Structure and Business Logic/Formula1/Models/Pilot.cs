﻿using System;
using System.Collections.Generic;
using System.Text;
using Formula1.Models.Contracts;
using Formula1.Utilities;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string fullName;
        private IFormulaOneCar car;
        private int numberOfWins;

        public Pilot(string fullName)
        {
            FullName = fullName;
            CanRace = false;
        }

        public string FullName 
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                }

                fullName = value;
            }
        }

        public IFormulaOneCar Car
        {
            get => car;
            private set
            {
                car = value ?? throw new NullReferenceException(ExceptionMessages.InvalidCarForPilot);
            }
        }

        public int NumberOfWins => numberOfWins;
        public bool CanRace
        {
            get;
            private set;
        }
        public void AddCar(IFormulaOneCar car)
        {
            Car = car;
            CanRace = true;
        }

        public void WinRace()
        {
            numberOfWins++;
        }

        public override string ToString()
        {
            return $"Pilot {FullName} has {NumberOfWins} wins.";
        }
    }
}
