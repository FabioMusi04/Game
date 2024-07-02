using System.Drawing;
using System.Windows.Forms;
using GameProject.Services.Entities;
using GameProject.Services.Levels;
using GameProject.Services.Game;

namespace GameProject.Services.States
{
  public class Playing : State, StateMethods
  {
    private Player _player;
    private LevelManager _levelManager;
    public Playing(GameSetup game) : base(game)
    {
      InitClasses();
    }

    private void InitClasses()
    {
      this._levelManager = new LevelManager(this._game);
      this._player = new Player(200, 200, (int)(48 * GameSetup.SCALE), (int)(48 * GameSetup.SCALE));
      this._player.LoadLevelData(this._levelManager.GetCurrentLevelData().GetLevelData());
    }
    public Player GetPlayer()
    {
      return this._player;
    }

    public void Update()
    {
      this._levelManager.Update();
      this._player.Update();
    }

    public void Draw(Graphics g)
    {
      this._levelManager.Draw(g);
      this._player.Render(g);
    }

    public void MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        this._player.Attack = true;
      }
    }

    public void MouseMove(object sender, MouseEventArgs e)
    {
      throw new System.NotImplementedException();
    }

    public void MousePress(object sender, MouseEventArgs e)
    {
      throw new System.NotImplementedException();
    }

    public void MouseRelease(object sender, MouseEventArgs e)
    {
      throw new System.NotImplementedException();
    }

    public void KeyPress(object sender, KeyPressEventArgs e)
    {
      switch (char.ToLower(e.KeyChar))
      {
        case 'w':
          this._player.Up = true;
          break;
        case 'a':
          this._player.Left = true;
          break;
        case 's':
          this._player.Down = true;
          break;
        case 'd':
          this._player.Right = true;
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
      switch (e.KeyCode)
      {
        case Keys.W:
          this._player.Up = false;
          break;
        case Keys.A:
          this._player.Left = false;
          break;
        case Keys.S:
          this._player.Down = false;
          break;
        case Keys.D:
          this._player.Right = false;
          break;
      }
    }
  }
}