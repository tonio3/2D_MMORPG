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
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider xpSlider;
    [Space]
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image _playerImage;
    [SerializeField] private OtherCharacterDataSO _characterDataSO;
    [SerializeField] private OtherPlayerInvenotrySlot[] _playerInvenotrySlots;
    [SerializeField] private  ItemDatabaseSO _itemDatabaseSO;
    [SerializeField] private TextMeshProUGUI _characterName;
    

    private void OnEnable()
    {
        _characterDataSO.CharacterAttributes.OnLevelChanged += UpdateLevel;
        _characterDataSO.CharacterAttributes.OnXPChanged += UpdateXP;
        _characterDataSO.CharacterAttributes.OnDamageChanged += UpdateDamage;
        _characterDataSO.CharacterAttributes.OnHealthChanged += UpdateHealth;
        _characterDataSO.CharacterIdentity.OnCharacterSpriteChanged += UpdateCharacterSprite;
        UpdateUI();
    }

    private async void UpdateUI()
    {
  
        //name
        _characterName.text = _characterDataSO.CharacterIdentity.CharacterName;


        // attributes
        var args = new Dictionary<string, object>() { { "_playerID", (string)_characterDataSO.CharacterIdentity.CharacterId } };
        var result = await CloudCodeService.Instance.CallEndpointAsync<OtherPlayerDataDTO>("GetPlayerStats", args);

        if (result != null)
        {
            _characterDataSO.CharacterAttributes.Endurance = result.Endurance;
            _characterDataSO.CharacterAttributes.Strength = result.Strength;
            _characterDataSO.CharacterAttributes.Xp = result.Xp;
            _characterDataSO.CharacterIdentity.CharacterSpriteId = result.CharacterSpriteId;
        }

        //level, xp
        levelText.text = _characterDataSO.CharacterAttributes.Level + "";
        xpSlider.value = LevelCalculator.XpBar(_characterDataSO.CharacterAttributes.Xp);


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

            _playerInvenotrySlots[i]._img.sprite = _itemDatabaseSO.GetItem(itemDTO.ItemType, itemDTO.ItemName).Spr;
        }

 
    }

    private void OnDisable()
    {
        _characterDataSO.CharacterAttributes.OnLevelChanged -= UpdateLevel;
        _characterDataSO.CharacterAttributes.OnXPChanged -= UpdateXP;
        _characterDataSO.CharacterAttributes.OnDamageChanged -= UpdateDamage;
        _characterDataSO.CharacterAttributes.OnHealthChanged -= UpdateHealth;
        _characterDataSO.CharacterIdentity.OnCharacterSpriteChanged -= UpdateCharacterSprite;
    }

    private void UpdateLevel(int level)
    {
        levelText.text = level + "";
    }

    private void UpdateXP(int xp)
    { 
        xpSlider.value = LevelCalculator.XpBar(xp);
    }

    private void UpdateDamage(int damage)
    {
        _damage.text = damage + "";
    }

    private void UpdateHealth(int health)
    {
        healthText.text = health + "";
    }

    private void UpdateCharacterSprite(Sprite sprite)
    {
        _playerImage.sprite = sprite;
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
 