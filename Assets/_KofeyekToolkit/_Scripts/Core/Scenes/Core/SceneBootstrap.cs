using UnityEngine;

namespace KofeyekToolkit.Core.Scenes.Core
{
    /// <summary>
    /// Базовый компонент инициализации, получающий аргументы после загрузки сцены.
    /// </summary>
    public abstract class SceneBootstrap : MonoBehaviour
    {
        internal void Initialize(ISceneArgs sceneArgs)
        {
            OnInitialize(sceneArgs);
        }
        
        protected abstract void OnInitialize(ISceneArgs sceneArgs);
    }
}