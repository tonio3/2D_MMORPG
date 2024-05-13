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
    public int InitialEndurance => _initialEndurance;

    [SerializeField] private int _initialEndurance;

    //Gold
    public int InitialGold => _initialGold;

    [SerializeField] private int _initialGold;

    //SkillPoints
    public int InitialSkillPoints => _initialSkillPoints;

    [SerializeField] private int _initialSkillPoints;


}
