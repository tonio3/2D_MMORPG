using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fishing
{
    public class FishingUIManager : UIEventSubscriber
    {
        [SerializeField] private FishDatabaseSO _fishDatabase;
        [Space]
        [SerializeField] private TextMeshProUGUI _fishName;
        [SerializeField] private Image _fishImage;
        [SerializeField] private UnityEvent BossFight;
        [SerializeField] BattleManager _battleManager;
        [SerializeField] PlayerInventorySO _playerInventory;
        [SerializeField] TextMeshProUGUI _rarityTxt;
        [SerializeField] CollectiblesSO _collectorsBook;

        protected override void InitialUIUpdate()
        {
             
        }

        protected override void SubscribeToPlayerEvents()
        {
          
        }

        protected override void UnsubscribeFromPlayerEvents()
        {
             
        }

        public void UpdatePopUpReward()
        {
            Sprite fishLootSpr = null;
            string fishName = null;

            var i = Random.Range(0, 3);

            if(i ==0)
            {
                var  junkItem = _fishDatabase.GetJunkItem();
                fishLootSpr = junkItem.MainSprite;
                fishName = junkItem.name;

                var iJunkItem = (IRarity)junkItem;

                _rarityTxt.text = iJunkItem.Rarity.name + "";
                _rarityTxt.color = iJunkItem.Rarity.RarityColor;
                _collectorsBook.AddItemToBook(junkItem);
            }

            if (i == 1)
            {
                var fish = _fishDatabase.GetRandomFish();
                fishLootSpr = fish.MainSprite;
                fishName = fish.name;

                var iJunkItem = (IRarity)fish;

                _rarityTxt.text = iJunkItem.Rarity.name + "";
                _rarityTxt.color = iJunkItem.Rarity.RarityColor;
                _collectorsBook.AddFishToBook(fish);
               
            }

            if (i == 2)
            {
                var fishBoss = _fishDatabase.GetFishBoss();
                BossFight.Invoke();
                _battleManager.SetupPVE(fishBoss);
            }

            _fishImage.sprite = fishLootSpr;
            _fishName.text = fishName;
        } 
    }

}
