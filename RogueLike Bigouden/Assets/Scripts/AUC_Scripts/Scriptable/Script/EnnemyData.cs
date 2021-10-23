using UnityEngine;

[CreateAssetMenu(fileName = "New Ennemy", menuName = "Ennemies")]
public class EnnemyData : ScriptableObject
{
    // Nom & Description
    public new string nameSO; // Nom de l'unité
    public string descriptionSO; // Description de l'unité

    // Common Int
    public int lifePointSO; // Point de vie de l'unité
    public int shieldPointSO; // Point de l'armure de l'unité
    public int damageSO; // Nombre de dégats
    public float detectZoneSO; // Fov

    // IA Barman
    public int damageAoeSO; // Impact de zone quand un coktail touche le sol
    public int damageAoeBeforeExplosionSO; // Dot du coktail

    // Common Float
    public float attackRangeSO; // Portée de l'attaque
    public float delayAttackSO; // Delay entre les attack des ennemis
    public float timeBeforeAggroSO; // Delay avant que les ennemis aggro le joueur
    public float attackSpeedSO; // Vitesse d'attaque de l'unité
    public float movementSpeedSO; // Vitesse de déplacement de l'unité

    // Special Float
    public float rangeAoeSO; // Range de l'aoe du coktail du serveur
    public float numberBulletShotSO; // Nombre de balles que tire le shooter
    
    // Special float runner
    public float moveSpeedChargeSO; // MS de la charge du runner
    public float chargeDurationSO; // MS de la charge du runner

    // Common Vector2
    public Vector2 bulletSpreadSO; // Random sur la trajectoire des balles (Faible)
    public Vector2 bulletSpeedSO; // Random sur la vitesse des balles (Faible)

    // Common Bool
    public bool isPlayerInAttackRangeSO; // Le player est-il en range ?
    public bool isReadyToShootSO; // Peut tirer ?
    public bool isAggroSO; // L'unité chase le joueur ?
    public bool isAttackingSO; // L'unité attaque ?
    public bool isStunSO; // L'unité est stun ?

    // Special Bool
    public bool isChargingSO; // Le runner charge t'il ?
}