using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int fallMeters = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.Fall(fallMeters);
            Destroy(gameObject);
        }
    }
}