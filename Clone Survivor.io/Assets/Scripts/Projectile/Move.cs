using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] GameObject[] bullet;
    [SerializeField] Transform spawnPos;
    [SerializeField] Transform targetPos;
    [SerializeField] float normalSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] GameObject[] spawnBullet;
    bool hasSpawn = false;
    bool isMoveToTarget = false;
    public float rotateTime = 1f;
    float counter = 0;
    

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isMoveToTarget = false;
            for(int i = 0; i < 3; i++)
            {
                spawnBullet[i] = Instantiate(bullet[i], spawnPos.transform.position, Quaternion.identity);
                
            }
            hasSpawn = true;
            spawnBullet[0].transform.Rotate(new Vector3(0,0,-45));
            spawnBullet[1].transform.Rotate(new Vector3(0, 0, 0));
            spawnBullet[2].transform.Rotate(new Vector3(0, 0, 45));
        }
        if (hasSpawn)
        {
            
            spawnBullet[0].transform.Translate(normalSpeed * Time.deltaTime, 0, 0);
            spawnBullet[1].transform.Translate(normalSpeed * Time.deltaTime, 0, 0);
            spawnBullet[2].transform.Translate(normalSpeed * Time.deltaTime, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            isMoveToTarget = true;
        }
        if(isMoveToTarget)
        {
            spawnBullet[0].transform.position = Vector3.MoveTowards(spawnBullet[0].transform.position, targetPos.position, Time.deltaTime * maxSpeed);
            spawnBullet[1].transform.position = Vector3.MoveTowards(spawnBullet[1].transform.position, targetPos.position, Time.deltaTime * maxSpeed);
            spawnBullet[2].transform.position = Vector3.MoveTowards(spawnBullet[2].transform.position, targetPos.position, Time.deltaTime * maxSpeed);
        }
    }
}
