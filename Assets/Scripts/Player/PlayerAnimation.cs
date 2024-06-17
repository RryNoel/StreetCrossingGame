using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private static readonly int IsWalk = Animator.StringToHash("Walk");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalkAnim()
    {
        animator.SetBool(IsWalk, true);
    }

    public void InvokeOutAnim()
    {
        Invoke("OutWalkAnim", 0.3f);
    }

    private void OutWalkAnim()
    {
        animator.SetBool(IsWalk, false);
    }

    public void SetLoseAnim()
    {
        animator.SetTrigger("Lose");
    }

}
