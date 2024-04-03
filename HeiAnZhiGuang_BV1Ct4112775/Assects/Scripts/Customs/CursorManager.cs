using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public  static CursorManager _instance;

    public Texture2D img_cursorNormal;
    public Texture2D img_cursorNpcTalk;
    public Texture2D img_cursorAttack;
    public Texture2D img_cursorLockTarget;
    public Texture2D img_cursorPickup;

    /// <summary>
    /// 鼠标指针的点击位置，一般为鼠标图标的左上角
    /// </summary>
    private Vector2 hotSpot = Vector2.zero;
    /// <summary>
    /// 
    /// </summary>
    private CursorMode mode = CursorMode.Auto;



    private void Start()
    {
        _instance = this;
    }
    public void SetNormal()
    {
        Cursor.SetCursor(img_cursorNormal, hotSpot, mode);

    }

    public void SetNpcTalk()
    {

        Cursor.SetCursor(img_cursorNpcTalk, hotSpot, mode);
    }

}
