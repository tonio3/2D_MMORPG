using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fishing
{
    public class CollectorsBookSlotUI : MonoBehaviour
    {
        [field: SerializeField] public ScriptableObjectWithIcon Reference { get; private set; }
        [SerializeField] private Image _img;
 
        public bool Obtained
        {
            set
            {
                _img.gameObject.SetActive(value);
                if (value)
                {
                    _img.sprite = Reference.MainSprite;
                }
            }
        }
    }

}
