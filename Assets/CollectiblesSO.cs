using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.Economy;
using Unity.VisualScripting;
using UnityEngine;

namespace Fishing 
{
    [CreateAssetMenu(fileName = "Book", menuName = "ScriptableObjects/Fishing/Book")]
    public class CollectiblesSO : ScriptableObject
    {
        [field:SerializeField] public List<FishSO> Fish;
        [field: SerializeField] public List<FishingJunkItemSO> JunkItems;
        [SerializeField] private FishDatabaseSO _database;

        internal event Action<FishSO> OnFishAdded;
        internal event Action<FishingJunkItemSO> OnJunkItemAdded;

        internal void AddFishToBook(FishSO fish)
        {
            if (Fish.Contains(fish)) return;

            Fish.Add(fish);
            OnFishAdded?.Invoke(fish);
        }

        internal void AddItemToBook(FishingJunkItemSO item)
        {
            if (JunkItems.Contains(item)) return;

            JunkItems.Add(item);
            OnJunkItemAdded?.Invoke(item);
        }

        internal async Task LoadCollectiblesFromCloud()
        {
            Fish.Clear();
 
            var serverData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "collectibles"});

            if (!serverData.TryGetValue("collectibles", out var cloudValue)) return;
            var adwdadad = cloudValue.Value.GetAs<List<int>>();

            foreach (var item in adwdadad)
            {
               Fish.Add(_database.GetParticularFish(item));
            }
 
        }

    }

}


