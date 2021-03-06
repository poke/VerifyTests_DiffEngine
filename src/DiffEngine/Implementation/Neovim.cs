﻿using System;
using DiffEngine;

static partial class Implementation
{
    public static Definition Neovim()
    {
        static string Arguments(string temp, string target)
        {
            return $"-d \"{temp}\" \"{target}\"";
        }

        return new(
            name: DiffTool.Neovim,
            url: "https://neovim.io/",
            autoRefresh: false,
            isMdi: false,
            supportsText: true,
            requiresTarget: true,
            binaryExtensions: Array.Empty<string>(),
            windows: new(Arguments, @"%ChocolateyToolsLocation%\neovim\*\nvim.exe"),
            notes: @"
 * Assumes installed through Chocolatey https://chocolatey.org/packages/neovim/");
    }
}