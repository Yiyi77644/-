using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMove move;
    private Animation anim;

    private void Start()
    {
        move = GetComponent<PlayerMove>();
        anim=GetComponent<Animation>();
    }

    private void LateUpdate()
    {
        if (move.state == PlayerState.Moving)
        {
            PlayAnim("Run");
        }
        else if (move.state == PlayerState.Idle)
        {
            PlayAnim("Idle");
        }
    }

    private void PlayAnim(string animName)
    {
        anim.CrossFade(animName);
    }
}
