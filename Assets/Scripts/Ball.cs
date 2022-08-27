using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;
    public float maxDamage = 100f;
    public float explosionForce = 2000f;
    public float explosionRadius =20f;  //20meter
    public float lifeTime = 10f;        //10seconds

    public LayerMask WhatIsProb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, WhatIsProb);
        Debug.Log("Attack targets : " + colliders.Length);

        foreach(var collider in colliders)
        {
            Rigidbody targetRigidBody = collider.GetComponent<Rigidbody>();
            targetRigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            Prob targetProb = collider.GetComponent<Prob>();
            targetProb.TakeDamage(CalculateDamage(collider.transform.position));
        }

        explosionParticle.transform.parent = null;
        explosionParticle.Play();
        explosionAudio.Play();

        GameManager.instance.OnBallDestroy();

        Destroy(explosionParticle.gameObject, explosionParticle.main.duration);
        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(targetPosition,transform.position);
        distance = distance<0 ? 0 : distance;
        return ((explosionRadius - distance) / explosionRadius) * maxDamage;
    }

    private void OnDestroy()
    { 
        GameManager.instance.OnBallDestroy();
        Debug.Log("Destroy Ball");
        
    }
}
