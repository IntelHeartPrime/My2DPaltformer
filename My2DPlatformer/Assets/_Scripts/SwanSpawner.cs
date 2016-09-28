using UnityEngine;
using System.Collections;

public class SwanSpawner : MonoBehaviour {

    public Rigidbody2D prop;
    public float leftSpawnPosX;
    public float rightSpawnPosX;
    public float minSpawnPosY;
    public float maxSpawnPosY;
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    public float minSpeed;
    public float maxSpeed;

	// Use this for initialization
	void Start () {
        Random.InitState(System.DateTime.Today.Millisecond);        //设置随机数种子
        StartCoroutine("Spawn");
	}
	IEnumerator Spawn()
    {
        float waitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        yield return new WaitForSeconds(waitTime);
        bool facingLeft = Random.Range(0, 2) == 0;      //是否面向左方
        float posX = facingLeft ? rightSpawnPosX : leftSpawnPosX;
        float posY = Random.Range(minSpawnPosY, maxSpawnPosY);
        Vector3 spawnPox = new Vector3(posX, posY, transform.position.z);
        Rigidbody2D propInstance = Instantiate(prop, spawnPox, Quaternion.identity) as Rigidbody2D;    //Perfabs的对象-->Rigidbody@D。可以通过Rigidbody2d的vecitoy属性进行运动
        if (!facingLeft)
        {
            Vector3 scale = propInstance.transform.localScale;
            scale.x *= -1;    //翻转
            propInstance.transform.localScale = scale;

        }
        float speed = Random.Range(minSpeed, maxSpeed);
        speed *= facingLeft ? -1f : 1f;
        propInstance.velocity = new Vector2(speed, 0);
        StartCoroutine(Spawn());    //递归协程
        while (propInstance != null)
        {
            //销毁对象的判断
            if (facingLeft)
            {
                if (propInstance.transform.position.x < leftSpawnPosX - 0.5f)
                {
                    Destroy(propInstance.gameObject);
                }
            }
            else
            {
                if (propInstance.transform.position.x > rightSpawnPosX + 0.5f)
                {
                    Destroy(propInstance.gameObject);
                }
            }
            //结束协程
            yield return null;
        }


    }
	// Update is called once per frame
	void Update () {
	    
	}
}
