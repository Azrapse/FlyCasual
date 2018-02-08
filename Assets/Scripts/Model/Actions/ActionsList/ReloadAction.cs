﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrade;
using Tokens;
using System.Linq;

namespace ActionsList
{

    public class ReloadAction : GenericAction
    {

        public ReloadAction() {
            Name = EffectName = "Reload";
            ImageUrl = "https://raw.githubusercontent.com/guidokessels/xwing-data/master/images/reference-cards/ReloadActionAndJamTokens.png";
        }

        public override void ActionTake()
        {
            foreach (var upgrade in Selection.ThisShip.UpgradeBar.GetUpgradesOnlyDiscarded())
            {
                if (upgrade.Type == UpgradeType.Missile || upgrade.Type == UpgradeType.Torpedo)
                {
                    upgrade.FlipFaceup();
                }
            }

            Selection.ThisShip.Tokens.AssignToken(new WeaponsDisabledToken(Selection.ThisShip), Phases.CurrentSubPhase.CallBack);
        }

        public override int GetActionPriority()
        {
            int result = 0;

            int discardedOrdnance = Selection.ThisShip.UpgradeBar.GetUpgradesOnlyDiscarded().Count(n => n.Type == UpgradeType.Missile || n.Type == UpgradeType.Torpedo);
            result = discardedOrdnance * 30;

            return result;
        }

    }

}
