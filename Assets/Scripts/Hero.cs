using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float walkSpeed = 10f;
    public float runSpeed = 18f;
    public float rotationSpeed = 10f;
    public float mind = 100f;
    public GameObject Fleshlight;

    private bool FleshlightOn = true;
    private bool FleshlightPurple = false;
    private Quaternion targetRotation;
    private Rigidbody2D rb;
    private Collider2D col;
    Animator animator;
    [Header("Set Dynamically")]
    public bool walk = false;
    public float speed = 0;
    public Image brainSprite;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //Слежение за курсором
        Vector3 mousePos = Input.mousePosition;
        //mousePos.z = 10f; // Устанавливаем дистанцию до камеры, чтобы мы могли получить точку в пространстве.
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos); // Получаем точку в пространстве, на которую смотрит курсор.

        Vector3 targetDir = lookPos - transform.position; // Вычисляем направление, в котором должен повернуться персонаж.
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg + 90; // Вычисляем угол поворота в градусах.
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward); // Создаем кватернион поворота.

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Применяем вращение к персонажу.
        MindController();
        FleshlightController();
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Scene 1");
        }
    }
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


    }

    void MindController()
    {
        Color color = new Color(1, 0.5f, 0.5f, 1);
        color.a = mind/100;
        brainSprite.color = color;
    }

    void FleshlightController()
    {
        if (Input.GetMouseButtonDown(0) && FleshlightOn == true)
        {
            Fleshlight.SetActive(false);
            FleshlightOn = false;
        } else if (Input.GetMouseButtonDown(0) && FleshlightOn == false)
        {
            Fleshlight.SetActive(true);
            FleshlightOn = true;

        }

        if (Input.GetMouseButtonDown(1) && FleshlightPurple==false)
        {
            Light2D light2D = Fleshlight.GetComponentInChildren<Light2D>();
            light2D.color = new Color(1, 0f, 1, 1);
            FleshlightPurple = true;
        } else if (Input.GetMouseButtonDown(1) && FleshlightPurple == true)
        {
            Light2D light2D = Fleshlight.GetComponentInChildren<Light2D>();
            light2D.color = Color.white;
            FleshlightPurple = false;
        }
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("mind", mind);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetFloat("mind", mind);
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat("mind", mind);
    }

    void OnEnable()
    {
        mind = PlayerPrefs.GetFloat("mind", 100f);
    }

}
