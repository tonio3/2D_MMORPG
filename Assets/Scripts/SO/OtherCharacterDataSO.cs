using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Economy;
using UnityEngine;

//[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class OtherCharacterDataSO : ScriptableObject
{
 
    [SerializeField] private CharacterAttributesSO _characterAttributes;
    public CharacterAttributesSO CharacterAttributes => _characterAttributes;

    [Space]

    [SerializeField] private CharacterIdentitySO _characterIdentity;
    public CharacterIdentitySO CharacterIdentity => _characterIdentity;
   
   
     
}
