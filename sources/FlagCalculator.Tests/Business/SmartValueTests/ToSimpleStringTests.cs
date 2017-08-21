using DustInTheWind.FlagCalculator.Business;
using NUnit.Framework;

namespace DustInTheWind.FlagCalculator.Tests.Business.SmartValueTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ToSimpleStringTests
    {
        [Test]
        public void render_in_base2_with_BitCount_0()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Binary,
                BitCount = 0
            };

            string actual = smartValue.ToSimpleString();

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

            string actual = smartValue.ToSimpleString();

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

            string actual = smartValue.ToSimpleString();

            Assert.That(actual, Is.EqualTo("00001101"));
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

            string actual = smartValue.ToSimpleString();

            Assert.That(actual, Is.EqualTo("1101"));
        }
    }
}
