using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus.Input;
using UnityEngine;

public class DepthLeverAnimation : MonoBehaviour
{
    [SerializeField] private Animator leverAnimator;

    public void forwardLever()
    {
        leverAnimator.SetBool("Forward", true);
    }
    public void backwardLever()
    {
        leverAnimator.SetBool("Backward", true);
    }
    public void resetLever()
    {
        leverAnimator.SetBool("Forward", false);
        leverAnimator.SetBool("Backward", false);
    }
}
