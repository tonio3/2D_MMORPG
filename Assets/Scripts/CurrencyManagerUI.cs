using UnityEngine;
using TMPro;
using System;

public class CurrencyManagerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _skillPointsText;

    [SerializeField] private PlayerCurrencySO _playerCurrency;

    private void Awake()
    {
      
    }

    private void OnEnable()
    {      
        SubscribeToPlayerEvents();

        //initial
        UpdateGoldText(_playerCurrency.Gold);
        UpdateSkillPointsText(_playerCurrency.SkillPoints);
    }

    private void OnDisable()
    {
        UnsubscribeFromPlayerEvents();      
    }

    private void SubscribeToPlayerEvents()
    {

        _playerCurrency.OnGoldChanged += UpdateGoldText;
        _playerCurrency.OnSkillPointsChanged += UpdateSkillPointsText;
    }
 

    private void UnsubscribeFromPlayerEvents()
    {
        _playerCurrency.OnGoldChanged -= UpdateGoldText;
        _playerCurrency.OnSkillPointsChanged -= UpdateSkillPointsText;
    }

    private void UpdateGoldText(int gold)
    {
        _goldText.text = gold + "";
    }

    private void UpdateSkillPointsText(int skillPoints)
    {
        _skillPointsText.text =skillPoints + "";
    }
 
}
