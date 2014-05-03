using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GcgjGame.Classes.Core
{
    [Serializable]
    public class LevelData
    {
        public Vector2 TileSize = new Vector2(32, 32);

        public List<GameObject> GameObjects = new List<GameObject>();

        public T CreateGameObject<T>(T go, Vector2 position) where T : GameObject
        {
            go.Initialize(position);
            go.LoadContent(ScreenManager.ContentManager);
            go.LevelData = this;
            go.LevelData.GameObjects.Add(go);
            return go;
        }
    }
}
