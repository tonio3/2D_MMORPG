using Fishing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.Leaderboards;
using UnityEngine;

namespace Fishing
{

    [CreateAssetMenu]
    public class CloudSaveCollectiblesSO : CloudSaveInitializable
    {
        [SerializeField] private CollectiblesSO _collectorsBookSO;

        // Variables declaration
        private Dictionary<string, object> saveData;


        // Subscribe and unsubscribe methods
        protected override void SubscribeToPlayerEvents()
        {
            _collectorsBookSO.OnFishAdded += SaveFish;
            _collectorsBookSO.OnJunkItemAdded += SaveJunk;
        }

        protected override void UnsubscribeFromPlayerEvents()
        {
            _collectorsBookSO.OnFishAdded -= SaveFish;
            _collectorsBookSO.OnJunkItemAdded -= SaveJunk; 
        }

        // Event handlers
        private async void SaveFish(FishSO fish)
        {
            await SaveCollectibleFishAsync();
        }

        private async void SaveJunk(FishingJunkItemSO fish)
        {
            await SaveCollectibleJunkAsync();
        }


        // Task methods
        private async Task SaveCollectibleFishAsync()
        {
            List<int> ItemsId = new List<int>();

            for (int i = 0; i < _collectorsBookSO.Fish.Count; i++)
            {
                ItemsId.Add(i);
            }
    
            saveData = new Dictionary<string, object> { { "collectibleFish", ItemsId } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
            Debug.Log("CollectableSaved");
        }

        private async Task SaveCollectibleJunkAsync()
        {
            List<int> ItemsId = new List<int>();

            for (int i = 0; i < _collectorsBookSO.Fish.Count; i++)
            {
                ItemsId.Add(i);
            }

            saveData = new Dictionary<string, object> { { "collectibleJunk", ItemsId } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
            Debug.Log("CollectableSaved");
        }

    }

}
