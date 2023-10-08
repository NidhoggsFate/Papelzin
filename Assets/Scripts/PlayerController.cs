using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float forcaPraCima;
    public float coolDown;

    public Rigidbody2D rb;
    
    private float coolDownTimer;
    
    public UnityEvent playerDied;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BoostPraCima();
        }
        
        if(coolDownTimer > 0)
            coolDownTimer -= Time.deltaTime;
    }

    void BoostPraCima()
    {
        if (GameController.instance.GamePaused || coolDownTimer > 0)
            return;
        
        coolDownTimer = coolDown;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, forcaPraCima));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            playerDied?.Invoke();
            rb.velocity = Vector2.zero;
        }
    }
}
