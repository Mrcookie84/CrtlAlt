using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviour
{
    public float speed = 5f;
    public int lane;  // 0 = Right, 1 = Up, 2 = Left, 3 = Down
    public int scoreGived;
    [HideInInspector] public bool canBePressed = false;
    [Header("Sprites par Lane")]
    public Sprite[] laneSprites; // 0=Right, 1=Up, 2=Left, 3=Down

    public SpriteRenderer sr;

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

        if (transform.position.y < -6f)
        {
            Miss();
        }
    }

    public void Pressed()
    {
        if (canBePressed)
            Hit(scoreGived);
    }

    protected virtual void Hit(int scoreUp)
    {
        FindObjectOfType<GameManager>().NoteHit(scoreUp);
        Destroy(gameObject);
    }

    protected virtual void Miss()
    {
        FindObjectOfType<GameManager>().NoteMiss();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
            canBePressed = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
            canBePressed = false;
    }
}