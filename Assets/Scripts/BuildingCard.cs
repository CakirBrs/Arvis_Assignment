using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Building Card", menuName ="Scriptable Object/Building Card")]
public class BuildingCard : ScriptableObject
{
    public new string name;
    public Sprite image;
    public int costGold;
    public int costGem;
    public float resorceGenerationDuration;

    public int generatedGold;
    public int generatedGem;

    public enum shapeOfBuildingType { Shape1, Shape2, Shape3, Shape4 };
    public shapeOfBuildingType shapeOfBuilding;

}
