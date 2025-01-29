using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static bool isPaused = true;

    public GameObject corpsPrefab;
    public GameObject UI;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        UI.SetActive(false);
        isPaused = false;
    }

    public void SpawnCorpses(Transform transform, EntityStatistics _statistics, Sprite sprite, bool player = false)
    {
        CorpsController _controller = Instantiate(corpsPrefab, transform.position, Quaternion.identity).GetComponent<CorpsController>();
        _controller._statistics = _statistics;
        _controller.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        _controller.transform.localScale = transform.GetChild(0).localScale;
        if (!player)
            Destroy(transform.gameObject);
        else
        {
            PlayerController.instance.transform.position = new(-16, -2, 0);
            DialogController.instance.ChangeText();
        }
    }
}
