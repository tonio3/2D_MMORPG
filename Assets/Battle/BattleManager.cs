using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BattleData
{
    public BattleData(int i)
    {
        dmg = i;
    }
    public int dmg;
}

public class BattleManager : MonoBehaviour
{

 
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
 

    [SerializeField] PlayerBattleCard playerDisplay;
    [SerializeField] EnemyBattleCard enemy;
    [SerializeField] AudioSource attackSourceP;
    [SerializeField] AudioSource takeDamageSourceP;

    [SerializeField] AudioSource attackSourceE;
    [SerializeField] AudioSource takeDamageSourceE;


    [SerializeField] DatabaseSO m_DatabaseSO;

    [SerializeField] TownE currentCity;
    [SerializeField] PlayerInventoryManager playerInventoryManager;


    EnemySO[] enemies;
    public int currentEnemy = 0;

   [SerializeField] UnityEvent OnVictory;
   [SerializeField] UnityEvent OnLose;
   [SerializeField] UnityEvent OnLeave;

    private void OnEnable()
    {
        playerDisplay.Init();

        currentEnemy = 0;
        var lenght = 2;
        enemies = new EnemySO[lenght];

        for (int i = 0; i < lenght; i++)
        {
            enemies[i] = m_DatabaseSO.GetRandomEnemy();
        }


        enemy.Init(enemies[0]);
 

        SimulateBattle();
    }

    public async void SimulateBattle()
    {

       
        await Task.Delay(500);
        var a = EnemyAttack();

        var b = PlayerAttack();


        await a;
        await b;

        if (playerDisplay.IsAlive()) { WinBattle(); }
        else { WinBattle(); }
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
        playerInventoryManager.InsertResources(currentCity);

        OnVictory.Invoke();
    }

    public void LoseBattle()
    {
        playerInventoryManager.InsertResources(currentCity);

        OnLose.Invoke();
    }

    private void OnDisable()
    {
        OnLeave.Invoke();
    }
}
