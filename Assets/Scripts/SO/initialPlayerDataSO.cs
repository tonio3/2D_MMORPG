using UnityEngine;

[CreateAssetMenu(fileName = "InitialPlayerData", menuName = "ScriptableObjects/Player/InitialPlayerData")]
public class InitialPlayerData : ScriptableObject
{
 
    //XP
    [SerializeField] private int _initialXP;
    public int InitialXP => _initialXP;

    //STRENGTH
    [SerializeField] private int _initialStrength;
    public int InitialStrength => _initialStrength;

    //HEALTH

    [SerializeField] private int _initialEndurance;
    public int InitialEndurance => _initialEndurance;


    //Gold
    [SerializeField] private int _initialGold;
    public int InitialGold => _initialGold;


    //SkillPoints
    [SerializeField] private int _initialSkillPoints;
    public int InitialSkillPoints => _initialSkillPoints;
 
}
