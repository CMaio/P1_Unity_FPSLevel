using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class barUI : MonoBehaviour
{
    [SerializeField] private Image topBar;
    [SerializeField] private Image underBar;

	public void updateBar(float actualHealth,float finalHealth,bool health)
    {
        float currentHealth;
        float finalStateHealth;
        if (health)
        {
            currentHealth = actualHealth / 100;
            finalStateHealth = currentHealth + (finalHealth / 100);
            finalStateHealth = Mathf.Min(finalStateHealth, 1.0f);
            StartCoroutine(updateBarIE(currentHealth, finalStateHealth, health));


        }else{
            currentHealth = actualHealth / 100;
            finalStateHealth = currentHealth - (finalHealth / 100);
            topBar.fillAmount = finalStateHealth;

            StartCoroutine(updateBarIE(currentHealth, finalStateHealth, health));
        }
    }



    IEnumerator updateBarIE(float currentHealth, float finalStateHealth,bool add)
    {
        if (add)
        {
            while (underBar.fillAmount < finalStateHealth)
            {
                yield return new WaitForSeconds(0.01f);
                underBar.fillAmount += 0.01f;
                Debug.Log(finalStateHealth);
                if(underBar.fillAmount >= 0.9f)
                {
                    topBar.fillAmount = 0.999f;
                }
            }
            
        }else{
            while (underBar.fillAmount > finalStateHealth)
            {
                yield return new WaitForSeconds(0.01f);
                underBar.fillAmount -= 0.01f;
            }
        }
        yield return null;
    }

    public void restartBar()
    {
        topBar.fillAmount = 1.0f;
        underBar.fillAmount = 1.0f;
    }
}
