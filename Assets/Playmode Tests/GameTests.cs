using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class GameTests : InputTestFixture
{
    GameObject _ship;
    Rigidbody2D _rb;
    GameObject _mainCamera;
    PlayerMoveForward _playerMoveForward;
    PlayerRotate _playerRotate;
    GameObject _shipPrefab;
    GameObject _trashPrefab;
    GameObject _trash2Prefab;
    GameObject _trash3Prefab;
    GameObject _bulletPrefab;
    float _shipInitialYPosition;
    float _shipInitialZRotation;
    Keyboard _keyboard;
    Gamepad _gamepad;
    
    [SetUp]
    public void SetUp()
    {
        _shipPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Ship.prefab");
        _trashPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Trash.prefab");
        _trash2Prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Trash 2.prefab");
        _trash3Prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Trash 3.prefab");
        _bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Bullet.prefab");
        _ship = Object.Instantiate(_shipPrefab, Vector3.zero, Quaternion.identity);
        _ship.tag = "Player";
        _mainCamera = new GameObject();
        _mainCamera.AddComponent<Camera>();
        _mainCamera.tag = "MainCamera";
        _rb = _ship.AddComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _playerMoveForward = _ship.AddComponent<PlayerMoveForward>();
        _playerMoveForward.Initialize(_rb);
        _playerRotate = _ship.AddComponent<PlayerRotate>();
        _playerRotate.Initialize(_rb);
        _shipInitialYPosition = _rb.position.y;
        _shipInitialZRotation = _ship.transform.rotation.z;
        _keyboard = InputSystem.AddDevice<Keyboard>();
        _gamepad = InputSystem.AddDevice<Gamepad>();
    }

    [TearDown]
    public override void TearDown()
    {
        Object.Destroy(_ship);
    }
    
    [UnityTest]
    public IEnumerator MainCameraIsSet()
    {
        yield return new WaitForSeconds(0.1f);
        Assert.That(GameObject.FindGameObjectWithTag("MainCamera"), Is.Not.Null);
    }

    [UnityTest]
    public IEnumerator PlayerCanMoveForwardWithWKey()
    {
        Press(_keyboard.wKey);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_rb.position.y, Is.GreaterThan(_shipInitialYPosition));
    }

    [UnityTest]
    public IEnumerator PlayerCanMoveForwardWithUpArrowKey()
    {
        Press(_keyboard.upArrowKey);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_rb.position.y, Is.GreaterThan(_shipInitialYPosition));
    }

    [UnityTest]
    public IEnumerator PlayerCanMoveForwardWithGamepadLeftStickUp()
    {
        Press(_gamepad.leftStick.up);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_rb.position.y, Is.GreaterThan(_shipInitialYPosition));
    }

    [UnityTest]
    public IEnumerator PlayerCanMoveForwardWithGamepadDPadUp()
    {
        Press(_gamepad.dpad.up);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_rb.position.y, Is.GreaterThan(_shipInitialYPosition));
    }

    [UnityTest]
    public IEnumerator PlayerCanRotateLeftWithAKey()
    {
        Press(_keyboard.aKey);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_ship.transform.rotation.z, Is.GreaterThan(_shipInitialZRotation));
    }

    [UnityTest]
    public IEnumerator PlayerCanRotateLeftWithLeftArrowKey()
    {
        Press(_keyboard.leftArrowKey);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_ship.transform.rotation.z, Is.GreaterThan(_shipInitialZRotation));
    }

    [UnityTest]
    public IEnumerator PlayerCanRotateLeftWithGamepadLeftStickLeft()
    {
        Press(_gamepad.leftStick.left);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_ship.transform.rotation.z, Is.GreaterThan(_shipInitialZRotation));
    }

    [UnityTest]
    public IEnumerator PlayerCanRotateLeftWithGamepadDPadLeft()
    {
        Press(_gamepad.dpad.left);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_ship.transform.rotation.z, Is.GreaterThan(_shipInitialZRotation));
    }

    [UnityTest]
    public IEnumerator PlayerCanRotateRightWithDKey()
    {
        Press(_keyboard.dKey);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_ship.transform.rotation.z, Is.LessThan(_shipInitialZRotation));
    }

    [UnityTest]
    public IEnumerator PlayerCanRotateRightWithRightArrowKey()
    {
        Press(_keyboard.rightArrowKey);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_ship.transform.rotation.z, Is.LessThan(_shipInitialZRotation));
    }

    [UnityTest]
    public IEnumerator PlayerCanRotateLRightWithGamepadLeftStickRight()
    {
        Press(_gamepad.leftStick.right);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_ship.transform.rotation.z, Is.LessThan(_shipInitialZRotation));
    }

    [UnityTest]
    public IEnumerator PlayerCanRotateRightWithGamepadDPadRight()
    {
        Press(_gamepad.dpad.right);
        yield return new WaitForSeconds(0.1f);
        Assert.That(_ship.transform.rotation.z, Is.LessThan(_shipInitialZRotation));
    }

    [UnityTest]
    public IEnumerator TrashSpawnerIsSpawningMovingTrash()
    {
        GameObject[] trashPrefabs = { _trashPrefab, _trash2Prefab, _trash3Prefab };
        GameObject spawner = new GameObject();
        TrashSpawner trashSpawner = spawner.AddComponent<TrashSpawner>();
        trashSpawner.SetTrashPrefabs(trashPrefabs);

        yield return new WaitForSeconds(0.1f);
        GameObject spawnedTrash = GameObject.FindGameObjectWithTag("Trash");
        Vector2 trashVelocity = spawnedTrash.GetComponent<Rigidbody2D>().velocity;
        Assert.That(spawnedTrash, Is.Not.Null);
        Assert.That(trashVelocity.magnitude, Is.GreaterThan(0));
    }

    [UnityTest]
    public IEnumerator WhenBulletHitsTrashBothAreDestroyedAndScoreIsIncremented()
    {
        GameObject[] trashPrefabs = { _trashPrefab, _trash2Prefab, _trash3Prefab };
        GameObject spawner = new GameObject();
        TrashSpawner trashSpawner = spawner.AddComponent<TrashSpawner>();
        trashSpawner.SetTrashPrefabs(trashPrefabs);
        GameObject trash = GameObject.Instantiate(_trashPrefab, new Vector3(1, 0, 0), Quaternion.identity);
        BulletPrefab bulletPrefab = _bulletPrefab.GetComponent<BulletPrefab>();
        bulletPrefab.Initialize(trashSpawner);
        GameObject bullet = Object.Instantiate(_bulletPrefab, trash.transform.position, Quaternion.identity);
        int previousScore = TrashSpawner.score;
        
        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(trash == null);
        Assert.IsTrue(bullet == null);
        Assert.That(TrashSpawner.score, Is.GreaterThan(previousScore));
    }

    [UnityTest]
    public IEnumerator WhenTrashHitsPlayerBothAreDestroyedGameIsOver()
    {
        GameObject[] trashPrefabs = { _trashPrefab, _trash2Prefab, _trash3Prefab };
        GameObject spawner = new GameObject();
        TrashSpawner trashSpawner = spawner.AddComponent<TrashSpawner>();
        trashSpawner.SetTrashPrefabs(trashPrefabs);
        GameObject trash = GameObject.Instantiate(_trashPrefab, _ship.transform.position, Quaternion.identity);
        Collider2D shipCollider = _ship.AddComponent<CircleCollider2D>();
        shipCollider.isTrigger = true;

        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(trash == null);
        Assert.IsTrue(_ship == null);
    }
}
