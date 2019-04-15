using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Elevator {

    /// <summary>
    /// This script holds references to all of the elevator doors and buttons using HashSet and Dictionaries.
    /// It's only responsibility is to calculate the elevator logic.
    /// </summary>
    [Serializable]
    public sealed class Elevator : ITickable, IInitializable
    {

        #region Public Types

        public enum State
        {
            Idle,
            Stop,
            GoingUp,
            GoingDown
        }

        [Serializable]
        public class Settings
        {
            public int NumberOfFloors;

            [Range(2f, 5f)]
            public float ElevatorSpeed = 2f;
        }

        #endregion Public Types


        #region Public Variables

        public State CurrentState;

        public HashSet<int> FloorsToVisit = new HashSet<int>();

        public Dictionary<int, ElevatorDoors> Doors = new Dictionary<int, ElevatorDoors>();
        public Dictionary<int, SetElevatorButton> SetButtons = new Dictionary<int, SetElevatorButton>();
        public Dictionary<int, GetElevatorButton> GetButtonsUp = new Dictionary<int, GetElevatorButton>();
        public Dictionary<int, GetElevatorButton> GetButtonsDown = new Dictionary<int, GetElevatorButton>();

        public float ElevatorSpeed { get; set; }
        public int CurrentFloor { get; set; }

        [PublicAPI]
        public event Action OnStop;

        [PublicAPI]
        public event Action OnMove;

        #endregion Public Variables


        #region Public Methods

        [UsedImplicitly]
        public Elevator(
            Settings settings) 
        {
            _Settings = settings;
        }

        [UsedImplicitly]
        public void Initialize()
        {
            CurrentState = State.Idle;
            CurrentFloor = 0;

            ElevatorSpeed = _Settings.ElevatorSpeed;
        }

        [UsedImplicitly]
        public void Tick()
        {
            ElevatorLogic();
        }

        #endregion Public Methods


        #region Private Variables

        private readonly Settings _Settings;

        private int _PreviousDirection; //1-Up, 0-Down;
        private int _HigherFloors;
        private int _LowerFloors;

        #endregion Private Variables

        #region Private Methods

        private void ElevatorLogic() {
            _HigherFloors = 0;
            _LowerFloors = 0;

            if (FloorsToVisit.Count == 0) {
                CurrentState = State.Idle;
            } else {
                CheckFloorsToVisit();

                for (var i = 0; i < _Settings.NumberOfFloors; i++) {

                    if (FloorsToVisit.Contains(i)) {

                        if (i == CurrentFloor && Doors[CurrentFloor].CabinPresent) {
                            CheckStopConditions();
                        }

                        CheckMoveConditions(i);
                    }
                }
            }
        }

        private void CheckFloorsToVisit()
        {
            for (var i = 0; i < _Settings.NumberOfFloors; i++) {
                if (FloorsToVisit.Contains(i) && i > CurrentFloor) _HigherFloors++;
                if (FloorsToVisit.Contains(i) && i < CurrentFloor) _LowerFloors++;
            }
        }

        private void CheckStopConditions()
        {
            if ((CurrentState == State.GoingUp || CurrentState == State.Idle || CurrentState == State.Stop) &&
                _HigherFloors == 0)
            {
                Stop();
            }

            if ((CurrentState == State.GoingDown || CurrentState == State.Idle || CurrentState == State.Stop) &&
                _LowerFloors == 0)
            {
                Stop();
            } 
            else if ((GetButtonsDown[CurrentFloor].IsActive || SetButtons[CurrentFloor].IsActive) &&
                       CurrentState == State.GoingDown)
            {
                Stop();
            } 
            else if ((GetButtonsUp[CurrentFloor].IsActive || SetButtons[CurrentFloor].IsActive) &&
                       CurrentState == State.GoingUp)
            {
                Stop();
            }
        }

        private void Stop() {
            CurrentState = State.Stop;
            OnStop?.Invoke();
            FloorsToVisit.Remove(CurrentFloor);
        }

        private void CheckMoveConditions(int i)
        {
            if (i > CurrentFloor &&
                (CurrentState == State.Stop || CurrentState == State.Idle) &&
                Doors[CurrentFloor].CurrentState == ElevatorDoors.State.Closed) {
                OnMove?.Invoke();

                if (_PreviousDirection == 0 && _LowerFloors > 0) {
                    CurrentState = State.GoingDown;
                    _PreviousDirection = 0;
                } else {
                    CurrentState = State.GoingUp;
                    _PreviousDirection = 1;
                }
            }

            if (i < CurrentFloor &&
                (CurrentState == State.Stop || CurrentState == State.Idle) &&
                Doors[CurrentFloor].CurrentState == ElevatorDoors.State.Closed) {
                OnMove?.Invoke();

                if (_PreviousDirection == 1 && _HigherFloors > 0) {
                    CurrentState = State.GoingUp;
                    _PreviousDirection = 1;
                } else {
                    CurrentState = State.GoingDown;
                    _PreviousDirection = 0;
                }
            }
        }

        #endregion Private Methods
    }
}

