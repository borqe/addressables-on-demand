using System.Collections.Generic;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class CustomHostingService : IHostingService
{
    public List<string> HostingServiceContentRoots { get; }

    public Dictionary<string, string> ProfileVariables { get; }

    public bool IsHostingServiceRunning { get; }

    public ILogger Logger { get; set; }
    public string DescriptiveName { get; set; }
    public int InstanceId { get; set; }

    public string EvaluateProfileString(string key)
    {
        return "";
    }

    public void OnAfterDeserialize(KeyDataStore dataStore)
    {
        return;
    }

    public void OnBeforeSerialize(KeyDataStore dataStore)
    {
        return;
    }

    public void OnGUI()
    {
        return;
    }

    public void StartHostingService()
    {
        return;
    }

    public void StopHostingService()
    {
        return;
    }
}
