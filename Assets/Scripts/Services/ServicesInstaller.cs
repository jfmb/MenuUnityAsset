using DefaultNamespace;
using Services.EventQueue.Classes.EventData;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;

namespace Services
{
    public class ServicesInstaller : MonoBehaviour
    {
        [SerializeField] private EventId nextSceneEventId;
        [SerializeField] private AllScenes nextScene;
        [SerializeField] private EventQueueInstaller eventQueueInstaller;
        [SerializeField] private EventsInstaller eventsInstaller;
        [SerializeField] private GameService[] servicesToInstall;
        
        private void Awake()
        {
            InstalPermanentDataSaver();
            InstallDeviceAdapters();
            InstallLanguages();

            InstallGameServicesFromGameObjects();
            
            StartNextScene();

            Debug.Log("Services installed");
        }

        private void InstalPermanentDataSaver()
        {
            var playerPrefsPermanentDataAdapter = new PlayerPrefsPermanentDataAdapter();
            ServiceLocator.RegisterService<IPermanentData>(playerPrefsPermanentDataAdapter);
        }

        private void InstallGameServicesFromGameObjects()
        {
            foreach (var newService in servicesToInstall)
            {
                newService.Install();
            }
        }

        private void InstallDeviceAdapters()
        {
//            ServiceLocator.RegisterService(deviceAdaptersInjector);
        }

        private void InstallLanguages()
        {
            // languagesInstaller.Install();
            // ServiceLocator.RegisterService(languagesInstaller);
        }
        
        // private void ReadSettingsJsonFile()
        // {
        //     var deviceFilePath = Path.Combine(ObtainTheDeviceFilesPath(), "settings.json");
        //     var jsonContent = string.Empty;
        //
        //     // MIM-67: if no settings.json file is found, the app uses the productive settings included in the build
        //     if(!File.Exists(deviceFilePath)) 
        //     {
        //         jsonContent = Resources.Load<TextAsset>("settings-production").text;
        //     }
        //     else
        //     {
        //         jsonContent = File.ReadAllText(deviceFilePath);
        //     }
        //
        //     Settings settings = JsonUtility.FromJson<Settings>(jsonContent);
        // }

        // private static string ObtainTheDeviceFilesPath()
        // {
        //     return SystemInfo.deviceType == DeviceType.Desktop ? 
        //         Application.streamingAssetsPath : Application.persistentDataPath;
        // }
        
        private void StartNextScene()
        {
            var args = new IntegerEventData((int)nextScene);
            Debug.Log("before enqueue");
            ServiceLocator.GetService<EventQueue.EventQueue>().EnqueueEvent(nextSceneEventId, args);
        }
    }
}