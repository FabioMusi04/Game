using System;
using GameProject.Services.Game;
namespace GameProject.Utils
{
  public class HelpMethods
  {
    public static bool CanMoveHere(float x, float y, float w, float h, int[,] levelData) // fixare
    {
      if(!IsTileSolid(x, y, levelData)){
        if(!IsTileSolid(x + w, y, levelData)){
          if(!IsTileSolid(x, y + h, levelData)){
            if(!IsTileSolid(x + w, y + h, levelData)){
              Console.WriteLine("Can move here");
              return true;
            }
          }
        }
      }
      return false;
    }
  
    private static bool IsTileSolid(float x, float y, int[,] levelData)
    {
      if(x < 0 || y < 0 || x >= GameSetup.GAME_WIDTH || y >= GameSetup.GAME_HEIGHT)
      {
        return true;
      }

      float xIndex = x / GameSetup.TILE_SIZE;
      float yIndex = y / GameSetup.TILE_SIZE;
      int value = levelData[(int)yIndex, (int)xIndex];
      Console.WriteLine(value);
      if(value >= 570 || value < 0 || value != 73) {
        return true;
      }
      return false;
    }
  }
}