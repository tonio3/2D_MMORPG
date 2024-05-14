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


    private void OnDisable()
    {
        _characterSprite = null;
        _characterId = "";
        _characterName = "";
        _characterSpriteId = -1;
    }

    [Space]
    //character Skin
    [NonSerialized]
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

    [NonSerialized]
    [SerializeField][ReadOnly] private int _characterSpriteId;
    public event Action<int> OnCharacterSpriteIdChanged;
    public int CharacterSpriteId
    {
        get { return _characterSpriteId; }
        set
        {
            _characterSpriteId = value;
            SetCharacterSprite(value);
            OnCharacterSpriteIdChanged?.Invoke(_characterSpriteId);
        }
    }

    [NonSerialized]
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

    [NonSerialized]
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
        CharacterName = myname;


        //sprite
        var serverData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {"characterSpriteId" });

        if (serverData.TryGetValue("characterSpriteId", out var sprId))
           CharacterSpriteId = sprId.Value.GetAs<int>();

    }

}