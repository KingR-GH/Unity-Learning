using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public enum ButtonType
    {
        up,
        down,
        inside
    }
    public class Button : MonoBehaviour
    {
        public int buttonFloor;
        public string elevatorWho;
        public ButtonType buttonType;

        public Button() { }

        public Button(int buttonFloor, string elevatorWho, ButtonType buttonType)
        {
            this.buttonFloor = buttonFloor;
            this.elevatorWho = elevatorWho;
            this.buttonType = buttonType;
        }

        private void OnMouseDown()
        {
            ButtonCliked();
        }
        public void ButtonCliked()
        {
            GameObject gameObject = GameObject.Find(elevatorWho);
            DestinationStat[] destinationArray = gameObject.GetComponent<Elevator>().destinationArray;
            if (buttonType == ButtonType.up)
            {
                destinationArray[buttonFloor].ButtonUp = true;
            }
            else if (buttonType == ButtonType.down)
            {
                destinationArray[buttonFloor].ButtonDown = true;
            }
            else if (buttonType == ButtonType.inside)
            {
                destinationArray[buttonFloor].InsideElevator = true;
            }
        }
    }
}
