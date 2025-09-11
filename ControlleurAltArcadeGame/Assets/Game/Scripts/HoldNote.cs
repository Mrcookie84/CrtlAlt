using UnityEngine;

public class HoldNote : Note
{
    public float duration = 1f;
    private float holdTimer = 0f;
    private bool holding = false;
    private bool failed = false;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (holding)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= duration)
                Hit();
        }

        if (transform.position.y < -6f && !failed)
            Miss();
    }

    public void StartHolding()
    {
        if (canBePressed && !failed)
        {
            holding = true;
            holdTimer = 0f;
        }
    }

    public void StopHolding()
    {
        if (holding && holdTimer < duration)
            Miss();
    }

    protected override void Hit()
    {
        if (failed) return;
        FindObjectOfType<GameManager>().NoteHit();
        Destroy(gameObject);
    }

    protected override void Miss()
    {
        if (failed) return;
        failed = true;
        FindObjectOfType<GameManager>().NoteMiss();
        Destroy(gameObject);
    }
}