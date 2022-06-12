using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    public Text jumpText;
    public Text wallText;
    public Text gamesText;
    public Text spikesText;
    public Text shieldsText;
    public Text totalCoinsText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        jumpText.text = "Jumps: " + PlayerPrefs.GetInt("jumps", 0).ToString();
        wallText.text = "Walls hit: " + PlayerPrefs.GetInt("walls", 0).ToString();
        gamesText.text = "Times played: " + PlayerPrefs.GetInt("gamesPlayed", 0).ToString();
        spikesText.text = "Spikes: " + PlayerPrefs.GetInt("spikes", 0).ToString();
        shieldsText.text = "Shields broke: " + PlayerPrefs.GetInt("shieldsBroke", 0).ToString();
        totalCoinsText.text = "Total coins: " + PlayerPrefs.GetInt("totalCoins", 0).ToString();
    }


}
