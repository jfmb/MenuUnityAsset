using System;
using System.IO;
using ScriptableObjects.Scripts.Ids;
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
        [SerializeField] private SceneId nextSceneId;
        [SerializeField] private EventQueueInstaller eventQueueInstaller;
        [SerializeField] private EventsInstaller eventsInstaller;
        [SerializeField] private bool deletePlayerPrefsBefore;

        private void Awake()
        {
            ResetPlayerPrefs();
            InstallDeviceAdapters();
            InstallLanguages();
//            InstallAppInfo();
            InstallEventQueue();
            StartNextScene();

            Debug.Log("Debug: services installed");
        }

        private void ResetPlayerPrefs()
        {
            if (!deletePlayerPrefsBefore)
            {
                return;
            }
        
            PlayerPrefs.DeleteAll();
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

        private void InstallAppInfo()
        {            
            // var appInfo = new AppInfo.AppInfo
            // {
            //     // MIM-64 use current App version
            //     BuildNumber = Application.version
            // };
            // ServiceLocator.RegisterService(appInfo);
            
            ReadSettingsJsonFile();
        }

        private void ReadSettingsJsonFile()
        {
            var deviceFilePath = Path.Combine(ObtainTheDeviceFilesPath(), "settings.json");
            var jsonContent = string.Empty;

            // MIM-67: if no settings.json file is found, the app uses the productive settings included in the build
            if(!File.Exists(deviceFilePath)) 
            {
                jsonContent = Resources.Load<TextAsset>("settings-production").text;
            }
            else
            {
                jsonContent = File.ReadAllText(deviceFilePath);
            }

            Settings settings = JsonUtility.FromJson<Settings>(jsonContent);
        }

        private static string ObtainTheDeviceFilesPath()
        {
            return SystemInfo.deviceType == DeviceType.Desktop ? 
                Application.streamingAssetsPath : Application.persistentDataPath;
        }

        private void InstallEventQueue()
        {
            eventQueueInstaller.Install();
            eventsInstaller.Install();
        }
        
        private void StartNextScene()
        {
            var args = new StringEventData(nextSceneId.Id);
            Debug.Log("before enqueue");
            ServiceLocator.GetService<EventQueue.EventQueue>().EnqueueEvent(nextSceneEventId, args);
        }
    }
}