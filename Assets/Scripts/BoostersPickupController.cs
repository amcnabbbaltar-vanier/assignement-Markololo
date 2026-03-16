using System.Diagnostics;
using UnityEngine;

public class BoostersPickupController : MonoBehaviour
{
    private ParticleSystem pickupEffect; // Reference to the particle system for the pickup effect
    private AudioSource pickupSound; // Reference to the audio source for the pickup sound
    private CharacterMovement character; 
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

    // Method for when the player collides with a pickup 
    // (refence to CharacterMovement script for actions)
    public void GotHit(CharacterMovement inputCharacter)
    {
        character = inputCharacter;
        pickupEffect.Play();
        pickupSound.Play();
        
        if(this.tag == "ScoreAdd" || this.tag == "ScoreSubtract" || this.tag == "JumpBooster" || this.tag == "SpeedBooster")
        {
            Invoke("DeactivateObject", 0.3f); //Delay the deactivation to allow the effect and sound to play    
        }
    }

    private void DeactivateObject()
    {
        if(this.tag == "ScoreAdd")
        {
            GameManager.Instance.Score += 50;
            character.UpdateScoreText();
            gameObject.SetActive(false); 
        }
        if(this.tag == "ScoreSubtract")
        {
            if (GameManager.Instance.LivePoints <= 1)
            {
                GameManager.Instance.ResetGame();
            }
            GameManager.Instance.LivePoints --;
            character.UpdateLivePointsText();
            gameObject.SetActive(false); 
        }
        if(this.tag == "JumpBooster")
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            character.BoostJump();
            Invoke("ReviveBoost", 30);
            // gameObject.SetActive(false);
            
        }
        if(this.tag == "SpeedBooster")
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            character.BoostSpeed();
            Invoke("ReviveBoost", 30);
            // gameObject.SetActive(false); 
        }
    }

/*
When you touch the SpeedBoost pickup and Jump boost 
it should make the pickup should disappear and disable the collision  
and it should reappear 30 seconds later.
--> disable/enable collider and renderer, no need to destroy and remake the object
*/
    private void ReviveBoost()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
