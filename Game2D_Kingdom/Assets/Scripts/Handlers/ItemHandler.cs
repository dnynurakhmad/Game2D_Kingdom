using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public ItemSet Item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gm = GameManager.Instance;
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            var player = collision.GetComponent<PlayerActive>();
            switch (Item)
            {
                case ItemSet.Item_Elixir:
                    gm.Item_Elixir++;
                    break;
                case ItemSet.Item_Scroll:
                    gm.Item_Scroll++;
                    break;
                case ItemSet.Power_Green:
                    player.HealthPoint.CurrentProp += 10;
                    DamageActive.PopUpDamage(gm.Origin_DamagePopUp, transform.position, 10, DamageState.Healing);
                    break;
                case ItemSet.Power_Red:
                    player.StaminaPoint.CurrentProp += 20;
                    break;
                case ItemSet.Power_Blue:
                    player.MagicPoint.CurrentProp += 50;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
