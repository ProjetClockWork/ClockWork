using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class playerLife : MonoBehaviour
{

    private int vie;
    [SerializeField]private int vieMax = 3;
    [SerializeField]private Slider barreVie;
    private Vector3 positionSpawn;
    private bool invincible;
    private SpriteRenderer skin;
    private float timer;

    void Start()
    {
        vie = vieMax;
        positionSpawn = transform.position;
        skin = GetComponent<SpriteRenderer>();
        updateSliderVie();
    }

    public void takeDamage(int damage)
    {
        if (!invincible)
        {
            vie = vie - damage;
            updateSliderVie();
            if (vie <= 0)
            {
                PlayerDead();
            }
            StartCoroutine(InvincibilityCycle());
        }
    }
    private void Update()
    {
        if (skin.color != Color.white)
        {
            skin.color = Color.Lerp(Color.red, Color.white, Time.time - timer);
        }
    }

    void updateSliderVie()
    {
        barreVie.value = (float)vie / (float)vieMax;
    }

    public void PlayerDead()
    {
        transform.position = positionSpawn;
        vie = vieMax;
        updateSliderVie();
        gameObject.SendMessage("storeInAllSlots");
    }

    IEnumerator InvincibilityCycle()
    {
        invincible = true;
        skin.color = Color.red;
        timer = Time.time;
        yield return new WaitForSeconds(0.5f);
        invincible = false;
    }

    private void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Kill")
        {
            PlayerDead();
        }

        if (truc.tag == "Respawn")
        {
            positionSpawn = transform.position;
        }
    }
}