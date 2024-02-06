using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float _jump = 10f;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Touch _touch;

    public string currentColor;

    [SerializeField] private Color colorCam, colorSarý, colorMor, colorEflatun;
    
    [SerializeField] Text _score, panelScore, highScore;
    private int scoreValue, panelValue;
    
    [SerializeField] private GameObject one, two, three, panel;

    [SerializeField] private AudioSource[] collisionSounds;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _sr = GetComponent<SpriteRenderer>();
        panel.SetActive(false);
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        audioSource.Stop();
        RandomColor();
    }
    void Update()
    {
        _score.text = scoreValue.ToString();
        PlayerMovement();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ColorChanger")
        {           
            one.transform.position = transform.position + new Vector3(0f, 15f, 0f);
            collision.gameObject.transform.position = one.transform.position + new Vector3(0f, 2f, 0f);
            scoreValue++;
            panelValue++;
            panelScore.text = panelValue.ToString();
            if (panelValue > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", panelValue);
                highScore.text = panelValue.ToString();
            }
            RandomColor();
            return;
        }
        if(collision.tag == "ColorChanger2")
        {            
            two.transform.position = transform.position + new Vector3(0f, 15f, 0f);
            collision.gameObject.transform.position = two.transform.position + new Vector3(0f, 2f, 0f);
            scoreValue++;
            panelValue++;
            panelScore.text = panelValue.ToString();
            if(panelValue > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", panelValue);
                highScore.text = panelValue.ToString();
            }
            RandomColor();
            return;
        }
        if(collision.tag == "ColorChanger3")
        {            
            three.transform.position = transform.position + new Vector3(0f, 15f, 0f);
            collision.gameObject.transform.position = three.transform.position + new Vector3(0f, 2f, 0f);
            scoreValue++;
            panelValue++;
            panelScore.text = panelValue.ToString();
            if (panelValue > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", panelValue);
                highScore.text = panelValue.ToString();
            }
            RandomColor();
            return;
        }
        if(collision.tag != currentColor || collision.tag == "Respawn")
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
            one.SetActive(false);
            two.SetActive(false);
            three.SetActive(false);
            gameObject.SetActive(false);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
       
    }
    void RandomColor()
    {
        int index = Random.Range(0, 4);
        switch(index)
        {
            case 0:
                currentColor = "Cam";
                _sr.color = colorCam;
                break;
            case 1:
                currentColor = "Sarý";
                _sr.color = colorSarý;
                break;
            case 2:
                currentColor = "Mor";
                _sr.color = colorMor;
                break;
            case 3:
                currentColor = "Eflatun";
                _sr.color = colorEflatun;
                break;
        }
    }
    void PlayerMovement()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            _rb.velocity = Vector2.up * _jump;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            audioSource.Play();

        }
        if (Input.GetButtonDown("Jump") || Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _rb.velocity = Vector2.up * _jump;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            audioSource.Play();
        }
    }
}
