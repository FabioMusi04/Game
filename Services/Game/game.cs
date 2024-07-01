using System;
using System.Threading;
using GameProject.Services.Window;
using System.Diagnostics;

namespace GameProject.Services.Game
{
   public class Game
   {
      readonly GameWindow window;
      private readonly GamePanel panel;
      private Thread gameThread;
      private const int FPS_TARGET = 120;
      public Game()
      {
         panel = new GamePanel();
         StartGameLoop();

         window = new GameWindow(panel);
      }

      private void StartGameLoop()
      {
         Console.WriteLine("Starting game loop");
         gameThread = new Thread(Start);
         gameThread.Start();
      }

      private static long NanoTime()
      {
         long nano = 10000L * Stopwatch.GetTimestamp();
         nano /= TimeSpan.TicksPerMillisecond;
         nano *= 100L;
         return nano;
      }
      public void Start()
      {
         double targetPerFrame = 1000000000.0 / FPS_TARGET;
         long lastTime = NanoTime();
         long now;
         while (true)
         {
            now = NanoTime();
            if (now - lastTime >= targetPerFrame)
            {
               panel.Invalidate();
               lastTime = now;
            }
         }
      }
   }
}
