using NUnit.Framework;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSceneWellConfiguredTests
{
    [SetUp]
    public void Setup()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/GameOver.unity");
    }

    [TearDown]
    public void TearDown()
    {
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
    }

    [Test]
    public void CanvasHasTextAndButton()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        TextMeshProUGUI text = canvas.GetComponentInChildren<TextMeshProUGUI>();
        Button button = canvas.GetComponentInChildren<Button>();
        Assert.That(button.isActiveAndEnabled);
        Assert.That(!text.transform.IsChildOf(button.transform));
    }

    [Test]
    public void CanvasContainsScoreScript()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        Assert.That(canvas.GetComponent<Score>(), Is.Not.Null);
    }

    [Test]
    public void ButtonOnClickIsSetToCallAFunction()
    {
        Button button = GameObject.FindObjectOfType<Button>();
        Assert.That(button.onClick.GetPersistentMethodName(0), Is.Not.Empty);
    }
}
