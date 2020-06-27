using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Harmony;
using System.Reflection;
using Psychology;

namespace NekoBoiNick.PsychologyImprisonmentPatch
{
    [StaticConstructorOnStartup]
    static class ImprisonmentPatch
  {
        static ImprisonmentPatch()
        {
            var harmony = HarmonyInstance.Create("com.dninemfive.psychologyimprisonmentpatch");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Message("Psychology Imprisonment Patch successfully loaded");
        }

        [HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), "DoRecruit")]
        [HarmonyPatch(new Type[] { typeof(Pawn), typeof(Pawn), typeof(float), typeof(string), typeof(string), typeof(bool), typeof(bool) }, new ArgumentType[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Ref, ArgumentType.Ref, ArgumentType.Normal, ArgumentType.Normal })]
        class RecruitAttemptPatch
        {
            [HarmonyPriority(Priority.Low)]
            [HarmonyPrefix]
            public static bool RemoveCapturedThoughts(Pawn recruiter, Pawn recruitee)
            {
                if(recruitee.IsPrisonerOfColony && recruitee.Faction != recruiter.Faction)
                {
                    recruitee.needs.mood.thoughts.memories.RemoveMemoriesOfDef(ThoughtDefOfPsychology.CapturedMe);
                }
                return true;
            }
        }
    }
}
