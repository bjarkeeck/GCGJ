using GcgjGame.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GcgjGame.Classes.GameObjects
{
    public class TerrainDirt : GameObject
    {
        [XmlIgnore]
        public Texture2D Texture;

        public override void Initialize(Vector2 position)
        {
            this.Rectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            this.Position = position;
            ZIndex = 1;
        }
        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Images/Dirt");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, this.Rectangle, Color.White);
        }
    }
}
