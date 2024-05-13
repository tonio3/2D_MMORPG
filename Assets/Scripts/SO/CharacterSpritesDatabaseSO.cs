using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSpritesDatabase", menuName = "ScriptableObjects/CharacterSpritesDatabase", order = 1)]
public class CharacterSpritesDatabaseSO : ScriptableObject
{
    public Sprite[] characterSprites;

    public (Sprite, int) GetRandomSpriteWithIndex()
    {
        if (characterSprites.Length == 0)
        {
            Debug.LogWarning("Databáze spriteů je prázdná!");
            return (null, -1);
        }

        int randomIndex = Random.Range(0, characterSprites.Length);
        return (characterSprites[randomIndex], randomIndex);
    }

    public int GetRandomSpriteId()
    {       
        int randomIndex = Random.Range(0, characterSprites.Length);
        return randomIndex;
    }

    public Sprite GetRandomSprite()
    {
        return characterSprites[Random.Range(0, characterSprites.Length)];
    }

    public Sprite GetSprite(int index)
    {
        int adjustedIndex = (index % characterSprites.Length + characterSprites.Length) % characterSprites.Length;  //loop
        return characterSprites[adjustedIndex];
    }
}