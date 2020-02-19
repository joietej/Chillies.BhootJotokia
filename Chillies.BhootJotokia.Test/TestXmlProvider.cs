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
        public async void LoadXmlAsAsync_Should_Deserialize_Xml_To_Object_WhenPathIsValid()
        {
            // Arrange
            var path = Path.GetFullPath($"../../../../Assets/BeerPack.xml");

            // Act
            var actual = await new XmlProvider().LoadXmlAsAsync<Folding>(path);

            // Assert
            actual.Should().NotBeNull();
        }

        [Fact]
        public async void LoadXmlAsAsync_Should_ThrowException_WhenPathIsInvalid()
        {
            // Arrange
            var path = Path.GetFullPath($"../../../../Assets/Foo.xml");

            // Act & Assert
            await Assert.ThrowsAsync<FileNotFoundException>(async () => await new XmlProvider().LoadXmlAsAsync<Folding>(path));
        }
    }
}