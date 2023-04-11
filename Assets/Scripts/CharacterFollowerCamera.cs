using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollowerCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smooth;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * smooth);
        }
    }
    public void Init(GameObject player)
    {
        this.player = player;
    }
}
