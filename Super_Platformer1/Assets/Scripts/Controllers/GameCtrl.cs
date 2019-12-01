using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;                                            //to work with files
using System.Runtime.Serialization.Formatters.Binary;       //RSFB helps serialization
using DG.Tweening;


/// <summary>
/// manages important game actions such as saving and storing results, restarting levels,
/// save/load data, HUD updates, etc.
/// </summary>
public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    public float restartDelay;

    [HideInInspector]
    public GameData data;                 // to work with game data in inspector
    public UI ui;
    public GameObject bigCoin;            // wins the player when he kills an enemy
    public GameObject player;             // cat game caracter
    public GameObject lever;              // the handle that frees the Dog
    public GameObject enemySpawner;       // make enemies while the boss battle lasts
    public GameObject signPlatform;       // a platform leading to the Boss
    //public Text txtCoinCount;           // keeps track of how much money has been collected
    //public Text txtScore;               // shows score in HUD
    public int coinValue;                 // the value of one small coin
    public int bigCoinValue;              // the value of a big coin
    public int enemyValue;                // the value of an enemy
    public float maxTime;                 // maximum time allowed to cross the level
    public GameObject btnPause;

    public enum Item
    {
        Coin,
        BigCoin,
        Enemy
    }

    string dataFilePath;                // path to store the data file
    BinaryFormatter bf;                 // helps saving/loading to binary files 
    float timeLeft;                     // time left before the timer expires
    bool timerOn;                       // checks whether the timer should be on or off
    bool isPaused;                      // pause/unpause game

    void Awake()
    {
        if (instance == null)
            instance = this;

        bf = new BinaryFormatter();

        dataFilePath = Application.persistentDataPath + "/game.dat";
        Debug.Log(dataFilePath);

        DeleteCheckPoints();
        
        HandleFirstBoot();
    }

    void Start ()
    {

        DataCtrl.instance.RefreshData();
        data = DataCtrl.instance.data;
        RefreshUI();

        //data.coinCount = 0;           //ovo vraca na nulu poene i coinse ali kada se izgubi svaki zivot
        //data.score = 0;

        GameCtrl.instance.data.keyFound[0] = false;
        GameCtrl.instance.data.keyFound[1] = false;
        GameCtrl.instance.data.keyFound[2] = false;
        RefreshUI();
        //LevelComplete();
        DeleteCheckPoints();


        timeLeft = maxTime;

        timerOn = true;
        isPaused = false;
        
        UpdateHearts();

        ui.bossHealth.gameObject.SetActive(false);
    }
	
	void Update ()
    {
        if (isPaused)
        {
            // postavlja Time.timeScale = 0
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (timeLeft > 0 && timerOn)
            UpdateTimer();

        AchievementCtrl.instance.CheckAchievements();

    }

    /// <summary>
    /// Brise Checkpoint sistem
    /// </summary>
    public void DeleteCheckPoints()
    {
        PlayerPrefs.DeleteKey("CPX");
        PlayerPrefs.DeleteKey("CPY");
    }

    public void SaveData()
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();         //this is important!
    }

    public void RefreshUI()
    {
        ui.txtCoinCount.text = " x " + data.coinCount;
        ui.txtScore.text = "Score: " + data.score;
    }

    void OnEnable()
    {
        Debug.Log("Data Loaded");
       
        RefreshUI();
    }

    void OnDisable()
    {
         Debug.Log("OnDisable called");
        // SaveData();
        DataCtrl.instance.SaveData(data);
        
        Time.timeScale = 1;

        // Sakrije AdMob banner
        AdsCtrl.instance.HideBanner();
    }

    /*void ResetData()
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);

        data.coinCount = 0;
        ui.txtCoinCount.text = " x 0";
        data.score = 0;
        for (int keyNumber = 0; keyNumber <= 2; keyNumber++)
        {
            data.keyFound[keyNumber] = false;
        }
        data.lives = 3;
        UpdateHearts();
        ui.txtScore.text = "Score: " + data.score;

        // resetuje level data
        foreach (LevelData level in data.levelData)
        {
            level.starsAwarded = 0;
            if (level.levelNumber != 1)
                level.isUnlocked = false;
        }

        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Data Reset");
    }*/

    /// <summary>
    /// Keeps picked up stars for the level
    /// </summary>
    /// <param name="levelNumber"></param>
    /// <param name="numOfStars"></param>
    public void SetStarsAwarded(int levelNumber, int numOfStars)
    {
        data.levelData[levelNumber].starsAwarded = numOfStars;

        Debug.Log("Broj osvojenih zvezdica je: " + data.levelData[levelNumber].starsAwarded);
    }

    /// <summary>
    /// Unlocks a certain level
    /// </summary>
    /// <param name="levelNumber"></param>
    public void UnlockLevel(int levelNumber)
    {
        //data.levelData[levelNumber].isUnlocked = true;

        if ((levelNumber + 1) <= (data.levelData.Length - 1))
            data.levelData[levelNumber + 1].isUnlocked = true;
    }

    /// <summary>
    /// Takes the current score for the Level Complete Menu
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return data.score;
    }
    public int GetHighScore()
    {
        return data.highScore;
    }

    /// <summary>
    /// Restarts the level when the player dies
    /// </summary>
    /// <param name="player"></param>
    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);
        //Invoke("RestartLevel", restartDelay);
         CheckLives();
    }

    public void PlayerDiedAnimation(GameObject player)
    {
        // throws the player back
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-150f, 400f));

        // rotates the player slightly
        player.transform.Rotate(new Vector3(0, 0, 45f));

        // extinguishes PlayerCtrl script
        player.GetComponent<PlayerCtrl>().enabled = false;

        // it unlocks the player's colliders so it can collapse through the ground when it dies
        foreach (Collider2D c2d in player.transform.GetComponents<Collider2D>())
        {
            c2d.enabled = false;
        }

        // disables children of gameobjects that are attached to the cat
        foreach (Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }

        // disables the camera that is attached to the player
        Camera.main.GetComponent<CameraCtrl>().enabled = false;

        // sets the cat's velocity to 0
        rb.velocity = Vector2.zero;

        // restart level
        StartCoroutine("PauseBeforeReload", player);
    }

    public void PlayerStompsEnemy(GameObject enemy)
    {
        // changes the enemy tag
        enemy.tag = "Untagged";     // we change this because the enemy can kill the player, but the moment the enemy becomes the "Untagged" tag, any gameobjects that has the Untagged tag cannot kill the player

        // destroy the enemyja
        Destroy(enemy);

        // updates the score
        UpdateScore(Item.Enemy);
    }

    IEnumerator PauseBeforeReload(GameObject player)
    {
        yield return new WaitForSeconds(1.5f);
        PlayerDied(player);
    }

    /// <summary>
    /// restarts the level when the player falls into the water
    /// </summary>
    /// <param name="player"></param>
    public void PlayerDrowned(GameObject player)
    {
        Invoke("RestartLevel", restartDelay);
        CheckLives();
    }

    public void UpdateCoinCount()
    {
        data.coinCount += 1;

        ui.txtCoinCount.text = " x " + data.coinCount;
    }

    public void UpdateScore(Item item)
    {
        int itemValue = 0;
        switch (item)
        {
            case Item.BigCoin:
                itemValue = bigCoinValue;
                break;
            case Item.Coin:
                itemValue = coinValue;
                break;
            case Item.Enemy:
                itemValue = enemyValue;
                break;
            default:
                break;

        }

        data.score += itemValue;

        ui.txtScore.text = "Score: " + data.score;
    }

    /// <summary>
    /// Called when a player bullet hits an enemy
    /// </summary>
    /// <param name="enemy"></param>
    public void BulletHitEnemy(Transform enemy)
    {
        // prikazuje enemy explosion SFX
        Vector3 pos = enemy.position;
        pos.z = 20f;
        SFXCtrl.instance.EnemyExplosion(pos);

        // pokazuje se BigCoin
        Instantiate(bigCoin, pos, Quaternion.identity);

        // unistava enemyja
        Destroy(enemy.gameObject);

        AudioCtrl.instance.EnemyExplosion(pos);
    }

    public void UpdateKeyCount(int keyNumber)
    {
        data.keyFound[keyNumber] = true;

        if (keyNumber == 0)
            ui.key0.sprite = ui.key0Full;
        else if (keyNumber == 1)
            ui.key1.sprite = ui.key1Full;
        else if (keyNumber == 2)
            ui.key2.sprite = ui.key2Full;

        if (data.keyFound[0] && data.keyFound[1])
            ShowSignPlatform();
    }

    void ShowSignPlatform()
    {
        signPlatform.SetActive(true);

        SFXCtrl.instance.ShowPlayerLanding(signPlatform.transform.position);

        timerOn = false;

        ui.bossHealth.gameObject.SetActive(true);
    }

    public void LevelComplete()
    {
        if (timerOn)
            timerOn = false;

        ui.panelMobileUI.SetActive(false);
        ui.levelCompleteMenu.SetActive(true);
        Time.timeScale = 0;

        if (data.score > data.highScore)
        { 
            data.highScore = data.score;
        }

        data.keyFound[0] = false;
        data.keyFound[1] = false;
        data.keyFound[2] = false;

        data.lives = 3;

        DeleteCheckPoints();

        AdsCtrl.instance.ShowInterstitial();
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //  AdsCtrl.instance.ShowInterstitial();
        
        // DeleteCheckPoints();
    }

    void UpdateTimer() 
    {
        timeLeft -= Time.deltaTime;

        ui.txtTimer.text = "Timer: " + (int)timeLeft;

        if (timeLeft <= 0)
        {
            ui.txtTimer.text = "Timer: 0";

            // informs GameCtrl to do what it takes

            GameObject player = GameObject.FindGameObjectWithTag("Player") as GameObject;
            PlayerDied(player);
        }
    }

    public void HandleFirstBoot()
    {
        if (data.isFirstBoot)
        {
            // podesi zivote na 3
            data.lives = 3;

            // podesi broj coinsa na 0
            data.coinCount = 0;

            // podesi pokupljene kljuceve na 0
            data.keyFound[0] = false;
            data.keyFound[1] = false;
            data.keyFound[2] = false;

            // podesi score na 0
            data.score = 0;

            // podesi isFirstBoot na false
            data.isFirstBoot = false;
        }

    }

    void UpdateHearts()
    {
        if (data.lives == 3)
        {
            ui.heart1.sprite = ui.fullHeart;
            ui.heart2.sprite = ui.fullHeart;
            ui.heart3.sprite = ui.fullHeart;
        }

        if (data.lives == 2)
        {
            ui.heart1.sprite = ui.emptyHeart;           // if the player has 2 lives then one heart is empty (first heart)
        }

        if (data.lives == 1)
        {
            ui.heart1.sprite = ui.emptyHeart;           // 1 more life left so the first two positions are empty hearts
            ui.heart2.sprite = ui.emptyHeart;
        }
    }

    void CheckLives()
    {
        int updatedLives = data.lives;
        updatedLives -= 1;
        data.lives = updatedLives;

        if (data.lives == 0)
        {
            data.lives = 3;
            //SaveData();
            DataCtrl.instance.SaveData(data);
            Invoke("GameOver", restartDelay);

        }
        else
        {
            //SaveData();
            DataCtrl.instance.SaveData(data);
            Invoke("RestartLevel", restartDelay);
        }
    }

    void GameOver()
    {
        //if(data.score > data.highScore)
        //{
        //    data.highScore = data.score;
        //  //  LeaderboardCtrl.instance.PostScore(data.highScore);
        //}
        //ui.panelGameOver.SetActive(true);
        

        LeaderboardCtrl.instance.ReportScore(GameCtrl.instance.data.score);

        if (timerOn)
            timerOn = false;

        ui.panelGameOver.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, false);

        data.score = 0;
        data.coinCount = 0;

        //data.keyFound[0] = false;
        //data.keyFound[1] = false;
        //data.keyFound[2] = false;

        DeleteCheckPoints();

        AdsCtrl.instance.ShowBanner();
        
        AdsCtrl.instance.ShowInterstitial();
    }

    public void ShowPausePanel()
    {
        if (ui.panelMobileUI.activeInHierarchy)
            ui.panelMobileUI.SetActive(false);

        // show pause menu
        ui.panelPause.SetActive(true);

        // animates the Pause panel
        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, false);

        AdsCtrl.instance.ShowBanner();

        AdsCtrl.instance.ShowInterstitial();

        Invoke("SetPause", 1.1f);

        btnPause.GetComponent<Button>().interactable = false;
    }

    void SetPause()
    {
        // set the bool
        isPaused = true;
    }

    /// <summary>
    /// hides panel pause
    /// </summary>
    public void HidePausePanel()
    {
        isPaused = false;

        if (!ui.panelMobileUI.activeInHierarchy)
            ui.panelMobileUI.SetActive(true);

        // sakrije pause manu
        //   ui.panelPause.SetActive(false);

        // animates the Pause panel
        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(600f, 0.7f, false);

        btnPause.GetComponent<Button>().interactable = true;

        AdsCtrl.instance.HideBanner();
    }

    public void StopCameraFollow(GameObject player)
    {
        Camera.main.GetComponent<CameraCtrl>().enabled = false;
        player.GetComponent<PlayerCtrl>().isStuck = true;           // stop parallax
       // Camera.main.transform.position = new Vector3(125.3f, 4.4f, -10f);
        player.transform.Find("Left_Check").gameObject.SetActive(false);
        player.transform.Find("Right_Check").gameObject.SetActive(false);
    }

    public void ShowLever()
    {
        lever.SetActive(true);

        DeactivateEnemySpawner();

        SFXCtrl.instance.ShowPlayerLanding(lever.gameObject.transform.position);

        AudioCtrl.instance.EnemyExplosion(lever.gameObject.transform.position);
    }

    public void ActivateEnemySpawner()
    {
        enemySpawner.SetActive(true);
    }

    public void DeactivateEnemySpawner()
    {
        enemySpawner.SetActive(false);
    }


}
