using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*IMPORTED FROM LESSON.*/
public class SettingPopup : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    public void OnplayMusic(int selector)
    {
        switch (selector)
        {
            case 1:
                Manager.Audio.PlayLevelMusic();
                break;
            case 2:
                Manager.Audio.StopMusic();
                break;
        }
    }


    public void Open()
    {
        camera.enabled = false;
        gameObject.SetActive(true);
        PauseGame();
    }

    public void Close()
    {

        gameObject.SetActive(false);
        UnPauseGame();
        camera.enabled = true;
    }

    public void PauseGame()
    {
        GameEvent.isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;

    }

    public void UnPauseGame()
    {
        GameEvent.isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void OnSoundToggle()
    {
        Manager.Audio.soundMute = !Manager.Audio.soundMute;
    }

    public void OnSoundValue(float volume)
    {
        Manager.Audio.soundVolume = volume;
    }

    public void OnMusicToggle()
    {
        Manager.Audio.musicMute = !Manager.Audio.musicMute;

    }
    public void OnMusicValue(float volume)
    {
        Manager.Audio.musicVolume = volume;
    }

}
