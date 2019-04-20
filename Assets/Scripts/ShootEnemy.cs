using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : Monster
{
    private int tmax = 3;
    private int tmin = 1;
    private Bullet bullet;
    private SpriteRenderer sprite;

    void Awake()
    {
        bullet = Resources.Load<Bullet>("Saw");
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        InvokeRepeating("Shoot", tmin, tmax);
    }

    void Shoot()
    {
        Vector3 position = transform.position;
        Bullet newSaw = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newSaw.Direction = -newSaw.transform.right;

    }
}