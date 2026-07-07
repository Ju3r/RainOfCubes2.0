using TMPro;
using UnityEngine;

public abstract class SpawnerView<T> : MonoBehaviour where T : PoolObject
{
    [SerializeField] protected Spawner<T> _spawner;
    [SerializeField] private TMP_Text _spawnedText;
    [SerializeField] private TMP_Text _createdText;
    [SerializeField] private TMP_Text _activeText;

    private string _startSpawnedText = "Заспавнено: ";
    private string _startCreatedText = "Создано: ";
    private string _startActiveText = "Активно: ";

    private void OnEnable()
    {
        _spawner.Created += RedrawCreatedText;
        _spawner.Spawned += RedrawSpawnedText;
        _spawner.RecalculatedActive += RedrawActiveText;
    }

    private void OnDisable()
    {
        _spawner.Created -= RedrawCreatedText;
        _spawner.Spawned -= RedrawSpawnedText;
        _spawner.RecalculatedActive -= RedrawActiveText;
    }

    private void RedrawCreatedText()
    {
        _createdText.text = _startCreatedText + _spawner.CreatedCount;
    }

    private void RedrawSpawnedText()
    {
        _spawnedText.text = _startSpawnedText + _spawner.SpawnedCount;
    }

    private void RedrawActiveText()
    {
        _activeText.text = _startActiveText + _spawner.ActiveCount;
    }
}
