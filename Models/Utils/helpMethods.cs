using System;
using GameProject.Services.Game;

namespace GameProject.Utils
{
  public class HelpMethods
  {
public static bool CanMoveHere(float x, float y, float w, float h, int[,] levelData, float currentX, float currentY, float dx, float dy)
{
    int tileSize = GameSetup.TILE_SIZE;

    // Function to check if a given point is inside a solid tile
    bool IsSolidTile(float px, float py)
    {
        int tileX = (int)(px / tileSize);
        int tileY = (int)(py / tileSize);

        // Check if the tile coordinates are within the level bounds
        if (tileX < 0 || tileY < 0 || tileX >= levelData.GetLength(1) || tileY >= levelData.GetLength(0))
        {
            return false;
        }

        return levelData[tileY, tileX] == 1; // Assuming 1 represents a solid tile
    }

    // Function to check if moving away from a solid tile
    bool IsMovingAway(float px, float py)
    {
        int tileX = (int)(px / tileSize);
        int tileY = (int)(py / tileSize);

        if (tileX < 0 || tileY < 0 || tileX >= levelData.GetLength(1) || tileY >= levelData.GetLength(0))
        {
            return false;
        }

        return (dx > 0 && px < currentX) || (dx < 0 && px > currentX) || (dy > 0 && py < currentY) || (dy < 0 && py > currentY);
    }

    // Check all four corners of the bounding box
    bool topLeft = IsSolidTile(x, y);
    bool topRight = IsSolidTile(x + w, y);
    bool bottomLeft = IsSolidTile(x, y + h);
    bool bottomRight = IsSolidTile(x + w, y + h);

    if (topLeft || topRight || bottomLeft || bottomRight)
    {
        // Check if moving away from solid tiles
        if ((topLeft && IsMovingAway(x, y)) || 
            (topRight && IsMovingAway(x + w, y)) || 
            (bottomLeft && IsMovingAway(x, y + h)) || 
            (bottomRight && IsMovingAway(x + w, y + h)))
        {
            return true;
        }
        
        return false;
    }

    return true;
}


    private static bool IsTileSolid(float x, float y, int[,] levelData)
    {
      if (x < 0 || y < 0 || x >= GameSetup.GAME_WIDTH || y >= GameSetup.GAME_HEIGHT)
      {
        return true;
      }

      float xIndex = x / GameSetup.TILE_SIZE;
      float yIndex = y / GameSetup.TILE_SIZE;
      int value = levelData[(int)yIndex, (int)xIndex];
      if (value >= 570 || value < 0 || value != 73)
      {
        return true;
      }
      return false;
    }
  }
}