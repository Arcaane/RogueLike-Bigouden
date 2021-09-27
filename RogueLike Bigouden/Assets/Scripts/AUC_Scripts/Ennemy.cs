using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ennemy", menuName = "Ennemies")]
public class Ennemy : ScriptableObject
{
    // Nom & Description
    public new string name; // Nom de l'unité
    public string description; // Description de l'unité
    
    // Sprite
    public Sprite artwork; // Sprite de l'unité
    
    // Common Int
    public int lifePoint; // Point de vie de l'unité
    public int shieldPoint; // Point de l'armure de l'unité
    public int damage; // Nombre de dégats
    public int detectZone; // Fov

    // IA Serveur
    public int damageAoe; // Impact de zone quand un coktail touche le sol
    public int damageAoeBeforeExplosion; // Dot du coktail
    
    // Common Float
    public float range; // Portée de l'attaque
    public float delayAttack; // Delay entre les attack des ennemis
    public float timeBeforeAggro; // Delay avant que les ennemis aggro le joueur
    public float attackSpeed; // Vitesse d'attaque de l'unité
    public float movementSpeed; // Vitesse de déplacement de l'unité
    
    // Special Float
    public float rangeAoe; // Range de l'aoe du coktail du serveur
    public float numberBulletShot; // Nombre de balles que tire le shooter
    public float moveSpeedCharge; // MS de la charge du runner
    public float chargeDuration; // MS de la charge du runner

    // Common Vector2
    public Vector2 bulletSpread; // Random sur la trajectoire des balles (Faible)
    public Vector2 bulletSpeed; // Random sur la vitesse des balles (Faible)

    // Common Bool
    public bool playerInAttackRange; // Le player est-il en range ?
    public bool readyToShoot; // Peut tirer ?
    public bool isAggro; // L'unité chase le joueur ?
    public bool isAttacking; // L'unité attaque ?
    public bool isStun; // L'unité est stun ?
    
    // Special Bool
    public bool isCharging; // Le runner charge t'il ?
}
