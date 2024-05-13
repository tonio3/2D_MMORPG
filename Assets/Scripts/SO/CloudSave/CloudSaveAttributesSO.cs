using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.Leaderboards;
using UnityEngine;

[CreateAssetMenu]
public class CloudSaveAttributesSO: ScriptableObject
{
    [SerializeField] private CharacterAttributesSO _characterAttributesSO;

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
        _characterAttributesSO.OnXPChanged += UpdateXpValue;
        _characterAttributesSO.OnStrengthChanged += UpdateStrengthValue;
        _characterAttributesSO.OnEnduranceChanged += UpdateHpValue;
    }


    private void UnsubscribeFromPlayerEvents()
    {
        _characterAttributesSO.OnXPChanged -= UpdateXpValue;
        _characterAttributesSO.OnStrengthChanged -= UpdateStrengthValue;
        _characterAttributesSO.OnEnduranceChanged -= UpdateHpValue;
    }


    private async void UpdateXpValue(int xp)
    {
        var saveData = new Dictionary<string, object>();
        saveData["xp"] = xp;
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        await LeaderboardsService.Instance.AddPlayerScoreAsync("LEADERBOARD", 1);
        Debug.Log("XpValueSaved");
    }

    private void UpdateStrengthValue(int strength)
    {
        var saveData = new Dictionary<string, object>();
        saveData["strength"] = strength;
        CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log("StrengthValueSaved");
    }

    private void UpdateHpValue(int hp)
    {
        var saveData = new Dictionary<string, object>();
        saveData["health"] = hp;
        CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log("HealthValueSaved");
    }
 
}
