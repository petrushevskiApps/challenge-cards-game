using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PetrushevskiApps.WhosGame.Scripts.Di
{
    [CreateAssetMenu(
        fileName = "ScriptableObjectInstaller",
        menuName = "Configuration/ScriptableObjectInstaller")]
    public class MainScriptableObjectInstaller : ScriptableObjectInstaller<MainScriptableObjectInstaller>
    {
        [SerializeField]
        private List<ScriptableObject> _scriptableObjectsToBind;

        public override void InstallBindings()
        {
            foreach (var scriptableObject in _scriptableObjectsToBind)
            {
                Container
                    .BindInterfacesTo(scriptableObject.GetType())
                    .FromInstance(scriptableObject);
                Container.QueueForInject(scriptableObject);
            }
        }
    }
}