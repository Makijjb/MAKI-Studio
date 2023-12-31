using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenStarter : MonoBehaviour
{
    public GameObject MovePoint;
    public bool Cooking = false; 
    public bool isInRange;
    public KeyCode interactKey;
    //public UnityEvent interactAction;
    private FoodObjectPool objectPool;
    private SteakPool Spool;
    private SaladPool Sapool;
    private ChickenPool CPool;
    private Collider2D foodCollision;
    private int foodid = 0;
    //Animation Variables
    public Animator ovencook;
    public float timer; 

    // Start is called before the first frame update

    void Start(){
        objectPool = FindAnyObjectByType<FoodObjectPool>();
        Spool = FindAnyObjectByType<SteakPool>();
        Sapool = FindAnyObjectByType<SaladPool>();
        CPool = FindAnyObjectByType<ChickenPool>();

    }

    // Update is called once per frame
    public void Update(){
        if(isInRange && foodid==1){
            if(Input.GetKeyDown(interactKey)){
                objectPool.ReturnFood(foodCollision.gameObject);
            }
        }

        else if(isInRange&&foodid==2){
            if(Input.GetKeyDown(interactKey)){
                Steak SteakComponent = foodCollision.GetComponent<Steak>();
                if(SteakComponent.isCookable==true&&Cooking==false){
                    Cooking = true;
                    ovencook.SetBool("OvenOn",true);
                    SteakComponent.cooked=true;
                    SteakComponent.PlayCookingAnimation();
                    foodCollision.transform.position = MovePoint.transform.position;
                }else{
                    Debug.Log("Please remove food item");
                }
            }
        }

        else if(isInRange&&foodid==3){
            if(Input.GetKeyDown(interactKey)){
                Debug.Log("This cannot go in the oven!");
            }
        }

        else if(isInRange&&foodid==4){
                if(Input.GetKeyDown(interactKey)){
                Chicken chickenComponent = foodCollision.GetComponent<Chicken>();
                if(chickenComponent.isCookable==true&&Cooking==false){
                    Cooking = true;
                    ovencook.SetBool("OvenOn",true);
                    chickenComponent.cooked=true;
                    chickenComponent.PlayCookingAnimation();
                    foodCollision.transform.position = MovePoint.transform.position;
                }else{
                    Debug.Log("Please remove food item");
                }
            }
        }
        else if (!isInRange && Cooking == true){
            if (Input.GetKeyDown(interactKey)){
                // Define the box area based on the MovePoint's collider
                Collider2D[] colliders = Physics2D.OverlapBoxAll(
                    MovePoint.transform.position,
                    MovePoint.GetComponent<Collider2D>().bounds.size,
                    0f
                );

                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Steak")||collider.CompareTag("Chicken"))
                    {
                        collider.transform.position = this.transform.position;
                        Cooking = false;
                        ovencook.SetBool("OvenOn",false);
                    }
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Food")){
            isInRange = true;
            foodCollision = collision;
            foodid = 1;
            Debug.Log("Food is now in range");
        }else if(collision.gameObject.CompareTag("Steak")){
            isInRange = true;
            foodCollision = collision;
            foodid=2;
            Debug.Log("Steak is now in range");
        }else if(collision.gameObject.CompareTag("Salad")){
            isInRange = true;
            foodid=3;
            foodCollision = collision;
            Debug.Log("Salad is now in range");
        }else if(collision.gameObject.CompareTag("Chicken")){
            isInRange = true;
            foodCollision = collision;
            foodid=4;
            Debug.Log("Chicken is now in range");
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Food")){
            isInRange = false;
            foodid=0;
            Debug.Log("Food is now out of range");
        }else if(collision.gameObject.CompareTag("Steak")){
            isInRange = false;
            foodCollision = collision;
            foodid=0;
            Debug.Log("Steak is now in range");
        }else if(collision.gameObject.CompareTag("Salad")){
            isInRange = false;
            foodid=0;
            foodCollision = collision;
            Debug.Log("Salad is now out of range");
        }else if(collision.gameObject.CompareTag("Chicken")){
            isInRange = false;
            foodCollision = collision;
            foodid=0;
            Debug.Log("Chicken is out of range");
        }

    }
}

