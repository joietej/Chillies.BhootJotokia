using Chillies.BhootJotokia.Models;
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

            RenderPanel(canvas, rootPanel, null, new Stack<GraphicsState>());

            return img;
        }

        private void RenderPanel(Graphics canvas, Panel panel, Panel? parent, Stack<GraphicsState> parentStates)
        {
            var currentState = DrawPanel(canvas, panel, parent);

            if (panel.AttachedPanels?.Any() == true)
            {
                foreach (var childPanel in panel.AttachedPanels)
                {
                    parentStates.Push(currentState);

                    RenderPanel(canvas, childPanel, panel, parentStates);

                    if (parentStates.Any())
                    {
                        canvas.Restore(parentStates.Pop());
                    }
                }
            }
        }

        private GraphicsState DrawPanel(Graphics canvas, Panel panel, Panel? parent)
        {
            GraphicsState current;

            if (parent == null)
            {
                current = DrawRootPanel(canvas, panel);
            }
            else
            {
                current = panel.AttachedToSide switch
                {
                    1 => DrawRightPanel(canvas, panel, parent.Value),
                    2 => DrawTopPanel(canvas, panel, parent.Value),
                    3 => DrawLeftPanel(canvas, panel, parent.Value),
                    0 => DrawDownPanel(canvas, panel, parent.Value),
                    _ => throw new System.ArgumentOutOfRangeException(nameof(panel.AttachedToSide))
                };
            }

            return current;
        }

        private GraphicsState DrawRootPanel(Graphics canvas, Panel panel)
        {
            
            canvas.DrawRectangle(new Pen(Color.Blue), 0, 0, panel.PanelWidth, panel.PanelHeight);

            return canvas.Save();
        }

        private GraphicsState DrawLeftPanel(Graphics canvas, Panel panel, Panel parentPanel)
        {
            canvas.TranslateTransform(-panel.PanelWidth, 0);

            canvas.DrawRectangle(new Pen(Color.Orange), 0, 0, panel.PanelWidth, panel.PanelHeight);

            if (panel.AttachedPanels?.Any() == true)
            {
                canvas.RotateTransform(90F);
                canvas.TranslateTransform(panel.PanelHeight,0);
            }

            return canvas.Save();
        }

        private GraphicsState DrawDownPanel(Graphics canvas, Panel panel, Panel parentPanel)
        {
            canvas.TranslateTransform(0, panel.PanelHeight);

            canvas.DrawRectangle(new Pen(Color.Yellow), 0, 0, panel.PanelWidth, panel.PanelHeight);

            return canvas.Save();
        }

        private GraphicsState DrawTopPanel(Graphics canvas, Panel panel, Panel parentPanel)
        {
            canvas.TranslateTransform(0, -panel.PanelHeight);

            canvas.DrawRectangle(new Pen(Color.Blue), 0, 0, panel.PanelWidth, panel.PanelHeight);

            return canvas.Save();
        }

        private GraphicsState DrawRightPanel(Graphics canvas, Panel panel, Panel parentPanel)
        {
            canvas.TranslateTransform(parentPanel.PanelWidth, 0);

            if (panel.AttachedPanels?.Any() == true)
            {
                canvas.RotateTransform(90F);
                canvas.TranslateTransform(0, -panel.PanelHeight);
            }

            canvas.DrawRectangle(new Pen(Color.Green), 0, 0, panel.PanelWidth, panel.PanelHeight);

            return canvas.Save();
        }
    }
}