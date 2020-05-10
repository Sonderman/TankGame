using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public Text healthText;
    private float health = 100f;
    Transform camTransform;
    public Transform ui;
    public Image healthBar;
    float movespeed = 10f;
    void Start()
    {
        camTransform = Camera.main.transform;
    }

    
    void Update()
    {
        Vector3 lookDirection = (ui.position - camTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        //ui.rotation = lookRotation;
        ui.rotation = Quaternion.Lerp(ui.rotation, lookRotation, Time.deltaTime * movespeed);
    }

    public void TakeDamage(int amount)
    {
        if (health > 0)
        {
            StartCoroutine(TakeDamageSmoothly(amount));
        }
        
    }

    private IEnumerator TakeDamageSmoothly(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            health--;
            //health -= amount;
            healthText.text = health.ToString();
            healthBar.fillAmount = health / 100f;
            yield return null;
        }
    }

}
