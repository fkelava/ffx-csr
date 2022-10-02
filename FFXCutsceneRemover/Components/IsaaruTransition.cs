﻿using FFX_Cutscene_Remover.ComponentUtil;
using System;
using System.Diagnostics;
using System.Linq;
using FFXCutsceneRemover.Logging;

namespace FFXCutsceneRemover
{
    class IsaaruTransition : Transition
    {
        public override void Execute(string defaultDescription = "")
        {
            int baseAddress = base.memoryWatchers.GetBaseAddress();

                if (Stage == 0)
                {
                    base.Execute();

                    BaseCutsceneValue = base.memoryWatchers.EventFileStart.Current;

                    Stage += 1;

                }
                //else if (base.memoryWatchers.State.Current == 1 && Stage == 1)
                //{
                //    BaseCutsceneValue2 = base.memoryWatchers.IsaaruTransition.Current;
                //    DiagnosticLog.Information("Test 1");
                //    Stage += 1;
                //}
                //// If we enter a battle before Isaaru reset to stage 0 so we can get the Base Cutscene Value again.
                //// This is needed because sometimes after a Maze Larva battle the cutscene value will progress for seemingly no reason.
                //else if (base.memoryWatchers.State.Current == 2 && Stage == 2)
                //{
                //    Stage -= 1;
                //    DiagnosticLog.Information("Test 2");
                //}
                else if (base.memoryWatchers.IsaaruTransition.Current == BaseCutsceneValue + 0x7F6C + 0x32 && Stage == 1)
                {
                    Formation = new byte[] { 0x01, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                    ConsoleOutput = false;
                    FullHeal = true;
                    base.Execute();

                    DiagnosticLog.Information("Test 3");

                    WriteValue<int>(base.memoryWatchers.IsaaruTransition, BaseCutsceneValue + 0x7F6C + 0x2F8);
                    Stage += 1;
                }
                else if (base.memoryWatchers.IsaaruTransition.Current == (BaseCutsceneValue + 0x7F6C + 0x37A) && Stage == 2)
                {
                    WriteValue<int>(base.memoryWatchers.IsaaruTransition, BaseCutsceneValue + 0x7F6C + 0x5C4);
                    DiagnosticLog.Information("Test 4");
                    Stage += 1;
                }
        }
    }
}