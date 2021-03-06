﻿// FlagCalculator
// Copyright (C) 2017 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace DustInTheWind.FlagCalculator.Business
{
    internal class NumericalBaseService
    {
        private NumericalBase numericalBase;

        public NumericalBase NumericalBase
        {
            get { return numericalBase; }
            set
            {
                numericalBase = value;
                OnNumericalBaseChanged();
            }
        }

        public event EventHandler NumericalBaseChanged;

        public NumericalBaseService()
        {
            numericalBase = NumericalBase.Decimal;
        }

        private NumericalBaseService(NumericalBase numericalBase)
        {
            this.numericalBase = numericalBase;
        }

        public void Roll()
        {
            switch (numericalBase)
            {
                case NumericalBase.Decimal:
                    NumericalBase = NumericalBase.Hexadecimal;
                    break;

                case NumericalBase.Hexadecimal:
                    NumericalBase = NumericalBase.Binary;
                    break;

                default:
                case NumericalBase.Binary:
                case NumericalBase.None:
                    NumericalBase = NumericalBase.Decimal;
                    break;
            }
        }

        private void OnNumericalBaseChanged()
        {
            NumericalBaseChanged?.Invoke(this, EventArgs.Empty);
        }

        public static implicit operator uint(NumericalBaseService numericalBase)
        {
            return (uint)numericalBase.numericalBase;
        }

        public static implicit operator NumericalBaseService(uint numericalBase)
        {
            return new NumericalBaseService((NumericalBase)numericalBase);
        }

        public static implicit operator NumericalBase(NumericalBaseService numericalBase)
        {
            return numericalBase.numericalBase;
        }

        public static implicit operator NumericalBaseService(NumericalBase numericalBase)
        {
            return new NumericalBaseService(numericalBase);
        }
    }
}
