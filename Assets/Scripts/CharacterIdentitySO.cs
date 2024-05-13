using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class CharacterIdentitySO : ScriptableObject
{

    [SerializeField] private CharacterSpritesDatabaseSO _characterSpriteDatabaseSO;
    [Space]

    //character Skin
    [SerializeField][ReadOnly] private Sprite _characterSprite;
    public event Action<Sprite> OnCharacterSpriteChanged;
    public Sprite CharacterSprite
    {
        get { return _characterSprite; }
        set
        {
            _characterSprite = value;
            OnCharacterSpriteChanged?.Invoke(_characterSprite);
        }
    }


    [SerializeField][ReadOnly] private int _characterSpriteId;
    public event Action<int> OnCharacterSpriteIdChanged;
    public int CharacterSpriteId
    {
        get { return _characterSpriteId; }
        set
        {
            if (_characterSpriteId == value) return;

            _characterSpriteId = value;
            SetCharacterSprite(value);
            OnCharacterSpriteIdChanged?.Invoke(_characterSpriteId);
        }
    }

    [SerializeField][ReadOnly] private string _characterId;
    public event Action<string> OnCharacterIdChanged;
    public string CharacterId
    {
        get { return _characterId; }
        set
        {
            _characterId = value;
            OnCharacterIdChanged?.Invoke(_characterId);
        }
    }


    [SerializeField][ReadOnly] private string _characterName;
    public event Action<string> OnCharacterNameChanged;
    public string CharacterName
    {
        get { return _characterName; }
        set
        {
            _characterName = value;
            OnCharacterNameChanged?.Invoke(_characterName);
        }
    }

    //character name
    private void SetCharacterSprite(int value)
    {
        CharacterSprite = _characterSpriteDatabaseSO.GetSprite(value);
    }

    public async Task LoadIdentityFromCloud()
    {
        //name
        var myname = await AuthenticationService.Instance.GetPlayerNameAsync();
        _characterName = myname;


        //sprite
        var serverData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {"characterSpriteId" });

        if (serverData.TryGetValue("characterSpriteId", out var sprId))
            _characterSpriteId = sprId.Value.GetAs<int>();

    }

}