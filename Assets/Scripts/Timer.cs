using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] float maxTime;
    [SerializeField] float time;

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] UnityEvent m_MyEvent;

    private void OnEnable()
    {
        time = maxTime;
    }

    private void Update()
    {
       time = time - Time.deltaTime;

       slider.value = time/ maxTime;

       text.text = (int)time+1 + "";
        
       if(time<= 0) {m_MyEvent.Invoke();}
    }
}
