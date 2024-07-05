using System;
using GameProject.Services.Game;

namespace GameProject.Utils
{
  public class HelpMethods
  {
    public static bool CanMoveHere(float x, float y, float w, float h, int[,] levelData, int direction)
    {
      int tileSize = GameSetup.TILE_SIZE;

      bool IsSolidTile(int tileX, int tileY)
      {
        if (tileX < 0 || tileY < 0 || tileX >= levelData.GetLength(1) || tileY >= levelData.GetLength(0))
        {
          return true;
        }

        int value = levelData[tileY, tileX];
        return value >= 570 || value < 0 || value != 73;
      }

      if (direction == Constants.Directions.UP)
      {
        int topTile = (int)(y / tileSize);
        for (int tileX = (int)(x / tileSize); tileX <= (int)((x + w) / tileSize); tileX++)
        {
          if (IsSolidTile(tileX, topTile))
          {
            return false;
          }
        }
      }
      else if (direction == Constants.Directions.DOWN)
      {
        int bottomTile = (int)((y + h) / tileSize);
        for (int tileX = (int)(x / tileSize); tileX <= (int)((x + w) / tileSize); tileX++)
        {
          if (IsSolidTile(tileX, bottomTile))
          {
            return false;
          }
        }
      }
      else if (direction == Constants.Directions.LEFT)
      {
        int leftTile = (int)(x / tileSize);
        for (int tileY = (int)(y / tileSize); tileY <= (int)((y + h) / tileSize); tileY++)
        {
          if (IsSolidTile(leftTile, tileY))
          {
            return false;
          }
        }
      }
      else if (direction == Constants.Directions.RIGHT)
      {
        int rightTile = (int)((x + w) / tileSize);
        for (int tileY = (int)(y / tileSize); tileY <= (int)((y + h) / tileSize); tileY++)
        {
          if (IsSolidTile(rightTile, tileY))
          {
            return false;
          }
        }
      }

      return true;
    }
  }
}