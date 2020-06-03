using HarmonyLib;
using Psychology;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace PsychologyImprisonmentPatch
{
  [StaticConstructorOnStartup]
  class Main
  {
    static Main()
    {
      new Harmony("NekoBoiNick.Psychology.ImprisonmentPatch").PatchAll(Assembly.GetExecutingAssembly());
      Log.Message("Psychology Imprisonment Patch successfully loaded", false);
    }
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
