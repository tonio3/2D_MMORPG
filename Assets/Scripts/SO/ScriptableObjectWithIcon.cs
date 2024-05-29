using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ScriptableObjectWithIcon :ScriptableObject{
  
    [FormerlySerializedAs("Spr")] public Sprite MainSprite;

}