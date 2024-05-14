using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy;
using UnityEngine;

[CreateAssetMenu]
public class CloudSaveCurrencySO : ScriptableObject
{
 
    readonly static string goldCurrencyID = "GOLD";
    readonly static string skillPointCurrencyID = "SKILLPOINTS";

    [SerializeField] private PlayerCurrencySO _playerCurrency;

    private void OnEnable()
    {
        SubscribeToPlayerEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromPlayerEvents();
    }

    private void SubscribeToPlayerEvents()
    {
        _playerCurrency.OnGoldChanged += UpdateGoldValue;
        _playerCurrency.OnSkillPointsChanged += UpdateSkillPointsValue;
    }

    private void UnsubscribeFromPlayerEvents()
    {
        _playerCurrency.OnGoldChanged -= UpdateGoldValue;
        _playerCurrency.OnSkillPointsChanged -= UpdateSkillPointsValue;
    }

    private void UpdateGoldValue(int gold)
    {
        EconomyService.Instance.PlayerBalances.SetBalanceAsync(goldCurrencyID, gold);
        Debug.Log("GoldValueSaved");
    }

    private async void UpdateSkillPointsValue(int skillPoints)
    {
        await TaskUpdate(skillPoints);
    }

    private async Task TaskUpdate(int skillPoints) 
    {
        await EconomyService.Instance.PlayerBalances.SetBalanceAsync(skillPointCurrencyID, skillPoints);
        Debug.Log("SkillPointsValueChanged");
    }

 
}
