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

using System;

namespace DustInTheWind.SomeData
{
    /// <summary>
    /// Represents parts of a house. Usually types of rooms.
    /// </summary>
    /// <remarks>
    /// It is a public enum that has the <see cref="FlagsAttribute"/> on it.
    /// FlagCalculator application should load this enum without any problem.
    /// </remarks>
    [Flags]
    public enum HousePart
    {
        /// <summary>
        /// Represents no house part.
        /// </summary>
        None = 0,

        /// <summary>
        /// A room used for sleeping in.
        /// </summary>
        Bedroom = 1 << 0,

        /// <summary>
        ///  UK also sitting room, AUSTRALIAN ENGLISH also lounge room.
        ///  The room in a house or apartment that is used for relaxing, and entertaining guests, but not usually for eating.
        /// </summary>
        LivingRoom = 1 << 1,

        /// <summary>
        /// A room with a bath and/or shower and often a toilet.
        /// </summary>
        Bathroom = 1 << 2,

        /// <summary>
        /// The area just inside the main entrance of a house, apartment or other building which leads to other rooms and usually to the stairs.
        /// </summary>
        Hall = 1 << 3,

        /// <summary>
        /// Usually just for washing machine, freezer etc.
        /// </summary>
        UtilityRoom = 1 << 4,

        /// <summary>
        /// A separated building from the house usually for storing garden tools.
        /// </summary>
        Shed = 1 << 5,

        /// <summary>
        /// Space in the roof of the house usually used only for storage.
        /// </summary>
        Loft = 1 << 6,

        /// <summary>
        /// Room in the roof space of a house (could be lived in.)
        /// </summary>
        Attic = 1 << 7,

        /// <summary>
        /// Room below ground level without any windows used for storage.
        /// </summary>
        Cellar = 1 << 8,

        /// <summary>
        /// Room below ground level, with windows, used for living and working.
        /// </summary>
        Basement = 1 << 9,

        /// <summary>
        /// The rooms below ground.
        /// </summary>
        BelowGroundSpaces = Cellar | Basement,

        /// <summary>
        /// Flat area at the top of a staircase.
        /// </summary>
        Landing = 1 << 10,

        /// <summary>
        /// Covered area before the entrance door.
        /// </summary>
        Porch = 1 << 11,

        /// <summary>
        /// Also called patio. Paved area between the house and garden for sitting and eating, etc.
        /// </summary>
        Terrace = 1 << 12,

        /// <summary>
        /// A room for reading, writing, studying in.
        /// </summary>
        Study = 1 << 13,

        /// <summary>
        /// An area with a wall or bars around it that is joined to the outside wall of a building on an upper level.
        /// </summary>
        Balcony = 1 << 14,

        /// <summary>
        /// Spaces that are open to the outside.
        /// </summary>
        OutsideSpaces = Porch | Terrace | Balcony,

        /// <summary>
        /// Represents all house pasts together.
        /// </summary>
        All = int.MaxValue
    }
}
