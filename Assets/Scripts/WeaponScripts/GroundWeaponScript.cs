using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWeaponScript : MonoBehaviour
{
    private GameObject WeaponHolder;
    public bool PickedUp = false;
    private GameObject wg;
    [SerializeField]
    private TextAsset WeaponNamesText;
    [SerializeField]
    private string[] WeaponNames;
    [SerializeField]
    public string Name;

    [SerializeField]
    public int Damage;
    [SerializeField]
    public int CritRate;
    [SerializeField]
    public float Recharge;

    [SerializeField]
    public float rarety;
    [SerializeField]
    public Sprite Icon;

    [SerializeField]
    public float Recharged;
    public float knockbacktime = 0.4f;

    private int GenDamage()
    {
        int damage = Random.Range(0, 100);
        int FinalDamage;
        if (damage < 5)
        {
            FinalDamage = Random.Range(50, 60);
            rarety += 10;
        }
        else if (damage < 10)
        {
            FinalDamage = Random.Range(40, 50);
            rarety += 8;
        }
        else if (damage < 35)
        {
            FinalDamage = Random.Range(30, 40);
            rarety += 5;
        }
        else if (damage < 55)
        {
            FinalDamage = Random.Range(20, 30);
            rarety += 2;
        }
        else if (damage < 80)
        {
            FinalDamage = Random.Range(10, 20);
            rarety -= 2;
        }
        else
        {
            FinalDamage = Random.Range(1, 10);
            rarety -= 5;
        }
        return FinalDamage;
    }
    private int GenCritRate()
    {
        int critrate = Random.Range(0, 100);
        int FinalCritRate;
        if (critrate < 5)
        {
            FinalCritRate = Random.Range(90, 100);
            rarety += 12;
        }
        else if (critrate < 10)
        {
            FinalCritRate = Random.Range(75, 90);
            rarety += 10;
        }
        else if (critrate < 25)
        {
            FinalCritRate = Random.Range(50, 75);
            rarety += 8;
        }
        else if (critrate < 35)
        {
            FinalCritRate = Random.Range(30, 50);
            rarety += 5;
        }
        else if (critrate < 55)
        {
            FinalCritRate = Random.Range(15, 30);
            rarety += 2;
        }
        else if (critrate < 80)
        {
            FinalCritRate = Random.Range(10, 15);
            rarety -= 2;
        }
        else
        {
            FinalCritRate = Random.Range(1, 10);
            rarety -= 5;
        }

        return FinalCritRate;
    }
    private float GenRecharge()
    {
        float Recharge = Random.Range(0, 100);
        float FinalRecharge;
        if (Recharge < 5)
        {
            FinalRecharge = Random.Range(0.2f, 0.5f);
            rarety += 10;
        }
        else if (Recharge < 10)
        {
            FinalRecharge = Random.Range(0.5f, 0.8f);
            rarety += 8;
        }
        else if (Recharge < 35)
        {
            FinalRecharge = Random.Range(0.8f, 1f);
            rarety += 5;
        }
        else if (Recharge < 55)
        {
            FinalRecharge = Random.Range(1f, 1.5f);
            rarety += 2;
        }
        else if (Recharge < 80)
        {
            FinalRecharge = Random.Range(1.5f, 2f);
            rarety -= 2;
        }
        else
        {
            FinalRecharge = Random.Range(2f, 3f);
            rarety -= 5;
        }

        return FinalRecharge;
    }
    private void Start()
    {
        WeaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");

        GenWeapon();
        Recharged = Recharge;
        //NamedWeapons();

    }
    public void GenWeapon()
    {

        Damage = GenDamage();
        CritRate = GenCritRate();
        Recharge = GenRecharge();
        wg = GameObject.Find("WeaponGenerator");
        gameObject.GetComponent<SpriteRenderer>().sprite =
            wg.GetComponent<WeaponGenerator>().WeaponSprites
            [Random.Range(0, wg.GetComponent<WeaponGenerator>().WeaponSprites.Length)];
        Icon = gameObject.GetComponent<SpriteRenderer>().sprite;
        PickedUp = false;
        name = GenName();
        gameObject.AddComponent<CapsuleCollider2D>();
        gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
    }
    private string GenName()
    {
        string name = null;
        WeaponNames = WeaponNamesText.text.Split(new string[] {",", "\n"}, System.StringSplitOptions.None);
        name = WeaponNames[Random.Range(0,WeaponNames.Length)];
        return name;
    }

    private void Update()
    {
        if (Recharged <= Recharge)
        {
            Recharged += 1f * Time.deltaTime;
        }
    }

    private void NamedWeapons()
    {
        if (gameObject.name == "Starter Weapon")
        {
            Damage = 20;
            CritRate = 15;
            Recharge = 1.4f;
            rarety = -2;
            Icon = wg.GetComponent<WeaponGenerator>().WeaponSprites[3];
            gameObject.GetComponent<SpriteRenderer>().sprite =
                wg.GetComponent<WeaponGenerator>().WeaponSprites[3];
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PickedUp == false)
        {
            for (int i = 0; i < Inventory.slots.Length; i++)
            {
                if (Inventory.isFull[i] == false)
                {
                    StartCoroutine(GetComponent<TooltipScript>().GetTooltip());
                    PickedUp = true;
                    Inventory.slots[i] = gameObject;
                    Icon = gameObject.GetComponent<SpriteRenderer>().sprite;
                    if (Inventory.spriteHolder[i] == null)
                    {
                        Inventory.spriteHolder[i] = GameObject.Find("ItemLocation(" + i + ")");
                    }
                    Inventory.spriteHolder[i].GetComponent<SpriteRenderer>().sprite = Icon;
                    WeaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
                    gameObject.transform.SetParent(WeaponHolder.transform);
                    transform.localPosition = new Vector3(0, 0, -1);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    gameObject.SetActive(false);
                    Inventory.isFull[i] = true;

                    break;
                }
            }
        }
        if (other.CompareTag("Enemy") && PickedUp == true)
        {
            if (Recharge <= Recharged)
            {
                Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
                if (enemy!=null)
                {
                    Recharged = 0;
                    enemy.GetComponentInChildren<EnemyAIScript>().canMove = false;
                    enemy.isKinematic = false;
                    Vector2 diffrence = enemy.transform.position - transform.position;
                    diffrence = diffrence.normalized * 4;
                    enemy.AddForce(diffrence, ForceMode2D.Impulse);
                    StartCoroutine(KnockBackCo(enemy));
                    enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y,-1);
                    int totalDamage = Damage;
                    if (Random.Range(0,100) < CritRate)
                    {
                        totalDamage = Damage + Damage;
                    }
                    enemy.GetComponent<EnemyScript>().health -= totalDamage;
                    if (enemy.GetComponent<EnemyScript>().health <= 0)
                    {
                        if (Random.Range(0,100) == 1)
                        {
                            Instantiate(gameObject, enemy.gameObject.transform.position, Quaternion.identity);
                        }
                        Destroy(enemy.gameObject);
                    }
                }
            }
        }
    }
    private IEnumerator KnockBackCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockbacktime);
            try
            {
                enemy.velocity = Vector2.zero;
                enemy.isKinematic = true;
                enemy.GetComponentInChildren<EnemyAIScript>().canMove = true;
            }
            catch (System.Exception)
            {

            }
        }
    }
}
