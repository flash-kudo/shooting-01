using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    public AudioClip audioClip4;
    public AudioClip audioClip5;
    public AudioClip audioClip6;
    public AudioClip audioClip7;
    public AudioClip audioClip8;
    public AudioClip audioClip9;
    public AudioClip audioClip10;
    public AudioClip audioClip11;
    public AudioClip audioClip12;
    public AudioClip audioClip13;
    public AudioClip audioClip14;
    public AudioClip audioClip15;
    public AudioClip audioClip16;
    public AudioClip audioClip17;
    public AudioClip audioClip18;
    public AudioClip audioClip19;
    public AudioClip audioClip20;
    public AudioClip audioClip21;
    public AudioClip audioClip22;
    public AudioClip audioClip23;
    public AudioClip audioClip24;
    public AudioClip audioClip25;
    public AudioClip audioClip26;

    public enum AudioClipType
    {
        CHARGE1,
        CHARGE2,
        CHARGE3,
        DAMAGE01,
        EN_BEAM,
        ET_DAMAGE1,
        ET_DAMAGE2,
        ET_EXPLOSION,
        FORT,
        FORT_EXPLOSION,
        EXPLOSION1,
        EXPLOSION2,
        GAMEOVER,
        GUNSHOT_FANTOM01,
        IDOUOCHIRUSD,
        KETTEI,
        LVUP,
        MACHINEGUN,
        MISSILE,
        NORIMONOUFO,
        OFFSET,
        PY_SELECT,
        REFLECTION,
        ROLLING_ROCKET,
        SAW,
        SE_LOCKON,

        AudioClipTypeMax
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAudioClip(AudioClipType audioClipType)
    {
        Debug.Log(audioClipType);
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
        switch (audioClipType)
        {
            case AudioClipType.CHARGE1:
                audioSource.PlayOneShot(audioClip1);
                break;
            case AudioClipType.CHARGE2:
                audioSource.PlayOneShot(audioClip2);
                break;
            case AudioClipType.CHARGE3:
                audioSource.PlayOneShot(audioClip3);
                break;
            case AudioClipType.DAMAGE01:
                audioSource.PlayOneShot(audioClip4);
                break;
            case AudioClipType.EN_BEAM:
                audioSource.PlayOneShot(audioClip5);
                break;
            case AudioClipType.ET_DAMAGE1:
                audioSource.PlayOneShot(audioClip6);
                break;
            case AudioClipType.ET_DAMAGE2:
                audioSource.PlayOneShot(audioClip7);
                break;
            case AudioClipType.ET_EXPLOSION:
                audioSource.PlayOneShot(audioClip8);
                break;
            case AudioClipType.FORT:
                audioSource.PlayOneShot(audioClip9);
                break;
            case AudioClipType.FORT_EXPLOSION:
                audioSource.PlayOneShot(audioClip10);
                break;
            case AudioClipType.EXPLOSION1:
                audioSource.PlayOneShot(audioClip11);
                break;
            case AudioClipType.EXPLOSION2:
                audioSource.PlayOneShot(audioClip12);
                break;
            case AudioClipType.GAMEOVER:
                audioSource.PlayOneShot(audioClip13);
                break;
            case AudioClipType.GUNSHOT_FANTOM01:
                audioSource.PlayOneShot(audioClip14);
                break;
            case AudioClipType.IDOUOCHIRUSD:
                audioSource.PlayOneShot(audioClip15);
                break;
            case AudioClipType.KETTEI:
                audioSource.PlayOneShot(audioClip16);
                break;
            case AudioClipType.LVUP:
                audioSource.PlayOneShot(audioClip17);
                break;
            case AudioClipType.MACHINEGUN:
                audioSource.PlayOneShot(audioClip18);
                break;
            case AudioClipType.MISSILE:
                audioSource.PlayOneShot(audioClip19);
                break;
            case AudioClipType.NORIMONOUFO:
                audioSource.PlayOneShot(audioClip20);
                break;
            case AudioClipType.OFFSET:
                audioSource.PlayOneShot(audioClip21);
                break;
            case AudioClipType.PY_SELECT:
                audioSource.PlayOneShot(audioClip22);
                break;
            case AudioClipType.REFLECTION:
                audioSource.PlayOneShot(audioClip23);
                break;
            case AudioClipType.ROLLING_ROCKET:
                audioSource.PlayOneShot(audioClip24);
                break;
            case AudioClipType.SAW:
                audioSource.PlayOneShot(audioClip25);
                break;
            case AudioClipType.SE_LOCKON:
                audioSource.PlayOneShot(audioClip26);
                break;
            default:
                break;
        }
    }
}
