using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
[System.Serializable]
public class ItemSO : ScriptableObjectWithIcon, IRarity
{
  
    public int Price;
 
    public ItemTypeSO ItemType;
    [field: SerializeField] public RaritySO Rarity { get; set; }
    public void Init(Sprite spr, RaritySO rarity)
    {
        MainSprite = spr;
        Rarity = rarity;
    }

}
