using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider xpSlider;
    [Space]
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI intelligenceText;
    [SerializeField] private TextMeshProUGUI dexterityText;
    [SerializeField] private TextMeshProUGUI luckText;
    [SerializeField] private TextMeshProUGUI healthText;

    private PlayerData _pd;

    private void Awake()
    {
        _pd = PlayerData.Instance;
    }

    private void OnEnable()
    {
        SubscribeToPlayerEvents();

        levelText.text = _pd.Level + "";
        xpSlider.value = _pd.XP;
        strengthText.text = _pd.Strength + "";
        intelligenceText.text = _pd.Intelligence + "";
        dexterityText.text = _pd.Dexterity + "";
        luckText.text = _pd.Luck + "";
        healthText.text = _pd.Health + "";
    }

    private void OnDisable()
    {
        UnsubscribeFromPlayerEvents();
    }

    private void SubscribeToPlayerEvents()
    {
        _pd.OnLevelChanged += UpdateLevelText;
        _pd.OnXPChanged += UpdateXPSlider;
        _pd.OnStrengthChanged += UpdateStrengthText;
        _pd.OnIntelligenceChanged += UpdateIntelligenceText;
        _pd.OnDexterityChanged += UpdateDexterityText;
        _pd.OnLuckChanged += UpdateLuckText;
        _pd.OnHealthChanged += UpdateHealthText;
    }

    private void UnsubscribeFromPlayerEvents()
    {
        _pd.OnLevelChanged -= UpdateLevelText;
        _pd.OnXPChanged -= UpdateXPSlider;
        _pd.OnStrengthChanged -= UpdateStrengthText;
        _pd.OnIntelligenceChanged -= UpdateIntelligenceText;
        _pd.OnDexterityChanged -= UpdateDexterityText;
        _pd.OnLuckChanged -= UpdateLuckText;
        _pd.OnHealthChanged -= UpdateHealthText;
    }

    private void UpdateLevelText(int level)
    {
        levelText.text = "Level: " + level;
    }

    private void UpdateXPSlider(int xp)
    {
       // xpSlider.value
    }

    private void UpdateStrengthText(int strength)
    {
        strengthText.text = strength + "";
    }

    private void UpdateIntelligenceText(int intelligence)
    {
        intelligenceText.text = intelligence + "";
    }

    private void UpdateDexterityText(int dexterity)
    {
        dexterityText.text = dexterity + "";
    }

    private void UpdateLuckText(int luck)
    {
        luckText.text = luck + "";
    }

    private void UpdateHealthText(int health)
    {
        healthText.text = health + "";
    }
}
