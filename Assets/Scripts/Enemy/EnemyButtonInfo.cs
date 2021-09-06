using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonInfo : MonoBehaviour
{
    // 0 : A, 1 : S, 2 : D
    [SerializeField] private GameObject[] buttonPrefabs = new GameObject[3];

    private EnemyController core; // º»Ã¼

    private Queue<int> indexes;
    private Queue<GameObject> buttons;

    private void Start()
    {
        core = gameObject.GetComponent<EnemyController>();
        
    }

    private void EnqueButtons()
    {
        for (int i = 0; i < core.GetMaxHealth(); i++)
        {
            int rand = Random.Range(0, 3);
            indexes.Enqueue(rand);
            GameObject newButton = Instantiate(buttonPrefabs[rand]);
            buttons.Enqueue(newButton);

            newButton.SetActive(false);
        }
    }

    public void ShowButtons(int num)
    {

    }

}
