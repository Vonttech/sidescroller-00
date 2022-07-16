using UnityEngine;

public static class PlayerData
{
    public static int playerFruitPoints;

    public static int playerInitialLifePoints;

    public static int playerLifePoints;

    public static int playerLifePointsSaved;

    public static string PlayerLevelRank()
    {
        float playerFruitPointsMean = playerFruitPoints * 1.75f / LevelData.totalFruitsInLevel;

        float playerLifePointsMean = playerLifePoints * 0.30f / playerInitialLifePoints;

        float playerRankMean = (playerFruitPointsMean + playerLifePointsMean ) / 2;

        if (playerRankMean >= 1f)
        {
            return "S";
        }
        
        if (playerRankMean < 1f &&
                 playerRankMean >= 0.8f)
        {
            return "A";
        }
        
        if (playerRankMean <= 0.8f &&
                 playerRankMean >= 0.6f)
        {
            return "B";
        }
        
        if (playerRankMean <= 0.6f &&
                 playerRankMean >= 0.4f)
        {
            return "C";
        }
        
        if (playerRankMean <= 0.4f &&
                 playerRankMean >= 0.2f)
        {
            return "D";
        }
        
        if (playerRankMean <= 0.2f &&
                 playerRankMean >= 0.1f)
        {
            return "E";
        }

        return "F";

    }

}
