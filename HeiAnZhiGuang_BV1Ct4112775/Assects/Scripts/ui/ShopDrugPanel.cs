using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDrugPanel : MonoBehaviour
{
    public static ShopDrugPanel _instance;


    private Button btn_close;
    private Button btn_buyDrug1001;
    private Button btn_buyDrug1002;
    private Button btn_buyDrug1003;
    private Button btn_ok;

    private InputField inp_buyCount;

    /// <summary>
    /// 药品商店UI是否显示。
    /// </summary>
    /// 如果当前是显示状态，则为true，否则为false
    private bool isShowShopDrug = false;
    /// <summary>
    /// 购买的药品的id
    /// </summary>
    private int buyDrugId = 0;




    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        btn_close = transform.Find(S_GameObjectName.SDP_btn_close).GetComponent<Button>();
        btn_buyDrug1001 = transform.Find("DrugItem1001/btn_buyDrug1001").GetComponent<Button>();
        btn_buyDrug1002 = transform.Find("DrugItem1002/btn_buyDrug1002").GetComponent<Button>();
        btn_buyDrug1003 = transform.Find("DrugItem1003/btn_buyDrug1003").GetComponent<Button>();
        btn_ok = transform.Find("inp_buyCount/btn_ok").GetComponent<Button>();
        inp_buyCount = transform.Find(S_GameObjectName.SDP_inp_buyCount).GetComponent<InputField>();

        btn_close.onClick.AddListener(OnCloseButtonClick);
        btn_buyDrug1001.onClick.AddListener(OnBuyId1001);
        btn_buyDrug1002.onClick.AddListener(OnBuyId1002);
        btn_buyDrug1003.onClick.AddListener(OnBuyId1003);
        btn_ok.onClick.AddListener(OnOkButtonClick);

        inp_buyCount.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }


    public void OnBuyId1001()
    {
        BuyDrug(1001);
    }
    public void OnBuyId1002()
    {
        BuyDrug(1002);
    }
    public void OnBuyId1003()
    {
        BuyDrug(1003);
    }
    public void OnOkButtonClick()
    {
        int count = int.Parse(inp_buyCount.text);
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(buyDrugId);
        int price = info.priceBuy;
        int priceTotal = price * count;
        //如果取款成功
        if (InventoryPanel._instance.GetCoinCount(priceTotal))
        {
            if (count > 0)
            {
                InventoryPanel._instance
                    .GetId(buyDrugId, count);
            }

        }
        else
        {
            //细节，待补充
        }
        inp_buyCount.gameObject.SetActive(false);

    }
    public void OnCloseButtonClick()
    {
        TransformState();
    }

    private void BuyDrug(int id)
    {
        ShowInpNumberDialog();
        buyDrugId = id;

    }

    private void ShowInpNumberDialog()
    {
        inp_buyCount.gameObject.SetActive(true);
        inp_buyCount.text = "0";
    }


    public void TransformState()
    {
        if (!isShowShopDrug)
        {
            gameObject.SetActive(true);
            isShowShopDrug = true;
        }
        else
        {
            gameObject.SetActive(false);
            isShowShopDrug = false;
        }
    }
}
