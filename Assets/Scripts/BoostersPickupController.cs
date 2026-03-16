using System.Diagnostics;
using UnityEngine;

public class BoostersPickupController : MonoBehaviour
{
    private ParticleSystem pickupEffect; // Reference to the particle system for the pickup effect
    private AudioSource pickupSound; // Reference to the audio source for the pickup sound

    // public float hoveringHeihgt = 0.04f; //change it in game to see best result 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupEffect = GetComponent<ParticleSystem>();
        pickupSound = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float newPositionY = transform.position.y + Mathf.Sin(Time.time) * 0.001f; //I'm trying to use time for smoothness
        transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z); //Hover
        //Rotate:
        transform.Rotate(Vector3.up, 50f * Time.deltaTime);
    }

    public void GotHit()
    {
        pickupEffect.Play();
        pickupSound.Play();
        
        if(this.tag == "ScoreAdd")
        {
            GameManager.Instance.score += 50;
            
        }
        if(this.tag == "ScoreSubtract")
        {
            //more code according to instructions
        }
        if(this.tag == "JumpBooster")
        {
            // Increase jump force
             // Reset jump force after 5 seconds
        }
        if(this.tag == "SpeedBooster")
        {
            // Increase speed multiplier
            // Invoke("ResetSpeedMultiplier", 5f); 
            // Reset speed multiplier after 5 seconds
        }
    }

}
