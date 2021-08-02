using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeart : MonoBehaviour
{

    public int healthValue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //se colidir com um obejto na cena com a tag player
        if (collision.gameObject.tag == "Player")
        {
            //pega o componente do player, o metodo increase life com a variavel healthvalue, depois detroi o objeto
            collision.gameObject.GetComponent<Player>().IncreaseLife(healthValue);
            Destroy(gameObject);
        }
    }


}
