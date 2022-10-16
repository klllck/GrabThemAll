using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] private Player player;
    [SerializeField] private Coin coin;
    [SerializeField] private Spike spike;
    [SerializeField] private int coinsAmount;
    [SerializeField] private int spikesAmount;

    private float fieldWidth;
    private float fieldHeight;

    public int CoinsAmount { get => coinsAmount; }

    private void Start()
    {
        var gameFieldBorders = Camera.main.GetComponent<BoxCollider2D>();
        fieldWidth = gameFieldBorders.size.x / 2;
        fieldHeight = gameFieldBorders.size.y / 2;

        Init(coinsAmount, spikesAmount);
    }

    public void Init(int coinsAmount, int spikesAmount)
    {
        ClearField();
        Instantiate(player, transform.position, Quaternion.identity);
        Spawn(coin.gameObject, coinsAmount);
        Spawn(spike.gameObject, spikesAmount);
        this.coinsAmount = coinsAmount;
        this.spikesAmount = spikesAmount;
    }

    private void Spawn(GameObject gameObject, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(gameObject, new Vector2(Random.Range(-fieldWidth, fieldWidth), Random.Range(-fieldHeight, fieldHeight)), Quaternion.identity);
        }
    }

    private void ClearField()
    {
        foreach (var item in FindObjectsOfType<Coin>())
            Destroy(item.gameObject);
        foreach (var item in FindObjectsOfType<Spike>())
            Destroy(item.gameObject);
    }
}
