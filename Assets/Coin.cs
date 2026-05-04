
using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource audioSource;
    private LevelManager level;
    [SerializeField] private AudioClip coinfall;
    void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
        level = FindObjectOfType<LevelManager>();
    }
    void Start()
    {


        audioSource.PlayOneShot(coinfall);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(coinfall);
            level.AddCoin();
            Destroy(gameObject, 0.5f);
        }
    }
}
