using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    private Player player;
    public int deaths = 0;
    public Text DeathText;
    public Text LivesText;

    public static GameState instance;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player) player = FindObjectOfType<Player>();
        DeathText.text = "Deaths: "+deaths;
        LivesText.text = "Lives: "+player.health;
    }

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
