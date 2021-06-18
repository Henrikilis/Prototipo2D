using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public GameObject _powerupPrefab;

    public List<PowerUp> _powerUps;

    public Dictionary<PowerUp, float> _activePowerUps = new Dictionary<PowerUp, float>();
    private int _chance;
    private List<PowerUp> _keys = new List<PowerUp>();
    
    void Start()
    {
        
    }

    
    void Update()
    {
        HandleActivePowerUps();
    }

    public void HandleActivePowerUps()
    {
        bool _changed = false;

        if(_activePowerUps.Count > 0)
        {
            foreach (PowerUp powerUp in _keys)
            {
                if(_activePowerUps[powerUp] > 0)
                {
                    _activePowerUps[powerUp] -= Time.deltaTime;
                }
                else
                {
                    _changed = true;
                    _activePowerUps.Remove(powerUp);
                }
            }
        }

        if (_changed)
        {
            _keys = new List<PowerUp>(_activePowerUps.Keys);   
        }

    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        if (!_activePowerUps.ContainsKey(powerUp))
        {
            powerUp.Start();
            _activePowerUps.Add(powerUp, powerUp._duration);

        }
        else
            _activePowerUps[powerUp] += powerUp._duration;

        _keys = new List<PowerUp>(_activePowerUps.Keys);

    }

    public GameObject SpawnPowerUp(PowerUp powerUp, Vector2 position)
    {
        _chance = Random.Range(0, powerUp._dropChance+1);

        if (_chance == powerUp._dropChance)
        {

            GameObject _powerUpGameObject = Instantiate(_powerupPrefab);

            var _powerUpBehavior = _powerUpGameObject.GetComponent<PowerUpBehavior>();
            SpriteRenderer _sr = _powerUpGameObject.GetComponent<SpriteRenderer>();

            _powerUpBehavior._controller = this;
            _powerUpBehavior.SetPowerup(powerUp);
            _sr.sprite = powerUp._sprite;
            _powerUpGameObject.transform.position = position;

            return _powerUpGameObject;
        }
        else return null;
    }
}
