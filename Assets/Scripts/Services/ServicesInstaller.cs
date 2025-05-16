using System;
using System.IO;
using DefaultNamespace;
using ScriptableObjects.Scripts.Ids;
using Services.EventQueue.Classes.EventData;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;

[Serializable]
public class Settings
{
    public string Context;
    public string KeycloakBaseURL;
    public string ServerURL;
}

namespace Services
{
    public class ServicesInstaller : MonoBehaviour
    {
        [SerializeField] private EventId nextSceneEventId;
        [SerializeField] private AllScenes nextScene;
        [SerializeField] private EventQueueInstaller eventQueueInstaller;
        [SerializeField] private EventsInstaller eventsInstaller;

        private void Awake()
        {
            InstallDeviceAdapters();
            InstallLanguages();

            InstallGameInfo();

            InstallEventQueue();
            StartNextScene();

            Debug.Log("Debug: services installed");
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

        private void InstallGameInfo()
        {
            var gameInfo = new GameInfo();

            ServiceLocator.RegisterService(gameInfo);
            
//            ReadSettingsJsonFile();
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

        private void InstallEventQueue()
        {
            eventQueueInstaller.Install();
            eventsInstaller.Install();
        }
        
        private void StartNextScene()
        {
            var args = new IntegerEventData((int)nextScene);
            Debug.Log("before enqueue");
            ServiceLocator.GetService<EventQueue.EventQueue>().EnqueueEvent(nextSceneEventId, args);
        }
    }
}