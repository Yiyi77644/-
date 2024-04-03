using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    public static EquipmentPanel _instance;

    private GameObject headgear;
    private GameObject armor;
    private GameObject rightHand;
    private GameObject leftHand;
    private GameObject shoes;
    private GameObject accessory;

    private PlayerStatus playerStatus;

    public GameObject equipmentItem;
    /// <summary>
    /// 装备面板UI是否显示。
    /// </summary>
    /// 如果当前是显示状态，则为true，否则为false
    private bool isShowEquip = false;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        headgear = transform.Find(S_GameObjectName.EP_img_headgear).gameObject;
        armor = transform.Find(S_GameObjectName.EP_img_armor).gameObject;
        rightHand = transform.Find(S_GameObjectName.EP_img_rightHand).gameObject;
        leftHand = transform.Find(S_GameObjectName.EP_img_leftHand).gameObject;
        shoes = transform.Find(S_GameObjectName.EP_img_shoes).gameObject;
        accessory = transform.Find(S_GameObjectName.EP_img_accessory).gameObject;

        playerStatus = GameObject.FindGameObjectWithTag(S_Tags.player).GetComponent<PlayerStatus>();

        gameObject.SetActive(false);
    }



    /// <summary>
    /// 穿戴/卸下某个id的装备
    /// </summary>
    /// <param name="id">所穿戴装备的id</param>
    /// <returns>返回：如果穿戴成功，则返回true，否则返回false</returns>
    public bool Dress(int id)
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        if (info.type != ObjectType.Equip)//穿戴不成功
        {
            return false;
        }
        if (playerStatus.heroType == HeroType.Magician 
            && info.applicationType == ApplicationType.Swordman)
        {
            return false;
        }
        if (playerStatus.heroType == HeroType.Swordman 
            && info.applicationType == ApplicationType.Magician)
        {
            return false;
        }

        GameObject parent = null;
        switch (info.dressType)
        {
            case DressType.Headgear:
                parent = headgear;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.RightHand:
                parent = rightHand;
                break;
            case DressType.LeftHand:
                parent = leftHand;
                break;
            case DressType.Shoes:
                parent = shoes;
                break;
            case DressType.Accessory:
                parent = accessory;
                break;
        }
        EquipmentItem item = parent.GetComponentInChildren<EquipmentItem>();

        //已经穿戴了同样类型的装备
        if (item != null)
        {
            //把已经穿戴的装备卸下，放回物品栏
            InventoryPanel._instance.GetId(item.equipId);
            item.SetInfo(info);
        }
        else
        {//没有穿戴同样类型的装备
            GameObject tmpGO = Resources.Load<GameObject>("Prefabs/EquipmentItem");
            GameObject itemGo = Instantiate(tmpGO, parent.transform);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<EquipmentItem>().SetInfo(info);
        }
        //UpdateProperty();
        return true;
    }

    public void TransformState()
    {
        if (!isShowEquip)
        {
            gameObject.SetActive(true);
            isShowEquip = true;
        }
        else
        {
            gameObject.SetActive(false);
            isShowEquip = false;
        }
    }
}
