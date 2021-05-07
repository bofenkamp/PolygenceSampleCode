using System;
using UnityEngine;

public abstract class Enemy
{
    protected float health;

    protected abstract void StartMoving();

    protected void Die()
    {
        Destroy(this.gameObject);
    }

    protected abstract void Attack();

    protected abstract AnimationController anim;
}

public abstract class Item
{
    protected float inventorySize;
}

public class LizardMan : Enemy
{
    float health;
    float inventorySize = 4f;

    protected override void StartMoving()
    {
        return;
    }

    protected override void Attack()
    {
        DoSomething();
    }
}

public enum KeyCode
{
    up, down, left, right
}

public class CaseSwitchExample
{
    private int numberOfBeans;

    void CaseSwitchBeans()
    {
        switch (numberOfBeans)
        {
            case 0:
                //do something for zero beans;
                break;
            case 1:
                //do something with a bean;
                break;
            default:
                //do something with plural beans
                break;
        }
    }

    private KeyCode[] options;
    private KeyCode keyPressed;

    void CaseSwitchList()
    {
        switch (keyPressed)
        {
            case KeyCode.up:
                //do a thing
                break;
        }
    }
}

/* EXAMPLE OF THE ASSIGNMENT BASED ON MY CODING ARCHITECTURE

 - Ideally, each class would be its own page of code;
   I'm just putting them all here so you can see them all at once

 - Please send this to me in a zip file when you're done so
   I can look at it before our next meeting

 - There isn't one right answer about how to do this,
   we just want to lay out the functions that allow the
   communication between classes that allows the actions
   in the coding architecture to be carried out

 - I put a comment at functions that start an action
   and a Debug.Log at the ends of an action. You don't
   have to do this; I just did it so it would be easier
   for you to read. Do what works best for you.

 - Some of the empty code may feel a bit weird. That's
   fine, as long as it makes sense to you what it's
   generally supposed to do

 - Keep an eye out for anything in my code that looks
   potentially problematic or redundant. There are a
   couple things here that might be...

*/

abstract class Interactable_Object
{
    public void AddToInventory(Inventory inventory)
    {
        inventory.ReceiveItem(this);
    }
}

class Fire : Interactable_Object
{
    private Fire fireToCreate;

    public void AddKindling(Kindling kindling)
    {
        Debug.Log("kindling added to fire");
    }

    //START EXTINGUISHING
    void WaitForFuelToRunOut()
    {
        Extinguish();
    }

    void Extinguish()
    {
        if (IsNoFiresLeft())
            LoseTheGame();
    }

    bool IsNoFiresLeft()
    {
        return something;
    }

    void LoseTheGame()
    {
        Debug.Log("game lost");
    }

    public void CreateNewFire()
    {
        fireToCreate.BeCreated();
    }

    public void BeCreated()
    {
        Beach beachThisIsOn = GetBeachThisIsOn();
        if (beachThisIsOn != null)
            beachThisIsOn.HasFireOnIt(this);
        else
            Debug.Log("Fire created off beach");
    }

    Beach GetBeachThisIsOn()
    {
        return something;
    }

    public void SetIslanderOnFire(Islander islander)
    {
        islander.Ignite();
    }
}

class Kindling : Interactable_Object
{

}

abstract class KindlingSource : Interactable_Object
{
    public Interactable_Object kindlingToProduce;

    public abstract void BeHarvested();

    protected void TurnIntoKindling(Kindling kindling)
    {
        Debug.Log("The kindling source has been turned into kindling");
    }

    protected void TurnIntoOil(Fire oilLamp)
    {
        Debug.Log("The kindling source has been turned into oil");
    }
}

class HarvestableStump : KindlingSource
{
    public override void BeHarvested()
    {
        TurnIntoKindling(kindlingToProduce as Kindling);
    }
}

class HarvestableTree : KindlingSource
{
    public override void BeHarvested()
    {
        TurnIntoKindling(kindlingToProduce as Kindling);
    }
}

class OilBoxes : KindlingSource
{
    public override void BeHarvested()
    {
        TurnIntoOil(kindlingToProduce as Fire);
    }
}

class Islander
{
    private Inventory inventory;

    //START HARVESTING KINDLING OR OIL LAMP SOURCE
    void HarvestKindlingSource(KindlingSource source)
    {
        source.BeHarvested();
    }

    //START ADDING OBJECT TO INVENTORY
    void AddObjectToInventory(Interactable_Object interactable)
    {
        interactable.AddToInventory(inventory);
    }

    //START ADDING KINDLING TO FIRE
    void AddKindlingToFire(Kindling kindling, Fire fire)
    {
        fire.AddKindling(kindling);
    }

    //MOVE AROUND THE ISLAND
    void TravelToLocation(Vector3 location)
    {
        Debug.Log("islander moved to a place");
    }

    //CREATE NEW FIRE
    void CreateNewFire(Fire oilLamp)
    {
        oilLamp.CreateNewFire();
    }

    //START SETTING ISLANDER ON FIRE
    void SetIslanderOnFire(Fire fire, Islander islander)
    {
        fire.SetIslanderOnFire(islander);
    }

    public void Ignite()
    {
        if (Fire.GetBeachThisIsOn() != null)
        {
            Beach.CreateFireOnBeach(this);
        }
        ReduceSpeed();
    }

    void ReduceSpeed()
    {
        Extinguish();
    }

    void IncreaseSpeed()
    {
        Debug.Log("an islander was set on fire and has recovered")
    }
}

class Inventory
{
    public void ReceiveItem(Interactable_Object interactable)
    {
        Debug.Log("new object in inventory");
    }
}

class Beach
{
    public void CreateFireOnBeach(Fire fire)
    {
        Debug.Log("This beach has a fire on it");
    }

    public bool HasFireOnIt()
    {
        return something;
    }
}

class Boat
{
    //START TRAVELLING NEAR A BEACH
    void TravelNearBeach(Beach beach)
    {
        if (beach.HasFireOnIt())
            Debug.Log("game won");
    }
}