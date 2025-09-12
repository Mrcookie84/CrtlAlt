using UnityEngine;

public class HoldNote : Note
{
    public float duration = 1f;
    private float holdTimer = 0f;
    private bool holding = false;
    private bool failed = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
        if (laneSprites != null && laneSprites.Length > lane && lane >= 0)
        {
            sr.sprite = laneSprites[lane];
        }
        else
        {
            Debug.LogWarning($"Lane {lane} n'a pas de sprite assigné !");
        }
    }
    
    
    
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (holding)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= duration)
                Hit(scoreGived);
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
            speed = 0f;
        }
    }

    public void StopHolding()
    {
        if (holding && holdTimer < duration)
            Miss();
    }

    protected override void Hit(int scoreUp)
    {
        if (failed) return;
        FindObjectOfType<GameManager>().NoteHit(scoreUp);
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