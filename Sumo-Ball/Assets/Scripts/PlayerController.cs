using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private Rigidbody playerRb;
    private GameObject focalPoint;
    [SerializeField] 
    private float _speed = 5.0f;
    private float powerUpStrength = 15.0f;
    public bool hasPowerUp ;  
    public GameObject powerUpIndicator;
    

    public PowerUpType currentPowerUp = PowerUpType.None;
    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerUpCountDown;

    public float hangTime = 1.0f;
    public float smashSpeed = 1.0f;
    public float explosionForce = 1.0f;
    public float explosionRadius = 6.0f;

    bool smashing = false;
    float floorY;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();        
        focalPoint = GameObject.Find("Focal Point");   
        
              
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        
            
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * _speed);

        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            
            LaunchRockets();
        }
        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
              
            smashing = true;
            StartCoroutine(Smash());
        }
        
        


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp") )
        {
            

            hasPowerUp = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDownRoutine());

            if(powerUpCountDown != null)
            {
                StopCoroutine(powerUpCountDown);
            }
            powerUpCountDown = StartCoroutine(PowerupCountDownRoutine());

        }
    }
    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        currentPowerUp = PowerUpType.None;
        powerUpIndicator.gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.PushBack)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromplayer = (collision.gameObject.transform.position - transform.position);

            enemyRigidbody.AddForce(awayFromplayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log("Player Collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
            
           
        }
    }
    void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up,Quaternion.identity);
            tmpRocket.GetComponent<RocketBehavior>().Fire(enemy.transform);
        }
    }
    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<enemy>();
        //Store the y position before taking off
        floorY = transform.position.y;
        //Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;
        while (Time.time < jumpTime)
        {
            //move the player up while still keeping their x velocity.
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }
        //Now move the player down
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }
        //Cycle through all enemies.
        for (int i = 0; i < enemies.Length; i++)
        {
            //Apply an explosion force that originates from our position.
            if (enemies[i] != null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
        }
        //We are no longer smashing, so set the boolean to false
        smashing = false;
    }
    

}
