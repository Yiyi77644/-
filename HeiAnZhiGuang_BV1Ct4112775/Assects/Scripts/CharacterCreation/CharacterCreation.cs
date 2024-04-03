using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    private GameObject[] characterGameObects;
    private int selectedIndex = 0;

    /// <summary>
    /// 所有可供选择的角色的个数
    /// </summary>
    private int length;

    private Button btn_next;
    private Button btn_prev;
    private AudioClip clickSound;
    private InputField inp_nameInput;

    void Start()
    {
        btn_next = GameObject.Find(S_GameObjectName.btn_next).GetComponent<Button>();
        btn_prev = GameObject.Find(S_GameObjectName.btn_prev).GetComponent<Button>();
        inp_nameInput = GameObject.Find(S_GameObjectName.inp_enterName).GetComponent<InputField>();
        length = characterPrefabs.Length;
        characterGameObects = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            characterGameObects[i] = Instantiate(characterPrefabs[i], transform.position, transform.rotation);

        }
        UpdateCharacterShow();

        clickSound = Resources.Load<AudioClip>("Sounds/Button/button");
        btn_next.onClick.AddListener(OnNextButtonClick);
        btn_prev.onClick.AddListener(OnPrevButtonClick);
    }

    void Update()
    {

    }

    private void UpdateCharacterShow()
    {
        characterGameObects[selectedIndex].SetActive(true);

        for (int i = 0; i < length; i++)
        {
            if (i != selectedIndex)
            {
                characterGameObects[i].SetActive(false);
            }

        }
    }

    public void OnNextButtonClick()
    {
        GetComponent<AudioSource>().PlayOneShot(clickSound);
        selectedIndex++;
        selectedIndex %= length;
        UpdateCharacterShow();
    }

    public void OnPrevButtonClick()
    {
        GetComponent<AudioSource>().PlayOneShot(clickSound);
        selectedIndex--;
        if (selectedIndex == -1)
        {
            selectedIndex = length - 1;
        }
        UpdateCharacterShow();
    }

    public void OnOkButtonClick()
    {        
        //存储选择的角色
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedIndex);
        //存储角色名
        PlayerPrefs.SetString("name", inp_nameInput.text);
        
        //加载下一个场景

    }
}
