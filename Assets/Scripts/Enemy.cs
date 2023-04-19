using UnityEngine;

public class Enemy : MonoBehaviour, IEnemyTargetable
{
    private Animator animator;
    [SerializeField] private float health = 100f;
    [SerializeField] private HealthBar healthBar;

    public Animator Animator { get => animator; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        healthBar = transform.GetComponentInChildren<HealthBar>();    }
    public void Damage(Bullet bullet)
    {
        if (IsDie()) { Animator.enabled = false; GameController.OnDiedEnemy?.Invoke(); }
        if (!IsDie()) { health -= bullet.Damage; healthBar.Change(bullet.Damage); Debug.Log(health);if (IsDie()) { Animator.enabled = false; GameController.OnDiedEnemy?.Invoke(); } }
       

    }
    bool IsDie()
    {
        if(health == 0) {  return true;  }
        return false;
    }
    public void EnemyDefault() { health = 100f; }    
}
