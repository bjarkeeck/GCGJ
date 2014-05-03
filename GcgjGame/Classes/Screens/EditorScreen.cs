using GcgjGame.Classes.Core;
using GcgjGame.Classes.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GcgjGame.Classes.Screens
{
    public class EditorScreen : Screen
    {
        public LevelData LevelData
        {
            get
            {
                return ScreenManager.GetScreen<GameScreen>().LevelData;
            }
        }
        private GameObject selectedGameObject;
        private Texture2D selectedTexture;
        private List<GameObject> gameObjects = new List<GameObject>();
        private GameScreen gameScreen
        {
            get
            {
                return ScreenManager.GetScreen<GameScreen>();
            }
        }

        public EditorScreen()
        {
            ScreenManager.OnResize += () =>
            {
                this.Rectangle = new Rectangle(ScreenManager.ScreenWidth - 32, 0, 32, ScreenManager.ScreenHeight);
                ScreenManager.GetScreen<GameScreen>().Rectangle.Width = ScreenManager.ScreenWidth - 32;
            };
            selectedTexture = ScreenManager.ContentManager.Load<Texture2D>("Images/Selected");

            InputHelper.RegisterShortcut("saveChanges", SaveChanges, Keys.LeftControl, Keys.S);

            Vector2 position = Vector2.Zero;
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(GameObject).IsAssignableFrom(x) && x != typeof(GameObject)))
            {
                GameObject go = (GameObject)Activator.CreateInstance(type);
                go.Initialize(position);
                go.LoadContent(ScreenManager.ContentManager);
                gameObjects.Add(go);
                position.Y += 32;
            }

            selectedGameObject = gameObjects.First();
        }

        Type deletingType;

        public override void Update()
        {
            foreach (GameObject go in gameObjects)
            {
                if (InputHelper.MouseLeft)
                {
                    if (go.Rectangle.Contains(new Vector2(InputHelper.MousePosition.X - Rectangle.X, InputHelper.MousePosition.Y)))
                        selectedGameObject = go;

                    if (gameScreen.Rectangle.Contains(InputHelper.MousePosition))
                    {
                        Vector2 position = new Vector2((int)(InputHelper.MousePosition.X / 32f) * 32, (int)(InputHelper.MousePosition.Y / 32f) * 32);
                        GameObject gameObject = gameScreen.LevelData.GameObjects.FirstOrDefault(x => x.Position == position && x.ZIndex == selectedGameObject.ZIndex);
                        if (gameObject == null)
                            gameScreen.LevelData.CreateGameObject((GameObject)Activator.CreateInstance(selectedGameObject.GetType()), position);
                        else
                        {
                            if (gameObject.GetType() != go.GetType())
                            {
                                if (gameObject != null)
                                    gameScreen.LevelData.GameObjects.Remove(gameObject);
                                gameScreen.LevelData.CreateGameObject((GameObject)Activator.CreateInstance(selectedGameObject.GetType()), position);
                            }
                        }
                    }
                }

            }

            if (InputHelper.MouseRightDown)
            {
                if (gameScreen.Rectangle.Contains(InputHelper.MousePosition))
                {
                    Vector2 position = new Vector2((int)(InputHelper.MousePosition.X / 32f) * 32, (int)(InputHelper.MousePosition.Y / 32f) * 32);
                    GameObject gameObject = gameScreen.LevelData.GameObjects.OrderByDescending(x => x.ZIndex).FirstOrDefault(x => x.Position == position);
                    if (gameObject != null)
                        deletingType = gameObject.GetType();
                }
            }

            if (InputHelper.MouseRight)
            {
                if (gameScreen.Rectangle.Contains(InputHelper.MousePosition))
                {
                    Vector2 position = new Vector2((int)(InputHelper.MousePosition.X / 32f) * 32, (int)(InputHelper.MousePosition.Y / 32f) * 32);
                    GameObject gameObject = gameScreen.LevelData.GameObjects.FirstOrDefault(x => x.Position == position && x.GetType() == deletingType);
                    if (gameObject != null)
                        gameScreen.LevelData.GameObjects.Remove(gameObject);
                }
            }
        }


        public void SaveChanges()
        {
            Serializer.SerializeLevel(ScreenManager.GetScreen<GameScreen>().LevelData);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.Draw(selectedTexture, selectedGameObject.Position, Color.White);
        }

    }
}
