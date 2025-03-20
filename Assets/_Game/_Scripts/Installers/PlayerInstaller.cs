using TestTask.PlayerComponents;
using UnityEngine;
using Zenject;

namespace TestTask.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _camera;
        [SerializeField] private GameObject _playerCanvas;

        public override void InstallBindings()
        {
            var cameraPrefab = Container.InstantiatePrefabForComponent<PlayerCamera>(_camera);
            Container.Bind<PlayerCamera>().FromInstance(cameraPrefab);

            var playerCanvasPrefab = Container.InstantiatePrefab(_playerCanvas);
            Container.Bind<PlayerTouchInput>().FromInstance(playerCanvasPrefab.transform.GetChild(0).GetComponent<PlayerTouchInput>());
            Container.Bind<FixedJoystick>().FromInstance(playerCanvasPrefab.transform.GetChild(1).GetComponent<FixedJoystick>());
            Container.Bind<PlayerDropUI>().FromInstance(playerCanvasPrefab.transform.GetChild(2).GetComponent<PlayerDropUI>());
            
            var playerPrefab = Container.InstantiatePrefab(_player); // B
            Container.QueueForInject(playerPrefab);
        }
    }
}