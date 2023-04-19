using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Action OnStartLevel;
    public static Action OnDiedEnemy;
    public static Action OnNextLevel;
    public static Action OnRestartGame;
    [SerializeField] private int curLevel = 1;
    [SerializeField] private int curWayPoint = 1;
    [SerializeField] private int curPlatform = 0;
    [SerializeField] private int curAmountEnemes;

    private Enemy[] enemiesArray;
    private Queue<Platform> platformsQueue = new Queue<Platform>();

    Character character;

    private static GameController i;
    private GameController() { }
    public enum State
    {
        FistWay,
        Start,
        OnPlay,
        End,
    }
    public State gameState;

    public static GameController I { get => i; set => i = value; }
   

    public void StartTap()
    {
        gameState = State.Start;
        OnStartLevel?.Invoke();
        ChangeWayPointCOunt();
    }
    private void OnEnable()
    {
        OnDiedEnemy += KillEnemy;
        OnRestartGame += RestartLevel;
    }

    private void Awake()
    {
        if (I == null) { I = this; }

        enemiesArray = GameObject.Find ("----------Enemy--------------").GetComponentsInChildren<Enemy>();
        character = FindObjectOfType<Character>();

    }
    private void Start()
    {
        LoadPlatformByIntLevel(curLevel);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) { NextLevel();  }
    }
    public static int CurLevel() { return I.curLevel; }
    public static int CurWayPoint() { return I.curWayPoint;  }
    public static int CurPlatform() { return I.curPlatform; }
    public void PointUpdate() { curWayPoint++; }

    void LoadPlatformByIntLevel(int intLevel)
    {
        var level = GameLoader.I.GetLevel(intLevel);
        foreach (var platform in level.PlatformsList)
        {
            platformsQueue.Enqueue(platform);
        }
        
        Debug.Log(platformsQueue.Count);
    }
    public Transform GetWayPoint() { if (platformsQueue.Count > 0) { var platform = platformsQueue.Dequeue(); curAmountEnemes = platform.EnemyAmount; return platform.WayPoint; } OnRestartGame?.Invoke(); return null; }
    public void KillEnemy() { if (curAmountEnemes > 0) { curAmountEnemes--; } if (curAmountEnemes == 0) { curPlatform++; } }
    void NextLevel() { LoadPlatformByIntLevel(curLevel++); OnNextLevel?.Invoke(); }

    public bool IsComplitePlatform()
    {
        if (curAmountEnemes == 0)
        {
            
            return true;
        }
        else return false;
    }
    void RestartLevel()
    {
        //CinemachineShake.Instance.ResetForOnStartLevel(false);
        //CinemachineShake.Instance.FollowClean();
        //LoadPlatformByIntLevel(curLevel);
        //character.StartState();
        //for (int i = 0; i < enemiesArray.Length; i++)
        //{
        //    //enemiesArray[i].transform.position = GameLoader.I.GetLevel(curLevel).EnemySpawnWaypoints[i].position;
        //    enemiesArray[i].Animator.enabled = true;
        //    enemiesArray[i].EnemyDefault();
        //}
        //CinemachineShake.Instance.SetFollow(character.transform.parent);
        //FunctionTimer.Create(()=> CinemachineShake.Instance.ResetForOnStartLevel(true),.2f);
        //curPlatform = 1;
        //curAmountEnemes = 3;
        SceneManager.LoadScene(0);
    }
    public void ChangeWayPointCOunt() { curWayPoint++; }
    private void OnDisable()
    {
        OnDiedEnemy -= KillEnemy;
        OnRestartGame -= RestartLevel;
    }
}
