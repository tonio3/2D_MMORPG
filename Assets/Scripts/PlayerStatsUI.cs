using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using Unity.Services.Economy;
using System.Linq;
using System.Collections.Generic;
using System.Data;

public class PlayerStatsUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider xpSlider;
    [Space]
    [SerializeField] TextMeshProUGUI _playerNameText;
    [Space]
    [SerializeField] private TextMeshProUGUI _strengthValueText;
 
    [SerializeField] private TextMeshProUGUI _enduranceValueText;

    [SerializeField] private TextMeshProUGUI _damageValueText;

    [SerializeField] private TextMeshProUGUI _healthValueText;

    [SerializeField] private Image _characterSprite;
    [SerializeField] private CharacterSpritesDatabaseSO _characterSpritesDatabaseSO;
    [SerializeField] private CharacterIdentitySO _characterIdentity;
    [SerializeField] private PlayerInventorySO _playerInventory;

    [SerializeField] private CharacterAttributesSO _characterAttributesSO;

    [SerializeField] private InventoryItemSlot[] _inventoryItems;
    [SerializeField] private GearItemSlot[] _gearItems;

    private void Awake()
    {
         
    }

    private void OnEnable()
    {
        SubscribeToPlayerEvents();

        UpdateLevelText(_characterAttributesSO.Level);
        UpdateDamageText(_characterAttributesSO.Damage);
        UpdateStrengthText(_characterAttributesSO.Strength);
        UpdateEnduranceText(_characterAttributesSO.Endurance);
        UpdateHealthText(_characterAttributesSO.Health);
        UpdateXPSlider(_characterAttributesSO.Xp);
        UpdateCharacterSprite(_characterIdentity.CharacterSprite);
        UpdateCharacterInventory();
        UpdatePlayerName(_characterIdentity.CharacterName);
    }

    private void UpdateCharacterInventory()
    {
        //Inventory items
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            var item = _playerInventory.ItemSlots[i];
            if (item.Item == null) { continue; };
            _inventoryItems[i].SetItem(item.Item);
        }

        //Gear items
        for (int i = 0; i < _gearItems.Length; i++)
        {
            var item = _playerInventory.GearSlots[i];
            if (item.Item == null) { continue; };
            _gearItems[i].SetItem(item.Item);
        }
    }

    private void OnDisable()
    {
        UnsubscribeFromPlayerEvents();
    }

    private void SubscribeToPlayerEvents()
    {
        _characterAttributesSO.OnLevelChanged += UpdateLevelText;
        _characterAttributesSO.OnXPChanged += UpdateXPSlider;
        _characterAttributesSO.OnStrengthChanged += UpdateStrengthText;
        _characterIdentity.OnCharacterNameChanged+= UpdatePlayerName;
        _characterAttributesSO.OnDamageChanged += UpdateDamageText;
        _characterAttributesSO.OnHealthChanged += UpdateHealthText;
        _characterAttributesSO.OnEnduranceChanged += UpdateEnduranceText;
        _characterIdentity.OnCharacterSpriteChanged += UpdateCharacterSprite;
    }

    private void UnsubscribeFromPlayerEvents()
    {
        _characterAttributesSO.OnLevelChanged -= UpdateLevelText;
        _characterAttributesSO.OnXPChanged -= UpdateXPSlider;
       _characterIdentity.OnCharacterNameChanged -= UpdatePlayerName;
        _characterAttributesSO.OnStrengthChanged -= UpdateStrengthText;
        _characterAttributesSO.OnDamageChanged -= UpdateDamageText;
        _characterAttributesSO.OnHealthChanged -= UpdateHealthText;
        _characterAttributesSO.OnEnduranceChanged -= UpdateEnduranceText;
        _characterIdentity.OnCharacterSpriteChanged -= UpdateCharacterSprite;
    }

    private void UpdateCharacterSprite(Sprite spr)
    {
        _characterSprite.sprite = spr;
    }

    private void UpdateLevelText(int level)
    {
        levelText.text = level + "";
    }

    private void UpdateXPSlider(int xp)
    {
        xpSlider.value = LevelCalculator.XpBar(xp);
    }

    private void UpdateStrengthText(int strength)
    {
        _strengthValueText.text = strength + "";
    }

    private void UpdatePlayerName(string playerName)
    {
        _playerNameText.text = playerName;
    }

  
    private void UpdateDamageText(int damage)
    {
        _damageValueText.text = damage + "";
    }

    private void UpdateEnduranceText(int endurance)
    {
        _enduranceValueText.text = endurance + "";
    }


    private void UpdateHealthText(int health)
    {
        _healthValueText.text = health + "";
    }
 
   
}
