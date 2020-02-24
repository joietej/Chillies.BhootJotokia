using Chillies.BhootJotokia.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Chillies.BhootJotokia.Models
{
    [XmlType("item")]
    public readonly struct Panel
    {
        public Panel(Guid panelId,
                string panelName,
                float panelWidth,
                float panelHeight,
                int attachedToSide,
                float minRot = 0,
                float maxRot = 0,
                float initialRot = 0,
                float startRot = 0,
                float endRot = 0,
                float hingeOffset = 0,
                float creaseBottom = 0,
                float creaseTop = 0,
                float creaseLeft = 0,
                float creaseRight = 0,
                bool ignoreCollisions = false,
                bool mouseEnabled = true,
                Panel[]? attachedPanels = null)
        {
            this.PanelId = panelId;
            this.PanelName = panelName;
            this.MinRot = minRot;
            this.MaxRot = maxRot;
            this.InitialRot = initialRot;
            this.StartRot = startRot;
            this.EndRot = endRot;
            this.HingeOffset = hingeOffset;
            this.PanelWidth = panelWidth;
            this.PanelHeight = panelHeight;
            this.AttachedToSide = attachedToSide;
            this.CreaseBottom = creaseBottom;
            this.CreaseTop = creaseTop;
            this.CreaseLeft = creaseLeft;
            this.CreaseRight = creaseRight;
            this.IgnoreCollisions = ignoreCollisions;
            this.MouseEnabled = mouseEnabled;
            this.AttachedPanels = attachedPanels ?? new Panel[] { };
        }

        public Guid PanelId { get; }
        public string PanelName { get; }
        public float MinRot { get; }
        public float MaxRot { get; }
        public float InitialRot { get; }
        public float StartRot { get; }
        public float EndRot { get; }
        public float HingeOffset { get; }
        public float PanelWidth { get; }
        public float PanelHeight { get; }
        public int AttachedToSide { get; }
        public float CreaseBottom { get; }
        public float CreaseTop { get; }
        public float CreaseLeft { get; }
        public float CreaseRight { get; }
        public bool IgnoreCollisions { get; }
        public bool MouseEnabled { get; }

        public Panel[] AttachedPanels { get; }

        public static Panel[] Load(XElement root, string elementName) =>
              root.Element(XName.Get(elementName))
              .Elements()
              .Select(p => new Panel(p.AtrAsGuid("panelId"),
                                      p.Atr("panelName"),
                                      p.AtrAsFloat("panelWidth"),
                                      p.AtrAsFloat("panelHeight"),
                                      p.AtrAsInt("attachedToSide"),
                                      attachedPanels: Panel.Load(p, "attachedPanels")))
              .ToArray();
    }
}