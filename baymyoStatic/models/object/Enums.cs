using System;

namespace baymyoStatic
{
    public enum DatabaseProccesType
    {
        Optimize = 0,
        Analyze = 1,
        Check = 2,
        Repair = 3
    }

    public enum BlockType
    {
        Ascx = 0,
        Html = 1
    }

    public enum BlockThemeType
    {
        None = 0,
        HtmlNoTheme = 1,
        HtmlAndTheme = 2,
        AscxNoTheme = 3,
        AscxAndTheme = 4
    }

    public enum AccountType
    {
        None = 0,
        Admin = 1,
        Doctor = 2,
        Editor = 3,
        Standart = 4,
        Private = 5
    }

    public enum SexType
    {
        Belirtilmedi = 0,
        Erkek = 1,
        Bayan = 2
    }

    public enum CounterViewType
    {
        Hidden = 0,
        Single = 1,
        Multiple = 2
    }
}