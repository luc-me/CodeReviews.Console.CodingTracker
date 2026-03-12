using System;
using System.Data;
using System.Globalization;
using Spectre.Console;
using Spectre.Console.Rendering;
using SQLitePCL;

namespace CodingTracker.luc_me;

public class UIhandler
{
    internal void Flyer()
    {
        AnsiConsole.Write(new FigletText("Coding").Centered().Color(Color.Aquamarine3));
        AnsiConsole.Write(new FigletText("Tracker").Centered().Color(Color.Aqua));
    }

    internal MenuOptions MainMenu()
    {
        
        MenuOptions op = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOptions>()
        .Title("What do you want to do?")
        .AddChoices(Enum.GetValues<MenuOptions>())
        .UseConverter<MenuOptions>(options => options.ToString().Replace("_"," "))
        );
        
        return op;
    }

    internal string GetTimeInput(string message)
    {
        DateTime timeTemp;
        bool backMenu=false;
        var time = new TextPrompt<string>(message)
            .DefaultValue(DateTime.UtcNow.ToString("H:mm")).ShowDefaultValue()
            .Validate(time =>
            {
                if (time=="0")
                {
                    backMenu=true;
                    return ValidationResult.Success();
                }
                if (!DateTime.TryParseExact(time,"H:mm", null, DateTimeStyles.None, out timeTemp) || timeTemp > DateTime.UtcNow)
                    return ValidationResult.Error("The time isn't valid");
                return ValidationResult.Success();
            }
            );

        
        var input = AnsiConsole.Prompt(time);
        
        if (backMenu)
        {
            return "0";
        }
        AnsiConsole.WriteLine(input);
        return input;
    }
    internal bool TryGetDuration(string startTime, string endTime,out string duration)
    {
        duration = "";
        
        DateTime time1;
        DateTime time2;
        DateTime.TryParse(startTime,out time1);
        DateTime.TryParse(endTime,out time2);
        TimeSpan timeDuration = time2-time1;
        if (timeDuration <= TimeSpan.Zero)
        {
            AnsiConsole.WriteLine($"\nThe end time must be after the start time!\n");
            return false;
        }
            
        else
        {
            duration = timeDuration.ToString(@"hh\:mm");
            AnsiConsole.WriteLine($"\nYou were coding for {duration}!\n");
            return true;
        }
        
        
    }
    internal void ShowTable(List<Coding> sessions)
    {
        var table = new Table();
        

        table.Border(TableBorder.Rounded);  
        table.BorderColor(Color.Grey);
        
        table.Expand();
        table.Title("[bold]All sessions[/]");

        table.AddColumn("Id");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        foreach (Coding session in sessions)
        {
            table.AddRow(
                session.Id.ToString(),
                session.StartTime,
                session.EndTime,
                session.Duration
            );
        }

        AnsiConsole.Write(table);
        
    }

    internal List<Coding> SelectList(List<Coding> sessions)
    {
        var selectedSessions = AnsiConsole.Prompt(
        new MultiSelectionPrompt<Coding>()
        .Title("Select the sessions you want to delete")
        .NotRequired()
        .AddChoices(sessions)
        .UseConverter(s => $"Id: {s.Id} | {s.StartTime} -> {s.EndTime} | Duration: {s.Duration}")
        );

        return selectedSessions;
    }

    internal void CleanConsole()
    {
        AnsiConsole.WriteLine("Press a key to continue");
        Console.ReadLine();
        AnsiConsole.Clear();
        Flyer();
    }

    internal void PrintSession(Coding s)
    {
        AnsiConsole.WriteLine($"\nSession Id: {s.Id} | {s.StartTime} -> {s.EndTime} | Duration: {s.Duration}");
    }
}
