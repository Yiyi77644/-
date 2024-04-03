using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDes : MonoBehaviour
{
    public static InventoryDes _instance;

    private Text txt_itemDes;

    private void Awake()
    {
        _instance = this;
        txt_itemDes = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示物品描述
    /// </summary>
    /// <param name="id">鼠标指针当前指向的物品</param>
    public void ShowDes(int id)
    {
        gameObject.SetActive(true);
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        transform.position = Input.mousePosition;
        string des = "";

        switch (info.type)
        {
            case ObjectType.Drug:
                des = GetDrugDes(info);
                break;
            case ObjectType.Equip:
                des = GetEquipDes(info);
                break;
        }
        txt_itemDes.text = des;
    }

    public void HideDes()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 获得关于药品的描述
    /// </summary>
    /// <param name="info">物品信息</param>
    /// <returns>返回一个字符串，内容是关于药品的文字描述</returns>
    private string GetDrugDes(ObjectInfo info)
    {
        string str = "名称：" + info.name + "\n+HP：" + info.hp
            + "\n+MP：" + info.mp + "\n出售价：" + info.priceSell
            + "\n购买价：" + info.priceBuy;

        return str;
    }

    /// <summary>
    /// 获得关于药品的描述
    /// </summary>
    /// <param name="info"></param>
    /// <returns返回一个字符串，内容是关于药品的文字描述></returns>
    string GetEquipDes(ObjectInfo info)
    {
        string str = "";
        str += "名称：" + info.name + "\n";
        switch (info.dressType)
        {
            case DressType.Headgear:
                str += "穿戴类型：头盔\n";
                break;
            case DressType.Armor:
                str += "穿戴类型：盔甲\n";
                break;
            case DressType.LeftHand:
                str += "穿戴类型：左手\n";
                break;
            case DressType.RightHand:
                str += "穿戴类型：右手\n";
                break;
            case DressType.Shoes:
                str += "穿戴类型：鞋\n";
                break;
            case DressType.Accessory:
                str += "穿戴类型：饰品\n";
                break;
        }
        switch (info.applicationType)
        {
            case ApplicationType.Swordman:
                str += "适用类型：剑士\n";
                break;
            case ApplicationType.Magician:
                str += "适用类型：魔法师\n";
                break;
            case ApplicationType.Common:
                str += "适用类型：通用\n";
                break;
        }

        str += "伤害值：" + info.attack + "\n" + "防御值：" + info.defend + "\n"
            + "速度值：" + info.speed + "\n" + "出售价：" + info.priceSell + "\n"
            + "购买价：" + info.priceBuy;

        return str;
    }
}
