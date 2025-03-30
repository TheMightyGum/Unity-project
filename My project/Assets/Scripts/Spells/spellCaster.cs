
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class spellCaster : MonoBehaviour
{
    InputAction hotkey;
    private spellBase activeSpell = null;

    public spellBase spellPrefab;
    public Transform spawnPoint;
    public Image cooldownBar;
    float cooldownTimer;

    void Start()
    {
        hotkey = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        if (hotkey.WasPressedThisFrame() && cooldownTimer>=spellPrefab.cooldown)
        {
            if (activeSpell == null)
            {
                readySpell();
            }
            else
            {
                castSpell();
                cooldownTimer = 0;
            }
        }
        cooldownTimer += Time.deltaTime;
        cooldownBar.rectTransform.sizeDelta = new Vector2(Mathf.Clamp(cooldownTimer / spellPrefab.cooldown, 0f, 1f) * 290, 40f);
        if (cooldownTimer >= spellPrefab.cooldown)
        {
            cooldownBar.color = Color.cyan;
        }
        else
        {
            cooldownBar.color = Color.blue;
        }
    }

    void readySpell()
    {
        activeSpell = Instantiate(spellPrefab, spawnPoint.position, spawnPoint.rotation);
        activeSpell.transform.SetParent(spawnPoint);
    }

    void castSpell()
    {
        activeSpell.transform.SetParent(null);
        activeSpell.Cast(AimDirection());
    }
    private Vector3 AimDirection()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            return (hit.point - spawnPoint.position).normalized;
        } 
        else 
        {
            return ray.direction;
        }
    }
}
