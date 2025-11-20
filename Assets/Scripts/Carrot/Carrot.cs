using UnityEngine;

public class Carrot : MonoBehaviour
{
    [Header("伤害设置")]
    public int damagePerEnemy = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 使用敌人的伤害值
                int damage = enemy.DamageToCarrot;
                LevelManager.Instance.TakeDamage(damage);
                
                // 销毁敌人
                Destroy(other.gameObject);
                
                Debug.Log($"敌人碰到萝卜！扣减 {damage} 点生命值，剩余 {LevelManager.Instance.TotalLives}");
            }
        }
    }
}