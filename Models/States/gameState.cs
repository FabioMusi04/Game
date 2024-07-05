namespace GameProject.Services.States
{
  public enum GameState
  {
    PLAYING, MENU, PAUSED
  }

  public class GameStateManager
  {
    public static GameState GameState = GameState.MENU;
  }
}