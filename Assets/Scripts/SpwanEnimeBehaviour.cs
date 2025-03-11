using System.Collections.Generic;
using UnityEngine;

public class SpwanEnimeBehaviour : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Transform spawnPoint;
    private int currentEnemi=0;
    private float currentTime=0;
    private int enemiCount = 0;



    // Update is called once per frame
    void Update()
    {


 
        currentTime += Time.deltaTime;
        if(currentTime > 3 && enemiCount < 22){
            currentEnemi = Random.Range(0, 11);
            if(currentEnemi <= 7 ){
                currentEnemi = 0;
            }
            else{
                currentEnemi = 1;
            }

            Instantiate(enemies[currentEnemi], spawnPoint.position, Quaternion.identity);
            enemiCount++;
            currentTime = 0;
        }
        
    }

}
