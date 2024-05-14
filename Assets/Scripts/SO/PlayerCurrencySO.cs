using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class PlayerCurrencySO : ScriptableObject
{

    private void OnDisable()
    {
        _gold = 0;
        _skillPoints = 0;
    }

    [SerializeField][ReadOnly] private int _gold;
    public event Action<int> OnGoldChanged;
    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
            OnGoldChanged?.Invoke(_gold);
        }
    }


    [SerializeField][ReadOnly] private int _skillPoints;
    public event Action<int> OnSkillPointsChanged;
    public int SkillPoints
    {
        get { return _skillPoints; }
        set
        {    
            _skillPoints = value;
            OnSkillPointsChanged?.Invoke(_skillPoints);
        }
    }

    public async Task LoadCurrencyFromCloud()
    {
        GetBalancesResult getBalancesResult = await EconomyService.Instance.PlayerBalances.GetBalancesAsync();
        List<PlayerBalance> firstFiveBalances = getBalancesResult.Balances;

        var goldBalance = firstFiveBalances.Find(balance => balance.CurrencyId == "GOLD");
        Gold = (int)goldBalance.Balance;

        var skillPointsBalance = firstFiveBalances.Find(balance => balance.CurrencyId == "SKILLPOINTS");
        SkillPoints = (int)skillPointsBalance.Balance;
    }

    public bool BuyForGold(int price)
    {
        var b = _gold >= price;
        if (b)
        {
            Gold -= price;
        } 
        return b;
    }

    public bool CanBuyForSkillPoints(int ammount)
    {
        return _skillPoints >= ammount;
    }

    public void Sell()
    {
       
    }
}