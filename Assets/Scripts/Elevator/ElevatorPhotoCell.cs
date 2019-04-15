using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Elevator {

    /// <summary>
    /// Elevator doors photoCell, it's not letting the doors close when a player is between the doors
    /// 
    /// It's not working as it should be according to the PDF describing the task - There should be some kind of blend between the animations
    /// so the doors could open smoothly whenever player steps inside in the middle of closing animation.
    /// Unfortunately i'm not that familiar with animations and didn't have enough time to make it work properly.
    /// </summary>

    [Serializable]
    public sealed class ElevatorPhotoCell : MonoBehaviour
    {
        #region Public Methods

        [Inject]
        public void Construct(
            Elevator elevator) {
            _Elevator = elevator;
        }

        #endregion Public Methods

        #region Unity Methods

        private void OnTriggerStay(Collider col)
        {
            if (col.tag == "Player")
            {
                _Elevator.Doors[_Elevator.CurrentFloor].CanClose = false;
                _Elevator.Doors[_Elevator.CurrentFloor].KillCoroutines();
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.tag == "Player")
            {
                _Elevator.Doors[_Elevator.CurrentFloor].CanClose = true;
                _Elevator.Doors[_Elevator.CurrentFloor].Close();
            }
        }

        #endregion Unity Methods

        #region Private Variables

        private Elevator _Elevator;

        #endregion Private Variables
    }
}

