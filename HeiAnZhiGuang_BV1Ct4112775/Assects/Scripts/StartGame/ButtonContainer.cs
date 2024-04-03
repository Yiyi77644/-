using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContainer : MonoBehaviour
{
    //1、游戏数据的保存和场景之间数据的传递使用，用PlayerPrefs
    //2、场景的分类：1）开始场景；2）角色选择界面；3）游戏场景 村庄、野兽等

    private Button btn_NewGame;
    private Button btn_LoadGame;
    private GameObject AudioContainer;
    private AudioClip clickSound;

    private void Start()
    {
        btn_LoadGame = transform.Find(S_GameObjectName.btn_loadGame).GetComponent<Button>();
        btn_NewGame = transform.Find(S_GameObjectName.btn_newGame).GetComponent<Button>();
        btn_LoadGame.onClick.AddListener(OnLoadGame);
        btn_NewGame.onClick.AddListener(OnNewGame);
        AudioContainer = GameObject.Find("AudioContainer").gameObject;

        clickSound = Resources.Load<AudioClip>("Sounds/Button/button");
    }

    /// <summary>
    /// 开始新游戏
    /// </summary>
    public void OnNewGame()
    {
        AudioContainer.GetComponent<AudioSource>().PlayOneShot(clickSound);
        PlayerPrefs.SetInt("DataFromSave", 0);

        //加载选择角色场景

    }

    /// <summary>
    /// 加载已经保存的数据
    /// </summary>
    public void OnLoadGame()
    {
        //取得游戏数据：1）从2场景获取；2）从已保存的数据获取。


        AudioContainer.GetComponent<AudioSource>().PlayOneShot(clickSound);
        //DataFromSave表示从已保存数据获取数据。
        PlayerPrefs.SetInt("DataFromSave", 1);

        //加载游戏场景
    }
}
