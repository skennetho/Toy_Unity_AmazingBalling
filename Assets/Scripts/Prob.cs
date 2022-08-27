using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prob : MonoBehaviour
{
    public int score = 5;
    public ParticleSystem explosionParticle;

    public float hp = 10;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if( hp < 0)
        {
            ParticleSystem instance = Instantiate(explosionParticle, transform.position, transform.rotation);
            AudioSource explosionAudio = instance.GetComponent<AudioSource>();
            instance.Play();
            explosionAudio.Play();
            GameManager.instance.AddScore(score);

            Destroy(instance.gameObject, instance.main.duration);
            gameObject.SetActive(false);
        }
    }
}
