using GcgjGame.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GcgjGame.Classes.Core
{

    public class InputHelper {
        private static Keys[] m_lastKeyState = new Keys[] { },
                              m_curKeyState = new Keys[] { };

        private static MouseState m_lastMouseState,
                                  m_curMouseState;

        private static List<Shortcut> m_shortcuts = new List<Shortcut>();

        private static List<Keys> m_shortcutKeys = new List<Keys>();

        public static Vector2 MousePositionInWorld
        {
            get
            {
                return MousePosition - GameScreen.CameraPosition;
            }
        }

        public static Vector2 MousePosition {
            get {
                return new Vector2(m_curMouseState.X, m_curMouseState.Y);
            }
        }

        public static int MouseWheel
        {
            get
            {
                return m_curMouseState.ScrollWheelValue - m_lastMouseState.ScrollWheelValue;
            }
        }

        public static bool MouseHasMoved
        {
            get
            {
                return m_lastMouseState.X != m_curMouseState.X || m_lastMouseState.Y != m_curMouseState.Y;
            }
        }

        internal static void Update() {
            if (m_curKeyState != null)
                m_lastKeyState = m_curKeyState;

            m_curKeyState = Keyboard.GetState().GetPressedKeys();

            m_lastMouseState = m_curMouseState;
            m_curMouseState = Mouse.GetState();

            // Shortcuts!
            m_shortcutKeys.RemoveAll(n => !m_curKeyState.Contains(n));

            foreach(var key in m_curKeyState)
                if (!m_shortcutKeys.Contains(key))
                    m_shortcutKeys.Add(key);

            foreach (var shortcut in m_shortcuts.ToList())
            {
                if (shortcut.Keys == null || shortcut.Keys.Length == 0)
                    continue;

                if (m_shortcutKeys.Count == shortcut.Keys.Length)
                {
                    bool valid = true;

                    for (int i = 0; i < m_shortcutKeys.Count; i++)
                        if (m_shortcutKeys[i] != shortcut.Keys[i])
                            valid = false;

                    if (valid && GetKeyDown(shortcut.Keys.Last()) && shortcut.Action != null)
                        shortcut.Action();
                }
            }
        }

        public static bool MouseLeft
        {
            get
            {
                return m_curMouseState.LeftButton == ButtonState.Pressed;
            }
        }

        public static bool MouseLeftUp {
            get
            {
                return m_curMouseState.LeftButton == ButtonState.Released && m_lastMouseState.LeftButton == ButtonState.Pressed;
            }
        }

        public static bool MouseLeftDown {
            get
            {
                return m_curMouseState.LeftButton == ButtonState.Pressed && m_lastMouseState.LeftButton == ButtonState.Released;
            }
        }

        public static bool MouseMiddle {
            get
            {
                return m_curMouseState.MiddleButton == ButtonState.Pressed;
            }
        }

        public static bool MouseMiddleUp {
            get
            {
                return m_curMouseState.MiddleButton == ButtonState.Released && m_lastMouseState.MiddleButton == ButtonState.Pressed;
            }
        }

        public static bool MouseMiddleDown {
            get
            {
                return m_curMouseState.MiddleButton == ButtonState.Pressed && m_lastMouseState.MiddleButton == ButtonState.Released;
            }
        }

        public static bool MouseRight {
            get
            {
                return m_curMouseState.RightButton == ButtonState.Pressed;
            }
        }

        public static bool MouseRightUp {
            get
            {
                return m_curMouseState.RightButton == ButtonState.Released && m_lastMouseState.RightButton == ButtonState.Pressed;
            }
        }

        public static bool MouseRightDown {
            get
            {
                return m_curMouseState.RightButton == ButtonState.Pressed && m_lastMouseState.RightButton == ButtonState.Released;
            }
        }

        public static bool AnyKey {
            get {
                return m_curKeyState.Length > 0;
            }
        }

        public static bool AnyKeyDown {
            get {
                return m_curKeyState.Except(m_lastKeyState).Count() > 0;
            }
        }
		
		public static bool AnyKeyUp {
			get {
				return m_lastKeyState.Except(m_curKeyState).Count() > 0;
			}
        }

        public static bool GetKey(Keys key) {
            return m_curKeyState.Contains(key);
        }

        public static bool GetKeyDown(Keys key) {
            return m_curKeyState.Except(m_lastKeyState).Contains(key);
        }

        public static bool GetKeyUp(Keys key) {
            return m_lastKeyState.Except(m_curKeyState).Contains(key);
        }

        internal static void HookKeyboard(GameWindow window)
        {

            OpenTK.GameWindow OpenTKWindow = null;
            Type type = typeof(OpenTKGameWindow);

            System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (field != null)
            {
                OpenTKWindow = field.GetValue(window) as OpenTK.GameWindow;
            }

            if (OpenTKWindow != null)
            {
                OpenTKWindow.KeyPress += (sender, e) =>
                {
                    if (OnOpenTKWindowKeyPress != null)
                        OnOpenTKWindowKeyPress(sender, e);
                };
            }
        }
        internal static Action<object, OpenTK.KeyPressEventArgs> OnOpenTKWindowKeyPress = null;

        #region Shortcuts
        public static void RegisterShortcut(Shortcut shortcut)
        {
            if (!m_shortcuts.Any(n => n != shortcut && n.Name == shortcut.Name))
            {
                m_shortcuts.Add(shortcut);
            }

        }
        public static Shortcut RegisterShortcut(string name, Action action, params Keys[] keys)
        {
            if (keys == null)
            {
                keys = new Keys[] { };
            }

            if (!m_shortcuts.Any(n => n.Name == name))
            {
                Shortcut result = new Shortcut(name, action, keys);

                m_shortcuts.Add(result);

                return result;
            }


            return null;
        }

        public static Shortcut GetShortcut(string name)
        {
            return m_shortcuts.FirstOrDefault(n => n.Name == name);
        }

        public static List<Shortcut> GetShortcuts()
        {
            return new List<Shortcut>(m_shortcuts);
        }
        public static List<Shortcut> GetShortcuts(Keys[] keys)
        {
            return m_shortcuts.Where(n =>
            {
                if (n.Keys.Length != keys.Length)
                    return false;

                for (int i = 0; i < keys.Length; i++)
                    if (keys[i] != n.Keys[i])
                        return false;

                return true;
            }).ToList();
        }

        public static void ChangeShortcut(string name, Keys[] keys)
        {
            var shortcut = m_shortcuts.FirstOrDefault(n => n.Name == name);

            if (shortcut != null)
            {
                shortcut.Keys = keys;
            }
        }
        public static void ChangeShortcut(string name, Keys[] keys, Action action)
        {
            var shortcut = m_shortcuts.FirstOrDefault(n => n.Name == name);

            if (shortcut != null)
            {
                shortcut.Keys = keys;
                shortcut.Action = action;
            }
        }

        public static void DeregisterShortcut(string name)
        {
            Shortcut result = m_shortcuts.FirstOrDefault(n => n.Name == name);

            if (result != null)
            {
                m_shortcuts.Remove(result);
            }
            else
            {
            }
        }
        #endregion
    }
}

public class Shortcut
{
    public Shortcut(string name, Action action, params Keys[] keys)
    {
        Name = name;
        Keys = keys;
        Action = action;
    }

    public readonly string Name;

    public Keys[] Keys
    {
        get
        {
            return m_keys;
        }
        set
        {
            if (value == null)
                m_keys = new Keys[] { };
            else
                m_keys = value;
        }
    }

    public Action Action { get; set; }

    Keys[] m_keys;
}
