using UnityEngine;
using System.Collections;

public class RandomBackgroundMusic : MonoBehaviour
{
    public AudioClip[] musicClips; // 音乐片段数组
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomMusic());
    }

    IEnumerator PlayRandomMusic()
    {
        while (true)
        {
            if (musicClips.Length == 0)
            {
                Debug.LogError("No music clips assigned.");
                yield break;
            }

            int randomIndex = Random.Range(0, musicClips.Length);
            audioSource.clip = musicClips[randomIndex];
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);
        }
    }
}
