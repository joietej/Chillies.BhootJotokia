using Chillies.BhootJotokia.Extensions;
using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Chillies.BhootJotokia.Models
{
    [Serializable]
    [XmlRoot(ElementName = "folding")]
    public readonly struct Folding : IXmlSerializable
    {
        public Folding(
                float rootX,
                float rootY,
                int originalDocumentHeight,
                int originalDocumentWidth,
                float initialCameraX,
                float initialCameraY,
                int backgroundColor,
                int initialCameraRadius,
                bool enableEffects = false,
                bool debugMode = false,
                bool showStats = false,
                bool calculatePanelCollisions = false,
                bool allowMouseInteraction = false,
                bool adjustCameraTargetPosition = false,
                bool freeCamera = false,
                int? startPosition = null,
                bool show3DStats = false,
                int? iconSetID = null,
                bool autoPlaySequence = false,
                bool loopSequence = false,
                int initialCameraTargetX = 0,
                int initialCameraTargetY = 0,
                int initialCameraTargetZ = 0,
                Panel[]? panels = null)
        {
            this.EnableEffects = enableEffects;
            this.DebugMode = debugMode;
            this.ShowStats = showStats;
            this.CalculatePanelCollisions = calculatePanelCollisions;
            this.AllowMouseInteraction = allowMouseInteraction;
            this.AdjustCameraTargetPosition = adjustCameraTargetPosition;
            this.FreeCamera = freeCamera;
            this.StartPosition = startPosition;
            this.InitialCameraX = initialCameraX;
            this.InitialCameraY = initialCameraY;
            this.Show3DStats = show3DStats;
            this.BackgroundColor = backgroundColor;
            this.RootX = rootX;
            this.RootY = rootY;
            this.OriginalDocumentHeight = originalDocumentHeight;
            this.OriginalDocumentWidth = originalDocumentWidth;
            this.InitialCameraRadius = initialCameraRadius;
            this.IconSetID = iconSetID;
            this.AutoPlaySequence = autoPlaySequence;
            this.LoopSequence = loopSequence;
            this.InitialCameraTargetX = initialCameraTargetX;
            this.InitialCameraTargetY = initialCameraTargetY;
            this.InitialCameraTargetZ = initialCameraTargetZ;
            this.Panels = panels ?? new Panel[] { };
        }

        public bool EnableEffects { get; }
        public bool DebugMode { get; }
        public bool ShowStats { get; }
        public bool CalculatePanelCollisions { get; }
        public bool AllowMouseInteraction { get; }
        public bool AdjustCameraTargetPosition { get; }
        public bool FreeCamera { get; }
        public int? StartPosition { get; }
        public float InitialCameraX { get; }
        public float InitialCameraY { get; }
        public bool Show3DStats { get; }
        public int BackgroundColor { get; }
        public float RootX { get; }
        public float RootY { get; }
        public int OriginalDocumentHeight { get; }
        public int OriginalDocumentWidth { get; }
        public int InitialCameraRadius { get; }
        public int? IconSetID { get; }
        public bool AutoPlaySequence { get; }
        public bool LoopSequence { get; }
        public int InitialCameraTargetX { get; }
        public int InitialCameraTargetY { get; }
        public int InitialCameraTargetZ { get; }

        public Panel[] Panels { get; }

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var root = XElement.Load(reader);

            (Unsafe.AsRef(this)) = (new Folding(root.AtrAsFloat("rootX"),
                                                root.AtrAsFloat("rootY"),
                                                root.AtrAsInt("originalDocumentHeight"),
                                                root.AtrAsInt("originalDocumentWidth"),
                                                root.AtrAsFloat("initialCameraX"),
                                                root.AtrAsFloat("initialCameraY"),
                                                root.AtrAsInt("backgroundColor"),
                                                root.AtrAsInt("initialCameraRadius"),
                                                panels: Panel.Load(root, "panels")));
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}