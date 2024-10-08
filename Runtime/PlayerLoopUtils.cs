using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;


namespace UnityUtils.LowLevel {

    public static class PlayerLoopUtils
    {
        public static bool InsertSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToInsert, int index)
        {
            if (loop.type != typeof(T))
                return HandleSubSystemLoop<T>(ref loop, systemToInsert, index);

            //found match
            var playerloopSystemList = new List<PlayerLoopSystem>();
            if (loop.subSystemList != null)
                playerloopSystemList.AddRange(loop.subSystemList);

            playerloopSystemList.Insert(index, systemToInsert);
            loop.subSystemList = playerloopSystemList.ToArray();
            return true;
        }

        public static void RemoveSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToRemove)
        {
            if (loop.subSystemList == null)
                return;
            var playerLoopSystemList = new List<PlayerLoopSystem>(loop.subSystemList);
            for(int x=0;x < playerLoopSystemList.Count;x++)
            {
                if(playerLoopSystemList[x].type == systemToRemove.type && playerLoopSystemList[x].updateDelegate == systemToRemove.updateDelegate)
                {
                    playerLoopSystemList.RemoveAt(x);
                    loop.subSystemList = playerLoopSystemList.ToArray();
                }
            }

            HandleSubSystemLoopForRemoval<T>(ref loop, systemToRemove);
        }

        static void HandleSubSystemLoopForRemoval<T>(ref PlayerLoopSystem loop, PlayerLoopSystem systemToRemove)
        {
            if (loop.subSystemList == null)
                return;

            for (int x = 0; x < loop.subSystemList.Length; x++)
            {
                RemoveSystem<T>(ref loop.subSystemList[x], systemToRemove);
            }

        }

        static bool HandleSubSystemLoop<T>(ref PlayerLoopSystem loop,in PlayerLoopSystem systemToInsert,int index)
        {
            if (loop.subSystemList == null)
                return false;

            for(int x=0;x< loop.subSystemList.Length;x++)
            {
                if (!InsertSystem<T>(ref loop.subSystemList[x], in systemToInsert, index))
                    continue;

                return true;
            }
            return false;
        }

        public static void PrintPlayerLoop(PlayerLoopSystem loop)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Unity Player Loop");
            foreach (PlayerLoopSystem subSystem in loop.subSystemList)
            {
                PrintSubsystem(subSystem, sb, 0);
            }

            Debug.Log(sb.ToString());
        }

        static void PrintSubsystem(PlayerLoopSystem system, StringBuilder sb, int lvl)
        {
            sb.Append(' ', lvl + 2).AppendLine(system.ToString());
            if (system.subSystemList == null || system.subSystemList.Length == 0) 
                return;

            foreach (PlayerLoopSystem subSystem in system.subSystemList)
            {
                PrintSubsystem(subSystem, sb, lvl + 1);
            }
        }
    }

}
