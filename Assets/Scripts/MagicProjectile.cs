﻿using UnityEngine;
 
public class MagicProjectile : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject impactParticle;
    [SerializeField] private GameObject projectileParticle;
    [SerializeField] private GameObject muzzleParticle;
    [SerializeField] private int damage = 50;
    [SerializeField] private float magicProjectileLifetime = 2.2f;
    [SerializeField] private float impactParticleLifetime = 2f;
    [SerializeField] private float muzzleParticleLifetime = 1.5f;

    private bool hasCollided = false;

    #endregion

    #region Methods

    private void Start()
    {
        InstantiateProjectileParticle();
        Destroy(gameObject, magicProjectileLifetime);   // self-destruction
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.identity);
            TryApplyDamage(collision);

            Destroy(impactParticle, impactParticleLifetime);
            Destroy(gameObject);			
        }
    }

    private void TryApplyDamage(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable idamageable))
        {
            idamageable.TakeDamage(damage);
        }
    }

    private void InstantiateProjectileParticle()
    {
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation);
        projectileParticle.transform.parent = transform;
        if (muzzleParticle != null)
        {
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation);
            Destroy(muzzleParticle, muzzleParticleLifetime);
        }
    }

    #endregion
}