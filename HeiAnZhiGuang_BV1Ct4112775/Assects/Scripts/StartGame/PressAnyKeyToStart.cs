using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKeyToStart : MonoBehaviour
{
    /// <summary>
    /// 标志位，表示是否有按键按下。
    /// </summary>
    private bool isAnyKeyDown = false;
    private GameObject btn_Container;

    private void Start()
    {
        btn_Container = transform.parent.parent.Find(S_GameObjectName.BtnPanel).gameObject;
    }

    void Update()
    {
        if (isAnyKeyDown == false)
        {
            if(Input .anyKey)
            {
                // show button container
                // hide self
                ShowButton();
            }
        }
    }

    private void ShowButton()
    {
        btn_Container.SetActive(true);
        gameObject.SetActive(false);
        isAnyKeyDown = true;
    }
}
