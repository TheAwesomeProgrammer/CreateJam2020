using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    //
    public AudioClip ExplosionSound;
    public AudioSource ExplosionSoundSource;

    public AudioClip FireSound;
    public AudioSource FireSoundSource;
    //
    public AudioClip MusicSound;
    public AudioSource MusicSoundSource;
    //
    public AudioClip WooshSound;
    public AudioSource WooshSoundSource;
    //
    public AudioClip SpawnBombSound;
    public AudioSource SPawnBombSoundSource;
    //
    public AudioClip SpawnFireSound;
    public AudioSource SpawnFireSoundSource;
    //
    public AudioClip CannonFireSound;
    public AudioSource CannonFireSoundSource;
    //
    public AudioClip OwlHitSound;
    public AudioSource OwlHitSoundSource;

    public int TestAudio = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //DEBUG
        /*
        if(Input.GetKeyDown(KeyCode.P))
        {
            switch(TestAudio)
            {
                case 1:
                    PlayExplosionSound();
                    break;
                case 2:
                    PlayCannonFireSound();
                    break;
                case 3:
                    PlayMusicSound();
                    break;
                case 4: PlaySpawnBombSound();
                    break;
                case 5: PlaySpawnFireSound();
                    break;
                case 6: PlayOwlHitSound();
                    break;
                case 7: PlayFireSound();
                    break;
                case 8: PlayWooshSound();
                    break;
                
            }
            TestAudio++;
        }
        */
    }

    //
    public void PlayExplosionSound()
    {
        ExplosionSoundSource.clip = ExplosionSound;
        ExplosionSoundSource.Play();
    }
    //
    public void PlayCannonFireSound()
    {
        CannonFireSoundSource.clip = CannonFireSound;
        CannonFireSoundSource.Play();
    }
    //
    public void PlayMusicSound()
    {
        MusicSoundSource.clip = MusicSound;
        MusicSoundSource.Play();
    }
    //
    public void PlaySpawnBombSound()
    {
        SPawnBombSoundSource.clip = SpawnBombSound;
        SPawnBombSoundSource.Play();
    }
    //
    public void PlaySpawnFireSound()
    {
        SpawnFireSoundSource.clip = SpawnFireSound;
        SpawnFireSoundSource.Play();
    }
    //
    public void PlayOwlHitSound()
    {
        OwlHitSoundSource.clip = OwlHitSound;
        OwlHitSoundSource.Play();
    }
    //
    public void PlayWooshSound()
    {
        WooshSoundSource.clip = WooshSound;
        WooshSoundSource.Play();
    }

    public void PlayFireSound()
    {
        FireSoundSource.clip = FireSound;
        FireSoundSource.Play();
    }

    public void StopFireSound()
    {
        FireSoundSource.clip = FireSound;
        FireSoundSource.Stop();
    }


}
