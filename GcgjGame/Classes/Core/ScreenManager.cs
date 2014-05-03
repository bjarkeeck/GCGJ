using GcgjGame.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GcgjGame.Classes.Core
{
    public class ScreenManager
    {
        public static bool EnableEditor = true;


        public static GraphicsDevice GraphicsDevice;
        private static GameWindow Window;
        private static SpriteBatch spriteBatch;


        private static int screenHeight;
        private static int screenWidth;
        public static int ScreenHeight
        {
            get
            {

                if (Window.ClientBounds.Height != screenHeight)
                {
                    screenHeight = Window.ClientBounds.Height;
                    if (OnResize != null)
                        OnResize();
                }
                return screenHeight;

            }
        }
        public static int ScreenWidth
        {
            get
            {
                if (screenWidth != Window.ClientBounds.Width)
                {
                    screenWidth = Window.ClientBounds.Width;
                    if (OnResize != null)
                        OnResize();
                }
                return screenWidth;
            }
        }

        public static Action OnResize;

        public static List<Screen> ActiveScreens = new List<Screen>();
        public static ContentManager ContentManager;

        public static T GetScreen<T>() where T : Screen
        {
            return (T)ActiveScreens.FirstOrDefault(x => x.GetType() == typeof(T));
        }

        public static void Initialize(GraphicsDevice graphicsDevice, GameWindow window, ContentManager contentManager)
        {
            ContentManager = contentManager;
            Window = window;
            GraphicsDevice = graphicsDevice;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadScreen(new GameScreen());
            if (EnableEditor)
                LoadScreen(new EditorScreen());


        }

        public static void Update(GameTime gameTime)
        {
            InputHelper.Update();
            var a = ScreenHeight;
            var b = ScreenWidth;
            Time.Update(gameTime);
            foreach (Screen screen in ActiveScreens)
                screen.Update();
        }

        public static void LoadScreen(Screen screen)
        {
            ActiveScreens.Add(screen);
        }

        public static void Draw()
        {
            foreach (Screen screen in ActiveScreens)
            {
                screen.UpdateRenderTarget();
                GraphicsDevice.SetRenderTarget(screen.RenderTarget);
                GraphicsDevice.Clear(Color.Transparent);
                screen.Draw(spriteBatch);
            }

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            foreach (Screen screen in ActiveScreens)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(screen.RenderTarget, screen.Rectangle, Color.White);
                spriteBatch.End();
            }

        }
    }
}
