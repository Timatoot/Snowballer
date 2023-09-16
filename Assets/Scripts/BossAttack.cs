using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float explosionForce = 1000f;
    public int damage = 4;
    public float shakeMagnitude = 0.1f;
    public float shakeDuration = 0.2f;
    public GameObject explosionRadiusObject;
    private GameObject player;
    private CircleCollider2D explosionRadius;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        SpriteRenderer renderer = explosionRadiusObject.GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        color.a = 0f;
        renderer.color = color;

        explosionRadius = explosionRadiusObject.GetComponent<CircleCollider2D>();
        StartCoroutine(ExplodeAfterDelay(3.0f));
    }

    IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        float radius = explosionRadius.radius;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            Physics2D.IgnoreCollision(hitCollider, explosionRadius, true);

            if (hitCollider.gameObject == player)
            {
                Vector2 target = player.transform.position;
                Vector2 bomb = transform.position;

                Vector2 direction = target - bomb;
                player.GetComponent<Rigidbody2D>().AddForce(direction.normalized * explosionForce);

                PlayerBehaviour playerBehaviour = player.GetComponent<PlayerBehaviour>();
                if (playerBehaviour != null)
                {
                    playerBehaviour.TakeHit(damage);
                }

                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                if (cameraShake != null)
                {
                    cameraShake.Shake(shakeMagnitude, shakeDuration);
                }
            }
        }
        Destroy(gameObject);
    }
}
