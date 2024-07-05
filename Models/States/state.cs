using System.Windows.Forms;
using GameProject.Services.Game;
using GameProject.Ui;

namespace GameProject.Services.States
{
  public class State
  {
    protected GameSetup _game;
    public State(GameSetup gameSetup)
    {
      this._game = gameSetup;
    } 

    public bool IsIn(MouseEventArgs e, MenuButton button)
    {
      return button.GetBounds().Contains(e.Location);
    }

    public GameSetup GetGameSetup()
    {
      return this._game;
    }
  }
}