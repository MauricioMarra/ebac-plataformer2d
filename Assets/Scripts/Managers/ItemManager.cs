using TMPro;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public int coins;
    public TextMeshProUGUI coinCountText;


    // Start is called before the first frame update
    void Start()
    {
        this.coins = 0;
        coinCountText.text = coins.ToString();
    }

    public void AddCoin(int amount = 1)
    {
        coins += amount;
        coinCountText.text = coins.ToString();
    }
}
