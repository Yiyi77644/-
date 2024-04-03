using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatussPanel : MonoBehaviour
{
    public static StatussPanel _instance;

    /// <summary>
    /// 当前状态面板的显示情况。如果正在显示中，则为true。
    /// </summary>
    private bool isShowStatus = false;

    private Text txt_attack;
    private Text txt_defend;
    private Text txt_speed;
    private Text txt_remain;
    private Text txt_summary;
    private Button btn_atkPlus;
    private Button btn_defPlus;
    private Button btn_vPlus;

    private PlayerStatus playerStatus;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        txt_attack = transform.Find(S_GameObjectName.SP_txt_attack).GetComponent<Text>();
        txt_defend = transform.Find(S_GameObjectName.SP_txt_defend).GetComponent<Text>();
        txt_speed = transform.Find(S_GameObjectName.SP_txt_speed).GetComponent<Text>();
        txt_remain = transform.Find(S_GameObjectName.SP_txt_remain).GetComponent<Text>();
        txt_summary = transform.Find(S_GameObjectName.SP_txt_summary).GetComponent<Text>();
        btn_atkPlus = transform.Find(S_GameObjectName.SP_btn_atkPlus).GetComponent<Button>();
        btn_defPlus = transform.Find(S_GameObjectName.SP_btn_defPlus).GetComponent<Button>();
        btn_vPlus = transform.Find(S_GameObjectName.SP_btn_vPlus).GetComponent<Button>();

        playerStatus = GameObject.FindGameObjectWithTag(S_Tags.player).GetComponent<PlayerStatus>();

        btn_atkPlus.onClick.AddListener(OnAttackPlusClick);
        btn_defPlus.onClick.AddListener(OnDefendPlusClick);
        btn_vPlus.onClick.AddListener(OnSpeedPlusClick);

        gameObject.SetActive(false);
    }

    /// <summary>
    /// 更新显示，根据playerStatus的属性值去更新显示
    /// </summary>
    private void UpdateShow()
    {
        txt_attack.text = playerStatus.attack + " + " + playerStatus.attackPlus;
        txt_defend.text = playerStatus.def + " + " + playerStatus.defPlus;
        txt_speed.text = playerStatus.speed + " + " + playerStatus.speedPlus;
        txt_remain.text = playerStatus.pointRemain + "";
        txt_summary.text = "伤害：" + (playerStatus.attack + playerStatus.attackPlus)
            + "  攻击：" + (playerStatus.def + playerStatus.defPlus)
            + "  速度：" + (playerStatus.speed + playerStatus.speedPlus);

        if(playerStatus.pointRemain > 0)
        {
            btn_atkPlus.gameObject.SetActive(true);
            btn_defPlus.gameObject.SetActive(true);
            btn_vPlus.gameObject.SetActive(true);
        }
        else
        {
            btn_atkPlus.gameObject.SetActive(false);
            btn_defPlus.gameObject.SetActive(false);
            btn_vPlus.gameObject.SetActive(false);
        }
    }



    public void OnAttackPlusClick()
    {
        bool success = playerStatus.GetPoint();
        if(success)
        {
            playerStatus.attackPlus++;
            UpdateShow();
        }
    }

    public void OnDefendPlusClick()
    {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.defPlus++;
            UpdateShow();
        }
    }

    public void OnSpeedPlusClick()
    {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.speedPlus++;
            UpdateShow();
        }
    }

    /// <summary>
    /// StatusPanel的显示和隐藏
    /// </summary>
    public void TransformState()
    {
        if (!isShowStatus)
        {
            gameObject.SetActive(true);
            UpdateShow();
            isShowStatus = true;
        }
        else
        {
            gameObject.SetActive(false);
            isShowStatus = false ;
        }
    }

}
