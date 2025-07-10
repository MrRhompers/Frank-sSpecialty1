using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonCredits : MonoBehaviour
{
    //goes from start to the first level of the game
    public void BackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
