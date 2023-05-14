using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float maxDistance = 10f; // ћаксимальное рассто€ние, на котором враг начинает наносить урон
    public float minDistance = 2f; // ћинимальное рассто€ние, на котором враг наносит максимальный урон
    public float maxDamage = 10f; // ћаксимальный урон, наносимый врагом
    public float minDamage = 1f; // ћинимальный урон, наносимый врагом

    private Hero hero; // —сылка на класс Hero

    void Start()
    {
        // Ќайдите класс Hero, который находитс€ на игроке
        hero = player.GetComponent<Hero>();
    }

    void Update()
    {
        // –ассчитываем рассто€ние между игроком и врагом
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        float distance = Mathf.Abs(playerPos.x - enemyPos.x) + Mathf.Abs(playerPos.y - enemyPos.y);

        // ≈сли игрок находитс€ в зоне поражени€, наносим урон в зависимости от рассто€ни€ до игрока
        if (distance < maxDistance)
        {
            // –ассчитываем урон
            float damage = Mathf.Lerp(minDamage, maxDamage, Mathf.InverseLerp(minDistance, maxDistance, distance));
            if (hero.mind >0)
            {
                hero.mind -= damage * Time.deltaTime;
            }
        }
    }
}