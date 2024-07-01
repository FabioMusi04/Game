using System;
using System.Threading;
using GameProject.Services.Window;
using System.Diagnostics;
using System.Windows.Forms;

namespace GameProject.Services.Game
{
   public class GameSetup
   {
      private GameWindow window;
      private GamePanel panel;
      private Thread gameThread;
      private Thread uiThread;
      private ManualResetEvent uiInitialized = new ManualResetEvent(false);

      private const int FPS_TARGET = 120;
      private const int UPS_TARGET = 200;

      public GameSetup()
      {
         uiThread = new Thread(InitializeUI);
         uiThread.SetApartmentState(ApartmentState.STA);
         uiThread.Start();

         StartGameLoop();
      }

      private void InitializeUI()
      {
         panel = new GamePanel();
         window = new GameWindow(panel);

         Console.WriteLine("UI Initialized");

         uiInitialized.Set();

         Application.EnableVisualStyles();
         Application.Run(window.form);
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

      public void Update()
      {
         panel.UpdateGame();
      }

      public void Start()
      {
         uiInitialized.WaitOne();

         double timePerFrame = 1000000000.0 / FPS_TARGET;
         double timePerUpdate = 1000000000.0 / UPS_TARGET;

         long previousTime = NanoTime();

         int frames = 0;
         int updates = 0;
         long lastCheck = DateTimeOffset.Now.ToUnixTimeMilliseconds();

         double deltaU = 0;
         double deltaF = 0;

         while (true)
         {
            long currentTime = NanoTime();

            deltaU += (currentTime - previousTime) / timePerUpdate;
            deltaF += (currentTime - previousTime) / timePerFrame;
            previousTime = currentTime;

            if (deltaU >= 1)
            {
               Update();
               updates++;
               deltaU--;
            }

            if (deltaF >= 1)
            {
               window?.form?.Invoke(new MethodInvoker(delegate ()
               {
                  panel.Refresh();
               }));

               frames++;
               deltaF--;
            }

            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastCheck >= 1000)
            {
               lastCheck = DateTimeOffset.Now.ToUnixTimeMilliseconds();
               Console.WriteLine("FPS: " + frames + " | UPS: " + updates);
               frames = 0;
               updates = 0;
            }
         }
      }
   }
}
