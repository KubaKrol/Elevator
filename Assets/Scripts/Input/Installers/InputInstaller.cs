using UnityEngine;
using Zenject;

namespace Assets.Scripts.Input.Installers {

    /// <summary>
    /// InputInstaller;
    /// </summary>
    public sealed class InputInstaller : MonoInstaller<InputInstaller> {

        #region Public Methods

        public override void InstallBindings() {

            Container.Bind<IGameInput>().To<MouseInput>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<Raycasting>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();


            Container.BindInstance(RaycastingSettings).AsSingle().NonLazy();
        }

        #endregion Public Methods


        #region Inspector Variables

        [SerializeField] private Raycasting.Settings RaycastingSettings;

        #endregion Inspector Variables


        #region Private Types

        #endregion Private Types
    }
}

