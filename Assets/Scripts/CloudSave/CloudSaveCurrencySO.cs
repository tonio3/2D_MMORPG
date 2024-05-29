using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy;
using UnityEngine;

[CreateAssetMenu]
public class CloudSaveCurrencySO : CloudSaveInitializable
{
 
    readonly static string goldCurrencyID = "GOLD";
    readonly static string skillPointCurrencyID = "SKILLPOINTS";

    [SerializeField] private PlayerCurrencySO _playerCurrency;
 

    protected override void SubscribeToPlayerEvents()
    {
        _playerCurrency.OnGoldChanged += UpdateGoldValue;
        _playerCurrency.OnSkillPointsChanged += UpdateSkillPointsValue;
    }

    protected override void UnsubscribeFromPlayerEvents()
    {
        _playerCurrency.OnGoldChanged -= UpdateGoldValue;
        _playerCurrency.OnSkillPointsChanged -= UpdateSkillPointsValue;
    }

    private async void UpdateGoldValue(int gold)
    {
        await TaskUpdateGoldValue(gold);
    }

  
    private async void UpdateSkillPointsValue(int skillPoints)
    {
        if (!base.IsInitialized) return;

        await UpdateSkillPointsValueTask(skillPoints);
    }

    private async Task TaskUpdateGoldValue(int skillPoints)
    {
        await EconomyService.Instance.PlayerBalances.SetBalanceAsync(skillPointCurrencyID, skillPoints);
        Debug.Log("SkillPointsValueChanged");
    }


    private async Task UpdateSkillPointsValueTask(int skillPoints) 
    {
        await EconomyService.Instance.PlayerBalances.SetBalanceAsync(skillPointCurrencyID, skillPoints);
        Debug.Log("SkillPointsValueChanged");
    }

}
