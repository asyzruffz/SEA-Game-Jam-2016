using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Yogie
{
    public class EnergyBar : MonoBehaviour
    {
        public Color activeSlotColor = Color.white;
        public Color deactiveSlotColor = Color.white;

        public Image[] slots;

        public Text chargeText;

        public int currentChargeAmount;
        public int currentCollectedPerCharge;

        public void Init()
        {
            currentChargeAmount = 0;
            currentCollectedPerCharge = 0;

            UpdateUI();
        }

        public bool UseCharge(int amount = 1)
        {
            if (currentChargeAmount >= amount)
            {
                currentChargeAmount -= amount;

                UpdateUI();

                return true;
            }

            return false;
        }

        public void AddCollectedPerCharge(int amount = 1)
        {
            currentCollectedPerCharge++;

            if (currentCollectedPerCharge >= slots.Length)
            {
                currentCollectedPerCharge = 0;
                currentChargeAmount++;
            }

            UpdateUI();
        }

        void UpdateUI()
        {
            int ln = slots.Length;

            for (int i = 0; i < ln; i++)
            {
                if (i < currentCollectedPerCharge)
                {
                    slots[i].color = activeSlotColor;
                }
                else
                {
                    slots[i].color = deactiveSlotColor;
                }
            }

            chargeText.text = currentChargeAmount.ToString();
        }
    }
}
