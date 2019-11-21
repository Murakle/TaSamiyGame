using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Enemy1;
    [SerializeField] private int count1;
    [SerializeField] private GameObject Enemy2;
    [SerializeField] private int count2;
    [SerializeField] private GameObject Enemy3;
    [SerializeField] private int count3;

    private int x, y;

    void Start()
    {
        Generate(Enemy1, count1);
        Generate(Enemy2, count2);
//        Generate(Enemy3, count3);
    }

    private void Generate(GameObject Enemy, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Debug.Log("Generated Boom");
            float w = GetComponent<RectTransform>().rect.width;
            float h = GetComponent<RectTransform>().rect.height;

            Vector3 p = new Vector3(w * Random.Range(0.0f, 1.0f) - w / 2,
                h * Random.Range(0.0f, 1.0f) - h / 2, -1);
            p += GetComponent<RectTransform>().position;
            var EnemyCopy = Instantiate(Enemy, p, Quaternion.identity);
            EnemyCopy.transform.SetParent(gameObject.transform);
        }
    }

    public void setXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }
}