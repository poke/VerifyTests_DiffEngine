﻿using System.IO;
using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

[UsesVerify]
public class MenuBuilderTest :
    XunitContextBase
{
    [Fact]
    public async Task Empty()
    {
        await using RecordingTracker tracker = new();
        var menu = MenuBuilder.Build(() => { }, () => { }, tracker);
        await Verifier.Verify(menu, settings);
    }

    [Fact]
    public async Task OnlyMove()
    {
        await using RecordingTracker tracker = new();
        tracker.AddMove(file2, file2, "theExe", "theArguments", true, null);
        var menu = MenuBuilder.Build(() => { }, () => { }, tracker);
        await Verifier.Verify(menu, settings);
    }

    [Fact]
    public async Task OnlyDelete()
    {
        await using RecordingTracker tracker = new();
        tracker.AddDelete(file1);
        var menu = MenuBuilder.Build(() => { }, () => { }, tracker);
        await Verifier.Verify(menu, settings);
    }

    [Fact]
    public async Task Full()
    {
        await using RecordingTracker tracker = new();
        tracker.AddDelete(file1);
        tracker.AddDelete(file2);
        tracker.AddMove(file3, file3, "theExe", "theArguments", true, null);
        tracker.AddMove(file4, file4, "theExe", "theArguments", true, null);
        var menu = MenuBuilder.Build(() => { }, () => { }, tracker);
        await Verifier.Verify(menu, settings);
    }

    [Fact]
    public async Task Many()
    {
        await using RecordingTracker tracker = new();
        foreach (var file in EmptyFiles.AllFiles.AllPaths)
        {
            tracker.AddDelete(file);
        }

        var menu = MenuBuilder.Build(() => { }, () => { }, tracker);
        await Verifier.Verify(menu, settings);
    }

    [Fact]
    public async Task Grouped()
    {
        await using RecordingTracker tracker = new();
        tracker.AddDelete("file2.txt");
        tracker.AddMove(file4, "file4.txt", "theExe", "theArguments", true, null);
        var menu = MenuBuilder.Build(() => { }, () => { }, tracker);
        await Verifier.Verify(menu, settings);
    }

    [Fact]
    public async Task FullGrouped()
    {
        await using RecordingTracker tracker = new();
        tracker.AddDelete(file1);
        tracker.AddDelete("file2.txt");
        tracker.AddMove(file3, file3, "theExe", "theArguments", true, null);
        tracker.AddMove(file4, "file4.txt", "theExe", "theArguments", true, null);
        var menu = MenuBuilder.Build(() => { }, () => { }, tracker);
        await Verifier.Verify(menu, settings);
    }

    public MenuBuilderTest(ITestOutputHelper output) :
        base(output)
    {
        settings = new();
        file1 = Path.GetFullPath("file1.txt");
        file2 = Path.GetFullPath("file2.txt");
        file3 = Path.GetFullPath("file3.txt");
        file4 = Path.GetFullPath("file4.txt");
        File.WriteAllText(file1, "");
        File.WriteAllText(file2, "");
        File.WriteAllText(file3, "");
        File.WriteAllText(file4, "");
    }

    public override void Dispose()
    {
        File.Delete(file1);
        File.Delete(file2);
        File.Delete(file3);
        File.Delete(file4);
        base.Dispose();
    }

    string file1;
    string file2;
    string file3;
    string file4;
    VerifySettings settings;
}