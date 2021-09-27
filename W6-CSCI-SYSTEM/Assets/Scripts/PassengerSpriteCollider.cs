using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PassengerSpriteCollider : MonoBehaviour
{
    [SerializeField] private GameObject explodeParticle;
    
    
    private Passenger _parentPassenger;
    private SpriteRenderer _spriteRenderer;
    private AngerBar _angerBar;
    private ScreenShake _CameraShake;
    

    // Start is called before the first frame update
    void Start()
    {
        _parentPassenger = transform.parent.GetComponent<Passenger>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _angerBar = FindObjectOfType<AngerBar>();
        _CameraShake = FindObjectOfType<ScreenShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(explodeParticle, transform.position, quaternion.identity);
        _CameraShake.ShakeScreen();
        _parentPassenger.CrashHappended();
        _angerBar.CrashAddAnger();
        _spriteRenderer.color*= new Color(0.7f, 0.5f, 0.5f);
        GameObject CollideSound = GameObject.Find("CollideSoundManager");
        CollideSound.GetComponent<AudioSource>().Play();
    }
}
