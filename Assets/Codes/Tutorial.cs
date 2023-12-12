using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private int panel = 0;
    public Text TutorialOne;
    void Start()
    {
        TutorialOne.text = "Don't Let Tiger \nFall from the island!";
    }
    public void Next()
    {
        panel++;
    }
    // Update is called once per frame
    void Update()
    {
        if(panel == 1)
        {
            TutorialOne.text = "Tiger will go to the spot\n which you tapped";
        }
        if(panel == 2)
        {
            TutorialOne.text = "Island gets \nsmaller gradually";
        }
            if(panel == 3)
        {
            TutorialOne.text = "Use Green button \nto make island bigger\nUse Yellow button to make island\n move slower";
        }
            if(panel == 4)
        {
            TutorialOne.text = "The number of ability usages is limited\nUse them wisely!";
        }
            if(panel == 5)
        {
            TutorialOne.text = "Survive to get coins\nYou can use them to increase number of\n ability usages.";
        }
            if(panel == 6)
        {
            SceneManager.LoadScene("Game");
        }
        
    }
}
