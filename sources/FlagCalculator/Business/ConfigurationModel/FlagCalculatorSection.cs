//// FlagCalculator
//// Copyright (C) 2017 Dust in the Wind
//// 
//// This program is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
//// 
//// This program is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU General Public License for more details.
//// 
//// You should have received a copy of the GNU General Public License
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

//using System.Collections.Generic;
//using System.Configuration;

//namespace DustInTheWind.FlagCalculator.Business.ConfigurationModel
//{
//    public class FlagCalculatorSection : ConfigurationSection
//    {
//        [ConfigurationCollection(typeof(EnumTypeConfigurationElement))]
//        public List<EnumTypeConfigurationElement> EnumTypes
//        {
//            get
//            {
//                return (List<EnumTypeConfigurationElement>)this["enumTypes"];
//            }
//            set
//            {
//                this["enumTypes"] = value;
//            }
//        }
//    }

//    public class EnumTypeCollection : ConfigurationElementCollection
//    {
//        public EnumTypeCollection()
//        {
//            HostConfigElement details = (HostConfigElement)CreateNewElement();
//            if (details.SSHServerHostname != "")
//            {
//                Add(details);
//            }
//        }

//        public override ConfigurationElementCollectionType CollectionType
//        {
//            get
//            {
//                return ConfigurationElementCollectionType.BasicMap;
//            }
//        }

//        protected override ConfigurationElement CreateNewElement()
//        {
//            return new HostConfigElement();
//        }

//        protected override Object GetElementKey(ConfigurationElement element)
//        {
//            return ((HostConfigElement)element).SSHServerHostname;
//        }

//        public HostConfigElement this[int index]
//        {
//            get
//            {
//                return (HostConfigElement)BaseGet(index);
//            }
//            set
//            {
//                if (BaseGet(index) != null)
//                {
//                    BaseRemoveAt(index);
//                }
//                BaseAdd(index, value);
//            }
//        }

//        new public HostConfigElement this[string name]
//        {
//            get
//            {
//                return (HostConfigElement)BaseGet(name);
//            }
//        }

//        public int IndexOf(HostConfigElement details)
//        {
//            return BaseIndexOf(details);
//        }

//        public void Add(HostConfigElement details)
//        {
//            BaseAdd(details);
//        }

//        protected override void BaseAdd(ConfigurationElement element)
//        {
//            BaseAdd(element, false);
//        }

//        public void Remove(HostConfigElement details)
//        {
//            if (BaseIndexOf(details) >= 0)
//                BaseRemove(details.SSHServerHostname);
//        }

//        public void RemoveAt(int index)
//        {
//            BaseRemoveAt(index);
//        }

//        public void Remove(string name)
//        {
//            BaseRemove(name);
//        }

//        public void Clear()
//        {
//            BaseClear();
//        }

//        protected override string ElementName
//        {
//            get { return "host"; }
//        }
//    }

//    public class EnumTypeConfigurationElement : ConfigurationElement
//    {
//        [ConfigurationProperty("value", IsRequired = true, IsKey = true)]
//        public int Value { get; set; }
//    }
//}
