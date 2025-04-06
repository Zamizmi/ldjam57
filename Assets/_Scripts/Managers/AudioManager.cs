using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFXPrefab;
    [SerializeField] private AudioClip[] lanternLitClips;
    [SerializeField] private AudioClip[] footstepsClips;
    [SerializeField] private AudioClip[] jumpClips;
    [SerializeField] private AudioClip[] leverClips;
    [SerializeField] private AudioClip thunderClip;
    [SerializeField] private AudioClip stormClip;

    private void OnEnable()
    {
        PlayerEvents.OnFootStep += PlayFootStepSound;
        PlayerEvents.OnJump += PlayJumpSound;
        InteractableEvents.OnLanternTurn += PlayLanternSound;
        InteractableEvents.OnLeverTurn += PlayerLeverSound;
        StoryEvents.OnStorm += PlayStorm;
        StoryEvents.OnThunder += PlayThunder;
    }

    public void PlayStorm(Vector3 position)
    {
        PlaySoundFXFadeIn(stormClip, position, 1f);
    }


    public void PlayThunder(Vector3 position)
    {
        PlaySoundFX(thunderClip, position, 2f);
    }

    private void PlayerLeverSound(Vector3 position)
    {
        int index = UnityEngine.Random.Range(0, leverClips.Length);
        float pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        PlaySoundFX(leverClips[index], position, 1f, pitch);
    }

    private void PlayLanternSound(Vector3 position)
    {
        int index = UnityEngine.Random.Range(0, lanternLitClips.Length);
        float pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        PlaySoundFX(lanternLitClips[index], position, 1f, pitch);
    }

    private void PlayFootStepSound(Vector3 position)
    {
        int index = UnityEngine.Random.Range(0, footstepsClips.Length);
        float pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        PlaySoundFX(footstepsClips[index], position, 1f, pitch);
    }

    private void PlayJumpSound(Vector3 position)
    {
        int index = UnityEngine.Random.Range(0, jumpClips.Length);
        float pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        PlaySoundFX(jumpClips[index], position, 1f, pitch);
    }

    private void PlaySoundFX(AudioClip clip, Vector3 spawnTransform, float volume = 1f, float pitch = 1f)
    {
        AudioSource audioSource = Instantiate(soundFXPrefab, spawnTransform, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    private void PlaySoundFXFadeIn(AudioClip clip, Vector3 spawnTransform, float fadeIn)
    {
        AudioSource audioSource = Instantiate(soundFXPrefab, spawnTransform, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.pitch = 1f;
        audioSource.volume = 0f;
        StartCoroutine(FadeIn(audioSource, fadeIn, 1f));
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    IEnumerator FadeIn(AudioSource source, float duration, float targetVolume)
    {
        float startVolume = 0f;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        source.volume = targetVolume;
    }
}

