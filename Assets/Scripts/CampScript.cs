using UnityEngine;
using System.Collections;

public class CampScript : MonoBehaviour {

    public GUIText energy;
    public GUIText question;
    private float timer;

    // Use this for initialization
    void Start () {
        energy.text = "Speed = "+ SavedVars.Instance.speed;
        question.text = "";
        timer = 2;
    }
	
    // Update is called once per frame
    void FixedUpdate () {

        timer = timer - Time.deltaTime;

        if(timer <= 0.0)
        {
            question.text = "Press SPACE to proceed forward to next level";
            if (SavedVars.Instance.speed < 8)
            {
                SavedVars.Instance.speed += Time.deltaTime / 10;
                energy.text = "Speed = "+ SavedVars.Instance.speed + "\nScore = " + SavedVars.Instance.score.ToString();
                SavedVars.Instance.score -= Time.deltaTime * 1;
            }
            else
            {
                            
                energy.text = "Speed = Max\nScore = " + SavedVars.Instance.score.ToString();
            }
            if (Input.GetKeyDown(KeyCode.Space))
                Application.LoadLevel(SavedVars.Instance.nextLevel);
            //if(user input is return)
            //	timer = 4;
            //else continue to next level
        }

    }
}
