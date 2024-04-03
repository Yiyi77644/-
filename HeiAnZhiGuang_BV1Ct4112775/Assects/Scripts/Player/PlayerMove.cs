using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Moving,
    Idle,

}

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// 表示鼠标是否按下。如果按下，则角色会朝向点击的位置移动。
    /// </summary>
    public bool isMoving = false;
    public float speed = 4;
    private PlayerDir dir;
    private CharacterController controller;

    public PlayerState state=PlayerState.Idle ;


    // Start is called before the first frame update
    void Start()
    {
        dir = GetComponent<PlayerDir>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(dir.targetPosition, transform.position);
        if(distance  > 0.1f)
        {
            isMoving = true;
            state = PlayerState.Moving;
            controller.SimpleMove(transform.forward * speed);
        }
        else
        {
            isMoving = false;
            state = PlayerState.Idle;
        }
    }
}
