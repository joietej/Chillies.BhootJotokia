using Chillies.BhootJotokia.Core;
using Chillies.BhootJotokia.Models;
using Chillies.BhootJotokia.Renderer;
using System.IO;
using Xunit;

namespace Chillies.BhootJotokia.Test
{
    public class TestHostRenderer
    {
        [Fact]
        public async void Render_Should_DrawRectangleAsync()
        {
            // Arrnage
            var path = Path.GetFullPath($"../../../../Assets/BeerPack.xml");
            var folding = await new XmlProvider().LoadXmlAsAsync<Folding>(path);

            // Act
            var image = new FoldingRenderer().Render(folding);

            // Assert
            image.Save(Path.GetFullPath($"../../../../Assets/img.bmp"));
        }
    }
}