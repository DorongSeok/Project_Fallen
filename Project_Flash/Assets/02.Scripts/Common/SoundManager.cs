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
        AddSoundsToList();
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
    public void ClearSFX()
    {
        AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
        audioSource.Stop();


    }
    public void PlayBgmOneShot(string path, Define.Sound type = Define.Sound.Bgm, float pitch = 1.0f)
    {
        AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
        AudioClip oneShotAudioClip = GetOrAddAudioClip(path, type);

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.pitch = pitch;
        audioSource.PlayOneShot(oneShotAudioClip);


        CoroutineHelper.StartCoroutine(PlayBgmOneShotEndChecker(audioSource));
    }
    IEnumerator PlayBgmOneShotEndChecker(AudioSource audioSource)
    {
        while (audioSource.isPlaying)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        audioSource.Play();
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
    
    public AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        // path 폴더 설정해야함
        if(path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }
    
        AudioClip audioClip = null;

        // 오디오 클립에 존재하지 않는다면 추가해줌
        if (_audioClips.TryGetValue(path, out audioClip) == false)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
            _audioClips.Add(path, audioClip);
        }
        // 아니라면 그냥 불러옴
        else
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
    
        if(audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {path}");
        }
    
        return audioClip;
    }
    private void AddSoundsToList()
    {
        // 미리 사운드 리소스들을 리스트에 넣어놔 불러오는데 걸리는 딜레이 방지

        // BGM
        Managers.Sound.GetOrAddAudioClip("BGM/EndingScene_BGM", Define.Sound.Bgm);
        Managers.Sound.GetOrAddAudioClip("BGM/FallingEndBGM", Define.Sound.Bgm);
        Managers.Sound.GetOrAddAudioClip("BGM/LeaderboardScene_BGM", Define.Sound.Bgm);
        Managers.Sound.GetOrAddAudioClip("BGM/LogoScene_BGM_1", Define.Sound.Bgm);
        Managers.Sound.GetOrAddAudioClip("BGM/LogoScene_BGM_2", Define.Sound.Bgm);
        Managers.Sound.GetOrAddAudioClip("BGM/t1_BGM", Define.Sound.Bgm);
        Managers.Sound.GetOrAddAudioClip("BGM/t2_BGM", Define.Sound.Bgm);
        Managers.Sound.GetOrAddAudioClip("BGM/t3_BGM", Define.Sound.Bgm);
        Managers.Sound.GetOrAddAudioClip("BGM/t4_BGM", Define.Sound.Bgm);
        Managers.Sound.GetOrAddAudioClip("BGM/t5_BGM", Define.Sound.Bgm);

        // Effect
        Managers.Sound.GetOrAddAudioClip("Effect/LogoScene_CrashEffect");
        Managers.Sound.GetOrAddAudioClip("Effect/LogoScene_FallingEffect");
        Managers.Sound.GetOrAddAudioClip("Effect/LogoScene_ShakeEffect");
        Managers.Sound.GetOrAddAudioClip("Effect/PlayerChargeMove");
        Managers.Sound.GetOrAddAudioClip("Effect/PlayerNormalMove");
    }
}