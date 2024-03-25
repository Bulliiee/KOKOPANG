using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Image Panel;

    private float time = 0f;
    private float F_time = 0.5f;
    void Update()
    {
        StartCoroutine(FadeFlow());
    }
    public void GoToLobby()
    {
        SceneManager.LoadScene("MainScene");
    }

    IEnumerator FadeFlow()
    {
        Color alpha = Panel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
    }
}
