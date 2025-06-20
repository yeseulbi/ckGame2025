using UnityEngine;
using UnityEngine.UIElements;

/*d
 
 
 */
public class EnemyMove : MonoBehaviour
{
    EnemyData enemy_Data;
    Animator animator;
    GameObject player;
    private void Awake()
    {
        enemy_Data = GetComponent<EnemyData>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }
    void Start()
    {

    }

    void FixedUpdate()
    {
        var currentPos = transform.position;
        Vector3 playerPos = player.transform.position;

        // enemyData.detectionRange�� ���� ���� ������, ������ ����->�÷��̾� ����, Player �±��� �ݶ��̴��� ������ �ν�
        RaycastHit2D hit = Physics2D.Raycast(currentPos, playerPos - currentPos, enemy_Data.detectionRange, LayerMask.GetMask("Effect"));

        if (hit && hit.collider.CompareTag("Player"))
        {
            Debug.Log("�þ� �� �÷��̾� ����");
            MoveToPlayer();        
        }
    }

    /// <summary>
    /// enemy�� �÷��̾ �����ϱ� ���� �Լ�. �÷��̾��� ������ ã�� ��θ� �����Ѵ�.
    /// </summary>
    void MoveToPlayer()
    {
        if (player != null)
        {
            /*����ĳ��Ʈ�� ���� �ִٸ� vector x �������� */

            transform.Translate(player.transform.position.normalized * Time.fixedDeltaTime * enemy_Data.moveSpeed); // ��� ���� ��
        }
    }
}
