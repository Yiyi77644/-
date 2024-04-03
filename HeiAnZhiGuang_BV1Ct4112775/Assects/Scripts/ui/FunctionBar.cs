using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionBar : MonoBehaviour
{
    public Button btn_status;
    public Button btn_bag;
    public Button btn_equip;
    public Button btn_skill;
    public Button btn_setting;

    private void Start()
    {
        btn_status.onClick.AddListener(OnStatusButtonClick);
        btn_bag.onClick.AddListener(OnBagButtonClick);
        btn_equip.onClick.AddListener(OnEquipButtonClick);
        btn_skill.onClick.AddListener(OnSkillButtonClick);
        btn_setting.onClick.AddListener(OnSettingButtonClick);
    }

    private void Update()
    {
        //物品拾取模拟
        if (Input.GetKeyDown(KeyCode.X))
        {
            InventoryPanel._instance.GetId(Random.Range(2001, 2023));
        }
    }

    public void OnStatusButtonClick()
    {
        StatussPanel._instance.TransformState();
    }

    public void OnBagButtonClick()
    {
        InventoryPanel._instance.TransformState();
    }

    public void OnEquipButtonClick()
    {
        EquipmentPanel._instance.TransformState();
    }

    public void OnSkillButtonClick()
    {

    }

    public void OnSettingButtonClick()
    {

    }



}
