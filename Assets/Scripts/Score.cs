using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    
    private void Start()
    {
        string score = TrashSpawner.score.ToString();
        _text.text = "Your score is: " + score;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
