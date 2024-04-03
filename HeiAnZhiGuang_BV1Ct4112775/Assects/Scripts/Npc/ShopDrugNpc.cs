using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDrugNpc : Npc
{
    //当鼠标一直在这个游戏物体之上时，会一直调用这个方法
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShopDrugPanel._instance.TransformState();
        }
    }
}
