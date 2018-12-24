using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Yogie
{
    public class GameController : MonoBehaviour
    {
        public Player[] players;

        public Light[] allLights;

        void Start()
        {
            if (OverlayTransition.core != null)
                OverlayTransition.core.FadeOut(null);

            InitGame();

        }

        [ContextMenu("Init Game")]
        public void InitGame()
        {
            RandomizeRoomLights();

            InitPlayers();
        }

        public void InitPlayers()
        {
            //players = new Player[totalPlayers];

            int ln = players.Length;

            for (int i = 0; i < ln; i++)
            {
                Player curPlayer = players[i];

                if (curPlayer != null)
                {
                    curPlayer.ResetProperties(true);
                    curPlayer.playerUI.Init(curPlayer, i);

                    //Randomize The Positions based on Lights
                    Vector3 positions = Vector3.zero;
                    bool found = false;

                    do
                    {
                        found = false;

                        int rand = Random.Range(0, allLights.Length);

                        if (curPlayer.playerType == Player.PlayerType.HUMAN)
                        {
                            if (allLights[rand].enabled)
                            {
                                found = true;
                                positions = allLights[rand].transform.position;
                                positions.y = 0;
                            }
                        }
                        else
                        {
                            if (!allLights[rand].enabled)
                            {
                                found = true;
                                positions = allLights[rand].transform.position;
                                positions.y = 0;
                            }
                        }
                    }
                    while (!found);

                    curPlayer.transform.position = positions;
                }
            }
        }

        public void RandomizeRoomLights()
        {
            int totalRoom = allLights.Length;

            int half = Mathf.RoundToInt(totalRoom / 2);

            List<int> roomIndex = new List<int>();

            do
            {
                int randRoom = Random.Range(0, totalRoom);

                bool exist = false;

                int rLn = roomIndex.Count;

                for (int r = 0; r < rLn; r++)
                {
                    if (randRoom == roomIndex[r])
                    {
                        exist = true;
                        break;
                    }
                }

                if (!exist)
                {
                    roomIndex.Add(randRoom);
                }
            }
            while (roomIndex.Count < half);

            int roomIndexLn = roomIndex.Count;

            for (int i = 0; i < totalRoom; i++)
            {
                allLights[i].enabled = false;
            }

            for (int i = 0; i < totalRoom; i++)
            {
                for (int j = 0; j < roomIndexLn; j++)
                {
                    if (i == roomIndex[j])
                    {
                        allLights[i].enabled = true;
                    }
                }
            }
        }

        public int GetTotalLightON()
        {
            int count = 0;

            int ln = allLights.Length;

            for (int i = 0; i < ln; i++)
            {
                if (allLights[i].enabled)
                    count++;
            }

            return count;
        }

        public int GetTotalLightOFF()
        {
            int count = 0;

            int ln = allLights.Length;

            for (int i = 0; i < ln; i++)
            {
                if (!allLights[i].enabled)
                    count++;
            }

            return count;
        }

        public float GetTotalLightNormalized()
        {
            float totalLightOn = (float)GetTotalLightON();

            return totalLightOn / (float)allLights.Length;
        }

        void FixedUpdate()
        {

        }

        void Update()
        {
            for (int j = 1; j < 12; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (Input.GetKeyDown("joystick "+j+" button " + i))
                    {
                        print("joystick "+j+" button " + i);
                    }
                }
            }
           
        }
    }
}
