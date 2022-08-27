using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public Rigidbody ball;
    public CamFollow cam;
    public Transform firePos;
    public Slider powerSlider;

    public AudioSource shootingAudio;
    public AudioClip fireClip;
    public AudioClip chargingClip;

    //Slider�� ������� ����.
    public float minForce = 15f;
    public float maxForce = 30f;
    public float chargingTime = 0.75f;

    private float currentForce;
    private float chargeSpeed;
    private bool fired;


    private void OnEnable()
    {
        currentForce = minForce;
        powerSlider.value = minForce;
        fired = false;
    }

    void Start()
    {
        //init
        chargeSpeed = (maxForce - minForce) / chargingTime;
    }

    void Update()
    {
        if(fired)
        {
            return;
        }

        powerSlider.value = minForce;
        if(currentForce>=maxForce && !fired)
        {
            currentForce = maxForce;
            Fire();
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            currentForce = minForce;
            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }
        else if (Input.GetButton("Fire1") && !fired)
        {
            currentForce = currentForce+chargeSpeed *Time.deltaTime;
            powerSlider.value = currentForce;
        }
        else if(Input.GetButtonUp("Fire1") &&!fired){
            Fire();
        }
    }

    private void Fire()
    {
        fired = true;

        Rigidbody ballInstance = Instantiate(ball, firePos.position, firePos.rotation);
        ballInstance.velocity = currentForce * firePos.forward;

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentForce = minForce;
        cam.SetTarget(ballInstance.transform, CamFollow.State.Tracking);
    }
}
