using Chillies.BhootJotokia.Models;
using System.Drawing;

namespace Chillies.BhootJotokia.Renderer
{
    public class FoldingRenderer
    {
        public Bitmap Render(Folding folding)
        {
            var img = new Bitmap(folding.OriginalDocumentWidth, folding.OriginalDocumentHeight, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
            var g = Graphics.FromImage(img);

            var rootPanel = folding.Panels[0];

            g = RenderPanel((int)folding.RootX, (int)folding.RootY, rootPanel, g);

            return img;
        }

        private Graphics RenderPanel(int X, int Y, Panel panel, Graphics g)
        {
            var rect = new Rectangle(X, Y, (int)panel.PanelWidth, (int)panel.PanelHeight);
            g.DrawRectangle(new Pen(Color.Blue), rect);
            return g;
        }
    }
}