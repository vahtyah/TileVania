using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    private void Start()
    {
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" )
    //    {
    //        GameSession.instance.IncementScore();
    //        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
    //        Destroy(gameObject);
    //        gameObject.SetActive(false);
    //    }
    //}
}
