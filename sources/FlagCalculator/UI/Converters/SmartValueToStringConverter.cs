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
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI.Converters
{
    public class SmartValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SmartValue)
                return ConvertFromSmartNumber(value);

            return null;
        }

        private static object ConvertFromSmartNumber(object value)
        {
            SmartValue smartValue = (SmartValue)value;

            string xamlContent = smartValue.ToString();

            if (smartValue.NumericalBase != NumericalBase.Binary)
                return xamlContent;

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
