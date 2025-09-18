using System.Collections;
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
        Die
    }

    private AnimEnum currentAnim;
    private bool isDead = false;      // bloque les autres anims après la mort
    private bool deathPlayed = false; // bloque le relancement de l'anim de mort

    void Start()
    {
        SetAnimation(AnimEnum.Climbing);
    }

    public void SetAnimation(AnimEnum newAnim)
    {
        // si le joueur est mort, on bloque tout sauf la première fois qu'on joue Die
        if (isDead && newAnim != AnimEnum.Die) return;

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

            case AnimEnum.Die:
                if (deathPlayed) return; // <-- empêche de relancer l'anim de mort
                deathPlayed = true;

                isDead = true;
                playerAnimator.SetBool("isClimbing", false);
                playerAnimator.SetBool("isFalling", false);
                playerAnimator.SetTrigger("Die");

                StartCoroutine(WaitForAnimation("Die"));
                break;
        }
    }

    private IEnumerator WaitForAnimation(string animName)
    {
        // attendre un frame pour que l'anim démarre
        yield return null;

        // attendre que l'anim soit finie
        while (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animName) &&
               playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        Debug.Log($"Animation {animName} terminée !");

        yield return new WaitForSeconds(2f);
        gameManager.Death();
    }
}
