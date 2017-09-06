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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI.Converters
{
    public class SmartNumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SmartNumber smartNumber = value as SmartNumber;

            if (smartNumber != null)
                return ConvertFromSmartNumber(smartNumber);

            return null;
        }

        private static object ConvertFromSmartNumber(SmartNumber smartNumber)
        {
            switch (smartNumber.NumericalBase)
            {
                case NumericalBase.None:
                case NumericalBase.Decimal:
                    return smartNumber.ToString();

                case NumericalBase.Hexadecimal:
                    return RenderHexadecimal(smartNumber);

                case NumericalBase.Binary:
                    return RenderBinary(smartNumber);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static object RenderHexadecimal(SmartNumber smartNumber)
        {
            string xamlContent = smartNumber.ToString();

            List<string> result = new List<string>();
            bool isBegin = true;

            foreach (char c in xamlContent)
            {
                bool isDigit = char.IsDigit(c) || (c >= 'A' && c <= 'F');
                
                if (isBegin)
                {
                    if (isDigit && c != '0')
                    {
                        isBegin = false;
                        result.Add($@"<Run Foreground=""Black"">{c}</Run>");
                    }
                    else
                    {
                        result.Add(c.ToString());
                    }
                }
                else
                {
                    if (isDigit)
                        result.Add($@"<Run Foreground=""Black"">{c}</Run>");
                    else
                        result.Add(c.ToString());
                }
            }

            xamlContent = string.Join("", result);

            string xamlTextBlock = @"
            <TextBlock
                xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                Padding = ""5""
                VerticalAlignment = ""Center""
                HorizontalAlignment = ""Stretch""
                Foreground = ""Gray"">

                {0}

            </TextBlock>";

            xamlTextBlock = string.Format(xamlTextBlock, xamlContent);

            using (StringReader stringReader = new StringReader(xamlTextBlock))
            using (XmlReader xmlReader = XmlReader.Create(stringReader))
                return XamlReader.Load(xmlReader);
        }

        private static object RenderBinary(SmartNumber smartNumber)
        {
            string xamlContent = smartNumber.ToString();

            xamlContent = xamlContent.Replace("1", @"<Run Foreground = ""Black"">1</Run>");

            string xamlTextBlock = @"
            <TextBlock
                xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                Padding = ""5""
                VerticalAlignment = ""Center""
                HorizontalAlignment = ""Stretch""
                Foreground = ""Gray"">

                {0}

            </TextBlock>";

            xamlTextBlock = string.Format(xamlTextBlock, xamlContent);

            using (StringReader stringReader = new StringReader(xamlTextBlock))
            using (XmlReader xmlReader = XmlReader.Create(stringReader))
                return XamlReader.Load(xmlReader);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("This converter cannot be used in two-way binding.");
        }
    }
}
