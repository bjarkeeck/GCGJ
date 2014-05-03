using GcgjGame.Classes.GameObjects;
using GcgjGame.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GcgjGame.Classes.Core
{
    public abstract class GameObject
    {
        public Rectangle Rectangle;
        public Vector2 Position;
        public LevelData LevelData;
        public string Name = null;
        public int ZIndex = 0;


        public virtual void Initialize(Vector2 position)
        {

        }

        public virtual void LoadContent(ContentManager content)
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public T CreateGameObject<T>(T go, Vector2 position) where T : GameObject
        {
            return LevelData.CreateGameObject(go, position);
        }

        public T CreateGameObject<T>(Vector2 position) where T : GameObject
        {
            return CreateGameObject((T)Activator.CreateInstance(typeof(T)), position);
        }

    }
}
