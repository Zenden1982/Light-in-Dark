using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float walkSpeed = 10f;
    public float runSpeed = 18f;
    public float rotationSpeed = 10f;
    private Quaternion targetRotation;
    private Rigidbody2D rb;
    private Collider2D col;
    Animator animator;
    [Header("Set Dynamically")]
    public bool walk = false;
    public float speed = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Бег
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = runSpeed;
        } else speed = walkSpeed;

        //Ходьба
        float xMove = Input.GetAxis("Horizontal") * speed;
        float yMove = Input.GetAxis("Vertical") * speed;
        rb.velocity = new Vector2(xMove, yMove);
        if (xMove != 0 || yMove != 0)
        {
            walk = true;
        }
        else walk = false;
        animator.SetBool("Walk", walk);


        //Слежение за курсором
        Vector3 mousePos = Input.mousePosition;
        //mousePos.z = 10f; // Устанавливаем дистанцию до камеры, чтобы мы могли получить точку в пространстве.
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos); // Получаем точку в пространстве, на которую смотрит курсор.

        Vector3 targetDir = lookPos - transform.position; // Вычисляем направление, в котором должен повернуться персонаж.
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg + 90; // Вычисляем угол поворота в градусах.
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward); // Создаем кватернион поворота.

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Применяем вращение к персонажу.
    }
}
