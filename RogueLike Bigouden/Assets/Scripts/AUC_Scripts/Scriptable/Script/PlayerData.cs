using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    // Nom & Description
    public string nameSO; // Nom de l'unité
    public string descriptionSO; // Description de l'unité

    //  Int
    public int actualUltPointSO; // Point d'ultimate collecté par le joueur
    public int ultMaxPointSO; // Point d'ultime pour lancer l'ult
    public int lifePointsSO; // Point de vie du joueur
    public int dashCounterSO; //Number of Dash
    public int shieldPointSO; // Point de bouclier de l'ennemis

    public int damageXSO; // Dégat de l'attaque de base
    public int damageYSO; // Dégat de l'attaque spé
    public int damageProjectileSO; // Dégat du projectile
    public int damageUltSO;

    public int ultPointToAddPerHitSO;
    public int ultPointToAddPerKillSO;

    //  Float
    public float movementSpeedSO;
    public float attackRangeXSO;
    public float attackCdXSO;
    public float attackRangeYSO;
    public float attackCdYSO;
    public float attackRangeProjectileSO;
    public float attackCdBSO;
    public float dashSpeedSO;
    public float dashDurationSO;
    public float dashCooldownSO;
    public float ultDurationSO;
    public float bonusSpeedSO; // Déplacement Dash

    // Vector2 
    public Vector2 firstAttackResetSO; // Premier Reset du X 
    public Vector2 secondAttackResetSO; // Deuxième Reset du X

    // Bool
    public bool isAttackingXSO; // Peut utiliser l'attaque X
    public bool readyToAttackXSO; // Peut utiliser l'attaque X
    public bool isAttackFirstXSO; // Peut utiliser l'attaque X
    public bool isAttackSecondXSO; // Peut utiliser l'attaque X
    public bool readyToAttackYSO; // Peut utiliser l'attaque Y
    public bool isAttackYSO; // Peut utiliser l'attaque Y
    public bool readyToAttackBSO; // Peut utiliser l'attaque projectile
    public bool isAttackBSO; // Peut utiliser l'attaque projectile
    public bool isDashingSO;
    public bool readyToDashSO;
    public bool onButterSO; // Le joueur se trouve sur une flaque de beurre
}