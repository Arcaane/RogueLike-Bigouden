using UnityEngine;

[CreateAssetMenu(fileName = "New Props", menuName = "ElementLD")]
public class PropsData : ScriptableObject
{
    // Common Int
    public int lifePointSO; // Point de vie de l'unité
    public int shieldPointSO; // Point de l'armure de l'unité
    public int damageSO; // Nombre de dégats
    
    // Common Float
    public float attackRangeSO; // Portée de l'attaque
    public float attackDelaySO; // Delay entre les attack des ennemis
    public float timeBeforeLaunchAnimation; // Delay avant que les ennemis aggro le joueur
    public float animationSpeed; // Vitesse de déplacement de l'unité
    
    // Common Bool
    public bool isPlayerInAttackRangeSO; // Le player est-il en range ?
    public bool isPlayerInAggroRangeSO;
    public bool isReadyToShootSO; // Peut tirer ?
    public bool isAggroSO; // L'unité chase le joueur ?
    public bool isAttackingSO; // L'unité attaque ?
    public bool isStunSO; // L'unité est stun ?
    
    // IA Barman
    public int damageAoeSO; // Impact de zone quand un coktail touche le sol
    public int damageAoeAfterExplosionSO; // Dot du coktail
    public float rangeAoeSO; // Range de l'aoe du coktail du serveur
    
    // Common Vector2 Shooter
    public Vector2 bulletSpreadSO; // Random sur la trajectoire des balles (Faible)
    public Vector2 bulletSpeedSO; // Random sur la vitesse des balles (Faible)

    // Runner Variables
    public float dashSpeedSO;
    public float stunDurationSO;
    public float rushDelaySO;
    public bool isReadyToDashSO;
}