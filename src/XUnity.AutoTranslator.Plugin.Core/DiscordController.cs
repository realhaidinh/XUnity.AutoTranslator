using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;

namespace XUnity.AutoTranslator.Plugin.Core
{
   internal enum ActivityState
   {
      InMenu,
      InBattle
   }
   internal class DiscordController
   {
      private Discord.Discord _discord;
      private ActivityManager _activityManager;
      private Discord.Activity _activity = new()
      {
         Instance = true,
         Assets =
            {
               LargeImage = "bingli_icon",
               SmallImage = "bingli"
            },
         Details = "Playing Kalpa Of Universe"
      };
      private readonly long _clientId = 1239990814478700577;
      public DiscordController()
      {
         _discord = new Discord.Discord(_clientId, (System.UInt64)Discord.CreateFlags.Default);
         _activityManager = _discord.GetActivityManager();
         this.ChangeState( ActivityState.InMenu );
      }
      public void Update()
      {
         _discord.RunCallbacks();
      }
      public void Dispose()
      {
         _discord.Dispose();
      }
      public void ChangeState( ActivityState state )
      {
         switch( state )
         {
            case ActivityState.InMenu:
               _activity.State = "In Menu";
               break;
            case ActivityState.InBattle:
               _activity.State = "In Battle";
               break;
            default:
               break;
         }
         _activity.Timestamps.Start = GetCurrentTimestamp();
         _activityManager.UpdateActivity( _activity, result => { } );
      }
      private static long GetCurrentTimestamp()
      {
         return (long)( DateTime.UtcNow - new DateTime( 1970, 1, 1 ) ).TotalSeconds;
      }
   }
}
