// FlagCalculator
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

namespace DustInTheWind.SomeData
{
    internal enum HouseObject
    {
        None = 0,
        Door = 1,
        Window = 1 << 2,
        Table = 1 << 3,
        Bed = 1 << 4,
        Chair = 1 << 5,
        Wardrobe = 1 << 6,
        Mirror = 1 << 7,
        Sink = 1 << 8,
        Stove = 1 << 9,
        Rug = 1 << 10,
        Bathtub = 1 << 11
    }
}
