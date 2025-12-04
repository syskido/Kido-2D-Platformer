using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
   

    private Animator anim; // Animation �� Animator�� ����
    private PlayerMovement playerMovement; // PlayerMovement ��� ����
    private float CooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>(); // Animation �� Animator�� ����
        playerMovement = GetComponent<PlayerMovement>(); // PlayerMovement ��� ����
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && CooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        CooldownTimer += Time.deltaTime;

    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        CooldownTimer = 0;

        fireballs[0].transform.position = firePoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.position.x));
        //pool fireballs
    }
}
