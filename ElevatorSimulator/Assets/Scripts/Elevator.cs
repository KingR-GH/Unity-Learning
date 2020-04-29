using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public enum ElevatorStat
    {
        movingUp,
        movingDown,
        upStopping,
        downStopping,
        stopping
    }

    public class Elevator:GameController
    {
        public int elevatorFloor;   //电梯所在层数
        protected int elevatorId;   //电梯号
        public int targetFloor; //目标楼层
        protected ElevatorStat elevatorStat;//电梯运动状态
        public DestinationStat[] destinationArray;//终点楼层状态数组


        void Start()
        {
            elevatorFloor = 1;
            elevatorStat = ElevatorStat.stopping;
            destinationArray = new DestinationStat[21] 
            { 
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
                gameObject.AddComponent<DestinationStat>(),
            };

        }
        public virtual void ElevatorMove()
        {
            if (elevatorStat == ElevatorStat.stopping)
            {
                for (int i = 0; i < 21; i++)
                {
                    if (!destinationArray[i].None)
                    {
                        targetFloor = i;
                        if (targetFloor > elevatorFloor)
                        {
                            elevatorStat = ElevatorStat.movingUp;
                        }
                        else if (targetFloor < elevatorFloor)
                        {
                            elevatorStat = ElevatorStat.movingDown;
                        }
                    }
                }
            }
            else if (elevatorStat == ElevatorStat.movingUp)
            {
                bool goingUpToButtonDown = true;
                for (int i = elevatorFloor; i < 21; i++)
                {
                    if (destinationArray[i].ButtonUp|| destinationArray[i].InsideElevator)
                    {
                        targetFloor = i;
                        goingUpToButtonDown = false;
                        break;
                    }
                }
                if (goingUpToButtonDown)
                {
                    for (int i = 20; i > elevatorFloor; i--)
                    {
                        if (destinationArray[i].ButtonDown)
                        {
                            targetFloor = i;
                            break;
                        }
                    }
                }

                if (targetFloor > elevatorFloor)
                {
                    timeCount += 1;
                    if (timeCount == timeMove)
                    {
                        ElevatorMoveUp();
                        timeCount = 0;
                    }
                }
                if (targetFloor == elevatorFloor)
                {
                    destinationArray[elevatorFloor].ButtonUp = false;
                    destinationArray[elevatorFloor].InsideElevator = false;
                    elevatorStat = ElevatorStat.upStopping;
                }
            }
            else if (elevatorStat == ElevatorStat.movingDown)
            {
                bool goingDownToButtonUp = true;
                for (int i = elevatorFloor; i >= 0; i--)
                {
                    if (destinationArray[i].ButtonDown || destinationArray[i].InsideElevator)
                    {
                        targetFloor = i;
                        goingDownToButtonUp = false;
                        break;
                    }
                }
                if (goingDownToButtonUp)
                {
                    for (int i = 0; i < elevatorFloor; i++)
                    {
                        if (destinationArray[i].ButtonUp)
                        {
                            targetFloor = i;
                            break;
                        }
                    }
                }
                
                if (targetFloor < elevatorFloor)
                {
                    timeCount += 1;
                    if (timeCount == timeMove)
                    {
                        ElevatorMoveDown();
                        timeCount = 0;
                    }
                }
                if (targetFloor == elevatorFloor)
                {
                    destinationArray[elevatorFloor].ButtonDown = false;
                    destinationArray[elevatorFloor].InsideElevator = false;
                    elevatorStat = ElevatorStat.downStopping;
                }
            }
            else if(elevatorStat == ElevatorStat.upStopping)
            {
                timeCount += 1;
                if (timeCount == timeStay)
                {
                    bool isDestinationFlag = true;
                    for (int i = elevatorFloor; i < 20; i++)
                    {
                        if (destinationArray[i].ButtonUp || destinationArray[i].InsideElevator)
                        {
                            targetFloor = i;
                            elevatorStat = ElevatorStat.movingUp;
                            isDestinationFlag = false;
                            break;
                        }
                    }
                    if (isDestinationFlag)
                    {
                        for (int i = 20; i > elevatorFloor; i--)
                        {
                            if (destinationArray[i].ButtonDown)
                            {
                                targetFloor = i;
                                elevatorStat = ElevatorStat.movingUp;
                                isDestinationFlag = false;
                                break;
                            }
                        }
                        if (isDestinationFlag)
                        {
                            elevatorStat = ElevatorStat.stopping;
                        }
                    }
                    timeCount = 0;
                }
            }
            else if (elevatorStat == ElevatorStat.downStopping)
            {
                timeCount += 1;
                if (timeCount == timeStay)
                {
                    bool isDestinationFlag = true;
                    for (int i = elevatorFloor; i > 0; i--)
                    {
                        if (destinationArray[i].ButtonDown || destinationArray[i].InsideElevator)
                        {
                            targetFloor = i;
                            elevatorStat = ElevatorStat.movingDown;
                            isDestinationFlag = false;
                            break;
                        }
                    }
                    if (isDestinationFlag)
                    {
                        for (int i = 0; i < elevatorFloor; i++)
                        {
                            if (destinationArray[i].ButtonUp)
                            {
                                targetFloor = i;
                                elevatorStat = ElevatorStat.movingDown;
                                isDestinationFlag = false;
                                break;
                            }
                        }
                        if (isDestinationFlag)
                        {
                            elevatorStat = ElevatorStat.stopping;
                        }
                    }
                    timeCount = 0;
                }
            }
        }

        public virtual void ElevatorMoveUp()
        {
            Vector3 newPosition = new Vector3(
            this.transform.position.x,
            this.transform.position.y + 2,
            this.transform.position.z);

            this.GetComponent<Transform>().position = newPosition;
            elevatorFloor += 1;
        }
        public virtual void ElevatorMoveDown()
        {
            Vector3 newPosition = new Vector3(
            this.transform.position.x,
            this.transform.position.y - 2,
            this.transform.position.z);

            this.GetComponent<Transform>().position = newPosition;
            elevatorFloor -= 1;
        }
    }
}
