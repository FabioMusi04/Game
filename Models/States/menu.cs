using System.Drawing;
using System.Windows.Forms;
using GameProject.Services.Game;

namespace GameProject.Services.States
{
  public class Menu : State, StateMethods
  {
    public Menu(GameSetup game) : base(game)
    {
    }
    public void Update()
    {
      // Update the game state here
    }

    public void Draw(Graphics g)
    {
      g.DrawString("Menu", new Font("Arial", 16), Brushes.Black, GameSetup.GAME_WIDTH / 2, GameSetup.GAME_HEIGHT / 2);
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
       if (e.KeyChar == (char)13)
       {
          GameStateManager.GameState = GameState.PLAYING;
       }
    }

    public void KeyUp(object sender, KeyEventArgs e)
    {
      // Handle key up event here
    }
  }
}