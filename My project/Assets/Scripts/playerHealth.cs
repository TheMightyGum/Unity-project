using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public Image healthBar;
    public TextMeshProUGUI healthText;
    public Image hurtOverlay;
    public float hurtOverlayDuration = 1f;
    bool isAlive = true;

    public void Update()
    {
        healthBar.rectTransform.sizeDelta = new Vector2(health / maxHealth * 290, 40f);
        healthText.GetComponent<TextMeshProUGUI>().text = health.ToString();
    }

    public virtual void TakeDamage(float dmg)
    {
        Image overlay = hurtOverlay.GetComponent<Image>();
        health -= dmg;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.5f);
        if (health <= 0 && isAlive)
        {
            GameOver();
            isAlive = false;
            return;
        }
        StartCoroutine(FadeOverlay(overlay));
    }
    private IEnumerator FadeOverlay(Image overlay)
    {
        yield return new WaitForSeconds(0.1f);
        float elapsedTime = 0;
        while (elapsedTime < hurtOverlayDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0.5f, 0, elapsedTime / hurtOverlayDuration);
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, alpha);
            yield return null;
        }
    }
    private void GameOver()
    {
        print("You died!!");
    }
}
