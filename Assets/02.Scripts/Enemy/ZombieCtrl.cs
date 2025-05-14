using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class ZombieCtrl : MonoBehaviour
{
    //1.NavMeshAgent�� �̿� �ؼ�
    //2.Player�� ���� ���� �ȿ� ������ �����ϰ�
    //3.���� ���� �ȿ� ������ �����ϴ� ���� ������ �ִϸ��̼� ����
    //4.���������� ���ݹ����� ���Ϸ��� �Ÿ��� ���ؾ���, �÷��̾�� ������ ��ġ�� �˾ƾ���
    [SerializeField] private NavMeshAgent navi;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform zombieTr;
    public Transform playerTr;

    public float traceDist = 20.0f; // ���� ����
    public float attackDist = 3f; // ���� ����

    private readonly int hashAttack = Animator.StringToHash("IsAttack"); //�����Ҵ�� ���� ���ڿ��� �о ������ ��ȯ 
    private readonly int hashTrace = Animator.StringToHash("IsTrace_B");

    void Start()
    {
        navi = GetComponent<NavMeshAgent>();
        playerTr = GameObject.FindWithTag("Player").transform; // ���̾��Ű���� Player��� �±׸� ���� ������Ʈ�� Ʈ�������� ������ 
        animator = GetComponent<Animator>();
        zombieTr = GetComponent<Transform>();
    }

    
    void Update()
    {
        float dist = Vector3.Distance(zombieTr.position,playerTr.position);
        
        if (dist <= attackDist) // �������� ��
        {
            animator.SetBool(hashAttack, true);
            
            navi.isStopped = true; // �������� �� �׺� ��������
        }
        else if(dist <= traceDist) // �������� ��
        {
            
            animator.SetBool(hashAttack, false);
            animator.SetBool(hashTrace, true);
            navi.isStopped = false; // ���� ���� �ȿ� ������ �׺� ���� ���� 
            navi.destination = playerTr.position;
        }
        else
        {
            animator.SetBool(hashTrace, false);
            navi.isStopped = true; // ���� ���� ���� �� ����
        }
    }
}
