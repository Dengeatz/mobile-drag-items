using System.Collections;
using TestTask.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TestTask.Root
{
    public class GameHandler : MonoBehaviour
    {
        private static GameHandler _instance;
        private Coroutines _coroutines;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate -= 1;
            QualitySettings.vSyncCount = 0;
            _instance = new GameHandler();
            _instance.RunGame();
        }

        public static GameHandler GetInstance()
        {
            return _instance;
        }

        private GameHandler()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);
        }

        private void RunGame()
        {
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.BOOT || sceneName == Scenes.GAMEPLAY)
            {
                _coroutines.StartCoroutine(BootToGameplay());
            }
        }

        private IEnumerator BootToGameplay()
        {
            yield return new WaitForEndOfFrame();

            yield return SceneManager.LoadSceneAsync(Scenes.GAMEPLAY);

            var sceneContext = Object.FindAnyObjectByType<SceneContext>();
            sceneContext.Run();
        }
    }
}