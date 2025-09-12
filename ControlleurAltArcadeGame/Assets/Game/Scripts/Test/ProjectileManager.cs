using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float projectileSpeed = 5f;
    public float cooldownTime = 15f;

    [Header("UI")]
    public Button shootButton;
    public Image cooldownImage;

    private bool canShoot = true;

    private void Awake()
    {
        Instance = this;
        shootButton.onClick.AddListener(ShootProjectile);
        cooldownImage.fillAmount = 0f;
    }

    private void ShootProjectile()
    {
        if (!canShoot) return;

        GameObject proj = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * projectileSpeed;

        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        canShoot = false;
        float timer = cooldownTime;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            cooldownImage.fillAmount = 1 - (timer / cooldownTime);
            yield return null;
        }

        cooldownImage.fillAmount = 0f;
        canShoot = true;
    }
}