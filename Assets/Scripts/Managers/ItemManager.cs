using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public int coins;

    // Start is called before the first frame update
    void Start()
    {
        this.coins = 0;
    }

    public void AddCoin(int amount = 1)
    {
        coins += amount;
    }

}
