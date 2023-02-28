using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] Transform initiTransform;
    [SerializeField] GameObject DieCanvas;
    bool died = false;
    public void GameOver()
    {
        DieCanvas.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<FPSController>().enabled = false;
        player.GetComponent<Shooter>().enabled = false;
        died = true;
    }



    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && died)
        {
            Restart();
        }
    }
    void Restart()
    {
        DieCanvas.SetActive(false);
        player.GetComponent<PlayerHealthSystem>().restart();
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = initiTransform.position;
        player.transform.rotation = initiTransform.rotation;
        player.GetComponent<PlayerHealthSystem>().restart();
        player.GetComponent<FPSController>().enabled = true;
        player.GetComponent<Shooter>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        died = false;
    }




}
