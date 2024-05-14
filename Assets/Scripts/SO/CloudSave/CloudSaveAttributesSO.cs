using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.Leaderboards;
using UnityEngine;

[CreateAssetMenu]
public class CloudSaveAttributesSO : ScriptableObject
{
    [SerializeField] private CharacterAttributesSO _characterAttributesSO;


    private int _xp,_strength,_endurance;

    public void InitAttributes(int xp, int strength, int endurance)
    {
        _xp = xp;
        _strength = strength;
        _endurance = endurance;
    }


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
        _characterAttributesSO.OnEnduranceChanged += UpdateEnduranceValue;
    }


    private void UnsubscribeFromPlayerEvents()
    {
        _characterAttributesSO.OnXPChanged -= UpdateXpValue;
        _characterAttributesSO.OnStrengthChanged -= UpdateStrengthValue;
        _characterAttributesSO.OnEnduranceChanged -= UpdateEnduranceValue;
    }


    private async void UpdateXpValue(int xp)
    {

        if (xp == _xp) return;

        var saveData = new Dictionary<string, object>();
        saveData["xp"] = xp;
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        await LeaderboardsService.Instance.AddPlayerScoreAsync("LEADERBOARD", 1);
        Debug.Log("XpValueSaved");
    }

    private async void UpdateStrengthValue(int strength)
    {
        if (strength == _strength) return;

        var saveData = new Dictionary<string, object>();
        saveData["strength"] = strength;
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log("StrengthValueSaved");
    }

    private async void UpdateEnduranceValue(int endur)
    {
        await TaskUpdate(endur);
    }

    private async Task TaskUpdate(int endur)
    {
        if (endur == _endurance) return;
        var saveData = new Dictionary<string, object>();
        saveData["endurance"] = endur;
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log("EnduranceValueSaved");
    }
 
}
