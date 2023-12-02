using System.Collections;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _trashPrefabs;
    [SerializeField] int _maxTrashNumberAtEasiestLevel = 5;
    [SerializeField] int _maxTrashNumberAtHardestLevel = 15;
    [SerializeField] int _durationInSecondsUntilHardestLevel = 90;
    public int trashCount;
    public static int score;

    private int _maxTrashNumber;
    
    private void Start()
    {
        trashCount = 0;
        score = 0;
        _maxTrashNumber = _maxTrashNumberAtEasiestLevel;
        StartCoroutine(IncrementMaxTrashNumber());
    }

    private void FixedUpdate()
    {
        if (trashCount < _maxTrashNumber)
        {
            SpawnTrash();
        }
    }

    IEnumerator IncrementMaxTrashNumber()
    {
        while (_maxTrashNumber < _maxTrashNumberAtHardestLevel)
        {
            yield return new WaitForSeconds(_durationInSecondsUntilHardestLevel / (_maxTrashNumberAtHardestLevel - _maxTrashNumberAtEasiestLevel));
            _maxTrashNumber++;
        }
    }

    private void SpawnTrash()
    {
        // which viewport position to spawn along a border of the screen
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition;

        // which border of the screen to spawn
        int edge = Random.Range(0, 4);
        if (edge ==0) {
            viewportSpawnPosition = new Vector2(offset, 0);
        } else if (edge == 1) {
            viewportSpawnPosition = new Vector2(offset, 1);
        } else if (edge == 2) {
            viewportSpawnPosition = new Vector2(0, offset);
        } else {
            viewportSpawnPosition = new Vector2(1, offset);
        }
        
        // instantiates a random trash prefab
        int i = Random.Range(0, _trashPrefabs.Length);
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        Instantiate(_trashPrefabs[i], worldSpawnPosition, Quaternion.identity);
        trashCount++;
    }

    public int GetNumberOfTrashPrefabs() // to be able to test that the prefabs have been implemented in the scene (see GameSceneWellConfigured.cs)
    {
        return _trashPrefabs.Length;
    }

    public void SetTrashPrefabs(GameObject[] trashPrefabs)
    {
        _trashPrefabs = trashPrefabs;
    }
}