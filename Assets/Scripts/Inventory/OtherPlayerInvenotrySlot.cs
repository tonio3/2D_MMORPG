using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherPlayerInvenotrySlot : MonoBehaviour
{
    [field: SerializeField] public Image _img { get; private set; }
    [field:SerializeField] public ItemSO _item { get; set; }
}
