using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.CloudCode;
using UnityEngine;
using UnityEngine.UI;

public class OtherPlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Slider _xpSlider;
    [Space]
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _playerImage;
    [SerializeField] private OtherCharacterDataSO _characterDataSO;
    [SerializeField] private OtherPlayerInvenotrySlot[] _playerInvenotrySlots;
    [SerializeField] private  ItemDatabaseSO _itemDatabaseSO;
    [SerializeField] private TextMeshProUGUI _characterName;
    

    private void OnEnable()
    {
        UpdateUI();
    }

    private async void UpdateUI()
    {
  
        //identity
        _characterName.text = _characterDataSO.CharacterIdentity.CharacterName;
       

        // attributes + sprite
        var args = new Dictionary<string, object>() { { "_playerID", (string)_characterDataSO.CharacterIdentity.CharacterId } };
        var result = await CloudCodeService.Instance.CallEndpointAsync<OtherPlayerDataDTO>("GetPlayerStats", args);

        if (result != null)
        {
            _characterDataSO.CharacterAttributes.Endurance = result.Endurance;
            _characterDataSO.CharacterAttributes.Strength = result.Strength;
            _characterDataSO.CharacterAttributes.Xp = result.Xp;
            _characterDataSO.CharacterIdentity.CharacterSpriteId = result.CharacterSpriteId;

            _healthText.text = _characterDataSO.CharacterAttributes.Health + "";
            _damage.text = _characterDataSO.CharacterAttributes.Damage + "";
            _xpSlider.value = LevelCalculator.XpBar(_characterDataSO.CharacterAttributes.Xp);
            _levelText.text = _characterDataSO.CharacterAttributes.Level + "";
            _playerImage.sprite = _characterDataSO.CharacterIdentity.CharacterSprite;
        }
 

        // Gear
        var args2 = new Dictionary<string, object>() { { "playerId", _characterDataSO.CharacterIdentity.CharacterId } };
        var result2 = await CloudCodeService.Instance.CallEndpointAsync<ResultType>("GetPlayerInventory", args2);


        var inv = result2.inventory;

        for (int i = 0; i < result2.inventory.Length; i++)
        {
            JObject instanceData = inv[i].instanceData;
            if (instanceData == null) return;
            ItemDTO itemDTO = new ItemDTO
            {
                ItemName = (string)instanceData["ItemName"],
                ItemType = (string)instanceData["ItemType"]
            };

            _playerInvenotrySlots[i]._img.sprite = _itemDatabaseSO.GetItem(itemDTO.ItemType, itemDTO.ItemName).MainSprite;
        }

 
    }
  
}

public class ResultType
{
    public InventoryResponse[] inventory;
}

public class InventoryResponse
{
    public JObject instanceData;
}
 