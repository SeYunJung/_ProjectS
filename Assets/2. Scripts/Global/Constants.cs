using UnityEngine;

// 웨이브
using System.Numerics;

public static class Wave 
{
    public const int FINAL = 2;
}

// 좌표
public static class Pos
{
    public const float BLOCK_OFFSET_X = 0.14f;
    public const float BLOCK_OFFSET_Y = 0.195f;
}

public static class ResourcePath
{
    public const string CHARACTER = "Character/";
    public const string GRID = "Grid/";
    public const string UI = "UI/";
}

public static class Prefab
{
    public const string PLAYER = "Player";
    public const string GRID = "Grid";
    public const string UI = "UI";
    public const string PROMOTION = "UIPromotion";
}

public static class Position
{
    public static readonly UnityEngine.Vector2 PLAYER = new UnityEngine.Vector2(4.5f, 5.54f);
}