using System;
using GameProject.Services.Window;

namespace GameProject.Services.Game
{
   public class Game
   {
      private readonly GameWindow window;
      private readonly GamePanel panel;
      public Game()
      {
         panel = new GamePanel();
         window = new GameWindow(panel);
      }
   }
}
