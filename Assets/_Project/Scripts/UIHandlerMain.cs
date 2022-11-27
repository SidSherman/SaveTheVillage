using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UIHandlerMain : MonoBehaviour
{
    [SerializeField] protected Image _soundBttnImage;
    [SerializeField] protected Sprite _soundBttnActive;
    [SerializeField] protected Sprite _soundBttnInactive;
    [SerializeField] protected AudioClip _clickSound;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected AudioListener _listener;

    public void SoundOnOff()
    {
        if(_listener.enabled == true)
        {
            _listener.enabled = false;
            _soundBttnImage.sprite = _soundBttnInactive;
        }
        else
        {
            _soundBttnImage.sprite = _soundBttnActive;
            _listener.enabled = true;
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }


}

