using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameSceneWellConfiguredTests
{
    [SetUp]
    public void Setup()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Game Scene.unity");
    }

    [TearDown]
    public void TearDown()
    {
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
    }

    [Test]
    public void PlayerIsWellPositioned()
    {
        GameObject ship = GameObject.Find("Ship");
        Assert.That(ship.transform.position, Is.EqualTo(Vector3.zero));
    }

    [Test]
    public void AllNecessaryScriptsAreImplemented()
    {
        Assert.That(GameObject.FindObjectOfType<WrapAroundScreen>().isActiveAndEnabled);
        Assert.That(GameObject.FindObjectOfType<PlayerMoveForward>().isActiveAndEnabled);
        Assert.That(GameObject.FindObjectOfType<PlayerRotate>().isActiveAndEnabled);
        Assert.That(GameObject.FindObjectOfType<Bullet>().isActiveAndEnabled);
    }

    [Test]
    public void TrashPrefabsAreImplementedInTheSpawner()
    {
        TrashSpawner trashSpawner = GameObject.FindObjectOfType<TrashSpawner>();
        Assert.That(trashSpawner.GetNumberOfTrashPrefabs, Is.GreaterThan(0));
    }

    [Test]
    public void AllRequiredTagsAreImplemented()
    {
        GameObject ship = GameObject.Find("Ship");
        GameObject trashPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Trash.prefab");
        var trash = Object.Instantiate(trashPrefab, Vector3.zero, Quaternion.identity);
        GameObject trash2Prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Trash 2.prefab");
        var trash2 = Object.Instantiate(trash2Prefab, Vector3.zero, Quaternion.identity);
        GameObject trash3Prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Trash 3.prefab");
        var trash3 = Object.Instantiate(trash3Prefab, Vector3.zero, Quaternion.identity);

        Assert.That(ship.CompareTag("Player"));
        Assert.That(trash.CompareTag("Trash"));
        Assert.That(trash2.CompareTag("Trash"));
        Assert.That(trash3.CompareTag("Trash"));
    }
}
