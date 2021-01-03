using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 10f;
    [SerializeField] public ParticleSystem projectileParticle;    

    
    [SerializeField] Transform targetEnemy;
    [SerializeField] float projSpeed;

    private void Start()
    {
        targetEnemy = GameObject.FindWithTag("Player").transform;
    }


    void Update()
    {
        Vector3 targetSpeed = targetEnemy.GetComponent<Rigidbody>().velocity;        
        float distance = Vector3.Distance(projectileParticle.transform.position, targetEnemy.position + targetSpeed);
        objectToPan.LookAt(targetEnemy.position + targetSpeed * (distance / projSpeed));
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {        
        ParticleSystem.EmissionModule emissionModule = projectileParticle.emission;         
        emissionModule.enabled = isActive;
    }
}
