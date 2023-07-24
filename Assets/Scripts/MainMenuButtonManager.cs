using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Mirror;

public class MainMenuButtonManager : MonoBehaviour {
    public PaperWarsNetworkManager NetworkManager;
    public GameObject MainMenuCanvas;
    public GameObject SettingsCanvas;

    public void Play() {
        NetworkManager.StartClient();
    }

    public void Settings() {
        MainMenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }

    public void Quit() {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit(0);
#endif
    }
}