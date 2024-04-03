using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 管理背包
/// </summary>
public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel _instance;

    /// <summary>
    /// 将背包方格上的脚本存起来
    /// </summary>
    public List<InventoryItemGrid> itemGridList = new List<InventoryItemGrid>();

    public Text txt_coinCount;
    /// <summary>
    /// 根据所给的Prefab创建一个物体
    /// </summary>
    public GameObject inventoryItem;


    private int coinCount = 1000;

    /// <summary>
    /// 背包UI是否显示。
    /// </summary>
    /// 如果当前是显示状态，则为true，否则为false
    private bool isShow = false;



    private void Awake()
    {
        _instance = this;

    }

    private void Start()
    {
        gameObject.SetActive(false);
    }



    /// <summary>
    /// 拾取到id为id的物品，并将其添加到背包格子中
    /// </summary>
    /// 处理拾取物品的功能
    /// <param name="id">所拾取/购买物品的id</param>
    /// <param name="count">物品的数量</param>
    public void GetId(int id, int count = 1)
    {
        //第一步：先查找在背包所有物品中，是否存在所拾取的物品
        //第二步：如果存在，itemCount+1;
        //第三步：如果不存在，查找空的格子，然后将新创建的InventoryItem放入这个空的背包格子里面
        InventoryItemGrid grid = null;

        foreach (InventoryItemGrid tempGrid in itemGridList)
        {
            if (tempGrid.itemId == id)
            {
                grid = tempGrid;
                break;
            }

        }

        if (grid != null)//背包中有该物品
        {
            grid.PlusNumber(count);
        }
        else//背包中没有该物品
        {
            foreach (InventoryItemGrid tempGrid in itemGridList)
            {
                if (tempGrid.itemId == 0)
                {
                    grid = tempGrid;
                    break;
                }
            }

            if (grid != null)
            {
                GameObject itemGO = Instantiate(inventoryItem, grid.gameObject.transform, false);
                //更新该背包格子的信息
                grid.SetId(id, count);

            }
        }

    }


    /// <summary>
    /// 获取背包中存储的金币数量
    /// </summary>
    /// <param name="count">购买物品需要花费的金币数量</param>
    /// <returns>如果背包中的金币数大于等于购买物品所需的金币数，则返回true，否则返回false</returns>
    public bool GetCoinCount(int count)
    {
        if (coinCount >= count)
        {
            coinCount -= count;
            //更新金币在背包中的显示
            txt_coinCount.text = coinCount.ToString();
            return true;
        }
        return false;
    }

    private void ShowInventory()
    {
        gameObject.SetActive(true);
        isShow = true;
    }

    private void HideInventory()
    {
        gameObject.SetActive(false);
        isShow = false;
    }

    /// <summary>
    /// 当前背包面板的状态
    /// </summary>
    /// 如果显示，点击按钮后将被隐藏；如果原来是隐藏状态，点击按钮之后将被显示
    public void TransformState()
    {
        if (!isShow)
        {
            ShowInventory();
        }
        else
        {
            HideInventory();
        }
    }


}
