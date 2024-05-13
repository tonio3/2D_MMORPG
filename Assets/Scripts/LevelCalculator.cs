using UnityEngine;

public class LevelCalculator
{
    // Metoda pro výpoèet levelu na základì zkušeností (XP)
    public static int LevelCalc(int xp)
    {
        // initial level
        int level = 1;


        if (xp >= 100)
        {
            level = xp / 100;
        }

        return level;
    }

    public static float XpBar(int xp)
    {
        float remainingXp = (float)(xp % 100) / 100;   // /100 == percentage
        return remainingXp;
    }
}