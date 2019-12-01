using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelCompleteCtrl : MonoBehaviour
{
    public Button btnNext;              // this button loads the next level
    public Sprite goldenStar;           // wins when a certain score is reached
    public Image Star1;                 // White Star UI Image
    public Image Star2;
    public Image Star3;
    public Text txtScore;               // to display the score
    public Text txtHighScore;
    public int levelNumber;             // what is the level

    [HideInInspector]
    public int score;                   // current score
    public int highScore;
    public int ScoreForThreeStars;      // score needed to win 3 gold stars
    public int ScoreForTwoStars;        // score needed to win 2 gold stars
    public int ScoreForOneStars;        // score needed to win 1 gold stars
    public int ScoreForNextLevel;       // the score required to unlock the next level
    public float animStartDelay;        // a brief delay before winning the gold star
    public float animDelay;             // delay animation between each gold star 0.7f

    bool showTwoStars, showThreeStars;  // checks how many stars it displays


    void Start ()
    {
        // beta testing -> comment on this section and up [HideInInspector] to write the score you want
        score = GameCtrl.instance.GetScore();       // takes the current score
        highScore = GameCtrl.instance.GetHighScore();

        if (score > highScore)
            highScore = score;

        // updates score text
        txtScore.text = "" + score;

        txtHighScore.text = "Highscore: " + highScore;

        // determines the number of stars to be won
        if (score >= ScoreForThreeStars)
        {
            showThreeStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 3);
            Invoke("ShowGoldenStars", animStartDelay);
        }
        if (score >= ScoreForTwoStars && score < ScoreForThreeStars)
        {
            showTwoStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 2);
            Invoke("ShowGoldenStars", animStartDelay);
        }
        if (score < ScoreForTwoStars && score != 0)               //--> pay attention here and predict the score well!
        {
            GameCtrl.instance.SetStarsAwarded(levelNumber, 1);
            Invoke("ShowGoldenStars", animStartDelay);
        }
    }

    void ShowGoldenStars()
    {
        StartCoroutine("HandleFirstStarAnim", Star1);
    }

    IEnumerator HandleFirstStarAnim(Image starImg)
    {
        DoAnim(starImg);

        // pause before the next star shows
        yield return new WaitForSeconds(animDelay);

        // invoked if more than one star is won
        if (showTwoStars || showThreeStars)
            StartCoroutine("HandleSecondStarAnim", Star2);
        else
            Invoke("CheckLevelStatus", 1.2f);
    }

    IEnumerator HandleSecondStarAnim(Image starImg)
    {
        DoAnim(starImg);

        yield return new WaitForSeconds(animDelay);

        showTwoStars = false;

        if (showThreeStars)
            StartCoroutine("HandleThirdStarAnim", Star3);
        else
            Invoke("CheckLevelStatus", 1.2f);
    }

    IEnumerator HandleThirdStarAnim(Image starImg)
    {
        DoAnim(starImg);

        yield return new WaitForSeconds(animDelay);

        showThreeStars = false;
        Invoke("CheckLevelStatus", 1.2f);
    }

    void CheckLevelStatus()
    {
        //--------  unlocks the next level if a certain result is reached-----------------------------------------
        if (score >= ScoreForNextLevel)
        {
            btnNext.interactable = true;

            // particle effect
            SFXCtrl.instance.ShowBulletSparkle(btnNext.gameObject.transform.position);

            // audio
            AudioCtrl.instance.KeyFound(btnNext.gameObject.transform.position);

            // unlock next level
            GameCtrl.instance.UnlockLevel(levelNumber);
            
        }
        else
        {
            btnNext.interactable = false;
        }
        //---------------------------------------------------------------------------------------------------------------
    }

    void DoAnim(Image starImg)
    {
        // increases the size of the star
        starImg.rectTransform.sizeDelta = new Vector2(150f, 150f);

        // show a gold star
        starImg.sprite = goldenStar;

        // returns the star size to normal using DoTween animation
        RectTransform t = starImg.rectTransform;
        t.DOSizeDelta(new Vector2(100f, 100f), 0.5f, false);

        // audio
        AudioCtrl.instance.KeyFound(starImg.gameObject.transform.position);

        // shows the sparkle effect
        SFXCtrl.instance.ShowBulletSparkle(starImg.gameObject.transform.position);
    }
}
