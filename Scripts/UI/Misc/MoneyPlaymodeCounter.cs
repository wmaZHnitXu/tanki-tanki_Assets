using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPlaymodeCounter : MonoBehaviour
{
    [SerializeField] float yOffsetUp;
    [SerializeField] float currentOffset;
    [SerializeField] float animSpeed = 1.0f;
    [SerializeField] float moneyFlowSpeed = 1.0f;
    private Vector3 defaultPosition;
    [SerializeField] private Text moneyText;
    private float moneyIntermediate;
    private float moneyTarget;
    private int moneyInt;
    private int currentMoneyInText;
    float cooldown;
    float maxCooldown = 1.0f;

    void Start() {
        defaultPosition = transform.localPosition;
    }
    void OnEnable() {
        Game.instance.level.OnProfitChangedEvent += ChangeMoney;
    }

    void OnDisable() {
        if (Game.instance.level != null) {
            Game.instance.level.OnProfitChangedEvent -= ChangeMoney;
        }
        else {
            Debug.LogError("kapec");
        }
    }

    void Update()
    {
        if (cooldown > 0.0f) {
            if (currentOffset != yOffsetUp) {
                currentOffset += (yOffsetUp - currentOffset) * animSpeed * Time.deltaTime;
                if (Mathf.Abs(currentOffset - yOffsetUp) < 0.01f) {
                    currentOffset = yOffsetUp;
                }
                transform.localPosition = new Vector3(defaultPosition.x, defaultPosition.y + currentOffset, defaultPosition.z);
            }
        }
        else {
            if (currentOffset != 0.0f) {
                currentOffset -= currentOffset * animSpeed * Time.deltaTime;
                if (Mathf.Abs(currentOffset) < 0.01f) {
                    currentOffset = 0.0f;
                }
                transform.localPosition = new Vector3(defaultPosition.x, defaultPosition.y + currentOffset, defaultPosition.z);
            }
        }

        //MONEY
        if (Mathf.Abs(moneyIntermediate - moneyTarget) > 0.01f) {
            moneyIntermediate += (moneyTarget - moneyIntermediate) * moneyFlowSpeed * Time.deltaTime;
            moneyInt = (int)moneyIntermediate;
            if (moneyInt != currentMoneyInText) {
                moneyText.text = moneyInt.ToString() + "$";
                currentMoneyInText = moneyInt;
                cooldown = maxCooldown;
            }
        }

        if (cooldown > 0.0f) {
            cooldown -= Time.deltaTime;
        }
        else {
            cooldown = 0.0f;
        }
    }

    public void ChangeMoney(Level level, int newProfit) {
        moneyTarget = newProfit;
        cooldown = maxCooldown;
    }
}
