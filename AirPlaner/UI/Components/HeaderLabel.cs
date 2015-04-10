using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;

namespace AirPlaner.UI.Components
{
    public class HeaderLabel : Label
    {
        public AirPlanerGame Game { get; set; }

        public HeaderLabel(Manager manager, AirPlanerGame game) : base(manager)
        {
            Game = game;
        }

        public override void Init()
        {
            base.Init();
            Skin.Layers[0].Text.Font.Resource = Game.Content.Load<SpriteFont>("headerfont");
        }
    }
}
