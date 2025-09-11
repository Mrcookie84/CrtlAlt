using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 5f;
    public int lane;  // 0 = Right, 1 = Up, 2 = Left, 3 = Down
    [HideInInspector] public bool canBePressed = false;
    
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
            Hit();
    }

    protected virtual void Hit()
    {
        FindObjectOfType<GameManager>().NoteHit();
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