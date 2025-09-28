using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypeText : MonoBehaviour
{

    [SerializeField, TextArea]
    string text;
    [SerializeField]
    TextMeshProUGUI tmp;

    [SerializeField]
    GameObject finished;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Coroutine());
    }

    private void Update()
    {
        if (finished.activeInHierarchy && (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)))
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

    void Finish()
    {
        finished.SetActive(true);
    }
    IEnumerator Coroutine()
    {

        for (int i = 0; i < text.Length; i++)
        {
            tmp.text = text[..i];
            yield return new WaitForSeconds(.03f);
        }
        Finish();

        
    }
}
