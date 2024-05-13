using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointToAttribute : MonoBehaviour
{
    [SerializeField] private PlayerCurrencySO _currency;
    [SerializeField] private CharacterAttributesSO _PlayerAttributes;

     public void UseAsEndurance()
     {
        if (_currency.SkillPoints == 0) return;
        _currency.SkillPoints--;
        _PlayerAttributes.Endurance++;
     }

     public void UseAsStrength()
     {
        if (_currency.SkillPoints == 0) return;
        _currency.SkillPoints--;
        _PlayerAttributes.Strength++;
     }
}
