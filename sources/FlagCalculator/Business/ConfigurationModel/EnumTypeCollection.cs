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

using System.Configuration;

namespace DustInTheWind.FlagCalculator.Business.ConfigurationModel
{
    public class EnumTypeCollection : ConfigurationElementCollection
    {
        public EnumTypeConfigurationElement this[int index]
        {
            get { return (EnumTypeConfigurationElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        public void Add(EnumTypeConfigurationElement configurationElement)
        {
            BaseAdd(configurationElement);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new EnumTypeConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement configurationElement)
        {
            return ((EnumTypeConfigurationElement)configurationElement).Value;
        }

        public void Remove(EnumTypeConfigurationElement configurationElement)
        {
            BaseRemove(configurationElement.Value);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }
    }
}