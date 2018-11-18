using System;
using FluentAssertions;
using NUnit.Framework;

namespace thegame.tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void DoSomething_WhenSomething()
        {
            1.Should().Be(1);
        }
    }
}
