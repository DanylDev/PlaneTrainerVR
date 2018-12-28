using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGeneratorManager : MonoBehaviour
{
    [SerializeField] private List<Button> allButtons = new List<Button>();
    [SerializeField] private GameObject menuButtonPrefab;

    private void Awake()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);

            if (sceneName.Contains("Menu"))
                continue;

            var NewButtonGO = Instantiate(menuButtonPrefab, transform);

            NewButtonGO.gameObject.name = sceneName + "_Button";
            NewButtonGO.GetComponentInChildren<Text>().text = sceneName;
            NewButtonGO.GetComponentInChildren<Button>().onClick.AddListener(() => LoadScene(sceneName));

            allButtons.Add(NewButtonGO.GetComponent<Button>());
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}