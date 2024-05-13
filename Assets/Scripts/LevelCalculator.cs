using UnityEngine;

public class LevelCalculator
{
    // Metoda pro v�po�et levelu na z�klad� zku�enost� (XP)
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