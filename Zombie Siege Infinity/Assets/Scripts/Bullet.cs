using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public int bulletDamage;

    // Called when the bullet collides with another object
    private void OnCollisionEnter(Collision ObjectWeHit)
    {
        // If the object we hit has the "Target" tag
        if(ObjectWeHit.gameObject.CompareTag("Target"))
        {
            print("Hit " + ObjectWeHit.gameObject.name + "!");

            // Create bullet impact effect
            CreateBulletImpactEffect(ObjectWeHit);

            // Destroy the bullet object
            Destroy(gameObject);
        }

        // If the object we hit has the "Wall" tag
        if(ObjectWeHit.gameObject.CompareTag("Wall"))
        {
            print("Hit a wall!");

            // Create bullet impact effect
            CreateBulletImpactEffect(ObjectWeHit);

            // Destroy the bullet object
            Destroy(gameObject); 
        }

        // If the object we hit has the "Enemy" tag
        if(ObjectWeHit.gameObject.CompareTag("Enemy"))
        {
            // Call the TakeDamage method of the Enemy component attached to the object we hit
            ObjectWeHit.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);

            // Create bullet spray effect
            CreateBulletSprayEffect(ObjectWeHit);

            // Destroy the bullet object
            Destroy(gameObject);
        }
    }

    // Creates a bullet spray effect at the collision point
    private void CreateBulletSprayEffect(Collision ObjectWeHit)
    {
        ContactPoint contact = ObjectWeHit.contacts[0];
        GameObject blood = Instantiate(
            GlobalReferences.Instance.bloodSprayEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        blood.transform.SetParent(ObjectWeHit.gameObject.transform);
    }

    // Creates a bullet impact effect at the collision point
    private void CreateBulletImpactEffect(Collision ObjectWeHit)
    {   
        ContactPoint contact = ObjectWeHit.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        hole.transform.SetParent(ObjectWeHit.gameObject.transform);
    }
}
