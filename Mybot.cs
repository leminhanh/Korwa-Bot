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

        Random rand;

        string[] freshestlewd;
        public Mybot()
        {
            rand = new Random();
            freshestlewd = new string[]
            {
                "lewd/lewd1.jpg", //0
                "lewd/lewd2.jpg", //1
                "lewd/lewd3.gif", //2
                "lewd/lewd4.jpg", //3 
                "lewd/lewd5.gif", //4
                "lewd/lewd6.jpg", //5
                "lewd/lewd7.jpg", //6
                "lewd/lewd8.jpg", //7
                "lewd/lewd9.gif", //8
                "lewd/lewd10.jpg", //9
                "lewd/lewd11.gif", //10
                "lewd/lewd12.jpg" //11
            };
            discord = new DiscordClient(x =>
          {
              x.LogLevel = LogSeverity.Info;
              x.LogHandler = Log;
          });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '`';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            RegisterLewdCommand();
            RegisterPurgeCommand();
            

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzIwMjQwMDExNDczNTg0MTMw.DBM_cg.UWkhf8DpCLetJqHn2CwW6Y24G5g", TokenType.Bot);
            });
        }
        private void RegisterLewdCommand()
        {
            commands.CreateCommand("lewd")
                .Do(async (e) =>
                {
                    int randomlewdIndex = rand.Next(freshestlewd.Length);
                    string lewdToPost = freshestlewd[randomlewdIndex];
                    await e.Channel.SendFile(lewdToPost);
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
