using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    private static GameLoader i;
    [Header("Data")]
    [SerializeField] LevelData levelData;
    [SerializeField] AudioClip[] audioClips;
    List<string> nameIconsList;

    public static GameLoader I { get => i; set => i = value; }
    private GameLoader(){}
    private void OnEnable()
    {
        nameIconsList = new List<string>();
        I = this;
        audioClips = Resources.LoadAll("Audio", typeof(AudioClip)).Cast<AudioClip>().ToArray();
    }
   
    public AudioClip GetAudioByName(string name)
    {
        foreach (var item in audioClips)
        {
            if(item.name == name) { return item; }
        }
        return null;
    }
    public Transform GetWayPoint(int curCountLevel, int curCountPoint) { return levelData.ListLevels[curCountLevel].GetWayPoint(curCountPoint); }
    
    public Level GetLevel(int levelName) { return levelData.ListLevels[levelName-1]; }
    
   

}
