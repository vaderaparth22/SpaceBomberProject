using System.Collections;
using System.Collections.Generic;
using Level;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{

    [SerializeField] private Cinemachine.CinemachineVirtualCamera vcam;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Collider worldBounds;
    [SerializeField] private GameLevel level;
    public void Awake()
    {
        GameManager.Instance.Initialize(level,vcam);
        UIManager.Instance.Initialize(level);
        InputManager.Instance.Initialize(mainCamera);
        AmmoManager.Instance.Initialize();
    }

    public void Start()
    {
        GameManager.Instance.GameStart();
        AmmoManager.Instance.GameStart();
        UIManager.Instance.GameStart();
    }

    public void Update()
    {
        GameManager.Instance.Refresh();
        AmmoManager.Instance.Refresh();
        UIManager.Instance.Refresh();
    }

    public void FixedUpdate()
    {
        GameManager.Instance.FixedRefresh();
        AmmoManager.Instance.FixedRefresh();
    }
}