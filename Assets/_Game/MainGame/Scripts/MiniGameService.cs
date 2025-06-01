using System.Threading.Tasks;
using Naninovel;
using UnityEngine;

namespace Game
{
    [InitializeAtRuntime]
    public class MiniGameService : IEngineService
    {
        private GameObject _miniGamePrefab;

        public async Task<bool> PlayMiniGameAsync()
        {
            var instance = Object.Instantiate(_miniGamePrefab) as GameObject;
            var controller = instance.GetComponent<MiniGameController>();

            bool isSuccess = await controller.PlayGameAsync();

            Object.Destroy(instance);
            return isSuccess;
        }

        public UniTask InitializeServiceAsync()
        {
            return LoadPrefabAsync();
        }

        public void ResetService()
        {
            // Reset logic if needed
        }

        public void DestroyService()
        {
            _miniGamePrefab = null;
        }

        private async UniTask LoadPrefabAsync()
        {
            _miniGamePrefab = await Resources.LoadAsync<GameObject>("MiniGame") as GameObject;
        }
    }
}
