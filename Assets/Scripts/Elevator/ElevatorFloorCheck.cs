using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Elevator {

    /// <summary>
    /// Script responsible for keeping track of elevator current floor.
    /// String checking is not efficient - could be changed to type checking.
    /// </summary>

    [Serializable]
    public sealed class ElevatorFloorCheck : MonoBehaviour 
    {
        #region Public Methods

        [Inject]
        public void Construct(
            Elevator elevator) 
        {
            _Elevator = elevator;
        }

        #endregion Public Methods

        #region Unity Methods

        private void OnTriggerEnter(Collider col)
        {
            if (col.tag == "ElevatorDoors")
            {
                _Elevator.CurrentFloor = col.GetComponent<ElevatorDoors>().Floor;
                col.GetComponent<ElevatorDoors>().CabinPresent = true;
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.tag == "ElevatorDoors")
            {
                col.GetComponent<ElevatorDoors>().CabinPresent = false;
            }
        }

        #endregion Unity Methods

        #region Private Variables

        private Elevator _Elevator;

        #endregion Private Variables
    }
}

