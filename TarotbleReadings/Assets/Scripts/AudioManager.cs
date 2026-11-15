using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{

    public enum SoundType
    {
        Button,
        NewCustomer,
        Speech,
        Silver,
        Gold,
        Prismatic,
        Deck,

    }

    [SerializeField] private SoundList[] soundList;
    public static AudioManager instance;
    public AudioSource audioSource;


    void Awake()
    {
        if(instance == null)
        {
            
        }
    }


    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0,clips.Length)];
       
        instance.audioSource.PlayOneShot(randomClip, volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);

        for(int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }

#endif

    

}


[Serializable]
public struct SoundList
{
    [HideInInspector] public string name;
    [SerializeField] public AudioClip[] sounds;
}