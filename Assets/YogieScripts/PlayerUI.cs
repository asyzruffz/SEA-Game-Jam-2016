using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Yogie
{
    public class PlayerUI : MonoBehaviour
    {
        public Image healthBar;
        public Image dead;

        public Text nameText;

        public Player player;

        public EnergyBar energyBar;

        public void Init(Player player, int index)
        {
            this.player = player;

            healthBar.fillAmount = 1;
            string type = "";

            if (player.playerType == Player.PlayerType.HUMAN)
                type = "Human";
            else
                type = "Ghost";

            nameText.text = "Player " + (index + 1) + " (" + type + ")";

            energyBar.Init();
        }
    }
}
