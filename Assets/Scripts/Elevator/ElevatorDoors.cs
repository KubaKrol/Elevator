using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using MEC;

namespace Assets.Scripts.Elevator 
{
    /// <summary>
    /// Script responsible for opening and closing the elevator doors.
    ///
    /// "CabinPresent" variable is set from ElevatorFloorCheck (This should be changed, there is no logic for CabinPresent in this script and its wrong - not enough time though).
    /// </summary>

    [Serializable]
    public sealed class ElevatorDoors : MonoBehaviour 
    {

        #region Public Types

        public enum State
        {
            Open,
            Closed
        }

        [Serializable]
        public class Settings
        {
            public AudioClip ElevatorDoorsOpenSound;
            public AudioClip ElevatorDoorsCloseSound;
        }

        #endregion Public Types


        #region Public Variables

        public State CurrentState;

        public int Floor;

        public bool CabinPresent;
        public bool CanClose;

        #endregion Public Variables


        #region Public Methods

        [Inject]
        public void Construct(
            Settings settings,
            Elevator elevator) 
        {
            _Settings = settings;
            _Elevator = elevator;
        }

        [PublicAPI]
        public void Open()
        {
            if (_Elevator.CurrentFloor == Floor && CurrentState == State.Closed)
            {
                Timing.RunCoroutine(OpenDoorsCoroutine());
            }
        }

        [PublicAPI]
        public void Close()
        {
            if (_Elevator.CurrentFloor == Floor && CanClose && CurrentState == State.Open)
            {
                Timing.RunCoroutine(CloseDoorsCoroutine());
            }
        }

        [PublicAPI]
        public void KillCoroutines()
        {
            Timing.KillCoroutines();
        }

        #endregion Public Methods


        #region Unity Methods

        private void Awake()
        {
            _Elevator.OnStop += Open;
            _Elevator.Doors.Add(Floor, this);
            _MyAudio = GetComponent<AudioSource>();
            _MyAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            CurrentState = State.Closed;
            CabinPresent = false;
        }

        #endregion Unity Methods


        #region Private Variables

        private Settings _Settings;
        private Elevator _Elevator;
        private Animator _MyAnimator;
        private AudioSource _MyAudio;

        #endregion Private Variables

        #region Coroutines

        private IEnumerator<float> OpenDoorsCoroutine()
        {
            CurrentState = State.Open;
            _MyAnimator.SetBool("DoorsOpen", true);
            _MyAudio.PlayOneShot(_Settings.ElevatorDoorsOpenSound);
            CanClose = true;
            Close();
            yield break;
        }

        private IEnumerator<float> CloseDoorsCoroutine()
        {
            yield return Timing.WaitForSeconds(3f);
            _MyAnimator.SetBool("DoorsOpen", false);
            yield return Timing.WaitForSeconds(0.4f);
            _MyAudio.PlayOneShot(_Settings.ElevatorDoorsCloseSound);
            yield return Timing.WaitForSeconds(1f);
            CurrentState = State.Closed;
        }

        #endregion Coroutines
    }
}

