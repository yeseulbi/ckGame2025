using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    public EnemyData enemy_Data;
    void Start()
    {
        transform.localScale += new Vector3(enemy_Data.attackRange, enemy_Data.attackRange);
        transform.position += new Vector3(enemy_Data.attackRange / 2, 0, 0);
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        Debug.Log("¾Æ¾ß!");
    }
}
