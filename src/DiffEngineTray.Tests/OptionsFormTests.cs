﻿#if DEBUG
using System.Collections.Generic;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

[UsesVerify]
public class OptionsFormTests :
    XunitContextBase
{
    public OptionsFormTests(ITestOutputHelper output) :
        base(output)
    {
        VersionReader.VersionString = "TheVersion";
    }

    //[Fact]
    //[Trait("Category", "Integration")]
    //public void Launch()
    //{
    //    using var form = new OptionsForm(
    //        new Settings
    //        {
    //            AcceptAllHotKey = new HotKey
    //            {
    //                Shift = true,
    //                Key = "A"
    //            }
    //        },
    //        x => Task.FromResult<IReadOnlyList<string>>(new List<string>()));
    //    form.ShowDialog();
    //    form.BringToFront();
    //}
    [Fact]
    public async Task WithKeys()
    {
        using OptionsForm form = new(
            new()
            {
                AcceptAllHotKey = new()
                {
                    Shift = true,
                    Key = "A"
                }
            },
            _ => Task.FromResult<IReadOnlyList<string>>(new List<string>()));
        await Verifier.Verify(form);
    }

    [Fact]
    public async Task Default()
    {
        using OptionsForm form = new(
            new(),
            _ => Task.FromResult<IReadOnlyList<string>>(new List<string>()));
        await Verifier.Verify(form);
    }
}
#endif