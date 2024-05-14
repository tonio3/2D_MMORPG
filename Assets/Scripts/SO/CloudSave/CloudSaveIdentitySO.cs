using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;

[CreateAssetMenu]
public class CloudSaveIdentitySO : ScriptableObject
{
    [SerializeField] private CharacterIdentitySO _playerIdentity;

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
        _playerIdentity.OnCharacterNameChanged += UpdateName;
        _playerIdentity.OnCharacterSpriteIdChanged += UpdateCharacterSpriteId;
    }

    private void UnsubscribeFromPlayerEvents()
    {
        _playerIdentity.OnCharacterNameChanged -= UpdateName;
        _playerIdentity.OnCharacterSpriteIdChanged -= UpdateCharacterSpriteId;
    }

    private void UpdateName(string newName)
    {        
        Debug.Log("Name updated");
    }

    private async void UpdateCharacterSpriteId(int sprId)
    {
       await TaskUpdateSpriteID(sprId);
    }

    private async Task TaskUpdateSpriteID(int sprId)
    {
        var saveData = new Dictionary<string, object>();
        saveData["characterSpriteId"] = sprId;
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log("CharacterSpriteSaved");
    }
 

}
