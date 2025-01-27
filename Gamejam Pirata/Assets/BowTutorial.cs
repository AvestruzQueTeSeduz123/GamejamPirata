using UnityEngine;

public class BowTutorial : MonoBehaviour
{
    public GameObject arrow;

    public GameObject bigArrow;
    public float lauchForce;
    public Transform shotPoint;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;

    Vector2 direction;
    public bool isShotting;
    Player player;
    [SerializeField]private bool canShoot = false;
    public bool unlockedBigShoot = true;
    public bool unlockedLightShoot = true;

    [SerializeField] private GameObject pointsContainer; // Contêiner para os pontos
    [SerializeField] private DialogueUI dialogueUI;

    void Start()
    {
        // Certifique-se de que há um contêiner na cena, caso contrário, crie um dinamicamente
        if (pointsContainer == null)
        {
            pointsContainer = new GameObject("PointsContainer");
        }

        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
            points[i].transform.SetParent(pointsContainer.transform); // Define como filho do contêiner
        }

        isShotting = false;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {

        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        transform.right = direction;

        if (Input.GetMouseButtonDown(0) && isShotting == false && canShoot == true && dialogueUI.isWriting == false && unlockedLightShoot == true)
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1) && isShotting == false && canShoot == true && unlockedBigShoot == true && dialogueUI.isWriting == false)
        {
            BigShoot();
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }

    void Shoot()
    {
        if(canShoot == true)
        {
            GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().linearVelocity = transform.right * lauchForce;
        isShotting = true;

        player.LifeUpdate();
        }
    }

    void BigShoot()
    {
        GameObject newArrow = Instantiate(bigArrow, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().linearVelocity = transform.right * lauchForce;
        isShotting = true;

        player.LifeUpdateBig();
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shotPoint.position + (direction.normalized * lauchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }

    public void CanShoot()
    {
        canShoot = true;
    }
}
