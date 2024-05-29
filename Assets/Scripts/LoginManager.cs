using Fishing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Economy;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.Events;

public class LoginManager : MonoBehaviour
{
    [SerializeField] TMP_InputField _nameText;
    [SerializeField] TMP_InputField _passwordText;
    [SerializeField] UnityEvent OnSuccess;
    [Space]
    [SerializeField] InitialPlayerData initialPlayerDataSO;
    [SerializeField] CharacterSpritesDatabaseSO _characterSpritesDatabaseSO;
    [SerializeField] CharacterAttributesSO _characterAttributesSO;
    [SerializeField] PlayerInventorySO _playerInventorySO;
    [SerializeField] CharacterIdentitySO _characterIdentitySO;
    [SerializeField] PlayerCurrencySO _playerCurrencySO;
    [SerializeField] CollectiblesSO _collectorsBookSO;
    [Space]
    [SerializeField] CloudSaveAttributesSO _cloudSaveAttributesSO;
    [SerializeField] CloudSaveCurrencySO _cloudSaveCurrencySO;
    [SerializeField] CloudSaveIdentitySO _cloudSaveIdentitySO;
    [SerializeField] CloudSaveInventorySO _cloudSaveInventorySO;
    [SerializeField] CloudSaveCollectiblesSO _cloudSaveCollectiblesSO;

    private readonly string userNamePepper = "dwaodoOO3";
    private readonly string passwordPepper = "Abc123!#abaDDc";
    private readonly string leaderboardId = "LEADERBOARD";

    async void Awake()
    {
        await UnityServices.InitializeAsync();
    }

    public async void Register() //button
    {
        /*
        if (string.IsNullOrEmpty(_nameText.text) || string.IsNullOrEmpty(_passwordText.text))
        {
            Debug.LogWarning("Please enter username and password.");
            return;
        }*/

        await SignUpWithUsernamePasswordAsync(_nameText.text, _passwordText.text);
        await AuthenticationService.Instance.UpdatePlayerNameAsync(_nameText.text);
        await SaveInitialPlayerData();
    }

    public async void Login() //button
    {
        /*
        if (string.IsNullOrEmpty(_nameText.text) || string.IsNullOrEmpty(_passwordText.text))
        {
            Debug.LogWarning("Please enter username and password.");
            return;
        }*/

        await SignInWithUsernamePasswordAsync(_nameText.text, _passwordText.text);

    }

    async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username + userNamePepper, password + passwordPepper); //password pepper 
            Debug.Log("SignUp is successful.");

            Succsess();
        }

        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }

        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username + userNamePepper, password + passwordPepper);
            Debug.Log("SignIn is successful.");
            
            await LoadPlayerDataFromCloudAsync();
            Succsess();
        }

        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {

            Debug.LogException(ex);
        }
    }

    private void Succsess()
    {
        OnSuccess.Invoke();
    }


    //register player data to cloud
    private async Task SaveInitialPlayerData()
    {
        // cloid init
        CloudInit();

        //identity
        int spriteId = _characterSpritesDatabaseSO.GetRandomSpriteId();
        _characterIdentitySO.CharacterSpriteId = spriteId;
        _characterIdentitySO.CharacterName = AuthenticationService.Instance.PlayerName;


        //currency
        _playerCurrencySO.Gold = initialPlayerDataSO.InitialGold;
        _playerCurrencySO.SkillPoints = initialPlayerDataSO.InitialSkillPoints;


        //attributes
        _characterAttributesSO.Xp = initialPlayerDataSO.InitialXP;
        _characterAttributesSO.Strength = initialPlayerDataSO.InitialStrength;
        _characterAttributesSO.Endurance = initialPlayerDataSO.InitialEndurance;
 

        List<Task> tasks = new List<Task>();

        //leaderboard
        Task task1 = LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId,1);
        tasks.Add(task1);

        //Inventory setup   
        for (int i = 0; i < 6; i++)
        {
            AddInventoryItemOptions options = new AddInventoryItemOptions
            {
                PlayersInventoryItemId = "InventorySlotId_" + i,
                InstanceData = null,
            };

            Task task = EconomyService.Instance.PlayerInventory.AddInventoryItemAsync("INVENTORYITEM", options);
            tasks.Add(task);
        }

        //Gear setup
        string[] gearSlots = { "Hat", "Armor", "Shoes", "Necklace", "Gloves", "Ring", "SpecialItem" };

        foreach (string slot in gearSlots)
        {
            AddInventoryItemOptions options = new AddInventoryItemOptions
            {
                PlayersInventoryItemId = "GearSlotId_" + slot,
                InstanceData = null,
            };

            Task task = EconomyService.Instance.PlayerInventory.AddInventoryItemAsync("GEARITEM", options);
            tasks.Add(task);
        }

        // Wait for all tasks to complete
        await Task.WhenAll(tasks);

    }


    public async Task LoadPlayerDataFromCloudAsync()
    {

        //get identity
        var loadIdentityTask = _characterIdentitySO.LoadIdentityFromCloud();
        var loadCurrencyTask = _playerCurrencySO.LoadCurrencyFromCloud();
        var loadAttributesTask = _characterAttributesSO.LoadAttributesFromCloud();
        var loadInventoryTask = _playerInventorySO.LoadInventoryFromCloud();
        var loadCollectiblesTask = _collectorsBookSO.LoadCollectiblesFromCloud();

        // Wait for all tasks to complete
        await Task.WhenAll(loadIdentityTask, loadCurrencyTask, loadAttributesTask, loadInventoryTask, loadCollectiblesTask);

        //cloud init
        CloudInit();
    }

    private void CloudInit()
    {
        // init cloud
        _cloudSaveAttributesSO.Initialize();
        _cloudSaveCurrencySO.Initialize();
        _cloudSaveIdentitySO.Initialize();
        _cloudSaveInventorySO.Initialize();
        _cloudSaveCollectiblesSO.Initialize();
        Debug.Log("Cloud initialized");
    }

    private void OnDestroy()
    {
        _cloudSaveAttributesSO.DeInitialize();
        _cloudSaveCurrencySO.DeInitialize();
        _cloudSaveIdentitySO.DeInitialize();
        _cloudSaveInventorySO.DeInitialize();
        _cloudSaveCollectiblesSO.DeInitialize();
        Debug.Log("Cloud Deinitialized");
    }

}

[Serializable]
public class ItemDTO
{
    public string ItemName;  // ID
    public string ItemType;  // Hat, Armor...
    public int InventorySlotId;       // ignore if gearInv
}
