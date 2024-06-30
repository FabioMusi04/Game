using System;
using GameProject.Services.Window;

namespace GameProject.Services.Game
{
   public class Game
   {
      private GameWindow window;
      private GamePanel panel;
      public Game()
      {
         panel = new GamePanel();
         window = new GameWindow(panel);
      }
   }
}
