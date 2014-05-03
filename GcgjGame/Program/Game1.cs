#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using GcgjGame.Classes.Core;
#endregion

namespace GcgjGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            ScreenManager.Initialize(graphics.GraphicsDevice, this.Window, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            ScreenManager.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            ScreenManager.Draw();
        }
    }
}
