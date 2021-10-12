using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    // Nom & Description
    public new string name; // Nom de l'unité
    public string description; // Description de l'unité

    // Common Int
    public int attackSpeed; // Vitesse d'attaque
    public int actualUltPoint; // Point d'ultimate collecté par le joueur
    public int ultMaxPoint; // Point d'ultime pour lancer l'ult
    public int lifePoints; // Point de vie du joueur
    public int shieldPoint; // Point de bouclier de l'ennemis

    public int damageX; // Dégat de l'attaque de base
    public int damageY; // Dégat de l'attaque spé
    public int damageProjectile; // Dégat du projectile

    // Common Float
    public float movementSpeed;
    public float attackRangeX;
    public float attackCdX;
    public float attackRangeY;
    public float attackCdY;
    public float attackRangeProjectile;
    public float attackCdB;
    public float dashRange;
    public float dashCooldown;
    public float ultDuration;
    public float bonusSpeed; // Déplacement Dash

    // Vector2 Bigouden
    public Vector2 firstAttackReset; // Premier Reset du X 
    public Vector2 secondAttackReset; // Deuxième Reset du X

    //Common Bool
    public bool readyToAttackX; // Peut utiliser l'attaque X
    public bool readyToAttackY; // Peut utiliser l'attaque Y
    public bool readyToAttackB; // Peut utiliser l'attaque projectile
    public bool isDashing;
    public bool readyToDash;
    public bool onButter; // Le joueur se trouve sur une flaque de beurre
}