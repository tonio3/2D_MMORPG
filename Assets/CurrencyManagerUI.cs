using UnityEngine;
using TMPro;

public class CurrencyManagerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _skillPointsText;
    [SerializeField] private TextMeshProUGUI _treePointsText;
    
    private PlayerData _pd;

    private void Awake()
    {
        _pd = PlayerData.Instance;
    }

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
        
        _pd.OnGoldChanged += UpdateGoldText;
        _pd.OnSkillPointsChanged += UpdateSkillPointsText;
        _pd.OnTreePointsChanged += UpdateTreePointsText;
    }

    private void UnsubscribeFromPlayerEvents()
    {
        _pd.OnGoldChanged -= UpdateGoldText;
        _pd.OnSkillPointsChanged -= UpdateSkillPointsText;
        _pd.OnTreePointsChanged -= UpdateTreePointsText;
    }

    private void UpdateGoldText(int gold)
    {
        _goldText.text = "Gold: " + gold;
    }

    private void UpdateSkillPointsText(int skillPoints)
    {
        _skillPointsText.text =skillPoints + "";
    }

    private void UpdateTreePointsText(int treePoints)
    {
        _treePointsText.text = treePoints + "";
    }
}
