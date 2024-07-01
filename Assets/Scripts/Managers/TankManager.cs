﻿using System;
using Tank;
using UnityEngine;

[Serializable]
public class TankManager
{
    public Color PlayerColor;
    public Transform SpawnPoint;
    [HideInInspector] public int PlayerNumber;
    [HideInInspector] public string ColoredPlayerText;
    [HideInInspector] public GameObject Instance;
    [HideInInspector] public int Wins;

    private TankMovement _movement;
    private TankShooting _shooting;
    private GameObject _canvasGameObject;

    public void Setup()
    {
        _movement = Instance.GetComponent<TankMovement>();
        _shooting = Instance.GetComponent<TankShooting>();
        _canvasGameObject = Instance.GetComponentInChildren<Canvas>().gameObject;

        _movement.PlayerNumber = PlayerNumber;
        _shooting.PlayerNumber = PlayerNumber;

        ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(PlayerColor) + ">PLAYER " + PlayerNumber + "</color>";

        MeshRenderer[] renderers = Instance.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material.color = PlayerColor;
        }
    }

    public void DisableControl()
    {
        _movement.enabled = false;
        _shooting.enabled = false;

        _canvasGameObject.SetActive(false);
    }

    public void EnableControl()
    {
        _movement.enabled = true;
        _shooting.enabled = true;

        _canvasGameObject.SetActive(true);
    }

    public void Reset()
    {
        Instance.transform.position = SpawnPoint.position;
        Instance.transform.rotation = SpawnPoint.rotation;

        Instance.SetActive(false);
        Instance.SetActive(true);
    }
}