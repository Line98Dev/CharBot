﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace CharBot.Services
{
	public class LoggingService
    {

        public LoggingService(BaseDiscordClient client, CommandService command)
        {
            client.Log += LogAsync;
            command.Log += LogAsync;
        }
        private Task LogAsync(LogMessage message)
        {
            if (message.Exception is CommandException cmdException)
            {
                Console.WriteLine($@"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
                                  + $@" failed to execute in {cmdException.Context.Channel}.");
                Console.WriteLine(cmdException);
            }
            else
                Console.WriteLine($@"[General/{message.Severity}] {message}");

            return Task.CompletedTask;
        }
        
    }
}
