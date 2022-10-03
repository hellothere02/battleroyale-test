using System.Collections;
using UnityEngine;

public class Person : MonoBehaviour, IDamageable, IMoveable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxDamageAmount;
    [SerializeField] private int speed;
    private int health;
    private int damageAmount;
    private int currentHealth;
    private GameObject opponent;
    public int Health { get => health; set => health = value; }
    public int DamageAmount { get => damageAmount; set => damageAmount = value; }
    public int Speed { get => speed; set => speed = value; }
    public GameObject Target { get; set; }

    public delegate void FindNewPerson(GameObject currentGO);
    public FindNewPerson FindNew;

    private void Start()
    {
        Health = Random.Range(1, maxHealth);
        DamageAmount = Random.Range(1, maxDamageAmount);
        currentHealth = health;
    }

    private void Update()
    {
        Move(Target);
    }

    public void GetDamage(int amount)
    {
        currentHealth -= amount;
    }

    public void Dying()
    {
        Destroy(this.gameObject);
    }

    public void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            FindNew?.Invoke(opponent);
            Dying();
        }
    }

    public void Move(GameObject target)
    {
        if (target == null) return;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable == null) return;
        opponent = collision.gameObject;
        StartCoroutine(Attack(damageable));
    }

    private IEnumerator Attack(IDamageable _opponent)
    {
        yield return new WaitForSeconds(2);
        if (opponent == null)
        {
            StopCoroutine(Attack(_opponent));
        }
        else
        {
            _opponent.GetDamage(damageAmount);
            CheckHealth();
            StartCoroutine(Attack(_opponent));
        }
    }
}
