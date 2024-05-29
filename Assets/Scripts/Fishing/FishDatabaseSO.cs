using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fishing
{
    [CreateAssetMenu(fileName = "FishDatabase", menuName = "ScriptableObjects/FishDatabase", order = 1)]
    public class FishDatabaseSO : ScriptableObject
    {
        [SerializeField] private List<FishSO> _fish;
        [SerializeField] private List<FishBossSO> _fishBosses;
        [SerializeField] private List<FishingJunkItemSO> _fishingJunkItems;

        internal FishSO GetRandomFish()
        {
            var i = Random.Range(0, _fish.Count);
            return _fish[i];
        }

        internal FishSO GetParticularFish(int i)
        {
            return _fish[i];
        }

        internal FishingJunkItemSO GetJunkItem()
        {
            var i = Random.Range(0, _fishingJunkItems.Count);
            return _fishingJunkItems[i];
        }

        internal FishBossSO GetFishBoss()
        {
            var i = Random.Range(0, _fishBosses.Count);
            return _fishBosses[i];
        }

        public void CreateJunkItemSO(string name, Sprite spr, RaritySO rarity)
        {
            var newJunkItem = ScriptableObject.CreateInstance<FishingJunkItemSO>();
            newJunkItem.Init(spr, rarity);

            _fishingJunkItems.Add(newJunkItem);

            AssetDatabase.CreateAsset(newJunkItem, $"Assets/ScriptableObjects/Database/Fishing/Junk/{name}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void CreateFishSO(string name, Sprite spr, RaritySO rarity)
        {
            var newFish = ScriptableObject.CreateInstance<FishSO>();
            newFish.Init(spr, rarity);
 
            _fish.Add(newFish);

            AssetDatabase.CreateAsset(newFish, $"Assets/ScriptableObjects/Database/Fishing/Bosses/{name}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
      
        public void CreateFishBossSO(string name, Sprite spr, RaritySO rarity, int level, int hp, int damage,Sprite weapon) 
        {
            var newFishBoss = ScriptableObject.CreateInstance<FishBossSO>();
            newFishBoss.Init( spr, rarity);

            _fishBosses.Add(newFishBoss);

            AssetDatabase.CreateAsset(newFishBoss, $"Assets/ScriptableObjects/Database/Fishing/FishBosses/{name}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();    
        }

        public void RemoveFish(FishSO fish)
        {
            _fish.Remove(fish);

            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(fish));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void RemoveFishBoss(FishBossSO fishBoss)
        {
            _fishBosses.Remove(fishBoss);

            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(fishBoss));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void RemoveJunkItem(FishingJunkItemSO junkItem)
        {
            _fishingJunkItems.Remove(junkItem);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(junkItem));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

    }

}
