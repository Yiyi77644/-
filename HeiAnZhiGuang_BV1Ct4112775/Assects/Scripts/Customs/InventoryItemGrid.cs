using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 管理背包格子
/// </summary>
public class InventoryItemGrid : MonoBehaviour
{
    /// <summary>
    /// 表示当前背包格子里面存储了什么物品.默认为0，表示当前背包格子中什么也没有
    /// </summary>
    public int itemId = 0;
    /// <summary>
    /// 表示当前背包格子中的物品的数量。默认为0.
    /// </summary>
    public int itemCount = 0;
    /// <summary>
    /// 当前背包格子中的物品的信息
    /// </summary>
    private ObjectInfo info = null;


    private Text txt_itemCount;

    private void Start()
    {
        txt_itemCount = transform.Find("InventoryItemCount").GetComponent<Text>();


    }

    /// <summary>
    /// 当背包格子中的物品发生变化时（包括类型、图标、数量等），就更新背包格子中关于物品的显示。
    /// </summary>
    /// <param name="id">物品的属性：id</param>
    /// <param name="num">物品的数量，默认为1</param>
    public void SetId(int id, int num = 1)
    {
        itemId = id;
        //先得到物品信息
        info = ObjectsInfo._instance.GetObjectInfoById(id);
        //更新背包格子中的显示
        InventoryItem item = GetComponentInChildren<InventoryItem>();
        item.SetIconName(id, info.iconName);//info.iconName是从txt文件中解析得来的

        txt_itemCount.gameObject.SetActive(true);
        itemCount = num;
        txt_itemCount.text = itemCount.ToString();
        //transform.SetAsLastSibling()可以让物体始终显示在其他兄弟物体的最上方
        txt_itemCount.gameObject.transform.SetAsLastSibling();
    }

    /// <summary>
    /// 背包格子中的物体数目增加，相应的显示数字也应当变化
    /// </summary>
    /// <param name="num">物品增加的数量</param>
    public void PlusNumber(int num = 1)
    {
        itemCount += num;
        txt_itemCount.text = itemCount.ToString();
    }

    /// <summary>
    /// 用于减去背包中物品的数量
    /// </summary>
    /// <param name="num"></param>
    /// <return>是否减量成功</return>
    public bool MinusNumber(int num = 1)
    {
        if (itemCount >= num)
        {
            itemCount -= num;
            txt_itemCount.text = itemCount.ToString();
            if (itemCount == 0)
            {
                //清空物品格子
                CleanInfo();
                //销毁物品
                Destroy(GetComponentInChildren<InventoryItem>().gameObject);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 清空背包格子中的物品信息
    /// </summary>
    public void CleanInfo()
    {
        itemId = 0;
        info = null;
        itemCount = 0;
        txt_itemCount.gameObject.SetActive(false);
    }

}
