using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/GameData/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{

    static PlayerData _instance;

    public static PlayerData Instance
    {
        get 
        {
            if (_instance == null)
            {          
                _instance = Resources.Load<PlayerData>("PlayerData");
                OnInit();
            }
            return _instance; 
        }
    }
 
    // Currency
    [Space, Header("Currency")]
    [SerializeField] private int _gold;
    public event Action<int> OnGoldChanged;
    public int Gold
    {
        get { return _gold; }
        set
        {
            if (_gold != value)
            {
                _gold = value;
                OnGoldChanged?.Invoke(_gold);             
            }
        }
    }

    [SerializeField] private int _skillPoints;
    public event Action<int> OnSkillPointsChanged;
    public int SkillPoints
    {
        get { return _skillPoints; }
        set
        {
            if (_skillPoints != value)
            {
                _skillPoints = value;
                OnSkillPointsChanged?.Invoke(_skillPoints);
            }
        }
    }

    [SerializeField] private int _treePoints;
    public event Action<int> OnTreePointsChanged;
    public int TreePoints
    {
        get { return _treePoints; }
        set
        {
            if (_treePoints != value)
            {
                _treePoints = value;
                OnTreePointsChanged?.Invoke(_treePoints);
            }
        }
    }

    // Attributes
    [Space, Header("Attributes")]
    [SerializeField] private int _level;
    public event Action<int> OnLevelChanged;
    public int Level
    {
        get { return _level; }
        set
        {
            if (_level != value)
            {
                _level = value;
                OnLevelChanged?.Invoke(_level);
            }
        }
    }

    [SerializeField] private int _xp;
    public event Action<int> OnXPChanged;
    public int XP
    {
        get { return _xp; }
        set
        {
            if (_xp != value)
            {
                _xp = value;
                OnXPChanged?.Invoke(_xp);
            }
        }
    }

    [SerializeField] private int _strength;
    public event Action<int> OnStrengthChanged;
    public int Strength
    {
        get { return _strength; }
        set
        {
            if (value != _strength)
            {
                _strength = value;
                OnStrengthChanged?.Invoke(_strength);
            }
        }
    }

    [SerializeField] private int _intelligence;
    public event Action<int> OnIntelligenceChanged;
    public int Intelligence
    {
        get { return _intelligence; }
        set
        {
            if (value != _intelligence)
            {
                _intelligence = value;
                OnIntelligenceChanged?.Invoke(_intelligence);
            }
        }
    }

    [SerializeField] private int _dexterity;
    public event Action<int> OnDexterityChanged;
    public int Dexterity
    {
        get { return _dexterity; }
        set
        {
            if (value != _dexterity)
            {
                _dexterity = value;
                OnDexterityChanged?.Invoke(_dexterity);
            }
        }
    }

    [SerializeField] private int _luck;
    public event Action<int> OnLuckChanged;
    public int Luck
    {
        get { return _luck; }
        set
        {
            if (value != _luck)
            {
                _luck = value;
                OnLuckChanged?.Invoke(_luck);
            }
        }
    }

    [SerializeField] private int _health;
    public event Action<int> OnHealthChanged;
    public int Health
    {
        get { return _health; }
        set
        {
            if (value != _health)
            {
                _health = value;
                OnHealthChanged?.Invoke(_health);
            }
        }
    }

    private static void OnInit()
    {
        // Naètìte data z uloženého JSONu pøi aktivaci objektu
        string jsonData = PlayerPrefs.GetString("PlayerData", "");
        if (!string.IsNullOrEmpty(jsonData))
        {
            JsonUtility.FromJsonOverwrite(jsonData, Instance);
        }
    }

    private void OnDisable()
    {
        // Pøevedení dat na JSON a uložení do PlayerPrefs pøi deaktivaci objektu
        string jsonData = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("PlayerData", jsonData);
        PlayerPrefs.Save();
    }


}
