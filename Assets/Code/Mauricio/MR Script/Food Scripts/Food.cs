using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour{
    public bool isCookable;
    public float CookingTimer;
    public int points;  
    public bool cooked = false; 

    public virtual void Cookable(){
        isCookable = true; 
    }

    public virtual void Timer(){
        CookingTimer = 5f;
    }

    public virtual void PointAllocation(){
        points = 1;
    }

    public void ChangeCook(bool Input){
        cooked = Input;
    }
}

