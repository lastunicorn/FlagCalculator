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
    /// <summary>
    /// Represents a type of house.
    /// </summary>
    /// <remarks>
    /// It is a public enum that does not have the <see cref="FlagsAttribute"/> on it.
    /// FlagCalculator application should raise a warning, but still load this enum.
    /// </remarks>
    public enum HouseType
    {
        /// <summary>
        /// Represents no house type.
        /// </summary>
        None,

        /// <summary>
        /// A building which people, usually one family, live in.
        /// </summary>
        House,

        /// <summary>
        /// Describes a house that is not connected to any other building.
        /// </summary>
        DetachedHouse,

        /// <summary>
        /// A house that is semi-detached is one that is joined to another similar house on only one side.
        /// </summary>
        SemiDetachedHouse,

        /// <summary>
        /// In UK usually flat. A set of rooms for living in, especially on one floor of a building.
        /// </summary>
        Apartment,

        /// <summary>
        /// A house that is joined to the houses on either side of it by shared walls.
        /// </summary>
        TerracedHouse,

        /// <summary>
        /// A small house, usually in the countryside.
        /// </summary>
        Cottage,

        /// <summary>
        /// A house that has only one story/floor.
        /// </summary>
        Bungalow,

        /// <summary>
        /// A rented room which has a bed, table, chairs and somewhere to cook in it.
        /// </summary>
        Bedsit,

        /// <summary>
        /// A house usually in the countryside or near the sea, particularly in southern Europe, and often one which people can rent for a holiday.
        /// </summary>
        Villa,

        /// <summary>
        /// A holiday house or apartment which is owned by several different people, each of whom is able to use it for a particular period of the year.
        /// </summary>
        TimeShare
    }
}
