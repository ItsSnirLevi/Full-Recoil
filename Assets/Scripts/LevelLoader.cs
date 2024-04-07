using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    private void Awake()
    {
        instance = this;
    }

    public Animator transition;
    public float transitionTime = 1f;

	public void LoadNext(int levelIndex)
    {
        StartCoroutine(LoadLevelCo(levelIndex));
    }

    IEnumerator LoadLevelCo(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

}
