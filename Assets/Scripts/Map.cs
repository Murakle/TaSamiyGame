using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject Enemy1;
    [SerializeField] private int count1;
    [SerializeField] private GameObject Enemy2;
    [SerializeField] private int count2;
    [SerializeField] private GameObject Enemy3;
    [SerializeField] private int count3;

    void Start()
    {
        Generate(Enemy1, count1);
//        Generate(Enemy2, count2);
//        Generate(Enemy3, count3);
    }

    private void Generate(GameObject Enemy, int count)
    {
        for (int i = 0; i < count1; i++)
        {
            float w = GetComponent<RectTransform>().rect.width;
            float h = GetComponent<RectTransform>().rect.height;
            Vector2 p = new Vector3(w * Random.Range(0.0f, 1.0f) - w / 2,
                h * Random.Range(0.0f, 1.0f) - h / 2, 0);

            var EnemyCopy = Instantiate(Enemy, p, Quaternion.identity);
            EnemyCopy.transform.SetParent(GameObject.FindWithTag("Enemies").transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}