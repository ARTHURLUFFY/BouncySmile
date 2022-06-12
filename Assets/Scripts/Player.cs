using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpSpeed;
    //[HideInInspector]
    public bool facingRight = true;
    public GameManager gm;
    private bool isAlive = true;
    private bool startPlaying = false;

    public float xForce;
    public float yForce;


    private EchoEffect echo;
    private SpriteRenderer playerColor;

    public CoinSpawner coinSpawner;

    public AdManager adManager;
    public int deaths = 3;

    public ParticleSystem plus1Particle;

    public bool shield = false;
    public GameObject shieldSprite;
    public GameObject shieldCanvas;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        isAlive = false;
        echo = GetComponent<EchoEffect>();
        playerColor = GetComponent<SpriteRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight && isAlive)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (!facingRight && isAlive)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (shield)
        {
            shieldSprite.SetActive(true);
        }
        else
        {
            shieldSprite.SetActive(false);
        }

      

        //Debug.Log(PlayerPrefs.GetInt("deaths", 3));
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
        */    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spikes" && !shield)
        {
            

            if (isAlive)
            {
                PlayerPrefs.SetInt("spikes", PlayerPrefs.GetInt("spikes", 0) + 1);
                SoundManager.instance.Play("Death");
                PlayerPrefs.SetInt("deaths", PlayerPrefs.GetInt("deaths", 3) - 1);
                if (PlayerPrefs.GetString("watchAd", "yes")== "yes")
                {
                    gm.ActivateLosingPanel();
                }
                else
                    gm.ActivateRetryButton();
                
            }
                
            isAlive = false;
            
            if(PlayerPrefs.GetInt("deaths",3)<=0)
            {
                PlayerPrefs.SetInt("deaths", 3);
                adManager.ShowAd();
            }
            
            
            gm.pauseButton.SetActive(false);
            echo.Active = false;
            playerColor.color = new Color(0f, 0f, 0f);
            if (collision.name == "rightSpike(Clone)")
            {
                rb.AddForce(new Vector2(gameObject.transform.position.x - xForce, gameObject.transform.position.y - yForce));
            }
            if (collision.name == "leftSpike(Clone)")
            {
                rb.AddForce(new Vector2(gameObject.transform.position.x + xForce, gameObject.transform.position.y - yForce));
            }
            if (collision.name == "SpikesUp")
            {
                if (facingRight)
                    rb.AddForce(new Vector2(gameObject.transform.position.x - xForce, gameObject.transform.position.y - yForce));
                else
                    rb.AddForce(new Vector2(gameObject.transform.position.x + xForce, gameObject.transform.position.y - yForce));
            }
            //gameObject.SetActive(false);

        }

        else if (collision.tag == "Spikes" && shield)
        {
            shield = false;
            PlayerPrefs.SetInt("shieldsBroke", PlayerPrefs.GetInt("shieldsBroke", 0) + 1);
            PlayerPrefs.SetInt("spikes", PlayerPrefs.GetInt("spikes", 0) + 1);
        }

        if (collision.tag == "Wall" && isAlive)
        {
            SoundManager.instance.Play("Wall");
            gm.WallCheck();
            PlayerPrefs.SetInt("walls", PlayerPrefs.GetInt("walls", 0) + 1);
        }

        if (collision.tag == "Coin")
        {
            plus1Particle.Play();
            SoundManager.instance.Play("Coin");
            Destroy(collision.gameObject);
            coinSpawner.coinSpawned = false;
            //gm.coins++;
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0)+1);
            PlayerPrefs.SetInt("totalCoins", PlayerPrefs.GetInt("totalCoins", 0) + 1);
            gm.coinsText.text = PlayerPrefs.GetInt("coins", 0).ToString();
        }
    }

    public void Jump()
    {
        PlayerPrefs.SetInt("jumps", PlayerPrefs.GetInt("jumps", 0)+1);
        if (isAlive)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);// * Time.fixedDeltaTime);   //(rb.velocity.x, rb.velocity.y + jumpSpeed * Time.deltaTime);
            SoundManager.instance.Play("Tap");
        }
        else if (!startPlaying)
        {
            PlayerPrefs.SetInt("gamesPlayed", PlayerPrefs.GetInt("gamesPlayed", 0) + 1);
            SoundManager.instance.Play("Start");
            //SoundManager.instance.Play("Tap");
            isAlive = true;
            rb.gravityScale = 1.2f;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            gm.tapText.gameObject.SetActive(false);
            gm.bestScoreText.gameObject.SetActive(false);
            gm.coinsText.gameObject.SetActive(false);
            gm.coinImage.SetActive(false);
            gm.settingsButton.SetActive(false);
            gm.shopButton.SetActive(false);
            gm.pauseButton.SetActive(true);
            Destroy(GetComponent<MovingObject>());
            startPlaying = true;
            echo.Active = true;
            shieldCanvas.SetActive(false);
            gm.statsButton.SetActive(false);
            gm.twoPlayersScreen.SetActive(false);
        }
    }
}
