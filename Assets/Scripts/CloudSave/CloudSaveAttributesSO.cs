using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.Leaderboards;
using UnityEngine;

[CreateAssetMenu]
public class CloudSaveAttributesSO : CloudSaveInitializable
{
    [SerializeField] private CharacterAttributesSO _characterAttributesSO;

    // Variables declaration
    private Dictionary<string, object> saveData;

  
    // Subscribe and unsubscribe methods
    protected override void SubscribeToPlayerEvents()
    {
        _characterAttributesSO.OnXPChanged += UpdateXpValue;
        _characterAttributesSO.OnStrengthChanged += UpdateStrengthValue;
        _characterAttributesSO.OnEnduranceChanged += UpdateEnduranceValue;
    }

    protected override void UnsubscribeFromPlayerEvents()
    {
        _characterAttributesSO.OnXPChanged -= UpdateXpValue;
        _characterAttributesSO.OnStrengthChanged -= UpdateStrengthValue;
        _characterAttributesSO.OnEnduranceChanged -= UpdateEnduranceValue;
    }

    // Event handlers
    private async void UpdateXpValue(int xp)
    {
        await UpdateXpValueTask(xp);
    }

    private async void UpdateStrengthValue(int strength)
    {
        await UpdateStrengthValueTask(strength);
    }

    private async void UpdateEnduranceValue(int endurance)
    {
        await UpdateEnduranceValueTask(endurance);
    }

    // Task methods
    private async Task UpdateXpValueTask(int xp)
    {
        saveData = new Dictionary<string, object> { { "xp", xp } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        await LeaderboardsService.Instance.AddPlayerScoreAsync("LEADERBOARD", 1);
        Debug.Log("XpValueSaved");
    }

    private async Task UpdateStrengthValueTask(int strength)
    {
        saveData = new Dictionary<string, object> { { "strength", strength } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log("StrengthValueSaved");
    }

    private async Task UpdateEnduranceValueTask(int endurance)
    {
        saveData = new Dictionary<string, object> { { "endurance", endurance } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log("EnduranceValueSaved");
    }
 
}
