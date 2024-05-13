using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.CloudCode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
 
public class BattleManager : MonoBehaviour
{

    [SerializeField] private CharacterAttributesSO _characterAttributesSO;
    [SerializeField] private PlayerCurrencySO _playerCurrencySO;
    [SerializeField] private CharacterIdentitySO _characterIdentitySO;

    [SerializeField] PlayerBattleCard playerDisplay;
    [SerializeField] EnemyBattleCard enemy;
    [SerializeField] AudioSource attackSourceP;
    [SerializeField] AudioSource takeDamageSourceP;

    [SerializeField] AudioSource attackSourceE;
    [SerializeField] AudioSource takeDamageSourceE;

    [SerializeField] CityDatabaseSO _cityDatabaseSO;
    [SerializeField] EnemyDatabaseSO _enemyDatabaseSO;

    
    [SerializeField] CharacterDataSO _characterDataSO;
   
    private EnemySO currentEnemy;

   [SerializeField] UnityEvent OnVictory;
   [SerializeField] UnityEvent OnLose;
   [SerializeField] UnityEvent OnLeave;
    [Space]
    [SerializeField][ReadOnly] CitySO currentCity;
    [SerializeField][ReadOnly] Image _background;
    public static BattleManager Instance;
   

    private void Awake()
    {
        Instance = this;     
    }
 
    public void SetupPVP()
    {
        playerDisplay.Init(_characterAttributesSO, _characterIdentitySO);

          
        var e = new EnemySO();
        e.Level = _characterDataSO.CharacterAttributes.Level;
        e.Spr = _characterDataSO.CharacterIdentity.CharacterSprite;
        e.BaseDamage = _characterDataSO.CharacterAttributes.Damage;
        e.BaseHealth = _characterDataSO.CharacterAttributes.Health;
        enemy.InitAsPVP(e);

        currentEnemy = e;
        SimulateBattle();
    }

    public void SetupPVE()
    {
        playerDisplay.Init(_characterAttributesSO, _characterIdentitySO);
        currentEnemy = _enemyDatabaseSO.GetRandomEnemy();
        currentEnemy.Level = _characterAttributesSO.Level;
        enemy.Init(currentEnemy);
    
        SimulateBattle();
    }

    public void SetupPVE(EnemySO enemySO)
    {
        playerDisplay.Init(_characterAttributesSO, _characterIdentitySO);
        currentEnemy = enemySO;
        enemy.Init(enemySO);

        SimulateBattle();
    }



    public async void SimulateBattle()
    {

        _background.sprite = _cityDatabaseSO.GetRandomCity();

        await Task.Delay(500);
        var a = EnemyAttack();

        var b = PlayerAttack();

        await a;
        await b;

        if (playerDisplay.IsAlive()) { WinBattle(); }
        else { LoseBattle(); }
    }


    private async Task EnemyAttack()
    {    
        enemy.Attack();
        await Task.Delay(70);
        attackSourceE.Play();   
        Debug.Log("E");

        await Task.Delay(200);
        takeDamageSourceP.Play();
        await Task.Delay(130);
        playerDisplay.TakeDamage(enemy.GetAttackDamage());
       
        await Task.Delay(enemy.AttackSpeed);

        if (playerDisplay.IsAlive() && enemy.IsAlive()) 
        {
            await   EnemyAttack();
        }
    }

    private async Task PlayerAttack()
    {
        playerDisplay.Attack();
        await Task.Delay(70);
        attackSourceP.Play();    
        Debug.Log("P");
       
        await Task.Delay(200);
        takeDamageSourceE.Play();

        await Task.Delay(130);
        enemy.TakeDamage(playerDisplay.GetAttackDamage());
       
        await Task.Delay(playerDisplay.AttackSpeed);
   
        if (playerDisplay.IsAlive() && enemy.IsAlive())
        { 
           await  PlayerAttack();
        }
    }

    public void WinBattle()
    {
        _characterAttributesSO.Xp +=(currentEnemy.CalcXpRewardBasedOnLevel());
        _playerCurrencySO.Gold +=(currentEnemy.CalcGoldRewardBasedOnLevel());
        OnVictory.Invoke();
    }

    public void LoseBattle()
    {
        OnLose.Invoke();
    }

    private void OnDisable()
    {
        OnLeave.Invoke();
    }
}
