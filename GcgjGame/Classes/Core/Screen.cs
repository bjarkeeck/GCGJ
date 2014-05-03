using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GcgjGame.Classes.Core
{
    public abstract class Screen
    {
        public RenderTarget2D RenderTarget { get; private set; }
        public Rectangle Rectangle;

        public Screen()
        {
            Rectangle = new Rectangle(20, 20, 20, 20);
        }

        public virtual void Update()
        {

        }

        public void UpdateRenderTarget()
        {
            if (RenderTarget == null || Rectangle.Width != RenderTarget.Width || Rectangle.Height != RenderTarget.Height)
            {
                if (RenderTarget != null)
                    RenderTarget.Dispose();
                RenderTarget = new RenderTarget2D(ScreenManager.GraphicsDevice, Rectangle.Width, Rectangle.Height);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
