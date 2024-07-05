using System;
using System.Threading;
using GameProject.Services.Window;
using System.Diagnostics;
using System.Windows.Forms;
using GameProject.Services.Entities;
using GameProject.Services.Levels;
using GameProject.Services.States;

namespace GameProject.Services.Game
{
   public class GameSetup
   {
      private GameWindow window;
      private GamePanel panel;
      private Thread gameThread;
      private readonly Thread uiThread;
      private readonly ManualResetEvent uiInitialized = new(false);

      private const int FPS_TARGET = 120;
      private const int UPS_TARGET = 200;

      private Playing _playing;
      private Menu _menu;
      private Pause _pause;

      public const int TILES_DEAULT_SIZE = 16;
      public const float SCALE = 1.0f;
      public const int TILES_IN_WIDTH = 32;
      public const int TILES_IN_HEIGHT = 32;
      public const int TILE_SIZE = (int)(TILES_DEAULT_SIZE * SCALE);
      public const int GAME_WIDTH = TILES_IN_WIDTH * TILE_SIZE;
      public const int GAME_HEIGHT = TILES_IN_HEIGHT * TILE_SIZE;
      public GameSetup()
      {
         InitClasses();

         uiThread = new Thread(InitializeUI);
         uiThread.SetApartmentState(ApartmentState.STA);
         uiThread.Start();

         StartGameLoop();
      }

      private void InitClasses()
      {
         this._menu = new Menu(this);
         this._pause = new Pause(this);
         this._playing = new Playing(this);
      }

      private void InitializeUI()
      {
         panel = new GamePanel(this);
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
         switch (GameStateManager.GameState)
         {
            case GameState.PLAYING:
               this._playing.Update();
               break;
            case GameState.MENU:
               this._menu.Update();
               break;
            case GameState.PAUSED:
               this._pause.Update();
               break;
            case GameState.OPTIONS:
               break;
            case GameState.QUIT:
               Application.Exit();
               break;
            default:
               break;
         }
      }

      public void Render(PaintEventArgs e)
      {
         switch (GameStateManager.GameState)
         {
            case GameState.PLAYING:
               this._playing.Draw(e.Graphics);
               break;
            case GameState.MENU:
               this._menu.Draw(e.Graphics);
               break;
            case GameState.PAUSED:
               this._pause.Draw(e.Graphics);
               break;
            default:
               break;
         }
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
      public Menu GetMenu()
      {
         return this._menu;
      }
      public Playing GetPlaying()
      {
         return this._playing;
      }
      public Pause GetPause()
      {
         return this._pause;
      }
   }
}
