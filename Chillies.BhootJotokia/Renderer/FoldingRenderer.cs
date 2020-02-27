using Chillies.BhootJotokia.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

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

            canvas.TranslateTransform(rootX, rootY);

            canvas.DrawRectangle(new Pen(Color.Blue), 0, 0, rootPanel.PanelWidth, rootPanel.PanelHeight);

            RenderChildPanels(canvas, rootPanel, new Stack<GraphicsState>(new[] { canvas.Save() }));

            return img;
        }

        private void RenderChildPanels(Graphics g, Panel p, Stack<GraphicsState> parentStates)
        {
            foreach (var childPanel in p.AttachedPanels)
            {
                childPanel.AttachedToSide switch
                {
                    1 => DrawRightPanel(g, p, childPanel),
                    2 => DrawTopPanel(g, p, childPanel),
                    3 => DrawLeftPanel(g, p, childPanel),
                    0 => DrawBasePanel(g, p, childPanel),
                    _ => throw new System.ArgumentOutOfRangeException(nameof(childPanel.AttachedToSide))
                };
            }
        }

        private void DrawLeftPanel(Graphics g, Panel p, Panel childPanel)
        {
            //g.TranslateTransform(p.Width, 0);

            g.DrawRectangle(new Pen(Color.Orange), 0, 0, childPanel.PanelWidth, childPanel.PanelHeight);

            if (childPanel.AttachedPanels?.Any() == true)
            {
                g.RotateTransform(90F);
                //g.TranslateTransform(0, -childPanel.Height);
            }

        }

        private void DrawBasePanel(Graphics g, Panel p, Panel childPanel)
        {
            throw new NotImplementedException();
        }

        private static void DrawTopPanel(Graphics g, Panel p, Panel childPanel)
        {
            g.TranslateTransform(0, -childPanel.PanelHeight);

            g.DrawRectangle(new Pen(Color.Blue), 0, 0, childPanel.PanelWidth, childPanel.PanelHeight);
        }

        private static void DrawRightPanel(Graphics g, Panel p, Panel childPanel)
        {
            g.TranslateTransform(p.PanelWidth, 0);

            if (childPanel.AttachedPanels?.Any() == true)
            {
                g.RotateTransform(90F);
                g.TranslateTransform(0, -childPanel.PanelHeight);
            }

            g.DrawRectangle(new Pen(Color.Green), 0, 0, childPanel.PanelWidth, childPanel.PanelHeight);
        }
    }
}