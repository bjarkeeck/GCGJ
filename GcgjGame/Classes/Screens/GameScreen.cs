using GcgjGame.Classes.Core;
using GcgjGame.Classes.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GcgjGame.Classes.Screens
{
    public class GameScreen : Screen
    {
        public LevelData LevelData;
        public GameScreen()
        {
            LevelData = Serializer.DeserializeLevel();

            foreach (GameObject g in LevelData.GameObjects)
            {
                g.LoadContent(ScreenManager.ContentManager);
            }

            ScreenManager.OnResize += () =>
            {
                this.Rectangle = new Rectangle(0, 0, ScreenManager.ScreenWidth, ScreenManager.ScreenHeight);
            };
        }

        public override void Update()
        {
            foreach (GameObject go in LevelData.GameObjects.ToList())
                go.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in LevelData.GameObjects.OrderBy(x => x.ZIndex).ToList())
                go.Draw(spriteBatch);
        }

    }
}
