using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayer : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    public float speed;
    public float jumpSpeed;
    //[HideInInspector]
    public bool facingRight = true;
    public GameManager2 gm;
    [HideInInspector]
    public bool isAlive = true;
    private bool startPlaying = false;

    public float xForce;
    public float yForce;


    public EchoEffect echo;
    private SpriteRenderer playerColor;

    public AdManager adManager;

    public TwoPlayer otherPlayer;

    public bool canBeHit;
    public float invisibleTime;

    // Start is called before the first frame update
    void Start()
    {
        canBeHit = true;
        if (gameObject.name == "Player1")
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }

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


        if (invisibleTime <= 0)
            canBeHit = true;
        else
            invisibleTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spikes" && canBeHit)
        {
            gm.retryButton.SetActive(true);
            //gm.backButton.SetActive(true);


            if (isAlive)
            {
                SoundManager.instance.Play("Death");
                

                if (gameObject.name == "Player1")
                {
                    gm.player2text.SetActive(true);
                    gm.player2text2.SetActive(true);
                    PlayerPrefs.SetInt("player2wins", PlayerPrefs.GetInt("player2wins", 0) + 1);
                    if (PlayerPrefs.GetInt("player2wins", 0) >= 3)
                        adManager.ShowAd();
                }
                else
                {
                    gm.player1text.SetActive(true);
                    gm.player1text2.SetActive(true);
                    //gm.player.rb.gravityScale = 0;
                    PlayerPrefs.SetInt("player1wins", PlayerPrefs.GetInt("player1wins", 0) + 1);
                    if (PlayerPrefs.GetInt("player1wins", 0) >= 3)
                        adManager.ShowAd();
                }

                otherPlayer.isAlive = false;
                otherPlayer.rb.velocity = Vector2.zero;//new Vector2(rb.velocity.x, rb.velocity.y);
                otherPlayer.rb.gravityScale = 0;
                //Time.timeScale = 0;
                /*
                if (PlayerPrefs.GetString("watchAd", "yes") == "yes")
                {
                    gm.ActivateLosingPanel();
                }
                else
                    gm.ActivateRetryButton();
                    */

            }

            isAlive = false;

            otherPlayer.echo.Active = false;
            echo.Active = false;
            playerColor.color = new Color(0f, 0f, 0f);
            if (collision.name == "rightSpike(Clone)")
            {
                if (gameObject.name == "Player1")
                    rb.AddForce(new Vector2(gameObject.transform.position.x - xForce, gameObject.transform.position.y - yForce));
                else
                    rb.AddForce(new Vector2(gameObject.transform.position.x - xForce, gameObject.transform.position.y + yForce));
            }
            if (collision.name == "leftSpike(Clone)")
            {
                if (gameObject.name == "Player1")
                    rb.AddForce(new Vector2(gameObject.transform.position.x + xForce, gameObject.transform.position.y - yForce));
                else
                    rb.AddForce(new Vector2(gameObject.transform.position.x + xForce, gameObject.transform.position.y + yForce));
            }
            if (collision.name == "SpikesUp")
            {
                if (gameObject.name == "Player1")
                {
                    if (facingRight)
                        rb.AddForce(new Vector2(gameObject.transform.position.x - xForce, gameObject.transform.position.y - yForce));
                    else
                        rb.AddForce(new Vector2(gameObject.transform.position.x + xForce, gameObject.transform.position.y - yForce));
                } 
            }

            if (collision.name == "SpikesDown" && gameObject.name == "Player2")
            {
                if (facingRight)
                    rb.AddForce(new Vector2(gameObject.transform.position.x - xForce, gameObject.transform.position.y + yForce));
                else
                    rb.AddForce(new Vector2(gameObject.transform.position.x + xForce, gameObject.transform.position.y + yForce));
            }
        }

        if (collision.tag == "Wall" && isAlive && gameObject.name=="Player1")
        {
            SoundManager.instance.Play("Wall");
            gm.WallCheck();
            canBeHit = false;
            invisibleTime = 0.2f;
            otherPlayer.canBeHit = false;
            otherPlayer.invisibleTime = 0.2f;
        }
    }

    public void Jump()
    {
        if (isAlive)
        {
            SoundManager.instance.Play("Tap");

            if (gameObject.name == "Player1")
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);// * Time.fixedDeltaTime);   //(rb.velocity.x, rb.velocity.y + jumpSpeed * Time.deltaTime);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -jumpSpeed);
            }
        }
    }

}
