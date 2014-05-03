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

        public static Vector2 CameraPosition;

        public override void Update()
        {

            foreach (GameObject go in LevelData.GameObjects.ToList())
                go.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(0, null, null, null, null, null, GetTransform());
            foreach (GameObject go in LevelData.GameObjects.OrderBy(x => x.ZIndex).ToList())
                go.Draw(spriteBatch);
            spriteBatch.End();
        }


        public Matrix GetTransform()
        {
            var translationMatrix = Matrix.CreateTranslation(new Vector3(CameraPosition.X, CameraPosition.Y, 0));
            return translationMatrix;
        }
    }
}
