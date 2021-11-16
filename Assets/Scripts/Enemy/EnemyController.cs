using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int maxHealth; // Setter�� ���� �ٸ� ��ũ��Ʈ���� ����
    private int currentHealth;

    private int attackCount = 0; // MainCharacter���� ���� Ƚ��

    private Animator animator;

    public GameObject myButton = null;
    private Rigidbody2D rb = null;
    private Transform playerTransform;
    private bool interaction = false;
    private bool run = false;
    private bool die = false; 
    private float runSpeed = 30f;

    private float rundist = 450f;

    private EffectManage effectController;
/*    private GameObject xSheet;
    private Animator xSheet_anim;*/

    // Getters
    public int GetMaxHealth() { return maxHealth; }
    public int GetCurrentHealth() { return currentHealth; }
    public int GetAttackCount() { return attackCount; }
    public bool GetInteractionState() { return interaction; }
    public bool GetDieState() { return die; }
    public bool GetRunState() { return run; }
    // Setters
    public void SetMaxHealth(int value) { maxHealth = value; }
    public void SetCurrentHealth(int health) { currentHealth = health; }
    public void ResetAttackCount() { attackCount = 0; }
    public void ChangeAttackCount(int amount) { attackCount = attackCount + amount; }
    public void SetRunState(bool value) { run = value; }
    public void SetInteractionState(bool value) { interaction = value; }
    // Init
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = ShieldmanController.Instance.transform;
        animator = GetComponent<Animator>();

        effectController = GameObject.Find("Effects").GetComponent<EffectManage>();
        /*xSheet = gameObject.transform.GetChild(0).gameObject;
        xSheet_anim = xSheet.GetComponent<Animator>();*/
    }
    // Enemy Run... 
    private void FixedUpdate()
    {
        if (ShieldmanController.Instance.GetDieState())
        {
            Destroy(gameObject);
        }
        // �ʿ��� ���̱� ������ �� ��ȣ�ۿ� ���� 
        // Overhead :: --> Coroutine
        if (!run && !die && !interaction && transform.position.x - playerTransform.position.x <= 1920
            && transform.position.x - playerTransform.position.x > rundist)
        {
            interaction = true;
            EnemyPool.Instance.PushEnemy(gameObject);
            Debug.Log(this + "inter");
        }
        else if (!die && transform.position.x - playerTransform.position.x <= rundist && !run)
        {
            interaction = false;
            run = true;
            EnemyPool.Instance.DeleteEnemy(gameObject);
            EnemyButtonManageVer2.Instance.SetButtonState(false);
            DeleteButtons();
            Debug.Log(this + "run");
        }
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            // �ǰ� animator ó�� �κ� 
            effectController.SuccessAttack(gameObject);
        }
        // Clamp �޼ҵ� �̿�, �ִ밪�� maxHealth���� ���ϰ� ���� 
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + " / " + maxHealth);
        if (currentHealth <= 0)
        {
            DieProcess();
            EnemyPool.Instance.DeleteEnemy(gameObject);
            EnemyButtonManageVer2.Instance.SetButtonState(false);
        }
    }
    public void GenerateButton(GameObject button)
    {
        Vector2 position = rb.position + Vector2.up * 70f + Vector2.left * 65f; // pivot : Right Center ����
        GameObject newButton = Instantiate(button, position, Quaternion.identity);
        myButton = newButton;
        myButton.transform.parent = gameObject.transform;
    }
    public void DeleteButton()
    {
        Destroy(myButton);
    }

    // remove buttons when runaway
    private void DeleteButtons()
    {
        Transform[] childs = gameObject.GetComponentsInChildren<Transform>();
        foreach(Transform child in childs)
        {
            if (child.gameObject != gameObject)
                Destroy(child.gameObject);
        }
    }
    /*    IEnumerator PlayXSheet()
        {   // Caller : EnemyButtonManager > InputProcess
            xSheet.transform.position = myButton.transform.position;
            xSheet.SetActive(true);
            xSheet_anim.SetTrigger("play");
            yield return new WaitForSeconds(0.2f);
            // after x play
            xSheet.SetActive(false);
        }*/
    private void DieProcess()
    {
        Destroy(myButton);
        die = true;
        interaction = false;
        
        // Į��� ���߿��� �״°� ó���ؾ��ؼ� �Ϲ�ȭ �ȵ�

        /*animator.SetBool("die", true);
        // Destroy after 3 seconds
        Destroy(gameObject, 3f);*/
    }
    IEnumerator RunAwayProcess()
    {
        interaction = false;
        animator.SetBool("turn", true); 
        Vector2 start = gameObject.transform.position;

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f)
            yield return new WaitForSeconds(0.3f);

        animator.SetBool("runaway", true);
        while (gameObject.transform.position.x - start.x < 150f)
        {
            gameObject.transform.Translate(Vector2.right * runSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
        yield return null;
    }

}
