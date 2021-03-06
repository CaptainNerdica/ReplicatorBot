﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace ReplicatorBot
{
	[Group("replicator")]
	public class ReplicatorCommands : ModuleBase<SocketCommandContext>
	{
		[Command("ping")]
		public Task PingAsync() => ReplyAsync("pong!");

		[Group("info")]
		[RequireUserPermission(GuildPermission.Administrator)]
		public class BotInfoModule : ModuleBase<SocketCommandContext>
		{
			[Command]
			[RequireUserPermission(GuildPermission.Administrator)]
			public async Task GetInfo()
			{
				DiscordServerInfo info = Program.Replicant.AvailableServers[Context.Guild.Id];
				string builder = $"Replicated User: {(info.TargetUserId == null ? "None" : Context.Guild.GetUser((ulong)info.TargetUserId).Nickname)}\n";
				builder += $"Enabled: {info.Enabled}\n";
				builder += $"Probability: {info.Proability}\n";
				builder += $"Server messages: {info.GuildTotalMessages}\n";
				builder += $"Target messages: {info.TargetTotalMessages}\n";
				builder += $"Last message time: {info.LastMessageReceived} UTC\n";
				await ReplyAsync(builder);
			}
		}
	}
}

