using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;

[CreateAssetMenu]
public class CloudSaveIdentitySO : CloudSaveInitializable
{
    [SerializeField] private CharacterIdentitySO _playerIdentity;
 

    protected override void SubscribeToPlayerEvents()
    {
        _playerIdentity.OnCharacterNameChanged += UpdateName;
        _playerIdentity.OnCharacterSpriteIdChanged += UpdateCharacterSpriteId;
    }

    protected override void UnsubscribeFromPlayerEvents()
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
       await UpdateSpriteIDTask(sprId);
    }

    private async Task UpdateSpriteIDTask(int sprId)
    {
        var saveData = new Dictionary<string, object>();
        saveData["characterSpriteId"] = sprId;
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log("CharacterSpriteSaved");
    }
 

}
