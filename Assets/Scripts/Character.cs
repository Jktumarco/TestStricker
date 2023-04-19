using UnityEngine;
using UnityEngine.AI;
using CodeMonkey.Utils;
using System;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject tempBullet;
    [SerializeField] Transform spawnBulletPoint;
    public static Action OnShooting;
    [SerializeField] GameObject curBullet;

    [SerializeField] Transform curEnemyTarget;

    [SerializeField] Transform targetRig;
    [SerializeField] private Transform movePositionTransform;
    [SerializeField] private Transform wayNext;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMashAgent;


    [SerializeField] private Transform emptyTarget;
    private void OnEnable()
    {
        GameController.OnDiedEnemy += MoveToNextPoint;
        GameController.OnStartLevel += FirstWayPoint;
        GameController.OnNextLevel += StartState;
        OnShooting += CheckCoplitePlatform;
    }

    private void CheckCoplitePlatform()
    {
        if (GameController.I.IsComplitePlatform()) { canStrike = false; }
    }

    private enum State
    {
        MovingToPlatform,
        Shooting,
        OnStayPlatform
    }

    private State currentState;
    [SerializeField] private bool canStrike = false;
    private float speed = 1f;
    [SerializeField] private Transform curWay;
    private bool isStay = false;

    private void Awake()
    {
        animator.Play("shoot");
        FunctionUpdater.Create(() => { if (emptyTarget != null) { LoockAtEnemy(emptyTarget); }; return false; }, "rotateAwake", true, true);
        //navMashAgent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        navMashAgent.destination = movePositionTransform.position;
    }
    void Shoot(RaycastHit raycastHit, Enemy enemy) {
        curBullet = PoolObject.Instanse.GetBullet();
        curBullet.transform.position = spawnBulletPoint.position;
        var bullet = curBullet.GetComponent<Bullet>();
        //CinemachineShake.Instance.ShakeCamera(1f, 0.05f);
        curEnemyTarget.position = raycastHit.point;
        //curBullet = Instantiate(tempBullet, spawnBulletPoint);
        //enemy.Damage(bullet);

    }

    void GoToWayPoint() { navMashAgent.destination = GetWayPoint().position; }

    Transform GetWayPoint() { return GameController.I.GetWayPoint(); }


    void Update()
    {
        if (navMashAgent.remainingDistance <= navMashAgent.stoppingDistance) { currentState = State.OnStayPlatform; animator.Play("shoot"); canStrike = true; }
        else if (navMashAgent.remainingDistance <= navMashAgent.stoppingDistance) { canStrike = false; }
        if (Input.GetMouseButtonDown(0) && currentState == State.OnStayPlatform)
        {
            var m = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(m);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                var enemy = raycastHit.collider.GetComponentInParent<Enemy>();

                if (enemy != null)
                {
                    emptyTarget = curEnemyTarget;
                    FunctionUpdater.Create(() =>
                    {
                        if (emptyTarget != null)
                        {
                            LoockAtEnemy(emptyTarget);
                        }; return false;
                    }, "rotate", true, true);
                    canStrike = true; Shoot(raycastHit, enemy); OnShooting?.Invoke();
                }
            }
        }  
        if (canStrike)
        {
            if (curBullet != null && emptyTarget)
            {
                Vector3 a = curBullet.transform.position;
                Vector3 b = emptyTarget.position;
                curBullet.transform.parent = null;
                curBullet.transform.position = Vector3.MoveTowards(a, b, speed);
            }
        }
    }
    void LoockAtEnemy(Transform target)
    {
        Vector3 diration = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(diration.x, 0, diration.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 25f * Time.deltaTime);
    }

    public void StartState()
    {
        navMashAgent.speed = 0f;
        curWay = GetWayPoint();
        transform.parent.position = curWay.position;
        navMashAgent.speed = 3f;
        navMashAgent.destination = curWay.position;
        currentState = State.MovingToPlatform;
    }
    int NextPoint()
    {
        var point = GameController.CurWayPoint();
        GameController.I.PointUpdate();
        return point;
    }
    void FirstWayPoint()
    {
        animator.Play("Run");
        ////curWay = GetWayPoint();
        navMashAgent.speed = 4f;
        navMashAgent.destination = GetWayPoint().position; ;
        GameController.I.ChangeWayPointCOunt();
    }
    void MoveToNextPoint()
    {
        if (GameController.I.IsComplitePlatform())
        {
            currentState = State.MovingToPlatform;
            curWay = GetWayPoint();
            if (curWay != null)
            {
                navMashAgent.destination = curWay.position;
                navMashAgent.speed = 4f;
                animator.Play("Run");
                emptyTarget = null;
            }
        }
    }

    private void OnDisable()
        {
        GameController.OnDiedEnemy -= MoveToNextPoint;
        GameController.OnStartLevel -= FirstWayPoint;
        GameController.OnNextLevel -= StartState;
    }  

}
