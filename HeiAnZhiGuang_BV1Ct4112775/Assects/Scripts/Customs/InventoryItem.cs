using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
IBeginDragHandler 用于处理开始拖动 UI 元素时的事件。
IDragHandler 用于处理 UI 元素被拖动时的事件。
IEndDragHandler 用于处理结束拖动 UI 元素时的事件。
在使用这些接口时，开发人员需要自己实现接口中的方法，并编写自己的逻辑来处理拖放事件。
————————————————
版权声明：本文为博主原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接和本声明。                     
原文链接：https://blog.csdn.net/weixin_46472622/article/details/134578343
*/


/// <summary>
/// 管理背包格子内的物品
/// </summary>
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    /**************这个sprite使用代码找不到，目前暂且使用Inspector面板指定的方式。****************/
    public Image sprite;

    /// <summary>
    /// 物品id
    /// </summary>
    private int itemId;
    /// <summary>
    /// 鼠标指针是否悬浮于物品上方
    /// </summary>
    private bool isHover = false;

    /// <summary>
    /// 用于接收鼠标指针悬停位置的背包面板的元素
    /// </summary>
    private Transform hoverInventoryUI;

    private void Awake()
    {
        /**************这个sprite使用代码找不到，目前暂且使用Inspector面板指定的方式。****************/
        //sprite = transform.Find("InventoryItemGrid").GetComponentInChildren<Image>();

    }
    private void Update()
    {
        ////GameObject tmpCanvas = GameObject.Find("Canvas").gameObject;
        ////hoverInventoryUI = GetOverUI(tmpCanvas).transform;
        ////print(1);
        //if (Input.GetMouseButtonDown(1))//&&EventSystem.current.IsPointerOverGameObject())
        //{
        //    bool success = EquipmentPanel._instance.Dress(itemId);
        //    print(success);
        //    if (success)
        //    {
        //        transform.parent.GetComponent<InventoryItemGrid>().MinusNumber();
        //        print("okok");
        //    }
        //}
        //if(isHover)
        //{
        //    InventoryDes._instance.ShowDes(itemId);
        //    if(Input.GetMouseButtonDown(1)) { 
            
        //    }
        //}

    }

    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);

        //更新显示
        sprite.name = info.iconName;
    }

    public void SetIconName(int id, string iconName)
    {
        sprite.sprite = Resources.Load<Sprite>("GUI/Icon/" + iconName);
        itemId = id;
    }

    #region 背包物品拖拽方法，有bug，需要修改。拖拽功能暂时搁置不做
    private Transform tmpDad;
    /// <summary>
    /// 开始拖拽UI元素时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //if(tmpUI.tag == Tags.inventoryItemGrid)
        //{
        //    eventData.selectedObject.transform.SetParent(tmpUI);
        //}
        #region 暂时的代码，用于将拖拽的物品显示在最上层，松开鼠标左键之后，回到原来父物体下方。

        eventData.selectedObject = transform.gameObject;
        tmpDad = transform.parent;
        eventData.selectedObject.transform.SetParent(transform.parent.parent);
        eventData.selectedObject.transform.SetAsLastSibling();


        #endregion
    }

    /// <summary>
    /// 拖拽元素时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    /// <summary>
    /// 结束拖拽元素时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        #region 暂时的代码，用于将拖拽的物品显示在最上层，松开鼠标左键之后，回到原来父物体下方。

        transform.SetParent(tmpDad);

        #endregion

        //if (hoverInventoryUI.tag == Tags.inventoryItemGrid)
        //{
        //    eventData.selectedObject.transform.SetParent(hoverInventoryUI);
        //    //如果是空格子，就更换被拖拽物体的父亲
        //    //如果不是空格子，就交换两个格子中的物品，并更新格子内的物品信息
        //    return;
        //}

        //if (hoverInventoryUI.tag == Tags.inventoryItemGrid)
        //{//当拖放到了一个空的格子里面
        //    if (hoverInventoryUI == this.transform.parent.gameObject)
        //    {//拖放到了自己的格子里面

        //    }
        //    else
        //    {
        //        InventoryItemGrid oldParent = this.transform.parent.GetComponent<InventoryItemGrid>();

        //        transform.parent = hoverInventoryUI.transform; 
        //        ResetLocalPosition();
        //        InventoryItemGrid newParent = hoverInventoryUI.GetComponent<InventoryItemGrid>();
        //        newParent.SetId(oldParent.itemId, oldParent.itemCount);

        //        oldParent.CleanInfo();
        //    }

        //}
        //else if (hoverInventoryUI.tag == Tags.inventoryItem)
        //{//当拖放到了一个有物品的格子里面
        //    InventoryItemGrid grid1 = this.transform.parent.GetComponent<InventoryItemGrid>();
        //    InventoryItemGrid grid2 = hoverInventoryUI.transform.parent.GetComponent<InventoryItemGrid>();
        //    int id = grid1.itemId;
        //    int num = grid1.itemCount;
        //    grid1.SetId(grid2.itemId, grid2.itemCount);
        //    grid2.SetId(id, num);
        //}
        GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;
        if (dropTarget != null)
        {
            if (dropTarget.CompareTag(S_Tags.inventoryItemGrid)) // 当拖放到了一个空的格子里面
            {
                print(dropTarget.name + dropTarget.transform.parent.name);
                if (dropTarget == this.transform.parent.gameObject) // 拖放到了自己的格子里面
                {
                    // Do nothing
                }
                else
                {
                    // 处理拖拽到空格子的逻辑
                    InventoryItemGrid oldSlot = this.transform.parent.GetComponent<InventoryItemGrid>();
                    this.transform.SetParent(dropTarget.transform);
                    ResetLocalPosition();

                    InventoryItemGrid newSlot = dropTarget.GetComponent<InventoryItemGrid>();
                    newSlot.SetId(oldSlot.itemId, oldSlot.itemCount);
                    oldSlot.CleanInfo();
                }
            }
            else if (dropTarget.CompareTag(S_Tags.inventoryItem)) // 当拖放到了一个有物品的格子里面
            {
                // 处理拖拽到有物品的格子里面的逻辑
                InventoryItemGrid slot1 = this.transform.parent.GetComponent<InventoryItemGrid>();
                InventoryItemGrid slot2 = dropTarget.transform.parent.GetComponent<InventoryItemGrid>();
                int id = slot1.itemId;
                int num = slot1.itemCount;
                slot1.SetId(slot2.itemId, slot2.itemCount);
                slot2.SetId(id, num);
            }
            //else if (dropTarget.CompareTag("Shortcut")) // 拖到的快捷方式里面
            //{
            //    // 处理拖拽到快捷方式里面的逻辑
            //    //dropTarget.GetComponent<ShortcutSlot>().SetItem(slot1.GetItem());
            //}
        }

        ResetLocalPosition();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //// 获取被拖拽的物体
        //GameObject draggedObject = eventData.pointerDrag;

        //// 获取释放时鼠标指针所在的目标物体（即拖拽物体最终停留的位置）
        //GameObject surface = eventData.pointerEnter;

        //// 在此处可以对拖拽释放时的逻辑进行处理
        //Debug.Log("Dropped on: " + surface.name);
        //draggedObject.transform.SetParent(surface.transform);
    }

    /// <summary>
    /// 获取鼠标指针悬停位置下的UI元素
    /// </summary>
    /// <param name="canvas">场景中的画布对象（根UI）</param>
    /// <returns>返回悬停位置下的UI元素对象</returns>
    public GameObject GetOverUI(GameObject canvas)
    {
        if (EventSystem.current == null)
        {
            return null;
        }
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        if (results.Count != 0)
        {
            print(results[0].gameObject.name);
            return results[0].gameObject;
        }

        return null;
    }
    #endregion

    /// <summary>
    /// 被拖拽物体返回原位置
    /// </summary>
    private void ResetLocalPosition()
    {
        transform.localPosition = Vector3.zero;
        transform.SetAsFirstSibling();
    }

    /// <summary>
    /// 鼠标指针进入时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryDes._instance.ShowDes(itemId);
        //isHover = true;
    }

    /// <summary>
    /// 鼠标指针退出时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryDes._instance.HideDes();
        //isHover = false;
    }

}






/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public Image sprite;

    private int itemId;
    private bool isHover = false;

    private Transform hoverInventoryUI;

    private void Awake()
    {
      
    }

    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        sprite.sprite = Resources.Load<Sprite>("GUI/Icon/" + info.iconName);
        itemId = id;
    }

    public void SetIconName(int id, string iconName)
    {
        sprite.sprite = Resources.Load<Sprite>("GUI/Icon/" + iconName);
        itemId = id;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (hoverInventoryUI != null && hoverInventoryUI.tag == "InventoryItemGrid")
        {
            transform.SetParent(hoverInventoryUI);
            ResetLocalPosition();
        }
        else
        {
            transform.SetParent(transform.parent);
            ResetLocalPosition();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        GameObject surface = eventData.pointerEnter;

        if (surface != null && surface.tag == "InventoryItemGrid")
        {
            draggedObject.transform.SetParent(surface.transform);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;
        InventoryDes._instance.ShowDes(itemId);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
        InventoryDes._instance.HideDes();
    }

    private void Update()
    {
        if (isHover && Input.GetMouseButtonDown(1))
        {
            bool success = EquipmentPanel._instance.Dress(itemId);
            if (success)
            {
                transform.parent.GetComponent<InventoryItemGrid>().MinusNumber();
            }
        }

        GameObject tmpCanvas = GameObject.Find("Canvas").gameObject;
        hoverInventoryUI = GetOverUI(tmpCanvas).transform;
    }

    private void ResetLocalPosition()
    {
        transform.localPosition = Vector3.zero;
    }

    public GameObject GetOverUI(GameObject canvas)
    {
        if (EventSystem.current == null)
        {
            return null;
        }

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);

        if (results.Count != 0)
        {
            return results[0].gameObject;
        }

        return null;
    }
}
*/