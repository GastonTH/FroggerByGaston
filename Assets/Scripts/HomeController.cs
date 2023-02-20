using UnityEngine;

public class HomeController : MonoBehaviour
{    
    void OnTriggerEnter2D( Collider2D col )
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject frog = GameObject.Find("Frogger");
            GameObject GM = GameObject.Find("GameManager");
            transform.GetChild(0).gameObject.SetActive(true);
            frog.gameObject.transform.position = new Vector3(0f,-10f,0f); // deberia mover al sapo al inicio de la partida
            GM.gameObject.GetComponent<GameManagerController>().AddHomeCount();
            transform.gameObject.GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        }
    }
}
