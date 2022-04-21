using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Options : MonoBehaviour
{
    Resolution[] resolutions;
    [SerializeField] TMPro.TMP_Dropdown resolutionDropdown;
    private void Start()
    {
        resolutionDropdown.ClearOptions();
        int defaultResolution = 0;
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        for (int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
                defaultResolution = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = defaultResolution;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution(int resol)
    {
        Screen.SetResolution(resolutions[resol].width, resolutions[resol].height, Screen.fullScreen);
    }
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetFullscreen (bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
