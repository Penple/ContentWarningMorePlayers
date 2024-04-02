using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using BepInEx;
using HarmonyLib;

namespace MorePlayers
{
    [BepInPlugin("dev.penple.moreplayers", "MorePlayers", "1.0.0")]
    public class MorePlayersPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("Applying MorePlayers patches.");
            var harmony = new Harmony("dev.penple.moreplayers");
            harmony.PatchAll();
            Logger.LogInfo("MorePlayers loaded. Ensure all players have the mod installed.");
        }
    }

    [HarmonyPatch(typeof(MainMenuHandler), "Start")]
    class MainMenuHandlerPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return new CodeMatcher(instructions)
                .MatchForward(false,
                    new CodeMatch(OpCodes.Ldc_I4_4),
                    new CodeMatch(OpCodes.Ldc_I4_1),
                    new CodeMatch(OpCodes.Newobj))
                .Set(OpCodes.Ldc_I4, 32)
                .InstructionEnumeration();
        }
    }

    [HarmonyPatch(typeof(PlayerHandler), "AllPlayersAsleep")]
    class PlayerHandlerPatch
    {
        static bool Prefix(ref bool __result, PlayerHandler __instance)
        {
            int num = 0;
            for (int i = 0; i < __instance.playerAlive.Count; i++)
            {
                if (__instance.playerAlive[i].data.sleepAmount >= 0.9f)
                {
                    num++;
                }
            }
            __result = num == __instance.playerAlive.Count || num >= 4;

            return false;
        }
    }

    [HarmonyPatch(typeof(SpawnHandler), "GetSpawnPoint")]
    class SpawnHandlerPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return new CodeMatcher(instructions)
                .MatchForward(true,
                    new CodeMatch(OpCodes.Ldfld),
                    new CodeMatch(OpCodes.Ldarg_0),
                    new CodeMatch(OpCodes.Ldfld),
                    new CodeMatch(OpCodes.Ldelem_Ref))
                .Repeat(matcher =>
                    matcher
                        .InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ldc_I4_4),
                            new CodeInstruction(OpCodes.Rem)
                        )
                )
                .InstructionEnumeration();
        }
    }

    [HarmonyPatch(typeof(BedBoss), "AssignBed")]
    class BedBossPatch
    {
        static bool Prefix(int viewId, int siblingId)
        {
            return siblingId < 4;
        }
    }
}
