using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static AudioSource playerHeadAudioSource;
    public static AudioSource gunCamAudioSource;
    public static AudioSource noiseAudioSource;
    public static AudioSource suitAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // ASSIGN ALL THE SOUND SOURCES INTO THEIR RESPECTIVE AREAS
        playerHeadAudioSource = GameObject.Find("PlayerHead").GetComponent<AudioSource>();
        gunCamAudioSource = GameObject.Find("GunCam").GetComponent<AudioSource>();
        noiseAudioSource = GameObject.Find("NoiseSource").GetComponent<AudioSource>();
        suitAudioSource = GameObject.Find("SuitSource").GetComponent<AudioSource>();
    }
    // PLAYER RELATED SOUNDS
    public static IEnumerator playerSound(AudioClip SFX, float delay)
    {
        playerHeadAudioSource.PlayOneShot(SFX);
        yield return new WaitForSeconds(delay * 5);
    }
    // WEAPON RELATED SOUNDS
    public static IEnumerator gunSounds(AudioClip SFX, float delay)
    {
        gunCamAudioSource.PlayOneShot(SFX);
        yield return new WaitForSeconds(delay * 5);
    }
    // AMBIENT RELEATED SOUNDS
    public static IEnumerator noiseSound(AudioClip SFX, float delay)
    {
        noiseAudioSource.PlayOneShot(SFX);
        yield return new WaitForSeconds(delay * 5);
    }
    // SUIT RELATED SOUNDS
    public static IEnumerator suitNoise(AudioClip SFX, float delay)
    {
        suitAudioSource.PlayOneShot(SFX);
        yield return new WaitForSeconds(delay * 5);
    }
}