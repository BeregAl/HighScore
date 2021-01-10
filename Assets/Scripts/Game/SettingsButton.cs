using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public void OpenSettings()
    {
        GameManager.instance.audioManager.PlayClick();
        GameManager.instance.uiManager.settingsDialog.ShowDialog();
    }
}
