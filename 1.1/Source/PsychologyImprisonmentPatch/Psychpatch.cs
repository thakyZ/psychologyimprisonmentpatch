using HarmonyLib;
using Psychology;
using RimWorld;
using System;
using Verse;

namespace PsychologyImprisonmentPatch
{
  public class PsychologyPatcher
  {
    public static void DoPatching()
    {
      var harmony = new Harmony("com.dninemfive.psychologyimprisonmentpatch");
      harmony.PatchAll();
      Log.Message("Psychology Imprisonment Patch successfully loaded");
    }
  }

  [HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), "DoRecruit")]
  [HarmonyPatch(new Type[] { typeof(Pawn), typeof(Pawn), typeof(float), typeof(string), typeof(string), typeof(bool), typeof(bool) }, new ArgumentType[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Ref, ArgumentType.Ref, ArgumentType.Normal, ArgumentType.Normal })]
  class RecruitAttemptPatch
  {
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
