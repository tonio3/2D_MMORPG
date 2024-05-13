using System;
using System.Collections;
using System.Collections.Generic;
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

    private async void UpdateName(string newName)
    {
        // await AuthenticationService.Instance.UpdatePlayerNameAsync(newName);
        Debug.Log("NameUpdated_Offline");
    }

    private void UpdateCharacterSpriteId(int sprId)
    {
        var saveData = new Dictionary<string, object>();
        saveData["characterSpriteId"] = sprId;
        CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log("CharacterSpriteSaved");
    }
 

}
