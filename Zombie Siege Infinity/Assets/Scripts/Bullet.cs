using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public int bulletDamage;
    private void OnCollisionEnter(Collision ObjectWeHit)
    {
        if(ObjectWeHit.gameObject.CompareTag("Target"))
        {
            print("hit" + ObjectWeHit.gameObject.name + "!");

            CreateBulletImpactEffect(ObjectWeHit);

            Destroy(gameObject);
        }

        if(ObjectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall!");

             CreateBulletImpactEffect(ObjectWeHit);

            Destroy(gameObject); 
        }

        if(ObjectWeHit.gameObject.CompareTag("Enemy"))
        {
            ObjectWeHit.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);

            CreateBulletSprayEffect(ObjectWeHit);

            Destroy(gameObject);
        }
    }

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

    void CreateBulletImpactEffect(Collision ObjectWeHit)
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
