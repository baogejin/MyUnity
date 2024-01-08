using System.Collections;
using UnityEngine;
using YooAsset;

public class AudioManager
{
    private AudioSource _audioSource;
    private AssetHandle _bgmHandle;
    public float BgmVolume
    {
        get
        {
            if (_audioSource == null)
                return 0;
            return _audioSource.volume;
        }
        set
        {
            _audioSource.volume = value;
        }
    }
    public float EffectVolume;

    public AudioManager(AudioSource bgmSource)
    {
        _audioSource = bgmSource;
        BgmVolume = _audioSource.volume;
        EffectVolume = _audioSource.volume;
    }

    public void PlayBgm(string fileName)
    {
        Game.StartCoroutine(DoPlayBgm(fileName));
    }

    private IEnumerator DoPlayBgm(string fileName)
    {
        if (_bgmHandle != null)
        {
            _bgmHandle.Release();
            _bgmHandle = null;
        }
        _bgmHandle = YooAssets.LoadAssetAsync<AudioClip>(fileName);
        yield return _bgmHandle;
        AudioClip audioClip = _bgmHandle.AssetObject as AudioClip;
        _audioSource.clip = audioClip;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void PlayEffect(string fileName)
    {
        Game.StartCoroutine(DoPlayEffect(fileName));
    }

    private IEnumerator DoPlayEffect(string fileName)
    {
        AssetHandle effectHandle = YooAssets.LoadAssetAsync<AudioClip>(fileName);
        yield return effectHandle;
        AudioClip audioClip = effectHandle.AssetObject as AudioClip;
        _audioSource.PlayOneShot(audioClip, EffectVolume);
        effectHandle.Release();
    }
}