using UnityEditor;
using UnityEngine;
using Fishing;
using System.Collections.Generic;

public class FishDatabaseSOEditorWindow : EditorWindow
{
    private string _name = "New SO";
    private Sprite _sprite;
    private RaritySO _rarity;
    private int _hp;
    private int _damage;
    private int _level;
    private Sprite _weapon;
    private CollectibleType _collectibleType;
    private FishDatabaseSO _database;

    private FishSO _selectedFish;
    private FishBossSO _selectedFishBoss;
    private FishingJunkItemSO _selectedJunkItem;

    public enum CollectibleType
    {
        JunkItem,
        Fish,
        FishBoss
    }

    [MenuItem("Window/Fish Database SO Editor")]
    public static void ShowWindow()
    {
        GetWindow<FishDatabaseSOEditorWindow>("Fish Database SO Editor");
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();

        _database = (FishDatabaseSO)EditorGUILayout.ObjectField("Database", _database, typeof(FishDatabaseSO), false);

        if (_database == null)
        {
            EditorGUILayout.HelpBox("Please assign a FishDatabaseSO.", MessageType.Warning);
            return;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Create New", EditorStyles.boldLabel);

        _collectibleType = (CollectibleType)EditorGUILayout.EnumPopup("Collectible Type", _collectibleType);
        _name = EditorGUILayout.TextField("Name", _name);
        _sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", _sprite, typeof(Sprite), false);
        _rarity = (RaritySO)EditorGUILayout.ObjectField("Rarity", _rarity, typeof(RaritySO), false);

        switch (_collectibleType)
        {
            case CollectibleType.JunkItem:
                DisplayJunkItemProperties();
                break;
            case CollectibleType.Fish:
                DisplayFishProperties();
                break;
            case CollectibleType.FishBoss:
                DisplayFishBossProperties();
                break;
        }

        if (GUILayout.Button("Create SO"))
        {
            switch (_collectibleType)
            {
                case CollectibleType.JunkItem:
                    _database.CreateJunkItemSO(_name, _sprite, _rarity);
                    break;
                case CollectibleType.Fish:
                    _database.CreateFishSO(_name, _sprite, _rarity);
                    break;
                case CollectibleType.FishBoss:
                    _database.CreateFishBossSO(_name, _sprite, _rarity, _level, _hp, _damage, _weapon);
                    break;
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Remove Selected", EditorStyles.boldLabel);

        _selectedFish = (FishSO)EditorGUILayout.ObjectField("Selected Fish", _selectedFish, typeof(FishSO), false);
        if (_selectedFish != null)
        {
            if (GUILayout.Button("Remove Fish"))
            {
                _database.RemoveFish(_selectedFish);
                _selectedFish = null;
            }
        }

        _selectedFishBoss = (FishBossSO)EditorGUILayout.ObjectField("Selected Fish Boss", _selectedFishBoss, typeof(FishBossSO), false);
        if (_selectedFishBoss != null)
        {
            if (GUILayout.Button("Remove Fish Boss"))
            {
                _database.RemoveFishBoss(_selectedFishBoss);
                _selectedFishBoss = null;
            }
        }

        _selectedJunkItem = (FishingJunkItemSO)EditorGUILayout.ObjectField("Selected Junk Item", _selectedJunkItem, typeof(FishingJunkItemSO), false);
        if (_selectedJunkItem != null)
        {
            if (GUILayout.Button("Remove Junk Item"))
            {
                _database.RemoveJunkItem(_selectedJunkItem);
                _selectedJunkItem = null;
            }
        }
    }

    private void DisplayJunkItemProperties()
    {
        // Implement properties for Junk Item if any
    }

    private void DisplayFishProperties()
    {
        // Implement properties for Fish if any
    }

    private void DisplayFishBossProperties()
    {
        EditorGUILayout.Space();
        _level = EditorGUILayout.IntField("Level", _level);
        _hp = EditorGUILayout.IntField("HP", _hp);
        _damage = EditorGUILayout.IntField("Damage", _damage);
        _weapon = (Sprite)EditorGUILayout.ObjectField("Weapon Sprite", _weapon, typeof(Sprite), false);
    }
}
