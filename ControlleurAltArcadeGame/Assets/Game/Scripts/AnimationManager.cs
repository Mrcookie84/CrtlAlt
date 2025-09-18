using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;
    public Animator playerAnimator;

    public enum AnimEnum
    {
        Climbing,
        Falling,
        Dead
    }

    private AnimEnum currentAnim;

    void Start()
    {
        SetAnimation(AnimEnum.Climbing);
    }

    public void SetAnimation(AnimEnum newAnim)
    {
        if (newAnim == currentAnim) return;

        currentAnim = newAnim;
        
        playerAnimator.ResetTrigger("Climb");
        playerAnimator.ResetTrigger("Fall");
        playerAnimator.ResetTrigger("Die");

        switch (newAnim)
        {
            case AnimEnum.Climbing:
                playerAnimator.SetBool("isClimbing", true);
                playerAnimator.SetBool("isFalling", false);
                break;

            case AnimEnum.Falling:
                playerAnimator.SetBool("isClimbing", false);
                playerAnimator.SetBool("isFalling", true);
                break;

            case AnimEnum.Dead:
                playerAnimator.SetTrigger("Die");
                break;
        }
    }

    
}