using DustInTheWind.FlagCalculator.Business;
using NUnit.Framework;

namespace DustInTheWind.FlagCalculator.Tests.Business.ProjectContextTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class ConstructorTests
    {
        private ProjectContext projectContext;

        [SetUp]
        public void SetUp()
        {
            projectContext = new ProjectContext();
        }

        [Test]
        public void DisplaySelected_is_false()
        {
            Assert.That(projectContext.DisplaySelected, Is.False);
        }

        [Test]
        public void DisplayUnselected_is_false()
        {
            Assert.That(projectContext.DisplayUnselected, Is.False);
        }

        [Test]
        public void NumericalBaseService_is_not_null()
        {
            Assert.That(projectContext.NumericalBaseService, Is.Not.Null);
        }

        [Test]
        public void FlagsNumber_is_not_null()
        {
            Assert.That(projectContext.FlagsNumber, Is.Not.Null);
        }
    }
}
