using System;
using System.Drawing;
using System.Windows.Forms;
using GameProject.Services.Game;

namespace GameProject.Services.States
{
  public class Pause : State, StateMethods
  {
    public Pause(GameSetup game) : base(game)
    {
    }
    public void Update()
    {
      // Update the game state here
    }

    public void Draw(Graphics g)
    {
      g.DrawString("Pause", new Font("Arial", 16), Brushes.Black, 10, 10);
    }

    public void MouseClick(object sender, MouseEventArgs e)
    {
      // Handle mouse click event here
    }

    public void MouseMove(object sender, MouseEventArgs e)
    {
      // Handle mouse move event here
    }

    public void MousePress(object sender, MouseEventArgs e)
    {
      // Handle mouse press event here
    }

    public void MouseRelease(object sender, MouseEventArgs e)
    {
      // Handle mouse release event here
    }

    public void KeyPress(object sender, KeyPressEventArgs e)
    {
      switch (e.KeyChar)
      {
        case (char)13:
          GameStateManager.GameState = GameState.PLAYING;
          break;
        case (char)8:
          GameStateManager.GameState = GameState.MENU;
          break;
        case (char)27:
          GameStateManager.GameState = GameState.MENU;
          break;
      }
    }

    public void KeyUp(object sender, KeyEventArgs e)
    {
      // Handle key up event here
    }
  }
}