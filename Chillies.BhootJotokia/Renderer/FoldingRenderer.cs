using Chillies.BhootJotokia.Models;
using System.Drawing;

namespace Chillies.BhootJotokia.Renderer
{
    public class FoldingRenderer
    {
        public Bitmap Render(Folding folding)
        {
            var img = new Bitmap(folding.OriginalDocumentWidth, folding.OriginalDocumentHeight, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);

            var canvas = Graphics.FromImage(img);

            var rootPanel = folding.Panels[0];

            var (rootX, rootY) = (folding.RootX - (rootPanel.PanelWidth / 2), folding.RootY - rootPanel.PanelHeight);

            RenderPanel(canvas, rootX, rootY, rootPanel.PanelWidth, rootPanel.PanelHeight, rootPanel.AttachedPanels, 2, 1);

            return img;
        }

        private void RenderPanel(Graphics canvas, float x, float y, float panelWidth, float panelHeight, Panel[] attachedPanels, int maxlevel, int clevel)
        {
            canvas.DrawRectangle(new Pen(Brushes.Blue), x, y, panelWidth, panelHeight);

            if (clevel == maxlevel) return;

            foreach (var childPanel in attachedPanels)
            {
                var (nx, ny, width, height) = getChildPanelDimentions(childPanel.AttachedToSide, x, y, (int)childPanel.PanelWidth, (int)childPanel.PanelHeight, (int)panelWidth, (int)panelHeight);

                RenderPanel(canvas, nx, ny, width, height, childPanel.AttachedPanels, maxlevel, clevel + 1);
            }
        }

        private (float x, float y, float width, float height) getChildPanelDimentions(int attachedSide,
                                                                float rootX,
                                                                float rootY,
                                                                float width,
                                                                float height,
                                                                float parentWidth,
                                                                float parentHeight)
        {
            return attachedSide switch
            {
                1 => (rootX + parentWidth, rootY, height, width),
                2 => (rootX, (rootY - height), width, height),
                3 => (rootX - height, rootY, height, width),
                0 => (rootX, rootY + parentHeight, width, height),
                _ => throw new System.ArgumentOutOfRangeException(nameof(attachedSide))
            };
        }
    }
}