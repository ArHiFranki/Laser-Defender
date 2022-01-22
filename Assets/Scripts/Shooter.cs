using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float firingRate = 0.2f;

    public bool isFiring;

    private Coroutine firingCoroutine;

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject inctance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D myRigidbody = inctance.GetComponent<Rigidbody2D>();
            if (myRigidbody != null)
            {
                myRigidbody.velocity = transform.up * projectileSpeed;
            }

            Destroy(inctance, projectileLifetime);
            yield return new WaitForSeconds(firingRate);
        }
    }
}
