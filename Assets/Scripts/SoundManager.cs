using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("General")]
    [SerializeField] AudioSource sfxAudioSource;

    [Header("Player Footstep")]
    [SerializeField] float playerFootstepSoundSpeed;
    [SerializeField] AudioSource playerFootstepAudioSource;
    [SerializeField] AudioClip[] playerFootstepSounds;

    float footstepTimer;

    public void PlayFootsteps()
    {
        footstepTimer += Time.deltaTime;
        float interval = 1f / playerFootstepSoundSpeed;

        if (footstepTimer >= interval)
        {
            footstepTimer = 0f;

            int randomIndex = Random.Range(0, playerFootstepSounds.Length);
            float randomVolume = Random.Range(0.85f, 1f);

            playerFootstepAudioSource.PlayOneShot(playerFootstepSounds[randomIndex], randomVolume);
        }
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxAudioSource.PlayOneShot(audioClip);
    }
}
