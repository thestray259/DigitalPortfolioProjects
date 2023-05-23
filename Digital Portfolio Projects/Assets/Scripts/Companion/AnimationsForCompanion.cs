using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsForCompanion : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Punch = Animator.StringToHash("Punch");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int Dance = Animator.StringToHash("Dance");
    private static readonly int DrawBow = Animator.StringToHash("Draw Bow");
    private static readonly int ReleaseBow = Animator.StringToHash("Release Bow");

    void Start()
    {
        
    }

    void Update()
    {
        if (CompanionBT.isIdle) animator.CrossFade(Idle, 0);
        if (CompanionBT.isWalking) animator.CrossFade(Walk, 0);
        if (CompanionBT.isRunning) animator.CrossFade(Run, 0);
        if (CompanionBT.isPunching) animator.CrossFade(Punch, 0);
        if (CompanionBT.isDead) animator.CrossFade(Dead, 0);
    }
}
