using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnCtrl : MonoBehaviour
{
    int levelNumber;           // check
    Button btn;                // button to which this script is bound
    Image btnImg;              // picture of this button
    Text btnText;               // text element
    Transform star1, star2, star3;

    public Sprite lockedBtn;        // picture of a locked button
    public Sprite unlockedBtn;      // picture unlocked
    public string sceneName;        // scene to be loaded


    void Start ()
    {
        // the button name is actually a number, just taking a string and converting it to int
        levelNumber = int.Parse(transform.gameObject.name);

        btn = transform.gameObject.GetComponent<Button>();
        btnImg = btn.GetComponent<Image>();
        btnText = btn.gameObject.transform.GetChild(0).GetComponent<Text>();

        star1 = btn.gameObject.transform.GetChild(1);
        star2 = btn.gameObject.transform.GetChild(2);
        star3 = btn.gameObject.transform.GetChild(3);

        BtnStatus();
    }

    /// <summary>
    /// Unlocks locks the entire button and displays the number of stars won
    /// </summary>
    void BtnStatus()
    {
        // taking lock status and number of stars
        bool unlocked = DataCtrl.instance.isUnlocked(levelNumber);
        int starsAwarded = DataCtrl.instance.getStars(levelNumber);

        if (unlocked)
        {
            if(starsAwarded == 3)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(true);
            }
            if (starsAwarded == 2)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(false);
            }
            if (starsAwarded == 1)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }
            if (starsAwarded == 0)
            {
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }

            btn.onClick.AddListener(LoadScene);
        }
        else
        {
            // displays a locked button image
            btnImg.overrideSprite = lockedBtn;

            // do not display any text
            btnText.text = "";

            // hide 3 stars
            star1.gameObject.SetActive(false);
            star2.gameObject.SetActive(false);
            star3.gameObject.SetActive(false);
        }
    }

    void LoadScene()
    {
        LoadingCtrl.instance.ShowLoading(btnText.text);

        SceneManager.LoadScene(sceneName);
    }
}
