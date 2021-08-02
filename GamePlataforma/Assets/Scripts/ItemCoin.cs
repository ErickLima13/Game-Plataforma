using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{

    public int scoreValue;


    //usamos o metodo OnTriggerEnter2D(Collider2D collision), pois o obejto esta como invisivel na cena, para não haver colisoes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.instance.UpdateScore(scoreValue);
            Destroy(gameObject);
        }
    }


}
