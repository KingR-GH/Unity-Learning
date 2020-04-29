using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestinationStat:GameController
    {
        private bool none;
        public bool ButtonUp { get; set; }
        public bool ButtonDown { get; set; }
        public bool InsideElevator { get; set; }
        public bool None { get => !(ButtonUp || ButtonDown || InsideElevator); set => none = value; }

        public DestinationStat()
        {
            ButtonUp = false;
            ButtonDown = false;
            InsideElevator = false;
            None = true;
        }

    }
}
