using UnityEngine;
using Zenject;

namespace Assets.Scripts.Elevator.Installers
{
    /// <summary>
    /// Elevator Zenject Installer;
    /// </summary>

    public sealed class ElevatorInstaller : MonoInstaller<ElevatorInstaller> {

        #region Public Methods

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Elevator>().AsSingle().NonLazy();

            Container.BindInstance(ElevatorSettings).AsSingle().NonLazy();
            Container.BindInstance(ElevatorCabinSettings).AsSingle().NonLazy();
            Container.BindInstance(ElevatorDoorsSettings).AsSingle().NonLazy();
            Container.BindInstance(GetElevatorButtonSettings).AsSingle().NonLazy();
            Container.BindInstance(SetElevatorButtonSettings).AsSingle().NonLazy();
            Container.BindInstance(OpenDoorsButtonSettings).AsSingle().NonLazy();
        }

        #endregion Public Methods


        #region Inspector Variables

        [SerializeField] private Elevator.Settings ElevatorSettings;
        [SerializeField] private ElevatorCabin.Settings ElevatorCabinSettings;
        [SerializeField] private ElevatorDoors.Settings ElevatorDoorsSettings;
        [SerializeField] private GetElevatorButton.Settings GetElevatorButtonSettings;
        [SerializeField] private SetElevatorButton.Settings SetElevatorButtonSettings;
        [SerializeField] private OpenDoorsButton.Settings OpenDoorsButtonSettings;


        #endregion Inspector Variables
    }
}

