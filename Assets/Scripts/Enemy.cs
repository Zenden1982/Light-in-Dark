using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float maxDistance = 10f; // ������������ ����������, �� ������� ���� �������� �������� ����
    public float minDistance = 2f; // ����������� ����������, �� ������� ���� ������� ������������ ����
    public float maxDamage = 10f; // ������������ ����, ��������� ������
    public float minDamage = 1f; // ����������� ����, ��������� ������

    private Hero hero; // ������ �� ����� Hero
    private GameObject fleshlight;
    private ShadowEnemy shadowEnemy;

    void Start()
    {
        // ������� ����� Hero, ������� ��������� �� ������
        hero = player.GetComponent<Hero>();
        fleshlight = GameObject.Find("Fleshlight");
        shadowEnemy = GetComponent<ShadowEnemy>();
    }

    void Update()
    {
        // ������������ ���������� ����� ������� � ������
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        float distance = Mathf.Abs(playerPos.x - enemyPos.x) + Mathf.Abs(playerPos.y - enemyPos.y);

        // ���� ����� ��������� � ���� ���������, ������� ���� � ����������� �� ���������� �� ������
        if (distance < maxDistance)
        {
            // ������������ ����
            float damage = Mathf.Lerp(minDamage, maxDamage, Mathf.InverseLerp(minDistance, maxDistance, distance));
            if (hero.mind > 0 && !shadowEnemy.fleshing)
            {
                hero.mind -= damage * Time.deltaTime;
            }
        }
    }
}