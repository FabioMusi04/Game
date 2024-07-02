namespace GameProject.Level {
    public class Level {
        private int[,] _levelData;
        public Level(int[,] levelData) {
            this._levelData = levelData;
        }

        public int GetSpriteIndex(int x, int y) {
            return _levelData[y, x];
        }
    }
}