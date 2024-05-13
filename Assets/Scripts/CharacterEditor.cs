using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEditor : MonoBehaviour
{
    [SerializeField] CharacterSpritesDatabaseSO _characterSpritesDatabaseSO;
    [SerializeField] Sprite _characterSprite;
    [SerializeField] int i = 0;
    [SerializeField] Image _characterRenderer;
    [SerializeField] private CharacterIdentitySO _characterIdentity;

    private void OnEnable()
    {
        InitialSprite();
    }
    
    public void InitialSprite()
    {
      i = _characterIdentity.CharacterSpriteId;
      _characterSprite = _characterSpritesDatabaseSO.GetSprite(i);
      _characterRenderer.sprite = _characterSprite;
    }

    public void Left()
    {
        i --;
        _characterSprite = _characterSpritesDatabaseSO.GetSprite(i);
        _characterRenderer.sprite = _characterSprite;
    }

    public void Right() 
    {
        i++;
        _characterSprite = _characterSpritesDatabaseSO.GetSprite(i);
        _characterRenderer.sprite = _characterSprite;
    }

    public void Save()
    {        
        _characterSprite = _characterSpritesDatabaseSO.GetSprite(i);
        _characterIdentity.CharacterSpriteId = i;
    }

}
