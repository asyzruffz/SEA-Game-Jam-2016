using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Yogie;

public class GameManager : MonoBehaviour {

    public Light[] Lights;
    public int LightsCount;

    public Player[] players;
    public Player[] ghosts;

    public float modifier;

    public GameObject meter;

    bool gameEndHuman = false;
    bool gameEndGhost = false;

    public Text winText;
    public GameObject goPanel;

    void Awake()
    {
        LightsCount = Lights.Length;
    }

    public void toggleLights(int lightIndex)
    {
        Lights[lightIndex].enabled = !Lights[lightIndex].enabled;
        
    }

	// Use this for initialization
	void Start () {
        meterRot = new Vector3(0, 0, 0);
    }

    public void replay()
    {
        SceneManager.LoadScene("Main");
    }

    Vector3 meterRot;
    int lightsOn;
    float ratio;
    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    // Update is called once per frame
    void Update () {
        lightsOn = 0;

        for ( int i=0; i< LightsCount; i++)
        {
            if( Lights[i].enabled)
                lightsOn++;
        }

        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;

            ratio = (float)lightsOn / (float)LightsCount;
            //Debug.Log("Light/Total: " + ratio);

            if (ratio > 0.5f)
            {
                for( int i=0; i<players.Length;i++)
                {
                    if( !players[i].isDead )
                        players[i].damage(-ratio * modifier);
                }

                for (int i = 0; i < ghosts.Length; i++)
                {
                    if (!ghosts[i].isDead)
                        ghosts[i].damage(ratio * modifier);
                }

                meter.transform.localRotation = Quaternion.Euler(0,0, (ratio-0.5f) * 150);
            }
            else if ( ratio < 0.5f)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    if (!players[i].isDead)
                        players[i].damage((1.0f - ratio) * modifier);
                }

                for (int i = 0; i < ghosts.Length; i++)
                {
                    if (!ghosts[i].isDead)
                        ghosts[i].damage(-(1.0f - ratio) * modifier);
                }

                meter.transform.localRotation = Quaternion.Euler(0, 0, ((0.5f - ratio) * -150));
            }
            else
            {
                meter.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            for (int i = 0; i < players.Length; i++)
            {
                if (!players[i].isDead)
                {
                    players[i].playerUI.healthBar.fillAmount = players[i].playerHealth / 100f;
                }

                gameEndHuman = true && players[i].isDead;
            }

            for (int i = 0; i < ghosts.Length; i++)
            {
                if (!ghosts[i].isDead)
                    ghosts[i].playerUI.healthBar.fillAmount = ghosts[i].playerHealth / 100f;

                gameEndGhost = true && ghosts[i].isDead;
            }
        }



            if (players[0].isDead && players[1].isDead)
            {
                winText.text = "Human Wins!";
                goPanel.SetActive(true);
            }
               

        if (ghosts[0].isDead && ghosts[1].isDead)
        {
            winText.text = "Ghost Wins!";
            goPanel.SetActive(true);
        }

    }
}
