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

            actual.RootX.Should().NotBe(default);
            actual.RootY.Should().NotBe(default);
            actual.InitialCameraX.Should().NotBe(default);
            actual.InitialCameraY.Should().NotBe(default);
            actual.OriginalDocumentHeight.Should().NotBe(default);
            actual.OriginalDocumentWidth.Should().NotBe(default);
            actual.BackgroundColor.Should().NotBe(default);

            actual.Panels.Should().HaveCount(1);

            var rootPanel = actual.Panels[0];

            rootPanel.Should().NotBeNull();
            rootPanel.PanelId.Should().NotBeEmpty();
            rootPanel.PanelName.Should().NotBeEmpty();
            rootPanel.PanelHeight.Should().NotBe(default);
            rootPanel.PanelWidth.Should().NotBe(default);
            rootPanel.AttachedPanels.Should().HaveCount(4);
            rootPanel.AttachedPanels[0].AttachedPanels.Should().HaveCount(2);
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