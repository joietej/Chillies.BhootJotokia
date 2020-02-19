using Chillies.BhootJotokia.Core;
using Chillies.BhootJotokia.Models;
using FluentAssertions;
using System.IO;
using Xunit;

namespace Chillies.BhootJotokia.Test
{
    public class TestXmlProvider
    {
        [Fact]
        public async void LoadXmlAsAsync_Should_Deserialize_Xml_To_Object()
        {
            // Arrange
            var path = Path.GetFullPath($"../../../../Assets/BeerPack.xml");

            // Act
            var actual = await new XmlProvider().LoadXmlAsAsync<Folding>(path);

            //Assert
            actual.Should().NotBeNull();
        }
    }
}