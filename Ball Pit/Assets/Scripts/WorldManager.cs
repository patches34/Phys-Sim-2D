using UnityEngine;

public class WorldManager : MonoBehaviour
{
    Vector2 screenSize = Vector2.zero;

    [SerializeField]
    PhysicsObject ballPrefab;

    [SerializeField]
    int spawnCount;

    [SerializeField]
    Transform spawnPool;

    // Start is called before the first frame update
    void Start()
    {
        SetScreenSize();

        for(int i = 0; i < spawnCount; i++)
        {
            SpawnBall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBall()
    {
        if(spawnPool.childCount < 1 || spawnPool.GetChild(0).gameObject.activeInHierarchy)
        {
            Vector3 spawnPos = Vector3.zero;
            spawnPos.x = Random.Range(-screenSize.x, screenSize.x);
            spawnPos.y = Random.Range(-screenSize.y, screenSize.y);

            PhysicsObject newBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity, spawnPool);

            newBall.worldManager = this;

            newBall.name = ballPrefab.name + "_" + spawnPool.childCount;

            newBall.transform.SetAsLastSibling();
        }
        else
        {
            spawnPool.GetChild(0).gameObject.SetActive(true);

            spawnPool.GetChild(0).SetAsLastSibling();
        }
    }

    #region Screen Size Stuff
    void SetScreenSize()
    {
        screenSize.y = Camera.main.orthographicSize;
        screenSize.x = screenSize.y * Camera.main.aspect;
    }

    //  Only works with script on Canvas Object
    //  https://docs.unity3d.com/Packages/com.unity.ugui@2.0/api/UnityEngine.EventSystems.UIBehaviour.html#UnityEngine_EventSystems_UIBehaviour_OnRectTransformDimensionsChange
    private void OnRectTransformDimensionsChange()
    {
        if (Camera.main != null)
        {
            SetScreenSize();
        }
    }

    public void CheckForScreenEdge(ref Vector3 position)
    {
        if (position.x > screenSize.x)
        {
            position.x = -screenSize.x;
        }
        else if (position.x < -screenSize.x)
        {
            position.x = screenSize.x;
        }

        if (position.y > screenSize.y)
        {
            position.y = -screenSize.y;
        }
        else if (position.y < -screenSize.y)
        {
            position.y = screenSize.y;
        }
    }


    #endregion
}
