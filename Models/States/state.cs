using GameProject.Services.Game;

namespace GameProject.Services.States
{
  public class State
  {
    protected GameSetup _game;
    public State(GameSetup gameSetup)
    {
      this._game = gameSetup;
    }  

    public GameSetup GetGameSetup()
    {
      return this._game;
    }
  }
}