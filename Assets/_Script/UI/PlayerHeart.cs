using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeart : MyMonoBehaviour
{
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    [SerializeField] private Image[] _heartList;

    private void OnEnable()
    {
        Kid.OnUpdatePlayerHealth += UpdateHeart;
    }

    private void OnDisable()
    {
        Kid.OnUpdatePlayerHealth -= UpdateHeart;
    }

    public void UpdateHeart(float currentHeart , float maxHeart)
    {
        for (int i = 0; i < _heartList.Length; i++)
        {
            if(i < currentHeart)
            {
                _heartList[i].sprite = _fullHeart;
            }
            else
            {
                _heartList[i].sprite = _emptyHeart;
            }
            if(i < maxHeart)
            {
                _heartList[i].enabled = true;
            }
            else
            {
                _heartList[i].enabled = false;
            }
        }
    }
}
