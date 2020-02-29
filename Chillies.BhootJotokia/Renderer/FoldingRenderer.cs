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

            var rootState = canvas.Save();

            RenderChildPanels(canvas, rootPanel, new Stack<GraphicsState>(new[] { rootState }), rootState);

            return img;
        }

        private void RenderChildPanels(Graphics canvas, Panel parentPanel, Stack<GraphicsState> parentStates, GraphicsState rootState)
        {
            foreach (var childPanel in parentPanel.AttachedPanels)
            {
                switch (childPanel.AttachedToSide)
                {
                    case 1:
                        DrawRightPanel(canvas, parentPanel, childPanel);
                        break;

                    case 2:
                        DrawTopPanel(canvas, parentPanel, childPanel);
                        break;

                    case 3:
                        DrawLeftPanel(canvas, parentPanel, childPanel);
                        break;

                    case 0:
                        DrawBasePanel(canvas, parentPanel, childPanel);
                        break;

                    default:
                        throw new System.ArgumentOutOfRangeException(nameof(childPanel.AttachedToSide));
                };

                parentStates.Push(canvas.Save());

                if (childPanel.AttachedPanels?.Any() == true)
                {
                    RenderChildPanels(canvas, childPanel, parentStates, rootState);
                }
                else
                {
                    parentStates.Pop();
                }

                if (parentStates.Any())
                {
                    canvas.Restore(parentStates.Pop());
                }
                else
                {
                    canvas.Restore(rootState);
                }
            }
        }

        private void DrawLeftPanel(Graphics canvas, Panel parentPanel, Panel childPanel)
        {
            //g.TranslateTransform(p.Width, 0);

            canvas.DrawRectangle(new Pen(Color.Orange), 0, 0, childPanel.PanelWidth, childPanel.PanelHeight);

            if (childPanel.AttachedPanels?.Any() == true)
            {
                canvas.RotateTransform(90F);
                //g.TranslateTransform(0, -childPanel.Height);
            }
        }

        private void DrawBasePanel(Graphics canvas, Panel parentPanel, Panel childPanel)
        {
            canvas.TranslateTransform(0, childPanel.PanelHeight);

            canvas.DrawRectangle(new Pen(Color.Yellow), 0, 0, childPanel.PanelWidth, childPanel.PanelHeight);
        }

        private static void DrawTopPanel(Graphics canvas, Panel parentPanel, Panel childPanel)
        {
            canvas.TranslateTransform(0, -childPanel.PanelHeight);

            canvas.DrawRectangle(new Pen(Color.Blue), 0, 0, childPanel.PanelWidth, childPanel.PanelHeight);
        }

        private static void DrawRightPanel(Graphics canvas, Panel parentPanel, Panel childPanel)
        {
            canvas.TranslateTransform(parentPanel.PanelWidth, 0);

            if (childPanel.AttachedPanels?.Any() == true)
            {
                canvas.RotateTransform(90F);
                canvas.TranslateTransform(0, -childPanel.PanelHeight);
            }

            canvas.DrawRectangle(new Pen(Color.Green), 0, 0, childPanel.PanelWidth, childPanel.PanelHeight);
        }
    }
}