using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class CharacterAttributesSO : ScriptableObject
{

    [SerializeField] private CharacterScalingSO _characterScalingSO;
 

    private void OnDisable()
    {
        _xp = 0;
        _level = 0;
        _strength = 0;
        _endurance = 0;
        _health = 0;
        _damage = 0;
    }


    [SerializeField][ReadOnly] private int _xp;
    public event Action<int> OnXPChanged;
    public int Xp
    {
        get { return _xp; }
        set
        {
            _xp = value;
            CalculateLevelFromXp(value);
            OnXPChanged?.Invoke(_xp);
        }

    }

    [SerializeField][ReadOnly] private int _level;
    public event Action<int> OnLevelChanged;
    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;
            OnLevelChanged?.Invoke(_level);
        }
    }

    // Attributes
    [SerializeField][ReadOnly] private int _strength;
    public event Action<int> OnStrengthChanged;
    public int Strength
    {
        get { return _strength; }
        set
        {
            _strength = value;
            CalculateDamageFromStrength(value);
            OnStrengthChanged?.Invoke(_strength);
        }
    }

    [SerializeField][ReadOnly] private int _endurance;
    public event Action<int> OnEnduranceChanged;
    public int Endurance
    {
        get { return _endurance; }
        set
        {     
            _endurance = value;
            CalculateHealthFromEndurance(value);
            OnEnduranceChanged?.Invoke(_endurance);
        }
    }


    [SerializeField][ReadOnly] private int _health;
    public event Action<int> OnHealthChanged;
    public int Health
    {
        get { return _health; }
        set
        {  
            _health = value;
            OnHealthChanged?.Invoke(_health);      
        }
    }

    [SerializeField][ReadOnly] private int _damage;
    public event Action<int> OnDamageChanged;
    public int Damage
    {
        get { return _damage; }
        private set
        {
            if (_damage != value)
            {
                _damage = value;
                OnDamageChanged?.Invoke(_damage);
            }
        }
    }
 


    private void CalculateHealthFromEndurance(int endurance)
    {
        Health = endurance * _characterScalingSO.EndurancePerPoint;
    }

    private void CalculateDamageFromStrength(int strength)
    {
        Damage = strength * _characterScalingSO.StrengthPerPoint;
    }

    private void CalculateLevelFromXp(int xp)
    {
        // initial level
        int level = 1;
        var xpScale = _characterScalingSO.XpPerLevelScaling;

        if (xp >= xpScale)
        {
            level = xp / xpScale;
        }

        Level = level;
    }

    public async Task LoadAttributesFromCloud()
    {

        var serverData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "xp", "strength", "endurance", "characterSpriteId" });

        if (serverData.TryGetValue("xp", out var XPValue))
           Xp = XPValue.Value.GetAs<int>();

        if (serverData.TryGetValue("strength", out var strengthValue))
           Strength = strengthValue.Value.GetAs<int>();

        if (serverData.TryGetValue("endurance", out var enduranceValue))
            Endurance = enduranceValue.Value.GetAs<int>();

    }

}