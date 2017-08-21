using DustInTheWind.FlagCalculator.Business;
using NUnit.Framework;

namespace DustInTheWind.FlagCalculator.Tests.Business.SmartValueTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ToStringTests
    {
        #region Base 2

        [Test]
        public void render_in_base2_with_BitCount_0()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Binary,
                BitCount = 0
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("1101"));
        }

        [Test]
        public void render_in_base2_with_BitCount_less_then_real_digit_count()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Binary,
                BitCount = 2
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("1101"));
        }

        [Test]
        public void render_in_base2_with_BitCount_more_then_real_digit_count()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Binary,
                BitCount = 8
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("0000 1101"));
        }

        [Test]
        public void render_in_base2_with_BitCount_more_then_real_digit_count_no_left_padding()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Binary,
                BitCount = 8,
                PadLeft = false
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("1101"));
        }

        #endregion

        #region Base 10

        [Test]
        public void render_in_base10_with_BitCount_0()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Decimal,
                BitCount = 0
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("13"));
        }

        [Test]
        public void render_in_base10_with_BitCount_less_then_real_digit_count()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Decimal,
                BitCount = 2
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("13"));
        }

        [Test]
        public void render_in_base10_with_BitCount_more_then_real_digit_count()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Decimal,
                BitCount = 16
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("13"));
        }

        [Test]
        public void render_in_base10_with_BitCount_more_then_real_digit_count_no_left_padding()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Decimal,
                BitCount = 16,
                PadLeft = false
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("13"));
        }

        #endregion

        #region Base 16

        [Test]
        public void render_in_base16_with_BitCount_0()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Hexadecimal,
                BitCount = 0
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("D"));
        }

        [Test]
        public void render_in_base16_with_BitCount_less_then_real_digit_count()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Hexadecimal,
                BitCount = 2
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("D"));
        }

        [Test]
        public void render_in_base16_with_BitCount_more_then_real_digit_count()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Hexadecimal,
                BitCount = 16
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("00 0D"));
        }

        [Test]
        public void render_in_base16_with_BitCount_more_then_real_digit_count_no_left_padding()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Hexadecimal,
                BitCount = 16,
                PadLeft = false
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("D"));
        }

        #endregion
    }
}
