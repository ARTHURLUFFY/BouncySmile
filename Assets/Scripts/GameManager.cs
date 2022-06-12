using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	bool[] spikeArrayLeft = new bool[11];
	bool[] spikeArrayRight = new bool[11];
	Vector3[] leftSpikePosition = new Vector3[11];
	Vector3[] rightSpikePosition = new Vector3[11];
	public GameObject leftSpike;
	public GameObject rightSpike;
	public static int score = 0;
	//public  int coins = 0;
	GameObject[] leftSpikes = new GameObject[11];
	GameObject[] rightSpikes = new GameObject[11];
	int spikeNumber;

	public Player player;

	public GameObject losingPanel;
	public GameObject retryButton;
	//public GameObject retryButton2;

	public Text scoreText;
	public Text tapText;
	//public Text tapText2;
	public Text coinsText;


	public Sprite zero;
	public Sprite one;
	public Sprite two;
	public Sprite three;
	public Sprite four;
	public Sprite five;
	public Sprite six;
	public Sprite seven;
	public Sprite eight;
	public Sprite nine;

	public SpriteRenderer leftNumber;
	public SpriteRenderer rightNumber;
	public Text bestScoreText;

	public GameObject scoreBelow100;
	public GameObject scoreAbove100;

	public SpriteRenderer firstNumber;
	public SpriteRenderer secondNumber;
	public SpriteRenderer thirdNumber;

	private CoinSpawner coinSpawner;

	public GameObject settingsButton;
	public GameObject pauseButton;
	public GameObject coinImage;
	public GameObject pauseImage;
	public GameObject playImage;
	public GameObject shopButton;
	public GameObject muteImage;
	public GameObject statsButton;
	public GameObject twoPlayersScreen;
	//private bool muted = false;

	public AdManager adManager;

	public Transform wallLeft;
	public Transform wallRight;


	// Start is called before the first frame update
	void Start()
    {
		bestScoreText.text = "Best score: " + PlayerPrefs.GetInt("highScore", 0).ToString();
		coinSpawner = GetComponent<CoinSpawner>();
		coinsText.text = PlayerPrefs.GetInt("coins",0).ToString();
		ScoreToImage();

		for (int i = 0; i < 11; i++)
		{
			leftSpikePosition[i] = new Vector3(wallLeft.transform.position.x+0.22f, 3.75f - i * 0.64f, 0);
			//leftSpikePosition[i] = new Vector3(-2.2f, 3.75f - i * 0.64f, 0);
		}
		for (int i = 0; i < 11; i++)
		{
			rightSpikePosition[i] = new Vector3(wallRight.transform.position.x-0.22f, 3.75f - i * 0.64f, 0);
			//rightSpikePosition[i] = new Vector3(2.2f, 3.75f - i * 0.64f, 0);
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (PlayerPrefs.GetString("mute", "mute") == "muted")
			muteImage.SetActive(true);
	}

	void ClearArray(bool[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = false;
		}
	}

	void FillSpikes(bool[] array, int spikeCount)
	{
		int randomPosition = 0;
		int spikesFilled = 0;
		while (spikesFilled < spikeCount)
		{
			randomPosition = Random.Range(0, array.Length);            //new Random.Next Range(0, spikeCount - 1);
			if (array[randomPosition] == false)
			{
				array[randomPosition] = true;
				spikesFilled++;
			}
		}
	}

	void SpawnSpikesLeft(bool[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				leftSpikes[i] = Instantiate(leftSpike, leftSpikePosition[i], Quaternion.identity) as GameObject;
			}

		}
	}
	void SpawnSpikesRight(bool[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				rightSpikes[i] = Instantiate(rightSpike, rightSpikePosition[i], Quaternion.identity) as GameObject;
			}

		}
	}

	void DespawnSpikes(GameObject[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			try
			{
				Destroy(array[i]);
			}
			catch { };

		}
	}


	
	public void WallCheck()
	{
		
		if (coinSpawner.coinSpawned == false) { coinSpawner.SpawnCoin(); }

		{
			score++;
			if(score > PlayerPrefs.GetInt("highScore", 0))
			{
				PlayerPrefs.SetInt("highScore", score);
			}


			if (score <= 2) { spikeNumber = 2;}
			else if (score > 2 && score <= 9) { spikeNumber = 3; }
			else if (score >= 10 && score <= 19) { spikeNumber = 4; }
			else if (score >= 20 && score <= 39) { spikeNumber = 5; }
			else if (score >= 40 && score <= 69) { spikeNumber = 6;}
			else if (score >= 70 && score <= 119) { spikeNumber = 7; }
			else if (score >= 120 && score <= 199) { spikeNumber = 8; }
			else if (score >= 200) { spikeNumber = 9; }
			//spikeNumber = 0;

			if (score == 100) { player.speed *= 1.1f; }

			if (player.facingRight == false)
			{
				FillSpikes(spikeArrayRight, spikeNumber);
				SpawnSpikesRight(spikeArrayRight);
				ClearArray(spikeArrayRight);
				DespawnSpikes(leftSpikes);
			}
			else
			{


				FillSpikes(spikeArrayLeft, spikeNumber); //dqsna stena
				SpawnSpikesLeft(spikeArrayLeft);
				ClearArray(spikeArrayLeft);
				DespawnSpikes(rightSpikes);
			}
			//Debug.Log(score);
			scoreText.text = score.ToString();
			ScoreToImage();
		}
	}

	public void Retry()
	{
		SoundManager.instance.Play("Button");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Yes()
	{
		adManager.ShowRewardingAd();
		DeActivateLosingPanel();
		ActivateRetryButton2();
		PlayerPrefs.SetString("watchAd", "no");
	}

	public void No()
	{
		score = 0;
		DeActivateLosingPanel();
		ActivateRetryButton();
		
	}
	public void ActivateRetryButton()
	{
		score = 0;
		retryButton.SetActive(true);
		PlayerPrefs.SetString("watchAd", "yes");
	}
	public void ActivateRetryButton2()
	{
		retryButton.SetActive(true);
	}

	public void ActivateLosingPanel()
	{
		losingPanel.SetActive(true);
	}
	public void DeActivateLosingPanel()
	{
		losingPanel.SetActive(false);
	}

	void ScoreToImage()
	{
		if (score<10)
		{
			if (score == 1) rightNumber.sprite = one;
			else if (score == 2) rightNumber.sprite = two;
			else if (score == 3) rightNumber.sprite = three;
			else if (score == 4) rightNumber.sprite = four;
			else if (score == 5) rightNumber.sprite = five;
			else if (score == 6) rightNumber.sprite = six;
			else if (score == 7) rightNumber.sprite = seven;
			else if (score == 8) rightNumber.sprite = eight;
			else if (score == 9) rightNumber.sprite = nine;
		}
		else if (score>=10 && score<100)
		{
			int leftNumb = score / 10;
			int rightNumb = score % 10;
			if (leftNumb == 1) { leftNumber.sprite = one; }
			else if (leftNumb == 2) { leftNumber.sprite = two; }
			else if (leftNumb == 3) { leftNumber.sprite = three; }
			else if (leftNumb == 4) { leftNumber.sprite = four; }
			else if (leftNumb == 5) { leftNumber.sprite = five; }
			else if (leftNumb == 6) { leftNumber.sprite = six; }
			else if (leftNumb == 7) { leftNumber.sprite = seven; }
			else if (leftNumb == 8) { leftNumber.sprite = eight; }
			else if (leftNumb == 9) { leftNumber.sprite = nine; }

			if (rightNumb == 0) { rightNumber.sprite = zero; }
			else if (rightNumb == 1) { rightNumber.sprite = one; }
			else if (rightNumb == 2) { rightNumber.sprite = two; }
			else if (rightNumb == 3) { rightNumber.sprite = three; }
			else if (rightNumb == 4) { rightNumber.sprite = four; }
			else if (rightNumb == 5) { rightNumber.sprite = five; }
			else if (rightNumb == 6) { rightNumber.sprite = six; }
			else if (rightNumb == 7) { rightNumber.sprite = seven; }
			else if (rightNumb == 8) { rightNumber.sprite = eight; }
			else if (rightNumb == 9) { rightNumber.sprite = nine; }
		}
		else
		{
			scoreBelow100.SetActive(false);
			scoreAbove100.SetActive(true);
			int firstNumb = score / 100;
			int secNumb = score / 10;
			int thirdNumb = score % 10;

			if (firstNumb == 1) { firstNumber.sprite = one; }
			else if (firstNumb == 2) { firstNumber.sprite = two; }
			else if (firstNumb == 3) { firstNumber.sprite = three; }
			else if (firstNumb == 4) { firstNumber.sprite = four; }
			else if (firstNumb == 5) { firstNumber.sprite = five; }
			else if (firstNumb == 6) { firstNumber.sprite = six; }
			else if (firstNumb == 7) { firstNumber.sprite = seven; }
			else if (firstNumb == 8) { firstNumber.sprite = eight; }
			else if (firstNumb == 9) { firstNumber.sprite = nine; }

			if (secNumb == 10 || secNumb == 20 || secNumb == 30 || secNumb == 40 || secNumb == 50 || secNumb == 60 || secNumb == 70 || secNumb == 80 || secNumb == 90) { secondNumber.sprite = zero; }
			else if (secNumb == 11 || secNumb == 21 || secNumb == 31 || secNumb == 41 || secNumb == 51 || secNumb == 61 || secNumb == 71 || secNumb == 81 || secNumb == 91) { secondNumber.sprite = one; }
			else if (secNumb == 12 || secNumb == 22 || secNumb == 32 || secNumb == 42 || secNumb == 52 || secNumb == 62 || secNumb == 72 || secNumb == 82 || secNumb == 92) { secondNumber.sprite = two; }
			else if (secNumb == 13 || secNumb == 23 || secNumb == 33 || secNumb == 43 || secNumb == 53 || secNumb == 63 || secNumb == 73 || secNumb == 83 || secNumb == 93) { secondNumber.sprite = three; }
			else if (secNumb == 14 || secNumb == 24 || secNumb == 34 || secNumb == 44 || secNumb == 54 || secNumb == 64 || secNumb == 74 || secNumb == 84 || secNumb == 94) { secondNumber.sprite = four; }
			else if (secNumb == 15 || secNumb == 25 || secNumb == 35 || secNumb == 45 || secNumb == 55 || secNumb == 65 || secNumb == 75 || secNumb == 85 || secNumb == 95) { secondNumber.sprite = five; }
			else if (secNumb == 16 || secNumb == 26 || secNumb == 36 || secNumb == 46 || secNumb == 56 || secNumb == 66 || secNumb == 76 || secNumb == 86 || secNumb == 96) { secondNumber.sprite = six; }
			else if (secNumb == 17 || secNumb == 27 || secNumb == 37 || secNumb == 47 || secNumb == 57 || secNumb == 67 || secNumb == 77 || secNumb == 87 || secNumb == 97) { secondNumber.sprite = seven; }
			else if (secNumb == 18 || secNumb == 28 || secNumb == 38 || secNumb == 48 || secNumb == 58 || secNumb == 68 || secNumb == 78 || secNumb == 88 || secNumb == 98) { secondNumber.sprite = eight; }
			else if (secNumb == 19 || secNumb == 29 || secNumb == 39 || secNumb == 49 || secNumb == 59 || secNumb == 69 || secNumb == 79 || secNumb == 89 || secNumb == 99) { secondNumber.sprite = nine; }

			if (thirdNumb == 0) { thirdNumber.sprite = zero; }
			else if (thirdNumb == 1) { thirdNumber.sprite = one; }
			else if (thirdNumb == 2) { thirdNumber.sprite = two; }
			else if (thirdNumb == 3) { thirdNumber.sprite = three; }
			else if (thirdNumb == 4) { thirdNumber.sprite = four; }
			else if (thirdNumb == 5) { thirdNumber.sprite = five; }
			else if (thirdNumb == 6) { thirdNumber.sprite = six; }
			else if (thirdNumb == 7) { thirdNumber.sprite = seven; }
			else if (thirdNumb == 8) { thirdNumber.sprite = eight; }
			else if (thirdNumb == 9) { thirdNumber.sprite = nine; }

			if (score > 999) { firstNumber.sprite = nine; secondNumber.sprite = nine; thirdNumber.sprite = nine; }
		}
	}
	public void Pause()
	{
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			playImage.SetActive(false);
			pauseImage.SetActive(true);
		}
		else 
		{ 
			Time.timeScale = 0;
			playImage.SetActive(true);
			pauseImage.SetActive(false);
		}
	}

	public void Mute()
	{
		if (PlayerPrefs.GetString("mute", "mute") == "muted")
		{
			muteImage.SetActive(false);
			//muted = false;
			PlayerPrefs.SetString("mute", "mute");
		}
			
		else
		{
			muteImage.SetActive(true);
			//muted = true;
			PlayerPrefs.SetString("mute", "muted");
		}
			
	}

	public void PlusShield()
	{
		if (player.shield == false && PlayerPrefs.GetInt("coins", 0)>=100)
		{
			PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) -100);
			player.shield = true;
			coinsText.text = PlayerPrefs.GetInt("coins", 0).ToString();
		}
			
	}

	public void AdShield()
	{
		if (player.shield==false)
		{
			adManager.ShowRewardingAd();
			player.shield = true;
		}

	}

	public void NextScene()
	{
		score = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}
}
