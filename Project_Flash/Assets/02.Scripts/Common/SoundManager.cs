using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        AudioMixer audioMixer = Resources.Load<AudioMixer>("Sounds/MyMixer");
        AudioMixerGroup[] audioMixerGroups = audioMixer.FindMatchingGroups("Master");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);
    
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
    
            _audioSources[(int)Define.Sound.Bgm].loop = true;
            _audioSources[(int)Define.Sound.Bgm].outputAudioMixerGroup = audioMixerGroups[1];
            _audioSources[(int)Define.Sound.Effect].outputAudioMixerGroup = audioMixerGroups[2];
        }
    }
    
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
    
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
        {
            return;
        }
    
        // BGM 재생
        if(type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
    
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        // 효과음 재생
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }
    
    private AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        // path 폴더 설정해야함
        if(path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }
    
        AudioClip audioClip = null;
    
        // 배경음악 클립 붙이기
        if(type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        // 효과음 클립 붙이기
        else
        {
            if(_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }
    
        if(audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {path}");
        }
    
        return audioClip;
    }
}