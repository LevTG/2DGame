using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10.0f;
    public Vector3 Direction;
    private SpriteRenderer sprite;
    private GameObject respawn;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        respawn = GameObject.FindGameObjectWithTag("Respawn");
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
