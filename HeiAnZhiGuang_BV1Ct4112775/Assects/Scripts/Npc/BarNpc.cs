using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarNpc : Npc
{
    public GameObject questPanel;
    public Button btn_close;
    public Button btn_ok;
    public Button btn_accept;
    public Button btn_cancel;

    public Text txt_des;
    /// <summary>
    /// 是否任务进行中
    /// </summary>
    public bool isInTask = false;
    /// <summary>
    /// 表示任务进度，已经杀死了几只小野狼
    /// </summary>
    public int killCount = 0;

    private PlayerStatus status;

    private void Start()
    {
        btn_close.onClick.AddListener(OnCloseButtonClick);
        btn_accept.onClick.AddListener(OnAcceptButtonClick);
        btn_cancel.onClick.AddListener(OnCancelButtonClick);
        btn_ok.onClick.AddListener(OnOkButtonClick);

        status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();


    }

    /// <summary>
    /// 当鼠标位于Collider之上时，会在每一帧调用这个方法
    /// </summary>
    private void OnMouseOver()
    {
        //当点击了老爷爷
        if (Input.GetMouseButtonDown(0))
        {
            if(isInTask)
            {
                ShowTaskProgress();
            }
            else
            {
                ShowTaskDes();
            }
            ShowQuest();
        }
    }

    private void ShowQuest()
    {
        questPanel.SetActive(true);
    }

    /// <summary>
    /// 显示任务描述
    /// </summary>
    private void ShowTaskDes()
    {
        txt_des.text = "任务：\n杀死10只小野狼\n奖励：\n1000金币";
        btn_accept.gameObject.SetActive(true );
        btn_cancel.gameObject.SetActive(true );
        btn_ok.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示任务进度
    /// </summary>
    private void ShowTaskProgress()
    {
        txt_des.text = "任务：\n你已经杀死了" + killCount + "/10只小野狼\n奖励：\n1000金币";
        btn_accept.gameObject.SetActive(false);
        btn_cancel.gameObject.SetActive(false);
        btn_ok.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏任务面板
    /// </summary>
    private void HideQuest()
    {

        questPanel.SetActive(false);
    }

    public void OnCloseButtonClick()
    {
        HideQuest();
    }

    public void OnAcceptButtonClick()
    {
        ShowTaskProgress();
        //表示正在任务中
        isInTask = true;
    }

    public void OnOkButtonClick()
    {
        //完成任务
        if(killCount >= 10)
        {
            status.GetCoin(1000);
            killCount = 0;
            ShowTaskDes();
        }
        //未完成任务
        else
        {
            HideQuest();
        }
    }

    public void OnCancelButtonClick()
    {
        HideQuest();
    }


}
