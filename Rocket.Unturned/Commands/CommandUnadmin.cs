﻿using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Rocket.API.Commands;
using Rocket.API.Player;
using Rocket.API.User;
using Rocket.Core.Commands;
using Rocket.Core.Player;
using Rocket.Core.User;
using Rocket.Unturned.Player;

namespace Rocket.Unturned.Commands
{
    public class CommandUnadmin : ICommand
    {
        public bool SupportsUser(IUser user) => user is UnturnedUser;

        public async Task ExecuteAsync(ICommandContext context)
        {
            if (context.Parameters.Length != 1)
                throw new CommandWrongUsageException();

            IPlayer targetUser = await context.Parameters.GetAsync<IPlayer>(0);

            if (targetUser is UnturnedPlayer uPlayer && uPlayer.IsAdmin)
            {
                uPlayer.Admin(false);
                return;
            }

            await context.User.SendMessageAsync($"Could not unadmin {targetUser.GetUser().DisplayName}", ConsoleColor.Red);
        }

        public string Name => "Unadmin";
        public string Summary => "Removes admin from a player.";
        public string Description => null;
        public string Permission => "Rocket.Unturned.Unadmin";
        public string Syntax => "<target player>";
        public IChildCommand[] ChildCommands => null;
        public string[] Aliases => null;
    }
}