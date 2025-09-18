using UnityEngine;

public class HoldNote : Note
{
    public float duration = 1f;
    private float holdTimer = 0f;
    private bool holding = false;
    private bool failed = false;
    
    public Sprite[] laneBodySprites;
    public Sprite[] laneTailSprites;
    
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer tailRenderer;
    

    void Start()
    {
        if (laneSprites != null && laneSprites.Length > lane && lane >= 0)
        {
            sr.sprite = laneSprites[lane];
        }
        else
        {
            Debug.LogWarning($"Lane {lane} n'a pas de sprite assigné !");
        }
        
        if (laneBodySprites != null && laneBodySprites.Length > lane && lane >= 0)
        {
            bodyRenderer.sprite = laneBodySprites[lane];
        }
        else
        {
            Debug.LogWarning($"Lane {lane} n'a pas de body sprite assigné !");
        }
        
        if (laneTailSprites != null && laneTailSprites.Length > lane && lane >= 0)
        {
            tailRenderer.sprite = laneTailSprites[lane];
        }
        else
        {
            Debug.LogWarning($"Lane {lane} n'a pas de tail sprite assigné !");
        }
    }
    
    void Update()
    {
        // Si on n'est PAS en train de hold, la note descend normalement
        if (!holding)
            transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (holding)
        {
            holdTimer += Time.deltaTime;

            // --- AJOUT : on réduit la taille du corps ---
            Vector2 size = bodyRenderer.size;
            size.y = Mathf.Max(0, size.y - (1f / duration) * Time.deltaTime * bodyRenderer.sprite.bounds.size.y);
            bodyRenderer.size = size;

            // Tu peux aussi faire descendre le tailRenderer pour "suivre" la réduction
            Vector3 tailPos = tailRenderer.transform.localPosition;
            tailPos.y -= (1f / duration) * Time.deltaTime * bodyRenderer.sprite.bounds.size.y;
            tailRenderer.transform.localPosition = tailPos;
            // --------------------------------------------

            if (holdTimer >= duration)
                Hit(scoreGived);
        }

        if (transform.position.y < -7f && !failed)
            Miss();
    }

    public void StartHolding()
    {
        if (canBePressed && !failed)
        {
            holding = true;
            holdTimer = 0f;
            speed = 0;
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