using GcgjGame.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GcgjGame.Classes.GameObjects
{
    public class TerrainGrass : GameObject
    {
        public Texture2D Texture;

        public override void Initialize(Vector2 position)
        {
            this.Rectangle = new Rectangle(0, 0, 1920, 1080);
            this.Position = position;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Images/MassiveGrass");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Camera2D cam = new Camera2D();

            int camX = (int)cam.Position.X % 1920;
            int camY = (int)cam.Position.X % 1080;
            int camxX = (int)cam.Position.X / 1920;
            int camxY = (int)cam.Position.X / 1080;



            if (camX == 960)
            {
                if (camY == 590)
                {
                    spriteBatch.Draw(Texture, new Rectangle(1920 * camxX, 1080 * camxY, 1920, 1080), Color.White);
                }
                else if (camY < 590)
                {
                    spriteBatch.Draw(Texture, new Rectangle(1920 * camxX, 1080 * camxY, 1920, 1080), Color.White);
                    spriteBatch.Draw(Texture, new Rectangle(1920 * camxX, 1080 * camxY, 1920, 1080), Color.White);
                    spriteBatch.Draw(Texture, new Rectangle(1920 * camxX, 1080 * camxY, 1920, 1080), Color.White);
                }
                else
                {
                    spriteBatch.Draw(Texture, new Rectangle(1920 * camxX, 1080 * camxY, 1920, 1080), Color.White);
                    spriteBatch.Draw(Texture, new Rectangle(1920 * camxX, 1080 * camxY, 1920, 1080), Color.White);
                    spriteBatch.Draw(Texture, new Rectangle(1920 * camxX, 1080 * camxY, 1920, 1080), Color.White);
                }
            }
            else if (camX < 960)
            {
                if (camY == 590)
                {
                    spriteBatch.Draw(Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                }
                else if (camY < 590)
                {
                    spriteBatch.Draw(Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.Draw(Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                }
                else
                {
                    spriteBatch.Draw(Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.Draw(Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                }
            }
            else
            {
                if (camY == 590)
                {
                    spriteBatch.Draw(Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                }
                else if (camY < 590)
                {
                    spriteBatch.Draw(Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.Draw(Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                }
                else
                {
                    spriteBatch.Draw(Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.Draw(Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                }
            }
        }

    }
}
