using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject corpsPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnCorpses(Transform transform, EntityStatistics _statistics, Sprite sprite)
    {
        CorpsController _controller = Instantiate(corpsPrefab, transform.position, Quaternion.identity).GetComponent<CorpsController>();
        _controller._statistics = _statistics;
        _controller.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        _controller.transform.localScale = transform.GetChild(0).localScale;
        Destroy(transform.gameObject);
    }
}
