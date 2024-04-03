using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDir : MonoBehaviour
{
    public GameObject effectClickPrefab;

    /// <summary>
    /// 目标位置
    /// </summary>
    public Vector3 targetPosition = Vector3.zero;

    private bool isMoving = false;


    private PlayerMove playerMove;

    private void Start()
    {
        targetPosition = transform.position;
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& !EventSystem.current .IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == S_Tags.ground)
            {
                isMoving = true;
                //实例化出来点击的方法
                ShowClickEffect(hitInfo.point);
                LookAtTarget(hitInfo.point);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            //得到要移动的目标位置，让主角面向目标位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == S_Tags.ground)
            {
                LookAtTarget(hitInfo.point);
            }

        }
        else
        {
            if (playerMove.isMoving)
            {
                LookAtTarget(targetPosition);
            }
        }

    }

    private void ShowClickEffect(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x, hitPoint.y + 0.1f, hitPoint.z);
        GameObject.Instantiate(effectClickPrefab, hitPoint, Quaternion.identity);
    }

    /// <summary>
    /// 让主角朝向目标位置
    /// </summary>
    /// <param name="hitPoint"></param>
    private void LookAtTarget(Vector3 hitPoint)
    {
        targetPosition = hitPoint;
        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        transform.LookAt(targetPosition);
    }
}
