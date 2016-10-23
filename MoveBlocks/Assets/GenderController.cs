using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenderController : MonoBehaviour
{
    public bool player;

    public Animator an1;
    public Animator an2;

    public Sprite spr1;
    public Sprite spr2;

    public StatsManager stats;


    void Awake()
    {
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
        if(stats.male)
        {
            if (player)
            {
                GetComponent<Animator>().runtimeAnimatorController = an1.runtimeAnimatorController;
                GetComponent<Image>().sprite = spr1;
            }
            else
            {
                GetComponent<Animator>().runtimeAnimatorController = an2.runtimeAnimatorController;
                GetComponent<Image>().sprite = spr2;
            }
        }
        else
        {
            if (player)
            {
                GetComponent<Animator>().runtimeAnimatorController = an2.runtimeAnimatorController;
                GetComponent<Image>().sprite = spr2;
            }
            else
            {
                GetComponent<Animator>().runtimeAnimatorController = an1.runtimeAnimatorController;
                GetComponent<Image>().sprite = spr1;
            }
        }

        
    }
    
	
}
