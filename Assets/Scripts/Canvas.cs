using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    static Canvas instance;
    int max;
    int current;
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    CanvasGroup group;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Cubes Sorted: " + (DeliveryTrigger.numTriggers - DeliveryTrigger.triggers.Count) + "/" + DeliveryTrigger.numTriggers;

        if (group.alpha == 1)
        {
            if (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space))
            {
                var s = SceneManager.GetActiveScene();

                var sc = SceneUtility.GetScenePathByBuildIndex(s.buildIndex + 1);
                if (sc.Length <= 0)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(s.buildIndex + 1);
                }

            }
        }
    }

    public static void SetCubes(int amount, int max = -1)
    {
        if (max != -1)
        {
            instance.max = max;
        }
        instance.current = amount;
        instance.text.text = "Cubes Sorted: " + instance.current + "/" + instance.max;
    }
    public static void SetWin()
    {
        instance.group.alpha = 1;
    }
}
