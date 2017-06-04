using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korwa_Bot_by_me
{
    class Mybot
    {
        DiscordClient discord;
        CommandService commands;

        public Mybot()
        {
            discord = new DiscordClient(x =>
          {
              x.LogLevel = LogSeverity.Info;
              x.LogHandler = Log;
          });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            RegisterPurgeCommand();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzIwMjQwMDExNDczNTg0MTMw.DBM_cg.UWkhf8DpCLetJqHn2CwW6Y24G5g", TokenType.Bot);
            });
        }

        private void RegisterPurgeCommand()
        {
            commands.CreateCommand("purge")
                .Do(async (e) =>
                {
                    Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(100);

                    await e.Channel.DeleteMessages(messagesToDelete);
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
