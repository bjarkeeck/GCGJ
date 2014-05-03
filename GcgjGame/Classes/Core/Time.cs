using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GcgjGame.Classes.Core
{
    public static class Time
    {
        public static float TimeSpeed { get; set; }

        public static float DeltaTime { get; private set; }

        public static float DeltaTimeMilliseconds { get; private set; }

        public static float RealDeltaTime { get; private set; }

        public static float RealDeltaTimeMilliseconds { get; private set; }

        public static float TotalTime { get; private set; }

        public static TimeSpan RealDeltaTimeSpan { get; private set; }

        public static TimeSpan TotalTimeSpan { get; private set; }

        internal static void Update(GameTime gameTime)
        {
            RealDeltaTimeMilliseconds = gameTime.ElapsedGameTime.Milliseconds;
            RealDeltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;
            DeltaTime = RealDeltaTime * TimeSpeed;
            DeltaTimeMilliseconds = RealDeltaTimeMilliseconds * TimeSpeed;
            TotalTime = (float)gameTime.TotalGameTime.TotalMilliseconds / 1000f;
            RealDeltaTimeSpan = gameTime.ElapsedGameTime;
            TotalTimeSpan = gameTime.TotalGameTime;
        }
    }
}
